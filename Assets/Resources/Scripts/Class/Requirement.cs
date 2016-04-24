using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Requirement
{
    public enum Requirements { Tuto, FirstBlood , FirstCap, FirstDeath, FirstHunt};
    
    public static bool Check(Requirements require)
    {
        switch (require)
        {
            case Requirements.FirstBlood:
			return Stats.Kill() > 0;
		case Requirements.FirstCap:
			return Stats.CapturedCristal() > 0;
		case Requirements.FirstDeath:
			return Stats.Death() > 0;
		case Requirements.FirstHunt:
			return Stats.Hunt() > 0;
		case Requirements.Tuto:
			return true;
            default :                
                return false;
        }
    }
}