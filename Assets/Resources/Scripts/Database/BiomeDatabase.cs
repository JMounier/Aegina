﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BiomeDatabase
{
    // Default
    public static readonly Biome Default = new Biome();


    // Biome forest
    public static readonly Biome Forest = new Biome(0, Resources.Load<Material>("Models/Islands/Materials/Forest"), Resources.Load<Material>("Models/Islands/Materials/Forest_Rock"),
        new SpawnConfig(new Entity(), 3f),
    #region Tree
        new SpawnConfig(EntityDatabase.Fir, 5), new SpawnConfig(EntityDatabase.Oak, 6),
    #endregion
    #region Rock
        new SpawnConfig(EntityDatabase.StoneRock, 3), new SpawnConfig(EntityDatabase.CopperRock, 1.5f), new SpawnConfig(EntityDatabase.IronRock, 1f),
        new SpawnConfig(EntityDatabase.GoldRock, 0.8f), new SpawnConfig(EntityDatabase.MithrilRock, 0.5f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.5f),
        new SpawnConfig(EntityDatabase.SunkiumRock, 0.2f),
    #endregion
    #region Small Element
        new SpawnConfig(EntityDatabase.ForestFlower, 4), new SpawnConfig(EntityDatabase.Branch, 4), new SpawnConfig(EntityDatabase.LittleRock, 2));
    #endregion

    // Biome Desert
    public static readonly Biome Desert = new Biome(1, Resources.Load<Material>("Models/Islands/Materials/Desert"), Resources.Load<Material>("Models/Islands/Materials/Desert_Rock"),
        new SpawnConfig(new Entity(), 3),
    #region Tree
        new SpawnConfig(EntityDatabase.Cactus, 2),
    #endregion
    #region Rock
        new SpawnConfig(EntityDatabase.StoneRock, 3), new SpawnConfig(EntityDatabase.CopperRock, 1.5f), new SpawnConfig(EntityDatabase.IronRock, 1f),
        new SpawnConfig(EntityDatabase.GoldRock, 0.8f), new SpawnConfig(EntityDatabase.MithrilRock, 0.5f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.5f),
        new SpawnConfig(EntityDatabase.SunkiumRock, 0.2f),

    #endregion
    #region Small Element
        new SpawnConfig(EntityDatabase.SmallCactus, 2), new SpawnConfig(EntityDatabase.Branch, 2), new SpawnConfig(EntityDatabase.LittleRock, 1));
    #endregion

    // Biome Ice
    public static readonly Biome Ice = new Biome(2, Resources.Load<Material>("Models/Islands/Materials/Ice"), Resources.Load<Material>("Models/Islands/Materials/Ice_Rock"),
        new SpawnConfig(new Entity(), 3),
    #region Tree
        new SpawnConfig(EntityDatabase.SnowFir, 5), new SpawnConfig(EntityDatabase.SnowOak, 7),
    #endregion
    #region Rock
        new SpawnConfig(EntityDatabase.StoneRock, 3), new SpawnConfig(EntityDatabase.CopperRock, 1.5f), new SpawnConfig(EntityDatabase.IronRock, 1f),
        new SpawnConfig(EntityDatabase.GoldRock, 0.8f), new SpawnConfig(EntityDatabase.MithrilRock, 0.5f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.5f),
        new SpawnConfig(EntityDatabase.SunkiumRock, 0.2f),
    #endregion
    #region Small Element
        new SpawnConfig(EntityDatabase.IceFlower, 5), new SpawnConfig(EntityDatabase.LittleRock, 3), new SpawnConfig(EntityDatabase.Branch, 2));
    #endregion

    // Biome Fall
    public static readonly Biome Fall = new Biome(3, Resources.Load<Material>("Models/Islands/Materials/Fall"), Resources.Load<Material>("Models/Islands/Materials/Fall_Rock"),
        new SpawnConfig(new Entity(), 3f),
    #region Tree
        new SpawnConfig(EntityDatabase.Fir, 0.5f), new SpawnConfig(EntityDatabase.Oak, 0.5f), new SpawnConfig(EntityDatabase.FallOak1, 6), new SpawnConfig(EntityDatabase.FallOak2, 4),
    #endregion
    #region Rock
        new SpawnConfig(EntityDatabase.StoneRock, 3), new SpawnConfig(EntityDatabase.CopperRock, 0.6f), new SpawnConfig(EntityDatabase.IronRock, 0.4f),
        new SpawnConfig(EntityDatabase.GoldRock, 0.2f), new SpawnConfig(EntityDatabase.MithrilRock, 0.2f), new SpawnConfig(EntityDatabase.FloatiumRock, 0.1f),
        new SpawnConfig(EntityDatabase.SunkiumRock, 0.05f),
    #endregion
    #region Small Element
        new SpawnConfig(EntityDatabase.ForestFlower, 1), new SpawnConfig(EntityDatabase.Branch, 5), new SpawnConfig(EntityDatabase.RedMushroom, 2),
		new SpawnConfig(EntityDatabase.Mushroom, 2), new SpawnConfig(EntityDatabase.LittleRock, 2), new SpawnConfig(EntityDatabase.Pumpink, 2));
    #endregion


    /// <summary>
    /// Liste tous les biomes du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Biome> Biomes
    {
        get
        {
            yield return Default;
            yield return Forest;
            yield return Ice;
            yield return Desert;
            yield return Fall;
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
