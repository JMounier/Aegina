using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncChunk : NetworkBehaviour
{

    [SyncVar]
    private Vector3 rotation;

    [SyncVar]
    private int biomeId;

    private Graph myGraph;

    // Use this for initialization
    void Start()
    {
        if (isServer)        
            this.rotation = gameObject.transform.eulerAngles;
            
        
        else
        {
            gameObject.transform.eulerAngles = rotation;
            Biome b = BiomeDatabase.Find(this.biomeId);
            gameObject.GetComponentInChildren<MeshRenderer>().materials = new Material[2] { b.Grass, b.Rock };
        }
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
}
