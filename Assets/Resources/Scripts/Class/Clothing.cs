using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clothing
{
    private int id;
    private Texture2D texture;
    private Text description;



    // Constructeur 
    public Clothing()
    {
        this.id = -1;
        this.texture = null;
        this.description = new Text();
    }

    public Clothing(int id, Texture2D texture, Text description)
    {
        this.id = id;
        if (texture.width != 512 || texture.height != 512)
            throw new System.Exception("Image mauvaise taille");
        this.texture = texture;
        this.description = description;
    }

    public Clothing(Clothing cloth)
    {
        this.id = cloth.id;
        this.texture = cloth.texture;
        this.description = cloth.description;
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

            foreach (Hair hair in Hairs)
                yield return hair;

            foreach (Beard beard in Beards)
                yield return beard;

            foreach (Body body in Bodies)
                yield return body;

            foreach (Pant pant in Pants)
                yield return pant;

            foreach (Tshirt tshirt in Tshirts)
                yield return tshirt;

            foreach (Gloves gloves in Glovess)
                yield return gloves;

            foreach (Eyes eyes in Eyess)
                yield return eyes;

        }
    }

    public static IEnumerable<Hair> Hairs
    {
        get
        {
            yield return new Hair();
        }
    }
    public static IEnumerable<Beard> Beards
    {
        get
        {
            yield return new Beard();
        }
    }
    public static IEnumerable<Body> Bodies
    {
        get
        {
            yield return WhiteSkin;
        }
    }
    public static IEnumerable<Pant> Pants
    {
        get
        {
            yield return BrownPant;
        }
    }
    public static IEnumerable<Tshirt> Tshirts
    {
        get
        {
            yield return new Tshirt();
        }
    }
    public static IEnumerable<Gloves> Glovess
    {
        get
        {
            yield return BrownGloves;
        }
    }
    public static IEnumerable<Eyes> Eyess
    {
        get
        {
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
                if (cloth is Hair)
                    return new Hair((Hair)cloth);
                else if (cloth is Beard)
                    return new Beard((Beard)cloth);
                else if (cloth is Body)
                    return new Body((Body)cloth);
                else if (cloth is Pant)
                    return new Pant((Pant)cloth);
                else if (cloth is Tshirt)
                    return new Tshirt((Tshirt)cloth);
                else if (cloth is Gloves)
                    return new Gloves((Gloves)cloth);
                else if (cloth is Eyes)
                    return new Eyes((Eyes)cloth);
                else
                    return new Clothing(cloth);
        }
        throw new System.Exception("Clothing.Find : Clothing not find");
    }

    //Skin
    public static readonly Body WhiteSkin = new Body(10, Resources.Load<Texture2D>("Models/Character/Textures/Body_White"), TextDatabase.WhiteSkin, new Color(145, 91, 55));
    public static readonly Pant BrownPant = new Pant(20, Resources.Load<Texture2D>("Models/Character/Textures/Skin_Base"), TextDatabase.brownPant, new Color(128, 64, 0));
    public static readonly Gloves BrownGloves = new Gloves(30, Resources.Load<Texture2D>("Models/Character/Textures/Gloves_Brown"), TextDatabase.brownGloves, new Color(128, 64, 0));
    public static readonly Eyes BrownEye = new Eyes(40, Resources.Load<Texture2D>("Models/Character/Textures/Skin_Base"), TextDatabase.BrownEyes, new Color(128, 64, 0));
   
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
}

public class Skin
{
    private Beard beard;

    private Hair hair;

    private Body body;
    private Pant pant;
    private Tshirt tshirt;
    private Gloves gloves;
    private Eyes eyes;

    // Constructeur
    public Skin()
    {
        this.beard = new Beard();
        this.hair = new Hair();
        this.body = new Body();
        this.pant = new Pant();
        this.tshirt = new Tshirt();
        this.gloves = new Gloves();
        this.eyes = new Eyes();
    }

    public Skin(Beard beard, Hair hair, Body body, Pant pant, Tshirt tshirt, Gloves gloves, Eyes eyes)
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
        Beard beard = Clothing.Find(int.Parse(skin[0])) as Beard;
        Hair hair = Clothing.Find(int.Parse(skin[1])) as Hair;
        Body body = Clothing.Find(int.Parse(skin[2])) as Body;
        Pant pant = Clothing.Find(int.Parse(skin[3])) as Pant;
        Tshirt tshirt = Clothing.Find(int.Parse(skin[4])) as Tshirt;
        Gloves gloves = Clothing.Find(int.Parse(skin[5])) as Gloves;
        Eyes eyes = Clothing.Find(int.Parse(skin[6])) as Eyes;

