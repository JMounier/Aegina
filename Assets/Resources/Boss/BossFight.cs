using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BossFight : NetworkBehaviour
{

    private GameObject boss;
    private bool inFight;

    private static int deathCount;
    private static int infightcount;

    private BossSceneManager bSM;
    private GameObject character;
    private SyncCharacter syncChar;



    // Use this for initialization
    void Start()
    {
		this.boss = null;


		if (GameObject.Find("Map").GetComponent<MapGeneration>() == null)
        {
            this.boss = GameObject.FindGameObjectWithTag("Mob");
            this.bSM = GameObject.Find("FightManager").GetComponent<BossSceneManager>();
        }

        if (isLocalPlayer)
        {
            this.syncChar = gameObject.GetComponent<SyncCharacter>();
            this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
            this.inFight = false;
        }

        if (!isServer)
            return;
        infightcount = 0;
        deathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.boss == null)
            return;

		if (this.bSM.Won)
			return;
		if (isServer && this.boss.GetComponent<BossAI>().Life <= 0)
		{
			//edit succes or stats
		}

        if (!isLocalPlayer)
            return;

        if (!inFight && Vector3.Distance(this.character.transform.position, this.boss.transform.position) < 28.4f)
		{
			this.inFight = true;
			CmdEnterFight();
            this.bSM.SpawnWall.SetActive(true);
        }
    }

	private void OnGUI()
	{
		if (this.syncChar.Life <= 0) 
		{
			int delta = 0;
			// draw button to switch spec cam
			this.bSM.SwitchView(delta);
		}
	}

	/// <summary>
	/// call this to use a cristal (consumable) in input manager
	/// </summary>
	/// <returns><c>true</c>, if cristal was used, <c>false</c> otherwise.</returns>
	public bool UseCristal ()
	{
		if(this.BossHere)
		{
			CmdUseCristal ();
			return true;
		}
		return false;
	}

    public void EnterSpec()
    {
       transform.GetChild(0).gameObject.SetActive(false);
       CmdDead();
    }
		
	[Command]
	private void CmdUseCristal()
	{
		this.boss.GetComponent<BossAI> ().UseCristal();
	}

    [Command]
    private void CmdEnterFight()
    {
        infightcount++;
    }

    [Command]
    private void CmdDead()
    {
        deathCount++;
		if (deathCount == infightcount)
			Respawn ();
    }

	/// <summary>
	/// Respawn all player in the fight and reset their inventory MUST BE SERVEUR !!!
	/// </summary>
	private void Respawn()
	{
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) 
		{
			Inventory i = player.GetComponent<Inventory> ();
			i.RpcDropAll ();
			i.RpcLoadInventory (GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject).Inventory);
			player.GetComponent<BossFight> ().RpcRestart ();
        }
		infightcount = 0;
		deathCount = 0;

		this.boss.GetComponent<BossAI> ().Restart ();
	}

    [ClientRpc]
    public void RpcRestart()
    {
		if (!isLocalPlayer)
            return;
		
        this.syncChar.Respawn();
        this.bSM.NotSpecAnyMore();
        this.inFight = false;
    }

    #region Getters/Setters
    public bool InFight
    {
        get { return this.inFight; }
		set { this.inFight = value;}
    }

    public bool BossHere
    {
        get { return this.boss != null; }
    }
    #endregion


}
