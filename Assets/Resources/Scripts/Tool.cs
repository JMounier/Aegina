using UnityEngine;
using System.Collections;

/// <summary>
///  Use it to create a new commun Tool.
/// </summary>
public class Tool : Item
{
    protected int durability;
    protected int maxDurability;
    protected int damage;
    protected int mineEfficiency;
    protected int axeEfficiency;
    protected int demolitionEfficiency;

    // Constructors
    public Tool () : base()
    {
        this.durability = 0;
        this.maxDurability = 0;
        this.damage = 0;
        this.mineEfficiency = 0;
        this.axeEfficiency = 0;
        this.demolitionEfficiency = 0;
    }

    public Tool(int id, string name, string description, int durability, int damage, int mineEfficiency, int axeEfficiency, int demolitionEfficiency) : 
        base(id, name, description, 1)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.mineEfficiency = mineEfficiency;
        this.axeEfficiency = axeEfficiency;
        this.demolitionEfficiency = demolitionEfficiency; 
    }

    public Tool(int id, string name, string description, int durability, int damage, int mineEfficiency, int axeEfficiency, int demolitionEfficiency, Texture2D icon) : 
        base(id, name, description, 1, icon)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.mineEfficiency = mineEfficiency;
        this.axeEfficiency = axeEfficiency;
        this.demolitionEfficiency = demolitionEfficiency;
    }

    // Getter & Setters

    public int Durability
    {
        get { return this.durability; }
        set { this.durability = value; }
    }

    public int MaxDurability
    {
        get { return this.maxDurability; }
        set { this.maxDurability = value; }
    }

    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }

    public int MineEfficiency
    {
        get { return this.mineEfficiency; }
        set { this.mineEfficiency = value; }
    }

    public int AxeEfficiency
    {
        get { return this.axeEfficiency; }
        set { this.axeEfficiency = value; }
    }

    public int DemolitionEfficiency
    {
        get { return this.demolitionEfficiency; }
        set { this.demolitionEfficiency = value; }
    }
}

/// <summary>
///  Use it to create a new Axe.
/// </summary>
public class Axe : Tool
{
    // Constructors
    public Axe() : base() { }

    public Axe(int id, string name, string description, int durability, int efficiency) :
        base(id, name, description, durability, 5, 1, efficiency,  1) { }

    public Axe(int id, string name, string description, int durability, int efficiency, Texture2D icon) :
       base(id, name, description, durability, 5, 1, efficiency, 1, icon) { }
}

/// <summary>
///  Use it to create a new Pickaxe.
/// </summary>
public class Pickaxe : Tool
{
    // Constructors
    public Pickaxe() : base() { }

    public Pickaxe(int id, string name, string description, int durability, int efficiency) :
        base(id, name, description, durability, 3, efficiency, 1, 1) { }

    public Pickaxe(int id, string name, string description, int durability, int efficiency, Texture2D icon) :
       base(id, name, description, durability, 3, efficiency, 1, 1, icon) { }
}

/// <summary>
///  Use it to create a new Sword.
/// </summary>
public class Sword : Tool
{
    // Constructors
    public Sword() : base() { }

    public Sword(int id, string name, string description, int durability, int damage) :
        base(id, name, description, durability, damage, 1, 1, 1) { }

    public Sword(int id, string name, string description, int durability, int damage, Texture2D icon) :
       base(id, name, description, durability, damage, 1, 1, 1, icon) { }
}