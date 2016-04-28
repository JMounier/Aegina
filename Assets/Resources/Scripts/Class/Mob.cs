using UnityEngine;
using System.Collections;

/// <summary>
/// Utilisez cette classe pour creer un nouveau mob.
/// </summary>
public class Mob : Entity
{
    private int spawnProbability;
    private int damage;
    private float visionFocus;
    private float visionFleeing;
    private float walkSpeed;
    private float runSpeed;
    private float attackSpeed;

    private int[] biomes;
    private DropConfig[] dropConfigs;

    // Constructors

    public Mob() : base()
    {
        this.spawnProbability = 0;
        this.damage = 0;
        this.visionFocus = 0;
        this.visionFleeing = 0;
        this.walkSpeed = 0;
        this.walkSpeed = 0;
        this.attackSpeed = 0;
        this.biomes = new int[0];
        this.dropConfigs = new DropConfig[0];
    }

    public Mob(Mob mob) : base(mob)
    {
        this.spawnProbability = mob.spawnProbability;
        this.damage = mob.damage;
        this.visionFocus = mob.visionFocus;
        this.visionFleeing = mob.visionFleeing;
        this.walkSpeed = mob.walkSpeed;
        this.runSpeed = mob.runSpeed;
        this.attackSpeed = mob.attackSpeed;
        this.dropConfigs = mob.dropConfigs;
        this.biomes = mob.biomes;
    }

    public Mob(int id, int life, GameObject prefab, int spawnProbability, int damage, float visionFocus, float visionFleeing, float walkSpeed, float runSpeed, float attackSpeed, int[] biomes, params DropConfig[] dropConfigs) : base(id, life, prefab)
    {
        this.spawnProbability = spawnProbability;
        this.damage = damage;
        this.visionFocus = visionFocus;
        this.visionFleeing = visionFleeing;
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.attackSpeed = attackSpeed;
        this.biomes = biomes;
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
            dc.Loot(prefab.transform.position, projection);
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
            if (col.gameObject.name.Contains("Island") && col.tag == "Ground")
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
            if (col.gameObject.name.Contains("Island") && col.tag == "Ground")
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
    public int SpawnProbability
    {
        get { return this.spawnProbability; }
        set { this.spawnProbability = value; }
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
    /// La vision de focus du mob.
    /// </summary>
    public float VisionFocus
    {
        get { return this.visionFocus; }
        set { this.visionFocus = value; }
    }

    /// <summary>
    /// La vision de fuite du mob.
    /// </summary>
    public float VisionFleeing
    {
        get { return this.visionFleeing; }
        set { this.visionFleeing = value; }
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
    /// La liste des biomes ou le mob peut spawn
    /// </summary>
    public int[] BiomesIDSpawnable
    {
        get { return this.biomes; }
    }

    /// <summary>
    /// Les loots spawnable a la mort du mob.
    /// </summary>
    public DropConfig[] DropConfigs
    {
        get { return this.dropConfigs; }
    }
}
