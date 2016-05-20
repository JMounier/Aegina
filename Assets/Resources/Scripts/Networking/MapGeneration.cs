using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class MapGeneration : NetworkBehaviour
{
    private Save save;
    private Dictionary<string, bool> loaded;
    private Heap<Tuple<int, int>> toSpawn;
    private Chunk generating;

    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            this.save = gameObject.GetComponent<Save>();
            this.loaded = new Dictionary<string, bool>();
            this.toSpawn = new Heap<Tuple<int, int>>();
            

            if (this.save.IsCoop)
            {
                // TO DO 
                this.generating = GenerateChunk(0, 0);
                this.loaded["0:0"] = true;              
            }
            else
            {
                // TO DO
            }
        }
    }

    void Update()
    {
        if (isServer)
        {
            List<string> l = new List<string>();
            foreach (string key in this.loaded.Keys)
                l.Add(key);
            foreach (string key in l)
                this.loaded[key] = false;

            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                int x = (int)Mathf.Round(player.transform.FindChild("Character").position.x / Chunk.Size);
                int y = (int)Mathf.Round(player.transform.FindChild("Character").position.z / Chunk.Size);
                for (int i = x - 2; i < x + 3; i++)
                    for (int j = y - 2; j < y + 3; j++)
                        if (Mathf.Abs(i - x) + Mathf.Abs(j - y) < 2)
                        {
                            string key = i.ToString() + ":" + j.ToString();
                            if (!this.loaded.ContainsKey(key))
                                this.toSpawn.Insert(Mathf.Abs(i - x) + Mathf.Abs(j - y), new Tuple<int, int>(i, j));
                            this.loaded[key] = true;
                        }
            }
            if (this.generating != null)
            {
                if (this.generating.Generate())
                    this.generating = null;
            }
            else if (!this.toSpawn.IsEmpty)
            {
                Tuple<int, int> chunk = this.toSpawn.ExtractMin().Item2;
                this.generating = GenerateChunk(chunk.Item1, chunk.Item2);
            }
        }
    }

    private Chunk GenerateChunk(int x, int y)
    {
        this.save.AddChunk(x, y);
        bool left = true, up = true, right = true, down = true, cristal = true;
        System.Random rand = new System.Random(chunkSeed(x, y));
        if (x != 0 || y != 0)
        {
            left = x == 1 && y == 0 || rand.NextDouble() < .5f;
            up = x == 0 && y == -1 || rand.NextDouble() < .5f;
            System.Random randRight = new System.Random(chunkSeed(x + 1, y));
            right = x == -1 && y == 0 || randRight.NextDouble() < .5f;
            System.Random randDown = new System.Random(chunkSeed(x, y - 1));
            randDown.NextDouble();
            down = x == 0 && y == 1 || randDown.NextDouble() < .5f;
            cristal = x == 0 && y == 0 || rand.NextDouble() < .1f;
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
