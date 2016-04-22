using UnityEngine;
using System.Collections;

/// <summary>
/// Les differentes teams
/// </summary>
public enum Team { Neutre, Orange, Red, Blue, Green };

/// <summary>
/// Creer un nouveau cristeau
/// </summary>
public class IslandCore : Element
{

    public IslandCore() : base() { }

    public IslandCore(IslandCore cristal) : base(cristal) { }

    public IslandCore(int id, GameObject prefab) : base(id, 100, prefab, TypeElement.None, 10) { }

    // Methods       
    /// <summary>
    /// Reset le cristal lorsqu'il n'a plus de pv
    /// </summary>
    protected override void Kill()
    {   
        base.life = base.lifeMax;
    }
}
