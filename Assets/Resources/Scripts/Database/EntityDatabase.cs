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
    public static readonly Chunk Chunk2_Two_Linear = new Chunk(1001, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Two_Linear"), Bridges.Two_Linear);
    public static readonly Chunk Chunk2_Two_Perpendicular = new Chunk(1002, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Two_Perpendicular"), Bridges.Two_Perpendicular);
    public static readonly Chunk Chunk2_Three = new Chunk(1003, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_One"), Bridges.Three);
    public static readonly Chunk Chunk2_All = new Chunk(1004, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_All"), Bridges.All);

    public static IEnumerable<Entity> Entitys
    {
        get
        {
            // Default
            yield return new Entity(Default);
            yield return new Entity(Log);

            // Tree
            yield return new Tree(Fir);
            yield return new Tree(SnowFir);
            yield return new Tree(Cactus);
            yield return new Tree(Oak);
            yield return new Tree(SnowOak);

            // Rocks
            yield return new Rock(Stone);

            // Chunk
            yield return new Chunk(Chunk2_One);
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
