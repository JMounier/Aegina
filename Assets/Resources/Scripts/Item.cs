 using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    private int maxquantity;
    public int Maxquantity
    {
        get { return maxquantity; }
    }
    private string itemname;
    public string Name
    {
        get { return itemname; }
    }
    private int iD;
    public int id
    {
        get { return iD; }
    }
    private string description;
    public string Description
    {
        get { return description; }
    }
    public Texture2D itemicon;
    private int attackpower;
    public int Attackpower
    {
        get { return attackpower; }
    }
    private int mineEfficiency;
    public int MineEfficiency
    {
        get { return mineEfficiency; }
    }
    private int axeEfficiency;
    public int AxeEfficiency
    {
        get { return axeEfficiency; }
    }
    public enum ItemStack_Type
    {
        Arme, Consommable, Ressource, Armure, outils
    }
    private ItemStack_Type itemType;
    public ItemStack_Type Type
    {
        get { return itemType; }
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
}
public class ItemStack
{
    private Item prefab;
    public Item Item
    {
        get { return prefab; }
    }
    private int quantity;
    public int Quantity
    {
        get { return quantity; }
    }
    public ItemStack(Item prefab, int quantity)
    {
        this.prefab = prefab;
        this.quantity = quantity;
    }

    public ItemStack()
    {
        this.prefab = new Item();
        this.quantity =0;
    }
    public void add(int nombre)
    {
        this.quantity += nombre;
    }
}