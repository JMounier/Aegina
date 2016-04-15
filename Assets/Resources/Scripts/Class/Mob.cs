using UnityEngine;
using System.Collections;

/// <summary>
/// Utilisez cette classe pour creer un nouveau mob.
/// </summary>
public class Mob : Entity
{
    private int groupSize;
    private int damage;
    private float vision;
    private float walkSpeed;
    private float runSpeed;
    private float attackSpeed;

    private DropConfig[] dropConfigs;

    // Constructors

    public Mob() : base()
    {
        this.groupSize = 0;
        this.damage = 0;
        this.vision = 0;
        this.walkSpeed = 0;
        this.walkSpeed = 0;
        this.attackSpeed = 0;
        this.dropConfigs = new DropConfig[0];
    }

    public Mob(Mob mob) : base(mob)
    {
        this.groupSize = mob.groupSize;
        this.damage = mob.damage;
        this.vision = mob.vision;
        this.walkSpeed = mob.walkSpeed;
        this.runSpeed = mob.runSpeed;
        this.attackSpeed = mob.attackSpeed;
        this.dropConfigs = mob.dropConfigs;
    }

    public Mob(int id, int life, GameObject prefab, int groupSize, int damage, float vision, float walkSpeed, float runSpeed, float attackSpeed, params DropConfig[] dropConfigs) : base(id, life, prefab)
    {
        this.groupSize = groupSize;
        this.damage = damage;
        this.vision = vision;
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.attackSpeed = attackSpeed;
        this.dropConfigs = dropConfigs;
    }

    // Methods
    /// <summary>
    /// Appellez cette fonction pour detruire le mob.
    /// </summary>
    protected override void Kill()
    {
        foreach (DropConfig dc in this.dropConfigs)
        {
            Vector3 projection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f));
            new Item(dc.I).Spawn(prefab.transform.position, projection, dc.Quantity);
        }
        base.Kill();
    }
    
    public override void Spawn(Vector3 pos, Transform parent)
    {
        base.Spawn(pos, parent);
        base.prefab.GetComponent<SyncMob>().MyMob = new Mob(this);
    }

    public override void Spawn(Vector3 pos, Quaternion rot, Transform parent)
    {
        base.Spawn(pos, rot, parent);
        base.prefab.GetComponent<SyncMob>().MyMob = new Mob(this);
    }

    public override void Spawn(Vector3 pos)
    {
        Transform mob = null;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(5, 100, 5)))
            if (col.gameObject.name.Contains("Island"))
            {
                mob = col.transform.parent.FindChild("Mob");
                break;
            }
        base.Spawn(pos, mob);
        base.prefab.GetComponent<SyncMob>().MyMob = new Mob(this);
    }

    public override void Spawn(Vector3 pos, Quaternion rot)
    {
        Transform mob = null;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(5, 100, 5)))
            if (col.gameObject.name.Contains("Island"))
            {
                mob = col.transform.parent.FindChild("Mob");
                break;
            }
        base.Spawn(pos, rot, mob);
        base.prefab.GetComponent<SyncMob>().MyMob = new Mob(this);
    }

    // Getters & Setters

    /// <summary>
    /// La taille maximum d'un groupe de ce mob.
    /// </summary>
    public int GroupSize
    {
        get { return this.groupSize; }
        set { this.groupSize = value; }
    }

    /// <summary>
    /// Les degats du mob. (0 si passif)
    /// </summary>
    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }

    /// <summary>
    /// La vision du mob.
    /// </summary>
    public float Vision
    {
        get { return this.vision; }
        set { this.vision = value; }
    }

    /// <summary>
    /// La vitesse de marche du mob.
    /// </summary>
    public float WalkSpeed
    {
        get { return this.walkSpeed; }
        set { this.walkSpeed = value; }
    }

    /// <summary>
    /// La vitesse de course du mob.
    /// </summary>
    public float RunSpeed
    {
        get { return this.runSpeed; }
        set { this.runSpeed = value; }
    }

    /// <summary>
    /// La vitesse d'attaque du mob.
    /// </summary>
    public float AttackSpeed
    {
        get { return this.attackSpeed; }
        set { this.attackSpeed = value; }
    }

    /// <summary>
    /// Les loots spawnable a la mort du mob.
    /// </summary>
    public DropConfig[] DropConfigs
    {
        get { return this.dropConfigs; }
    }
}
