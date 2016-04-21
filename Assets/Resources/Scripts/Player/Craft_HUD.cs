using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Craft_HUD : MonoBehaviour
{
    private Inventory inventory;
	private GameObject character;
	private Sound sound;
    private List<Craft> CraftElementary, CraftWorkTop, CraftConsumable, CraftTools, CraftArmor;
    private List<Craft>[] Craftslist;
    private Craft.Type type;
    private bool[] nearwork;
    private GUISkin skin;
    private int craftindex;
    private bool showcraft;
    private int pos;
    private Craft craftshow;
    private bool tooltip = false;
    private Item tooltipItem;
    private bool[] craftMastered;
    // Use this for initialization
    void Start()
    {
        this.inventory = gameObject.GetComponent<Inventory>();
		this.sound = GetComponent<Sound> ();
        this.type = Craft.Type.None;
        this.skin = Resources.Load<GUISkin>("Sprites/GUISkin/skin");
        this.Craftslist = new List<Craft>[5];
        this.CraftElementary = new List<Craft>();
        this.CraftWorkTop = new List<Craft>();
        this.CraftConsumable = new List<Craft>();
        this.CraftTools = new List<Craft>();
        this.CraftArmor = new List<Craft>();
		this.character = GetComponentInChildren<CharacterCollision>().gameObject;
        this.Craftslist[0] = CraftElementary;
        this.Craftslist[1] = CraftWorkTop;
        this.Craftslist[2] = CraftConsumable;
        this.Craftslist[3] = CraftTools;
        this.Craftslist[4] = CraftArmor;
        int i = 0;
        foreach (Craft craft in CraftDatabase.Crafts)
        {
            if (!craft.Secret)
            {
                this.Craftslist[(int)(craft.What) - 1].Add(craft);
            }
            i++;
        }
		this.craftindex = 0;
		this.showcraft = false;
        this.pos = -1;
        this.craftshow = new Craft(Craft.Type.None);
        this.craftMastered = new bool[i+1];
		this.nearwork = new bool[4];
		for (int j = 0; j < 4; j++) {
			nearwork [j] = false;
		}
		for (int j = 0; j < this.craftMastered.Length; j++)
        {
			this.craftMastered[j] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        what_is_near();
        if (type != Craft.Type.None)
        {
            this.craftindex = (this.craftindex - (int)Input.mouseScrollDelta.y) % (Craftslist[(int)this.type - 1].Count);
            while (craftindex < 0)
            {
                craftindex += (Craftslist[(int)this.type - 1].Count);
            }
        }
    }
    private void OnGUI()
    {
        tooltip = false;
        if (inventory.InventoryShown)
        {
            GUI.Box(new Rect(0, 2 * Screen.height / 9 - Screen.height / 20, 1 * Screen.height / 7, 5 * Screen.height / 9 + Screen.height / 10), "", this.type != Craft.Type.None ? skin.GetStyle("craftframe_open") : skin.GetStyle("craftframe_close"));
            Categoryze();
            if (showcraft)
                Craft_used_HUD();
        }
        if (tooltip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 35 + 20 * (tooltipItem.Description.Length / 35 + 1)),
                "<color=#ffffff>" + tooltipItem.Name + "</color>\n\n" + tooltipItem.Description, this.skin.GetStyle("tooltip"));
        }
    }
    /// <summary>
    /// Affiche l'interface de craft
    /// </summary>
    private void Categoryze()
    {
        for (int i = 0; i < 5; i++)
        {
            Rect box = new Rect(0, (i + 2) * Screen.height / 9, Screen.height / 9, Screen.height / 9);
            if (GUI.Button(box, "", skin.GetStyle("slot")))
            {
                craftindex = 0;
                if (this.type == (Craft.Type)(i + 1))
                {
                    this.type = Craft.Type.None;
                }
                else
                {
                    this.type = (Craft.Type)(i + 1);
                }
                this.showcraft = false;
                this.pos = -1;
                this.craftindex = 0;
				sound.PlaySound (AudioClips.Button,1f);
            }
            box = new Rect(10, (i + 2) * Screen.height / 9 + 10, Screen.height / 9 - 20, Screen.height / 9 - 20);
            GUI.DrawTexture(box, Resources.Load<Texture2D>("Sprites/CraftsIcon/Craft" + (i + 1)));
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
        for (int i = 0; i < 10; i++)
        {
            box = new Rect(Screen.height / 9, 2 * Screen.height / 9 + i * Screen.height / 18, Screen.height / 18, Screen.height / 18);
            if (GUI.Button(box, Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Items.Icon, skin.GetStyle("Inventory")))
            {
                if (this.pos == i)
                {
                    showcraft = false;
                    this.pos = -1;
                }
                else
                {
                    showcraft = this.pos != i;
                    this.pos = i;
                }
                this.craftshow = Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)];
				sound.PlaySound (AudioClips.Button,1f);
            }
            if (Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Quantity > 1)
            {
                GUI.Box(box, Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Quantity.ToString(), skin.GetStyle("Quantity"));
            }
            if (box.Contains(Event.current.mousePosition))
            {
                tooltip = true;
                tooltipItem = Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Items;
            }

        }
        box = new Rect(Screen.height / 9, 2 * Screen.height / 9 - Screen.height / 18, Screen.height / 18, Screen.height / 18);
        if (GUI.Button(box, "", skin.GetStyle("up_arrow")))
        {
            craftindex = (craftindex - 1) % (Craftslist[(int)this.type - 1].Count);
			sound.PlaySound (AudioClips.Button,1f);
        }
        box = new Rect(Screen.height / 9, 7 * Screen.height / 9, Screen.height / 18, Screen.height / 18);
        if (GUI.Button(box, "", skin.GetStyle("down_arrow")))
        {
            craftindex = (craftindex + 1) % (Craftslist[(int)this.type - 1].Count);
			sound.PlaySound (AudioClips.Button,1f);
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
        int cost = 1;
        if (!craftMastered[craftshow.ID])
        {
            cost++;
        }
        foreach (ItemStack item in craftshow.Consume)
        {
            box = new Rect((3 + i) * Screen.height / 18, 2 * Screen.height / 9 + pos * Screen.height / 18, Screen.height / 18, Screen.height / 18);
            GUI.Box(box,"", skin.GetStyle("Slot"));
            
            Rect littlebox = new Rect((3 + i) * Screen.height / 18 + 5, 2 * Screen.height / 9 + pos * Screen.height / 18+5, Screen.height / 18 - 10, Screen.height / 18-10);
            GUI.DrawTexture(littlebox, item.Items.Icon);
            GUI.Box(box, inventory.InventoryContains(item.Items,item.Quantity * cost) ? (item.Quantity * cost).ToString() : "<color=#ff0000>" + (item.Quantity* cost).ToString() + "</color>", skin.GetStyle("Quantity"));
            if (box.Contains(Event.current.mousePosition))
            {
                tooltip = true;
                tooltipItem = item.Items;
            }
            i += 1;
        }
        box = new Rect((3 + i) * Screen.height / 18, 2 * Screen.height / 9 + pos * Screen.height / 18, Screen.height / 18, Screen.height / 18);
        bool RecipeComplete = inventory.InventoryContains(craftshow,cost == 1);
		bool WorkTopNear = (!craftshow.Fire || nearwork[0])&&(!craftshow.Workbench || nearwork[1])&&(!craftshow.Forge || nearwork[2])&&(!craftshow.Brewer || nearwork[3]);
        if (WorkTopNear && RecipeComplete)
        {
            if (GUI.Button(box, Resources.Load<Texture2D>("Sprites/CraftsIcon/Valid"), this.skin.GetStyle("Slot")))
            {
                inventory.DeleteItems(craftshow.Consume,cost == 1);
                ItemStack its = new ItemStack(craftshow.Product.Items, craftshow.Product.Quantity);
                if (Random.Range(0,100) < 5)
                {
                    craftMastered[craftshow.ID] = true;
                }
                inventory.AddItemStack(its);
                if (its.Quantity != 0)
                    inventory.Drop(its);
				if (craftshow.Workbench)
					sound.PlaySound (AudioClips.workbensh, 1f);
				else if (craftshow.Forge)
					sound.PlaySound (AudioClips.forge, 1f);
				else if (craftshow.Brewer)
					sound.PlaySound (AudioClips.cooking,1f);
				else
					sound.PlaySound (AudioClips.Button,1f);
            }

        }
        else
        {
            GUI.Box(box, Resources.Load<Texture2D>("Sprites/CraftsIcon/Invalid"), this.skin.GetStyle("Slot"));
        }

    }
    /// <summary>
    /// recherche les atelier proche
    /// </summary>
    /// <returns></returns>
    private void what_is_near()
    {
		Collider[] NearObjects = Physics.OverlapSphere(this.character.transform.position,3.5f);
		foreach (Collider item in NearObjects) {
			if (item.name == "Firepit")
				nearwork [0] = true;
			else if (item.name == "Workbench")
				nearwork [1] = true;
			else if (item.name == "Hoven")
				nearwork [2] = true;
			else if (item.name == "Cauldron")
				nearwork [3] = true;
		}
    }
}
