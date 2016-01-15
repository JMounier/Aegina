using UnityEngine;
using System.Collections;

/// <summary>
///  Utilisez cette classe pour creer de nouveau outils.
/// </summary>
public class Tool : Item
{
    protected int durability;
    protected int maxDurability;
    protected int damage;
    protected int miningEfficiency;
    protected int choppingEfficiency;

    // Constructors
    public Tool () : base()
    {
        this.durability = 0;
        this.maxDurability = 0;
        this.damage = 0;
        this.miningEfficiency = 0;
        this.choppingEfficiency = 0;
    }

    public Tool(int id, string[] name, string[] description, int durability, int damage, int miningEfficiency, int choppingEfficiency) : 
        base(id, name, description, 1)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
    }

    public Tool(int id, int meta, string[] name, string[] description, int durability, int damage, int miningEfficiency, int choppingEfficiency) :
       base(id, meta, name, description, 1)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
    }

    public Tool(int id, string[] name, string[] description, int durability, int damage, int miningEfficiency, int choppingEfficiency, Texture2D icon) : 
        base(id, name, description, 1, icon)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
    }

    public Tool(int id, int meta, string[] name, string[] description, int durability, int damage, int miningEfficiency, int choppingEfficiency, Texture2D icon) :
        base(id, meta, name, description, 1, icon)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
    }

    // Getter & Setters

    /// <summary>
    ///  La durabilité restante de l'outil
    /// </summary>
    public int Durability
    {
        get { return this.durability; }
        set { this.durability = value; }
    }

    /// <summary>
    ///  La durabilité totale de l'objet
    /// </summary>
    public int MaxDurability
    {
        get { return this.maxDurability; }
        set { this.maxDurability = value; }
    }

    /// <summary>
    ///  Les dégats infliger par l'outil sur une entité.
    /// </summary>
    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }

    /// <summary>
    ///  L'efficacite de l'outil a miner.
    /// </summary>
    public int MiningEfficiency
    {
        get { return this.miningEfficiency; }
        set { this.miningEfficiency = value; }
    }

    /// <summary>
    ///  L'efficacite de l'outil a couper du bois.
    /// </summary>
    public int ChoppingEfficiency
    {
        get { return this.choppingEfficiency; }
        set { this.choppingEfficiency = value; }
    }
}

/// <summary>
///  Utilisez cette classe pour creer de nouvelle haches.
/// /// </summary>
public class Axe : Tool
{
    // Constructors
    public Axe() : base() { }

    public Axe(int id, string[] name, string[] description, int durability, int efficiency) :
        base(id, name, description, durability, 5, 1, efficiency) { }

    public Axe(int id, int meta, string[] name, string[] description, int durability, int efficiency) :
       base(id, meta, name, description, durability, 5, 1, efficiency) { }

    public Axe(int id, string[] name, string[] description, int durability, int efficiency, Texture2D icon) :
       base(id, name, description, durability, 5, 1, efficiency, icon) { }

    public Axe(int id, int meta, string[] name, string[] description, int durability, int efficiency, Texture2D icon) :
       base(id, meta, name, description, durability, 5, 1, efficiency, icon) { }
}

/// <summary>
///  Utilisez cette classe pour creer de nouvelle pioches.
/// </summary>
public class Pickaxe : Tool
{
    // Constructors
    public Pickaxe() : base() { }

    public Pickaxe(int id, string[] name, string[] description, int durability, int efficiency) :
        base(id, name, description, durability, 3, efficiency, 1) { }

    public Pickaxe(int id, int meta, string[] name, string[] description, int durability, int efficiency) :
       base(id, meta, name, description, durability, 3, efficiency, 1)
    { }

    public Pickaxe(int id, string[] name, string[] description, int durability, int efficiency, Texture2D icon) :
       base(id, name, description, durability, 3, efficiency, 1, icon) { }

    public Pickaxe(int id, int meta, string[] name, string[] description, int durability, int efficiency, Texture2D icon) :
       base(id, meta, name, description, durability, 3, efficiency, 1, icon)
    { }
}

/// <summary>
///  Utilisez cette classe pour creer de nouvelles épées.
/// </summary>
public class Sword : Tool
{
    // Constructors
    public Sword() : base() { }

    public Sword(int id, string[] name, string[] description, int durability, int damage) :
        base(id, name, description, durability, damage, 1, 1) { }

    public Sword(int id, int meta, string[] name, string[] description, int durability, int damage) :
        base(id, meta, name, description, durability, damage, 1, 1) { }

    public Sword(int id, int meta, string[] name, string[] description, int durability, int damage, Texture2D icon) :
       base(id, meta, name, description, durability, damage, 1, 1, icon) { }
}