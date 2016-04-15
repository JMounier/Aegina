using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Succes
{
    private int id;
    private Text description;
    private Texture2D icon;
    private bool achived;
    private Requirement.Requirements[] requirements;

    private Succes[] sons;
    private int nbParentsLeft;
    public int posX, posY;

    public static List<Succes> currentSucces = new List<Succes>();

    public Succes(int id, Text description, Item item, Requirement.Requirements[] requirements, int nbParents, params Succes[] sons)
    {
        this.achived = false;
        this.id = id;
        this.description = description;
        this.icon = item.Icon;
        this.requirements = requirements;
        this.nbParentsLeft = nbParents;
        this.sons = sons;
    }
    #region Methods

    public static void Update(bool display = true)
    {
        int j = 0;
        while (j < currentSucces.Count)
        {
            Succes succ = currentSucces[j];
            bool b = true;

            for (int i = 0; i < succ.requirements.Length && b; i++)
                b = Requirement.Check(succ.requirements[i]);

            if (b)
            {
                currentSucces.RemoveAt(j);
                succ.Unlock(display);
            }
            else
                j++;
        }
    }

    private void Unlock(bool display)
    {
        this.achived = true;
        foreach (Succes succ in this.sons)
        {
            succ.nbParentsLeft--;
            if (succ.nbParentsLeft == 0)
                currentSucces.Add(succ);
        }
        if(display)
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<Story_Hud>().Display(this);
            }
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

    public bool Achived
    {
        get { return this.achived; }
    }

    public Succes[] Sons
    {
        get { return this.sons; }
    }
    #endregion
}
