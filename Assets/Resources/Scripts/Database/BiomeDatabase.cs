using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BiomeDatabase
{
    // Default
    public static readonly Biome Default = new Biome();


    // Biome forest
    public static readonly Biome Forest = new Biome(0, Resources.Load<Material>("Models/Islands/Materials/Forest"), Resources.Load<Material>("Models/Islands/Materials/Forest_Rock"),
        new SpawnConfig(new Entity(), .5f), new SpawnConfig(EntityDatabase.Fir, 1), new SpawnConfig(EntityDatabase.StoneRock, 1),
        new SpawnConfig(EntityDatabase.Oak, 2), new SpawnConfig(EntityDatabase.ForestFlower, 1), new SpawnConfig(EntityDatabase.Branch, 1),
        new SpawnConfig(EntityDatabase.LittleRock, 1));


    // Biome Desert
    public static readonly Biome Desert = new Biome(1, Resources.Load<Material>("Models/Islands/Materials/Desert"), Resources.Load<Material>("Models/Islands/Materials/Desert_Rock"),
        new SpawnConfig(new Entity(), 1f), new SpawnConfig(EntityDatabase.Cactus, 1), new SpawnConfig(EntityDatabase.StoneRock, 1),
        new SpawnConfig(EntityDatabase.SmallCactus, 1.5f), new SpawnConfig(EntityDatabase.Branch, 2), new SpawnConfig(EntityDatabase.LittleRock, 1));


    // Biome Ice
    public static readonly Biome Ice = new Biome(2, Resources.Load<Material>("Models/Islands/Materials/Ice"), Resources.Load<Material>("Models/Islands/Materials/Ice_Rock"),
        new SpawnConfig(new Entity(), .5f), new SpawnConfig(EntityDatabase.SnowFir, 3), new SpawnConfig(EntityDatabase.StoneRock, 1),
        new SpawnConfig(EntityDatabase.SnowOak, 2), new SpawnConfig(EntityDatabase.IceFlower, 1), new SpawnConfig(EntityDatabase.LittleRock, 1));


    /// <summary>
    /// Liste tous les biomes du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Biome> Biomes
    {
        get
        {
            // Default
            yield return Default;
            yield return Forest;
            yield return Ice;
            yield return Desert;
        }
    }

    /// <summary>
    /// Retourne le biome correspondant. (En copie)
    /// </summary>
    public static Biome Find(int id)
    {
        foreach (Biome biome in Biomes)
        {
            if (biome.ID == id)
                return new Biome(biome);
        }
        throw new System.Exception("Items.Find : Item not find");
    }

    /// <summary>
    /// Retourne un biome random. (En copie)
    /// </summary>
    public static Biome RandBiome
    {
        get
        {
            List<Biome> biomes = new List<Biome>();
            foreach (Biome biome in Biomes)
                if (biome.ID > -1)
                    biomes.Add(biome);

            return new Biome(biomes[Random.Range(0, biomes.Count)]);
        }
    }
}
