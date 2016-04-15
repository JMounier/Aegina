using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{
    Save save;

    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            this.save = gameObject.GetComponent<Save>();
            this.GenerateChunk(0, 0, Bridges.Three, Directions.East,true);
            this.GenerateChunk(1, 0, Bridges.All, Directions.North);
            this.GenerateChunk(2, 0, Bridges.One, Directions.West);
            this.GenerateChunk(-1, 0, Bridges.Three, Directions.East);
            this.GenerateChunk(-2, 0, Bridges.One, Directions.East);
            this.GenerateChunk(0, 1, Bridges.Three, Directions.West);
            this.GenerateChunk(0, 2, Bridges.One, Directions.South);
            this.GenerateChunk(-1, 1, Bridges.One, Directions.East);
            this.GenerateChunk(1, 1, Bridges.TwoL, Directions.South);
            this.GenerateChunk(1, -1, Bridges.TwoL, Directions.West);
            this.GenerateChunk(-1, -1, Bridges.TwoL, Directions.North);
            this.GenerateChunk(0, -1, Bridges.All, Directions.East);
            this.GenerateChunk(0, -2, Bridges.One, Directions.North);          
        }
    }

    private void GenerateChunk(int x, int y, Bridges bridge, Directions dir, bool islandCore = false)
    {
        System.Random rand = new System.Random(chunkSeed(x, y, this.save.Seed));
        EntityDatabase.RandChunk(bridge, rand).Generate(x, y, rand, dir, gameObject, islandCore);
    }

    /// <summary>
    /// Retourne le seed propre a un chunk avec sa position et le seed du monde.
    /// </summary>
    /// <param name="x">Position x du chunk.</param>
    /// <param name="y">Position y du chunk.</param>
    /// <param name="seedWord">Seed du monde.</param>
    /// <returns></returns>
    private int chunkSeed(int x, int y, int seedWord)
    {
        return (467 * x - 131 * y + 1) * seedWord;
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
                s += (int) ((seed[i] - '0') * Mathf.Pow(36, power));
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
