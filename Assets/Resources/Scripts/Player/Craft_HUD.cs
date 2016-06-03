using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Craft_HUD : NetworkBehaviour
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
	private Dictionary<int,bool> craftMastered;
    private int Decal;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.inventory = gameObject.GetComponent<Inventory>();
        this.sound = GetComponent<Sound>();
        this.type = Craft.Type.None;
        this.skin = Resources.Load<GUISkin>("Sprites/GUISkin/skin");
        this.Craftslist = new List<Craft>[5];
        this.CraftElementary = new List<Craft>();
        this.CraftWorkTop = new List<Craft>();
        this.CraftConsumable = new List<Craft>();
        this.CraftTools = new List<Craft>();
        this.CraftArmor = new List<Craft>();
		this.craftMastered = new Dictionary<int, bool> ();
        this.character = GetComponentInChildren<CharacterCollision>().gameObject;
        this.Craftslist[0] = CraftElementary;
        this.Craftslist[1] = CraftWorkTop;
        this.Craftslist[2] = CraftConsumable;
        this.Craftslist[3] = CraftTools;
        this.Craftslist[4] = CraftArmor;
        foreach (Craft craft in CraftDatabase.Crafts)
        {
            if (!craft.Secret)
            {
                this.Craftslist[(int)(craft.What) - 1].Add(craft);
            }
			if (craft.ID != -1) 
				this.craftMastered.Add (craft.ID, false);
        }

        this.craftindex = 0;
        this.showcraft = false;
        this.pos = -1;
        this.craftshow = new Craft(Craft.Type.None);
        this.nearwork = new bool[4];
        for (int j = 0; j < 4; j++)
            nearwork[j] = false;
		
        List<int> mastered = new List<int>();
		mastered.AddRange(new int[] { 0, 20, 28, 70});
        if (SuccessDatabase.StoneAge.Achived)
        {
			mastered.AddRange(new int[] { 1, 2, 40, 50, 60, 71, 72, 73}); //id des craft de l'armure en cuir à ajouter
            if (SuccessDatabase.CopperAge.Achived)
            {
				mastered.AddRange(new int[] { 4, 5, 10, 16, 41, 51, 61}); //id du craft de l'armure en cuivre
                if (SuccessDatabase.IronAge.Achived)
                {
					mastered.AddRange(new int[] { 3, 6, 10, 22, 23, 24, 25, 26, 27, 42, 52, 62}); //id des crafts de l'armure en fer à ajouter
                    if (SuccessDatabase.GoldAge.Achived)
                    {
						mastered.AddRange(new int[] { 12, 21, 43, 53, 63}); //id des crafts des pièges à ajouter et de l'armure en or
                        if (SuccessDatabase.MithrilAge.Achived)
                        {
							mastered.AddRange(new int[] { 13, 44, 54, 64 }); //id des crafts des murailles et de l'armure en mitril à ajouter
                            if (SuccessDatabase.FloatiumAge.Achived)
                            {
								mastered.AddRange(new int[] { 14, 45, 55, 65});//id des crafts de l'armure en floatium à ajouter
                                if (SuccessDatabase.SunkiumAge.Achived)
									mastered.AddRange(new int[] { 15, 46, 56, 66}); //id des crafts de l'armure en sunkium à ajouter
                            }
                            }
                        }
                    }
                }
            }
        
        foreach (int ids in mastered)
            this.craftMastered[ids] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        this.skin.GetStyle("Quantity").fontSize = (int)(0.01f * Screen.width);
        Decal = Screen.height / 100;
        what_is_near();

        if (type != Craft.Type.None)
        {
            int mouseScrollDelta = (int)Input.mouseScrollDelta.y;
            this.craftindex = (this.craftindex - mouseScrollDelta) % (Craftslist[(int)this.type - 1].Count);
            while (craftindex < 0)
                craftindex += (Craftslist[(int)this.type - 1].Count);
            if (mouseScrollDelta != 0)
            {
                showcraft = false;
                this.pos = -1;
            }
        }
    }

    private void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        tooltip = false;
        if (inventory.InventoryShown)
        {
            GUI.Box(new Rect(0, 2 * Screen.height / 9 - Screen.height / 20, 1 * Screen.height / 7, 5 * Screen.height / 9 + Screen.height / 10), "", skin.GetStyle("craftframe_close"));
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
                sound.PlaySound(AudioClips.Button, 1f);
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
            if (GUI.Button(box, "", skin.GetStyle("Inventory")))
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
                sound.PlaySound(AudioClips.Button, 1f);
            }
            box.x += Screen.height / 100;
            box.y += Screen.height / 100;
            box.height -= Screen.height / 50;
            box.width -= Screen.height / 50;
            GUI.DrawTexture(box, Craftslist[(int)this.type - 1][(craftindex + i) % (Craftslist[(int)this.type - 1].Count)].Product.Items.Icon);
            box.x -= Screen.height / 200;
            box.y -= Screen.height / 200;
            box.height += Screen.height / 100;
            box.width += Screen.height / 100;
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
            sound.PlaySound(AudioClips.Button, 1f);
            showcraft = false;
            this.pos = -1;
        }
        box = new Rect(Screen.height / 9, 7 * Screen.height / 9, Screen.height / 18, Screen.height / 18);
        if (GUI.Button(box, "", skin.GetStyle("down_arrow")))
        {
            craftindex = (craftindex + 1) % (Craftslist[(int)this.type - 1].Count);
            sound.PlaySound(AudioClips.Button, 1f);
            showcraft = false;
            this.pos = -1;
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

            if (GUI.Button(box, "", skin.GetStyle("Slot")))
            {
                Craft Craft = null;
                foreach (Craft craft in CraftDatabase.Crafts)
                {
                    if (craft.Product.Items.ID == item.Items.ID)
                    {
                        Craft = craft;
                        break;
                    }
                }
                if (Craft != null)
                {
                    int index = 0;
                    pos = 0;
                    this.type = Craft.What;
                    foreach (Craft recette in this.Craftslist[((int)type) - 1])
                    {
                        if (recette.ID == Craft.ID)
                        {
                            this.showcraft = true;
                            craftindex = index;
                            craftshow = recette;
                        }
                        index += 1;
                    }
                }
            }
            Rect littlebox = new Rect((3 + i) * Screen.height / 18 + Decal, 2 * Screen.height / 9 + pos * Screen.height / 18 + Decal, Screen.height / 18 - 2 * Decal, Screen.height / 18 - 2 * Decal);
            GUI.DrawTexture(littlebox, item.Items.Icon);
            littlebox = new Rect((3 + i) * Screen.height / 18 + Decal / 2, 2 * Screen.height / 9 + pos * Screen.height / 18 + Decal / 2, Screen.height / 18 - Decal, Screen.height / 18 - Decal);
            GUI.Box(littlebox, inventory.InventoryContains(item.Items, item.Quantity * cost) ? (item.Quantity * cost).ToString() : "<color=#ff0000>" + (item.Quantity * cost).ToString() + "</color>", skin.GetStyle("Quantity"));
            if (box.Contains(Event.current.mousePosition))
            {
                tooltip = true;
                tooltipItem = item.Items;
            }
            i += 1;
        }
        box = new Rect((3 + i) * Screen.height / 18, 2 * Screen.height / 9 + pos * Screen.height / 18, Screen.height / 18, Screen.height / 18);

        bool RecipeComplete = inventory.InventoryContains(craftshow, cost == 1);
        bool WorkTopNear = (!craftshow.Fire || nearwork[0]) && (!craftshow.Workbench || nearwork[1]) && (!craftshow.Forge || nearwork[2]) && (!craftshow.Brewer || nearwork[3]);

        if (WorkTopNear && RecipeComplete)
        {
            if (GUI.Button(box, "", this.skin.GetStyle("Slot")))
            {
                CmdMakeCraft(craftshow.ID);
                inventory.DeleteItems(craftshow.Consume, cost == 1);
                ItemStack its = new ItemStack(craftshow.Product.Items, craftshow.Product.Quantity);
                inventory.AddItemStack(its);
                if (its.Quantity != 0)
                    inventory.Drop(its);
                if (craftshow.Workbench)
                    sound.PlaySound(AudioClips.workbensh, 1f);
                else if (craftshow.Forge)
                    sound.PlaySound(AudioClips.forge, 1f);
                else if (craftshow.Brewer)
                    sound.PlaySound(AudioClips.cooking, 1f);
                else
                    sound.PlaySound(AudioClips.Button, 1f);
            }
            box.x += Decal;
            box.y += Decal;
            box.width -= 2 * Decal;
            box.height -= 2 * Decal;
            GUI.DrawTexture(box, Resources.Load<Texture2D>("Sprites/CraftsIcon/Valid"));
        }
        else
        {
            GUI.Box(box, "", this.skin.GetStyle("Slot"));
            box.x += Decal;
            box.y += Decal;
            box.width -= 2 * Decal;
            box.height -= 2 * Decal;
            GUI.DrawTexture(box, Resources.Load<Texture2D>("Sprites/CraftsIcon/Invalid"));
        }

    }
    /// <summary>
    /// Recherche les atelier proche
    /// </summary>
    /// <returns></returns>
    private void what_is_near()
    {
        Collider[] NearObjects = Physics.OverlapSphere(this.character.transform.position, 3.5f);
        foreach (Collider item in NearObjects)
        {
            if (item.name == "Firepit")
                this.nearwork[0] = true;
            else if (item.name == "Workbench")
                this.nearwork[1] = true;
            else if (item.name == "Hoven")
                this.nearwork[2] = true;
            else if (item.name == "Cauldron")
                this.nearwork[3] = true;
        }
    }

    public void mastered(params int[] newmastered)
    {
		if (isLocalPlayer)
			foreach (int ids in newmastered) {
				try{
					craftMastered [ids] = true;
				}
				catch{
					Debug.Log (ids);
				}
			}
    }

    [Command]
    private void CmdMakeCraft(int id)
    {
        Stats.AddCrafted(id);
    }
}
