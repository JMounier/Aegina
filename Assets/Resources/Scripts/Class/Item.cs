using UnityEngine;
using System.Collections;

/// <summary>
///  Utiliser cette classe pour creer un nouvelle objet.
/// </summary>
public class Item
{
    protected int iD;
    protected int meta;
    protected Text name;
    protected Text description;
    protected Texture2D icon;
    protected int size;
    protected Entity ent;

    // Constructors
    public Item()
    {
        this.size = 0;
        this.name = new Text();
        this.iD = -1;
        this.meta = 0;
        this.description = new Text();
        this.icon = null;
        this.ent = new Entity();
    }

    public Item(Item item)
    {
        this.size = item.size;
        this.name = item.name;
        this.iD = item.iD;
        this.meta = item.meta;
        this.description = item.description;
        this.icon = item.icon;
        this.ent = new Entity(item.ent);
    }

    public Item(int id, Text name, Text description, int size, Texture2D icon, Entity ent)
    {
        this.name = name;
        this.iD = id;
        this.meta = 0;
        this.description = description;
        this.size = size;
        this.icon = icon;
        this.ent = ent;
    }

    public Item(int id, int meta, Text name, Text description, int size, Texture2D icon, Entity ent)
    {
        this.name = name;
        this.iD = id;
        this.meta = meta;
        this.description = description;
        this.size = size;
        this.icon = icon;
        this.ent = ent;
    }

    // Methods
    /// <summary>
    /// Instancie l'item dans le monde avec une position et une quantite. (Must be server!)
    /// </summary>
    public void Spawn(Vector3 pos, Vector3 force, int quantity)
    {
        if (quantity > 0)
        {
            this.ent.Spawn(pos, GameObject.Find("Loots").transform);
            this.ent.Prefab.GetComponent<Rigidbody>().AddRelativeForce(force * 120);
            this.ent.Prefab.GetComponent<Loot>().Items = new ItemStack(new Item(this), quantity);
        }
    }

    /// <summary>
    /// Instancie l'item dans le monde avec une position et une rotation et une quantite. (Must be server!)
    /// </summary>
    public void Spawn(Vector3 pos, Quaternion rot, Vector3 force, int quantity)
    {
        if (quantity > 0)
        {
            this.ent.Spawn(pos, rot, GameObject.Find("Loots").transform);
            this.ent.Prefab.GetComponent<Rigidbody>().AddRelativeForce(force * 120);
            this.ent.Prefab.GetComponent<Loot>().Items = new ItemStack(new Item(this), quantity);
        }
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
        get { return this.name.GetText(); }
    }
    public Text NameText
    {
        get { return this.name; }
    }
    /// <summary>
    /// Retourne la description de l'item dans la langue du joueur.
    /// </summary>
    public string Description
    {
        get { return this.description.GetText(); }
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

    /// <summary>
    /// Retourne l'entite associe a l'item.
    /// </summary>
    public Entity Ent
    {
        get { return this.ent; }
        set { this.ent = value; }
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
        set
        {
            this.quantity = Mathf.Clamp(value, 0, items.Size);
            if (this.quantity == 0)
                this.items = new Item();
        }
    }

    public Item Items
    {
        set { this.items = value; }
        get { return this.items; }
    }
}