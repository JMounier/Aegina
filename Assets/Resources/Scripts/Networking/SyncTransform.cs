using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncTransform : NetworkBehaviour
{
    [SerializeField]
    private Transform trans;

    [SerializeField]
    private bool syncPosition = true;
    [SerializeField]
    private float tresholdPosition = 0.5f;

    [SerializeField]
    private bool syncRotation = true;
    [SerializeField]
    private float tresholdRotation = 0.01f;

    [SyncVar]
    private Vector3 syncPos;
    private Vector3 lastPos;

    [SyncVar]
    private Vector3 syncRot;
    private Vector3 lastRot;

    // Use this for initialization
    void Start()
    {
        this.lastPos = this.trans.position;
        this.lastRot = this.trans.eulerAngles;
        if (isLocalPlayer)
        {
            TransmitPosition();
            TransmitRotation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (this.syncPosition && Vector3.Distance(this.lastPos, this.trans.position) > this.tresholdPosition)
                this.TransmitPosition();
            if (this.syncRotation && Vector3.Distance(this.lastRot, this.trans.eulerAngles) > this.tresholdRotation)
                this.TransmitRotation();
        }
        if (!isLocalPlayer)
        {
            if (this.syncPosition)
                this.trans.position = this.syncPos;
            if (this.syncRotation)
                this.trans.rotation = Quaternion.Lerp(this.trans.rotation, Quaternion.Euler(this.syncRot), Time.deltaTime * 15);
        }
    }

    [Command]
    private void CmdSendPosition(Vector3 pos)
    {
        this.syncPos = pos;
    }

    [Command]
    private void CmdSendRotation(Vector3 rot)
    {
        this.syncRot = rot;
    }

    [Client]
    private void TransmitPosition()
    {
        this.CmdSendPosition(this.trans.position);
    }

    [Client]
    private void TransmitRotation()
    {
        this.CmdSendRotation(this.trans.eulerAngles);
    }
}
