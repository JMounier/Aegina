using UnityEngine;
using System.Collections;

public class Armor : Item
{

    protected GameObject armorPrefab;
    protected int armorValue;

    public Armor() : base()
    {
        this.armorValue = 0;
        this.armorPrefab = null;
    }

    public Armor(Armor armor) : base(armor)
    {
        this.armorValue = armor.ArmorValue;
        this.armorPrefab = armor.armorPrefab;
    }

    public Armor(int id, Text name, Text description, Texture2D icon, Entity ent, int armorValue, GameObject armorPrefab) :
        base(id, name, description, 1, icon, ent)
    {
        this.armorValue = armorValue;
        this.armorPrefab = armorPrefab;
    }


    //Getters Setters

    public GameObject ArmorPrefab
    {
        set { this.armorPrefab = value; }
        get { return this.armorPrefab; }
    }

    public int ArmorValue
    {
        get { return this.armorValue; }
        set { this.armorValue = value; }
    }
}

public class TopArmor : Armor
{
    // Constructors
    public TopArmor() : base() { }

    public TopArmor(TopArmor topArmor) : base(topArmor) { }

    public TopArmor(int id, Text name, Text description, Texture2D icon, Entity ent, int armorValue, GameObject armorPrefab) :
       base(id, name, description, icon, ent, armorValue, armorPrefab)
    { }
}

public class BottomArmor : Armor
{
    // Constructors
    public BottomArmor() : base() { }

    public BottomArmor(BottomArmor bottomArmor) : base(bottomArmor) { }

    public BottomArmor(int id, Text name, Text description, Texture2D icon, Entity ent, int armorValue, GameObject armorPrefab) :
       base(id, name, description, icon, ent, armorValue, armorPrefab)
    { }
}