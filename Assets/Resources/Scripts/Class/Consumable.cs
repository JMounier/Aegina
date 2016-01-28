using UnityEngine;
using System.Collections;

/// <summary>
///  Utilisez cette classe pour creer de nouveau consomable.
/// </summary>
public class Consumable : Item
{
    private Effect e;

    // Constructors
    public Consumable() : base()
    {
        this.e = new Effect();
    }

    public Consumable(Consumable consumable) : base(consumable)
    {
        this.e = consumable.e;
    }

    public Consumable(int id, string[] name, string[] description, int size, Texture2D icon, Entity ent, Effect e) : base(id, name, description, size, icon, ent)
    {
        this.e = e;
    }

    public Consumable(int id, int meta, string[] name, string[] description, int size, Texture2D icon, Entity ent, Effect e) : base(id, meta, name, description, size, icon, ent)
    {
        this.e = e;
    }

    // Methods

    /// <summary>
    /// Consume l'objet et applique ces effets.
    /// </summary>
    public void Consume()
    {
        // To do
    }

    // Getter & Setters

    /// <summary>
    ///  L'effet du consumable.
    /// </summary>
    public Effect E
    {
        get { return this.E; }
        set { this.e = value; }
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