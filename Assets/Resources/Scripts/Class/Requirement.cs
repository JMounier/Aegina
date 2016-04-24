using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Requirement
{
    public enum Requirements { Tuto, FirstBlood };
    
    public static bool Check(Requirements require)
    {
        switch (require)
        {
            case Requirements.FirstBlood:
                return Stats.Death() > 0;
            case Requirements.Tuto:
                return Stats.TutoComplete();
            default :                
                return false;
        }
    }
}