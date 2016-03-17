using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Craft_HUD : MonoBehaviour {
    private Inventory inventory;
    private List<Craft> CraftElementary,CraftWorkTop,CraftConsumable,CraftTools,CraftArmor;
    private List<Craft>[] Craftslist;
    private Craft.Type type;
    private bool[] nearwork;
    private GUISkin skin;
    private int craftindex;
    private bool showcraft;
    private int pos;
    private Craft craftshow;
	// Use this for initialization
	void Start ()
    {
        this.inventory = gameObject.GetComponent<Inventory>();
        this.type = Craft.Type.None;
        this.skin = Resources.Load<GUISkin>("Sprites/GUISkin/skin");
        this.Craftslist = new List<Craft>[5];
        this.Craftslist[0] = CraftElementary;
        this.Craftslist[1] = CraftWorkTop;
        this.Craftslist[2] = CraftConsumable;
        this.Craftslist[3] = CraftTools;
        this.Craftslist[4] = CraftArmor;
        craftindex = 0;
        showcraft = false;
        pos = -1;
        craftshow = new Craft(Craft.Type.None);
	}
	
	// Update is called once per frame
    void Update()
    {
        nearwork = what_is_near();
    }
	private void OnGUI ()
    {
        if (inventory.InventoryShown)
        {
            Categoryze();
            if (showcraft)
            {
                Craft_used_HUD(craftshow, pos);
            }
        }
        
	}
    /// <summary>
    /// Affiche l'interface de craft
    /// </summary>
    private void Categoryze()
    {
        for (int i = 0; i < 5; i++)
        {
            Rect box = new Rect(0,i*Screen.height/5,Screen.width/10,Screen.height/5);
            if (GUI.Button(box, Resources.Load<Texture>("Sprites/"/*to fix*/), skin.GetStyle("CraftIndex")))
            {
                craftindex = 0;
                if (this.type == (Craft.Type)(i+1))
                {
                    this.type = Craft.Type.None;
                }
                else
                {
                    this.type = (Craft.Type)(i+1);
                }
                this.showcraft = false;
                this.pos = -1;
                this.craftindex = 0;
            }
        }
        sub_Categoryze(this.type);
    }
    /// <summary>
    /// affiche la liste des crafts disponibles du type selectionne
    /// </summary>
    /// <param name="type"></param>
    private void sub_Categoryze(Craft.Type type)
    {
        if (this.type == Craft.Type.None)
        {
            return;
        }
        Rect box = new Rect(Screen.width / 10, 0, Screen.width / 10, Screen.width / 22);
        if (GUI.Button(box,"",skin.GetStyle("CraftArrowUp")))
        {
            craftindex = (craftindex - 1) % (Craftslist[(int)this.type - 1].Count);
        }
        for (int i = 0; i < 5; i++)
        {
            box = new Rect(Screen.width / 10, Screen.height / 22 + 4 * i * Screen.height / 22, Screen.width / 10, 4 * Screen.height / 22);
            if (GUI.Button(box,Resources.Load<Texture2D>("Sprites/"+Craftslist[(int)this.type-1][(craftindex+i)% (Craftslist[(int)this.type - 1].Count)]),skin.GetStyle("Craft")))
            {
                showcraft = this.pos != i;
                this.pos = i;
                this.craftshow = Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)];
            }
        }
        box = new Rect(Screen.width / 10, 21 * Screen.height / 22, Screen.width / 10, Screen.height / 22);
        if (GUI.Button(box, "", skin.GetStyle("CraftArrowDown")))
        {
            craftindex = (craftindex + 1) % (Craftslist[(int)this.type - 1].Count);
        }
    }
    /// <summary>
    /// affiche les elements necessaires
    /// </summary>
    /// <param name="craft"></param>
    private void Craft_used_HUD(Craft craft,int pos)
    {

    }
    /// <summary>
    /// recherche les atelier proche
    /// </summary>
    /// <returns></returns>
    private bool[] what_is_near()
    {
        return new bool[0];
    }
}
