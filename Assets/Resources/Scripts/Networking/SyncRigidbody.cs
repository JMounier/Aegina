using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncRigidbody : NetworkBehaviour
{

    [SerializeField]
    private Rigidbody rig;

    [SyncVar]
    private Vector3 syncVelocity;
    private Vector3 lastVelocity;

    // Use this for initialization
    void Start()
    {
        this.lastVelocity = this.rig.velocity;
        this.syncVelocity = this.rig.velocity;
        if (isLocalPlayer)
            TransmitVelocity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            this.TransmitVelocity();
        }
        if (!isLocalPlayer)
        {
            this.rig.velocity = this.syncVelocity;
        }
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
