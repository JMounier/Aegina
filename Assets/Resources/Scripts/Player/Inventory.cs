using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour
{
    private int rows = 4;
    private int columns = 6;
    private int pos_x_inventory, pos_y_inventory;
    private int pos_x_toolbar, pos_y_toolbar;
    private int[] previndex = new int[2];
    private bool draggingItemStack = false;
    private int cursor = 0;
    private bool inventoryShown = false;
    private bool tooltipshown = false;
    private GUISkin skin;
    private ItemStack selectedItem;
    private ItemStack[,] slots;


    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        // this.pos_x_inventory = (Screen.width - this.rows * 40 + 16) / 2;
        // this.pos_y_inventory = (Screen.height - this.columns * 40 + 31) / 2;
        this.pos_x_inventory = 8;
        this.pos_y_inventory = 8;
        this.pos_x_toolbar = (Screen.width - this.columns * 50) / 2;
        this.pos_y_toolbar = Screen.height - 50;

        this.slots = new ItemStack[this.rows, this.columns];
        this.ClearInventory();
        // this.LoadInventory();

        // Tests
        this.AddItemStack(new ItemStack(ItemDatabase.Stone, 42));
        this.AddItemStack(new ItemStack(ItemDatabase.Log, 64));
        this.AddItemStack(new ItemStack(ItemDatabase.Log, 64));
        this.AddItemStack(new ItemStack(ItemDatabase.CopperPickaxe, 1));
        this.AddItemStack(new ItemStack(ItemDatabase.Floatium, 7));
        this.AddItemStack(new ItemStack(ItemDatabase.IronIngot, 14));

        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/Skin");
    }

    // Methods
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;
        this.DrawToolbar();
        if (this.inventoryShown)
        {
            this.InteractInventory();
            this.DrawInventory();
        }
        else if (this.draggingItemStack)
        {
            if (this.slots[this.previndex[0], this.previndex[1]].Quantity == 0)
                this.slots[this.previndex[0], this.previndex[1]] = this.selectedItem;
            else
                this.slots[this.previndex[0], this.previndex[1]].Quantity += this.selectedItem.Quantity;
            this.draggingItemStack = false;
        }

    }

    /// <sumary>
    ///  S'occupe de toute les interractions entre la souris et l'inventaire, permet le drag and drop et dessinne la tooltip.
    /// </sumary>
    void InteractInventory()
    {
        this.tooltipshown = false;
        Rect rect;
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                if (i == this.rows - 1)
                    rect = new Rect(this.pos_x_inventory + j * 40, this.pos_y_inventory + i * 40 + 15, 40, 40);
                else
                    rect = new Rect(this.pos_x_inventory + j * 40, this.pos_y_inventory + i * 40, 40, 40);

                if (rect.Contains(Event.current.mousePosition))
                {
                    // Prise du stack
                    if (!this.draggingItemStack && Event.current.button == 0 && Event.current.type == EventType.MouseDown)
                    {
                        if (this.slots[i, j].Items.ID != -1)
                        {
                            this.draggingItemStack = true;
                            this.previndex[0] = i;
                            this.previndex[1] = j;

                            if (Event.current.shift)
                            {
                                this.selectedItem = new ItemStack(this.slots[i, j].Items, (this.slots[i, j].Quantity + 1) / 2);
                                if (this.slots[i, j].Quantity == 1)
                                {
                                    this.slots[i, j] = new ItemStack();
                                }
                                else
                                {
                                    this.slots[i, j].Quantity -= (this.slots[i, j].Quantity + 1) / 2;
                                }
                            }
                            else
                            {
                                this.selectedItem = this.slots[i, j];
                                this.slots[i, j] = new ItemStack();
                            }
                        }

                    }
                    // Description du stack
                    else if (!this.draggingItemStack && this.slots[i, j].Items.ID != -1)
                    {
                        this.previndex[0] = i;
                        this.previndex[1] = j;
                        this.selectedItem = this.slots[i, j];
                        this.tooltipshown = true;
                    }
                    // Relachement du stack dans un slot
                    else if (this.draggingItemStack && Event.current.button == 0 && Event.current.type == EventType.MouseUp)
                    {
                        this.draggingItemStack = false;
                        if (this.slots[i, j].Items.ID == this.selectedItem.Items.ID)
                        {
                            if (this.slots[i, j].Quantity == this.selectedItem.Items.Size)
                            {
                                this.selectedItem.Quantity += this.slots[this.previndex[0], this.previndex[1]].Quantity;
                                this.slots[this.previndex[0], this.previndex[1]] = this.slots[i, j];
                                this.slots[i, j] = this.selectedItem;
                            }
                            else if (this.slots[i, j].Quantity + this.selectedItem.Quantity > this.selectedItem.Items.Size)
                            {
                                int diff = this.slots[i, j].Quantity + this.selectedItem.Quantity - this.selectedItem.Items.Size;
                                this.slots[i, j].Quantity += this.selectedItem.Quantity;
                                this.selectedItem.Quantity = diff;
                                this.slots[this.previndex[0], this.previndex[1]] = this.selectedItem;
                            }
                            else
                                this.slots[i, j].Quantity += this.selectedItem.Quantity;
                        }
                        else if (this.slots[i, j].Items.ID != -1)
                        {
                            if (this.slots[this.previndex[0], this.previndex[1]].Items.ID == -1)
                            {
                                this.slots[this.previndex[0], this.previndex[1]] = this.slots[i, j];
                                this.slots[i, j] = this.selectedItem;
                            }
                            else
                            {
                                this.slots[this.previndex[0], this.previndex[1]].Quantity += this.selectedItem.Quantity;
                            }
                        }
                        else
                        {
                            this.slots[i, j] = this.selectedItem;
                        }
                    }
                    // Relachement d'un item dans un slot
                    else if (this.draggingItemStack && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
                    {
                        if (this.slots[i, j].Items.ID == this.selectedItem.Items.ID && this.slots[i, j].Quantity < this.slots[i, j].Items.Size)
                        {
                            this.slots[i, j].Quantity++;
                            this.selectedItem.Quantity--;
                        }
                        else if (this.slots[i, j].Items.ID == -1)
                        {
                            this.slots[i, j] = new ItemStack(selectedItem.Items, 1);
                            this.selectedItem.Quantity--;
                        }
                        if (this.selectedItem.Quantity == 0)
                            this.draggingItemStack = false;
                    }
                }
            }
        // Relachement de l'item hors de l'inventaire
        rect = new Rect(pos_x_inventory, pos_y_inventory, columns * 50, rows * 50);
        if (!rect.Contains(Event.current.mousePosition) && this.draggingItemStack && Event.current.button == 0 && Event.current.type == EventType.MouseUp)
        {
            this.Drop(this.selectedItem);
            this.draggingItemStack = false;
            this.SaveInventory();
        }
        // Relachement d'un item hors de l'inventaire
        if (!rect.Contains(Event.current.mousePosition) && this.draggingItemStack && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
        {
            this.Drop(new ItemStack(selectedItem.Items, 1));
            this.selectedItem.Quantity--;
            if (this.selectedItem.Quantity == 0)
                this.draggingItemStack = false;
            this.SaveInventory();
        }
    }

    /// <sumary>
    /// S'occupe de dessiner l'inventaire avec les items dedans.
    /// </sumary>
    void DrawInventory()
    {
        Rect rect = new Rect(this.pos_x_inventory - 8, this.pos_y_inventory - 8, this.columns * 40 + 16, this.rows * 40 + 31);
        GUI.Box(rect, "", this.skin.GetStyle("inventory"));
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                // Dessin du slot
                if (i == this.rows - 1)
                    rect = new Rect(this.pos_x_inventory + j * 40, this.pos_y_inventory + i * 40 + 15, 40, 40);
                else
                    rect = new Rect(this.pos_x_inventory + j * 40, this.pos_y_inventory + i * 40, 40, 40);
                GUI.Box(rect, "", this.skin.GetStyle("slot"));

                if (this.slots[i, j].Items.ID != -1)
                {
                    // Dessin de l'item + quantite
                    rect.x += 6;
                    rect.y += 6;
                    rect.width -= 12;
                    rect.height -= 12;
                    GUI.DrawTexture(rect, this.slots[i, j].Items.Icon);
                    if (this.slots[i, j].Quantity > 1)
                        GUI.Box(rect, this.slots[i, j].Quantity.ToString(), this.skin.GetStyle("quantity"));
                }
            }

        if (this.draggingItemStack)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 45, 45), this.selectedItem.Items.Icon);
            if (this.selectedItem.Quantity > 1)
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 45, 45), this.selectedItem.Quantity.ToString(), this.skin.GetStyle("quantity2"));
        }
        if (this.tooltipshown)
        {
            GUI.Box(new Rect(this.pos_x_inventory + (this.previndex[1] + 1) * 40, this.pos_y_inventory + this.previndex[0] * 40, 200, 35 + 20 * (this.selectedItem.Items.Description.Length / 35 + 1)),
                "<color=#ffffff>" + this.selectedItem.Items.Name + "</color>\n\n" + this.selectedItem.Items.Description, this.skin.GetStyle("tooltip"));
        }
    }

    /// <sumary>
    /// S'occupe de dessiner la toolbar en bas de l'ecran.
    /// </sumary>
    void DrawToolbar()
    {
        Rect rect = new Rect(this.pos_x_toolbar, this.pos_y_toolbar, this.columns * 50, 50);
        GUI.Box(rect, "", this.skin.GetStyle("toolbar"));
        for (int i = 0; i < this.columns; i++)
        {
            rect = new Rect(this.pos_x_toolbar + i * 50, this.pos_y_toolbar, 50, 50);
            if (this.cursor == i)
                GUI.Box(rect, "", this.skin.GetStyle("toolbar_selected"));

            if (this.slots[this.rows - 1, i].Items.ID != -1)
            {
                rect.x += 8;
                rect.y += 8;
                rect.width -= 16;
                rect.height -= 16;
                GUI.DrawTexture(rect, this.slots[this.rows - 1, i].Items.Icon);
                if (this.slots[this.rows - 1, i].Quantity != 1)
                    GUI.Box(rect, this.slots[this.rows - 1, i].Quantity.ToString(), this.skin.GetStyle("quantity"));
            }
        }
    }

    /// <sumary>
    /// Permet d'ajotuer des objes dans l'inventaire.
    /// </sumary>
    public void AddItemStack(ItemStack iStack)
    {
        int i = 0;
        int j = 0;
        int n = iStack.Quantity;
        while (n > 0 && i < this.rows)
        {
            if (j == this.columns)
            {
                i++;
                j = 0;
            }

            if (this.slots[i, j].Items.ID == -1)
            {
                this.slots[i, j] = iStack;
                n = 0;
            }
            else if (this.slots[i, j].Items.ID == iStack.Items.ID)
            {
                int mem = iStack.Items.Size - this.slots[i, j].Quantity;
                this.slots[i, j].Quantity += iStack.Quantity;
                n -= mem;
            }
            j++;
        }

        i = 0;
        j = 0;
        while (n > 0 && i < this.rows)
        {
            if (j == this.columns)
            {
                i++;
                j = 0;
            }

            if (this.slots[i, j].Items.ID == -1)
            {
                this.slots[i, j] = iStack;
                n = 0;
            }
            j++;
        }
    }

    /// <sumary>
    /// Verifie si un objet possedant l'id it se trouve dans l'inventaire.
    /// </sumary>
    public bool InventoryContains(Item it)
    {
        for (int i = 0; i < this.rows; i++)
        {
            for (int j = 0; j < this.columns; j++)
            {
                if (this.slots[i, j].Items.ID == it.ID && this.slots[i, j].Items.Meta == it.Meta)
                    return true;
            }
        }
        return false;
    }

    /// <sumary>
    /// Reinitialise l'inventaire.
    /// </sumary>
    public void ClearInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
                this.slots[i, j] = new ItemStack();
    }

    /// <sumary>
    /// Sauvegarde l'inventaire en local.
    /// </sumary>
    public void SaveInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
                PlayerPrefs.SetString("Inventory " + i + " " + j, this.slots[i, j].Items.ID + " " + this.slots[i, j].Items.Meta + " " + this.slots[i, j].Quantity);
    }

    /// <sumary>
    /// Recupere l'inventaire local.
    /// </sumary>
    public void LoadInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                string[] save = PlayerPrefs.GetString("Inventory " + i + " " + j, "-1 0 0").Split();
                this.slots[i, j] = new ItemStack(ItemDatabase.Find(System.Convert.ToInt32(save[0]), System.Convert.ToInt32(save[1])), System.Convert.ToInt32(save[2]));
            }
    }

    /// <sumary>
    /// Jette un stack d'objet.
    /// </sumary>
    [Client]
    public void Drop(ItemStack itemS)
    {
        CmdDrop(itemS.Quantity, itemS.Items.ID, itemS.Items.Meta, gameObject.transform.position, -gameObject.GetComponentInChildren<CharacterCollision>().gameObject.transform.forward);
    }

    /// <sumary>
    /// Commande pour jetter un stack d'objet.
    /// </sumary>
    [Command]
    private void CmdDrop(int quantity, int id, int meta, Vector3 pos, Vector3 forward)
    {
        ItemDatabase.Find(id, meta).Spawn(pos + forward * 0.3f + Vector3.up * 0.3f, forward + Vector3.up, quantity);
    }

    /// <sumary>
    /// Utilise un objet
    /// </sumary>
    public void UsingItem()
    {
        if (cursor == -1)
            return;
        if (this.UsedItem.Items is Consumable)
        {
            (UsedItem.Items as Consumable).Consume();
        }
        print("Item utiliser");
    }

    // Getters & Setters

    /// <sumary>
    /// Si l'inventaire est affiché.
    /// </sumary>
    public bool InventoryShown
    {
        get { return this.inventoryShown; }
        set { this.inventoryShown = value; }
    }

    /// <sumary>
    /// Si l'utilisateur déplace un objet.
    /// </sumary>
    public bool Draggingitem
    {
        get { return this.draggingItemStack; }
    }

    /// <sumary>
    /// La position de l'item utilisé.
    /// </sumary>
    public int Cursors
    {
        get { return this.cursor; }
        set { this.cursor = value % this.columns; }
    }

    /// <sumary>
    /// Renvoie l'item stack utilisé.
    /// </sumary>
    public ItemStack UsedItem
    {
        get { return this.slots[this.rows - 1, this.cursor]; }
    }
}
