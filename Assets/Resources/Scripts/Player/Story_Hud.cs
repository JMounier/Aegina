using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class Story_Hud : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// Must be server!!!!
    /// Display the achivement on the client.
    /// </summary>
    /// <param name="succes"></param>
    public void Display(Succes succes)
    {
        RpcDisplay(succes.ID);
    }

    [ClientRpc]
    private void RpcDisplay(int id)
    {
        if (!isLocalPlayer)
            return;

    }
}
