using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    private int rows = 8;
    private int columns = 8;
    private int[] previndex = new int[2];
    private bool draggingItemStack = false;
    private bool inventoryShown = false;
    private bool tooltipshown = false;
    private GUISkin skin;
    private ItemStack selectedItem;
    private ItemStack[,] slots;


    // Use this for initialization
    void Start()
    {
        this.slots = new ItemStack[this.rows, this.columns];
        this.ClearInventory();

        this.AddItemStack(new ItemStack(ItemDatabase.Stone, 42));
        this.AddItemStack(new ItemStack(ItemDatabase.Log, 100));
        this.AddItemStack(new ItemStack(ItemDatabase.Sand, 10000));
        this.AddItemStack(new ItemStack(ItemDatabase.Log, 30));

        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
    }

    // Methods
    void OnGUI()
    {
        if (this.inventoryShown)
        {
            GUI.skin = this.skin;
            this.DrawInventory();
            if (this.tooltipshown)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 200, 35 + 10 * (this.selectedItem.Items.Description.Length / 28 + 1)),
                    "<color=#ffffff>" + this.selectedItem.Items.Name + "</color>\n\n" + this.selectedItem.Items.Description, this.skin.GetStyle("tooltip"));
            }
            if (GUI.Button(new Rect(200, 220 + 32 * this.columns, 100, 20), "save", this.skin.GetStyle("button")))
            {
                SaveInventory();
            }
            if (GUI.Button(new Rect(310, 220 + 32 * this.columns, 100, 20), "Load", this.skin.GetStyle("button")))
            {
                LoadInventory();
            }
            if (this.draggingItemStack)
            {
                GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), this.selectedItem.Items.Icon);
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), this.selectedItem.Quantity.ToString(), this.skin.GetStyle("quantity2"));
            }
        }
    }
    public void DrawInventory()
    {
        this.tooltipshown = false;
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                // Dessin du slot
                Rect rect = new Rect(200 + j * 32, 100 + i * 32, 32, 32);
                GUI.Box(rect, "", this.skin.GetStyle("slot"));

                if (this.slots[i, j].Items.ID != -1)
                {
                    // Dessin de l'item + quantite
                    GUI.DrawTexture(rect, this.slots[i, j].Items.Icon);
                    GUI.Box(rect, this.slots[i, j].Quantity.ToString(), this.skin.GetStyle("quantity"));

                    // Interaction avec le slot
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        // Prise du stack
                        if (!this.draggingItemStack && Event.current.button == 0 && Event.current.type == EventType.MouseDown)
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
                        // Description du stack
                        else if (!this.draggingItemStack)
                        {
                            this.selectedItem = this.slots[i, j];
                            this.tooltipshown = true;
                        }
                    }
                }
                // Relachement du stack
                if (this.draggingItemStack && Event.current.button == 0 && Event.current.type == EventType.MouseUp && rect.Contains(Event.current.mousePosition))
                {
                    this.draggingItemStack = false;
                    if (this.slots[i, j].Items.ID == this.selectedItem.Items.ID)
                    {
                        if (this.slots[i, j].Quantity + this.selectedItem.Quantity > this.selectedItem.Items.Size)
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
            }
    }

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

    public void ClearInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
                this.slots[i, j] = new ItemStack();
    }

    public void SaveInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
                PlayerPrefs.SetString("Inventory " + i + " " + j, this.slots[i, j].Items.ID + " " + this.slots[i, j].Items.Meta + " " + this.slots[i, j].Quantity);
    }

    public void LoadInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                string[] save = PlayerPrefs.GetString("Inventory " + i + " " + j, "-1 0 0").Split();
                this.slots[i, j] = new ItemStack(ItemDatabase.Find(System.Convert.ToInt32(save[0]), System.Convert.ToInt32(save[1])), System.Convert.ToInt32(save[2]));
            }
    }

    // Getters & Setters
    public bool InventoryShown
    {
        get { return this.inventoryShown; }
        set { this.inventoryShown = value; }
    }

}
