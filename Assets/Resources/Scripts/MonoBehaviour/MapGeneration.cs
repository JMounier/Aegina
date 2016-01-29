using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{

    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            EntityDatabase.Chunk2.Generate(0, 0, BiomeDatabase.Ice);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
