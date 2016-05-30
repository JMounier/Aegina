﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuccessDatabase
{
    public static readonly Success FirstEnd = new Success(73, 40, 1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.FirstEnd }, 1);
    public static readonly Success SecondEnd = new Success(72, 40, -1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.SecondEnd }, 1);
    public static readonly Success GunDam = new Success(71, 38, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.KillTheBoss }, 1,FirstEnd,SecondEnd);
    public static readonly Success SunkiumAge = new Success(70, 36, 0, TextDatabase.Apple, ItemDatabase.SunkiumIngot, new Requirement.Requirements[0] { }, 1,GunDam);
    public static readonly Success DivineLVL5 = new Success(63, 34, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.DivineCrisatlLVL5 }, 2,SunkiumAge);
    public static readonly Success OtherLVL5 = new Success(62, 32, 1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.OtherCristalLVL5 }, 1,DivineLVL5);
    public static readonly Success SunkiumEquip = new Success(61, 32, -1, TextDatabase.Apple, ItemDatabase.SunkiumSword, new Requirement.Requirements[1] { Requirement.Requirements.SunkiumEquip }, 1,DivineLVL5);
    public static readonly Success floatiumAge = new Success(60, 30, 0, TextDatabase.Apple, ItemDatabase.FloatiumIngot, new Requirement.Requirements[0] { }, 1,SunkiumEquip,OtherLVL5);
    public static readonly Success SlimeChief = new Success(54, 28, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.HuntSlimeChief }, 3,floatiumAge);
    public static readonly Success Slimicide = new Success(53, 26, 2, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.HuntMassSlime }, 1,SlimeChief);
    public static readonly Success FloatiumArmor = new Success(52, 26, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.FloatiumArmor }, 1,SlimeChief);
    public static readonly Success MithrilWeapon = new Success(51, 26,-2, TextDatabase.Apple, ItemDatabase.MithrilSword, new Requirement.Requirements[1] { Requirement.Requirements.MithrilWeapon }, 1,SlimeChief);
    public static readonly Success MithrilAge = new Success(50, 24, 0, TextDatabase.Apple, ItemDatabase.MithrilIngot, new Requirement.Requirements[0] { }, 2,MithrilWeapon,FloatiumArmor,Slimicide);
    public static readonly Success DivineLVL4 = new Success(42, 22, 1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.DivineCrisatlLVL4 }, 1,MithrilAge);
    public static readonly Success OtherLVL3 = new Success(41, 22, -1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.OtherCrisatlLVL3 }, 1,MithrilAge);
    public static readonly Success GoldAge = new Success(40, 20, 0, TextDatabase.Apple, ItemDatabase.GoldIngot, new Requirement.Requirements[0] { }, 1,OtherLVL3,DivineLVL4);
    public static readonly Success Pampichief = new Success(34, 18, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.HuntPampiChief }, 3,GoldAge);
    public static readonly Success Trap = new Success(33, 16, -2, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.CraftTrap }, 1, Pampichief);
    public static readonly Success EquipIron = new Success(32, 16, 0, TextDatabase.Apple, ItemDatabase.IronSword, new Requirement.Requirements[1] { Requirement.Requirements.EquipInIron }, 1, Pampichief);
    public static readonly Success Pampicide = new Success(31, 16, 2, TextDatabase.Apple, ItemDatabase.Cact, new Requirement.Requirements[1] { Requirement.Requirements.HuntMassPampi }, 1,Pampichief);
    public static readonly Success IronAge = new Success(30, 14, 0, TextDatabase.Apple, ItemDatabase.IronIngot, new Requirement.Requirements[0] { }, 4, Pampicide,Trap,EquipIron);
    public static readonly Success Forge = new Success(24, 12, 3, TextDatabase.Apple, ItemDatabase.Forge, new Requirement.Requirements[1] { Requirement.Requirements.CraftForge }, 1, IronAge);
    public static readonly Success DivineLVL3 = new Success(23, 12, 1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.DivineCristalLVL3 }, 1, IronAge);
    public static readonly Success Cauldron = new Success(22, 12, -1, TextDatabase.Apple, ItemDatabase.Cauldron, new Requirement.Requirements[1] { Requirement.Requirements.CraftCauldron }, 1, IronAge);
    public static readonly Success FirstHeal = new Success(21, 12, -3, TextDatabase.Apple, ItemDatabase.HealingPotion, new Requirement.Requirements[1] { Requirement.Requirements.DrinkHealPotion }, 1,IronAge);
    public static readonly Success CopperAge = new Success(20, 10, 0, TextDatabase.Apple, ItemDatabase.CopperIngot, new Requirement.Requirements[0] { }, 1, FirstHeal, Cauldron, DivineLVL3, Forge);
    public static readonly Success BoarChief = new Success(15, 8, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.HuntBoarChief }, 4, CopperAge);
    public static readonly Success Boaricide = new Success(14, 6, 3, TextDatabase.Apple, ItemDatabase.Fang, new Requirement.Requirements[1] { Requirement.Requirements.HuntMassBoar }, 1, BoarChief);
    public static readonly Success LeatherArmor = new Success(13, 6, 1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.CraftFirstArmor }, 1, BoarChief);
    public static readonly Success StoneWeapon = new Success(12, 6, -1, TextDatabase.Apple, ItemDatabase.StoneSword, new Requirement.Requirements[1] { Requirement.Requirements.CraftStoneWeapon }, 1, BoarChief);
    public static readonly Success FirstChest = new Success(11, 6, -3, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.CraftChest }, 1, BoarChief);
    public static readonly Success StoneAge = new Success(10, 4, 0, TextDatabase.Apple, ItemDatabase.Stone, new Requirement.Requirements[0] { }, 2, FirstChest, StoneWeapon, LeatherArmor, Boaricide);
    public static readonly Success FirstBlood = new Success(1, 0, 2, TextDatabase.Apple, ItemDatabase.IronSword, new Requirement.Requirements[1] { Requirement.Requirements.FirstBlood }, 1);
    public static readonly Success FirstDeath = new Success(2, 0, -2, TextDatabase.Apple, ItemDatabase.Bone, new Requirement.Requirements[1] { Requirement.Requirements.FirstDeath }, 1);
    public static readonly Success FirstHunt = new Success(3, 2, -1, TextDatabase.Apple, ItemDatabase.Gigot, new Requirement.Requirements[1] { Requirement.Requirements.FirstHunt }, 1, StoneAge);
    public static readonly Success Cristal = new Success(4, 2, 1, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.FirstCap }, 1,StoneAge);
    public static readonly Success Tuto = new Success(0, -2, 0, TextDatabase.Apple, ItemDatabase.Workbench, new Requirement.Requirements[1] { Requirement.Requirements.Tuto }, 1);
    public static readonly Success Root = new Success(-1, 0, 0, TextDatabase.Apple, ItemDatabase.Apple, new Requirement.Requirements[1] { Requirement.Requirements.PlayTheGame }, 0,Tuto,FirstBlood,FirstDeath,FirstHunt,Cristal);


    public static IEnumerable<Success> Success
    {
        get
        {
            yield return FirstEnd;
            yield return SecondEnd;
            yield return GunDam;
            yield return SunkiumAge;
            yield return DivineLVL5;
            yield return OtherLVL5;
            yield return SunkiumEquip;
            yield return floatiumAge;
            yield return SlimeChief;
            yield return Slimicide;
            yield return FloatiumArmor;
            yield return MithrilWeapon;
            yield return MithrilAge;
            yield return DivineLVL4;
            yield return OtherLVL3;
            yield return GoldAge;
            yield return Trap;
            yield return EquipIron;
            yield return Pampichief;
            yield return IronAge;
            yield return Pampicide;
            yield return Forge;
            yield return DivineLVL3;
            yield return Cauldron;
            yield return FirstBlood;
            yield return FirstDeath;
            yield return FirstHunt;
            yield return Cristal;
            yield return Tuto;
            yield return StoneAge;
            yield return FirstChest;
            yield return StoneWeapon;
            yield return LeatherArmor;
            yield return Boaricide;
            yield return BoarChief;
            yield return CopperAge;
            yield return FirstHeal;


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
