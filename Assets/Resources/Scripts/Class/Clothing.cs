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

            foreach (Tshirt tshirt in Tshirts)
                yield return tshirt;

            foreach (Pant pant in Pants)
                yield return pant;

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
            yield return NoneHair;

            yield return NormalBrownHair;
            yield return NormalBlackHair;
            yield return NormalBlondHair;
            yield return NormalRedHair;
            yield return NormalWhiteHair;

            yield return LongBrownHair;
            yield return LongBlackHair;
            yield return LongBlondHair;
            yield return LongRedHair;
            yield return LongWhiteHair;

            yield return MecheBrownHair;
            yield return MecheBlackHair;
            yield return MecheBlondHair;
            yield return MecheRedHair;
            yield return MecheWhiteHair;

            yield return CreteBrownHair;
            yield return CreteBlackHair;
            yield return CreteBlondHair;
            yield return CreteRedHair;
            yield return CreteWhiteHair;
        }
    }
    public static IEnumerable<Beard> Beards
    {
        get
        {
            yield return NoneBeard;

            yield return BeardMoustachSplitBlackBeard;
            yield return BeardMoustachSplitBlondBeard;
            yield return BeardMoustachSplitBrownBeard;
            yield return BeardMoustachSplitRedBeard;
            yield return BeardMoustachSplitWhiteBeard;

            yield return BeardOnlyBlackBeard;
            yield return BeardOnlyBlondBeard;
            yield return BeardOnlyBrownBeard;
            yield return BeardOnlyRedBeard;
            yield return BeardOnlyWhiteBeard;

            yield return MoustachBlackBeard;
            yield return MoustachBlondBeard;
            yield return MoustachBrownBeard;
            yield return MoustachRedBeard;
            yield return MoustachWhiteBeard;

            yield return NormalBlackBeard;
            yield return NormalBlondBeard;
            yield return NormalBrownBeard;
            yield return NormalRedBeard;
            yield return NormalWhiteBeard;

        }
    }
    public static IEnumerable<Body> Bodies
    {
        get
        {
            yield return WhiteBody;
            yield return BasicBody;
            yield return DarkBody;
            yield return BlackBody;
            yield return AlienBody;
            yield return AquaBody;
        }
    }
    public static IEnumerable<Pant> Pants
    {
        get
        {
            yield return BlackOveralls;
            yield return BlueOveralls;
            yield return BrownOveralls;
            yield return GreenOveralls;
            yield return RedOveralls;
            yield return WhiteOveralls;
            yield return BrownPant;
            yield return WhitePant;
        }
    }
    public static IEnumerable<Tshirt> Tshirts
    {
        get
        {
            yield return NoneTshirt;
            yield return RedTshirt;
        }
    }
    public static IEnumerable<Gloves> Gloves
    {
        get
        {
            yield return BrownGloves;
            yield return WhiteGloves;
            yield return RedGloves;
            yield return PurpleGloves;
            yield return GreenGloves;
            yield return BlueGloves;
        }
    }
    public static IEnumerable<Eyes> Eyes
    {
        get
        {
            yield return BlackEye;
            yield return BrownEye;
            yield return BlueEye;
            yield return GreenEye;
            yield return RedEye;
            yield return OrangeEye;
        }
    }

    public static IEnumerable<Hat> Hats
    {
        get
        {
            yield return NoneHat;
            yield return AmericanTopHat;
            yield return BlackTopHat;
            yield return RedStrawHat;
            yield return WhiteStrawHat;
            yield return YellowStrawHat;
            yield return PurpleStrawHat;
            yield return BlackStrawHat;
            yield return BrownCowboy;
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
                else if (cloth is Gloves)
                    return new Gloves((Gloves)cloth);
                else if (cloth is Tshirt)
                    return new Tshirt((Tshirt)cloth);
                else if (cloth is Eyes)
                    return new Eyes((Eyes)cloth);
                else if (cloth is Hat)
                    return new Hat((Hat)cloth);
                else
                    return new Clothing(cloth);
        }
        throw new System.Exception("Clothing.Find : Clothing not find");
    }

    //Skin
    public static readonly Body WhiteBody = new Body(10, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_White"), TextDatabase.WhiteBody, new Color(.815f, .6f, .388f));
    public static readonly Body BasicBody = new Body(11, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_Basic"), TextDatabase.BasicBody, new Color(.588f, .368f, .223f));
    public static readonly Body DarkBody = new Body(12, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_Dark"), TextDatabase.DarkBody, new Color(.392f, .196f, .105f));
    public static readonly Body BlackBody = new Body(13, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_Black"), TextDatabase.BlackBody, new Color(.086f, 0f, 0f));
    public static readonly Body AlienBody = new Body(14, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_Alien"), TextDatabase.AlienBody, new Color(.341f, .525f, .184f));
    public static readonly Body AquaBody = new Body(15, Resources.Load<Texture2D>("Models/Character/Textures/Bodies/Body_Aqua"), TextDatabase.AquaBody, new Color(.290f, .439f, .384f));

    public static readonly Pant BrownOveralls = new Pant(20, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Overalls_Brown"), TextDatabase.BrownOveralls, new Color(.188f, .074f, .02f), Pant.TypePant.Overalls);
    public static readonly Pant BlueOveralls = new Pant(21, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Overalls_Blue"), TextDatabase.BlueOveralls, new Color(.02f, .047f, .188f), Pant.TypePant.Overalls);
    public static readonly Pant RedOveralls = new Pant(22, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Overalls_Red"), TextDatabase.RedOveralls, new Color(.203f, 0f, .059f), Pant.TypePant.Overalls);
    public static readonly Pant GreenOveralls = new Pant(23, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Overalls_Green"), TextDatabase.GreenOveralls, new Color(.157f, .188f, .019f), Pant.TypePant.Overalls);
    public static readonly Pant BlackOveralls = new Pant(24, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Overalls_Black"), TextDatabase.BlackOveralls, new Color(.043f, .043f, .043f), Pant.TypePant.Overalls);
    public static readonly Pant WhiteOveralls = new Pant(25, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Overalls_White"), TextDatabase.WhiteOveralls, new Color(.525f, .525f, .525f), Pant.TypePant.Overalls);
    public static readonly Pant BrownPant = new Pant(26, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Pant_Brown"), TextDatabase.BrownPant, new Color(.188f, .074f, .02f), Pant.TypePant.Pant);
    public static readonly Pant WhitePant = new Pant(27, Resources.Load<Texture2D>("Models/Character/Textures/Pants/Pant_White"), TextDatabase.WhitePant, new Color(.525f, .525f, .525f), Pant.TypePant.Pant);

    public static readonly Eyes BlackEye = new Eyes(40, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Black"), TextDatabase.BlackEyes, new Color(0f, 0f, 0f));
    public static readonly Eyes GreenEye = new Eyes(41, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Green"), TextDatabase.GreenEyes, new Color(.026f, .525f, .314f));
    public static readonly Eyes BrownEye = new Eyes(42, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Brown"), TextDatabase.BrwonEyes, new Color(.490f, .309f, .133f));
    public static readonly Eyes BlueEye = new Eyes(43, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Blue"), TextDatabase.BlueEyes, new Color(.145f, .231f, .666f));
    public static readonly Eyes RedEye = new Eyes(44, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Red"), TextDatabase.RedEyes, new Color(.890f, .196f, .193f));
    public static readonly Eyes OrangeEye = new Eyes(45, Resources.Load<Texture2D>("Models/Character/Textures/Eyes/Eyes_Orange"), TextDatabase.OrangeEyes, new Color(.949f, .580f, .110f));

    public static readonly Hair NoneHair = new Hair(50, TextDatabase.NoneHair);
    public static readonly Hair NormalBlackHair = new Hair(51, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Black"), TextDatabase.BlackHair, Color.black, Hair.TypeHair.Normal);
    public static readonly Hair NormalBrownHair = new Hair(52, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Brown"), TextDatabase.BrownHair, new Color(.337f, .078f, .031f), Hair.TypeHair.Normal);
    public static readonly Hair NormalRedHair = new Hair(53, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Red"), TextDatabase.RedHair, new Color(.333f, .113f, .016f), Hair.TypeHair.Normal);
    public static readonly Hair NormalBlondHair = new Hair(54, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Blond"), TextDatabase.BlondHair, new Color(.627f, .494f, 0f), Hair.TypeHair.Normal);
    public static readonly Hair NormalWhiteHair = new Hair(55, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_White"), TextDatabase.WhiteHair, new Color(.514f, .514f, .514f), Hair.TypeHair.Normal);
    public static readonly Hair CreteBlackHair = new Hair(56, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Black"), TextDatabase.BlackHair, Color.black, Hair.TypeHair.Crete);
    public static readonly Hair CreteBrownHair = new Hair(57, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Brown"), TextDatabase.BrownHair, new Color(.337f, .078f, .031f), Hair.TypeHair.Crete);
    public static readonly Hair CreteRedHair = new Hair(58, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Red"), TextDatabase.RedHair, new Color(.333f, .113f, .016f), Hair.TypeHair.Crete);
    public static readonly Hair CreteBlondHair = new Hair(59, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Blond"), TextDatabase.BlondHair, new Color(.627f, .494f, 0f), Hair.TypeHair.Crete);
    public static readonly Hair CreteWhiteHair = new Hair(60, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_White"), TextDatabase.WhiteHair, new Color(.514f, .514f, .514f), Hair.TypeHair.Crete);
    public static readonly Hair LongBlackHair = new Hair(61, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Black"), TextDatabase.BlackHair, Color.black, Hair.TypeHair.LongHair);
    public static readonly Hair LongBrownHair = new Hair(62, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Brown"), TextDatabase.BrownHair, new Color(.337f, .078f, .031f), Hair.TypeHair.LongHair);
    public static readonly Hair LongRedHair = new Hair(63, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Red"), TextDatabase.RedHair, new Color(.333f, .113f, .016f), Hair.TypeHair.LongHair);
    public static readonly Hair LongBlondHair = new Hair(64, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Blond"), TextDatabase.BlondHair, new Color(.627f, .494f, 0f), Hair.TypeHair.LongHair);
    public static readonly Hair LongWhiteHair = new Hair(65, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_White"), TextDatabase.WhiteHair, new Color(.514f, .514f, .514f), Hair.TypeHair.LongHair);
    public static readonly Hair MecheBlackHair = new Hair(66, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Black"), TextDatabase.BlackHair, Color.black, Hair.TypeHair.Meche);
    public static readonly Hair MecheBrownHair = new Hair(67, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Brown"), TextDatabase.BrownHair, new Color(.337f, .078f, .031f), Hair.TypeHair.Meche);
    public static readonly Hair MecheRedHair = new Hair(68, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Red"), TextDatabase.RedHair, new Color(.333f, .113f, .016f), Hair.TypeHair.Meche);
    public static readonly Hair MecheBlondHair = new Hair(69, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_Blond"), TextDatabase.BlondHair, new Color(.627f, .494f, 0f), Hair.TypeHair.Meche);
    public static readonly Hair MecheWhiteHair = new Hair(70, Resources.Load<Texture2D>("Models/Character/Textures/Hairs/Hair_White"), TextDatabase.WhiteHair, new Color(.514f, .514f, .514f), Hair.TypeHair.Meche);

    public static readonly Hat NoneHat = new Hat(80, TextDatabase.NoneHat);
    public static readonly Hat AmericanTopHat = new Hat(81, Resources.Load<Texture2D>("Models/Character/Textures/Hat/TopAmerica"), TextDatabase.AmericanTopHat, Color.blue, Hat.TypeHat.TopHat);
    public static readonly Hat BlackTopHat = new Hat(82, Resources.Load<Texture2D>("Models/Character/Textures/Hat/TopBlack"), TextDatabase.BlackTopHat, Color.black, Hat.TypeHat.TopHat);
    public static readonly Hat RedStrawHat = new Hat(83, Resources.Load<Texture2D>("Models/Character/Textures/Hat/StrawRed"), TextDatabase.StrawRed, Color.red, Hat.TypeHat.StrawHat);
    public static readonly Hat BlackStrawHat = new Hat(84, Resources.Load<Texture2D>("Models/Character/Textures/Hat/StrawBlack"), TextDatabase.Strawblack, Color.black, Hat.TypeHat.StrawHat);
    public static readonly Hat WhiteStrawHat = new Hat(85, Resources.Load<Texture2D>("Models/Character/Textures/Hat/StrawWhite"), TextDatabase.StrawWhite, Color.white, Hat.TypeHat.StrawHat);
    public static readonly Hat YellowStrawHat = new Hat(86, Resources.Load<Texture2D>("Models/Character/Textures/Hat/StrawYellow"), TextDatabase.StrawYellow, Color.yellow, Hat.TypeHat.StrawHat);
    public static readonly Hat PurpleStrawHat = new Hat(87, Resources.Load<Texture2D>("Models/Character/Textures/Hat/StrawPurple"), TextDatabase.StrawPurple, new Color(.733f, .156f, .878f), Hat.TypeHat.StrawHat);
    public static readonly Hat BrownCowboy = new Hat(88, Resources.Load<Texture2D>("Models/Character/Textures/Hat/CowBoyBrown"), TextDatabase.CowBoyBrown, new Color(.439f, .251f, .164f), Hat.TypeHat.CowBoy);


    public static readonly Beard NoneBeard = new Beard(90, TextDatabase.NoneBeard);
    public static readonly Beard NormalBlackBeard = new Beard(91, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Black"), TextDatabase.BlackBeard, Color.black, Beard.TypeBeard.Beard);
    public static readonly Beard NormalBrownBeard = new Beard(92, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Brown"), TextDatabase.BrownBeard, new Color(.337f, .078f, .031f), Beard.TypeBeard.Beard);
    public static readonly Beard NormalRedBeard = new Beard(93, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Red"), TextDatabase.RedBeard, new Color(.333f, .113f, .016f), Beard.TypeBeard.Beard);
    public static readonly Beard NormalBlondBeard = new Beard(94, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Blond"), TextDatabase.BlondBeard, new Color(.627f, .494f, 0f), Beard.TypeBeard.Beard);
    public static readonly Beard NormalWhiteBeard = new Beard(95, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_White"), TextDatabase.WhiteBeard, new Color(.514f, .514f, .514f), Beard.TypeBeard.Beard);
    public static readonly Beard BeardMoustachSplitBlackBeard = new Beard(96, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Black"), TextDatabase.BlackBeard, Color.black, Beard.TypeBeard.BeardMoustachSplit);
    public static readonly Beard BeardMoustachSplitBrownBeard = new Beard(97, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Brown"), TextDatabase.BrownBeard, new Color(.337f, .078f, .031f), Beard.TypeBeard.BeardMoustachSplit);
    public static readonly Beard BeardMoustachSplitRedBeard = new Beard(98, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Red"), TextDatabase.RedBeard, new Color(.333f, .113f, .016f), Beard.TypeBeard.BeardMoustachSplit);
    public static readonly Beard BeardMoustachSplitBlondBeard = new Beard(99, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Blond"), TextDatabase.BlondBeard, new Color(.627f, .494f, 0f), Beard.TypeBeard.BeardMoustachSplit);
    public static readonly Beard BeardMoustachSplitWhiteBeard = new Beard(100, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_White"), TextDatabase.WhiteBeard, new Color(.514f, .514f, .514f), Beard.TypeBeard.BeardMoustachSplit);
    public static readonly Beard BeardOnlyBlackBeard = new Beard(101, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Black"), TextDatabase.BlackBeard, Color.black, Beard.TypeBeard.BeardOnly);
    public static readonly Beard BeardOnlyBrownBeard = new Beard(102, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Brown"), TextDatabase.BrownBeard, new Color(.337f, .078f, .031f), Beard.TypeBeard.BeardOnly);
    public static readonly Beard BeardOnlyRedBeard = new Beard(103, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Red"), TextDatabase.RedBeard, new Color(.333f, .113f, .016f), Beard.TypeBeard.BeardOnly);
    public static readonly Beard BeardOnlyBlondBeard = new Beard(104, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Blond"), TextDatabase.BlondBeard, new Color(.627f, .494f, 0f), Beard.TypeBeard.BeardOnly);
    public static readonly Beard BeardOnlyWhiteBeard = new Beard(105, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_White"), TextDatabase.WhiteBeard, new Color(.514f, .514f, .514f), Beard.TypeBeard.BeardOnly);
    public static readonly Beard MoustachBlackBeard = new Beard(106, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Black"), TextDatabase.BlackBeard, Color.black, Beard.TypeBeard.Moustach);
    public static readonly Beard MoustachBrownBeard = new Beard(107, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Brown"), TextDatabase.BrownBeard, new Color(.337f, .078f, .031f), Beard.TypeBeard.Moustach);
    public static readonly Beard MoustachRedBeard = new Beard(108, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Red"), TextDatabase.RedBeard, new Color(.333f, .113f, .016f), Beard.TypeBeard.Moustach);
    public static readonly Beard MoustachBlondBeard = new Beard(109, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_Blond"), TextDatabase.BlondBeard, new Color(.627f, .494f, 0f), Beard.TypeBeard.Moustach);
    public static readonly Beard MoustachWhiteBeard = new Beard(110, Resources.Load<Texture2D>("Models/Character/Textures/Beards/Beard_White"), TextDatabase.WhiteBeard, new Color(.514f, .514f, .514f), Beard.TypeBeard.Moustach);

    public static readonly Gloves BrownGloves = new Gloves(111, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_Brown"), TextDatabase.BrownGloves, new Color(.247f, .145f, .078f));
    public static readonly Gloves BlueGloves = new Gloves(112, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_Blue"), TextDatabase.BlueGloves, new Color(.047f, .117f, .176f));
    public static readonly Gloves WhiteGloves = new Gloves(113, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_White"), TextDatabase.WhiteGloves, new Color(.576f, .576f, .576f));
    public static readonly Gloves RedGloves = new Gloves(114, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_Red"), TextDatabase.RedGloves, new Color(.337f, .055f, .020f));
    public static readonly Gloves GreenGloves = new Gloves(115, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_Green"), TextDatabase.GreenGloves, new Color(.074f, .243f, .066f));
    public static readonly Gloves PurpleGloves = new Gloves(116, Resources.Load<Texture2D>("Models/Character/Textures/Gloves/Gloves_Purple"), TextDatabase.PurpleGloves, new Color(.227f, .066f, .243f));

    public static readonly Tshirt NoneTshirt = new Tshirt(120, TextDatabase.NoneTshirt);
    public static readonly Tshirt RedTshirt = new Tshirt(121, Resources.Load<Texture2D>("Models/Character/Textures/TShirts/Red"), TextDatabase.RedTshirt, Color.red, Tshirt.TypeTshirt.TShirt);

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
    private Tshirt tshirt;
    private Pant pant;
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
        this.tshirt = new Tshirt();
        this.pant = new Pant();
        this.gloves = new Gloves();
        this.eyes = new Eyes();
    }

    public Skin(Beard beard, Hair hair, Hat hat, Body body, Tshirt tshirt, Pant pant, Gloves gloves, Eyes eyes)
    {
        this.beard = beard;
        this.hair = hair;
        this.hat = hat;
        this.body = body;
        this.tshirt = tshirt;
        this.pant = pant;
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
        if (this.tshirt.GetTypeTshirt != Tshirt.TypeTshirt.None)
            merge.Add(this.tshirt);
        merge.Add(this.pant);
        merge.Add(this.gloves);
        merge.Add(this.eyes);
        if (!this.bodyApplied)
        {
            Texture2D bodyTexture = Clothing.MergeSkin(merge.ToArray());
            ChangeBody(bodyTexture, character);
        }
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
            skin.Tshirt.ID.ToString() + ":" +
            skin.Pant.ID.ToString() + ":" +
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
        Tshirt tshirt = Clothing.Find(int.Parse(skin[3])) as Tshirt;
        Pant pant = Clothing.Find(int.Parse(skin[4])) as Pant;
        Gloves gloves = Clothing.Find(int.Parse(skin[5])) as Gloves;
        Eyes eyes = Clothing.Find(int.Parse(skin[6])) as Eyes;
        Hat hat = Clothing.Find(int.Parse(skin[7])) as Hat;

        return new Skin(beard, hair, hat, body, tshirt, pant, gloves, eyes);
    }
    public static void ChangeHat(Hat hat, GameObject character)
    {
        foreach (Transform t in character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hat"))
        {
            if (t.gameObject.name == hat.GetTypeHat.ToString())
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = hat.Texture;
            }
            else
                t.gameObject.SetActive(false);
        }
    }

    public void ChangeBeard(Beard beard, GameObject character)
    {
        foreach (Transform t in character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Beard"))
        {
            if (t.gameObject.name == beard.GetTypeBeard.ToString())
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = beard.Texture;
            }
            else
                t.gameObject.SetActive(false);
        }
    }

    public static void ChangeHair(Hair hair, GameObject character)
    {
        foreach (Transform t in character.transform.FindChild("Character").FindChild("Armature").FindChild("Head_slot").FindChild("Hair"))
        {
            if (t.gameObject.name == hair.GetTypeHair.ToString())
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = hair.Texture;
            }
            else
                t.gameObject.SetActive(false);
        }
    }

    public void ChangeBody(Texture2D skin, GameObject character)
    {
        character.transform.FindChild("Character").FindChild("NPC_Man_Normal001").GetComponentInChildren<Renderer>().material.mainTexture = skin;
    }

    public void ForceApply(GameObject character)
    {
        this.beardApplied = false;
        this.hairApplied = false;
        this.hatApplied = false;
        this.bodyApplied = false;
        Apply(character);
    }

    // Getter/Seter
    public Beard Beard
    {
        get { return this.beard; }
        set
        {
            this.beardApplied = this.beard.ID == value.ID;
            this.beard = value;
        }
    }

    public Hair Hair
    {
        get { return this.hair; }
        set
        {
            this.hairApplied = this.hair.ID == value.ID;
            this.hair = value;
        }
    }

    public Body Body
    {
        get { return this.body; }
        set
        {
            this.bodyApplied = this.body.ID == value.ID;
            this.body = value;
        }
    }

    public Pant Pant
    {
        get { return this.pant; }
        set
        {
            this.bodyApplied = this.pant.ID == value.ID;
            this.pant = value;
        }
    }

    public Tshirt Tshirt
    {
        get { return this.tshirt; }
        set
        {
            this.bodyApplied = this.tshirt.ID == value.ID;
            this.tshirt = value;
        }
    }

    public Gloves Gloves
    {
        get { return this.gloves; }
        set
        {
            this.bodyApplied = this.gloves.ID == value.ID;
            this.gloves = value;
        }
    }

    public Eyes Eyes
    {
        get { return this.eyes; }
        set
        {
            this.bodyApplied = this.eyes.ID == value.ID;
            this.eyes = value;
        }
    }

    public Hat Hat
    {
        get { return this.hat; }
        set
        {
            this.hatApplied = this.hair.ID == value.ID;
            this.hat = value;
        }
    }
}

public class Hair : Clothing
{
    private Color color;
    private TypeHair type;

    public enum TypeHair { None, Normal, Crete, LongHair, Meche };

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

    public enum TypeBeard { None, Beard, BeardOnly, BeardMoustachSplit, Moustach };

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
    private TypePant type;
    public enum TypePant { Overalls, Pant };

    // Constructeur
    public Pant() : base()
    {
        this.color = new Color();
        this.type = TypePant.Overalls;
    }

    public Pant(int id, Texture2D texture, Text description, Color color, TypePant type) : base(id, texture, description)
    {
        this.color = color;
        this.type = type;
    }

    public Pant(Pant pant) : base(pant)
    {
        this.color = pant.color;
        this.type = pant.type;
    }

    // Getter/Setter 
    public Color32 Color
    {
        get { return this.color; }
        set { this.color = value; }
    }

    public TypePant GetTypePant
    {
        get { return this.type; }
        set { this.type = value; }
    }
}

public class Tshirt : Clothing
{
    private Color32 color;
    private TypeTshirt type;

    public enum TypeTshirt { None, TShirt };

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

    public enum TypeHat { None, TopHat, StrawHat, CowBoy };

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