using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/// <summary>
/// Utiliser cette classe pour creer une nouvelle element (Objet 3D avec lequel on interagit).
/// </summary>
public class Element : Entity
{
    public enum TypeElement { None, Tree, Rock, Small, WorkTop };
    protected DropConfig[] dropConfigs;
    protected TypeElement type;
    protected int armor;

    // Constructor
    public Element() : base()
    {
        this.dropConfigs = new DropConfig[0];
        this.type = TypeElement.None;
        this.armor = 0;
    }

    public Element(Element element) : base(element)
    {
        this.dropConfigs = element.dropConfigs;
        this.type = element.type;
        this.armor = element.armor;
    }

    public Element(int id, int life, GameObject prefab, TypeElement type, int armor, params DropConfig[] dropConfigs) : base(id, life, prefab)
    {
        this.dropConfigs = dropConfigs;
        this.type = type;
        this.armor = armor;
    }

    // Methods
    /// <summary>
    /// Appellez cette fonction pour detruire l'element.
    /// </summary>
    protected override void Kill()
    {
        foreach (DropConfig dc in this.dropConfigs)
        {
            Vector3 projection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f));
			dc.Loot(prefab.transform.position, projection);
			//new Item(dc.I).Spawn(prefab.transform.position, projection, dc.Quantity);
        }
        int idSave = base.prefab.GetComponent<SyncElement>().IdSave;
        int x = (int)(base.prefab.transform.parent.parent.position.x / Chunk.Size);
        int y = (int)(base.prefab.transform.parent.parent.position.z / Chunk.Size);
        base.prefab.transform.parent.parent.GetComponent<SyncChunk>().ToReset.Add(new Tuple<float, Vector3>(.5f, base.prefab.transform.position));
        if (idSave == -1)
            GameObject.Find("Map").GetComponent<Save>().SaveDestroyedWorktop(x, y, this);
        else
            GameObject.Find("Map").GetComponent<Save>().SaveDestroyedElement(x, y, idSave);
        base.Kill();
    }
       
    /// <summary>
    /// Instancie l'entite dans le monde avec une position et un parent. (Must be server!)
    /// </summary>
    public void Spawn(Vector3 pos, Transform parent, int idSave, bool workTopLoad = false)
    {
        base.Spawn(pos, parent);
        base.prefab.GetComponent<SyncElement>().Elmt = new Element(this);
        base.prefab.GetComponent<SyncElement>().IdSave = idSave;
        if (idSave == -1 && !workTopLoad)
        {
            int x = (int)(parent.parent.position.x / Chunk.Size);
            int y = (int)(parent.parent.position.y / Chunk.Size);
            GameObject.Find("Map").GetComponent<Save>().SaveBuildWorktop(x, y, this);
        }
    }
    

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et une rotation et un parent. (Must be server!)
    /// </summary>
    public void Spawn(Vector3 pos, Quaternion rot, Transform parent, int idSave, bool workTopLoad = false)
    {
        base.Spawn(pos, rot, parent);
        base.prefab.GetComponent<SyncElement>().Elmt = new Element(this);
        base.prefab.GetComponent<SyncElement>().IdSave = idSave;
        if (idSave == -1 && !workTopLoad)
        {
            int x = (int)(parent.parent.position.x / Chunk.Size);
            int y = (int)(parent.parent.position.y / Chunk.Size);
            GameObject.Find("Map").GetComponent<Save>().SaveBuildWorktop(x, y, this);
        }
    }

    public void GetDamage(float damage)
    {
        this.Life -= Mathf.Max(damage - this.armor, 0);
        if (this.Life <= 0)
            Stats.AddDestroyed(this);
    }

    // Getters & Setters
    /// <summary>
    /// Les loots spawnable a la mort de l'element.
    /// </summary>
    public DropConfig[] DropConfigs
    {
        get { return this.dropConfigs; }
    }

    /// <summary>
    /// Le type d'element.
    /// </summary>
    public TypeElement Type
    {
        get { return this.type; }
    }

    public int Armor
    {
        get { return this.armor; }
    }
}

/// <summary>
/// Utiliser cette classe pour creer une nouvelle configuration de drop.
/// </summary>
public class DropConfig
{
    private int itemID;
    private int min;
    private int max;

    // Cnostructor
    public DropConfig()
    {
		this.itemID = 0;
        this.min = 0;
        this.max = 0;
    }

	public DropConfig(int itemID, int quantity)
    {
		this.itemID = itemID;
		this.max = quantity; //Mathf.Clamp(quantity, 0, i.Size);
        this.min = this.max;
    }

	public DropConfig(int itemID, int min, int max)
    {
		this.itemID = itemID;
		this.max = max; //Mathf.Clamp(max, 0, i.Size);
        this.min = Mathf.Clamp(min, 0, max);
    }

	//Methods

	public void Loot(Vector3 position, Vector3 force)
	{
		ItemDatabase.Find(this.itemID).Spawn(position,force, Random.Range(this.min,this.max + 1));
	}


    // Getter & Setter

    /// <summary>
    /// L'item a drop.
    /// </summary>
    public Item I
    {
		get { return ItemDatabase.Find(itemID); }
		set { this.itemID = value.ID; }
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