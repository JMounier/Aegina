﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncElement : NetworkBehaviour {

    [SyncVar]
    private Vector3 rotation;
    private Element elmt;
    private int idSave;

    // Use this for initialization
    void Start()
    {
        if (isServer)
            this.rotation = gameObject.transform.eulerAngles;
        else
            gameObject.transform.eulerAngles = rotation;
    }

    public Element Elmt
    {
        set { this.elmt = value; }
        get { return this.elmt; }
    }

    public int IdSave
    {
        get { return this.idSave; }
        set { this.idSave = value; }
    }
}
