﻿using UnityEngine;
using System.Collections;

/// <summary>
///  Use it to create a new consumable item.
/// </summary>
public class Consumable : Item
{
    private Effect e;
    
    // Constructors
    public Consumable() : base()
    {
        this.e = new Effect();
    }

    public Consumable(int id, string name, string description, int size, Effect e) : base(id, name, description, size)
    {
        this.e = e;
    }

    public Consumable(int id, int meta, string name, string description, int size, Effect e) : base(id, meta, name, description, size)
    {
        this.e = e;
    }
    
    public Consumable(int id, string name, string description, int size, Texture2D icon, Effect e) : base(id, name, description, size, icon)
    {
        this.e = e;
    }

    public Consumable(int id, int meta, string name, string description, int size, Texture2D icon, Effect e) : base(id, meta, name, description, size, icon)
    {
        this.e = e;
    }

    // Methods

    public void Consume()
    {
        // To do
    }

    // Getter & Setters
    public Effect E
    {
        get { return this.E; }
        set { this.e = value; }
    }
}

/// <summary>
///  Use it to create a new effect.
/// </summary>
public class Effect
{
    public static enum Effect { None, Speed, Slowness, Haste, MiningFatigue, Strength, InstantHealth, InstantDamage, JumpBoost, Regeneration,
        Resistance, Hunger, Weakness, Poison, Saturation, Thirst, Refreshment};

    private Effect e;
    private int power;

    // Constructors
    public Effect()
    {
        this.e = Effect.None;
        this.power = 0;
    }

    public Effect(Effect e)
    {
        this.e = e;
        this.power = 1;
    }

    public Effect(Effect e, int power)
    {
        this.e = e;
        this.power = power;
    }

    // Getter & Setters
    public Effect E
    {
        get { return this.e; }
        set { this.e = value; }
    }

    public int Power
    {
        get { return this.power = power; }
        set { this.power = value; }
    }
}