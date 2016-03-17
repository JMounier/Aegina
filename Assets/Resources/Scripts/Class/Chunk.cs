using UnityEngine;
using System.Collections;

/// <summary>
///  Les differentes sorties possibles.
/// </summary>
public enum Bridges { None, One, TwoL, TwoI, Three, All };
public enum Directions { North, East, South, West };

/// <summary>
///  Utiliser cette classe pour creer un nouveau chunk.
/// </summary>
public class Chunk : Entity
{
    private static int size = 100;

    private Bridges bridge;
    private Biome b;
    private bool isPrisme;

    // Constructor

    public Chunk() : base()
    {
        this.bridge = Bridges.None;
        this.b = BiomeDatabase.Default;
        this.isPrisme = false;
    }

    public Chunk(Chunk chunk) : base(chunk)
    {
        this.bridge = chunk.bridge;
        this.b = chunk.B;
        this.isPrisme = chunk.IsPrisme;
    }

    public Chunk(int id, GameObject prefab, Bridges bridge) : base(id, 1, prefab)
    {
        this.bridge = bridge;
        this.isPrisme = false;
    }

    // Methods 
    public void Generate(int x, int y, Directions direction, Biome b, GameObject map, bool isPrisme = false)
    {
        this.isPrisme = isPrisme;
        this.b = b;
        Spawn(new Vector3(x * size, 0, y * size), Quaternion.Euler(new Vector3(0, 90 * (int)direction, 0)), map.transform);
        Prefab.GetComponentInChildren<MeshRenderer>().materials = new Material[2] { b.Grass, b.Rock };
        Prefab.GetComponent<SyncChunk>().BiomeId = b.ID;
        foreach (Transform content in Prefab.transform)
            if (content.CompareTag("Elements"))
                foreach (Transform ancre in content.transform)
                {
                    if (ancre.CompareTag("Ancre"))
                    {
                        this.GenerateEntity(this.b.Chose(), ancre.gameObject);
                    }
                    else if (ancre.CompareTag("MainAncre"))
                    {
                        if (this.isPrisme)                        
                            this.GenerateEntity(EntityDatabase.IslandCore, ancre.gameObject);                        
                        else
                            this.GenerateEntity(this.b.Chose(), ancre.gameObject);
                    }
                }
    }

    private void GenerateEntity(Entity e, GameObject ancre)
    {
        if (e.ID != -1)
        {
            Vector3 rot = e.Prefab.transform.eulerAngles;
            rot.y = Random.Range(0, 360);
            if (e is IslandCore)
                new IslandCore(e as IslandCore).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent);
            else if (e is Element)
                new Element(e as Element).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent);
            else
                new Entity(e).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent);
        }
        GameObject.Destroy(ancre);
    }

    // Getters & Setters
    /// <summary>
    ///  Le type de pont qont que possede le chunk.
    /// </summary>
    public Bridges Bridge
    {
        get { return this.bridge; }
    }

    public Biome B
    {
        get { return this.b; }
    }

    public bool IsPrisme
    {
        get { return this.isPrisme; }
        set { this.isPrisme = value; }
    }
}