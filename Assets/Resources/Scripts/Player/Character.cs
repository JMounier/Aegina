using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private Texture2D[] lifeBar;
    private Texture2D[] hungerBar;
    private Texture2D[] ThirstBar;
    private int pv_max;
    private int pv;
    private int hunger_max;
    private int hunger;
    private int thirst_max;
    private int thirst;
    private int pos_x_hungerBar, pos_y_hungerBar;
    private int pos_x_lifeBar, pos_y_lifeBar;
    private int columns = 6;
    private float cd_max;
    private float cd;
    // Use this for initialization
    void Start()
    {
        this.pv_max = 100;
        this.pv = this.pv_max;
        this.hunger_max = 100;
        this.hunger = this.hunger_max;
        this.thirst_max = 100;
        this.thirst = this.thirst_max;
        this.pos_x_lifeBar = (Screen.width - this.columns * 50) / 2;
        this.pos_y_lifeBar = Screen.height - 68;
        this.pos_x_hungerBar = Screen.width - 25;
        this.pos_y_hungerBar = 10;
        this.cd_max = 3;
        this.cd = this.cd_max;
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
    }

    void OnGUI()
    {
        GUI.DrawTexture((new Rect(this.pos_x_lifeBar, this.pos_y_lifeBar, this.columns * 50, 14)), this.lifeBar[this.pv * 100 / this.pv_max]);
        GUI.DrawTexture((new Rect(this.pos_x_hungerBar, this.pos_y_hungerBar, 7.8f, 175)), this.hungerBar[this.hunger * 100 / this.hunger_max]);
        GUI.DrawTexture((new Rect(this.pos_x_hungerBar - 15, this.pos_y_hungerBar, 7.8f, 175)), this.ThirstBar[this.thirst * 100 / this.thirst_max]);
    }
    // Update is called once per frame
    void Update()
    {
        if (this.thirst == 0 || this.hunger == 0)
            this.pv = 0;
        if (cooldown(this.cd))
        {
            this.thirst -= 1;
            this.hunger -= 1;
            this.cd = this.cd_max;
        }
        else
            this.cd -= Time.deltaTime;
    }
    /// <summary>
    /// Permet de savoir quand il faut enlever des points dans les barres de faim ou soif
    /// </summary>

    public bool cooldown(float n)
    {
        if (n > 0)
            return false;
        return true;
    }


    // Getter/Setter
    public int Pv_max
    {
        get { return this.pv_max; }
        set { Mathf.Max(value, 0); }
    }
    public int Pv
    {
        get { return this.pv; }
        set { Mathf.Max(value, 0); }
    }
    public int Hunger_max
    {
        get { return this.hunger_max; }
        set { Mathf.Max(value, 0); }
    }
    public int Hunger
    {
        get { return this.hunger; }
        set { Mathf.Max(value, 0); }
    }
    public int Thirst_max
    {
        get { return this.thirst_max; }
        set { Mathf.Max(value, 0); }
    }
    public int Thirst
    {
        get { return this.thirst; }
        set { Mathf.Max(value, 0); }
    }
}
