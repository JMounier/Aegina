using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCharacter : NetworkBehaviour
{
    // Carateristiques
    private int lifeMax;
    [SyncVar]
    private float life;
    private int hungerMax;
    [SyncVar]
    private float hunger;
    private int thirstMax;
    [SyncVar]
    private float thirst;

    // Bonus / Malus
    [SyncVar]
    private float speed;
    [SyncVar]
    private float cdSpeed;

    [SyncVar]
    private float jump;
    [SyncVar]
    private float cdJump;

    [SyncVar]
    private float regen;
    [SyncVar]
    private float cdRegen;

    [SyncVar]
    private float poison;
    [SyncVar]
    private float cdPoison;

    // Constants
    private readonly static float starvation = 0.1f;
    private readonly static float thirstiness = 0.2f;

    private Texture2D[] lifeBar;
    private Texture2D[] hungerBar;
    private Texture2D[] ThirstBar;


    private GameObject character;
    private Inventory inventory;
    private Controller controller;

    // Use this for initialization
    void Start()
    {

        if (!isLocalPlayer)
            return;

        this.inventory = gameObject.GetComponent<Inventory>();
        this.controller = gameObject.GetComponent<Controller>();

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

        this.character = gameObject.transform.FindChild("Character").gameObject;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            //Debug.Log(this.life);
            return;
        }

        if (this.life <= 0)
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
            this.Life -= 30 * Time.deltaTime;
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
            gameObject.GetComponent<Social>().CmdSendActivity(Activity.Death, gameObject);
            this.CmdLife(this.lifeMax);
            this.CmdHunger(this.hungerMax);
            this.CmdThirst(this.thirstMax);
            this.CmdPoison(0);
            this.CmdRegen(0);
            this.CmdSpeed(0);
            this.CmdJump(0);
            this.character.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 newPos = new Vector3(Random.Range(-10f, 10f), 7, Random.Range(-10f, 10f));
            this.character.transform.position = newPos;
        }
    }
    /// <summary>
    /// informe le joueur au'il doit prendre des degats. MUST BE SERVER
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        this.Life -= damage; //il faudra gere l'armure
    }

    [Command]
    private void CmdLoad()
    {

        PlayerSave save = GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject);

        this.lifeMax = 100;
        this.thirstMax = 100;
        this.hungerMax = 100;

        this.life = save.Life;
        this.hunger = save.Hunger;
        this.thirst = save.Thirst;
        this.speed = save.Speed;
        this.cdSpeed = save.CdSpeed;
        this.jump = save.Jump;
        this.cdJump = save.CdJump;
        this.regen = save.Regen;
        this.cdRegen = save.CdRegen;
        this.poison = save.Poison;
        this.cdPoison = save.CdPoison;

        Vector3 newPos = new Vector3(save.X, 7, save.Y);
        gameObject.GetComponent<Social>().RpcTeleport(newPos);
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
            if (isServer)
                this.life = Mathf.Clamp(value, 0f, this.lifeMax);
            else
                this.CmdLife(Mathf.Clamp(value, 0f, this.lifeMax));
        }
    }

    [Command]
    private void CmdLife(float life)
    {
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
            if (isServer)
                this.hunger = Mathf.Clamp(value, 0f, this.hungerMax);
            else
                this.CmdHunger(Mathf.Clamp(value, 0f, this.hungerMax));
        }
    }

    [Command]
    private void CmdHunger(float hunger)
    {
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
            if (isServer)
                this.thirst = Mathf.Clamp(value, 0f, this.thirstMax);
            else
                this.CmdThirst(Mathf.Clamp(value, 0f, this.thirstMax));
        }
    }

    [Command]
    private void CmdThirst(float thirst)
    {
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
            if (isServer)
                this.speed = value;
            else
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
            if (isServer)
                this.cdSpeed = value;
            else
                this.CmdCdSpeed(value);
        }
    }

    [Command]
    private void CmdCdSpeed(float cdSpeed)
    {
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
            if (isServer)
                this.jump = value;
            else
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
            if (isServer)
                this.cdJump = value;
            else
                this.CmdCdJump(value);
        }
    }

    [Command]
    private void CmdCdJump(float cdJump)
    {
        this.cdJump = cdJump;
    }

    /// <summary>
    /// Le bonus de regen du personnage.
    /// </summary>
    public float Regen
    {
        get { return this.regen; }
        set
        {
            if (isServer)
                this.regen = value;
            else
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
            if (isServer)
                this.cdRegen = value;
            else
                this.CmdCdRegen(value);
        }
    }

    [Command]
    private void CmdCdRegen(float cdRegen)
    {
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
            if (isServer)
                this.poison = value;
            else
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
            if (isServer)
                this.cdPoison = value;
            else
                this.CmdCdPoison(value);
        }
    }

    [Command]
    private void CmdCdPoison(float cdPoison)
    {
        this.cdPoison = cdPoison;
    }
    #endregion
}
