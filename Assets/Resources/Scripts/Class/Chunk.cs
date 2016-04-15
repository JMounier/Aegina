using UnityEngine;
using System.Collections.Generic;
using System;
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
    public readonly static int Size = 100;

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
    public void Generate(int x, int y, System.Random rand, Directions direction, GameObject map, bool isPrisme = false)
    {
        // Load the chunk
        Save save = map.GetComponent<Save>();
        save.AddChunk(x, y);
        List<int> chunkSave = save.LoadChunk(x, y);
        int posSave = 0;

        this.isPrisme = isPrisme;
        this.b = BiomeDatabase.RandBiome(rand);
        Spawn(new Vector3(x * Size, 0, y * Size), Quaternion.Euler(new Vector3(0, 90 * (int)direction, 0)), map.transform);

        // Set good biome
        List<Vector3> posIslands = new List<Vector3>();
        foreach (Transform child in Prefab.transform)
            if (child.name.Contains("Island"))
            {
                posIslands.Add(child.transform.position);
                if (child.GetComponent<MeshRenderer>().materials[0].name.Contains("Rock"))
                    child.GetComponent<MeshRenderer>().materials = new Material[2] { b.Rock, b.Grass };
                else
                    child.GetComponent<MeshRenderer>().materials = new Material[2] { b.Grass, b.Rock };
            }

        Prefab.GetComponent<SyncChunk>().BiomeId = b.ID;
        Prefab.GetComponent<SyncChunk>().IsCristal = isPrisme;
        // Generate elements
        int idSave = 0;
        foreach (Transform content in Prefab.transform)
            if (content.CompareTag("Elements"))
                foreach (Transform ancre in content.transform)
                {
                    if (ancre.CompareTag("Ancre"))
                    {
                        if (posSave < chunkSave.Count && chunkSave[posSave] == idSave)
                            posSave++;
                        else
                            this.GenerateEntity(this.b.Chose(rand), ancre.gameObject, idSave);
                        idSave++;
                    }

                    else if (ancre.CompareTag("MainAncre"))
                    {
                        if (this.isPrisme)
                        {
                            this.GenerateEntity(EntityDatabase.IslandCore, ancre.gameObject, idSave);
                        }
                        idSave++;
                    }
                }
        // Generate Graph
        Prefab.GetComponent<SyncChunk>().MyGraph = new Graph(posIslands.ToArray());
        if (isPrisme)
        {
            Prefab.GetComponent<SyncChunk>().FindCristal();
        }

        //generate Mobs
        foreach (Mob mob in EntityDatabase.Mobs)
        {
            for (int i = 0; i < mob.GroupSize; i++)
            {
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-Size / 2 + x * Size, Size / 2 + y * Size), 7, UnityEngine.Random.Range(-Size / 2 + x * Size, Size / 2 + y * Size));               
                if (Graph.isValidPosition(pos))
                    new Mob(mob).Spawn(pos, Prefab.transform.FindChild("Mob"));
            }
        }
    }

    private void GenerateEntity(Entity e, GameObject ancre, int idSave)
    {
        if (e.ID != -1)
        {
            Vector3 rot = e.Prefab.transform.eulerAngles;
            rot.y = UnityEngine.Random.Range(0, 360);
            if (e is IslandCore)
                new IslandCore(e as IslandCore).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent, idSave);
            else if (e is Element)
                new Element(e as Element).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent, idSave);
            else
                throw new Exception("You want to spawn something else than an element in the chunk...");

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