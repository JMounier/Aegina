using UnityEngine;
using System.Collections;

/// <summary>
///  Utilisez cette classe pour creer de nouveau consomable.
/// </summary>
public class Consumable : Item
{
    private Effect e;
    private GameObject consumablePrefab; 
    // Constructors
    public Consumable() : base()
    {
        this.e = new Effect();
        this.consumablePrefab = null;
    }

    public Consumable(Consumable consumable) : base(consumable)
    {
        this.e = consumable.e;
        this.consumablePrefab = consumable.consumablePrefab;
    }

    public Consumable(int id, Text name, Text description, int size, Texture2D icon, Entity ent, Effect e, GameObject consumablePrefab) : base(id, name, description, size, icon, ent)
    {
        this.e = e;
        this.consumablePrefab = consumablePrefab;
    }       

    // Getter & Setters

    /// <summary>
    ///  L'effet du consumable.
    /// </summary>
    public Effect E
    {
        get { return this.e; }
        set { this.e = value; }
    }

    public GameObject ConsumablePrefab
    {
        get { return this.consumablePrefab; }
        set { this.consumablePrefab = value;  }
    }
}

/// <summary>
///  Utilisez cette classe pour creer de nouveaux effets.
/// </summary>
public class Effect
{
    public enum EffectType { None, Speed, Slowness, Haste, MiningFatigue, Strength, InstantHealth, InstantDamage, JumpBoost, Regeneration,
        Resistance, Hunger, Weakness, Poison, Saturation, Thirst, Refreshment};

    private EffectType et;
    private int power;
    private float duration;

    // Constructors
    public Effect()
    {
        this.et = EffectType.None;
        this.power = 0;
    }

    public Effect(EffectType et)
    {
        this.et = et;
        this.power = 1;
    }

    public Effect(EffectType et, int power)
    {
        this.et = et;
        this.power = power;
    }

    // Getter & Setters
    /// <summary>
    ///  Le type d'effet.
    /// </summary>
    public EffectType ET
    {
        get { return this.et; }
        set { this.et = value; }
    }

    /// <summary>
    ///  La puissance de l'effet.
    /// </summary>
    public int Power
    {
        get { return this.power; }
        set { this.power = value; }
    }
}