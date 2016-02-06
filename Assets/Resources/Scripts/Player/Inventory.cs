﻿using UnityEngine;
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
    private Transform trans;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        this.pos_x_inventory = (Screen.width - this.rows * 40 + 16) / 2;
        this.pos_y_inventory = (Screen.height - this.columns * 40 + 31) / 2;
        this.pos_x_toolbar = (Screen.width - this.columns * 50) / 2;
        this.pos_y_toolbar = Screen.height - 50;

        this.slots = new ItemStack[this.rows, this.columns];
        this.ClearInventory();
        // this.LoadInventory();
        // Tests
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.Stone), 42));
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.Log), 64));
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.Log), 64));
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.Log), 64));
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.CopperPickaxe), 1));
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.Floatium), 7));
        this.AddItemStack(new ItemStack(new Item(ItemDatabase.IronIngot), 14));    

        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/Skin");
        this.trans = gameObject.GetComponent<Transform>();
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

    /// <summary>
    /// S'occupe de toute les interractions entre la souris et l'inventaire, permet le drag and drop et dessinne la tooltip.
    /// </summary>
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
                            else if (this.slots[this.previndex[0], this.previndex[1]].Items.ID == this.selectedItem.Items.ID)
                            {
                                this.slots[this.previndex[0], this.previndex[1]].Quantity += this.selectedItem.Quantity;
                            }
                            else
                            {
                                this.Drop(this.selectedItem);
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
        rect = new Rect(pos_x_inventory, pos_y_inventory, columns * 40, rows * 40);
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

    /// <summary>
    /// S'occupe de dessiner l'inventaire avec les items dedans.
    /// </summary>
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

    /// <summary>
    /// S'occupe de dessiner la toolbar en bas de l'ecran.
    /// </summary>
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

    /// <summary>
    /// Permet d'ajotuer des objes dans l'inventaire.
    /// </summary>
    /// <param name="iStack"></param>
    public void AddItemStack(ItemStack iStack)
    {
        int i = 0;
        int j = 0;
        while (iStack.Quantity > 0 && i < this.rows)
        {
            if (j == this.columns)
            {
                i++;
                j = -1;
            }

            else if (this.slots[i, j].Items.ID == iStack.Items.ID)
            {
                int mem = iStack.Items.Size - this.slots[i, j].Quantity;
                this.slots[i, j].Quantity += iStack.Quantity;
                iStack.Quantity -= mem;
            }
            j++;
        }

        i = 0;
        j = 0;
        while (iStack.Quantity > 0 && i < this.rows)
        {
            if (j == this.columns)
            {
                i++;
                j = -1;
            }

            else if (this.slots[i, j].Items.ID == -1)
            {
                this.slots[i, j] = new ItemStack(iStack.Items, iStack.Quantity);
                iStack.Quantity = 0;
            }
            j++;
        }
    }

    /// <summary>
    /// Verifie si une quantite d'objet se trouve dans l'inventaire.
    /// </summary>
    /// <param name="it"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Verifie si un objet se trouve dans l'inventaire.
    /// </summary>
    /// <param name="it"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public bool InventoryContains(Item it, int quantity)
    {
        int i = 0;
        int j = 0;
        while (quantity > 0 && j < this.columns)
        {
            if (this.slots[i, j].Items.ID == it.ID && this.slots[i, j].Items.Meta == it.Meta)
                quantity -= this.slots[i, j].Quantity;
            i = (i + 1) % this.rows;
            if (i == 0)
                j += 1;
        }
        return quantity <= 0;
    }

    /// <summary>
    /// Verifie si un objet se trouve en au moins une certaine quantite dans l'inventaire.
    /// </summary>
    /// <param name="itS"></param>
    /// <returns></returns>
    public bool InventoryContains(ItemStack itS)
    {
        return InventoryContains(itS.Items, itS.Quantity);
    }

    /// <summary>
    /// Verifie si une liste d'objets se trouve dans l'inventaire.
    /// </summary>
    /// <param name="itlist"></param>
    /// <returns></returns>
    public bool InventoryContains(ItemStack[] itlist)
    {
        bool contain_all = true;
        int i = 0;
        int len = itlist.Length;
        while(contain_all && i < len)
        {
            contain_all = contain_all && InventoryContains(itlist[i]);
            i += 1;
        }
        return contain_all;
    }

    /// <summary>
    /// suprime une certaine quantite d'un objet dans l'inventaire
    /// </summary>
    /// <param name="itS"></param>
    public void DeleteItems(ItemStack itS)
    {
        DeleteItems(itS.Items, itS.Quantity);
    }

    /// <summary>
    /// suprime une certaine quantite d'un objet dans l'inventaire
    /// </summary>
    /// <param name="it"></param>
    /// <param name="quantity"></param>
    public void DeleteItems(Item it, int quantity)
    {
        int i = 0;
        int j = 0;
        while (quantity > 0 && j < this.columns)
        {
            if (this.slots[i, j].Items.ID == it.ID && this.slots[i, j].Items.Meta == it.Meta)
            {
                if (this.slots[i,j].Quantity <= quantity)
                {
                    quantity -= this.slots[i, j].Quantity;
                    this.slots[i, j] = new ItemStack();
                }
                else
                {
                    this.slots[i, j].Quantity -= quantity;
                    quantity = 0;
                }

            }
            i = (i + 1) % this.rows;
            if (i == 0)
                j += 1;
        }
    }

    /// <summary>
    /// suprime une liste d'objet de de l'inventaire
    /// </summary>
    /// <param name="itSlist"></param>
    public void DeleteItems(ItemStack[] itSlist)
    {
        foreach (ItemStack item in itSlist)
        {
            DeleteItems(item);
        }
    }

    /// <summary>
    /// Reinitialise l'inventaire.
    /// </summary>
    public void ClearInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
                this.slots[i, j] = new ItemStack();
    }

    /// <summary>
    /// Sauvegarde l'inventaire en local.
    /// </summary>
    public void SaveInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
                PlayerPrefs.SetString("Inventory " + i + " " + j, this.slots[i, j].Items.ID + " " + this.slots[i, j].Items.Meta + " " + this.slots[i, j].Quantity);
    }

    /// <summary>
    /// Recupere l'inventaire local.
    /// </summary>
    public void LoadInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                string[] save = PlayerPrefs.GetString("Inventory " + i + " " + j, "-1 0 0").Split();
                this.slots[i, j] = new ItemStack(ItemDatabase.Find(System.Convert.ToInt32(save[0]), System.Convert.ToInt32(save[1])), System.Convert.ToInt32(save[2]));
            }
    }

    /// <summary>
    /// Informe l'inventaire de la colision avec un loot.
    /// </summary>
    /// <param name="loot"></param>
    [Client]
    public void DetectLoot(GameObject loot)
    {
        if (isLocalPlayer)
            CmdGetItemStack(loot);
    }

    /// <summary>
    /// Recupere les informations du loot puis les ajoutes.
    /// </summary>
    /// <param name="loot"></param>
    [Command]
    private void CmdGetItemStack(GameObject loot)
    {
        if (loot != null)
        {
            Loot l = loot.GetComponent<Loot>();
            if (l.Items.Items.Ent.LifeMax - l.Items.Items.Ent.Life > 0.8f && l.Items.Quantity > 0)
            {
                int quantity = l.Items.Quantity;
                l.Items.Quantity = 0;
                loot.GetComponent<SphereCollider>().enabled = false;
                RpcAddItemStack(l.Items.Items.ID, quantity, loot);
            }
        }
    }

    /// <summary>
    /// Depuis les informations du loot, ajoute a l'inventaire les loots. Puis actualise la quantite. (Must be server)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <param name="loot"></param>
    [ClientRpc]
    private void RpcAddItemStack(int id, int quantity, GameObject loot)
    {
        if (isLocalPlayer)
        {
            ItemStack itemS = new ItemStack(new Item(ItemDatabase.Find(id)), quantity);
            AddItemStack(itemS);
            CmdSetQuantity(itemS.Quantity, loot, gameObject.GetComponentInChildren<CharacterCollision>().transform.position);
        }
    }

    /// <summary>
    /// Actualise la quantite restante du loot apres ajout.
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="loot"></param>
    /// <param name="posPlayer"></param>
    [Command]
    private void CmdSetQuantity(int quantity, GameObject loot, Vector3 posPlayer)
    {
        if (loot != null)
        {
            loot.GetComponent<Loot>().Items.Quantity = quantity;

            if (quantity == 0)
            {
                loot.GetComponent<Loot>().Items.Items.Ent.Life = 0.18f;
                loot.GetComponent<Loot>().Items.Items.Ent.Prefab.GetComponent<Rigidbody>().AddForce((posPlayer - loot.GetComponent<Loot>().Items.Items.Ent.Prefab.transform.position + Vector3.up * 1f) * 300);
            }
            else
                loot.GetComponent<SphereCollider>().enabled = true;
        }

    }

    /// <summary>
    /// Jette un stack d'objet.
    /// </summary>
    /// <param name="itemS"></param>
    [Client]
    public void Drop(ItemStack itemS)
    {
        CmdDrop(itemS.Quantity, itemS.Items.ID, itemS.Items.Meta, this.trans.GetComponentInChildren<CharacterCollision>().gameObject.transform.position, -this.trans.GetComponentInChildren<CharacterCollision>().gameObject.transform.forward);
    }

    /// <summary>
    /// Commande pour jetter un stack d'objet.
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="id"></param>
    /// <param name="meta"></param>
    /// <param name="pos"></param>
    /// <param name="forward"></param>
    [Command]
    private void CmdDrop(int quantity, int id, int meta, Vector3 pos, Vector3 forward)
    {
        ItemDatabase.Find(id, meta).Spawn(pos + forward * 0.3f + Vector3.up * 0.7f, forward + Vector3.up, quantity);
    }

    /// <summary>
    /// Utilise un objet
    /// </summary>
    public void UsingItem()
    {       
        if (this.UsedItem.Items is Consumable)
        {
            (UsedItem.Items as Consumable).Consume();
        }
        print("Item utiliser");
    }

    // Getters & Setters

    /// <summary>
    /// Si l'inventaire est affiché.
    /// </summary>
    public bool InventoryShown
    {
        get { return this.inventoryShown; }
        set { this.inventoryShown = value; }
    }

    /// <summary>
    /// Si l'utilisateur déplace un objet.
    /// </summary>
    public bool Draggingitem
    {
        get { return this.draggingItemStack; }
    }

    /// <summary>
    /// La position de l'item utilisé.
    /// </summary>
    public int Cursors
    {
        get { return this.cursor; }
        set { this.cursor = value % this.columns; }
    }

    /// <summary>
    /// Renvoie l'item stack utilisé.
    /// </summary>
    public ItemStack UsedItem
    {
        get { return this.slots[this.rows - 1, this.cursor]; }
        set { this.slots[this.rows - 1, this.cursor] = value; }
    }
}
