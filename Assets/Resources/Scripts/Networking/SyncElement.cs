using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncElement : NetworkBehaviour {

    [SyncVar]
    private Vector3 rotation;

	// Use this for initialization
	void Start ()
    {
        if (isServer)
            this.rotation = gameObject.transform.eulerAngles;
        else
            gameObject.transform.eulerAngles = rotation;
	}	
}
