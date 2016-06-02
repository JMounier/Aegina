using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Success
{
    private static List<Success> currentSuccess;

    private int id;
    private Text description;
    private Texture2D icon;
    private Texture2D shadow;
    private bool achived;
    private Requirement.Requirements[] requirements;

    private Success[] sons;
    private int nbParentMax;
    private int nbParentsLeft;
    private int posX, posY;

    public static void Reset()
    {
        currentSuccess = new List<Success>() { SuccessDatabase.Root };
        foreach (Success suc in SuccessDatabase.Success)
        {
            suc.achived = false;
            suc.nbParentsLeft = suc.nbParentMax;
        }
        Update(false);
    }

    public Success(int id, int x, int y, Text description, Item item, Requirement.Requirements[] requirements, int nbParents, params Success[] sons)
    {
        this.achived = false;
        this.id = id;
        this.posX = x;
        this.posY = y;
        this.description = description;
        this.icon = item.Icon;
        this.shadow = GetShadow(item.Icon);
        this.requirements = requirements;
        this.nbParentsLeft = nbParents;
        this.nbParentMax = nbParents;
        this.sons = sons;
    }
    #region Methods

    public static void Update(bool display = true)
    {
        int j = 0;
        while (j < currentSuccess.Count)
        {
            Success succ = currentSuccess[j];
            bool b = true;

            for (int i = 0; i < succ.requirements.Length && b; i++)
                b = Requirement.Check(succ.requirements[i]);

            if (b)
            {
                currentSuccess.RemoveAt(j);
                succ.Unlock(display);
                if (succ.id % 10 == 0)
                {
                    GameObject[] allplayer = GameObject.FindGameObjectsWithTag("Player");
					if(display)
					switch (succ.id / 10)
                    {
                        case 1:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(1, 2, 40, 50, 60, 71, 72, 73);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                            break;
                        case 2:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(4, 5, 10, 16, 41, 51, 61);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                            break;
                        case 3:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(3, 6, 10, 22, 23, 24, 25, 26, 27, 42, 52, 62);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                            break;
                        case 4:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(12, 21, 43, 53, 63);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                                break;
                        case 5:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(13, 44, 54, 64);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                                break;
                        case 6:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(14, 45, 55, 65);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                                break;
                        case 7:
                                foreach (GameObject player in allplayer)
                                {
                                    player.GetComponent<Craft_HUD>().mastered(15, 46, 56, 66);
                                    player.GetComponent<Tutoriel>().Story();
                                }
                                break;
                        default:
                            break;
                    }
                }
            }
            else
                j++;
        }
    }

    public void Unlock(bool display)
    {
        this.achived = true;
        foreach (Success succ in this.sons)
        {
            succ.nbParentsLeft--;
            if (succ.nbParentsLeft == 0)
                currentSuccess.Add(succ);
        }
        if (display)
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Success_HUD>().Display(this);
    }

    private static Texture2D GetShadow(Texture2D img)
    {
        Texture2D shadow = new Texture2D(img.width, img.height);
        for (int i = 0; i < img.width; i++)
            for (int j = 0; j < img.height; j++)
            {
                if (img.GetPixel(i, j).a == 0)
                    shadow.SetPixel(i, j, Color.clear);
                else
                    shadow.SetPixel(i, j, Color.gray);
            }
        shadow.Apply();
        return shadow;
    }

    /// <summary>
    /// Determines whether this succes isseen.
    /// </summary>
    /// <returns><c>true</c> if this instance isseen ; otherwise, <c>false</c>.</returns>
    public bool IsSeen
    {
        get { return this.nbParentMax != this.nbParentsLeft || this.id == 0; }
    }
    #endregion

    #region Getters/Setters

    public int ID
    {
        get { return this.id; }
    }
    public int PosX
    {
        get { return this.posX; }
    }

    public int PosY
    {
        get { return this.posY; }
    }

    public Text Description
    {
        get { return this.description; }
    }

    public Texture2D Icon
    {
        get { return this.icon; }
    }

    public Texture2D Shadow
    {
        get { return this.shadow; }
    }

    public bool Achived
    {
        get { return this.achived; }
        set { this.achived = value; }
    }

    public Success[] Sons
    {
        get { return this.sons; }
    }

    public int NbParentLeft
    {
        get { return this.nbParentsLeft; }
        set { this.nbParentsLeft = value; }
    }
    #endregion
}
