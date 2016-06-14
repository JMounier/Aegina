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
    private Controller control;
    private MapGeneration mp;



    // Use this for initialization
    void Start()
    {
        this.bossPos = Vector3.zero;

        this.mp = GameObject.Find("Map").GetComponent<MapGeneration>();
        if (this.mp == null)
        {
            this.bossPos = GameObject.FindGameObjectWithTag("Mob").transform.position;
            this.bSM = GameObject.Find("FightManager").GetComponent<BossSceneManager>();
        }
        if (isLocalPlayer)
        {
            this.syncChar = gameObject.GetComponent<SyncCharacter>();
            this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
            this.control = gameObject.GetComponent<Controller>();
        }

        this.inFight = false;
        if (!isServer)
            return;

        deathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.bossPos == Vector3.zero)
            return;

        if (isServer)
        {

        }


        if (!isLocalPlayer)
            return;

        if (!inFight && Vector3.Distance(this.character.transform.position, this.bossPos) < 28.4f)
        {
            this.inFight = true;
            this.bSM.SpawnWall.SetActive(true);
            CmdEnterFight();
        }
    }


    public void Spec()
    {
        if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            this.bSM.SwitchView(0);
            transform.GetChild(0).gameObject.SetActive(false);
            CmdDead();
        }
    }

    [Command]
    private void CmdEnterFight()
    {
        Debug.Log("+1");
        infightcount++;
    }

    [Command]
    private void CmdDead()
    {
        deathCount++;
        if (deathCount == infightcount)
            Respawn();
    }

    private void Respawn()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log("coucou");
            if(player.GetComponent<BossFight>().inFight)
                player.GetComponent<BossFight>().RpcRestart();
        }
        infightcount = 0;
        deathCount = 0;
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
    }

    public bool BossHere
    {
        get { return this.mp == null; }
    }
    #endregion


}
