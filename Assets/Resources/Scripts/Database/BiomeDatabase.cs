using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BiomeDatabase
{
    // Default
    public static readonly Biome Default = new Biome();

    // To do...
    public static readonly Biome Forest = new Biome(0, new SpawnConfig(new Entity(), 1), new SpawnConfig(new Entity(EntityDatabase.Fir), 1), new SpawnConfig(new Entity(EntityDatabase.SnowFir), 3));
    public static readonly Biome Desert = new Biome(1, new Entity());
    public static readonly Biome Ice = new Biome(2, new Entity());


    /// <summary>
    /// Liste tous les biomes du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Biome> Biomes
    {
        get
        {
            // Default
            yield return new Biome(Default);
            yield return new Biome(Forest);
            yield return new Biome(Ice);
        }
    }

    /// <summary>
    /// Retourne l'item correspondant.
    /// </summary>
    public static Biome Find(int id)
    {
        foreach (Biome biome in Biomes)
        {
            if (biome.ID == id)
                return biome;
        }
        throw new System.Exception("Items.Find : Item not find");
    }       
}
