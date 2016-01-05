using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour {
    private int rows = 8, columns = 8;
    private string tooltip;
    private bool draggingItemStack;
    private ItemStack dragItemStack;
    private int Previndex;
    private bool Tooltipshown = false;
    private GUISkin skin;
    public List<ItemStack> inventory = new List<ItemStack>();
    public List<ItemStack> slots = new List<ItemStack>();
    public bool inventoryShown = false;
    
    
	// Use this for initialization
	void Start () {
        for (int i = 0; i <rows*columns; i++)
        {
            slots.Add(new ItemStack());
            inventory.Add(new ItemStack());
        }
        AddItemStack(1,64);
        AddItemStack(1,1,20);
        AddItemStack(1, 125);
        for (int i = 8 ; i < 21; i++)
        {
            AddItemStack(i,1);
        }
        skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
	}
	
    void Update()
    {
    }
	// Update is called once per frame
	void OnGUI ()
    {
        tooltip = "";
        GUI.skin = skin;
        if(inventoryShown)
        {
            DrawInventory();
            if (Tooltipshown && !draggingItemStack )
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 200, 20+20*(tooltip.Length/30+1)), tooltip, skin.GetStyle("tooltip"));
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
        if (draggingItemStack)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), dragItemStack.Item.itemicon);
            GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), dragItemStack.Quantity.ToString(), skin.GetStyle("quantity2"));
        }
        if (draggingItemStack && Event.current.type == EventType.MouseUp)
        {
            if (!new Rect(200, 200, 32 * rows, 32 * columns).Contains(Event.current.mousePosition) || !inventoryShown)
            {
                if (inventory[Previndex].Item.id != dragItemStack.Item.id)
                {
                    inventory[Previndex] = dragItemStack;
                }
                else
                {
                    inventory[Previndex].Add(dragItemStack.Quantity);
                }
                draggingItemStack = false;
                dragItemStack = null;
            }
        }
        
    }
   public void DrawInventory()
    {
        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Rect rect = new Rect(200 + j * 32, 100 + i * 32, 32, 32);
                GUI.Box(rect,"",skin.GetStyle("slot"));
                slots[index] = inventory[index];
                if (slots[index].Item.Name != null)
                {
                    GUI.DrawTexture(rect, slots[index].Item.itemicon);
                    GUI.Box(rect, slots[index].Quantity.ToString(), skin.GetStyle("quantity"));
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        tooltip = Tooltip(inventory[index]);
                        if (Event.current.button == 0 && Event.current.type == EventType.mouseDrag && !draggingItemStack)
                        {
                            draggingItemStack = true;
                            Previndex = index;

                            if (Event.current.alt)
                            {
                                dragItemStack = new ItemStack(slots[index].Item, 1);
                                if (slots[index].Quantity == 1)
                                {
                                    inventory[index] = new ItemStack();
                                }
                                else
                                {
                                    inventory[index].Add(-1);
                                }
                            }
                            else if (Event.current.control)
                            {
                                dragItemStack = new ItemStack(slots[index].Item, (slots[index].Quantity+1)/2);
                                if (slots[index].Quantity == 1)
                                {
                                    inventory[index] = new ItemStack();
                                }
                                else
                                {
                                    inventory[index].Add(-((slots[index].Quantity+1)/ 2));
                                }
                            }
                            else if (Event.current.shift)
                            {
                                int temp = 10;
                                if (slots[index].Quantity <= 10)
                                {
                                    temp = inventory[index].Quantity;
                                    inventory[index] = new ItemStack();
                                }
                                else
                                {
                                    inventory[index].Add(-10);
                                }
                                dragItemStack = new ItemStack(slots[index].Item, temp);
                            }
                            else
                            {
                                dragItemStack = slots[index];
                                inventory[index] = new ItemStack();
                            }
                            
                        }
                        if (Event.current.type == EventType.MouseUp && draggingItemStack)
                        {
                            if (inventory[index].Item.id == dragItemStack.Item.id)
                            {
                                if (inventory[index].Quantity + dragItemStack.Quantity > inventory[index].Item.Maxquantity)
                                {
                                    int temp = inventory[index].Item.Maxquantity - inventory[index].Quantity;
                                    dragItemStack.Add(-temp);
                                    inventory[index].Add(temp);
                                    if (inventory[Previndex].Item.id != dragItemStack.Item.id)
                                    {
                                        inventory[Previndex] = dragItemStack;
                                    }
                                    else
                                    {
                                        inventory[Previndex].Add(dragItemStack.Quantity);
                                    }
                                    draggingItemStack = false;
                                    dragItemStack = null;
                                }
                                else
                                {
                                    inventory[index].Add(dragItemStack.Quantity);
                                    draggingItemStack = false;
                                    dragItemStack = null;
                                }
                            }
                            else
                            {
                                if (inventory[Previndex].Item.id != dragItemStack.Item.id)
                                {
                                    inventory[Previndex] = inventory[index];
                                    inventory[index] = dragItemStack;
                                }
                                else
                                {
                                    inventory[Previndex].Add(dragItemStack.Quantity);
                                }
                                dragItemStack = null;
                                draggingItemStack = false;
                            }
                        }
                        if (Event.current.isMouse && Event.current.type == EventType.mouseDown && Event.current.button == 1)
                        {
                            if (slots[index].Item.Type == Item.ItemStack_Type.Consommable)
                            {
                                Consumable(slots[index], index);  
                            }
                        }
                    }
                    Tooltipshown = tooltip != "";
                    

                }
                else if (rect.Contains(Event.current.mousePosition))
                {
                    Tooltipshown = false;
                    if (Event.current.type == EventType.MouseUp && draggingItemStack)
                    {
                        draggingItemStack = false;
                        inventory[index] = dragItemStack;
                        dragItemStack = null;
                    }
                }
                index += 1;
            }
        }
    }
    string Tooltip(ItemStack item)
    {
        tooltip += "<color=#ffffff>"+item.Item.Name+"</color>\n\n" +item.Item.Description;
        return tooltip;
    }
   public void AddItemStack( int id, int quantity)
    {
        int i = 0;
        while (quantity > 0 && i < inventory.Count)
        {
            while (i < inventory.Count && inventory[i].Item.id != id )
            {
                i++;
            }
            if (i < inventory.Count)
            {
                if (inventory[i].Quantity + quantity > inventory[i].Item.Maxquantity)
                {
                    int temp = inventory[i].Item.Maxquantity - inventory[i].Quantity;
                    quantity -= temp;
                    inventory[i].Add(temp);
                    i++;
                }
                else
                {
                    inventory[i].Add(quantity);
                    quantity = 0;
                }
                
            }
            else
            {
                i = 0;
                while (i < inventory.Count && inventory[i].Item.Name != null)
                {
                    i++;
                }
                if (i < inventory.Count)
                {
                    inventory[i] = new ItemStack(ItemDatabase.Find(id), 1);
                    quantity -= 1;
                }
                else
                {
                    print(quantity + "objets n'ont pas pu être ajoutés");
                    quantity = 0;
                }
            }
        }
    }
    public void AddItemStack(int id, int quantity,int pos)
    {
        inventory[pos] = new ItemStack(ItemDatabase.Find(id), 1);
        inventory[pos].Add(quantity - 1);

    }

    public bool InventoryContains(int id)
    {
        int i = 0;
        while (i < inventory.Count && inventory[i].Item.id != id)
        {
            i++;
        }
        return (i < inventory.Count);
    }
    public void RemoveItemStack( int id)
    {
        int i = 0;
        while (i < inventory.Count && inventory[i].Item.id != id)
        {
            i++;
        }
        if (i < inventory.Count)
        {
            if(inventory[i].Quantity > 1)
            {
                inventory[i].Add(-1);
            }
            else
                inventory[i] = new ItemStack();
        }
        else
        {
            print("item doesn't exist");
        }
    }
    public void RemoveItemStack(int id, int quantity)
    {
        int i = 0;
        while (quantity > 0)
        {
            while (i < inventory.Count && inventory[i].Item.id != id)
            {
                i++;
            }
            if (i < inventory.Count)
            {
                if (inventory[i].Quantity > quantity)
                {
                    inventory[i].Add(-quantity);
                }
                else
                {
                    quantity -= inventory[i].Quantity;
                    inventory[i] = new ItemStack();
                }
            }
            else
            {
                print(quantity.ToString() +" manquant");
                quantity = 0;
            }
        }
    }
    private void Consumable(ItemStack item,int slot)
    {
        switch (item.Item.id)
        {
            case 6:
                print("crafting window not available yet");
                break;
            case 7:
                print("crafting window not available yet");
                break;
            default:
                inventory[slot] = new ItemStack();
                break;
        }
    }
    public void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
                PlayerPrefs.SetInt("Inventory " + i, inventory[i].Item.id);
                PlayerPrefs.SetInt("Inventoryquantity " + i, inventory[i].Quantity);
        }
        
    }
    public void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            int id = PlayerPrefs.GetInt("Inventory " + i,-1);
            if (id == -1)
            {
                inventory[i] = new ItemStack();
            }
            else
            {
                inventory[i] = new ItemStack(ItemDatabase.Find(id), 1);
                int quantity = PlayerPrefs.GetInt("Inventoryquantity " + i, 0);
                AddItemStack(id, quantity, i);
            }
        }
    }
}
