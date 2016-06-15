using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CraftDatabase
{
    public static readonly Craft Torch = new Craft(0, new ItemStack(ItemDatabase.Torch, 1), false, false, false, false, false, Craft.Type.WorkTop, new ItemStack(ItemDatabase.Stick, 1), new ItemStack(ItemDatabase.AnimalFat,1));
	public static readonly Craft FireCamp = new Craft(1,new ItemStack(ItemDatabase.Firepit,1),false,false,false,false,false,Craft.Type.WorkTop,new ItemStack(ItemDatabase.Torch,1),new ItemStack(ItemDatabase.WoodenPlank,1));
    public static readonly Craft Workbench = new Craft(2, new ItemStack(ItemDatabase.Workbench, 1), false, false, false, false, false, Craft.Type.WorkTop, new ItemStack(ItemDatabase.WoodenPlank, 2), new ItemStack(ItemDatabase.Stick, 4));
    public static readonly Craft Forge = new Craft(3, new ItemStack(ItemDatabase.Forge, 1), false, true, false, false, false, Craft.Type.WorkTop, new ItemStack(ItemDatabase.CuttedStone, 5), new ItemStack(ItemDatabase.Firepit, 1));
    public static readonly Craft Brewer = new Craft(4, new ItemStack(ItemDatabase.Cauldron, 1), false, false, true, false, false, Craft.Type.WorkTop, new ItemStack(ItemDatabase.IronIngot, 3), new ItemStack(ItemDatabase.CopperIngot, 1));
    public static readonly Craft Chest = new Craft(5, new ItemStack(ItemDatabase.Chest, 1), false, true, false, false, false, Craft.Type.WorkTop, new ItemStack(ItemDatabase.WoodenPlank, 4), new ItemStack(ItemDatabase.Stick, 2));
    public static readonly Craft WolfTrap = new Craft(6, new ItemStack(ItemDatabase.WolfTrap, 1), false, true, false, false, false, Craft.Type.WorkTop, new ItemStack(ItemDatabase.IronIngot, 2), new ItemStack(ItemDatabase.Stick, 1), new ItemStack(ItemDatabase.Apple, 1));

    public static readonly Craft CopperIngot = new Craft(10, new ItemStack(ItemDatabase.CopperIngot, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Copper, 3));
    public static readonly Craft IronIngot = new Craft(11, new ItemStack(ItemDatabase.IronIngot, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Iron, 3));
    public static readonly Craft GoldIngot = new Craft(12, new ItemStack(ItemDatabase.GoldIngot, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Gold, 3));
    public static readonly Craft MithrilIngot = new Craft(13, new ItemStack(ItemDatabase.MithrilIngot, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Mithril, 3));
    public static readonly Craft FLoatiumIngot = new Craft(14, new ItemStack(ItemDatabase.FloatiumIngot, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Floatium, 3));
    public static readonly Craft SunkiumIngot = new Craft(15, new ItemStack(ItemDatabase.SunkiumIngot, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Sunkium, 3));
    public static readonly Craft Glass = new Craft(16, new ItemStack(ItemDatabase.Glass, 1), false, false, true, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Sand, 3));

    public static readonly Craft MeatBrochette = new Craft(20, new ItemStack(ItemDatabase.MeatBalls, 1), false, false, false, false, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Gigot, 1), new ItemStack(ItemDatabase.Stick, 1));
	public static readonly Craft Stew = new Craft(21,new ItemStack(ItemDatabase.Soup,1),false,false,false,true,false,Craft.Type.Consumable,new ItemStack(ItemDatabase.Bowl,1),new ItemStack(ItemDatabase.Gigot,2),new ItemStack(ItemDatabase.WaterCact,1));
    public static readonly Craft Potion1 = new Craft(22, new ItemStack(ItemDatabase.WaterPotion, 1), false, false, false, true, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Glass, 1), new ItemStack(ItemDatabase.WaterCact, 3));
    public static readonly Craft Potion2 = new Craft(23, new ItemStack(ItemDatabase.SpeedPotion, 1), false, false, false, true, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.WaterPotion, 1), new ItemStack(ItemDatabase.WaterCact, 3));
    public static readonly Craft Potion3 = new Craft(24, new ItemStack(ItemDatabase.PoisonPotion, 1), false, false, false, true, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Glass, 1), new ItemStack(ItemDatabase.RedMushroom, 2), new ItemStack(ItemDatabase.WaterCact, 1));
    public static readonly Craft Potion4 = new Craft(25, new ItemStack(ItemDatabase.RegenerationPotion, 1), false, false, false, true, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Glass, 1), new ItemStack(ItemDatabase.Mushroom, 2), new ItemStack(ItemDatabase.WaterCact, 1));
    public static readonly Craft Potion5 = new Craft(26, new ItemStack(ItemDatabase.HealingPotion, 1), false, false, false, true, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Glass, 1), new ItemStack(ItemDatabase.Petal, 2), new ItemStack(ItemDatabase.WaterCact, 1));
    public static readonly Craft Potion6 = new Craft(27, new ItemStack(ItemDatabase.JumpPotion, 1), false, false, false, true, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Glass, 1), new ItemStack(ItemDatabase.Cact, 2), new ItemStack(ItemDatabase.WaterCact, 1));
    public static readonly Craft water = new Craft(28, new ItemStack(ItemDatabase.WaterCact, 2), false, false, false, false, false, Craft.Type.Consumable, new ItemStack(ItemDatabase.Cact, 3));

    public static readonly Craft StonePickaxe = new Craft(40, new ItemStack(ItemDatabase.StonePickaxe, 1), false, false, false, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.Stone, 3), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft CopperPickaxe = new Craft(41, new ItemStack(ItemDatabase.CopperPickaxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.CopperIngot, 2), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft IronPickaxe = new Craft(42, new ItemStack(ItemDatabase.IronPickaxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.IronIngot, 2), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft GoldPickaxe = new Craft(43, new ItemStack(ItemDatabase.GoldPickaxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.GoldIngot, 2), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft MithrilPickaxe = new Craft(44, new ItemStack(ItemDatabase.MithrilPickaxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.MithrilIngot, 2), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft FloatiumPickaxe = new Craft(45, new ItemStack(ItemDatabase.FloatiumPickaxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.FloatiumIngot, 2), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft SunkiumPickaxe = new Craft(46, new ItemStack(ItemDatabase.SunkiumPickaxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.SunkiumIngot, 2), new ItemStack(ItemDatabase.Stick, 1));

	public static readonly Craft StoneAxe = new Craft(50, new ItemStack(ItemDatabase.StoneAxe, 1), false, false, false, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.Stone, 2), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft CopperAxe = new Craft(51, new ItemStack(ItemDatabase.CopperAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.CopperIngot, 1), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft IronAxe = new Craft(52, new ItemStack(ItemDatabase.IronAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.IronIngot, 1), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft GoldAxe = new Craft(53, new ItemStack(ItemDatabase.GoldAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.GoldIngot, 1), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft MithrilAxe = new Craft(54, new ItemStack(ItemDatabase.MithrilAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.MithrilIngot, 1), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft FloatiumAxe = new Craft(55, new ItemStack(ItemDatabase.FloatiumAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.FloatiumIngot, 1), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft SunkiumAxe = new Craft(56, new ItemStack(ItemDatabase.SunkiumAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.SunkiumAxe, 1), new ItemStack(ItemDatabase.Stick, 1));

    public static readonly Craft StoneSword = new Craft(60, new ItemStack(ItemDatabase.StoneSword, 1), false, false, false, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.Stone, 4), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft CopperSword = new Craft(61, new ItemStack(ItemDatabase.CopperSword, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.CopperIngot, 3), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft IronSword = new Craft(62, new ItemStack(ItemDatabase.IronSword, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.IronIngot, 3), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft GoldSword = new Craft(63, new ItemStack(ItemDatabase.GoldSword, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.GoldIngot, 3), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft MithrilSword = new Craft(64, new ItemStack(ItemDatabase.MithrilSword, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.MithrilIngot, 3), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft FloatiumSword = new Craft(65, new ItemStack(ItemDatabase.FloatiumSword, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.FloatiumIngot, 3), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft SunkiumSword = new Craft(66, new ItemStack(ItemDatabase.SunkiumSword, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.SunkiumIngot, 3), new ItemStack(ItemDatabase.Stick, 1));

    public static readonly Craft Stick = new Craft(70, new ItemStack(ItemDatabase.Stick, 2), false, false, false, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Log, 1));
    public static readonly Craft Plank = new Craft(71, new ItemStack(ItemDatabase.WoodenPlank, 2), false, false, false, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Log, 3));
    public static readonly Craft Cup = new Craft(72, new ItemStack(ItemDatabase.Bowl, 2), false, true, false, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Log, 3));
    public static readonly Craft CutStone = new Craft(73, new ItemStack(ItemDatabase.CuttedStone, 2), false, true, false, false, false, Craft.Type.Elementary, new ItemStack(ItemDatabase.Stone, 3));

	public static readonly Craft LeatherTop = new Craft(80,new ItemStack(ItemDatabase.LeatherTopArmor,1),false,true,false,false,false,Craft.Type.Armor,new ItemStack(ItemDatabase.Bone,3),new ItemStack(ItemDatabase.Hide,10));
	public static readonly Craft IronTop = new Craft(81,new ItemStack(ItemDatabase.IronTopArmor,1),false,true,true,false,false,Craft.Type.Armor,new ItemStack(ItemDatabase.Bone,2),new ItemStack(ItemDatabase.IronIngot,5));
	public static readonly Craft MithrilTop = new Craft(81,new ItemStack(ItemDatabase.MithrilTopArmor,1),false,true,true,false,false,Craft.Type.Armor,new ItemStack(ItemDatabase.FloatiumIngot,2),new ItemStack(ItemDatabase.MithrilIngot,5));
	public static readonly Craft SunkiumTop = new Craft(81,new ItemStack(ItemDatabase.SunkiumTopArmor,1),false,true,true,false,false,Craft.Type.Armor,new ItemStack(ItemDatabase.IronIngot,2),new ItemStack(ItemDatabase.SunkiumIngot,5));

    public static readonly Craft LeatherBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft IronBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft MithrilBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft SunkiumBottom = new Craft(Craft.Type.Armor);

    public static readonly Craft CopperBattleAxe = new Craft(90, new ItemStack(ItemDatabase.CopperBattleAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.CopperIngot, 4), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft IronBattleAxe = new Craft(91, new ItemStack(ItemDatabase.IronBattleAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.IronIngot, 4), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft GoldBattleAxe = new Craft(92, new ItemStack(ItemDatabase.GoldBattleAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.GoldIngot, 4), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft MithrilBattleAxe = new Craft(93, new ItemStack(ItemDatabase.MithrilBattleAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.MithrilIngot, 4), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft FloatiumBattleAxe = new Craft(94, new ItemStack(ItemDatabase.FloatiumBattleAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.FloatiumIngot, 4), new ItemStack(ItemDatabase.Stick, 1));
    public static readonly Craft SunkiumBattleAxe = new Craft(95, new ItemStack(ItemDatabase.SunkiumBattleAxe, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.SunkiumIngot, 4), new ItemStack(ItemDatabase.Stick, 1));

    public static readonly Craft CopperSpear = new Craft(100, new ItemStack(ItemDatabase.CopperSpear, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.CopperIngot, 2), new ItemStack(ItemDatabase.Stick, 3));
    public static readonly Craft IronSpear = new Craft(101, new ItemStack(ItemDatabase.IronSpear, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.IronIngot, 2), new ItemStack(ItemDatabase.Stick, 3));
    public static readonly Craft GoldSpear = new Craft(102, new ItemStack(ItemDatabase.GoldSpear, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.GoldIngot, 2), new ItemStack(ItemDatabase.Stick, 3));
    public static readonly Craft MithrilSpear = new Craft(103, new ItemStack(ItemDatabase.MithrilSpear, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.MithrilIngot, 2), new ItemStack(ItemDatabase.Stick, 3));
    public static readonly Craft FloatiumSpear = new Craft(104, new ItemStack(ItemDatabase.FloatiumSpear, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.FloatiumIngot, 2), new ItemStack(ItemDatabase.Stick, 3));
    public static readonly Craft SunkiumSpear = new Craft(105, new ItemStack(ItemDatabase.SunkiumSpear, 1), false, true, true, false, false, Craft.Type.Tools, new ItemStack(ItemDatabase.SunkiumIngot, 2), new ItemStack(ItemDatabase.Stick, 3));

    public static readonly Craft InstableCore = new Craft(666, new ItemStack(ItemDatabase.InstableCore, 1), false, false, false, false, true, Craft.Type.Consumable, new ItemStack(ItemDatabase.BoarCore, 1), new ItemStack(ItemDatabase.SlimeCore, 1), new ItemStack(ItemDatabase.PampiCore, 1));



    public static IEnumerable<Craft> Crafts
    {
        get
        {
            yield return Torch;
            yield return FireCamp;
            yield return Workbench;
            yield return Forge;
            yield return Brewer;
            yield return Chest;
            yield return WolfTrap;

            yield return Stick;
            yield return Plank;
            yield return Cup;
            yield return CutStone;
            yield return CopperIngot;
            yield return IronIngot;
            yield return GoldIngot;
            yield return MithrilIngot;
            yield return FLoatiumIngot;
            yield return SunkiumIngot;
            yield return Glass;

            yield return MeatBrochette;
            yield return Stew;
            yield return Potion1;
            yield return Potion2;
            yield return Potion3;
            yield return Potion4;
            yield return Potion5;
            yield return Potion6;
            yield return water;

            yield return StonePickaxe;
            yield return CopperPickaxe;
            yield return IronPickaxe;
            yield return GoldPickaxe;
            yield return MithrilPickaxe;
            yield return FloatiumPickaxe;
            yield return SunkiumPickaxe;

            yield return StoneAxe;
            yield return CopperAxe;
            yield return IronAxe;
            yield return GoldAxe;
            yield return MithrilAxe;
            yield return FloatiumAxe;
            yield return SunkiumAxe;

            yield return StoneSword;
            yield return CopperSword;
            yield return IronSword;
            yield return GoldSword;
            yield return MithrilSword;
            yield return FloatiumSword;
            yield return SunkiumSword;

            yield return LeatherTop;
            yield return IronTop;
            yield return MithrilTop;
            yield return SunkiumTop;

            yield return LeatherBottom;
            yield return IronBottom;
            yield return MithrilBottom;
            yield return SunkiumBottom;
        }
    }
}
