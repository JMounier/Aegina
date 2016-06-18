using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SyncChunk : NetworkBehaviour
{

    [SyncVar]
    private Vector3 rotation;

    [SyncVar]
    private int biomeId;

    [SyncVar]
    private bool isCristal;

    private SyncCore cristal;

    private Graph myGraph;
    private List<Tuple<float, Vector3>> toReset;
    [SerializeField]
    private bool debugGraph = false;
    private bool biomeUpdated = false;
    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.rotation = gameObject.transform.eulerAngles;
            this.toReset = new List<Tuple<float, Vector3>>();
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.name.Contains("Island") && SceneManager.GetActiveScene().name == "main")
                {
                    if (child.GetComponent<MeshRenderer>().materials[0].name.Contains("Rock"))
                        child.GetComponent<MeshRenderer>().materials = new Material[2] { BiomeDatabase.Forest.Rock, BiomeDatabase.Forest.Grass };
                    else
                        child.GetComponent<MeshRenderer>().materials = new Material[2] { BiomeDatabase.Forest.Grass, BiomeDatabase.Forest.Rock };
                }
            }
        }
    }

    void Update()
    {
        if (this.biomeId != 0 && !biomeUpdated)
        {
            biomeUpdated = true;
            Biome b = BiomeDatabase.Find(this.biomeId);
            foreach (Transform child in gameObject.transform)
            {
                if (child.name.Contains("Island"))
                {
                    if (child.GetComponent<MeshRenderer>().materials[0].name.Contains("Rock"))
                        child.GetComponent<MeshRenderer>().materials = new Material[2] { b.Rock, b.Grass };
                    else
                        child.GetComponent<MeshRenderer>().materials = new Material[2] { b.Grass, b.Rock };
                }
            }
        }
        if (gameObject.transform.eulerAngles != this.rotation)
            gameObject.transform.eulerAngles = this.rotation;
        if (!isServer)
            return;
        if (debugGraph)
            this.myGraph.DebugDrawGraph();
        if (this.toReset.Count > 0)

            if (this.ToReset[0].Item1 <= 0)
            {
                Node node = this.myGraph.GetNode(this.toReset[0].Item2);
                if (node != null)
                    this.myGraph.Reset(node, true);
                this.toReset.RemoveAt(0);
            }
            else
                this.ToReset[0].Item1 -= Time.deltaTime;
    }
    public void FindCristal()
    {
        this.cristal = this.transform.FindChild("Elements").FindChild("IslandCore(Clone)").GetComponent<SyncCore>();
    }

    public int BiomeId
    {
        get { return this.biomeId; }
        set { this.biomeId = value; }
    }

    public Graph MyGraph
    {
        get { return this.myGraph; }
        set { this.myGraph = value; }
    }

    public List<Tuple<float, Vector3>> ToReset
    {
        get { return this.toReset; }
        set { this.toReset = value; }
    }

    public bool IsCristal
    {
        get { return this.isCristal; }
        set { this.isCristal = value; }
    }

    public SyncCore Cristal
    {
        get { return this.cristal; }
        set { this.cristal = value; }
    }
}
