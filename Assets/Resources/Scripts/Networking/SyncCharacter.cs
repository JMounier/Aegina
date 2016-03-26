using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCharacter : NetworkBehaviour
{

    private int lifeMax;
    private float life;
    private int hungerMax;
    private float hunger;
    private int thirstMax;
    private float thirst;

    private static float starvation = 0.1f;
    private static float thirstiness = 0.2f;

    private Texture2D[] lifeBar;
    private Texture2D[] hungerBar;
    private Texture2D[] ThirstBar;

    private float pos_x_hungerBar, pos_y_hungerBar;
    private int pos_x_lifeBar, pos_y_lifeBar;

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

        this.lifeBar = new Texture2D[101];
        for (int i = 0; i < 101; i++)
        {
            this.lifeBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Life/LifeBar" + i.ToString());
        }

        this.hungerBar = new Texture2D[101];
        for (int i = 0; i < 101; i++)
        {
            this.hungerBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Hunger/HungerBar" + i.ToString());
        }

        this.ThirstBar = new Texture2D[101];
        for (int i = 0; i < 101; i++)
        {
            this.ThirstBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Thirst/ThirstBar" + i.ToString());
        }

        this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
        this.Spawn();
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
            return;
        GUI.DrawTexture((new Rect((Screen.width - this.inventory.Columns * this.inventory.ToolbarSize) / 2, (int)(Screen.height - this.inventory.ToolbarSize * 1.6f), this.inventory.Columns * this.inventory.ToolbarSize, this.inventory.ToolbarSize/3.5f)), this.lifeBar[Mathf.CeilToInt(this.life * 100 / this.lifeMax)]);
        GUI.DrawTexture((new Rect(Screen.width / 1.03f, Screen.height * 0.0125f, Screen.width / 85, Screen.height / 2f)), this.hungerBar[Mathf.CeilToInt(this.hunger * 100 / this.hungerMax)]);
        GUI.DrawTexture((new Rect(Screen.width / 1.03f - Screen.width * 0.025f, Screen.height * 0.0125f, Screen.width / 85, Screen.height / 2f)), this.ThirstBar[Mathf.CeilToInt(this.thirst * 100 / this.thirstMax)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        this.Hunger -= Time.deltaTime * starvation;
        this.Thirst -= Time.deltaTime * thirstiness;
        if (this.character.transform.position.y <= -10)
            this.Life -= 20 * Time.deltaTime;
        if (this.character.transform.position.y < 0 && !this.controller.IsJumping)
            this.controller.IsJumping = true;
        if (this.hunger == 0 || this.thirst == 0)
            this.Life -= Time.deltaTime;
    }

    /// <summary>
    /// Spawn le joueur.
    /// </summary>
    private void Spawn()
    {
        this.life = this.lifeMax;
        this.hunger = this.hungerMax;
        this.thirst = this.thirstMax;
        this.character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 newPos = new Vector3(Random.Range(-10f, 10f), 7, Random.Range(-10f, 10f));
        this.character.transform.position = newPos;
    }

    /// <summary>
    /// Tue le joueur et le fait respawn dans le monde.
    /// </summary>
    private void Kill()
    {
        gameObject.GetComponent<Social>().CmdSendActivity(Activity.Death, gameObject);
        this.Spawn();
    }

    // Getter/Setter
    /// <summary>
    /// La vie de l'entite.
    /// </summary>
    public float Life
    {
        get { return this.life; }
        set
        {
            this.life = Mathf.Clamp(value, 0f, this.lifeMax);
            if (this.life == 0)
                this.Kill();
        }
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
            this.life = Mathf.Clamp(value, 0f, this.lifeMax);
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
            this.hunger = Mathf.Clamp(value, 0f, this.hungerMax);
        }
    }

    /// <summary>
    /// La faim du joueur.
    /// </summary>
    public float Hunger
    {
        get { return this.hunger; }
        set { this.hunger = Mathf.Clamp(value, 0f, this.hungerMax); }
    }

    /// <summary>
    /// La soif maximum du joueur.
    /// </summary>
    public int Thirst_max
    {
        get { return this.thirstMax; }
        set
        {
            this.thirstMax = Mathf.Max(value, 0);
            this.thirst = Mathf.Clamp(value, 0f, this.thirstMax);
        }
    }

    /// <summary>
    /// La soif du joueur.
    /// </summary>
    public float Thirst
    {
        get { return this.thirst; }
        set { this.thirst = Mathf.Clamp(value, 0f, this.thirstMax); }
    }
}
