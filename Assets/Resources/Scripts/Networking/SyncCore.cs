using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

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
    private int upgrade;
    [SyncVar]
    private int life;


    private Item[] needs;


    protected override void Start()
    {
        if (isServer)
        {
            ChunkSave cs = GameObject.Find("Map").GetComponent<Save>().LoadChunk((int)Mathf.Round(gameObject.transform.position.x / Chunk.Size), (int)Mathf.Round(gameObject.transform.position.z / Chunk.Size));
            this.CmdSetTeam((Team)cs.CristalCaracteristics[0]);
            this.levelAttack = (int)cs.CristalCaracteristics[1];
            this.levelProd = (int)cs.CristalCaracteristics[2];
            this.levelPortal = (int)cs.CristalCaracteristics[3];
            this.levelTot = (int)levelAttack + levelProd + levelPortal;
            this.upgrade = (int)cs.CristalCaracteristics[4];
            this.life = (int)cs.CristalCaracteristics[5];
        }
        else
            this.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)this.team);

        this.needs = new Item[6] { new Item(ItemDatabase.Copper), new Item(ItemDatabase.Iron), new Item(ItemDatabase.Gold),
            new Item(ItemDatabase.Mithril), new Item(ItemDatabase.Floatium), new Item(ItemDatabase.Sunkium) };
    }

    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 0.25f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,  7 + Mathf.Sin(Time.time) * .5f, gameObject.transform.position.z);
    }
    void NeedUpdate()
    {
        this.levelTot = this.levelProd + this.levelAttack + this.levelPortal;
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
        get
        {
            ItemStack[] liste = new ItemStack[6];
            if (this.levelTot == 0)
            {
                liste = new ItemStack[3];
                liste[0] = new ItemStack(needs[0], 15);
                liste[1] = new ItemStack(needs[1], 15);
                liste[2] = new ItemStack(needs[2], 1);
            }
            else
            {
                liste[0] = new ItemStack(needs[0], (this.levelAttack + this.levelProd + 2 * this.LevelPortal) * 10);
                liste[1] = new ItemStack(needs[1], (2 * this.levelAttack + this.levelProd + this.LevelPortal) * 10);
                liste[2] = new ItemStack(needs[2], (this.levelAttack + 2 * this.levelProd + this.LevelPortal) * 10);
                liste[3] = new ItemStack(needs[3], (this.levelAttack + 2 * this.levelProd + 3 * this.LevelPortal) * 10);
                liste[4] = new ItemStack(needs[4], (2 * this.levelAttack + this.levelProd + 3 * this.LevelPortal) * 10);
                liste[5] = new ItemStack(needs[5], (this.levelAttack + this.levelProd + 3 * this.LevelPortal) * 10);
            }
            return liste;
        }
    }
    public ItemStack RepairCost
    {
        get { return new ItemStack(new Item(needs[levelTot]), 10); }
    }
    public Team Team
    {
        get { return team; }
    }
    public int Life
    {
        get { return life; }
        set { this.life = Mathf.Clamp(this.life + value, 0, 1000); }
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

    public void AttackCristal(int damage, Team team)
    {
        this.life -= damage;
        if (this.life <= 0)
        {
            this.life = 500;
            bool leveldown = this.LevelTot != 0;
            while (leveldown)
            {
                int i = Random.Range(0, 2);
                switch (i)
                {
                    case 0:
                        if (levelAttack > 0)
                        {
                            levelAttack -= 1;
                            NeedUpdate();
                            upgrade -= 1;
                            leveldown = false;
                        }
                        break;
                    case 1:
                        if (levelProd > 0)
                        {
                            levelProd -= 1;
                            NeedUpdate();
                            upgrade -= 1;
                            leveldown = false;
                        }
                        break;
                    default:
                        if (levelPortal > 0)
                        {
                            levelPortal -= 1;
                            upgrade -= 1;
                            NeedUpdate();
                            leveldown = false;
                        }
                        break;
                }
            }
            this.team = team;
            RpcSetColor(team);
        }
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