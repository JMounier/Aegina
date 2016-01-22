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
        Entity chunk = EntityDatabase.Chunk2;
        chunk.Spawn(new Vector3(0, 0, 0));
        chunk.Prefab.transform.parent = gameObject.transform;
        foreach (Transform content in chunk.Prefab.transform)
        {
            if (content.CompareTag("Elements"))
                foreach (Transform ancre in content.transform)
                    if (ancre.CompareTag("Ancre")) { 
                        BiomeDatabase.Forest.Generate(ancre.gameObject);}
        }

    }
}
