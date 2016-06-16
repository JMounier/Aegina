using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BossFight : NetworkBehaviour
{
    public enum State { Infight, Spec, Outfight }
    private GameObject boss;

    private static int deathCount;
    private static int infightcount;

    private State state;

    private BossSceneManager bSM;
    private GameObject character;
    private SyncCharacter syncChar;



    // Use this for initialization
    void Start()
    {
        this.boss = null;

        this.state = State.Outfight;

        if (GameObject.Find("Map").GetComponent<MapGeneration>() == null)
        {
            this.boss = GameObject.FindGameObjectWithTag("Mob");
            this.bSM = GameObject.Find("FightManager").GetComponent<BossSceneManager>();
        }

        if (isLocalPlayer)
        {
            this.syncChar = gameObject.GetComponent<SyncCharacter>();
            this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
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

        if (!isLocalPlayer)
            return;

        if (this.state == State.Outfight && Vector3.Distance(this.character.transform.position, this.boss.transform.position) < 28.4f)
        {
            this.state = State.Infight;
            this.bSM.SpawnWall.SetActive(true);
            CmdEnterFight();
        }
    }

    private void OnGUI()
    {
        if (isLocalPlayer && this.state == State.Spec)
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
    public bool UseCristal()
    {
        if (this.BossHere)
        {
            CmdUseCristal();
            return true;
        }
        return false;
    }

    public void EnterSpec()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        this.state = State.Spec;
        this.bSM.SwitchView(0);
        CmdDead();
    }

    [Command]
    private void CmdUseCristal()
    {
        this.boss.GetComponent<BossAI>().UseCristal();
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
            Respawn();
    }

    /// <summary>
    /// Respawn all player in the fight and reset their inventory MUST BE SERVEUR !!!
    /// </summary>
    private void Respawn()
    {
        infightcount = 0;
        deathCount = 0;
        foreach (Transform loot in GameObject.Find("Loots").transform)
            loot.GetComponent<Loot>().Items.Items.Ent.Life = 0;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Inventory i = player.GetComponent<Inventory>();
            i.RpcLoadInventory(GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject).Inventory);
            player.GetComponent<BossFight>().RpcRestart();
        }

        this.boss.GetComponent<BossAI>().Restart();
    }

    [ClientRpc]
    public void RpcRestart()
    {
        if (!isLocalPlayer)
            return;
        transform.GetChild(0).gameObject.SetActive(true);
        this.bSM.NotSpecAnyMore();
        this.bSM.IncreaseTryCount();
        this.syncChar.Respawn();
        this.state = State.Outfight;
    }

    #region Getters/Setters
    public State MyState
    {
        get { return this.state; }
        set { this.state = value; }
    }   

    public bool BossHere
    {
        get { return this.boss != null; }
    }
    #endregion    
}
