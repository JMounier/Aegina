using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Succes {

    private int id;
    private Text description;
    private Texture2D icon;
    private bool achived;


    private int nbCondition;
    private Requirement[] requirements; 

    private Succes[] sons;

    public Succes(int id, Text description, Texture2D icon, Requirement[] requirements, params Succes[] sons)
    {
        this.id = id;
        this.description = description;
        this.icon = icon;
        this.sons = sons;

        // a voir comment on gere ça.
        this.requirements = requirements;
        this.nbCondition = requirements.Length;

        this.achived = false;
        this.Checkreq();
    }

    #region Methods

    public void Checkreq()
    {
        if (!this.achived)
        {
            bool b = true;
            foreach (Requirement req in requirements)
            {
                b = req.Complit;
                if (!b)
                    break;
            }
            this.achived = b;
        }
    }

    #endregion

    #region Getters/Setters
    public int ID
    {
        get { return this.id; }
    }

    public int NbCondition
    {
        get { return this.nbCondition; }
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
