using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuccessDatabase
{
    public static readonly Success Root = new Success(0, TextDatabase.Apple, ItemDatabase.Workbench, new Requirement.Requirements[1] { Requirement.Requirements.Tuto }, 0);
	public static IEnumerable<Success> Success {
		get { 
			yield return Root;
		}
	}
	public static Success Find(int id){
		foreach (Success success in Success) {
			if (success.ID==id) 
				return success;
		}
		throw new System.Exception ("Succes not found");
	}
}
