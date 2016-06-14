using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BossFight : NetworkBehaviour
{

    private Vector3 bossPos;
    private bool inFight;
    private static int deathCount;

    private BossSceneManager bSM;
    private GameObject character;
    private SyncCharacter syncChar;
    private Controller control;


    private List<GameObject> playersInFight;

    // Use this for initialization
    void Start()
    {
        this.bossPos = Vector3.zero;
        if (GameObject.Find("Map").GetComponent<MapGeneration>() != null)
            this.bossPos = GameObject.FindGameObjectWithTag("Mob").transform.position;
        this.bSM = GameObject.Find("FightManager").GetComponent<BossSceneManager>();
        this.syncChar = gameObject.GetComponent<SyncCharacter>();
        this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
        this.control = gameObject.GetComponent<Controller>();


        this.inFight = false;
        if (!isServer)
            return;

        deathCount = 0;
        this.playersInFight = new List<GameObject>();
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
            CmdEnterFight(gameObject);
        }
    }


    public void Spec()
    {
        if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            CmdDead();
        }
    }

    [Command]
    private void CmdEnterFight(GameObject player)
    {
        if (!this.playersInFight.Contains(player))
        {
            this.playersInFight.Add(player);
            this.bSM.SpawnWall.SetActive(true);
        }
    }

    [Command]
    private void CmdDead()
    {
        deathCount++;
        if (deathCount == this.playersInFight.Count)
            Respawn();
    }

    private void Respawn()
    {
        foreach (GameObject player in this.playersInFight)
        {
            // peut etre transmettre l'inventaire ici
            player.GetComponent<BossFight>().RpcRestart();
        }
    }

    [ClientRpc]
    public void RpcRestart()
    {
        if (!isLocalPlayer)
            return;

        //rendre l'inventaire.

        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.character.transform.position = Vector3.up * 7;

        this.syncChar.Respawn();

        this.bSM.SpawnWall.SetActive(false);
        this.inFight = false;
    }

    #region Getters/Setters
    public bool InFight
    {
        get { return this.inFight; }
    }
    #endregion


}
