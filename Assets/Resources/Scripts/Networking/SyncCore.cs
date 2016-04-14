using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCore : SyncElement
{
    [SyncVar]
    private Team team;
    [SyncVar]
    private int levelTot;
    [SyncVar]
    private int levelAttack;
    [SyncVar]
    private int levelProd;
    [SyncVar]
    private int levelPortal;
    [SyncVar]
    private int upgrade = 0;
    private ItemStack[] needs;

    void Start()
    {
        if (isServer)
            this.CmdSetTeam(Team.Neutre);
        else
        {
            Shader sh = this.GetComponentInChildren<MeshRenderer>().material.shader;
            this.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)this.team);
            this.GetComponentInChildren<MeshRenderer>().material.shader = sh;
        }
        this.levelTot = 0;
        this.levelAttack = 0;
        this.levelProd = 0;
        this.levelPortal = 0;
        this.needs = new ItemStack[3] { new ItemStack(ItemDatabase.Iron, 15), new ItemStack(ItemDatabase.Gold, 1), new ItemStack(ItemDatabase.Copper, 15) };
    }
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 0.25f);
        gameObject.transform.Translate(Vector3.up * 0.005f * (Mathf.Sin(Time.time)));
        
    }
    void NeedUpdate()
    {
        this.levelTot = this.levelProd + this.levelAttack + this.levelPortal;
        if (this.levelTot != 0)
        {
            this.needs = new ItemStack[6] { new ItemStack(ItemDatabase.Iron, 10 * levelTot+10*levelAttack), new ItemStack(ItemDatabase.Gold, 5 * levelTot+5*levelProd), new ItemStack(ItemDatabase.Copper, 20 * levelTot-10*levelPortal), new ItemStack(ItemDatabase.Mithril, 3 * levelTot+3*levelProd+6*levelPortal), new ItemStack(ItemDatabase.Floatium, 3 * levelTot + 3*levelAttack + 6*levelPortal), new ItemStack(ItemDatabase.Sunkium, levelTot + 2*levelPortal) };
        }
    }

    #region Getters
    public Team T
    {
        get { return (Team)this.team; }
    }
    public int Upgrade
    {
        get { return upgrade; }
        set { upgrade = value; }
    }
    /// <summary>
    /// Le niveau total du cristal
    /// </summary>
    public int LevelTot
    {
        get { return this.levelTot; }
    }

    /// <summary>
    /// Le niveau de production du cristal
    /// </summary>
    public int LevelProd
    {
        get { return this.levelProd; }
    }

    /// <summary>
    /// Le niveau d'attaque du cristal
    /// </summary>
    public int LevelAtk
    {
        get { return this.levelAttack; }
    }

    /// <summary>
    /// Le niveau de portail du cristal
    /// </summary>
    public int LevelPortal
    {
        get { return this.levelPortal; }
    }

    public ItemStack[] Needs
    {
        get { return this.needs; }
    }

    public Team Team
    {
        get { return team; }
    }
    #endregion

    #region Cmd
    [Command]
    public void CmdSetTeam(Team team)
    {
        this.team = team;
        RpcSetColor(team);
    }
    [Command]
    private void CmdSetLevelTot(int level)
    {
        this.levelTot = level;
        NeedUpdate();
    }
    [Command]
    public void CmdSetLevelProd(int level)
    {
        this.levelProd = level;
        this.CmdSetLevelTot(level + this.levelAttack + this.levelPortal);
    }
    [Command]
    public void CmdSetLevelAtk(int level)
    {
        this.levelAttack = level;
        this.CmdSetLevelTot(level + this.levelProd + this.levelPortal);
    }
    [Command]
    public void CmdSetLevelPort(int level)
    {
        this.levelPortal = level;
        this.CmdSetLevelTot(level + this.levelAttack + this.levelProd);
    }

    #endregion

    #region Rpc
    [ClientRpc]
    private void RpcSetColor(Team team)
    {
        Shader sh = this.GetComponentInChildren<MeshRenderer>().material.shader;
        this.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)team);
        this.GetComponentInChildren<MeshRenderer>().material.shader = sh;
    }

    #endregion
}