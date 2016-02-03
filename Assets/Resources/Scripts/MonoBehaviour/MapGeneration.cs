using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{
    private bool isLoad = false;
    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            new Chunk(EntityDatabase.Chunk2_Two_Perpendicular).Generate(0, 0, new Vector3(0,0,0), BiomeDatabase.RandBiome, gameObject);
            new Chunk(EntityDatabase.Chunk2_Two_Perpendicular).Generate(0, 1, new Vector3(0, 90, 0), BiomeDatabase.RandBiome, gameObject);
            new Chunk(EntityDatabase.Chunk2_Two_Perpendicular).Generate(1, 1, new Vector3(0, 180, 0), BiomeDatabase.RandBiome, gameObject);
            new Chunk(EntityDatabase.Chunk2_Two_Perpendicular).Generate(1, 0, new Vector3(0, -90, 0), BiomeDatabase.RandBiome, gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
