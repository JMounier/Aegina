using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BiomeDatabase
{
    /*
        TO INCREMENT WHEN YOU ADD A BIOME ! ! ! ! !
    */
    private static int nbBiome = 3;

    // Default
    public static readonly Biome Default = new Biome();


    // Biome forest
    public static readonly Biome Forest = new Biome(0,Resources.Load<Material>("Models/Islands/Materials/Forest") , Resources.Load<Material>("Models/Islands/Materials/Forest_Rock"), 
        new SpawnConfig(new Entity(), 1.5f), new SpawnConfig(new Entity(EntityDatabase.Fir), 1), new SpawnConfig(new Entity(EntityDatabase.Stone), 1),
        new SpawnConfig(new Entity(EntityDatabase.Oak), 2));


    // Biome Desert
    public static readonly Biome Desert = new Biome(1, Resources.Load<Material>("Models/Islands/Materials/Desert"), Resources.Load<Material>("Models/Islands/Materials/Desert_Rock"), 
        new SpawnConfig(new Entity(), 2f), new SpawnConfig(new Entity(EntityDatabase.Cactus), 2), new SpawnConfig(new Entity(EntityDatabase.Stone), 1));


    // Biome Ice
    public static readonly Biome Ice = new Biome(2, Resources.Load<Material>("Models/Islands/Materials/Ice"), Resources.Load<Material>("Models/Islands/Materials/Ice_Rock"),
        new SpawnConfig(new Entity(), 1.5f), new SpawnConfig(new Entity(EntityDatabase.SnowFir), 3), new SpawnConfig(new Entity(EntityDatabase.Stone), 1),
        new SpawnConfig(new Entity(EntityDatabase.SnowOak), 2));


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
            yield return new Biome(Desert);
        }
    }

    /// <summary>
    /// Retourne le biome correspondant.
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
    
    public static Biome RandBiome
    {
        get
        {
            int ran = Random.Range(0, nbBiome);
            return Find(ran);
        }
     }  
}
