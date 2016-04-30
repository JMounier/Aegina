using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SyncCharacter : NetworkBehaviour
{
    // Carateristiques
    private int lifeMax;
    private float life;

    private int hungerMax;
    private float hunger;

    private int thirstMax;
    private float thirst;

    // Bonus / Malus
    [SyncVar]
    private float speed;
    private float cdSpeed;

    [SyncVar]
    private float jump;
    private float cdJump;

    [SyncVar]
    private float regen;
    private float cdRegen;

    [SyncVar]
    private float poison;
    private float cdPoison;

    // Constants
    private readonly static float starvation = 0.1f;
    private readonly static float thirstiness = 0.2f;

    private GUISkin skin;
    private Texture2D[] lifeBar;
    private Texture2D[] hungerBar;
    private Texture2D[] ThirstBar;


    private GameObject character;
    private Inventory inventory;
    private Controller controller;

    // Use this for initialization
    void Start()
    {
        this.character = gameObject.transform.FindChild("Character").gameObject;

        if (!isLocalPlayer)
            return;

        this.inventory = gameObject.GetComponent<Inventory>();
        this.controller = gameObject.GetComponent<Controller>();
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");

        this.lifeMax = 100;
        this.hungerMax = 100;
        this.thirstMax = 100;
        this.life = 100;
        this.hunger = 100;
        this.thirst = 100;
        this.speed = 0;
        this.cdSpeed = 0;
        this.jump = 0;
        this.cdJump = 0;
        this.regen = 0;
        this.cdRegen = 0;
        this.poison = 0;
        this.cdPoison = 0;


        // Initialisation des images
        this.lifeBar = new Texture2D[101];
        for (int i = 0; i < 101; i++)
            this.lifeBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Life/LifeBar" + i.ToString());

        this.hungerBar = new Texture2D[101];
        for (int i = 0; i < 101; i++)
            this.hungerBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Hunger/HungerBar" + i.ToString());

        this.ThirstBar = new Texture2D[101];
        for (int i = 0; i < 101; i++)
            this.ThirstBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Thirst/ThirstBar" + i.ToString());

        this.CmdLoad();
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        // Bars
        GUI.DrawTexture((new Rect((Screen.width - this.inventory.Columns * this.inventory.ToolbarSize) / 2, (int)(Screen.height - this.inventory.ToolbarSize * 1.6f), this.inventory.Columns * this.inventory.ToolbarSize, this.inventory.ToolbarSize / 3.5f)), this.lifeBar[Mathf.CeilToInt(this.life * 100 / this.lifeMax)]);
        GUI.DrawTexture((new Rect(Screen.width / 1.03f, Screen.height * 0.0125f, Screen.width / 85, Screen.height / 2f)), this.hungerBar[Mathf.CeilToInt(this.hunger * 100 / this.hungerMax)]);
        GUI.DrawTexture((new Rect(Screen.width / 1.03f - Screen.width * 0.025f, Screen.height * 0.0125f, Screen.width / 85, Screen.height / 2f)), this.ThirstBar[Mathf.CeilToInt(this.thirst * 100 / this.thirstMax)]);

        if (this.life <= 0)
        {
            if (GUI.Button(new Rect(5 * Screen.width / 14, Screen.height / 2 - 100, 2 * Screen.width / 7, 100), TextDatabase.Respawn.GetText(), skin.GetStyle("button")))
            {
                Respawn();
                GetComponent<Sound>().PlaySound(AudioClips.Button, 1f);
            }
            if (GUI.Button(new Rect(5 * Screen.width / 14, Screen.height / 2 + 100, 2 * Screen.width / 7, 100), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
            {
                Respawn();
                GetComponent<Sound>().PlaySound(AudioClips.Button, 1f);
                if (isServer)
                {
                    GameObject.Find("Map").GetComponent<Save>().SaveWorld();
                    GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
                }
                else
                    GetComponent<Menu>().CmdDisconnect();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            if (this.life <= 0 && gameObject.transform.FindChild("Character").GetComponent<CapsuleCollider>().enabled)
            {
                gameObject.transform.FindChild("Character").FindChild("Armature").gameObject.SetActive(false);
                gameObject.transform.FindChild("Character").FindChild("NPC_Man_Normal001").gameObject.SetActive(false);
                gameObject.transform.FindChild("Character").GetComponent<CapsuleCollider>().enabled = false;
            }
            else if (this.life > 0 && !gameObject.transform.FindChild("Character").GetComponent<CapsuleCollider>().enabled)
            {
                gameObject.transform.FindChild("Character").FindChild("Armature").gameObject.SetActive(true);
                gameObject.transform.FindChild("Character").FindChild("NPC_Man_Normal001").gameObject.SetActive(true);
                gameObject.transform.FindChild("Character").GetComponent<CapsuleCollider>().enabled = true;
            }
            return;
        }

        if (this.life <= 0 && gameObject.transform.FindChild("Character").FindChild("Armature").gameObject.activeInHierarchy)
            this.Kill();

        // Bonus
        if (this.cdJump <= 0)
            this.Jump = 0;
        else
            this.CdJump -= Time.deltaTime;

        if (this.cdSpeed <= 0)
            this.Speed = 0;
        else
            this.CdSpeed -= Time.deltaTime;

        if (this.cdRegen <= 0)
            this.Regen = 0;
        else
        {
            this.Life += Time.deltaTime * this.regen;
            this.CdRegen -= Time.deltaTime;
        }

        if (this.cdPoison <= 0)
            this.Poison = 0;
        else
        {
            this.Life -= Time.deltaTime * this.poison;
            this.CdPoison -= Time.deltaTime;
        }



        this.Hunger -= Time.deltaTime * starvation;
        this.Thirst -= Time.deltaTime * thirstiness;
        if (this.character.transform.position.y <= -10)
        {
            this.Life -= 30 * Time.deltaTime;
        }
        if (this.character.transform.position.y < 0 && !this.controller.IsJumping)
            this.controller.IsJumping = true;
        if (this.hunger == 0 || this.thirst == 0)
            this.Life -= Time.deltaTime;
    }

    /// <summary>
    /// Tue le joueur et le fait respawn dans le monde.
    /// </summary>
    private void Kill()
    {
        if (isLocalPlayer)
        {
            gameObject.transform.FindChild("Character").FindChild("Armature").gameObject.SetActive(false);
            gameObject.transform.FindChild("Character").FindChild("NPC_Man_Normal001").gameObject.SetActive(false);
            gameObject.transform.FindChild("Character").GetComponent<CapsuleCollider>().enabled = false;
            gameObject.transform.FindChild("Character").GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Inventory>().DropAll();
            gameObject.GetComponent<Social_HUD>().CmdSendActivity(Activity.Death);
            GetComponent<InputManager>().IAmDead();
        }
    }

    private void Respawn()
    {
        gameObject.transform.FindChild("Character").FindChild("Armature").gameObject.SetActive(true);
        gameObject.transform.FindChild("Character").FindChild("NPC_Man_Normal001").gameObject.SetActive(true);
        gameObject.transform.FindChild("Character").GetComponent<CapsuleCollider>().enabled = true;
        gameObject.transform.FindChild("Character").GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Controller>().Pause = false;
        this.Life = this.lifeMax;
        this.Hunger = this.hungerMax;
        this.Thirst = this.thirstMax;
        this.Poison = 0;
        this.Regen = 0;
        this.Speed = 0;
        this.Jump = 0;
        this.character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        CmdRespawnPos();
    }

    [Command]
    private void CmdRespawnPos()
    {
        List<Vector2> listrespos = GameObject.Find("Map").GetComponent<Save>().Respawn[(int)gameObject.GetComponent<Social_HUD>().Team];
        Vector2 resPos = listrespos[Random.Range(0, listrespos.Count)];
        Vector3 newPos = new Vector3(resPos.x + Random.Range(-10f, 10f), 7, resPos.y + Random.Range(-10f, 10f));
        while (!Graph.isValidPosition(newPos))
            newPos = new Vector3(resPos.x + Random.Range(-10f, 10f), 7, resPos.y + Random.Range(-10f, 10f));
        gameObject.GetComponent<Social_HUD>().RpcTeleport(newPos);
    }
    /// <summary>
    /// Informe le joueur qu'il doit prendre des degats. MUST BE SERVER
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        SyncChunk chunk = null;
        foreach (Collider col in Physics.OverlapBox(this.character.transform.position, new Vector3(5, 100, 5)))
        {
            if (col.gameObject.name.Contains("Island"))
            {
                chunk = col.transform.parent.GetComponent<SyncChunk>();
                break;
            }
        }
        float armor = 100;
        if (chunk != null && chunk.IsCristal && chunk.Cristal.Team == gameObject.GetComponent<Social_HUD>().Team)        
            armor += chunk.Cristal.LevelAtk * 20;
        
        this.Life -= 100 * damage / armor;
    }

    [Command]
    private void CmdLoad()
    {

        PlayerSave save = GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject);

        this.lifeMax = 100;
        this.thirstMax = 100;
        this.hungerMax = 100;

        this.Life = save.Life;
        this.Hunger = save.Hunger;
        this.Thirst = save.Thirst;
        this.Speed = save.Speed;
        this.CdSpeed = save.CdSpeed;
        this.Jump = save.Jump;
        this.CdJump = save.CdJump;
        this.Regen = save.Regen;
        this.CdRegen = save.CdRegen;
        this.Poison = save.Poison;
        this.CdPoison = save.CdPoison;

        Vector3 newPos = new Vector3(save.X, 7, save.Y);
        gameObject.GetComponent<Social_HUD>().RpcTeleport(newPos);
    }

    #region Getter & Setter
    /// <summary>
    /// La vie de l'entite.
    /// </summary>
    public float Life
    {
        get { return this.life; }
        set
        {
            this.life = Mathf.Clamp(value, 0f, this.lifeMax);
            this.CmdLife(this.life, !isLocalPlayer);
        }
    }

    [Command]
    private void CmdLife(float life, bool fromServer)
    {
        this.life = life;
        RpcLife(life, fromServer);
    }

    [ClientRpc]
    private void RpcLife(float life, bool fromServer)
    {
        if (fromServer || !isLocalPlayer)
            this.life = life;
    }

    /// <summary>
    /// La vie maximum de l'entite.
    /// </summary>
    public int LifeMax
    {
        get { return this.lifeMax; }
        set
        {
            this.lifeMax = Mathf.Max(value, 0);
            this.Life = Mathf.Clamp(value, 0f, this.lifeMax);
        }
    }

    /// <summary>
    /// La faim maximum du joueur.
    /// </summary>
    public int HungerMax
    {
        get { return this.hungerMax; }
        set
        {
            this.hungerMax = Mathf.Max(value, 0);
            this.Hunger = Mathf.Clamp(value, 0f, this.hungerMax);
        }
    }

    /// <summary>
    /// La faim du joueur.
    /// </summary>
    public float Hunger
    {
        get { return this.hunger; }
        set
        {
            this.hunger = Mathf.Clamp(value, 0f, this.hungerMax);
            this.CmdHunger(Mathf.Clamp(value, 0f, this.hungerMax));
        }
    }

    [Command]
    private void CmdHunger(float hunger)
    {
        this.hunger = hunger;
        RpcHunger(hunger);
    }

    [ClientRpc]
    private void RpcHunger(float hunger)
    {
        if (!isLocalPlayer)
            this.hunger = hunger;
    }

    /// <summary>
    /// La soif maximum du joueur.
    /// </summary>
    public int ThirstMax
    {
        get { return this.thirstMax; }
        set
        {
            this.thirstMax = Mathf.Max(value, 0);
            this.Thirst = Mathf.Clamp(value, 0f, this.thirstMax);
        }
    }

    /// <summary>
    /// La soif du joueur.
    /// </summary>
    public float Thirst
    {
        get { return this.thirst; }
        set
        {
            this.thirst = Mathf.Clamp(value, 0f, this.thirstMax);
            this.CmdThirst(Mathf.Clamp(value, 0f, this.thirstMax));
        }
    }

    [Command]
    private void CmdThirst(float thirst)
    {
        this.thirst = thirst;
        RpcThirst(thirst);
    }

    [ClientRpc]
    private void RpcThirst(float thirst)
    {
        if (!isLocalPlayer)
            this.thirst = thirst;
    }

    /// <summary>
    /// Le bonus de vitesse du personnage.
    /// </summary>
    public float Speed
    {
        get { return this.speed; }
        set
        {
            this.speed = value;
            this.CmdSpeed(value);
        }
    }

    [Command]
    private void CmdSpeed(float speed)
    {
        this.speed = speed;
    }

    /// <summary>
    /// La duree du bonus de vitesse restant du personnage.
    /// </summary>
    public float CdSpeed
    {
        get { return this.cdSpeed; }
        set
        {
            this.cdSpeed = value;
            this.CmdCdSpeed(value);
        }
    }

    [Command]
    private void CmdCdSpeed(float cdSpeed)
    {
        this.cdSpeed = cdSpeed;
        RpcCdSpeed(cdSpeed);
    }

    [ClientRpc]
    private void RpcCdSpeed(float cdSpeed)
    {
        if (!isLocalPlayer)
            this.cdSpeed = cdSpeed;
    }

    /// <summary>
    /// Le bonus de saut du personnage.
    /// </summary>
    public float Jump
    {
        get { return this.jump; }
        set
        {
            this.jump = value;
            this.CmdJump(value);
        }
    }

    [Command]
    private void CmdJump(float jump)
    {
        this.jump = jump;
    }

    /// <summary>
    /// La duree du bonus de saut restant du personnage.
    /// </summary>
    public float CdJump
    {
        get { return this.cdJump; }
        set
        {
            this.cdJump = value;
            this.CmdCdJump(value);
        }
    }

    [Command]
    private void CmdCdJump(float cdJump)
    {
        this.cdJump = cdJump;
        RpcCdJump(cdJump);
    }

    [ClientRpc]
    private void RpcCdJump(float cdJump)
    {
        if (!isLocalPlayer)
            this.cdSpeed = cdJump;
    }

    /// <summary>
    /// Le bonus de regen du personnage.
    /// </summary>
    public float Regen
    {
        get { return this.regen; }
        set
        {
            this.regen = value;
            this.CmdRegen(value);
        }
    }

    [Command]
    private void CmdRegen(float regen)
    {
        this.regen = regen;
    }

    /// <summary>
    /// La duree du bonus de regen restant du personnage.
    /// </summary>
    public float CdRegen
    {
        get { return this.cdRegen; }
        set
        {
            this.cdRegen = value;
            this.CmdCdRegen(value);
        }
    }

    [Command]
    private void CmdCdRegen(float cdRegen)
    {
        this.cdRegen = cdRegen;
        RpcCdRegen(cdRegen);
    }

    [ClientRpc]
    private void RpcCdRegen(float cdRegen)
    {
        if (!isLocalPlayer)
            this.cdRegen = cdRegen;
    }

    /// <summary>
    /// Le bonus de saut du personnage.
    /// </summary>
    public float Poison
    {
        get { return this.poison; }
        set
        {
                this.poison = value;
                this.CmdPoison(value);
        }
    }

    [Command]
    private void CmdPoison(float poison)
    {
        this.poison = poison;
    }

    /// <summary>
    /// La duree du bonus de saut restant du personnage.
    /// </summary>
    public float CdPoison
    {
        get { return this.cdPoison; }
        set
        {
                this.cdPoison = value;
                this.CmdCdPoison(value);
        }
    }

    [Command]
    private void CmdCdPoison(float cdPoison)
    {
        this.cdPoison = cdPoison;
        RpcCdPoison(cdPoison);
    }

    [ClientRpc]
    private void RpcCdPoison(float cdPoison)
    {
        if (!isLocalPlayer)
            this.cdPoison = cdPoison;
    }
    #endregion
}
