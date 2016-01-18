using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer une nouvelle entite.
/// </summary>
public class Entity
{
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

    public Entity(int life, GameObject prefab)
    {
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
        GameObject.Instantiate(this.prefab, this.prefab.transform.position, this.prefab.transform.rotation);
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position.
    /// </summary>
    public void Spawn(Vector3 pos)
    {
        GameObject.Instantiate(this.prefab, pos, this.prefab.transform.rotation);
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une rotation.
    /// </summary>
    public void Spawn(Quaternion rot)
    {
        GameObject.Instantiate(this.prefab, this.prefab.transform.position, rot);
    }

    /// <summary>
    /// Instancie l'entite dans le monde avec une position et une rotation.
    /// </summary>
    public void Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject.Instantiate(this.prefab, pos, rot);
    }

    // Getter & Setter

    /// <summary>
    /// La vie de l'entite.
    /// </summary>
    public int Life
    {
        get { return this.life; }
        set { this.life = Mathf.Clamp(value, 0, this.lifeMax); }
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
