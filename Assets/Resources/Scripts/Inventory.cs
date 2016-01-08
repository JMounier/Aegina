using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    private int rows = 8;
    private int columns = 8;
    private int[] previndex;
    private bool draggingItemStack = false;
    private bool inventoryShown = false;
    private GUISkin skin;
    private ItemStack selectedItem;
    private ItemStack[,] slots;


    // Use this for initialization
    void Start()
    {
        this.slots = new ItemStack[this.rows, this.columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                this.slots[i, j] = new ItemStack();

        AddItemStack(new ItemStack(ItemDatabase.Floatium, 42));
        AddItemStack(new ItemStack(ItemDatabase.Log, 15));
        AddItemStack(new ItemStack(ItemDatabase.Sand, 100000));
       
        skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
    }
    
    // Methods
    void OnGUI()
    {
        GUI.skin = skin;
        if (this.inventoryShown)
        {
            DrawInventory();
            if (!this.draggingItemStack)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 200, 20 + 20 * (selectedItem.Items.Description.Length / 30 + 1)),
                    "<color=#ffffff>" + this.selectedItem.Items.Name + "</color>\n\n" + this.selectedItem.Items.Description, skin.GetStyle("tooltip"));
            }
            if (GUI.Button(new Rect(200, 220 + 32 * columns, 100, 20), "save", skin.GetStyle("button")))
            {
                SaveInventory();
            }
            if (GUI.Button(new Rect(310, 220 + 32 * columns, 100, 20), "Load", skin.GetStyle("button")))
            {
                LoadInventory();
            }
        }
        if (this.draggingItemStack)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), selectedItem.Items.Icon);
            GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), selectedItem.Quantity.ToString(), skin.GetStyle("quantity2"));          
        }

    }
    public void DrawInventory()
    {
        for (int i = 0; i < this.rows; i++)
            for (int j = 0; j < this.columns; j++)
            {
                // Dessin du slot
                Rect rect = new Rect(200 + j * 32, 100 + i * 32, 32, 32);
                GUI.Box(rect, "", skin.GetStyle("slot"));

                // Dessin de l'item + quantite
                GUI.DrawTexture(rect, this.slots[i, j].Items.Icon);
                GUI.Box(rect, this.slots[i, j].Quantity.ToString(), skin.GetStyle("quantity"));

                // Interaction avec le slot
                if (!this.draggingItemStack && rect.Contains(Event.current.mousePosition))
                {
                    this.selectedItem = this.slots[i, j];
                    if (Event.current.button == 0 && Event.current.type == EventType.mouseDrag)
                    {
                        this.draggingItemStack = true;
                        this.previndex = new int[2] { i, j };

                        if (Event.current.shift)
                        {
                            this.selectedItem = new ItemStack(slots[i, j].Items, (slots[i, j].Quantity + 1) / 2);
                            if (this.slots[i, j].Quantity == 1)
                            {
                                this.slots[i, j] = new ItemStack();
                            }
                            else
                            {
                                this.slots[i, j].Quantity -= (slots[i, j].Quantity + 1) / 2;
                            }
                        }
                        else
                        {
                            this.slots[i, j] = new ItemStack();
                        }

                    }
                    else if (draggingItemStack && Event.current.type == EventType.MouseUp)
                    {
                        draggingItemStack = false;
                        if (this.slots[i, j].Items.ID == this.selectedItem.Items.ID)
                        {
                            if (this.slots[i, j].Quantity + this.selectedItem.Quantity > this.selectedItem.Items.Size)
                                this.selectedItem.Quantity -= this.selectedItem.Items.Size - this.slots[i, j].Quantity;

                            this.slots[i, j].Quantity += this.selectedItem.Quantity;
                        }
                        else
                        {
                            this.slots[this.previndex[0], this.previndex[1]] = this.slots[i, j];
                            this.slots[i, j] = this.selectedItem;
                        }
                    }
                }
            }

    }

    public void AddItemStack(ItemStack iStack)
    {
        int i = 0;
        int j = 0;
        while (iStack.Quantity > 0)
        {
            j++;
            if (j == this.columns)
            {
                i++;
                j = 0;
            }
            if (i == this.rows)
                break;
            if (this.slots[i, j].Items.ID == -1)
            {
                this.slots[i, j] = iStack;
                iStack.Quantity = 0;
            }
            else if (this.slots[i, j].Items.ID == iStack.Items.ID)
            {
                int mem = iStack.Items.Size - this.slots[i, j].Quantity;
                this.slots[i, j].Quantity += iStack.Quantity;
                iStack.Quantity -= mem;
            }
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
