using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCore : SyncElement
{
    private IslandCore cristal;

    public IslandCore Cristal
    {
        get { return this.cristal; }
        set { this.cristal = value; }
    }

    public void CmdSetTeam(Team team)
    {
        this.cristal.T = team;
        Debug.Log(this);
        this.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)this.cristal.T);
    }
}
