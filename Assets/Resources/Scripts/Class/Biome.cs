using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer un nouveau biome.
/// </summary>
public class Biome
{
    private int iD;
    private SpawnConfig[] spawnConfiguration;

    // Constructor

    public Biome()
    {
        this.iD = -1;
        this.spawnConfiguration = new SpawnConfig[0];
    }

    public Biome(int id, params Entity[] spawnableEntity)
    {
        this.iD = id;
        this.spawnConfiguration = new SpawnConfig[spawnableEntity.Length];
        float ratio = 1 / spawnableEntity.Length;
        for (int i = 0; i < spawnableEntity.Length; i++)
        {
            this.spawnConfiguration[i] = new SpawnConfig(spawnableEntity[i], ratio);
        }
    }

    public Biome(int id, params SpawnConfig[] spawnConfiguration)
    {
        this.iD = id;
        this.spawnConfiguration = spawnConfiguration;
        float sum = 0f;

        for (int i = 0; i < spawnConfiguration.Length; i++)        
            sum += spawnConfiguration[i].Ratio;
        
        for (int i = 0; i < spawnConfiguration.Length; i++)        
            this.spawnConfiguration[i].Ratio /= sum;
        
    }

    // Methods
    /// <summary>
    /// Genere une entite du biome sur l'ancre avec une rotation aleatoire sur Y.
    /// </summary>
    public void Generate(GameObject ancre)
    {
        float rand = Random.Range(0f, 1f);
        float sum = 0f;
        foreach (SpawnConfig sc in this.spawnConfiguration)
        {
            sum += sc.Ratio;            
            if (rand < sum)
            {
                if (sc.E.ID != -1)
                {
                    Vector3 rot = sc.E.Prefab.transform.eulerAngles;
                    rot.y = Random.Range(0, 360);
                    sc.E.Spawn(ancre.transform.position, Quaternion.Euler(rot));
                    sc.E.Prefab.transform.parent = ancre.transform.parent;
                }
                break;
            }
        }
        GameObject.Destroy(ancre);
    }

    /// <summary>
    /// Genere des entites de biome sur chaque ancres avec une rotation aleatoire sur Y.
    /// </summary>
    public void Generate(params GameObject[] ancres)
    {
        foreach (GameObject ancre in ancres)
            Generate(ancre);
    }

    // Getters & Setters
    /// <summary>
    /// L'identifiant unique du biome.
    /// </summary>
    public int ID
    {
        get { return this.iD; }
    }
}

/// <summary>
/// Utiliser cette classe pour creer une nouvelle configuration de spawn.
/// </summary>
public class SpawnConfig
{
    private Entity e;
    private float ratio;

    // Cnostructor
    public SpawnConfig()
    {
        this.e = new Entity();
        this.ratio = 1f;
    }

    public SpawnConfig(Entity e)
    {
        this.e = e;
        this.ratio = 1f;
    }

    public SpawnConfig(Entity e, float ratio)
    {
        this.e = e;
        this.ratio = ratio;
    }

    // Getter & Setter

    /// <summary>
    /// L'entite configure.
    /// </summary>
    public Entity E
    {
        get { return this.e; }
        set { this.e = value; }
    }

    /// <summary>
    /// Le ratio de spawn de l'entite.
    /// </summary>
    public float Ratio
    {
        get { return this.ratio; }
        set { this.ratio = value; }
    }
}
