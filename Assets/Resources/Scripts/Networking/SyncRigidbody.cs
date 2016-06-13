﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncRigidbody : NetworkBehaviour
{

    [SerializeField]
    private Rigidbody rig;

    [SyncVar]
    private Vector3 syncVelocity;

    // Use this for initialization
    void Start()
    {
        this.syncVelocity = this.rig.velocity;
        if (isLocalPlayer)
            TransmitVelocity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
            this.TransmitVelocity();
        else
            this.rig.velocity = new Vector3(this.rig.velocity.x, this.syncVelocity.y, this.rig.velocity.z);
    }

    [Command]
    private void CmdSendVelocity(Vector3 vel)
    {
        this.syncVelocity = vel;
    }

    [Client]
    private void TransmitVelocity()
    {
        this.CmdSendVelocity(this.rig.velocity);
    }
}
