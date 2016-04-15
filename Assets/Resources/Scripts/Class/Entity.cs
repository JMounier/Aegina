using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Utiliser cette classe pour creer une nouvelle entite.
/// </summary>
public class Entity
{
    protected int iD;
    protected int lifeMax;
    protected float life;
    protected GameObject prefab;

    // Constructor
    public Entity()
    {
        this.iD = -1;
        this.lifeMax = 0;
        this.life = 0;
        this.prefab = null;
    }

    public Entity(Entity ent)
    {
        this.iD = ent.iD;
        this.lifeMax = ent.lifeMax;
        this.life = ent.life;
        this.prefab = ent.prefab;
    }

    public Entity(int id, int life, GameObject prefab)
    {
        this.iD = id;
        this.lifeMax = life;
        this.life = life;
        this.prefab = prefab;
    }

    // Methods
       
    /// <summary>
    /// Instancie l'entite dans le monde avec une position. (Must be server!)
    /// </summary>
    public virtual void Spawn(Vector3 pos)
    {
        this.prefab = GameObject.Instantiate(this.prefab, pos, this.prefab.transform.rotation) as GameObject;
        NetworkServer.Spawn(this.prefab);
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et un parent. (Must be server!)
    /// </summary>
    public virtual void Spawn(Vector3 pos, Transform parent)
    {
        this.prefab = GameObject.Instantiate(this.prefab, pos, this.prefab.transform.rotation) as GameObject;
        this.prefab.transform.parent = parent;
        NetworkServer.Spawn(this.prefab);
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et une rotation. (Must be server!)
    /// </summary>
    public virtual void Spawn(Vector3 pos, Quaternion rot)
    {
        this.prefab = GameObject.Instantiate(this.prefab, pos, rot) as GameObject;
        NetworkServer.Spawn(this.prefab);
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et une rotation et un parent. (Must be server!)
    /// </summary>
    public virtual void Spawn(Vector3 pos, Quaternion rot, Transform parent)
    {
        this.prefab = GameObject.Instantiate(this.prefab, pos, rot) as GameObject;
        this.prefab.transform.parent = parent;
        NetworkServer.Spawn(this.prefab);
    }

    /// <summary>
    /// Desinstancie l'entite dans le monde.
    /// </summary>
    protected virtual void Kill()
    {
        NetworkServer.UnSpawn(this.prefab);
        GameObject.Destroy(this.prefab);
    }

    // Getter & Setter

    /// <summary>
    /// L'identifiant unique de l'entite.
    /// </summary>
    public int ID
    {
        get { return this.iD; }
    }

    /// <summary>
    /// La vie de l'entite.
    /// </summary>
    public float Life
    {
        get { return this.life; }
        set
        {
            this.life = Mathf.Clamp(value, 0, this.lifeMax);
            if (this.life == 0)
                this.Kill();
        }
    }

    /// <summary>
    /// La vie maximum de l'entite.
    /// </summary>
    public int LifeMax
    {
        get { return this.lifeMax; }
        set
        {
            this.lifeMax = Mathf.Max(value, 0);
            this.life = Mathf.Clamp(value, 0, this.lifeMax);
        }
    }

    /// <summary>
    /// Le prefab de l'entite.
    /// </summary>
    public GameObject Prefab
    {
        get { return this.prefab; }
        set { this.prefab = value; }
    }
}
