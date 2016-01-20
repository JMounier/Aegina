using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EntityDatabase
{
    // Default
    public static readonly Entity Default = new Entity();

    // Loot

    // Tree
    public static readonly Tree Fir = new Tree(100, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Fir"), 1);
    public static readonly Tree SnowFir = new Tree(101, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowFir"), 1);

    // Rock

    // Chunk
    public static readonly Chunk Chunk1 = new Chunk(1000, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1"), Chunk.Bridges.None);

    public static IEnumerable<Entity> Entitys
    {
        get
        {
            yield return Fir;
            yield return SnowFir;
        }
    }

    public static IEnumerable<Tree> Trees
    {
        get
        {
            foreach (Tree t in Entitys)
            {
                yield return t;
            }
        }
    }

    public static Entity Find(int id)
    {
        foreach (Entity i in Entitys)
        {
            if (i.ID == id)
                return i;
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
