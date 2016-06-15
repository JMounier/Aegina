using UnityEngine;
using System.Collections;

public class Armor : Item
{

    protected int armorValue;
    protected Material exterieur;
    protected Material interieur;

    public Armor() : base()
    {
        this.armorValue = 0;
        this.exterieur = null;
        this.interieur = null;
    }

    public Armor(Armor armor) : base(armor)
    {
        this.armorValue = armor.ArmorValue;
        this.exterieur = armor.exterieur;
        this.interieur = armor.interieur;
    }

    public Armor(int id, Text name, Text description, Texture2D icon, Entity ent, int armorValue, Material exterieur, Material interieur) :
        base(id, name, description, 1, icon, ent)
    {
        this.armorValue = armorValue;
        this.exterieur = exterieur;
        this.interieur = interieur;
    }


    //Getters Setters

    public Material Exterieur
    {
        get { return this.exterieur; }
        set { this.exterieur = value; }
    }

    public Material Interieur
    {
        get { return this.interieur; }
        set { this.interieur = value; }
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

    public TopArmor(int id, Text name, Text description, Texture2D icon, Entity ent, int armorValue, Material exterieur, Material interieur) :
       base(id, name, description, icon, ent, armorValue, exterieur, interieur)
    { }

    public static void SetArmor(GameObject player, TopArmor topArmor)
    {
        if (topArmor == null)
        {
			Transform Back = player.transform.FindChild("Character").FindChild("Armature").FindChild("Backet_Bone");
			Back.FindChild("Armor").gameObject.SetActive(false);
			Back.FindChild("ArmorL").gameObject.SetActive(false);
			Back.FindChild("ArmorR").gameObject.SetActive(false);
        }
        else
        {
			Transform Back = player.transform.FindChild("Character").FindChild("Armature").FindChild("Backet_Bone");
            Transform torse = Back.FindChild("Armor");
			Transform leftBracer = Back.FindChild("ArmorL");
			Transform rightBracer = Back.FindChild("ArmorR");
            torse.gameObject.SetActive(true);
            leftBracer.gameObject.SetActive(true);
            rightBracer.gameObject.SetActive(true);
			leftBracer.GetComponent<MeshRenderer>().materials = new Material[] { topArmor.Exterieur,topArmor.Interieur} ;
			rightBracer.GetComponent<MeshRenderer>().materials = new Material[] { topArmor.Exterieur,topArmor.Interieur};
			torse.GetComponent<MeshRenderer> ().materials = new Material[] { topArmor.Interieur, topArmor.Exterieur };
        }

    }
}

public class BottomArmor : Armor
{
    // Constructors
    public BottomArmor() : base() { }

    public BottomArmor(BottomArmor bottomArmor) : base(bottomArmor) { }

    public BottomArmor(int id, Text name, Text description, Texture2D icon, Entity ent, int armorValue, Material exterieur, Material interieur) :
       base(id, name, description, icon, ent, armorValue, exterieur, interieur)
    { }

    public static void SetArmor(GameObject player, BottomArmor topArmor)
    {
        //To Do
    }
}