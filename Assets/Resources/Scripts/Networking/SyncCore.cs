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
    private int upgrade = 0;
	[SyncVar] 
	private int life = 1000;
    
	private int[,,,] quantity;

    private Item[] needs;


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
        this.needs = new Item[6] { ItemDatabase.Copper, ItemDatabase.Iron, ItemDatabase.Gold, ItemDatabase.Mithril, ItemDatabase.Floatium, ItemDatabase.Sunkium };
		this.quantity = new int[5,5,5,6];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				for (int k = 0; k < 5; k++) {
					this.quantity [i,j,k,0] = (i + j + 2*k) * 10;
					this.quantity [i,j,k,1] = (2 * i + j + k) * 10;
					this.quantity [i,j,k,2] = (i + 2*j + k) * 5;
					this.quantity [i,j,k,3] = (i + 2*j + 3*k) * 3;
					this.quantity [i,j,k,4] = (2*i + j + 3*k) * 3;
					this.quantity [i,j,k,5] = (i + j + 3*k);
				}
			}
		}

    }
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, 0.25f);
        gameObject.transform.Translate(Vector3.up * 0.005f * (Mathf.Sin(Time.time)));
        
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
			ItemStack[] liste;
			if (this.levelTot == 0) {
				liste = new ItemStack[3];
				liste [0] = new ItemStack (needs [0], 15);
				liste [1] = new ItemStack (needs [1], 1);
				liste [2] = new ItemStack (needs [2], 15);
			} 
			else {
				liste = new ItemStack[6];
				for (int i = 0; i < 6; i++) {
					liste [i] = new ItemStack (needs [i], quantity [this.levelAttack,this.levelProd,this.LevelPortal,i]);
				}
			}
			return liste;
        }
    }
	public ItemStack RepairCost
	{
		get{return new ItemStack (needs [levelTot], 10);}
	}
    public Team Team
    {
        get { return team; }
    }
	public int Life{
		get{ return life;}
		set{ this.life = Mathf.Clamp (this.life + value, 0, 1000);}
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
	public void AttackCristal(int damage, Team team){
		this.life -= damage;
		if (this.life <= 0) {
			this.life = 500;
			bool leveldown = this.LevelTot != 0;
			while (leveldown) 
			{
				int i = Random.Range (0, 2);
				switch (i) 
				{
				case 0:
					if (levelAttack > 0) {
						levelAttack -= 1;
						NeedUpdate ();
						upgrade -= 1;
						leveldown = false;
					}
					break;
				case 1:
					if (levelProd > 0) {
						levelProd -= 1;
						NeedUpdate ();
						upgrade -= 1;
						leveldown = false;
					}
					break;
				default:
					if (levelPortal > 0) {
						levelPortal -= 1;
						upgrade -= 1;
						NeedUpdate ();
						leveldown = false;
					}
					break;
				}
			}
			this.team = team;
			RpcSetColor (team);
		}
	}

    #endregion

    #region Rp	c
    [ClientRpc]
    private void RpcSetColor(Team team)
    {
        Shader sh = this.GetComponentInChildren<MeshRenderer>().material.shader;
        this.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Models/Components/Islands/Materials/Team" + (int)team);
        this.GetComponentInChildren<MeshRenderer>().material.shader = sh;
    }

    #endregion
}