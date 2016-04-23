using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Success
{
    private static List<Success> currentSuccess = new List<Success>() { SuccessDatabase.Root };

    private int id;
    private Text description;
    private Texture2D icon;
    private bool achived;
    private Requirement.Requirements[] requirements;

    private Success[] sons;
	private int nbParentMax;
    private int nbParentsLeft;
    public int posX, posY;

    public Success(int id, Text description, Item item, Requirement.Requirements[] requirements, int nbParents, params Success[] sons)
    {
        this.achived = false;
        this.id = id;
        this.description = description;
        this.icon = item.Icon;
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

    private void Unlock(bool display)
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
                player.GetComponent<Story_Hud>().Display(this);            
    }

	/// <summary>
	/// Determines whether this succes isseen.
	/// </summary>
	/// <returns><c>true</c> if this instance isseen ; otherwise, <c>false</c>.</returns>
	public bool Isseen(){
		return this.nbParentMax != this.nbParentsLeft;
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

    public Success[] Sons
    {
        get { return this.sons; }
    }
    #endregion
}
