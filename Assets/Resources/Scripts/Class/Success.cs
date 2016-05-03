﻿using UnityEngine;
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
