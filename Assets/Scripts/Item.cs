using UnityEngine;
using System.Collections;

[System.Serializable]
public class Prefab {

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
    public enum Item_Type
    {
        Arme, Consommable, Ressource, Armure, outils
    }
    private Item_Type itemType;
    public Item_Type Type
    {
        get { return itemType; }
    }
    public Prefab(string name, int id, string desc,int maxquantity, int attackpower, int MineEfficiency, int axeEfficiency, Item_Type type)
    {
        this.itemname = name;
        this.iD = id;
        this.description = desc;
        this.attackpower = attackpower;
        this.itemicon = Resources.Load<Texture2D>("ItemIcons/"+ itemname);
        this.mineEfficiency = MineEfficiency;
        this.axeEfficiency = axeEfficiency;
        this.itemType = type;
        this.maxquantity = maxquantity;
    }
    public Prefab()
    {
        this.mineEfficiency = 1;
        this.axeEfficiency = 1;
        this.attackpower = 1;
        this.iD = -1;
    }
}
public class Item
{
    private Prefab prefab;
    public Prefab Prefab
    {
        get { return prefab; }
    }
    private int quantity;
    public string Description
    {
        get { return prefab.Description; }
    }
    public Texture2D itemicon
    {
        get { return prefab.itemicon; }
    }
    public string Name
    {
        get { return prefab.Name; }
    }
    public int id
    {
        get { return prefab.id; }
    }
    public int Maxquantity
    {
        get { return prefab.Maxquantity; }
    }
    public Prefab.Item_Type Type
    {
        get { return prefab.Type; }
    }
    public int Quantity
    {
        get { return quantity; }
    }
    public Item(Prefab prefab, int quantity)
    {
        this.prefab = prefab;
        this.quantity = quantity;
    }
    public void add(int nombre)
    {
        this.quantity += nombre;
    }
    public Item()
    {
        this.prefab = new Prefab();
        this.quantity =0;
    }
}