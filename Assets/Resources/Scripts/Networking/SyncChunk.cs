using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SyncChunk : NetworkBehaviour
{

    [SyncVar]
    private Vector3 rotation;

    [SyncVar]
    private int biomeId;

    private Graph myGraph;
    private List<Tuple<float, Vector3>> toReset;
    [SerializeField]
    private bool debugGraph = false;

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
            gameObject.transform.eulerAngles = rotation;
            Biome b = BiomeDatabase.Find(this.biomeId);
            gameObject.GetComponentInChildren<MeshRenderer>().materials = new Material[2] { b.Grass, b.Rock };
        }
    }

    void Update()
    {
        if (!isServer)
            return;
        if (debugGraph)
            this.myGraph.DebugDrawGraph();
        if (this.toReset.Count > 0)
            if (this.ToReset[0].Item1 <= 0)
            {
                Node node = this.myGraph.GetNode(this.toReset[0].Item2);
                if (node != null)
                    this.myGraph.Reset(node);
                this.toReset.RemoveAt(0);
            }
            else
                this.ToReset[0].Item1 -= Time.deltaTime;
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
}
