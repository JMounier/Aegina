using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class Requirement
    {

    private int id;
    private int statID;
    private int value;
    private char op;
    private bool complit;

    public Requirement(int id, int statID, char op, int value)
    {
		this.id = id;
		this.statID = statID;
        this.op = op;
        this.value = value;
        this.complit = false;
    }

	#region Methods

	public void Check()
	{
		//Fixe Me
	}

	#endregion

    #region Getters/Setters

    public int Id
    {
        get { return this.id; }
    }

    public bool Complit
    {
        get { return this.complit; }
    }
    #endregion
}
