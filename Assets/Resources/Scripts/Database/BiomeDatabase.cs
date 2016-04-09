using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BiomeDatabase
{
    // Default
    public static readonly Biome Default = new Biome();


    // Biome forest
    public static readonly Biome Forest = new Biome(0, Resources.Load<Material>("Models/Islands/Materials/Forest"), Resources.Load<Material>("Models/Islands/Materials/Forest_Rock"),
        new SpawnConfig(new Entity(), 3f), new SpawnConfig(EntityDatabase.Fir, 5), new SpawnConfig(EntityDatabase.StoneRock, 3),
        new SpawnConfig(EntityDatabase.CopperRock, 0.6f), new SpawnConfig(EntityDatabase.IronRock, 0.4f), new SpawnConfig(EntityDatabase.GoldRock, 0.2f),
        new SpawnConfig(EntityDatabase.MithrilRock, 0.2f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.1f), new SpawnConfig(EntityDatabase.SunkiumRock, 0.05f),
        new SpawnConfig(EntityDatabase.Oak, 6), new SpawnConfig(EntityDatabase.ForestFlower, 4), new SpawnConfig(EntityDatabase.Branch, 4),
        new SpawnConfig(EntityDatabase.LittleRock, 2));


    // Biome Desert
    public static readonly Biome Desert = new Biome(1, Resources.Load<Material>("Models/Islands/Materials/Desert"), Resources.Load<Material>("Models/Islands/Materials/Desert_Rock"),
        new SpawnConfig(new Entity(), 3), new SpawnConfig(EntityDatabase.Cactus, 2), new SpawnConfig(EntityDatabase.StoneRock, 4),
        new SpawnConfig(EntityDatabase.CopperRock, 0.6f), new SpawnConfig(EntityDatabase.IronRock, 0.4f), new SpawnConfig(EntityDatabase.GoldRock, 0.2f),
        new SpawnConfig(EntityDatabase.MithrilRock, 0.2f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.1f), new SpawnConfig(EntityDatabase.SunkiumRock, 0.05f),
        new SpawnConfig(EntityDatabase.SmallCactus, 2), new SpawnConfig(EntityDatabase.Branch, 2), new SpawnConfig(EntityDatabase.LittleRock, 2));


    // Biome Ice
    public static readonly Biome Ice = new Biome(2, Resources.Load<Material>("Models/Islands/Materials/Ice"), Resources.Load<Material>("Models/Islands/Materials/Ice_Rock"),
        new SpawnConfig(new Entity(), 3), new SpawnConfig(EntityDatabase.SnowFir, 5), new SpawnConfig(EntityDatabase.StoneRock, 3),
        new SpawnConfig(EntityDatabase.CopperRock, 0.6f), new SpawnConfig(EntityDatabase.IronRock, 0.4f), new SpawnConfig(EntityDatabase.GoldRock, 0.2f),
        new SpawnConfig(EntityDatabase.MithrilRock, 0.2f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.1f), new SpawnConfig(EntityDatabase.SunkiumRock, 0.05f),
        new SpawnConfig(EntityDatabase.SnowOak, 7), new SpawnConfig(EntityDatabase.IceFlower, 5), new SpawnConfig(EntityDatabase.LittleRock, 3));


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
    public static Biome RandBiome(System.Random rand)
    {        
            List<Biome> biomes = new List<Biome>();
            foreach (Biome biome in Biomes)
                if (biome.ID > -1)
                    biomes.Add(biome);

            return new Biome(biomes[rand.Next(biomes.Count)]);        
    }
}
