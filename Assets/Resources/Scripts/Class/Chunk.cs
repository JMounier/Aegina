using UnityEngine;
using System.Collections;

public class Chunk : Entity
{
    public enum Bridges { None, One, Two_Perpendicular, Two_Linear, Three, All};
    private Bridges bridge;

    public Chunk() : base()
    {
        this.bridge = Bridges.None;
    }
    public Chunk(int id, GameObject prefab, Bridges bridge) : base(id, 1, prefab)
    {
        this.bridge = bridge;
    }

}