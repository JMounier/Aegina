using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EntityDatabase
{
    // Default
    public static readonly Entity Default = new Entity();
    // Loot
    public static readonly Entity Log = new Entity(0, 60, Resources.Load<GameObject>("Prefabs/Loots/Log"));
    public static readonly Entity Stone = new Entity(1, 60, Resources.Load<GameObject>("Prefabs/Loots/Stone"));
    public static readonly Entity Sand = new Entity(2, 60, Resources.Load<GameObject>("Prefabs/Loots/Sand"));
    public static readonly Entity Copper = new Entity(3, 60, Resources.Load<GameObject>("Prefabs/Loots/Copper"));
    public static readonly Entity Iron = new Entity(4, 60, Resources.Load<GameObject>("Prefabs/Loots/Iron"));
    public static readonly Entity Gold = new Entity(5, 60, Resources.Load<GameObject>("Prefabs/Loots/Gold"));
    public static readonly Entity Mithril = new Entity(6, 60, Resources.Load<GameObject>("Prefabs/Loots/Mithril"));
    public static readonly Entity Floatium = new Entity(7, 60, Resources.Load<GameObject>("Prefabs/Loots/Floatium"));
    public static readonly Entity Sunkium = new Entity(8, 60, Resources.Load<GameObject>("Prefabs/Loots/Sunkium"));

    public static readonly Entity CopperIngot = new Entity(9, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperIngot"));
    public static readonly Entity IronIngot = new Entity(10, 60, Resources.Load<GameObject>("Prefabs/Loots/IronIngot"));
    public static readonly Entity GoldIngot = new Entity(11, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldIngot"));
    public static readonly Entity MithrilIngot = new Entity(12, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilIngot"));
    public static readonly Entity FloatiumIngot = new Entity(13, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumIngot"));
    public static readonly Entity SunkiumIngot = new Entity(14, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumIngot"));

    public static readonly Entity CopperPickaxe = new Entity(52, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperPickaxe"));
    public static readonly Entity IronPickaxe = new Entity(53, 60, Resources.Load<GameObject>("Prefabs/Loots/IronPickaxe"));
    public static readonly Entity GoldPickaxe = new Entity(54, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldPickaxe"));
    public static readonly Entity MithrilPickaxe = new Entity(55, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilPickaxe"));
    public static readonly Entity FloatiumPickaxe = new Entity(56, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumPickaxe"));
    public static readonly Entity SunkiumPickaxe = new Entity(57, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumPickaxe"));

    // SmallElements
    public static readonly Element Branch = new Element(90, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/Branch"), 1);
    public static readonly Element ForestFlower = new Element(91, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/ForestFlower"), 1);
    public static readonly Element IceFlower = new Element(92, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/IceFlower"), 1);
    public static readonly Element SmallCactus = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/SmallCactus"), 1);
    public static readonly Element LittleRock = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/LittleRock"), 1);

    // Tree
    public static readonly Tree Fir = new Tree(100, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Fir"), 1);
    public static readonly Tree SnowFir = new Tree(101, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowFir"), 1);
    public static readonly Tree Cactus = new Tree(102, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Cactus"), 1);
    public static readonly Tree Oak = new Tree(103, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Oak"), 1);
    public static readonly Tree SnowOak = new Tree(104, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowOak"), 1);

    // Rock
    public static readonly Rock StoneRock = new Rock(110, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/Stone"), 1);

    // Chunk
    public static readonly Chunk Chunk2_One = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_One"), Bridges.One);
    public static readonly Chunk Chunk2_TwoI = new Chunk(1006, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk2_TwoL = new Chunk(1007, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk2_Three = new Chunk(1008, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Three"), Bridges.Three);
    public static readonly Chunk Chunk2_All = new Chunk(1009, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_All"), Bridges.All);

    // IslandCore
    public static readonly IslandCore IslandCore = new IslandCore(42, Resources.Load<GameObject>("Prefabs/Elements/Cristals/IslandCore"), Team.Neutre, 0, 0, 0);

    /// <summary>
    /// Liste tous les entites du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Entity> Entitys
    {
        get
        {
            // Default
            yield return Default;
            yield return Log;

            // Tree
            foreach (Tree tree in Trees)
                yield return tree;

            // Rocks
            foreach (Rock rock in Rocks)
                yield return rock;

            // Chunk
            foreach (Chunk chunk in Chunks)
                yield return chunk;

            // IslandCore
            foreach (Element el in Elements)
                yield return el;
        }
    }

    /// <summary>
    /// Liste tous les elements du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Element> Elements
    {
        get
        {
            yield return IslandCore;

            // Flowers
            yield return Branch;
            yield return ForestFlower;
            yield return IceFlower;
            yield return SmallCactus;
            yield return LittleRock;
        }
    }

    /// <summary>
    /// Liste tous les arbres du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Tree> Trees
    {
        get
        {
            yield return Fir;
            yield return SnowFir;
            yield return Cactus;
            yield return Oak;
            yield return SnowOak;
        }
    }

    /// <summary>
    /// Liste tous les roches du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Rock> Rocks
    {
        get
        {
            yield return StoneRock;
        }
    }

    /// <summary>
    /// Liste tous les chunks du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Chunk> Chunks
    {
        get
        {
            yield return Chunk2_One;
            yield return Chunk2_TwoI;
            yield return Chunk2_TwoL;
            yield return Chunk2_Three;           
        }
    }

    /// <summary>
    /// Retourne un chunk aleatoire. (Une copie)
    /// </summary>
    public static Chunk RandChunk(Bridges bridge)
    {
        List<Chunk> chunks = new List<Chunk>();
        foreach (Chunk c in Chunks)
            if (c.Bridge == bridge)
                chunks.Add(c);

        return new Chunk(chunks[Random.Range(0, chunks.Count)]);
    }

    /// <summary>
    /// Recherche une entite par son identifiant et la retourne. (Une copie)
    /// </summary>
    public static Entity Find(int id)
    {
        foreach (Entity i in Entitys)
        {
            if (i.ID == id)
            {
                if (i is Tree)
                    return new Tree((Tree)i);
                else if (i is Rock)
                    return new Rock((Rock)i);
                else if (i is Chunk)
                    return new Chunk((Chunk)i);
                else if (i is IslandCore)
                    return new IslandCore((IslandCore)i);
                else
                    return new Entity(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
