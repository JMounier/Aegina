using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private Texture2D[] lifeBar;
    private int pv_max;
    private int pv;
    private int hungry_max;
    private int hungry;
    private int thirst_max;
    private int thirst;

    // Use this for initialization
    void Start()
    {
        this.lifeBar = new Texture2D[100];
        for (int i = 0; i < 101; i++)
        {
            this.lifeBar[i] = Resources.Load<Texture2D>("Sprites/Bars/Life/LifeBar" + i.ToString());
        }
        this.pv_max = 100;
        this.pv = this.pv_max;
        this.hungry_max = 100;
        this.hungry = this.hungry_max;
        this.thirst_max = 100;
        this.thirst = this.thirst_max;
    }

    void OnGUI()
    {

    }
    // Update is called once per frame
    void Update()
    {
        GUI.Box()
    }
    
    // Getter/Setter
    public int Pv_max
    {
        get { return this.pv_max; }
        set { Mathf.Clamp(value, 0, 100); }
    }
    public int Pv
    {
        get { return this.pv; }
        set { Mathf.Clamp(value, 0, this.pv_max); }
    }
    public int Hungry_max
    {
        get { return this.hungry_max; }
        set { Mathf.Clamp(value, 0, 100); }
    }
    public int Hungry
    {
        get { return this.hungry; }
        set { Mathf.Clamp(value, 0, this.hungry_max); }
    }
    public int Thirst_max
    {
        get { return this.thirst_max; }
        set { Mathf.Clamp(value, 0, 100); }
    }
    public int Thirst
    {
        get { return this.thirst; }
        set { Mathf.Clamp(value, 0, this.thirst_max); }
    }
}
