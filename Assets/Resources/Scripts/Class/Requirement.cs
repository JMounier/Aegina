using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Requirement
{
    public enum Requirements
    {
        PlayTheGame, Tuto, FirstBlood, FirstCap, FirstDeath, FirstHunt, CraftChest, CraftStoneWeapon, HuntMassBoar, CraftFirstArmor, HuntBoarChief, CraftForge,
        DivineCristalLVL3, CraftCauldron, DrinkHealPotion, CraftTrap, EquipInIron, HuntMassPampi, HuntPampiChief, DivineCrisatlLVL4, OtherCrisatlLVL3, MithrilArmor, FloatiumWeapon,
        HuntMassSlime, HuntSlimeChief, SunkiumEquip, OtherCristalLVL5, DivineCrisatlLVL5, KillTheBoss
    };

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
                return Stats.Crafted(CraftDatabase.Chest) > 0;
            case Requirements.CraftStoneWeapon:
                return Stats.Crafted(CraftDatabase.StoneSword) > 0;
            case Requirements.HuntMassBoar:
                return Stats.Hunted(EntityDatabase.Boar) > 9;
            case Requirements.CraftFirstArmor:
                return Stats.Crafted(CraftDatabase.LeatherTop) > 0 && Stats.Crafted(CraftDatabase.LeatherBottom) > 0;
            case Requirements.HuntBoarChief:
                return Stats.Hunted(EntityDatabase.BoarChief) > 0;
            case Requirements.CraftForge:
                return Stats.Crafted(CraftDatabase.Forge) > 0;
            case Requirements.DivineCristalLVL3:
                return Stats.CristalLevel(2) > 2;
            case Requirements.CraftCauldron:
                return Stats.Crafted(CraftDatabase.Brewer) > 0;
            case Requirements.DrinkHealPotion:
                return Stats.Used(ItemDatabase.HealingPotion) > 0;
            case Requirements.CraftTrap:
                return Stats.Crafted(CraftDatabase.WolfTrap) > 0;
            case Requirements.EquipInIron:
                return Stats.Crafted(CraftDatabase.IronTop) > 0 && Stats.Crafted(CraftDatabase.IronBottom) > 0 && (Stats.Crafted(CraftDatabase.IronSword) > 0 || Stats.Crafted(CraftDatabase.IronSpear) > 0 || Stats.Crafted(CraftDatabase.IronBattleAxe) > 0);
            case Requirements.HuntMassPampi:
                return Stats.Hunted(EntityDatabase.Pampa) > 24;
            case Requirements.HuntPampiChief:
                return Stats.Hunted(EntityDatabase.PampaChief) > 0;
            case Requirements.DivineCrisatlLVL4:
                return Stats.CristalLevel(2) > 3;
            case Requirements.OtherCrisatlLVL3:
                return Stats.CristalLevel(0) > 2 && Stats.CristalLevel(1) > 2;
            case Requirements.MithrilArmor:
                return Stats.Crafted(CraftDatabase.MithrilTop) > 0 && Stats.Crafted(CraftDatabase.MithrilBottom) > 0;
            case Requirements.FloatiumWeapon:
                return Stats.Crafted(CraftDatabase.FloatiumSword) > 0 || Stats.Crafted(CraftDatabase.FloatiumSpear) > 0 || Stats.Crafted(CraftDatabase.FloatiumBattleAxe) > 0;
            case Requirements.HuntMassSlime:
                return Stats.Hunted(EntityDatabase.SlimeAqua) + Stats.Hunted(EntityDatabase.SlimeAquaSmall) + Stats.Hunted(EntityDatabase.SlimeGreen) + Stats.Hunted(EntityDatabase.SlimeGreenSmall)
                    + Stats.Hunted(EntityDatabase.SlimeYellow) + Stats.Hunted(EntityDatabase.SlimeYellowSmall) > 49;
            case Requirements.HuntSlimeChief:
                return Stats.Hunted(EntityDatabase.SlimeChief) > 0;
            case Requirements.SunkiumEquip:
                return Stats.Crafted(CraftDatabase.SunkiumTop) > 0 && Stats.Crafted(CraftDatabase.SunkiumBottom) > 0 && (Stats.Crafted(CraftDatabase.SunkiumSword) > 0 || Stats.Crafted(CraftDatabase.SunkiumSpear) > 0 || Stats.Crafted(CraftDatabase.SunkiumBattleAxe) > 0);// TO IMPROVE
            case Requirements.OtherCristalLVL5:
                return Stats.CristalLevel(0) > 4 && Stats.CristalLevel(1) > 4;
            case Requirements.DivineCrisatlLVL5:
                return Stats.CristalLevel(2) > 4;
            case Requirements.KillTheBoss:
                return Stats.BossKill;
            default:
                return false;
        }
        return false;
    }
    public static void Unlock(Requirements require)
    {
        switch (require)
        {
            case Requirements.PlayTheGame:
                Stats.IncrementTimePlayer(1);
                break;
            case Requirements.FirstBlood:
                Stats.IncrementKill();
                break;
            case Requirements.FirstCap:
                Stats.IncrementCapturedCristal();
                break;
            case Requirements.FirstDeath:
                Stats.IncrementDeath();
                break;
            case Requirements.FirstHunt:
                Stats.AddHunt(EntityDatabase.Boar);
                break;
            case Requirements.Tuto:
                Stats.SetTutoComplete();
                break;
            case Requirements.CraftChest:
                Stats.AddCrafted(CraftDatabase.Chest);
                break;
            case Requirements.CraftStoneWeapon:
                Stats.AddCrafted(CraftDatabase.StoneSword);
                break;
            case Requirements.HuntMassBoar:
                while (Stats.Hunted(EntityDatabase.Boar) < 10)
                    Stats.AddHunt(EntityDatabase.Boar);
                break;
            case Requirements.CraftFirstArmor:
                Stats.AddCrafted(CraftDatabase.LeatherTop);
                Stats.AddCrafted(CraftDatabase.LeatherBottom);
                break;
            case Requirements.HuntBoarChief:
                Stats.AddHunt(EntityDatabase.BoarChief);
                break;
            case Requirements.CraftForge:
                Stats.AddCrafted(CraftDatabase.Forge);
                break;
            case Requirements.DivineCristalLVL3:
                if (Stats.CristalLevel(2) < 3)
                    Stats.ChangeCristalLevel(2, 3);
                break;
            case Requirements.CraftCauldron:
                Stats.AddCrafted(CraftDatabase.Brewer);
                break;
            case Requirements.DrinkHealPotion:
                Stats.AddUsed(ItemDatabase.HealingPotion.ID);
                break;
            case Requirements.CraftTrap:
                Stats.AddCrafted(CraftDatabase.WolfTrap);
                break;
            case Requirements.EquipInIron:
                Stats.AddCrafted(CraftDatabase.IronTop);
                Stats.AddCrafted(CraftDatabase.IronBottom);
                Stats.AddCrafted(CraftDatabase.IronSword);
                break;
            case Requirements.HuntMassPampi:
                while (Stats.Hunted(EntityDatabase.Pampa) < 25)
                    Stats.AddHunt(EntityDatabase.Pampa);
                break;
            case Requirements.HuntPampiChief:
                Stats.AddHunt(EntityDatabase.PampaChief);
                break;
            case Requirements.DivineCrisatlLVL4:
                if (Stats.CristalLevel(2) < 4)
                    Stats.ChangeCristalLevel(2, 4);
                break;
            case Requirements.OtherCrisatlLVL3:
                if (Stats.CristalLevel(0) < 3)
                    Stats.ChangeCristalLevel(0, 3);
                if (Stats.CristalLevel(1) < 3)
                    Stats.ChangeCristalLevel(1, 3);
                break;
            case Requirements.MithrilArmor:
                Stats.AddCrafted(CraftDatabase.MithrilTop);
                Stats.AddCrafted(CraftDatabase.MithrilBottom);
                break;
            case Requirements.FloatiumWeapon:
                Stats.AddCrafted(CraftDatabase.FloatiumSword);
                break;
            case Requirements.HuntMassSlime:
                while (Stats.Hunted(EntityDatabase.SlimeAqua) + Stats.Hunted(EntityDatabase.SlimeAquaSmall) + Stats.Hunted(EntityDatabase.SlimeGreen) + Stats.Hunted(EntityDatabase.SlimeGreenSmall)
                    + Stats.Hunted(EntityDatabase.SlimeYellow) + Stats.Hunted(EntityDatabase.SlimeYellowSmall) < 50)
                    Stats.AddHunt(EntityDatabase.SlimeAqua);
                break;
            case Requirements.HuntSlimeChief:
                Stats.AddHunt(EntityDatabase.SlimeChief);
                break;
            case Requirements.SunkiumEquip:
                Stats.AddCrafted(CraftDatabase.SunkiumTop);
                Stats.AddCrafted(CraftDatabase.SunkiumBottom);
                Stats.AddCrafted(CraftDatabase.SunkiumSword);
                break;
            case Requirements.OtherCristalLVL5:
                    Stats.ChangeCristalLevel(0, 5);
                    Stats.ChangeCristalLevel(1, 5);
                break;
            case Requirements.DivineCrisatlLVL5:
                Stats.ChangeCristalLevel(2, 5);
                break;
            case Requirements.KillTheBoss:
                Stats.BossKill = true;
                break;
            default:
                break;
        }
    }
}