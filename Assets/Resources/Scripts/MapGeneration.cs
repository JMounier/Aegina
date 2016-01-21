using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{

    // Use this for initialization    
    void Start()
    {
        if (isServer)
            CreateChunk(0, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateChunk(int x, int y)
    {
        EntityDatabase.Chunk1.Spawn(new Vector3(0, 0, 0));
        foreach (Transform iles in EntityDatabase.Chunk1.Prefab.transform)
            foreach (Transform ancres in iles.transform)
                foreach (Transform ancre in ancres.transform)                
                    if (ancre.CompareTag("Ancre"))
                        BiomeDatabase.Forest.Generate(ancre.gameObject);
                
    }
}
