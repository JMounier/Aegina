using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class MapGeneration : NetworkBehaviour
{
    private Save save;
    private Dictionary<string, bool> loaded;
    private Heap<Tuple<int, int>> toSpawn;
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
            foreach (string key in this.loaded.Keys)
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
                            {
                                this.toSpawn.Insert(Mathf.Abs(i - x) + Mathf.Abs(j - y), new Tuple<int, int>(i, j));
                                this.loaded.Add(key, true);
                            }
                            this.loaded[key] = true;
                        }
            }

            if (!this.toSpawn.IsEmpty)
            {
                Tuple<int, int> chunk = this.toSpawn.ExtractMin().Item2;
                GenerateChunk(chunk.Item1, chunk.Item2);
            }
        }
    }

    private void GenerateChunk(int x, int y)
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
            EntityDatabase.RandChunk(Bridges.None, rand).Generate(x, y, rand, (Directions)rand.Next(4), gameObject, cristal);
        // ONE
        else if (up && !right && !down && !left)
            EntityDatabase.RandChunk(Bridges.One, rand).Generate(x, y, rand, Directions.North, gameObject, cristal);
        else if (!up && right && !down && !left)
            EntityDatabase.RandChunk(Bridges.One, rand).Generate(x, y, rand, Directions.East, gameObject, cristal);
        else if (!up && !right && down && !left)
            EntityDatabase.RandChunk(Bridges.One, rand).Generate(x, y, rand, Directions.South, gameObject, cristal);
        else if (!up && !right && !down && left)
            EntityDatabase.RandChunk(Bridges.One, rand).Generate(x, y, rand, Directions.West, gameObject, cristal);
        // TWO_I
        else if (up && !right && down && !left)
            EntityDatabase.RandChunk(Bridges.TwoI, rand).Generate(x, y, rand, (Directions)(rand.Next(2) * 2), gameObject, cristal);
        else if (!up && right && !down && left)
            EntityDatabase.RandChunk(Bridges.TwoI, rand).Generate(x, y, rand, (Directions)(rand.Next(2) * 2 + 1), gameObject, cristal);
        // TWO_L
        else if (up && right && !down && !left)
            EntityDatabase.RandChunk(Bridges.TwoL, rand).Generate(x, y, rand, Directions.North, gameObject, cristal);
        else if (!up && right && down && !left)
            EntityDatabase.RandChunk(Bridges.TwoL, rand).Generate(x, y, rand, Directions.East, gameObject, cristal);
        else if (!up && !right && down && left)
            EntityDatabase.RandChunk(Bridges.TwoL, rand).Generate(x, y, rand, Directions.South, gameObject, cristal);
        else if (up && !right && !down && left)
            EntityDatabase.RandChunk(Bridges.TwoL, rand).Generate(x, y, rand, Directions.West, gameObject, cristal);
        // THREE
        else if (up && right && down && !left)
            EntityDatabase.RandChunk(Bridges.Three, rand).Generate(x, y, rand, Directions.North, gameObject, cristal);
        else if (!up && right && down && left)
            EntityDatabase.RandChunk(Bridges.Three, rand).Generate(x, y, rand, Directions.East, gameObject, cristal);
        else if (up && !right && down && left)
            EntityDatabase.RandChunk(Bridges.Three, rand).Generate(x, y, rand, Directions.South, gameObject, cristal);
        else if (up && right && !down && left)
            EntityDatabase.RandChunk(Bridges.Three, rand).Generate(x, y, rand, Directions.West, gameObject, cristal);
        else
            EntityDatabase.RandChunk(Bridges.All, rand).Generate(x, y, rand, (Directions)rand.Next(4), gameObject, cristal);
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
