using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CraftDatabase
{
    public static readonly Craft FireCamp = new Craft(Craft.Type.WorkTop);
    public static readonly Craft Workbench = new Craft(Craft.Type.WorkTop);
    public static readonly Craft Forge = new Craft(Craft.Type.WorkTop);
    public static readonly Craft Brewer = new Craft(Craft.Type.WorkTop);

    public static readonly Craft Stick = new Craft(Craft.Type.Elementary);
    public static readonly Craft Plank = new Craft(Craft.Type.Elementary);
    public static readonly Craft Cup = new Craft(Craft.Type.Elementary);
    public static readonly Craft CutStone = new Craft(Craft.Type.Elementary);
    public static readonly Craft CopperIngot = new Craft(Craft.Type.Elementary);
    public static readonly Craft IronIngot = new Craft(10, new ItemStack(ItemDatabase.IronIngot, 1), false, false, true, false,Craft.Type.Elementary, new ItemStack(ItemDatabase.Iron, 3));
    public static readonly Craft GoldIngot = new Craft(Craft.Type.Elementary);
    public static readonly Craft MithrilIngot = new Craft(Craft.Type.Elementary);
    public static readonly Craft FLoatiumIngot = new Craft(Craft.Type.Elementary);
    public static readonly Craft SunkiumIngot= new Craft(Craft.Type.Elementary);
    public static readonly Craft Glass = new Craft(Craft.Type.Elementary);

    public static readonly Craft CactusAndFlowerSalad = new Craft(Craft.Type.Consumable);
    public static readonly Craft MeatBrochette = new Craft(Craft.Type.Consumable);
    public static readonly Craft Stew = new Craft(Craft.Type.Consumable);
    public static readonly Craft MarrowBone = new Craft(Craft.Type.Consumable);
    public static readonly Craft BeefBourguignon = new Craft(Craft.Type.Consumable);
    public static readonly Craft Potion1 = new Craft(Craft.Type.Consumable);
    public static readonly Craft Potion2 = new Craft(Craft.Type.Consumable);
    public static readonly Craft Potion3 = new Craft(Craft.Type.Consumable);
    public static readonly Craft Potion4 = new Craft(Craft.Type.Consumable);
    public static readonly Craft Potion5 = new Craft(Craft.Type.Consumable);
    public static readonly Craft Potion6 = new Craft(Craft.Type.Consumable);
    public static readonly Craft water = new Craft(Craft.Type.Consumable);

    public static readonly Craft StonePickaxe = new Craft(Craft.Type.Tools);
    public static readonly Craft CopperPickaxe = new Craft(Craft.Type.Tools);
    public static readonly Craft IronPickaxe = new Craft(Craft.Type.Tools);
    public static readonly Craft GoldPickaxe = new Craft(Craft.Type.Tools);
    public static readonly Craft MithrilPickaxe = new Craft(Craft.Type.Tools);
    public static readonly Craft FloatiumPickaxe = new Craft(Craft.Type.Tools);
    public static readonly Craft SunkiumPickaxe = new Craft(Craft.Type.Tools);

    public static readonly Craft StoneAxe = new Craft(Craft.Type.Tools);
    public static readonly Craft CopperAxe = new Craft(Craft.Type.Tools);
    public static readonly Craft IronAxe = new Craft(Craft.Type.Tools);
    public static readonly Craft GoldAxe = new Craft(Craft.Type.Tools);
    public static readonly Craft MithrilAxe = new Craft(Craft.Type.Tools);
    public static readonly Craft FloatiumAxe = new Craft(Craft.Type.Tools);
    public static readonly Craft SunkiumAxe = new Craft(Craft.Type.Tools);

    public static readonly Craft StoneSword = new Craft(Craft.Type.Tools);
    public static readonly Craft CopperSword = new Craft(Craft.Type.Tools);
    public static readonly Craft IronSword = new Craft(Craft.Type.Tools);
    public static readonly Craft GoldSword = new Craft(Craft.Type.Tools);
    public static readonly Craft MithrilSword = new Craft(Craft.Type.Tools);
    public static readonly Craft FloatiumSword = new Craft(Craft.Type.Tools);
    public static readonly Craft SunkiumSword = new Craft(Craft.Type.Tools);

    public static readonly Craft LeatherTop = new Craft(Craft.Type.Armor);
    public static readonly Craft CopperTop = new Craft(Craft.Type.Armor);
    public static readonly Craft IronTop = new Craft(Craft.Type.Armor);
    public static readonly Craft GoldTop = new Craft(Craft.Type.Armor);
    public static readonly Craft MithrilTop = new Craft(Craft.Type.Armor);
    public static readonly Craft FLoatiumTop = new Craft(Craft.Type.Armor);
    public static readonly Craft SunkiumTop = new Craft(Craft.Type.Armor);

    public static readonly Craft LeatherBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft CopperBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft IronBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft GoldBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft MithrilBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft FLoatiumBottom = new Craft(Craft.Type.Armor);
    public static readonly Craft SunkiumBottom = new Craft(Craft.Type.Armor);



    public static IEnumerable<Craft> Crafts
    {
        get
        {
            yield return FireCamp;
            yield return Workbench;
            yield return Forge;
            yield return Brewer;

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

            yield return CactusAndFlowerSalad;
            yield return MeatBrochette;
            yield return Stew;
            yield return MarrowBone;
            yield return BeefBourguignon;
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
            yield return CopperTop;
            yield return IronTop;
            yield return GoldTop;
            yield return MithrilTop;
            yield return FLoatiumTop;
            yield return SunkiumTop;

            yield return LeatherBottom;
            yield return CopperBottom;
            yield return IronBottom;
            yield return GoldBottom;
            yield return MithrilBottom;
            yield return FLoatiumBottom;
            yield return SunkiumBottom;
        }
    }
}
