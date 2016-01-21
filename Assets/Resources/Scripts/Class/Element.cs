using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Utiliser cette classe pour creer une nouvelle element (Objet 3D avec lequel on interagit).
/// </summary>
public class Element : Entity
{
    protected Loot[] loots;
    protected int armor;

    // Constructor
    public Element() : base()
    {
        this.loots = new Loot[0];
        this.armor = 0;
    }

    public Element(int id, int life, GameObject prefab, int armor, params Loot[] loots) : base(id, life, prefab)
    {
        this.loots = loots;
        this.armor = armor;
    }

    // Methods
    protected override void Kill()
    {
        foreach (Loot l in this.Loots)
        {
            
            l.Spawn(base.prefab.transform.position);
        }
        base.Kill();
    }

    // Getters & Setters
    /// <summary>
    /// Les point d'armure de l'element.
    /// </summary>
    public int Armor
    {
        get { return this.armor; }
        set { this.armor = value; }
    }

    /// <summary>
    /// Les loots spawnable a la mort de l'element.
    /// </summary>
    public Loot[] Loots
    {
        get { return this.loots; }
    }
}

public class Tree : Element
{
    public Tree() : base() { }

    public Tree(int id, int life, GameObject prefab, int armor, params Loot[] loots) : base(id, life, prefab, armor, loots) { }
}

public class Rock : Element
{
    public Rock() : base() { }

    public Rock(int id, int life, GameObject prefab, int armor, params Loot[] loots) : base(id, life, prefab, armor, loots) { }
}