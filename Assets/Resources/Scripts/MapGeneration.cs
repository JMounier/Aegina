using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (isServer)
            CreateChunk(0, 0);

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void CreateChunk(int x, int y)
    {
        // TO DO
    }
}
