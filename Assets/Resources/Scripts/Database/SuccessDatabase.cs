using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuccessDatabase
{
	//pose pas de question j'en ai besoin
	public static readonly Texture2D Texturevide = Resources.Load<Texture2D> ("Sprites/Interfaces/Void");



    public static readonly Success Root = new Success(0, TextDatabase.Apple, ItemDatabase.Workbench, new Requirement.Requirements[1] { Requirement.Requirements.Tuto }, 0);
	public static IEnumerable<Success> Success
	{
		get{ 
			yield return Root;
		}
	}


	public static Success Find(int id)
	{
		foreach (Success succ in Success) 
			if (succ.ID == id)
				return succ;		
		throw new System.Exception("No success found with this ID");

	}
}
