using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer un nouveau biome.
/// </summary>
public class Biome
{
    private int iD;
    private SpawnConfig[] spwanConfiguration;
    
    // Constructor

    public Biome()
    {
        this.iD = -1;
        this.spwanConfiguration = new SpawnConfig[0];        
    }

    public Biome(int id, params Entity[] spawnableEntity)
    {
        this.iD = id;
        this.spwanConfiguration = new SpawnConfig[spawnableEntity.Length];
        float ratio = 1 / spawnableEntity.Length;
        for (int i = 0; i < spawnableEntity.Length; i++)
        {
            this.spwanConfiguration[i] = new SpawnConfig(spawnableEntity[i], ratio);
        }
    }

    public Biome(int id, params SpawnConfig[] spwanConfiguration)
    {
        this.iD = id;
        this.spwanConfiguration = spwanConfiguration;
        float sum = 0f;   
        for (int i = 0; i < spwanConfiguration.Length; i++)
        {
            sum += spwanConfiguration[i].Ratio;
        }

        for (int i = 0; i < spwanConfiguration.Length; i++)
        {
            this.spwanConfiguration[i].Ratio /= sum;
        }
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
        this.e = null;
        this.ratio = 0f;
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
