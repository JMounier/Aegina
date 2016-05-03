﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuccessDatabase
{   
	public static readonly Success FirstBlood = new Success(1, -2, 1, TextDatabase.Apple, ItemDatabase.IronSword, new Requirement.Requirements[1]{Requirement.Requirements.FirstBlood}, 1);
	public static readonly Success FirstDeath = new Success(2, -2, -1, TextDatabase.Apple, ItemDatabase.Bone, new Requirement.Requirements[1]{Requirement.Requirements.FirstDeath}, 1);
	public static readonly Success FirstHunt = new Success(3, 2, -1, TextDatabase.Apple, ItemDatabase.Gigot, new Requirement.Requirements[1]{Requirement.Requirements.FirstHunt}, 1);
	public static readonly Success Cristal = new Success(4, 2, 0, TextDatabase.Apple, ItemDatabase.FloatiumIngot, new Requirement.Requirements[1]{Requirement.Requirements.FirstCap}, 1);
        
	public static readonly Success Root = new Success(0, 0, 0, TextDatabase.Apple, ItemDatabase.Workbench, new Requirement.Requirements[1] { Requirement.Requirements.Tuto }, 0, Cristal, FirstBlood, FirstDeath, FirstHunt);
    
	public static IEnumerable<Success> Success
	{
		get{ 		
			yield return FirstBlood;
			yield return FirstDeath;
			yield return FirstHunt;
			yield return Cristal;


			//always last
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
