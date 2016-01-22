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

    // Rock

    // Chunk
    public static readonly Chunk Chunk2 = new Chunk(1000, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2"), Chunk.Bridges.None);

    public static IEnumerable<Entity> Entitys
    {
        get
        {
            // Default
            yield return Default;
            yield return Log;

            // Tree
            yield return Fir;
            yield return SnowFir;

            // Chunk
            yield return Chunk2;
        }
    }

    public static IEnumerable<Tree> Trees
    {
        get
        {
            foreach (Tree t in Entitys)            
                yield return t;            
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
