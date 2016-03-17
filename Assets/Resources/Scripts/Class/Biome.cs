using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer un nouveau biome.
/// </summary>
public class Biome
{
    private int iD;
    private SpawnConfig[] spawnConfiguration;
    private Material grass;
    private Material rock;

    // Constructor

    public Biome()
    {
        this.iD = -1;
        this.spawnConfiguration = new SpawnConfig[0];
        this.grass = null;
        this.rock = null;
    }

    public Biome(Biome biome)
    {
        this.iD = biome.ID;
        this.spawnConfiguration =biome.spawnConfiguration;
        this.grass = biome.Grass;
        this.rock = biome.Rock;
    }
     
    public Biome(int id, Material grass, Material rock, params Entity[] spawnableEntity)
    {
        this.iD = id;
        this.spawnConfiguration = new SpawnConfig[spawnableEntity.Length];
        float ratio = 1 / spawnableEntity.Length;
        for (int i = 0; i < spawnableEntity.Length; i++)
        {
            this.spawnConfiguration[i] = new SpawnConfig(spawnableEntity[i], ratio);
        }
        this.grass = grass;
        this.rock = rock;
    }

    public Biome(int id, Material grass, Material rock, params SpawnConfig[] spawnConfiguration)
    {
        this.iD = id;
        this.spawnConfiguration = spawnConfiguration;
        float sum = 0f;

        for (int i = 0; i < spawnConfiguration.Length; i++)        
            sum += spawnConfiguration[i].Ratio;
        
        for (int i = 0; i < spawnConfiguration.Length; i++)        
            this.spawnConfiguration[i].Ratio /= sum;
        this.grass = grass;
        this.rock = rock;
    }

    // Methods
    /// <summary>
    /// Genere une entite du biome sur l'ancre avec une rotation aleatoire sur Y. (Must be server!)
    /// </summary>
    public Entity Chose()
    {
        float rand = Random.Range(0f, 1f);
        float sum = 0f;
        for (int i = 0; i < this.spawnConfiguration.Length; i++)
        {
            SpawnConfig sc = this.spawnConfiguration[i];
            sum += sc.Ratio;
            if (rand < sum)
            {
                return sc.E;
            }
        }
        throw new System.Exception("Biome.Chose : Weird rand");
    }
      
    // Getters & Setters
    /// <summary>
    /// L'identifiant unique du biome.
    /// </summary>
    public int ID
    {
        get { return this.iD; }
    }

    /// <summary>
    /// Le materiaux de l'herbe du biome.
    /// </summary>
    public Material Grass
    {
        get { return this.grass; }
    }

    /// <summary>
    /// Le materiaux des rochers du biome.
    /// </summary>
    public Material Rock
    {
        get { return this.rock; }
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
