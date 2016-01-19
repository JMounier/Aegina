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

}
