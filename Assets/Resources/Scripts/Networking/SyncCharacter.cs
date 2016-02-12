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

    private static float starvation = 0.2f;
    private static float thirstiness = 0.1f;

    private Texture2D[] lifeBar;
    private Texture2D[] hungerBar;
    private Texture2D[] ThirstBar;

    private float pos_x_hungerBar, pos_y_hungerBar;
    private int pos_x_lifeBar, pos_y_lifeBar;
    private int columns = 6;

    private GameObject character;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.lifeMax = 100;
        this.hungerMax = 100;
        this.thirstMax = 100;

        this.pos_x_lifeBar = (Screen.width - this.columns * 50) / 2;
        this.pos_y_lifeBar = Screen.height - 68;
        this.pos_x_hungerBar = Screen.width / 1.03f;
        this.pos_y_hungerBar = Screen.height * 0.0125f;

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
        GUI.DrawTexture((new Rect(this.pos_x_lifeBar, this.pos_y_lifeBar, this.columns * 50, 14)), this.lifeBar[Mathf.CeilToInt(this.life * 100 / this.lifeMax)]);
        GUI.DrawTexture((new Rect(this.pos_x_hungerBar, this.pos_y_hungerBar, Screen.width / 85, Screen.height / 2f)), this.hungerBar[Mathf.CeilToInt(this.hunger * 100 / this.hungerMax)]);
        GUI.DrawTexture((new Rect(this.pos_x_hungerBar - Screen.width * 0.025f, this.pos_y_hungerBar, Screen.width / 85, Screen.height / 2f)), this.ThirstBar[Mathf.CeilToInt(this.thirst * 100 / this.thirstMax)]);
    }

    // Update is called once per frame
    void Update()
    {
        this.Hunger -= Time.deltaTime * starvation;
        this.Thirst -= Time.deltaTime * thirstiness;
        if (this.character.transform.position.y <= -10)
            this.Life -= 15 * Time.deltaTime;
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
        Vector3 newPos = new Vector3(Random.Range(-10f,10f), 8, Random.Range(-10f, 10f));
        this.character.transform.position = newPos;
    }

    /// <summary>
    /// Tue le joueur et le fait respawn dans le monde.
    /// </summary>
    private void Kill()
    {
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
