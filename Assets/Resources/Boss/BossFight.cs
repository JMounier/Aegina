using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BossFight : NetworkBehaviour
{

    private Vector3 bossPos;
    private bool inFight;

    private static int deathCount;
    private static int infightcount;

    private BossSceneManager bSM;
    private GameObject character;
    private SyncCharacter syncChar;
    private MapGeneration mg;



    // Use this for initialization
    void Start()
    {
        this.bossPos = Vector3.zero;

        this.mg = GameObject.Find("Map").GetComponent<MapGeneration>();
        if (this.mg == null)
        {
            this.bossPos = GameObject.FindGameObjectWithTag("Mob").transform.position;
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
        if (this.bossPos == Vector3.zero)
            return;
		
        if (!isLocalPlayer)
            return;

        if (!inFight && Vector3.Distance(this.character.transform.position, this.bossPos) < 28.4f)
		{
			this.inFight = true;
			CmdEnterFight();
            this.bSM.SpawnWall.SetActive(true);
        }
    }


    public void EnterSpec()
    {
       this.bSM.SwitchView(0);
       transform.GetChild(0).gameObject.SetActive(false);
       CmdDead();
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
	/// Respawn all player in the fight.
	/// </summary>
	private void Respawn()
	{
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
			player.GetComponent<BossFight> ().RpcRestart ();
		infightcount = 0;
		deathCount = 0;
	}

    [ClientRpc]
    public void RpcRestart()
    {
		if (!isLocalPlayer || !this.inFight)
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
        get { return this.mg == null; }
    }
    #endregion


}
