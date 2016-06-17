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

    private int x;
    private int y;
    private int step;
    private int posSave;
    private System.Random rand;
    private ChunkSave cs;
    private List<Vector3> posIslands;
    private List<Transform> ancres;

    // Constructor
    public Chunk() : base()
    {
        this.bridge = Bridges.None;
        this.b = BiomeDatabase.Default;
        this.isPrisme = false;
        this.step = 0;
        this.posSave = 0;
    }

    public Chunk(Chunk chunk) : base(chunk)
    {
        this.bridge = chunk.bridge;
        this.b = chunk.B;
        this.isPrisme = chunk.IsPrisme;
        this.step = 0;
        this.posSave = 0;
    }

    public Chunk(int id, GameObject prefab, Bridges bridge) : base(id, 1, prefab)
    {
        this.bridge = bridge;
        this.isPrisme = false;
        this.step = 0;
        this.posSave = 0;
    }

    // Methods 
    public Chunk StartGenerate(int x, int y, System.Random rand, Directions direction, GameObject map, bool isPrisme = false)
    {
        // Load the chunk
        this.x = x;
        this.y = y;
        this.rand = rand;
        this.cs = map.GetComponent<Save>().LoadChunk(x, y);
        this.isPrisme = isPrisme;
        this.b = BiomeDatabase.RandBiome(this.rand);
        Spawn(new Vector3(x * Size, 0, y * Size), Quaternion.Euler(new Vector3(0, 90 * (int)direction, 0)), map.transform);

        // Set good biome
        this.posIslands = new List<Vector3>();
        foreach (Transform child in Prefab.transform)
            if (child.name.Contains("Island"))
            {
                this.posIslands.Add(child.transform.position);
                if (child.GetComponent<MeshRenderer>().materials[0].name.Contains("Rock"))
                    child.GetComponent<MeshRenderer>().materials = new Material[2] { b.Rock, b.Grass };
                else
                    child.GetComponent<MeshRenderer>().materials = new Material[2] { b.Grass, b.Rock };
            }

        Prefab.GetComponent<SyncChunk>().BiomeId = b.ID;
        Prefab.GetComponent<SyncChunk>().IsCristal = isPrisme;
        this.ancres = new List<Transform>();
        foreach (Transform content in Prefab.transform)
            if (content.CompareTag("Elements"))
                foreach (Transform ancre in content.transform)
                    ancres.Add(ancre);
        return this;
    }
    public bool Generate()
    {
        List<Tuple<int, Vector3>> chunkSave = cs.ListIdSave;

        // Generate elements
        if (this.step > -1 && this.step < this.ancres.Count)
        {
            int min = Mathf.Min(step + 3, this.ancres.Count);
            for (int i = step; i < min; i++)
            {
                Transform ancre = this.ancres[i];
                if (ancre.CompareTag("Ancre"))
                    if (this.posSave < chunkSave.Count && chunkSave[this.posSave].Item1 == i)
                    {
                        chunkSave[this.posSave].Item2 = ancre.position;
                        this.rand.NextDouble();
                        this.rand.NextDouble();
                        this.posSave++;
                    }
                    else
                        this.GenerateEntity(this.b.Chose(this.rand), ancre.gameObject, (int)(this.rand.NextDouble() * 360), i);


                else if (ancre.CompareTag("MainAncre"))
                    if (this.isPrisme)
                        this.GenerateEntity(EntityDatabase.IslandCore, ancre.gameObject, 0, i);
                GameObject.Destroy(ancre.gameObject);
            }
            this.step = min - 1;
        }
        else if (step == this.ancres.Count)
        {
            // Load IslandCore
            if (this.isPrisme)
                Prefab.GetComponent<SyncChunk>().FindCristal();

            //Generate Worktop
            foreach (Triple<Element, Vector3, Vector3> worktop in cs.WorkTops)
                new Element(worktop.Item1).Spawn(worktop.Item2, Quaternion.Euler(worktop.Item3), Prefab.transform.FindChild("Elements"), -1, true);

            List<Quadruple<Element, Vector3, Vector3, ItemStack[,]>> copy = new List<Quadruple<Element, Vector3, Vector3, ItemStack[,]>>(cs.Chests);
            cs.Chests.Clear();
            foreach (Quadruple<Element, Vector3, Vector3, ItemStack[,]> chest in copy)
            {
                Chest c = new Chest(chest.Item1 as Chest);
                c.Content = chest.Item4;
                c.Spawn(chest.Item2, Quaternion.Euler(chest.Item3), Prefab.transform.FindChild("Elements"), -1, false);
            }
        }
        else if (step == this.ancres.Count + 1)
        {
            // Generate Graph
            if (Prefab.GetComponent<SyncChunk>().MyGraph == null)
                Prefab.GetComponent<SyncChunk>().MyGraph = new Graph(this.posIslands.ToArray());
            Prefab.GetComponent<SyncChunk>().MyGraph.GenerateGraph(300);
            if (!Prefab.GetComponent<SyncChunk>().MyGraph.IsFileEmpty)
                this.step--;
        }
        else if (step == this.ancres.Count + 2 && base.iD != EntityDatabase.Chunk0_Empty.iD)
        {
            //generate Mobs
            foreach (Mob mob in EntityDatabase.Mobs)
            {
                bool biomeValid = false;
                foreach (int biomeId in mob.BiomesIDSpawnable)
                    if (biomeId == this.b.ID)
                    {
                        biomeValid = true;
                        break;
                    }
                if (biomeValid)
                    for (int i = 0; i < mob.SpawnProbability; i++)
                        new Mob(mob).Spawn(Prefab.GetComponent<SyncChunk>().MyGraph.ChoseRandomNode().Position, Prefab.transform.FindChild("Mob"));
            }
            return true;
        }
        this.step++;
        return false;
    }

    private void GenerateEntity(Entity e, GameObject ancre, int rotY, int idSave)
    {
        if (e.ID != -1)
        {
            Vector3 rot = e.Prefab.transform.eulerAngles;
            rot.y = rotY;
            if (e is IslandCore)
                new IslandCore(e as IslandCore).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent, idSave);
            else if (e is Element)
                new Element(e as Element).Spawn(ancre.transform.position, Quaternion.Euler(rot), ancre.transform.parent, idSave);
            else
                throw new Exception("You want to spawn something else than an element in the chunk...");

        }
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

    public int X
    {
        get { return this.x; }
    }

    public int Y
    {
        get { return this.y; }
    }
}