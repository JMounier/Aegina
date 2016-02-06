using UnityEngine;
using System.Collections;

/// <summary>
/// Utilisez cette classe pour creer de nouveau outils.
/// </summary>
public class Tool : Item
{
    protected int durability;
    protected int maxDurability;
    protected int damage;
    protected int miningEfficiency;
    protected int choppingEfficiency;

    // Constructors
    public Tool() : base()
    {
        this.durability = 0;
        this.maxDurability = 0;
        this.damage = 0;
        this.miningEfficiency = 0;
        this.choppingEfficiency = 0;
    }

    public Tool(Tool tool) : base(tool)
    {
        this.durability = tool.durability;
        this.maxDurability = tool.maxDurability;
        this.damage = tool.damage;
        this.miningEfficiency = tool.miningEfficiency;
        this.choppingEfficiency = tool.choppingEfficiency;
    }

    public Tool(int id, Text name, Text description, int durability, int damage, int miningEfficiency, int choppingEfficiency, Texture2D icon, Entity ent) : 
        base(id, name, description, 1, icon, ent)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
    }

    public Tool(int id, int meta, Text name, Text description, int durability, int damage, int miningEfficiency, int choppingEfficiency, Texture2D icon, Entity ent) :
        base(id, meta, name, description, 1, icon, ent)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
    }

    // Getter & Setters

    /// <summary>
    /// La durabilité restante de l'outil
    /// </summary>
    public int Durability
    {
        get { return this.durability; }
        set { this.durability = value; }
    }

    /// <summary>
    /// La durabilité totale de l'objet
    /// </summary>
    public int MaxDurability
    {
        get { return this.maxDurability; }
        set { this.maxDurability = value; }
    }

    /// <summary>
    /// Les dégats infliger par l'outil sur une entité.
    /// </summary>
    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }

    /// <summary>
    /// L'efficacite de l'outil a miner.
    /// </summary>
    public int MiningEfficiency
    {
        get { return this.miningEfficiency; }
        set { this.miningEfficiency = value; }
    }

    /// <summary>
    /// L'efficacite de l'outil a couper du bois.
    /// </summary>
    public int ChoppingEfficiency
    {
        get { return this.choppingEfficiency; }
        set { this.choppingEfficiency = value; }
    }
}

/// <summary>
/// Utilisez cette classe pour creer de nouvelle haches.
/// </summary>
public class Axe : Tool
{
    // Constructors
    public Axe() : base() { }
    
    public Axe(Axe axe) : base(axe) { }

    public Axe(int id, Text name, Text description, int durability, int efficiency, Texture2D icon, Entity ent) :
       base(id, name, description, durability, 5, 1, efficiency, icon, ent) { }

    public Axe(int id, int meta, Text name, Text description, int durability, int efficiency, Texture2D icon, Entity ent) :
       base(id, meta, name, description, durability, 5, 1, efficiency, icon, ent) { }
}

/// <summary>
/// Utilisez cette classe pour creer de nouvelle pioches.
/// </summary>
public class Pickaxe : Tool
{
    // Constructors
    public Pickaxe() : base() { }
    public Pickaxe(Pickaxe pickaxe) : base(pickaxe) { }

    public Pickaxe(int id, Text name, Text description, int durability, int efficiency, Texture2D icon, Entity ent) :
       base(id, name, description, durability, 3, efficiency, 1, icon, ent) { }

    public Pickaxe(int id, int meta, Text name, Text description, int durability, int efficiency, Texture2D icon, Entity ent) :
       base(id, meta, name, description, durability, 3, efficiency, 1, icon, ent)
    { }
}

/// <summary>
/// Utilisez cette classe pour creer de nouvelles épées.
/// </summary>
public class Sword : Tool
{
    // Constructors
    public Sword() : base() { }
    public Sword(Sword sword) : base(sword) { }

    public Sword(int id, int meta, Text name, Text description, int durability, int damage, Texture2D icon, Entity ent) :
       base(id, meta, name, description, durability, damage, 1, 1, icon, ent) { }
}