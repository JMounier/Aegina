 using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    private int maxquantity;
    private string itemname;
    private int iD;
    private string description;
    public Texture2D itemicon;
    private int attackpower;
    private int mineEfficiency;
    private int axeEfficiency;
    private ItemStack_Type itemType;
    public enum ItemStack_Type
    {
        Arme, Consommable, Ressource, Armure, Outils
    }


    public Item(string name, int id, string desc,int maxquantity, int attackpower, int MineEfficiency, int axeEfficiency, ItemStack_Type type)
    {
        this.itemname = name;
        this.iD = id;
        this.description = desc;
        this.attackpower = attackpower;
        this.itemicon = Resources.Load<Texture2D>("Sprites/ItemIcons/"+ itemname);
        this.mineEfficiency = MineEfficiency;
        this.axeEfficiency = axeEfficiency;
        this.itemType = type;
        this.maxquantity = maxquantity;
    }
    public Item()
    {
        this.mineEfficiency = 1;
        this.axeEfficiency = 1;
        this.attackpower = 1;
        this.iD = -1;
    }
    public int Maxquantity
    {
        get { return maxquantity; }
    }
    public string Name
    {
        get { return itemname; }
    }
    public int id
    {
        get { return iD; }
    }
    public string Description
    {
        get { return description; }
    }
    public int Attackpower
    {
        get { return attackpower; }
    }
    public int MineEfficiency
    {
        get { return mineEfficiency; }
    }
    public int AxeEfficiency
    {
        get { return axeEfficiency; }
    }
    public ItemStack_Type Type
    {
        get { return itemType; }
    }
}
public class ItemStack
{
    private Item items;
    private int quantity;
    public ItemStack(Item prefab, int quantity)
    {
        this.items = prefab;
        this.quantity = quantity;
    }

    public ItemStack()
    {
        this.items = new Item();
        this.quantity =0;
    }
    public void Add(int nombre)
    {
        this.quantity += nombre;
    }
    public int Quantity
    {
        get { return quantity; }
    }
    public Item Item
    {
        get { return items; }
    }
}