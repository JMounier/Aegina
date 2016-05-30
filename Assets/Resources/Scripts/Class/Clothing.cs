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
        int a = 0;
        Texture2D newCloth = new Texture2D(skin[0].texture.width, skin[0].texture.height);
        foreach (Clothing cloth in skin)
        {
            Debug.Log(a);
            Debug.Log(cloth.Description.GetText());
            if (newCloth.width != cloth.texture.width || newCloth.height != cloth.texture.height)
                throw new System.Exception("Merging texture error, not same size : [" + skin[0].texture.name + " | " + cloth.texture.name + "]");
            for (int i = 0; i < newCloth.width; i++)
                for (int j = 0; j < newCloth.height; j++)
                {
                    Color pixel = cloth.texture.GetPixel(i, j);
                    if (pixel.a != 0)
                        newCloth.SetPixel(i, j, pixel);
                }
            a++;
        }
        newCloth.Apply();
        return newCloth;
    }
    // Method
    public static IEnumerable<Clothing> Clothings
    {
        get
        {
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

            foreach (Gloves gloves in Gloves)
                yield return gloves;

            foreach (Eyes eyes in Eyes)
                yield return eyes;

            foreach (Hat hat in Hats)
                yield return hat;
        }
    }

    public static IEnumerable<Hair> Hairs
    {
        get
        {
            yield return GreenHair;
        }
    }
    public static IEnumerable<Beard> Beards
    {
        get
        {
            yield return NoneBeard; 
            yield return PurpleBeard;
            yield return BrownBeard;
        }
    }
    public static IEnumerable<Body> Bodies
    {
        get
        {
            yield return WhiteBody;
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
            yield return NoneTshirt;
        }
    }
    public static IEnumerable<Gloves> Gloves
    {
        get
        {
            yield return BrownGloves;
        }
    }
    public static IEnumerable<Eyes> Eyes
    {
        get
        {
            yield return BlackEye;
            yield return RedEye;
            yield return BlueEye;
            yield return GreenEye;
        }
    }

    public static IEnumerable<Hat> Hats
    {
        get
        {
            yield return NoneHat;
            yield return AmericanTopHat;
            yield return BlackTopHat;
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
                else if (cloth is Hat)
                    return new Hat((Hat)cloth);
                else
                    return new Clothing(cloth);
        }
        Debug.Log(id.ToString());
        throw new System.Exception("Clothing.Find : Clothing not find");
    }

    //Skin
    public static readonly Body WhiteBody = new Body(10, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_White"), TextDatabase.WhiteBody, new Color(145f, 91f, 55f));

    public static readonly Pant BrownPant = new Pant(20, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Pants_Brown"), TextDatabase.BrownPant, new Color(128f, 64f, 0f));

    public static readonly Gloves BrownGloves = new Gloves(30, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_Brown"), TextDatabase.BrownGloves, new Color(128f, 64f, 0f));

    public static readonly Eyes BlackEye = new Eyes(40, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Black"), TextDatabase.BlackEyes, new Color(0f, 0f, 0f));
    public static readonly Eyes GreenEye = new Eyes(41, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Green"), TextDatabase.GreenEyes, new Color(.05f, .447f, .278f));
    public static readonly Eyes RedEye = new Eyes(42, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Red"), TextDatabase.RedEyes, new Color(.392f, .149f, .0f));
    public static readonly Eyes BlueEye = new Eyes(43, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Blue"), TextDatabase.BlueEyes, new Color(.145f, .454f, .584f));

    public static readonly Hair GreenHair = new Hair(50, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Green"), TextDatabase.GreenHair, Color.green, Hair.TypeHair.Hair);
    
    public static readonly Beard NoneBeard = new Beard(60, TextDatabase.NoneBeard);
    public static readonly Beard PurpleBeard = new Beard(61, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Purple"), TextDatabase.PurpleBeard, new Color( .368f, .211f,.419f), Beard.TypeBeard.Beard);
    public static readonly Beard BrownBeard = new Beard(62, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Brown"), TextDatabase.BrownBeard, new Color(.207f, .019f, .003f), Beard.TypeBeard.Beard);
    

    public static readonly Tshirt NoneTshirt = new Tshirt(70, TextDatabase.PurpleBeard);

    public static readonly Hat NoneHat = new Hat(80, TextDatabase.NoneHat);
    public static readonly Hat AmericanTopHat = new Hat(81, Resources.Load<Texture2D>("Models/Character/Cosmetics/Textures/HautTextAMERICA"), TextDatabase.AmericanTopHat, Color.blue, Hat.TypeHat.TopHat);
    public static readonly Hat BlackTopHat = new Hat(82, Resources.Load<Texture2D>("Models/Character/Cosmetics/Textures/HautTextBlack"), TextDatabase.BlackTopHat, Color.black, Hat.TypeHat.TopHat);

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
    }
}

public class Skin
{
    private Beard beard;
    private Hair hair;
    private Hat hat;
    private Body body;
    private Pant pant;
    private Tshirt tshirt;
    private Gloves gloves;
    private Eyes eyes;
    private bool beardApplied, hairApplied, hatApplied, bodyApplied;

    // Constructeur
    public Skin()
    {
        this.beard = new Beard();
        this.hair = new Hair();
        this.hat = new Hat();
        this.body = new Body();
        this.pant = new Pant();
        this.tshirt = new Tshirt();
        this.gloves = new Gloves();
        this.eyes = new Eyes();
    }

    public Skin(Beard beard, Hair hair, Hat hat, Body body, Pant pant, Tshirt tshirt, Gloves gloves, Eyes eyes)
    {
        this.beard = beard;
        this.hair = hair;
        this.hat = hat;
        this.body = body;
        this.pant = pant;
        this.tshirt = tshirt;
        this.gloves = gloves;
        this.eyes = eyes;
    }

    // Method
    public void Apply(GameObject character)
    {
        if (!this.beardApplied)
            ChangeBeard(this.beard, character);
        if (!this.hairApplied)
            ChangeHair(this.hair, character);
        if (!this.hatApplied)
            ChangeHat(this.hat, character);

        List<Clothing> merge = new List<Clothing>();
        merge.Add(this.body);
        merge.Add(this.pant);
        if (tshirt.GetTypeTshirt == Tshirt.TypeTshirt.Tshhirt)
            merge.Add(this.tshirt);
        merge.Add(gloves);
        merge.Add(eyes);

        Texture2D bodyTexture = Clothing.MergeSkin(merge.ToArray());
        if (!this.bodyApplied)
            ChangeBody(bodyTexture, character);
        this.beardApplied = true;
        this.hairApplied = true;
        this.hatApplied = true;
        this.bodyApplied = true;
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
            skin.Eyes.ID.ToString() + ":" +
            skin.hat.ID.ToString();
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
        Hat hat = Clothing.Find(int.Parse(skin[7])) as Hat;

        return new Skin(beard, hair, hat, body, pant, tshirt, gloves, eyes);
    }
    public static void ChangeHat(Hat hat, GameObject character)
    {
        switch (hat.GetTypeHat)
        {
            case Hat.TypeHat.None:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("HautForm").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("StrawHat").gameObject.SetActive(false);
                break;
            case Hat.TypeHat.TopHat:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("HautForm").gameObject.SetActive(true);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("StrawHat").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("HautForm").GetComponentInChildren<Renderer>().material.mainTexture = hat.Texture;
                break;
            case Hat.TypeHat.StrawHat:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("HautForm").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("StrawHat").gameObject.SetActive(true);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("StrawHat").GetComponentInChildren<Renderer>().material.mainTexture = hat.Texture;
                break;
            case Hat.TypeHat.Cowboy:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("HautForm").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat").FindChild("StrawHat").gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    
    public void ChangeBeard(Beard beard, GameObject character)
    {
        switch (beard.GetTypeBeard)
        {
            case Beard.TypeBeard.None:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("NPC_Beard_008").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("Moustach").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BeardMoustachSplit").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BearsOnly").gameObject.SetActive(false);
                break;
            case Beard.TypeBeard.Beard:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("NPC_Beard_008").gameObject.SetActive(true);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("Moustach").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BeardMoustachSplit").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BearsOnly").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("NPC_Beard_008").GetComponent<Renderer>().material.mainTexture = Beard.Texture;
                break;
            case Beard.TypeBeard.BeardMoustachSplit:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("NPC_Beard_008").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("Moustach").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BeardMoustachSplit").gameObject.SetActive(true);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BearsOnly").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BeardMoustachSplit").GetComponent<Renderer>().material.mainTexture = Beard.Texture;
                break;
            case Beard.TypeBeard.BearsOnly:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("NPC_Beard_008").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("Moustach").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BeardMoustachSplit").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BearsOnly").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BearsOnly").GetComponent<Renderer>().material.mainTexture = Beard.Texture;
                break;
            case Beard.TypeBeard.Moustach:
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("NPC_Beard_008").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("Moustach").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BeardMoustachSplit").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("BearsOnly").gameObject.SetActive(false);
                character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard").FindChild("Moustache").GetComponent<Renderer>().material.mainTexture = Beard.Texture;
                break;
            default:
                break;   
        }
    }

    public static void ChangeHair(Hair hair, GameObject character)
    {
        if (hair.GetTypeHair == Hair.TypeHair.None)
        {
            character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair").FindChild("NPC_Hair_009").gameObject.SetActive(false);
            character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair").FindChild("Crete").gameObject.SetActive(false);
            character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair").FindChild("Mesh").gameObject.SetActive(false);
            character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair").FindChild("LongHair").gameObject.SetActive(false);
        }
        else
        {
            character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair").FindChild("NPC_Hair_009").gameObject.SetActive(true);
            character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair").FindChild("NPC_Hair_009").GetComponentInChildren<Renderer>().material.mainTexture = hair.Texture;
        }
    }

    public void ChangeBody(Texture2D skin, GameObject character)
    {
        character.transform.FindChild("Character").FindChild("NPC_Man_Normal001").GetComponentInChildren<Renderer>().material.mainTexture = skin;
    }

    // Getter/Seter
    public Beard Beard
    {
        get { return this.beard; }
        set
        {
            this.beardApplied = false;
            this.beard = value;
        }
    }

    public Hair Hair
    {
        get { return this.hair; }
        set
        {
            this.hairApplied = false;
            this.hair = value;
        }
    }

    public Body Body
    {
        get { return this.body; }
        set
        {
            this.bodyApplied = false;
            this.body = value;
        }
    }

    public Pant Pant
    {
        get { return this.pant; }
        set
        {
            this.bodyApplied = false;
            this.pant = value;
        }
    }

    public Tshirt Tshirt
    {
        get { return this.tshirt; }
        set
        {
            this.bodyApplied = false;
            this.tshirt = value;
        }
    }

    public Gloves Gloves
    {
        get { return this.gloves; }
        set
        {
            this.bodyApplied = false;
            this.gloves = value;
        }
    }

    public Eyes Eyes
    {
        get { return this.eyes; }
        set
        {
            this.bodyApplied = false;
            this.eyes = value;
        }
    }

    public Hat Hat
    {
        get { return this.hat; }
        set
        {
            this.hatApplied = false;
            this.hat = value;
        }
    }
}

public class Hair : Clothing
{
    private Color color;
    private TypeHair type;

    public enum TypeHair { None, Hair, Crete, LongHair, Mesh };

    // Constructeur
    public Hair() : base()
    {
        this.color = new Color();
        this.type = TypeHair.None;
    }

    public Hair(int id, Text description) : base(id, null, description)
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

    public enum TypeBeard { None, Beard, BearsOnly, BeardMoustachSplit, Moustach };

    // Constructeur
    public Beard() : base()
    {
        this.color = new Color();
        this.type = TypeBeard.None;
    }

    public Beard(int id, Text description) : base(id, null, description)
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

    public enum TypeTshirt { None, Tshhirt };

    // Constructeur
    public Tshirt() : base()
    {
        this.color = new Color();
        this.type = TypeTshirt.None;
    }

    public Tshirt(int id, Text description) : base(id, null, description)
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
public class Hat : Clothing
{
    private Color32 color;
    private TypeHat type;

    public enum TypeHat { None, TopHat, StrawHat, Cowboy };

    // Constructeur
    public Hat() : base()
    {
        this.color = new Color();
        this.type = TypeHat.None;
    }

    public Hat(int id, Text description) : base(id, null, description)
    {
        this.color = new Color();
        this.type = TypeHat.None;
    }

    public Hat(int id, Texture2D texture, Text description, Color color, TypeHat type) : base(id, texture, description)
    {
        this.color = color;
        this.type = type;
    }

    public Hat(Hat hat) : base(hat)
    {
        this.color = hat.color;
        this.type = hat.type;
    }

    // Getter/Setter 
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }

    public TypeHat GetTypeHat
    {
        get { return this.type; }
        set { this.type = value; }
    }
}