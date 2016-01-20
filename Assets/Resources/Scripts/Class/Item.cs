using UnityEngine;
using System.Collections;

/// <summary>
///  Utiliser cette classe pour creer un nouvelle objet.
/// </summary>
public class Item
{
    protected int iD;
    protected int meta;
    protected string[] name;
    protected string[] description;
    protected Texture2D icon;
    protected int size;

    // Constructors
    public Item()
    {
        this.size = 0;
        this.name = new string[] { "",""} ;
        this.iD = -1;
        this.meta = 0;
        this.description = new string[] { "", "" };
        this.icon = null;
    }

    public Item(int id, string[] name, string[] description, int size)
    {
        this.name = name;
        this.iD = id;
        this.meta = 0;
        this.description = description;
        this.size = size;
        this.icon = Resources.Load<Texture2D>("Sprites/ItemIcons/" + name[0]);
    }

    public Item(int id, int meta, string[] name, string[] description, int size)
    {
        this.name = name;
        this.iD = id;
        this.meta = meta;
        this.description = description;
        this.size = size;
        this.icon = Resources.Load<Texture2D>("Sprites/ItemIcons/" + name[0]);
    }

    public Item(int id, string[] name, string[] description, int size, Texture2D icon)
    {
        this.name = name;
        this.iD = id;
        this.meta = 0;
        this.description = description;
        this.size = size;
        this.icon = icon;
    }

    public Item(int id, int meta, string[] name, string[] description, int size, Texture2D icon)
    {
        this.name = name;
        this.iD = id;
        this.meta = meta;
        this.description = description;
        this.size = size;
        this.icon = icon;
    }

    // Getter & Setters

    /// <summary>
    /// Retourne l'identifiant de l'item.
    /// </summary>
    public int ID
    {
        get { return this.iD; }
        set { this.iD = value; }
    }

    /// <summary>
    /// Retourne la métadonnées de l'item.
    /// </summary>
    public int Meta
    {
        get { return this.meta; }
        set { this.meta = value; }
    }

    /// <summary>
    /// Retourne le nom de l'item.
    /// </summary>
    public string Name
    {
        get { return this.name[PlayerPrefs.GetInt("langue",0)]; }
        set { this.name[PlayerPrefs.GetInt("langue", 0)] = value; }
    }

    /// <summary>
    /// Retourne la description de l'item dans la langue du joueur.
    /// </summary>
    public string Description
    {
        get { return this.description[PlayerPrefs.GetInt("langue", 0)]; }
        set { this.description[PlayerPrefs.GetInt("langue", 0)] = value; }
    }

    /// <summary>
    /// Retourne l'icon de l'item.
    /// </summary>
    public Texture2D Icon
    {
        get { return this.icon; }
        set { this.icon = value; }
    }

    /// <summary>
    /// Retourne le nombre maximum d'item stackable.
    /// </summary>
    public int Size
    {
        get { return this.size; }
        set { this.size = value; }
    }
}

/// <summary>
///  Use it to create a group of a same Item.
/// </summary>
public class ItemStack
{
    private Item items;
    private int quantity;

    // Constructors
    public ItemStack()
    {
        this.items = new Item();
        this.quantity = 0;
    }

    public ItemStack(Item items, int quantity)
    {
        this.items = items;
        this.quantity = Mathf.Clamp(quantity, 0, items.Size);
    }

    // Getter & Setters

    public int Quantity
    {
        get { return this.quantity; }
        set { this.quantity = Mathf.Clamp(value, 0, items.Size); }
    }

    public Item Items
    {
        get { return this.items; }
    }
}