        return new Skin(beard, hair, body, pant, tshirt, gloves, eyes);
    }

    // Getter/Seter
    public Beard Beard
    {
        get { return this.beard; }
        set { this.beard = value; }
    }

    public Hair Hair
    {
        get { return this.hair; }
        set { this.hair = value; }
    }

    public Body Body
    {
        get { return this.body; }
        set { this.body = value; }
    }

    public Pant Pant
    {
        get { return this.pant; }
        set { this.pant = value; }
    }

    public Tshirt Tshirt
    {
        get { return this.tshirt; }
        set { this.tshirt = value; }
    }

    public Gloves Gloves
    {
        get { return this.gloves; }
        set { this.gloves = value; }
    }

    public Eyes Eyes
    {
        get { return this.eyes; }
        set { this.eyes = value; }
    }
}

public class Hair : Clothing
{
    private Color color;
    private TypeHair type;

    public enum TypeHair { None, Hair };

    // Constructeur
    public Hair() : base()
    {
        this.color = new Color();
        this.type = TypeHair.None;
    }

    public Hair(int id, Texture2D texture, Text description, Color color, TypeHair type) : base(id, texture, description)
    {
        this.color = color;
        this.type = type;
    }

    public Hair(Hair hair) : base(hair)
    {
        this.color = hair.color;
        this.type = hair.type;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }

    public TypeHair GetTypeHair
    {
        get { return this.type; }
        set { this.type = value; }
    }
}

public class Beard : Clothing
{
    private Color color;
    private TypeBeard type;

    public enum TypeBeard { None, Hair };

    // Constructeur
    public Beard() : base()
    {
        this.color = new Color();
        this.type = TypeBeard.None;
    }

    public Beard(int id, Texture2D texture, Text description, Color color, TypeBeard type) : base(id, texture, description)
    {
        this.color = color;
        this.type = type;
    }

    public Beard(Beard beard) : base(beard)
    {
        this.color = beard.color;
        this.type = beard.type;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }

    public TypeBeard GetTypeBeard
    {
        get { return this.type; }
        set { this.type = value; }
    }
}


public class Body : Clothing
{
    private Color color;

    // Constructeur
    public Body() : base()
    {
        this.color = new Color();
    }

    public Body(int id, Texture2D texture, Text description, Color color) : base(id, texture, description)
    {
        this.color = color;
    }

    public Body(Body body) : base(body)
    {
        this.color = body.color;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }
}

public class Pant : Clothing
{
    private Color color;

    // Constructeur
    public Pant() : base()
    {
        this.color = new Color();
    }

    public Pant(int id, Texture2D texture, Text description, Color color) : base(id, texture, description)
    {
        this.color = color;
    }

    public Pant(Pant pant) : base(pant)
    {
        this.color = pant.color;
    }

    // Getter/Setter 
    public Color32 Color
    {
        get { return this.color; }
        set { this.color = value; }
    }
}

public class Tshirt : Clothing
{
    private Color32 color;
    private TypeTshirt type;

    public enum TypeTshirt { None, Hair };

    // Constructeur
    public Tshirt() : base()
    {
        this.color = new Color();
        this.type = TypeTshirt.None;
    }

    public Tshirt(int id, Texture2D texture, Text description, Color color, TypeTshirt type) : base(id, texture, description)
    {
        this.color = color;
        this.type = type;
    }

    public Tshirt(Tshirt tshirt) : base(tshirt)
    {
        this.color = tshirt.color;
        this.type = tshirt.type;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }

    public TypeTshirt GetTypeTshirt
    {
        get { return this.type; }
        set { this.type = value; }
    }
}

public class Gloves : Clothing
{
    private Color color;

    // Constructeur
    public Gloves() : base()
    {
        this.color = new Color();
    }

    public Gloves(int id, Texture2D texture, Text description, Color color) : base(id, texture, description)
    {
        this.color = color;
    }

    public Gloves(Gloves gloves) : base(gloves)
    {
        this.color = gloves.color;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }
}

public class Eyes : Clothing
{
    private Color color;

    // Constructeur
    public Eyes() : base()
    {
        this.color = new Color();
    }

    public Eyes(int id, Texture2D texture, Text description, Color color) : base(id, texture, description)
    {
        this.color = color;
    }

    public Eyes(Eyes eyes) : base(eyes)
    {
        this.color = eyes.color;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }
}
