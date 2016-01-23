using UnityEngine;
using System.Collections;

/// <summary>
///  Utiliser cette classe pour creer un nouveau chunk.
/// </summary>
public class Chunk : Entity
{
    public enum Bridges { None, One, Two_Perpendicular, Two_Linear, Three, All};
    private Bridges bridge;

    // Constructor

    public Chunk() : base()
    {
        this.bridge = Bridges.None;
    }
       
    public Chunk(int id, GameObject prefab, Bridges bridge) : base(id, 1, prefab)
    {
        this.bridge = bridge;
    }

    // Getters & Setters
    /// <summary>
    ///  Le type de pont qont que possede le chunk.
    /// </summary>
    public Bridges Bridge
    {
        get { return this.bridge; }
    }
}