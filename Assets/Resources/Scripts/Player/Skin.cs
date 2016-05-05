using UnityEngine;
using System.Collections;

public enum Gloves { brown };
public enum Panths { brown };
public enum Bodys { white };

public class Skin
{
    private Texture2D NewTexture;
    private static Texture2D[] GlovesArray;
    private static Texture2D[] PanthsArray;
    private static Texture2D[] BodiesArray;

    // Use this for initialization
    void Start () 
    {
        this.NewTexture = new Texture2D(512, 512);

        GlovesArray = new Texture2D[] {
            Resources.Load<Texture2D>("Models/Character/Textures/Gloves_Brown")
            };
        PanthsArray = new Texture2D[] {
            Resources.Load<Texture2D>("Models/Character/Textures/Pants_Brown")
            };
        PanthsArray = new Texture2D[] {
            Resources.Load<Texture2D>("Models/Character/Textures/Body_White")
            };

    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public Texture2D ChangeSkin(Texture2D newSkin) 
    {
        for (int i = 0; i < this.NewTexture.height; i++)
            for (int j = 0; j < this.NewTexture.width; j++)
            {
                Color pixel = newSkin.GetPixel(j, i);
                if (pixel.a != 0)
                    this.NewTexture.SetPixel(j, i, pixel);                   
            }
        return this.NewTexture; 
    }
}
