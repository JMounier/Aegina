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
    protected GameObject toolPrefab;


    // Constructors
    public Tool() : base()
    {
        this.durability = 0;
        this.maxDurability = 0;
        this.damage = 0;
        this.miningEfficiency = 0;
        this.choppingEfficiency = 0;
        this.toolPrefab = null;
    }

    public Tool(Tool tool) : base(tool)
    {
        this.durability = tool.durability;
        this.maxDurability = tool.maxDurability;
        this.damage = tool.damage;
        this.miningEfficiency = tool.miningEfficiency;
        this.choppingEfficiency = tool.choppingEfficiency;
        this.toolPrefab = tool.toolPrefab;
    }

    public Tool(int id, Text name, Text description, int durability, int damage, int miningEfficiency, int choppingEfficiency, Texture2D icon, Entity ent, GameObject toolPrefab) :
        base(id, name, description, 1, icon, ent)
    {
        this.durability = durability;
        this.maxDurability = durability;
        this.damage = damage;
        this.miningEfficiency = miningEfficiency;
        this.choppingEfficiency = choppingEfficiency;
        this.toolPrefab = toolPrefab;
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

    /// <summary>
    /// Renvoi l'outil prefabrique.
    /// </summary>
    public GameObject ToolPrefab
    {
        set { this.toolPrefab = value; }
        get { return this.toolPrefab; }
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

    public Axe(int id, Text name, Text description, int durability, int efficiency, Texture2D icon, Entity ent, GameObject toolPrefab) :
       base(id, name, description, durability, 100 + efficiency / 3, 1, efficiency, icon, ent, toolPrefab)
    { }
}

/// <summary>
/// Utilisez cette classe pour creer de nouvelle pioches.
/// </summary>
public class Pickaxe : Tool
{
    // Constructors
    public Pickaxe() : base() { }
    public Pickaxe(Pickaxe pickaxe) : base(pickaxe) { }

    public Pickaxe(int id, Text name, Text description, int durability, int efficiency, Texture2D icon, Entity ent, GameObject toolPrefab) :
       base(id, name, description, durability, 100 + efficiency / 4, efficiency, 1, icon, ent, toolPrefab)
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
    public Sword(int id, Text name, Text description, int durability, int damage, Texture2D icon, Entity ent, GameObject toolPrefab) :
       base(id, name, description, durability, damage, 1, 1, icon, ent, toolPrefab)
    { }
}
public class BattleAxe : Tool
{
    public BattleAxe() : base() { }
    public BattleAxe(BattleAxe battleAxe) : base(battleAxe) { }
    public BattleAxe(int id, Text name, Text description, int durability, int damage, Texture2D icon, Entity ent, GameObject toolPrefab) :
       base(id, name, description, durability, damage, 1, 1, icon, ent, toolPrefab)
    { }
}
public class Spear : Tool
{
    public Spear() : base() { }
    public Spear(Spear spear) : base(spear) { }
    public Spear(int id, Text name, Text description, int durability, int damage, Texture2D icon, Entity ent, GameObject toolPrefab) :
       base(id, name, description, durability, damage, 1, 1, icon, ent, toolPrefab)
    { }
}