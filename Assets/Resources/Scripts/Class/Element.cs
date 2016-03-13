using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Utiliser cette classe pour creer une nouvelle element (Objet 3D avec lequel on interagit).
/// </summary>
public class Element : Entity
{
    protected DropConfig[] dropConfigs;
    protected int armor;

    // Constructor
    public Element() : base()
    {
        this.dropConfigs = new DropConfig[0];
        this.armor = 0;
    }

    public Element(Element element) : base(element)
    {
        this.dropConfigs = element.dropConfigs;
        this.armor = element.armor;
    }

    public Element(int id, int life, GameObject prefab, int armor, params DropConfig[] dropConfigs) : base(id, life, prefab)
    {
        this.dropConfigs = dropConfigs;
        this.armor = armor;
    }

    // Methods
    /// <summary>
    /// Appellez cette fonction pour detruire l'element.
    /// </summary>
    protected override void Kill()
    {
        foreach (var dc in this.dropConfigs)
        {
            Vector3 projection = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            dc.I.Spawn(prefab.transform.position, projection, dc.Quantity);
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
    public DropConfig[] DropConfigs
    {
        get { return this.dropConfigs; }
    }
}

/// <summary>
/// Utiliser cette classe pour creer une nouvelle configuration de drop.
/// </summary>
public class DropConfig
{
    private Item i;
    private int min;
    private int max;

    // Cnostructor
    public DropConfig()
    {
        this.i = new Item();
        this.min = 0;
        this.max = 0;
    }

    public DropConfig(Item i, int quantity)
    {
        this.i = i;
        this.max = Mathf.Clamp(quantity, 0, i.Size);
        this.min = this.max;
    }

    public DropConfig(Item i, int min, int max)
    {
        this.i = i;
        this.max = Mathf.Clamp(max, 0, i.Size);
        this.min = Mathf.Clamp(min, 0, max);
    }

    // Getter & Setter

    /// <summary>
    /// L'item a drop.
    /// </summary>
    public Item I
    {
        get { return this.i; }
        set { this.i = value; }
    }

    /// <summary>
    /// La quantite minimum du drop.
    /// </summary>
    public int Min
    {
        get { return this.min; }
        set { this.min = value; }
    }

    /// <summary>
    /// La quantite maximum du drop.
    /// </summary>
    public int Max
    {
        get { return this.max; }
        set { this.max = value; }
    }

    /// <summary>
    /// Donne une qantite aleatoire correspondant aux bornes.
    /// </summary>
    public int Quantity
    {
        get { return Random.Range(this.min, this.max + 1); }
    }
}

public class Tree : Element
{
    public Tree() : base() { }

    public Tree(Tree tree) : base(tree) { }

    public Tree(int id, int life, GameObject prefab, int armor, params DropConfig[] dropConfigs) : base(id, life, prefab, armor, dropConfigs) { }
}

public class Rock : Element
{
    public Rock() : base() { }

    public Rock(Rock rock) : base(rock) { }

    public Rock(int id, int life, GameObject prefab, int armor, params DropConfig[] dropConfigs) : base(id, life, prefab, armor, dropConfigs) { }
}