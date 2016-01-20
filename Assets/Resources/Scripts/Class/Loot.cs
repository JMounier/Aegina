using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer un nouveau loot.
/// </summary>
public class Loot : Entity
{
    private Item i;
    private int minQ;
    private int maxQ;


    // Constructor
    public Loot() : base()
    {
        this.i = new Item();
        this.minQ = 0;
        this.maxQ = 0;
    }

    public Loot(int id, GameObject prefab, Item i) : base(id, 60, prefab)
    {
        this.i = i;
        this.minQ = 1;
        this.maxQ = 1;
    }

    public Loot(int id, GameObject prefab, Item i, int min, int max) : base(id, 60, prefab)
    {
        this.i = new Item();
        this.maxQ = Mathf.Clamp(max, 0, i.Size);
        this.minQ = Mathf.Clamp(min,0,this.maxQ);
    }

    // Methods

    public override void Spawn(Vector3 pos)
    {
        base.Spawn(pos);
        SpawnLoot();
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une rotation.
    /// </summary>
    public override void Spawn(Quaternion rot)
    {
        base.Spawn(rot);
        SpawnLoot();
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et une rotation.
    /// </summary>
    public override void Spawn(Vector3 pos, Quaternion rot)
    {
        base.Spawn(pos, rot);
        SpawnLoot();
    }


    /// <summary>
    /// Petit bon a l'instanciation du loot (Do not use).
    /// </summary>
    private void SpawnLoot()
    {
        Prefab.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(-1, 1), Random.Range(0, 1), Random.Range(-1, 1)));
    }

    // Getters & Setters
    /// <summary>
    /// L'item correspondant au loot.
    /// </summary>
    public Item I
    {
        get { return this.i; }
        set { this.i = value; }
    }

    /// <summary>
    /// La quantite min spawnable.
    /// </summary>
    public int MinQ
    {
        get { return this.minQ; }
        set { this.minQ = Mathf.Clamp(value, 0, this.maxQ); }
    }

    /// <summary>
    /// La quantite max spawnable.
    /// </summary>
    public int MaxQ
    {
        get { return this.maxQ; }
        set { this.maxQ = Mathf.Clamp(value, this.minQ, this.i.Size); }
    }
}
