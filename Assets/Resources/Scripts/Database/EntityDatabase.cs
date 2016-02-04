using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EntityDatabase
{
    // Default
    public static readonly Entity Default = new Entity();
    public static readonly Entity Log = new Entity(0, 60, Resources.Load<GameObject>("Prefabs/Loots/Log"));

    // Tree
    public static readonly Tree Fir = new Tree(100, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Fir"), 1);
    public static readonly Tree SnowFir = new Tree(101, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowFir"), 1);
    public static readonly Tree Cactus = new Tree(102, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Cactus"), 1);
    public static readonly Tree Oak = new Tree(103, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Oak"), 1);
    public static readonly Tree SnowOak = new Tree(104, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowOak"), 1);

    // Rock
    public static readonly Rock Stone = new Rock(110, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/Stone"), 1);

    // Chunk
    public static readonly Chunk Chunk2_One = new Chunk(1000, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_One"), Bridges.One);
    public static readonly Chunk Chunk2_TwoI = new Chunk(1001, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk2_TwoL = new Chunk(1002, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk2_Three = new Chunk(1003, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Three"), Bridges.Three);
    public static readonly Chunk Chunk2_All = new Chunk(1004, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_All"), Bridges.All);

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
            yield return Stone;
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
            yield return Chunk2_All;
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
                else
                    return new Entity(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
