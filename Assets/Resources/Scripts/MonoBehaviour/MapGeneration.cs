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
            new Chunk(EntityDatabase.Chunk2_One).Generate(0, 0, BiomeDatabase.RandBiome, gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
