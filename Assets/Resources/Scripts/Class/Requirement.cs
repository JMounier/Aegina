using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Requirement
{
    public enum Requirements { PlayTheGame,Tuto, FirstBlood, FirstCap, FirstDeath, FirstHunt, CraftChest, CraftStoneWeapon, HuntMassBoar, CraftFirstArmor, HuntBoarChief, CraftForge, DivineCristalLVL3, CraftCauldron, DrinkHealPotion, CraftTrap, EquipInIron, HuntMassPampi, HuntPampiChief, DivineCrisatlLVL4, OtherCrisatlLVL3, FloatiumArmor, MithrilWeapon, HuntMassSlime, HuntSlimeChief,SunkiumEquip, OtherCristalLVL5, DivineCrisatlLVL5, KillTheBoss, FirstEnd, SecondEnd };

    public static bool Check(Requirements require)
    {
        switch (require)
        {
            case Requirements.PlayTheGame:
                return Stats.TimePlayer() > 0;
            case Requirements.FirstBlood:
                return Stats.Kill() > 0;
            case Requirements.FirstCap:
                return Stats.CapturedCristal() > 0;
            case Requirements.FirstDeath:
                return Stats.Death() > 0;
            case Requirements.FirstHunt:
                return Stats.Hunt() > 0;
            case Requirements.Tuto:
                return Stats.TutoComplete();
            case Requirements.CraftChest:
                break;// TO DO
            case Requirements.CraftStoneWeapon:
                return Stats.Crafted(CraftDatabase.StoneSword) > 0;// TO IMPROVE
            case Requirements.HuntMassBoar:
                return Stats.Hunted(EntityDatabase.Boar) > 10;
            case Requirements.CraftFirstArmor:
                return Stats.Crafted(CraftDatabase.LeatherTop) > 0 && Stats.Crafted(CraftDatabase.LeatherBottom) > 0;
            case Requirements.HuntBoarChief:
                break;// TO DO
            case Requirements.CraftForge:
                return Stats.Crafted(CraftDatabase.Forge) > 0;
            case Requirements.DivineCristalLVL3:
                return Stats.CristalLevel(2) > 2;
            case Requirements.CraftCauldron:
                return Stats.Crafted(CraftDatabase.Brewer) > 0;
            case Requirements.DrinkHealPotion:
                return Stats.Used(ItemDatabase.HealingPotion) > 0;
            case Requirements.CraftTrap:
                break;// TO DO 
            case Requirements.EquipInIron:
                return Stats.Crafted(CraftDatabase.IronTop) > 0 && Stats.Crafted(CraftDatabase.IronBottom) > 0 && Stats.Crafted(CraftDatabase.IronSword) > 0;// TO IMPROVE
            case Requirements.HuntMassPampi:
                return Stats.Hunted(EntityDatabase.Pampa) > 25;
            case Requirements.HuntPampiChief:
                break;// TO DO
            case Requirements.DivineCrisatlLVL4:
                return Stats.CristalLevel(2) > 3;
            case Requirements.OtherCrisatlLVL3:
                return Stats.CristalLevel(0) > 2 && Stats.CristalLevel(1) > 2;
            case Requirements.FloatiumArmor:
                return Stats.Crafted(CraftDatabase.FLoatiumTop) > 0 && Stats.Crafted(CraftDatabase.FLoatiumBottom) > 0;// TO IMPROVE
            case Requirements.MithrilWeapon:
                return Stats.Crafted(CraftDatabase.MithrilSword) > 0;// TO IMPROVE
            case Requirements.HuntMassSlime:
                break;// TO DO
            case Requirements.HuntSlimeChief:
                break;// TO DO
            case Requirements.SunkiumEquip:
                return Stats.Crafted(CraftDatabase.SunkiumTop) > 0 && Stats.Crafted(CraftDatabase.SunkiumBottom) > 0 && Stats.Crafted(CraftDatabase.SunkiumSword) > 0;// TO IMPROVE
            case Requirements.OtherCristalLVL5:
                return Stats.CristalLevel(0) > 4 && Stats.CristalLevel(1) > 4;
            case Requirements.DivineCrisatlLVL5:
                return Stats.CristalLevel(2) > 4;
            case Requirements.KillTheBoss:
                break;// TO DO
            case Requirements.FirstEnd:
                break;// TO DO
            case Requirements.SecondEnd:
                break;// TO DO
            default:
                return false;
        }
        return false;
    }
}