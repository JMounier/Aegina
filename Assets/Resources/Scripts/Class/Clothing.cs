using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clothing
{
    private int id;
    private Texture2D texture;
    private Text description;
    Type type;

    public enum Type { None, Body, Tshirt, Pant, Gloves, Eyes, Hair, Beard };


    // Constructeur 
    public Clothing()
    {
        this.id = -1;
        this.texture = null;
        this.description = new Text();
        this.type = Type.None;
    }

    public Clothing(int id, Texture2D texture, Text description, Type type)
    {
        this.id = id;
        if (texture.width != 512 || texture.height != 512)
            throw new System.Exception("Image mauvaise taille");
        this.texture = texture;
        this.description = description;
        this.type = type;
    }

    public Clothing(Clothing cloth)
    {
        this.id = cloth.id;
        this.texture = cloth.texture;
        this.description = cloth.description;
        this.type = cloth.type;
    }

    public static Texture2D MergeSkin(params Clothing[] skin)
    {
        Texture2D newCloth = new Texture2D(512, 512);
        foreach (Clothing cloth in skin)
        {
            for (int i = 0; i < 512; i++)
                for (int j = 0; j < 512; j++)
                {
                    Color pixel = cloth.texture.GetPixel(j, i);
                    if (pixel.a != 0)
                        newCloth.SetPixel(j, i, pixel);
                }
        }
        return newCloth;
    }
    // Method
    public static IEnumerable<Clothing> Clothings
    {
        get
        {
            yield return WhiteSkin;
            yield return BrownPant;
            yield return BrownGloves;
            yield return BrownEye;
        }
    }

    /// <summary>
    /// Retourne le Clothing correspondant. (En copie)
    /// </summary>
    public static Clothing Find(int id)
    {
        foreach (Clothing cloth in Clothings)
        {
            if (cloth.id == id)
                return new Clothing(cloth);
        }
        throw new System.Exception("Clothing.Find : Clothing not find");
    }

    //Skin
    public static readonly Clothing WhiteSkin = new Clothing(10, Resources.Load<Texture2D>("Models/Character/Textures/Body_White"), TextDatabase.WhiteSkin, Type.Body);
    public static readonly Clothing BrownPant = new Clothing(20, Resources.Load<Texture2D>("Models/Character/Textures/Skin_Base"), TextDatabase.brownPant, Type.Pant);
    public static readonly Clothing BrownGloves = new Clothing(30, Resources.Load<Texture2D>("Models/Character/Textures/Gloves_Brown"), TextDatabase.brownGloves, Type.Gloves);
    public static readonly Clothing BrownEye = new Clothing(40, Resources.Load<Texture2D>("Models/Character/Textures/Skin_Base"), TextDatabase.BrownEyes, Type.Eyes);
    private Clothing cloth;

    //Getter/Setter
    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public Texture2D Texture
    {
        get { return this.texture; }
    }

    public Text Description
    {
        get { return this.description; }
        set { this.description = value; }
    }

    public Type Types
    {
        get { return this.type; }
        set { this.type = value; }
    }
}

public class Skin
{
    private Clothing beard;

    private Clothing hair;

    private Clothing body;
    private Clothing pant;
    private Clothing tshirt;
    private Clothing gloves;
    private Clothing eyes;

    // Constructeur
    public Skin()
    {
        this.beard = new Clothing();
        this.hair = new Clothing();
        this.body = new Clothing();
        this.pant = new Clothing();
        this.tshirt = new Clothing();
        this.gloves = new Clothing();
        this.eyes = new Clothing();
    }

    public Skin(Clothing beard, Clothing hair, Clothing body, Clothing pant, Clothing tshirt, Clothing gloves, Clothing eyes)
    {
        this.beard = beard;
        this.hair = hair;
        this.body = body;
        this.pant = pant;
        this.tshirt = tshirt;
        this.gloves = gloves;
        this.eyes = eyes;
    }

    // Method
    public void Apply(GameObject character)
    {
        character.GetComponent<SyncCharacter>().ChangeBeard(this.beard.Texture);
        character.GetComponent<SyncCharacter>().ChangeBeard(this.hair.Texture);
        Texture2D bodyTexture = Clothing.MergeSkin(this.body, this.pant, this.tshirt, this.gloves, this.eyes);
        character.GetComponent<SyncCharacter>().ChangeBeard(bodyTexture);
    }

    /// <summary>
    /// Parse a skin to string.
    /// </summary>
    /// <param name="skin"></param>
    /// <returns></returns>
    public static string Save(Skin skin)
    {
        string save = 
            skin.Beard.ID.ToString() + ":" +
            skin.Hair.ID.ToString() + ":" +
            skin.Body.ID.ToString() + ":" +
            skin.Pant.ID.ToString() + ":" +
            skin.Tshirt.ID.ToString() + ":" +
            skin.Gloves.ID.ToString() + ":" +
            skin.Eyes.ID.ToString();
        return save;
    }

    /// <summary>
    /// Parse a string to a skin.
    /// </summary>
    /// <param name="save"></param>
    /// <returns></returns>
    public static Skin Load(string save)
    {
        string[] skin = save.Split(':');
        Clothing beard = Clothing.Find(int.Parse(skin[0]));
        Clothing hair = Clothing.Find(int.Parse(skin[1]));
        Clothing body = Clothing.Find(int.Parse(skin[2]));
        Clothing pant = Clothing.Find(int.Parse(skin[3]));
        Clothing tshirt = Clothing.Find(int.Parse(skin[4]));
        Clothing gloves = Clothing.Find(int.Parse(skin[5]));
        Clothing eyes = Clothing.Find(int.Parse(skin[6]));

        return new Skin(beard, hair, body, pant, tshirt, gloves, eyes);
    }

    // Getter/Seter
    public Clothing Beard
    {
        get { return this.beard; }
        set { this.beard = value; }
    }

    public Clothing Hair
    {
        get { return this.hair; }
        set { this.hair = value; }
    }

    public Clothing Body
    {
        get { return this.body; }
        set { this.body = value; }
    }

    public Clothing Pant
    {
        get { return this.pant; }
        set { this.pant = value; }
    }

    public Clothing Tshirt
    {
        get { return this.tshirt; }
        set { this.tshirt = value; }
    }

    public Clothing Gloves
    {
        get { return this.gloves; }
        set { this.hair = value; }
    }

    public Clothing Eyes
    {
        get { return this.eyes; }
        set { this.eyes = value; }
    }
}