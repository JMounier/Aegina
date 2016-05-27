using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class MapGeneration : NetworkBehaviour
{
    private Save save;
    private Dictionary<string, Chunk> generated;
    private Chunk generating;

    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            this.save = gameObject.GetComponent<Save>();
            this.generated = new Dictionary<string, Chunk>();

            if (this.save.IsCoop)
            {
                // TO DO 
                this.generating = GenerateChunk(0, 0);
            }
            else
            {
                // TO DO
                this.generating = GenerateChunk(0, 0);
            }
        }
    }

    void Update()
    {
        if (!isServer)
            return;

        // Position des joueurs
        List<Tuple<int, int>> posPlayers = new List<Tuple<int, int>>();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            int x = (int)Mathf.Round(player.transform.FindChild("Character").position.x / Chunk.Size);
            int y = (int)Mathf.Round(player.transform.FindChild("Character").position.z / Chunk.Size);
            posPlayers.Add(new Tuple<int, int>(x, y));
        }
        // Recherche un chunk a generer
        if (this.generating == null)
        {
            int min = int.MaxValue;
            Tuple<int, int> best = null;
            foreach (Tuple<int, int> pos in posPlayers)
            {
                int x = pos.Item1;
                int y = pos.Item2;               

                for (int i = x - 1; i < x + 2; i++)
                    for (int j = y - 1; j < y + 2; j++)
                    {
                        int dist = Mathf.Abs(i - x) + Mathf.Abs(j - y);
                        string key = i.ToString() + ":" + j.ToString();
                        if (dist < min && !this.generated.ContainsKey(key))
                        {
                            min = dist;
                            best = new Tuple<int, int>(i, j);
                        }
                    }             
            }
            if (best != null)
                this.generating = GenerateChunk(best.Item1, best.Item2);
        }
        else if (this.generating.Generate())
        {

            this.generated.Add(this.generating.X.ToString() + ":" + this.generating.Y.ToString(), this.generating);
            this.generating = null;
        }

        // Degeneration
        List<Chunk> toDespawn = new List<Chunk>();

        foreach (string key in this.generated.Keys)
        {
            int i = 0;
            Chunk c = this.generated[key];
            bool check = false;

            while (!check && i < posPlayers.Count)
            {
                int xP = posPlayers[i].Item1;
                int yP = posPlayers[i].Item2;
                if (Mathf.Abs(xP - c.X) < 2 && Mathf.Abs(yP - c.Y) < 2)
                    check = true;
                i++;
            }
            if (!check)
                toDespawn.Add(c);
        }
        foreach (Chunk c in toDespawn)
        {
            this.save.RemoveChunk(c.X, c.Y);
            this.generated.Remove(c.X.ToString() + ":" + c.Y.ToString());
            NetworkServer.UnSpawn(c.Prefab);
            GameObject.Destroy(c.Prefab);
        }

    }

    private Chunk GenerateChunk(int x, int y)
    {
        this.save.AddChunk(x, y);
        bool left = true, up = true, right = true, down = true, cristal = true;
        System.Random rand = new System.Random(chunkSeed(x, y));
        if (x != 0 || y != 0)
        {
            left = rand.NextDouble() < .5f || x == 1 && y == 0;
            up = rand.NextDouble() < .5f || x == 0 && y == -1;
            System.Random randRight = new System.Random(chunkSeed(x + 1, y));
            right = randRight.NextDouble() < .5f || x == -1 && y == 0;
            System.Random randDown = new System.Random(chunkSeed(x, y - 1));
            randDown.NextDouble();
            down = randDown.NextDouble() < .5f || x == 0 && y == 1;
            cristal = rand.NextDouble() < .1f || x == 0 && y == 0;
        }
        if (!up && !right && !down && !left)
            return EntityDatabase.RandChunk(Bridges.None, rand).StartGenerate(x, y, rand, (Directions)rand.Next(4), gameObject, cristal);
        // ONE
        if (up && !right && !down && !left)
            return EntityDatabase.RandChunk(Bridges.One, rand).StartGenerate(x, y, rand, Directions.North, gameObject, cristal);
        if (!up && right && !down && !left)
            return EntityDatabase.RandChunk(Bridges.One, rand).StartGenerate(x, y, rand, Directions.East, gameObject, cristal);
        if (!up && !right && down && !left)
            return EntityDatabase.RandChunk(Bridges.One, rand).StartGenerate(x, y, rand, Directions.South, gameObject, cristal);
        if (!up && !right && !down && left)
            return EntityDatabase.RandChunk(Bridges.One, rand).StartGenerate(x, y, rand, Directions.West, gameObject, cristal);
        // TWO_I
        if (up && !right && down && !left)
            return EntityDatabase.RandChunk(Bridges.TwoI, rand).StartGenerate(x, y, rand, (Directions)(rand.Next(2) * 2), gameObject, cristal);
        if (!up && right && !down && left)
            return EntityDatabase.RandChunk(Bridges.TwoI, rand).StartGenerate(x, y, rand, (Directions)(rand.Next(2) * 2 + 1), gameObject, cristal);
        // TWO_L
        if (up && right && !down && !left)
            return EntityDatabase.RandChunk(Bridges.TwoL, rand).StartGenerate(x, y, rand, Directions.North, gameObject, cristal);
        if (!up && right && down && !left)
            return EntityDatabase.RandChunk(Bridges.TwoL, rand).StartGenerate(x, y, rand, Directions.East, gameObject, cristal);
        if (!up && !right && down && left)
            return EntityDatabase.RandChunk(Bridges.TwoL, rand).StartGenerate(x, y, rand, Directions.South, gameObject, cristal);
        if (up && !right && !down && left)
            return EntityDatabase.RandChunk(Bridges.TwoL, rand).StartGenerate(x, y, rand, Directions.West, gameObject, cristal);
        // THREE
        if (up && right && down && !left)
            return EntityDatabase.RandChunk(Bridges.Three, rand).StartGenerate(x, y, rand, Directions.North, gameObject, cristal);
        if (!up && right && down && left)
            return EntityDatabase.RandChunk(Bridges.Three, rand).StartGenerate(x, y, rand, Directions.East, gameObject, cristal);
        if (up && !right && down && left)
            return EntityDatabase.RandChunk(Bridges.Three, rand).StartGenerate(x, y, rand, Directions.South, gameObject, cristal);
        if (up && right && !down && left)
            return EntityDatabase.RandChunk(Bridges.Three, rand).StartGenerate(x, y, rand, Directions.West, gameObject, cristal);
        return EntityDatabase.RandChunk(Bridges.All, rand).StartGenerate(x, y, rand, (Directions)rand.Next(4), gameObject, cristal);
    }

    /// <summary>
    /// Retourne le seed propre a un chunk avec sa position et le seed du monde.
    /// </summary>
    /// <param name="x">Position x du chunk.</param>
    /// <param name="y">Position y du chunk.</param>
    /// <param name="seedWord">Seed du monde.</param>
    /// <returns></returns>
    private int chunkSeed(int x, int y)
    {
        return (467 * x - 131 * y + 1) * this.save.Seed;
    }

    /// <summary>
    /// Retourne si le chunk de coordonnee X, Y est charge.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool isLoaded(int x, int y)
    {
        return this.generated.ContainsKey(x.ToString() + ":" + y.ToString());
    }

    /// <summary>
    /// Transform a seed in string to int.
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static int SeedToInt(string seed)
    {
        seed = seed.ToLower();
        int s = 0;
        int power = 0;
        for (int i = seed.Length - 1; i > -1; i--)
        {
            if (seed[i] >= '0' && seed[i] <= '9')
                s += (int)((seed[i] - '0') * Mathf.Pow(36, power));
            if (seed[i] >= 'a' && seed[i] <= 'z')
                s += (int)((seed[i] - 'a' + 10) * Mathf.Pow(36, power));
            power++;
        }
        return s;
    }

    /// <summary>
    /// Transform a seed in int to string.
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static string SeedToString(int seed)
    {
        seed = Mathf.Abs(seed);
        string s = "";
        string tab = "0123456789abcdefghijklmnopqrstuvwxyz";
        while (seed > 0)
        {
            s = tab[seed % 36] + s;
            seed /= 36;
        }
        return s;
    }
}
