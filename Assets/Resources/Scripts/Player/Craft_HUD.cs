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
        this.CraftElementary = new List<Craft>();
        this.CraftElementary.Add(CraftDatabase.IronIngot);
        this.CraftElementary.Add(CraftDatabase.GoldIngot);
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
        if (type != Craft.Type.None)
        {
            this.craftindex = (this.craftindex - (int)Input.mouseScrollDelta.y) % (Craftslist[(int)this.type - 1].Count);
            while (craftindex < 0)
            {
                craftindex += (Craftslist[(int)this.type - 1].Count);
            }
        }
    }
	private void OnGUI ()
    {
        if (inventory.InventoryShown)
        {
            Categoryze();
            if (showcraft)
            {
                Craft_used_HUD();
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
            Rect box = new Rect(0,(i+2)*Screen.height/9,Screen.height/9,Screen.height/9);
            if (GUI.Button(box, Resources.Load<Texture>("Sprites/"/*to fix*/), skin.GetStyle("Inventory")))
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
        Rect box = new Rect();
        if (GUI.Button(box,"",skin.GetStyle("Inventory")))
        {
            craftindex = (craftindex - 1) % (Craftslist[(int)this.type - 1].Count);
        }
        for (int i = 0; i < 10; i++)
        {
            box = new Rect(Screen.height / 9, 2 * Screen.height / 9 + i * Screen.height / 18, Screen.height / 18, Screen.height / 18);
            if (i == 0 || i == 9)
            {
                GUI.Box(box,Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Items.Icon, skin.GetStyle("Inventory"));
            }
            else if (GUI.Button(box, Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Items.Icon, skin.GetStyle("Inventory")))
            {
                showcraft = this.pos != i;
                this.pos = i;
                this.craftshow = Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)];
            }
        }
        box = new Rect(Screen.height / 9, 2 * Screen.height / 9, Screen.height / 18, Screen.height / 36);
        if (GUI.Button(box, "", skin.GetStyle("Inventory")))
        {
            craftindex = (craftindex - 1) % (Craftslist[(int)this.type - 1].Count);
        }
        box = new Rect(Screen.height / 9, 7 * Screen.height / 9 - Screen.height / 38, Screen.height / 18, Screen.height / 36);
        if (GUI.Button(box, "", skin.GetStyle("Inventory")))
        {
            craftindex = (craftindex + 1) % (Craftslist[(int)this.type - 1].Count);
        }
    }
    /// <summary>
    /// affiche les elements necessaires
    /// </summary>
    /// <param name="craft"></param>
    private void Craft_used_HUD()
    {
        Rect box = new Rect();
        int i = 0;
        bool tooltip = false;
        Item tooltipItem = new Item();
        foreach (ItemStack item in craftshow.Consume)
        {
            box = new Rect((3 + i) * Screen.height / 18, 2 * Screen.height / 9 + pos * Screen.height / 18, Screen.height / 18, Screen.height / 18);
            GUI.Box(box, item.Items.Icon, skin.GetStyle("Slot"));
            GUI.Box(box, item.Quantity.ToString(), skin.GetStyle("Quantity"));
            if (box.Contains(Event.current.mousePosition))
            {
                tooltip = true;
                tooltipItem = item.Items;
            }
            i += 1;

            box = new Rect((3 + i) * Screen.height / 18, 2 * Screen.height / 9 + pos * Screen.height / 18, Screen.height / 18, Screen.height / 18);
            if(GUI.Button(box, "", this.skin.GetStyle("Slot")))
            {
                if (inventory.InventoryContains(craftshow) && inventory.InventoryContains(new ItemStack()))
                {
                    inventory.DeleteItems(craftshow.Consume);
                    inventory.RpcAddItemStack(craftshow.Product.Items.ID, craftshow.Product.Quantity, null);
                }
            }
            if (tooltip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 35 + 20 * (tooltipItem.Description.Length / 35 + 1)),
                "<color=#ffffff>" + tooltipItem.Name + "</color>\n\n" + tooltipItem.Description, this.skin.GetStyle("tooltip"));
            }
        }
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
