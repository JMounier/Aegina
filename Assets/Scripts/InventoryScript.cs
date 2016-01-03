using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InventoryScript : MonoBehaviour {
    private int rows = 8, columns = 8;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;
    private ItemDatabase database;
    private bool inventoryShown = false;
    private string tooltip;
    private bool draggingItem;
    private Item dragItem;
    private int Previndex;
    public bool InventoryShown
    {
        get { return inventoryShown; }
    }
    private bool Tooltipshown = false;
	// Use this for initialization
	void Start () {
        for (int i = 0; i <rows*columns; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        AddItem(1,64);
        AddItem(1,1,20);
        AddItem(1, 125);
        for (int i = 8 ; i < 21; i++)
        {
            AddItem(i,1);
        }
        
	}
	
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryShown = !inventoryShown;
        }
    }
	// Update is called once per frame
	void OnGUI ()
    {
        tooltip = "";
        GUI.skin = skin;
        if(inventoryShown)
        {
            DrawInventory();
            if (Tooltipshown && !draggingItem )
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 200, 20+20*(tooltip.Length/30+1)), tooltip, skin.GetStyle("tooltip"));
            }
            if (GUI.Button(new Rect(200, 220 + 32 * columns, 100, 20), "save"))
            {
                SaveInventory();
            }
            if (GUI.Button(new Rect(310, 220 + 32 * columns, 100, 20), "Load"))
            {
                LoadInventory();
            }
        }
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), dragItem.itemicon);
            GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), dragItem.Quantity.ToString(), skin.GetStyle("quantity2"));
        }
        if (draggingItem && Event.current.type == EventType.MouseUp)
        {
            if (!new Rect(200, 200, 32 * rows, 32 * columns).Contains(Event.current.mousePosition) || !InventoryShown)
            {
                if (inventory[Previndex].id != dragItem.id)
                {
                    inventory[Previndex] = dragItem;
                }
                else
                {
                    inventory[Previndex].add(dragItem.Quantity);
                }
                draggingItem = false;
                dragItem = null;
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
                if (slots[index].Name != null)
                {
                    GUI.DrawTexture(rect, slots[index].itemicon);
                    GUI.Box(rect, slots[index].Quantity.ToString(), skin.GetStyle("quantity"));
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        tooltip = Tooltip(inventory[index]);
                        if (Event.current.button == 0 && Event.current.type == EventType.mouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            Previndex = index;

                            if (Event.current.alt)
                            {
                                dragItem = new Item(slots[index].Prefab, 1);
                                if (slots[index].Quantity == 1)
                                {
                                    inventory[index] = new Item();
                                }
                                else
                                {
                                    inventory[index].add(-1);
                                }
                            }
                            else if (Event.current.control)
                            {
                                dragItem = new Item(slots[index].Prefab, (slots[index].Quantity+1)/2);
                                if (slots[index].Quantity == 1)
                                {
                                    inventory[index] = new Item();
                                }
                                else
                                {
                                    inventory[index].add(-((slots[index].Quantity+1)/ 2));
                                }
                            }
                            else if (Event.current.shift)
                            {
                                int temp = 10;
                                if (slots[index].Quantity <= 10)
                                {
                                    temp = inventory[index].Quantity;
                                    inventory[index] = new Item();
                                }
                                else
                                {
                                    inventory[index].add(-10);
                                }
                                dragItem = new Item(slots[index].Prefab, temp);
                            }
                            else
                            {
                                dragItem = slots[index];
                                inventory[index] = new Item();
                            }
                            
                        }
                        if (Event.current.type == EventType.MouseUp && draggingItem)
                        {
                            if (inventory[index].id == dragItem.id)
                            {
                                if (inventory[index].Quantity + dragItem.Quantity > inventory[index].Maxquantity)
                                {
                                    int temp = inventory[index].Maxquantity - inventory[index].Quantity;
                                    dragItem.add(-temp);
                                    inventory[index].add(temp);
                                    if (inventory[Previndex].id != dragItem.id)
                                    {
                                        inventory[Previndex] = dragItem;
                                    }
                                    else
                                    {
                                        inventory[Previndex].add(dragItem.Quantity);
                                    }
                                    draggingItem = false;
                                    dragItem = null;
                                }
                                else
                                {
                                    inventory[index].add(dragItem.Quantity);
                                    draggingItem = false;
                                    dragItem = null;
                                }
                            }
                            else
                            {
                                if (inventory[Previndex].id != dragItem.id)
                                {
                                    inventory[Previndex] = inventory[index];
                                    inventory[index] = dragItem;
                                }
                                else
                                {
                                    inventory[Previndex].add(dragItem.Quantity);
                                }
                                dragItem = null;
                                draggingItem = false;
                            }
                        }
                        if (Event.current.isMouse && Event.current.type == EventType.mouseDown && Event.current.button == 1)
                        {
                            if (slots[index].Type == Prefab.Item_Type.Consommable)
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
                    if (Event.current.type == EventType.MouseUp && draggingItem)
                    {
                        draggingItem = false;
                        inventory[index] = dragItem;
                        dragItem = null;
                    }
                }
                index += 1;
            }
        }
    }
    string Tooltip(Item item)
    {
        tooltip += "<color=#ffffff>"+item.Name+"</color>\n\n" +item.Description;
        return tooltip;
    }
   public void AddItem( int id, int quantity)
    {
        int i = 0;
        while (quantity > 0 && i < inventory.Count)
        {
            while (i < inventory.Count && inventory[i].id != id )
            {
                i++;
            }
            if (i < inventory.Count)
            {
                if (inventory[i].Quantity + quantity > inventory[i].Maxquantity)
                {
                    int temp = inventory[i].Maxquantity - inventory[i].Quantity;
                    quantity -= temp;
                    inventory[i].add(temp);
                    i++;
                }
                else
                {
                    inventory[i].add(quantity);
                    quantity = 0;
                }
                
            }
            else
            {
                i = 0;
                while (i < inventory.Count && inventory[i].Name != null)
                {
                    i++;
                }
                if (i < inventory.Count)
                {
                    int j = 0;
                    while (j < database.items.Count && database.items[j].id != id)
                    {
                        j++;
                    }
                    if (j < database.items.Count)
                    {
                        inventory[i] = new Item(database.items[j], 1);
                    }
                    else
                    {
                        print("id not found");
                    }
                    quantity -= 1;
                }
                else
                {
                    print(quantity + "objets n'on pas pu être ajoutés");
                    quantity = 0;
                }
            }
        }
    }
    public void AddItem(int id, int quantity,int pos)
    {
        int j = 0;
        while (j < database.items.Count && database.items[j].id != id)
        {
            j++;
        }
        if (j < database.items.Count)
        {
            inventory[pos] = new Item(database.items[j], 1);
            inventory[pos].add(quantity - 1);
        }
        else
        {
            print("id not found");
        }

    }

    public bool InventoryContains(int id)
    {
        int i = 0;
        while (i < inventory.Count && inventory[i].id != id)
        {
            i++;
        }
        return (i < inventory.Count);
    }
    public void RemoveItem( int id)
    {
        int i = 0;
        while (i < inventory.Count && inventory[i].id != id)
        {
            i++;
        }
        if (i < inventory.Count)
        {
            if(inventory[i].Quantity > 1)
            {
                inventory[i].add(-1);
            }
            else
                inventory[i] = new Item();
        }
        else
        {
            print("item doesn't exist");
        }
    }
    public void RemoveItem(int id, int quantity)
    {
        int i = 0;
        while (quantity > 0)
        {
            while (i < inventory.Count && inventory[i].id != id)
            {
                i++;
            }
            if (i < inventory.Count)
            {
                if (inventory[i].Quantity > quantity)
                {
                    inventory[i].add(-quantity);
                }
                else
                {
                    quantity -= inventory[i].Quantity;
                    inventory[i] = new Item();
                }
            }
            else
            {
                print(quantity.ToString() +" manquant");
                quantity = 0;
            }
        }
    }
    private void Consumable(Item item,int slot)
    {
        switch (item.id)
        {
            case 6:
                print("crafting window not available yet");
                break;
            case 7:
                print("crafting window not available yet");
                break;
            default:
                inventory[slot] = new Item();
                break;
        }
    }
    public void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
                PlayerPrefs.SetInt("Inventory " + i, inventory[i].id);
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
                inventory[i] = new Item();
            }
            else
            {
                int j = 0;
                while (j < database.items.Count && database.items[j].id != id)
                {
                    j++;
                }
                if (j < database.items.Count)
                {
                    inventory[i] = new Item(database.items[j], 1);
                }
                else
                {
                    print("id not found");
                }
            
            int quantity = PlayerPrefs.GetInt("Inventoryquantity " + i, 0);
            AddItem(id, quantity, i);
            }
        }
    }
}
