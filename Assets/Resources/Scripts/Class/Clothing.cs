using UnityEngine;
using System.Collections;


public class Clothing
{
    private int id;
    private Texture2D cloth;
    private Text description;
    Type type;

    public enum Type { Skin, Tshirt, Pant, Gloves, Eyes };


    // Constructeur 
    public Clothing()
    {
        this.id = -1;
        this.cloth = null;
        this.description = new Text();
    }

    public Clothing(int id, Texture2D cloth, Text description, Type type)
    {
        this.id = id;
        this.cloth = cloth;
        this.description = description;
        this.type = type;
    }

    public static Tuple<Texture2D, string> MergeSkin(Clothing[] skin)
    {
        Texture2D newCloth = new Texture2D(512, 512);
        string sauvegarde = "";
        foreach (Clothing cloth in skin)
        {
            newCloth = ChangeSkin(newCloth, cloth.cloth);
            sauvegarde += cloth.id + " ";
        }
        Tuple<Texture2D, string> result = new Tuple<Texture2D, string>(newCloth, sauvegarde);
        return result;
    }


    public static Tuple<Texture2D, string> MergeNewSkin(Tuple<Texture2D, string> oldSKin, Clothing skin)
    {
        Texture2D newCloth = ChangeSkin(oldSKin.Item1, skin.cloth);
        int compt = (int)skin.type;
        string newString = "";
        int i = 0;
        while(i < newString.Length)
        {
            if (compt == 0)
            {
                while (oldSKin.Item2 != " ")
                    i++;
                newString += skin.id.ToString();
            }
            else
                newString += oldSKin.Item2;
            i++;            
        }
        Tuple<Texture2D, string> result = new Tuple<Texture2D, string>(newCloth, newString);
        return result;
    }

    public static Texture2D ChangeSkin(Texture2D OriginSkin, Texture2D newSkin)
    {
        if (newSkin.width != OriginSkin.width || newSkin.height != OriginSkin.height)
            throw new System.Exception("Image mauvaise taille");
        else
            for (int i = 0; i < OriginSkin.height; i++)
                for (int j = 0; j < OriginSkin.width; j++)
                {
                    Color pixel = newSkin.GetPixel(j, i);
                    if (pixel.a != 0)
                        OriginSkin.SetPixel(j, i, pixel);
                }
        return OriginSkin;
    }

    //Skin
    public static readonly Clothing whiteSkin = new Clothing(1, Resources.Load<Texture2D>("Models/Character/Textures/Body_White"), TextDatabase.WhiteSkin, Type.Skin);
    public static readonly Clothing brownPant = new Clothing(2, Resources.Load<Texture2D>("Models/Character/Textures/Skin_Base"), TextDatabase.brownPant, Type.Pant);
    public static readonly Clothing brownGloves = new Clothing(3, Resources.Load<Texture2D>("Models/Character/Textures/Gloves_Brown"), TextDatabase.brownGloves, Type.Gloves);
    public static readonly Clothing brownEye = new Clothing(4, Resources.Load<Texture2D>("Models/Character/Textures/Skin_Base"), TextDatabase.BrownEyes, Type.Eyes);
}
