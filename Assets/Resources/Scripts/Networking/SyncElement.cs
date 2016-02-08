using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncElement : NetworkBehaviour {

    [SyncVar]
    private Vector3 rotation;
    [SyncVar]
    private int idElmt;

	// Use this for initialization
	void Start ()
    {
        if (isServer)
            this.rotation = gameObject.transform.eulerAngles;
        else
            gameObject.transform.eulerAngles = rotation;
	}	

    [Command]
    public void CmdSetElement(int id)
    {
        this.idElmt = id;
    }

    public Element Elmt
    {
        get { return EntityDatabase.Find(this.idElmt) as Element; }
    }
}
