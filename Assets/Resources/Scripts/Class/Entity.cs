using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer une nouvelle entite.
/// </summary>
public class Entity
{
    protected int iD;
    protected int lifeMax;
    protected int life;
    protected GameObject prefab;

    // Constructor
    public Entity()
    {
        this.lifeMax = 0;
        this.life = 0;
        this.prefab = null;
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
    /// Instancie l'entite dans le monde.
    /// </summary>
    public void Spawn()
    {
        this.prefab = GameObject.Instantiate(this.prefab, this.prefab.transform.position, this.prefab.transform.rotation) as GameObject;
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position.
    /// </summary>
    public virtual void Spawn(Vector3 pos)
    {
        this.prefab = GameObject.Instantiate(this.prefab, pos, this.prefab.transform.rotation) as GameObject;
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une rotation.
    /// </summary>
    public virtual void Spawn(Quaternion rot)
    {
        this.prefab = GameObject.Instantiate(this.prefab, this.prefab.transform.position, rot) as GameObject;
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et une rotation.
    /// </summary>
    public virtual void Spawn(Vector3 pos, Quaternion rot)
    {
        this.prefab = GameObject.Instantiate(this.prefab, pos, rot) as GameObject;
    }

    /// <summary>
    /// Desinstancie l'entite dans le monde.
    /// </summary>
    protected virtual void Kill()
    {
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
    public int Life
    {
        get { return this.life; }
        set {
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
        set { this.lifeMax = (int) Mathf.Clamp(value, 0f, float.PositiveInfinity); }
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
