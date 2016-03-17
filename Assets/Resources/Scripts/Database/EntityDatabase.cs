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
    public static readonly Element Branch = new Element(90, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/Branch"), Element.TypeElement.Small);
    public static readonly Element ForestFlower = new Element(91, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/ForestFlower"), Element.TypeElement.Small);
    public static readonly Element IceFlower = new Element(92, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/IceFlower"), Element.TypeElement.Small);
    public static readonly Element SmallCactus = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/SmallCactus"), Element.TypeElement.Small);
    public static readonly Element LittleRock = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/LittleRock"), Element.TypeElement.Small);

    // Tree
    public static readonly Element Fir = new Element(100, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Fir"), Element.TypeElement.Tree, new DropConfig(ItemDatabase.Find(0), 1, 5));
    public static readonly Element SnowFir = new Element(101, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowFir"), Element.TypeElement.Tree);
    public static readonly Element Cactus = new Element(102, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Cactus"), Element.TypeElement.Tree);
    public static readonly Element Oak = new Element(103, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Oak"), Element.TypeElement.Tree);
    public static readonly Element SnowOak = new Element(104, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowOak"), Element.TypeElement.Tree);

    // Rock
    public static readonly Element StoneRock = new Element(110, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/Stone"), Element.TypeElement.Rock, new DropConfig(ItemDatabase.Find(1), 1, 5));

    // IslandCore
    public static readonly IslandCore IslandCore = new IslandCore(142, Resources.Load<GameObject>("Prefabs/Elements/Cristals/IslandCore"), Team.Neutre, 0, 0, 0);

    // Mobs
    public static readonly Mob Boar = new Mob(500, 100, Resources.Load<GameObject>("Prefabs/Mobs/Boar"), 5, 10, 10, 1f, 1.5f);

    // Chunk
    public static readonly Chunk Chunk1_One = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_One"), Bridges.One);
    public static readonly Chunk Chunk1_TwoI = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk1_TwoL = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk1_Three = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_Three"), Bridges.Three);
    public static readonly Chunk Chunk1_All = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_All"), Bridges.All);

    public static readonly Chunk Chunk2_One = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_One"), Bridges.One);
    public static readonly Chunk Chunk2_TwoI = new Chunk(1006, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk2_TwoL = new Chunk(1007, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk2_Three = new Chunk(1008, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Three"), Bridges.Three);
    public static readonly Chunk Chunk2_All = new Chunk(1009, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_All"), Bridges.All);



    /// <summary>
    /// Liste tous les entites du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Entity> Entitys
    {
        get
        {
            // Default
            yield return Default;
            // Material
            yield return Log;
            yield return Stone;
            yield return Sand;
            yield return Iron;
            yield return Copper;
            yield return Gold;
            yield return Mithril;
            yield return Floatium;
            yield return Sunkium;

            //Ingot
            yield return IronIngot;
            yield return CopperIngot;
            yield return GoldIngot;
            yield return MithrilIngot;
            yield return FloatiumIngot;
            yield return SunkiumIngot;

            //Pickaxe
            yield return IronPickaxe;
            yield return CopperPickaxe;
            yield return GoldPickaxe;
            yield return MithrilPickaxe;
            yield return FloatiumPickaxe;
            yield return SunkiumPickaxe;

            // Chunk
            foreach (Chunk chunk in Chunks)
                yield return chunk;

            // IslandCore
            foreach (Element el in Elements)
                yield return el;

            // Mobs
            foreach (Mob mob in Mobs)
                yield return mob;
        }
    }

    /// <summary>
    /// Liste tous les elements du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Element> Elements
    {
        get
        {
            // Flowers
            yield return Branch;
            yield return ForestFlower;
            yield return IceFlower;
            yield return SmallCactus;
            yield return LittleRock;

            // Tree
            yield return Fir;
            yield return SnowFir;
            yield return Cactus;
            yield return Oak;
            yield return SnowOak;

            // Rocks
            yield return StoneRock;

            // IslandCore
            yield return IslandCore;
        }
    }

    /// <summary>
    /// Liste tous les chunks du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Chunk> Chunks
    {
        get
        {

            yield return Chunk1_One;
            yield return Chunk1_TwoI;
            yield return Chunk1_TwoL;
            yield return Chunk1_Three;
            yield return Chunk1_All;

            yield return Chunk2_One;
            yield return Chunk2_TwoI;
            yield return Chunk2_TwoL;
            yield return Chunk2_Three;
            yield return Chunk2_All;
        }
    }

    /// <summary>
    /// Liste tous les mobs du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Mob> Mobs
    {
        get
        {
            yield return Boar;
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
                if (i is Chunk)
                    return new Chunk((Chunk)i);
                else if (i is IslandCore)
                    return new IslandCore((IslandCore)i);
                else if (i is Element)
                    return new Element((Element)i);
                else
                    return new Entity(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
