using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCore : SyncElement
{
    private IslandCore cristal;
    void Start()
    {
        this.cristal = new IslandCore(EntityDatabase.IslandCore);
    }

    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 0.25f);
        gameObject.transform.Translate( Vector3.up * 0.01f*(Mathf.Sin(Time.time)));
    }

    public IslandCore Cristal
    {
        get { return this.cristal; }
        set { this.cristal = value; }
    }

    [Command]
    public void CmdSetTeam(Team team)
    {
        this.cristal.T = team;
        RpcSetColor(team);
    }

    [ClientRpc]
    private void RpcSetColor(Team team)
    {
        Shader sh = this.GetComponentInChildren<MeshRenderer>().material.shader;
        this.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)this.cristal.T);
        this.GetComponentInChildren<MeshRenderer>().material.shader = sh;
    }
}