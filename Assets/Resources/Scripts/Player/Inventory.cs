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
    private int size_inventory, size_toolbar;
    private int[] previndex = new int[2];
    private bool draggingItemStack = false;
    private int cursor = 0;
    private bool inventoryShown = false;
    private bool tooltipshown = false;
    private GUISkin skin;
    private ItemStack selectedItem;
    /// <summary>
    /// Utilisation : slots[rows, columns]
    /// </summary>
    private ItemStack[,] slots;
    private Transform trans;
    private Sound sound;
    private Item lastUseddItem;

    #region Behaviour Methods
    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.size_inventory = Screen.width / 22;
        this.size_toolbar = (Screen.width / 20);
        this.pos_x_inventory = (Screen.width - this.columns * this.size_inventory) / 2;
        this.pos_y_inventory = (int)((Screen.height - (this.rows + 0.5f) * this.size_inventory) / 2);
        this.pos_x_toolbar = (Screen.width - this.columns * this.size_toolbar) / 2;
        this.pos_y_toolbar = Screen.height - this.size_toolbar - 5;
        this.slots = new ItemStack[this.rows, this.columns];
        this.ClearInventory();

        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/Skin");
        this.trans = gameObject.GetComponent<Transform>();
        this.sound = gameObject.GetComponent<Sound>();
        this.lastUseddItem = new Item();

        this.CmdLoadInventory();
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;
        this.size_inventory = Screen.width / 22;
        this.size_toolbar = Screen.width / 20;
        this.pos_x_inventory = (Screen.width - this.columns * this.size_inventory) / 2;
        this.pos_y_inventory = (int)((Screen.height - (this.rows + 0.5f) * this.size_inventory) / 2);
        this.pos_x_toolbar = (Screen.width - this.columns * this.size_toolbar) / 2;
        this.pos_y_toolbar = Screen.height - this.size_toolbar;

        // Mise de l'outil dans la main du joueur
        if (this.lastUseddItem != this.UsedItem.Items)
        {
            if (this.lastUseddItem is Tool || this.lastUseddItem is Consumable)
                this.CmdRemoveTool();

            if (this.UsedItem.Items is Tool || this.UsedItem.Items is Consumable)
            {
                this.CmdSetTool(this.UsedItem.Items.ID);
            }

            if (this.lastUseddItem is WorkTop)
            {
                GameObject.Destroy((this.lastUseddItem as WorkTop).Previsu);
                this.lastUseddItem = ItemDatabase.Find(this.lastUseddItem.ID);
            }

            if (this.UsedItem.Items is WorkTop)
            {
                GameObject charact = gameObject.transform.FindChild("Character").gameObject;
                this.UsedItem.Items = ItemDatabase.Find(this.UsedItem.Items.ID);
                (this.UsedItem.Items as WorkTop).Previsu = GameObject.Instantiate((this.UsedItem.Items as WorkTop).Previsu);
                // set pos rot and parent
                (this.UsedItem.Items as WorkTop).Previsu.transform.position = (charact.transform.position - charact.transform.forward);
                (this.UsedItem.Items as WorkTop).Previsu.transform.parent = WorkTop.GetHierarchy(charact.transform.position);
                (this.UsedItem.Items as WorkTop).Previsu.transform.LookAt(new Vector3(charact.transform.position.x, (this.UsedItem.Items as WorkTop).Previsu.transform.position.y, charact.transform.position.z));
            }

            this.lastUseddItem = this.UsedItem.Items;
        }
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

    public override void OnStartClient()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Inventory>().CmdRemoveTool();
            player.GetComponent<Inventory>().lastUseddItem = new Item();
        }
    }
    #endregion

    #region Interaction and Draw
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
                    rect = new Rect(this.pos_x_inventory + j * this.size_inventory, this.pos_y_inventory + this.size_inventory * (i + .5f), this.size_inventory, this.size_inventory);
                else
                    rect = new Rect(this.pos_x_inventory + j * this.size_inventory, this.pos_y_inventory + i * this.size_inventory, this.size_inventory, this.size_inventory);

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
        rect = new Rect(pos_x_inventory, pos_y_inventory, columns * this.size_inventory, (rows + .5f) * this.size_inventory);
        Rect secondrect = new Rect(this.pos_x_toolbar, this.pos_y_toolbar, this.columns * this.size_toolbar, this.size_toolbar);
        if (!rect.Contains(Event.current.mousePosition) && !secondrect.Contains(Event.current.mousePosition) && this.draggingItemStack && Event.current.button == 0 && Event.current.type == EventType.MouseUp)
        {
            this.Drop(this.selectedItem);
            this.draggingItemStack = false;
        }
        // Relachement d'un item hors de l'inventaire
        if (!rect.Contains(Event.current.mousePosition) && !secondrect.Contains(Event.current.mousePosition) && this.draggingItemStack && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
        {
            this.Drop(new ItemStack(selectedItem.Items, 1));
            this.selectedItem.Quantity--;
            if (this.selectedItem.Quantity == 0)
                this.draggingItemStack = false;
        }
    }

    /// <summary>
    /// S'occupe de dessiner l'inventaire avec les items dedans.
    /// </summary>
    void DrawInventory()
    {
        Rect rect = new Rect(this.pos_x_inventory - 24, this.pos_y_inventory - 32, this.columns * this.size_inventory + 48, (this.rows + .5f) * this.size_inventory + 64);
        GUI.Box(rect, "", this.skin.GetStyle("inventory"));
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                // Dessin du slot
                if (i == this.rows - 1)
                    rect = new Rect(this.pos_x_inventory + j * this.size_inventory, this.pos_y_inventory + (i + .5f) * this.size_inventory, this.size_inventory, this.size_inventory);
                else
                    rect = new Rect(this.pos_x_inventory + j * this.size_inventory, this.pos_y_inventory + i * this.size_inventory, this.size_inventory, this.size_inventory);
                GUI.Box(rect, "", this.skin.GetStyle("slot"));

                if (this.slots[i, j].Items.ID != -1)
                {
                    // Dessin de l'item + quantite
                    rect.x += this.size_inventory / 5;
                    rect.y += this.size_inventory / 5;
                    rect.width -= this.size_inventory / 2.5f;
                    rect.height -= this.size_inventory / 2.5f;
                    GUI.DrawTexture(rect, this.slots[i, j].Items.Icon);
                    rect.x -= this.size_inventory / 10;
                    rect.y -= this.size_inventory / 10;
                    rect.width += this.size_inventory / 5;
                    rect.height += this.size_inventory / 5;
                    if (this.slots[i, j].Quantity > 1)
                        GUI.Box(rect, this.slots[i, j].Quantity.ToString(), this.skin.GetStyle("quantity"));
                }
            }

        if (this.draggingItemStack)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, this.size_inventory * 1.125f, this.size_inventory * 1.125f), this.selectedItem.Items.Icon);
            if (this.selectedItem.Quantity > 1)
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, this.size_inventory * 1.125f, this.size_inventory * 1.125f), this.selectedItem.Quantity.ToString(), this.skin.GetStyle("quantity2"));
        }
        if (this.tooltipshown)
        {
            GUI.Box(new Rect(this.pos_x_inventory + (this.previndex[1] + 1) * this.size_inventory, this.pos_y_inventory + this.previndex[0] * this.size_inventory, 200, 35 + 20 * (this.selectedItem.Items.Description.Length / 35 + 1)),
                "<color=#ffffff>" + this.selectedItem.Items.Name + "</color>\n\n" + this.selectedItem.Items.Description, this.skin.GetStyle("tooltip"));
        }
    }

    /// <summary>
    /// S'occupe de dessiner la toolbar en bas de l'ecran.
    /// </summary>
    void DrawToolbar()
    {
        Rect rect = new Rect(this.pos_x_toolbar - 10, this.pos_y_toolbar - 11, this.columns * this.size_toolbar + 20, this.size_toolbar + 11);
        GUI.Box(rect, "", this.skin.GetStyle("frame"));
        rect = new Rect(this.pos_x_toolbar, this.pos_y_toolbar, this.columns * this.size_toolbar, this.size_toolbar);
        GUI.Box(rect, "", this.skin.GetStyle("toolbar"));
        for (int j = 0; j < this.columns; j++)
        {
            rect = new Rect(this.pos_x_toolbar + j * this.size_toolbar, this.pos_y_toolbar, this.size_toolbar, this.size_toolbar);
            if (this.cursor == j)
                GUI.Box(rect, "", this.skin.GetStyle("toolbar_selected"));
            if (rect.Contains(Event.current.mousePosition))
            {


                // Prise du stack
                int i = this.rows - 1;

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
            //fin de la gestion de la toolbar
            if (this.slots[this.rows - 1, j].Items.ID != -1)
            {
                rect.x += this.size_inventory / 5;
                rect.y += this.size_inventory / 5;
                rect.width -= this.size_inventory / 2.5f;
                rect.height -= this.size_inventory / 2.5f;
                GUI.DrawTexture(rect, this.slots[this.rows - 1, j].Items.Icon);
                if (this.slots[this.rows - 1, j].Quantity != 1)
                    GUI.Box(rect, this.slots[this.rows - 1, j].Quantity.ToString(), this.skin.GetStyle("quantity"));
            }
        }
    }


    /// <summary>
    /// Permet d'ajotuer des objes dans l'inventaire.
    /// </summary>
    /// <param name="iStack">Ajoute un maximum d'element, puis laisse le restant dans cette variable.</param>
    public void AddItemStack(ItemStack iStack, bool playSound = true)
    {
        int i = 0;
        int j = 0;
        while (iStack.Quantity > 0 && i < this.rows)
        {
            int k = (i + this.rows - 1) % this.rows;
            if (j == this.columns)
            {
                i++;
                j = -1;
            }
            else if (this.slots[k, j].Items.ID == iStack.Items.ID)
            {
                int mem = iStack.Items.Size - this.slots[k, j].Quantity;
                this.slots[k, j].Quantity += iStack.Quantity;
                iStack.Quantity -= mem;
            }
            j++;
        }

        i = 0;
        j = 0;
        while (iStack.Quantity > 0 && i < this.rows)
        {
            int k = (i + this.rows - 1) % this.rows;
            if (j == this.columns)
            {
                i++;
                j = -1;
            }

            else if (this.slots[k, j].Items.ID == -1)
            {
                this.slots[k, j] = new ItemStack(iStack.Items, iStack.Quantity);
                iStack.Quantity = 0;
            }
            j++;
        }
        if (playSound && iStack.Quantity == 0)
            this.sound.PlaySound(AudioClips.Plop, .5f);
        this.SaveInventory();
    }
    #endregion

    #region Public Methods
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
    public bool InventoryContains(ItemStack[] itlist, bool mastered = true)
    {
        bool contain_all = true;
        int i = 0;
        int len = itlist.Length;
        while (contain_all && i < len)
        {
            contain_all = contain_all && InventoryContains(itlist[i].Items, itlist[i].Quantity * (mastered ? 1 : 2));
            i += 1;
        }
        return contain_all;
    }
    /// <summary>
    /// Verifie si un Craft est possible
    /// </summary>
    /// <param name="craft"></param>
    /// <returns></returns>
    public bool InventoryContains(Craft craft, bool mastered = true)
    {
        return InventoryContains(craft.Consume, mastered);
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
                if (this.slots[i, j].Quantity <= quantity)
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
        this.SaveInventory();
    }

    /// <summary>
    /// suprime une liste d'objet de de l'inventaire
    /// </summary>
    /// <param name="itSlist"></param>
    public void DeleteItems(ItemStack[] itSlist, bool mastered = true)
    {
        foreach (ItemStack item in itSlist)
        {
            DeleteItems(item.Items, item.Quantity * (mastered ? 1 : 2));
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

    #endregion

    #region Tool and Looting
    /// <summary>
    /// Met l'outil dans la main du joueur.
    /// </summary>
    [Command]
    private void CmdSetTool(int toolID)
    {
        GameObject actualTool = null;
        GameObject toolPrefab = null;

        Item item = ItemDatabase.Find(toolID);
        if (item is Tool)
        {
            Tool outil = (Tool)item;
            toolPrefab = outil.ToolPrefab;
        }
        else
        {
            Consumable comsumable = (Consumable)item;
            toolPrefab = comsumable.ConsumablePrefab;
        }

        actualTool = GameObject.Instantiate(toolPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject;
        actualTool.transform.parent = gameObject.transform.FindChild("Character/Armature/WeaponSlot");
        actualTool.transform.localPosition = toolPrefab.transform.localPosition;
        actualTool.transform.localRotation = toolPrefab.transform.localRotation;
        actualTool.transform.localScale = toolPrefab.transform.localScale;
        NetworkServer.Spawn(actualTool);
        RpcSetTool(actualTool, toolPrefab.transform.localPosition, toolPrefab.transform.localRotation, toolPrefab.transform.localScale);
    }

    [ClientRpc]
    public void RpcSetTool(GameObject obj, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        obj.transform.parent = gameObject.transform.FindChild("Character/Armature/WeaponSlot");
        obj.transform.localPosition = pos;
        obj.transform.localRotation = rot;
        obj.transform.localScale = scale;
    }

    /// <summary>
    /// Retire l'objet dans la main du joueur.
    /// </summary>
    [Command]
    private void CmdRemoveTool()
    {
        if (gameObject.transform.FindChild("Character/Armature/WeaponSlot").childCount > 0)
        {
            GameObject actualTool = gameObject.transform.FindChild("Character/Armature/WeaponSlot").GetChild(0).gameObject;
            NetworkServer.UnSpawn(actualTool);
            GameObject.Destroy(actualTool);
        }
    }

    /// <summary>
    /// Informe l'inventaire de la colision avec un loot.
    /// </summary>
    /// <param name="loot"></param>
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
    /// <param name="id">L'id de l'item a ajouter</param>
    /// <param name="quantity">La quantite a ajouter</param>
    /// <param name="loot">Mettre null si il n'est pas question de loot</param>
    [ClientRpc]
    public void RpcAddItemStack(int id, int quantity, GameObject loot)
    {
        if (isLocalPlayer)
        {
            ItemStack itemS = new ItemStack(ItemDatabase.Find(id), quantity);
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

    [Client]
    public void DropAll()
    {
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                Drop(slots[i, j]);        
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

    #endregion

    #region Save & Load
    /// <summary>
    /// Sauvegarde l'inventaire et l'envoi au serveur.
    /// </summary>
    private void SaveInventory()
    {
        string save = "";
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                if (this.slots[i, j].Items.ID != -1)
                    save += i + ":" + j + ":" + this.slots[i, j].Items.ID + ":" + this.slots[i, j].Quantity + "|";
            }
        this.CmdSaveInventory(save);
    }

    /// <summary>
    /// Sauvegarde l'inventaire sur le serveur. 
    /// Rq : Appelez SaveInventory pour creer le str et appeller cette fct.
    /// </summary>
    /// <param name="save"></param>
    [Command]
    private void CmdSaveInventory(string save)
    {
        GameObject.Find("Map").GetComponent<Save>().SavePlayerInventory(gameObject, save);
    }

    /// <summary>
    /// Charge l'inventaire du personnage.
    /// </summary>
    [Command]
    private void CmdLoadInventory()
    {
        RpcLoadInventory(GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject).Inventory);
    }

    /// <summary>
    /// Charge l'inventaire du personnage. (Must be server)
    /// Rq : Appellez CmdLoadInventory pour automatiquement charger l'inventaire du joueur.
    /// </summary>
    /// <param name="str"></param>
    [ClientRpc]
    private void RpcLoadInventory(string save)
    {
        if (isLocalPlayer)
        {
            string[] strSlots = save.Split('|');
            foreach (string itemStack in strSlots)
            {
                if (itemStack != "")
                {
                    string[] info = itemStack.Split(':');
                    try
                    {
                        this.slots[int.Parse(info[0]), int.Parse(info[1])] = new ItemStack(ItemDatabase.Find(int.Parse(info[2])), int.Parse(info[3]));
                    }
                    catch
                    {
                        throw new System.ArgumentException("Le save (str) est corompu : " + itemStack);
                    }
                }
            }
        }
    }
    #endregion

    #region Getters & Setters
    /// <summary>
    /// Si l'inventaire est affiché.
    /// </summary>
    public bool InventoryShown
    {
        get { return this.inventoryShown; }
        set
        {
            this.inventoryShown = value;
            if (!this.inventoryShown)
                this.SaveInventory();
        }
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
        set
        {
            this.cursor = value % this.columns;
            if (this.cursor < 0)
                this.cursor += this.columns;
        }
    }

    /// <summary>
    /// Renvoie l'item stack utilisé.
    /// </summary>
    public ItemStack UsedItem
    {
        get { return this.slots[this.rows - 1, this.cursor]; }
        set
        {
            this.slots[this.rows - 1, this.cursor] = value;
            this.SaveInventory();
        }
    }

    /// <summary>
    /// La taille de la toolbar
    /// </summary>
    public int ToolbarSize
    {
        get { return this.size_toolbar; }
    }

    /// <summary>
    /// La taille de l'inventaire
    /// </summary>
    public int InventorySize
    {
        get { return this.size_inventory; }
    }

    /// <summary>
    /// Le nombre de lignes de l'inventaire
    /// </summary>
    public int Rows
    {
        get { return this.rows; }
    }

    /// <summary>
    /// Le nombre de colonnes de l'inventaire
    /// </summary>
    public int Columns
    {
        get { return this.columns; }
    }
    #endregion
}
