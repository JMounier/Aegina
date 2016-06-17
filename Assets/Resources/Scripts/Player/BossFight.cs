using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BossFight : NetworkBehaviour
{
    public enum State { Infight, Spec, Outfight }

    private static int deathCount;
    private static int infightcount;
    private static Texture2D[] Bosslife = new Texture2D[101];
    [SyncVar]
    private float syncBossLife;
    private State state;

    private GameObject character;
    private SyncCharacter syncChar;
    private Controller cnt;


    // Use this for initialization
    void Start()
    {
        if (isServer && SceneManager.GetActiveScene().name != "main")
        {
            this.syncBossLife = 500;
            this.syncChar = gameObject.GetComponent<SyncCharacter>();
        }
        if (!isLocalPlayer || SceneManager.GetActiveScene().name == "main")
            return;
        this.state = State.Outfight;

        this.syncChar = gameObject.GetComponent<SyncCharacter>();
        this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
        this.cnt = gameObject.GetComponent<Controller>();

        for (int i = 0; i < 101; i++)
            Bosslife[i] = Resources.Load<Texture2D>("Sprites/Bars/BossLife/BossLifeBar" + i.ToString());
        if (!isServer)
            return;
        infightcount = 0;
        deathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "main" || GameObject.FindGameObjectWithTag("Mob") == null)
            return;
        if (isServer)
            this.syncBossLife = GameObject.FindGameObjectWithTag("Mob").GetComponent<SyncBoss>().Life;
        if (!isLocalPlayer)
            return;

        if (this.state == State.Outfight && Vector3.Distance(this.character.transform.position, GameObject.FindGameObjectWithTag("Mob").transform.position) < 28.4f)
        {
            this.state = State.Infight;
            GameObject.Find("FightManager").GetComponent<BossSceneManager>().SpawnWall.SetActive(true);
            CmdEnterFight();
        }
    }

    private void OnGUI()
    {
        if (SceneManager.GetActiveScene().name == "main")
            return;

        if (isLocalPlayer && this.state == State.Spec)
        {
            int delta = 0;
            // draw button to switch spec cam
            GameObject.Find("FightManager").GetComponent<BossSceneManager>().SwitchView(delta);
        }
        if (this.state == State.Infight || this.state == State.Spec)
            GUI.DrawTexture(new Rect(Screen.width / 5, Screen.height / (5 * 8.86f), 3 * Screen.width / 5, Screen.height / 15), Bosslife[(int)(syncBossLife / 5)]);
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
        GameObject.Find("FightManager").GetComponent<BossSceneManager>().SwitchView(0);
        CmdDead();
    }

    public void receiveDamageByBoss()
    {
        if (isLocalPlayer)
            CmdReceiveDamageBoss(gameObject.GetComponent<Inventory>().Armor);
    }

    public void ShockWave()
    {
        if (isServer)
            this.RpcShockWave();
    }

    [ClientRpc]
    private void RpcShockWave()
    {
        if (isLocalPlayer && !this.cnt.IsJumping && this.state == State.Infight)
        {
            this.syncChar.Life -= 50 * 100 / gameObject.GetComponentInParent<Inventory>().Armor;
            Vector3 dir = (this.character.transform.position - GameObject.FindGameObjectWithTag("Boss").transform.position ).normalized;
            gameObject.GetComponentInChildren<Rigidbody>().AddForce(dir.x * 15000f, 10000f, dir.z * 15000f);
            gameObject.GetComponentInParent<Controller>().CdDisable = 0.5f;
        }
    }

    [Command]
    private void CmdReceiveDamageBoss(float armor)
    {
        this.syncChar.Life -= 100 * GameObject.FindGameObjectWithTag("Mob").GetComponent<SyncBoss>().Damage / armor;
    }

    [Command]
    private void CmdUseCristal()
    {
        GameObject.FindGameObjectWithTag("Mob").GetComponent<SyncBoss>().UseCristal();
    }

    [Command]
    private void CmdEnterFight()
    {
        infightcount++;
        GameObject.FindGameObjectWithTag("Mob").GetComponent<SyncBoss>().Fight = true;
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

        GameObject.FindGameObjectWithTag("Mob").GetComponent<SyncBoss>().Restart();
    }

    [ClientRpc]
    public void RpcRestart()
    {
        if (!isLocalPlayer)
            return;
        transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FightManager").GetComponent<BossSceneManager>().NotSpecAnyMore();
        GameObject.Find("FightManager").GetComponent<BossSceneManager>().IncreaseTryCount();
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
        get { return GameObject.FindGameObjectWithTag("Mob") != null; }
    }
    #endregion    
}
