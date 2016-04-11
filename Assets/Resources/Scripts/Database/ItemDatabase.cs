using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// La base de donnée de tous les items.
/// </summary>
public static class ItemDatabase
{
    // Default
    public static readonly Item Default = new Item();

    // Ressources
    public static readonly Item Log = new Item(0, TextDatabase.Log, TextDatabase.LogDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Log"), new Entity(EntityDatabase.Log));
    public static readonly Item Stone = new Item(1, TextDatabase.Stone, TextDatabase.StoneDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Stone"), new Entity(EntityDatabase.Stone));
    public static readonly Item Sand = new Item(2, TextDatabase.Sand, TextDatabase.SandDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Sand"), new Entity(EntityDatabase.Sand));
    public static readonly Item Copper = new Item(3, TextDatabase.Copper, TextDatabase.CopperDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Copper"), new Entity(EntityDatabase.Copper));
    public static readonly Item Iron = new Item(4, TextDatabase.Iron, TextDatabase.IronDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Iron"), new Entity(EntityDatabase.Iron));
    public static readonly Item Gold = new Item(5, TextDatabase.Gold, TextDatabase.GoldDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Gold"), new Entity(EntityDatabase.Gold));
    public static readonly Item Mithril = new Item(6, TextDatabase.Mithril, TextDatabase.MithrilDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Mithril"), new Entity(EntityDatabase.Mithril));
    public static readonly Item Floatium = new Item(7, TextDatabase.Floatium, TextDatabase.FloatiumDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Floatium"), new Entity(EntityDatabase.Floatium));
    public static readonly Item Sunkium = new Item(8, TextDatabase.Sunkium, TextDatabase.SunkiumDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Sunkium"), new Entity(EntityDatabase.Sunkium));

    public static readonly Item CopperIngot = new Item(9, TextDatabase.CopperIngot, TextDatabase.CopperIngotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/CopperIngot"), new Entity(EntityDatabase.CopperIngot));
    public static readonly Item IronIngot = new Item(10, TextDatabase.IronIngot, TextDatabase.IronIngotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/IronIngot"), new Entity(EntityDatabase.IronIngot));
    public static readonly Item GoldIngot = new Item(11, TextDatabase.GoldIngot, TextDatabase.GoldIngotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/GoldIngot"), new Entity(EntityDatabase.GoldIngot));
    public static readonly Item MithrilIngot = new Item(12, TextDatabase.MithrilIngot, TextDatabase.MithrilIngotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/MithrilIngot"), new Entity(EntityDatabase.MithrilIngot));
    public static readonly Item FloatiumIngot = new Item(13, TextDatabase.FloatiumIngot, TextDatabase.FloatiumIngotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/FloatiumIngot"), new Entity(EntityDatabase.FloatiumIngot));
    public static readonly Item SunkiumIngot = new Item(14, TextDatabase.SunkiumIngot, TextDatabase.SunkiumIngotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/SunkiumIngot"), new Entity(EntityDatabase.SunkiumIngot));

    public static readonly Item WoodenPlank = new Item(15, TextDatabase.WoodenPlank, TextDatabase.WoodenPlankDescription, 20, Resources.Load<Texture2D>("Sprites/Items/Elementaries/WoodenPlank"), new Entity(EntityDatabase.WoodenPlank));
    public static readonly Item Glass = new Item(16, TextDatabase.Glass, TextDatabase.GlassDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/Glass"), new Entity(EntityDatabase.Glass));
    public static readonly Item Bowl = new Item(17, TextDatabase.Bowl, TextDatabase.BowlDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/Bowl"), new Entity(EntityDatabase.Bowl));
    public static readonly Item CuttedStone = new Item(18, TextDatabase.CuttedStone, TextDatabase.CuttedStoneDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/CuttedStone"), new Entity(EntityDatabase.CuttedStone));
    // Potions
    public static readonly Consumable AquaPotion = new Consumable(19, TextDatabase.AquaPotion, TextDatabase.AquaPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/AquaPotion"), new Entity(EntityDatabase.AquaPotion), new Effect(Effect.EffectType.Refreshment, 2), Resources.Load<GameObject>("Prefabs/Items/Potions/Aqua"));
    public static readonly Consumable BluePotion = new Consumable(20, TextDatabase.BluePotion, TextDatabase.BluePotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/BluePotion"), new Entity(EntityDatabase.BluePotion), new Effect(Effect.EffectType.Refreshment, 3), Resources.Load<GameObject>("Prefabs/Items/Potions/Blue"));
    public static readonly Consumable GreenPotion = new Consumable(21, TextDatabase.GreenPotion, TextDatabase.GreenPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/GreenPotion"), new Entity(EntityDatabase.GreenPotion), new Effect(Effect.EffectType.Poison, 2), Resources.Load<GameObject>("Prefabs/Items/Potions/Green"));
    public static readonly Consumable PurplePotion = new Consumable(22, TextDatabase.PurplePotion, TextDatabase.PurplePotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/PurplePotion"), new Entity(EntityDatabase.PurplePotion), new Effect(Effect.EffectType.Speed, 1), Resources.Load<GameObject>("Prefabs/Items/Potions/Purple"));
    public static readonly Consumable RedPotion = new Consumable(23, TextDatabase.RedPotion, TextDatabase.RedPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/RedPotion"), new Entity(EntityDatabase.RedPotion), new Effect(Effect.EffectType.Regeneration, 1), Resources.Load<GameObject>("Prefabs/Items/Potions/Red"));
    public static readonly Consumable YellowPotion = new Consumable(24, TextDatabase.YellowPotion, TextDatabase.YellowPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/YellowPotion"), new Entity(EntityDatabase.YellowPotion), new Effect(Effect.EffectType.JumpBoost, 1), Resources.Load<GameObject>("Prefabs/Items/Potions/Yellow"));
    // Foods
    public static readonly Consumable MeatBalls = new Consumable(25, TextDatabase.RedPotion, TextDatabase.RedPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Foods/MeatBalls"), new Entity(EntityDatabase.MeatBalls), new Effect(Effect.EffectType.Saturation, 2), Resources.Load<GameObject>("Prefabs/Items/Foods/MeatBalls"));
    public static readonly Consumable WaterCact = new Consumable(26, TextDatabase.YellowPotion, TextDatabase.YellowPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Foods/WaterCact"), new Entity(EntityDatabase.WaterCact), new Effect(Effect.EffectType.Refreshment, 2), Resources.Load<GameObject>("Prefabs/Items/Foods/WaterCact"));

    // WorckTops
    public static readonly Item Forge = new Item(40, TextDatabase.Forge, TextDatabase.ForgeDescription, 64, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Forge"), new Entity(EntityDatabase.Forge));
    public static readonly WorkTop Cauldron = new WorkTop(41, TextDatabase.Cauldron, TextDatabase.CauldronDescription, 64, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Cauldron"), new Entity(EntityDatabase.CauldronLoot), Resources.Load<GameObject>("Prefabs/Elements/Previsualisations/Cauldron"));
    public static readonly Item Workbench = new Item(42, TextDatabase.Workbench, TextDatabase.WorkbenchDescription, 64, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Workbench"), new Entity(EntityDatabase.Workbench));
    public static readonly Item Firepit = new Item(43, TextDatabase.Firepit, TextDatabase.FirepitDescription, 64, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Firepit"), new Entity(EntityDatabase.Firepit));

    // Tools
    public static readonly Pickaxe StonePickaxe = new Pickaxe(50, TextDatabase.StonePickaxe, TextDatabase.StonePickaxeDescription, 100, 100, Resources.Load<Texture2D>("Sprites/Items/Tools/StonePickaxe"), new Entity(), null);
    public static readonly Pickaxe CopperPickaxe = new Pickaxe(51, TextDatabase.CopperPickaxe, TextDatabase.CopperPickaxeDescription, 150, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperPickaxe"), new Entity(EntityDatabase.CopperPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/CopperPickaxe"));
    public static readonly Pickaxe IronPickaxe = new Pickaxe(52, TextDatabase.IronPickaxe, TextDatabase.IronPickaxeDescription, 250, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/IronPickaxe"), new Entity(EntityDatabase.IronPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/IronPickaxe"));
    public static readonly Pickaxe GoldPickaxe = new Pickaxe(53, TextDatabase.GoldPickaxe, TextDatabase.GoldPickaxeDescription, 50, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldPickaxe"), new Entity(EntityDatabase.GoldPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/GoldPickaxe"));
    public static readonly Pickaxe MithrilPickaxe = new Pickaxe(54, TextDatabase.MithrilPickaxe, TextDatabase.MithrilPickaxeDescription, 200, 200, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilPickaxe"), new Entity(EntityDatabase.MithrilPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/MithrilPickaxe"));
    public static readonly Pickaxe FloatiumPickaxe = new Pickaxe(55, TextDatabase.FloatiumPickaxe, TextDatabase.FloatiumPickaxeDescription, 200, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumPickaxe"), new Entity(EntityDatabase.FloatiumPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/FloatiumPickaxe"));
    public static readonly Pickaxe SunkiumPickaxe = new Pickaxe(56, TextDatabase.SunkiumPickaxe, TextDatabase.SunkiumPickaxeDescription, 500, 300, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumPickaxe"), new Entity(EntityDatabase.SunkiumPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/SunkiumPickaxe"));

    public static readonly Axe StoneAxe = new Axe(60, TextDatabase.StoneAxe, TextDatabase.StoneAxeDescription, 100, 100, Resources.Load<Texture2D>("Sprites/Items/Tools/StoneAxe"), new Entity(EntityDatabase.StoneAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/StoneAxe"));
    public static readonly Axe CopperAxe = new Axe(61, TextDatabase.CopperAxe, TextDatabase.CopperAxeDescription, 150, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperAxe"), new Entity(EntityDatabase.CopperAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/CopperAxe"));
    public static readonly Axe IronAxe = new Axe(62, TextDatabase.IronAxe, TextDatabase.IronAxeDescription, 250, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/IronAxe"), new Entity(EntityDatabase.IronAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/IronAxe"));
    public static readonly Axe GoldAxe = new Axe(63, TextDatabase.GoldAxe, TextDatabase.GoldAxeDescription, 50, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldAxe"), new Entity(EntityDatabase.GoldAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/GoldAxe"));
    public static readonly Axe MithrilAxe = new Axe(64, TextDatabase.MithrilAxe, TextDatabase.MithrilAxeDescription, 200, 200, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilAxe"), new Entity(EntityDatabase.MithrilAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/MithrilAxe"));
    public static readonly Axe FloatiumAxe = new Axe(65, TextDatabase.FloatiumAxe, TextDatabase.FloatiumAxeDescription, 200, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumAxe"), new Entity(EntityDatabase.FloatiumAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/FloatiumAxe"));
    public static readonly Axe SunkiumAxe = new Axe(66, TextDatabase.SunkiumAxe, TextDatabase.SunkiumAxeDescription, 500, 300, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumAxe"), new Entity(EntityDatabase.SunkiumAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/SunkiumAxe"));

    public static readonly Sword StoneSword = new Sword(70, TextDatabase.StoneSword, TextDatabase.StoneSwordDescription, 100, 140, Resources.Load<Texture2D>("Sprites/Items/Tools/StoneSword"), new Entity(EntityDatabase.StoneSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/StoneSword"));
    public static readonly Sword CopperSword = new Sword(71, TextDatabase.CopperSword, TextDatabase.CopperSwordDescription, 150, 180, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperSword"), new Entity(EntityDatabase.CopperSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/CopperSword"));
    public static readonly Sword IronSword = new Sword(72, TextDatabase.IronSword, TextDatabase.IronSwordDescription, 250, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/IronSword"), new Entity(EntityDatabase.IronSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/IronSword"));
    public static readonly Sword GoldSword = new Sword(73, TextDatabase.GoldSword, TextDatabase.GoldSwordDescription, 50, 180, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldSword"), new Entity(EntityDatabase.GoldSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/GoldSword"));
    public static readonly Sword MithrilSword = new Sword(74, TextDatabase.MithrilSword, TextDatabase.MithrilSwordDescription, 200, 200, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilSword"), new Entity(EntityDatabase.MithrilSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/MithrilSword"));
    public static readonly Sword FloatiumSword = new Sword(75, TextDatabase.FloatiumSword, TextDatabase.FloatiumSwordDescription, 200, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumSword"), new Entity(EntityDatabase.FloatiumSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/FloatiumSword"));
    public static readonly Sword SunkiumSword = new Sword(76, TextDatabase.SunkiumSword, TextDatabase.SunkiumSwordDescription, 500, 300, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumSword"), new Entity(EntityDatabase.SunkiumSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/SunkiumSword"));

    public static readonly Item Cact = new Item(80, TextDatabase.WoodenPlank, TextDatabase.WoodenPlankDescription, 80, Resources.Load<Texture2D>("Sprites/Items/Plants/Cact"), new Entity(EntityDatabase.Cact));
    public static readonly Item Petal = new Item(81, TextDatabase.Glass, TextDatabase.GlassDescription, 81, Resources.Load<Texture2D>("Sprites/Items/Plants/Petal"), new Entity(EntityDatabase.Petal));

    /// <summary>
    /// Liste tous les items du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Item> Items
    {
        get
        {
            // Default
            yield return Default;

            // Ressource
            yield return Log;
            yield return Stone;
            yield return Sand;

            yield return Copper;
            yield return Iron;
            yield return Floatium;
            yield return Sunkium;
            yield return Gold;
            yield return Mithril;

            yield return CopperIngot;
            yield return IronIngot;
            yield return GoldIngot;
            yield return MithrilIngot;
            yield return FloatiumIngot;
            yield return SunkiumIngot;

            yield return Cact;
            yield return Petal;
            yield return WoodenPlank;
            yield return Glass;
            yield return CuttedStone;
            yield return Bowl;

            //Workbenches
            yield return Forge;
            yield return Cauldron;
            yield return Workbench;
            yield return Firepit;

            // Tools
            foreach (Pickaxe pickaxe in Pickaxes)
                yield return pickaxe;

            foreach (Axe axe in Axes)
                yield return axe;

            foreach (Sword sword in Swords)
                yield return sword;

            foreach (Consumable cons in Consumables)
                yield return cons;

            foreach (WorkTop work in Worktops)
                yield return work;
        }
    }

    /// <summary>
    /// Liste tous les haches du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Pickaxe> Pickaxes
    {
        get
        {
            yield return StonePickaxe;
            yield return CopperPickaxe;
            yield return IronPickaxe;
            yield return GoldPickaxe;
            yield return MithrilPickaxe;
            yield return FloatiumPickaxe;
            yield return SunkiumPickaxe;
        }
    }

    /// <summary>
    /// Liste tous les pioches du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Axe> Axes
    {
        get
        {
            yield return StoneAxe;
            yield return IronAxe;
            yield return GoldAxe;
            yield return MithrilAxe;
            yield return CopperAxe;
            yield return SunkiumAxe;
            yield return FloatiumAxe;
        }
    }

    /// <summary>
    /// Liste tous les epee du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Sword> Swords
    {
        get
        {
            yield return StoneSword;
            yield return IronSword;
            yield return GoldSword;
            yield return MithrilSword;
            yield return CopperSword;
            yield return SunkiumSword;
            yield return FloatiumSword;
        }
    }

    /// <summary>
    /// Liste tous les consomables du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Consumable> Consumables
    {
        get
        {
            yield return AquaPotion;
            yield return BluePotion;
            yield return GreenPotion;
            yield return PurplePotion;
            yield return RedPotion;
            yield return YellowPotion;
            yield return MeatBalls;
            yield return WaterCact;
        }
    }

    public static IEnumerable<WorkTop> Worktops
    {
        get
        {
            yield return Cauldron;
        }
    }

    /// <summary>
    /// Retourne l'item correspondant a l'identifiant. (La copie)
    /// </summary>
    public static Item Find(int id)
    {
        return Find(id, 0);
    }

    /// <summary>
    /// Retourne l'item correspondant au nom anglais. (La copie)
    /// Ne fait pas attention a la casse.
    /// </summary>
    public static Item Find(string name)
    {
        return Find(name, 0);
    }

    /// <summary>
    /// Retourne l'item correspondant a l'identifiant et au metadonnee. (La copie)
    /// </summary>
    public static Item Find(int id, int meta)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id && i.Meta == meta)
            {
                if (i is Pickaxe)
                    return new Pickaxe((Pickaxe)i);
                else if (i is Axe)
                    return new Axe((Axe)i);
                else if (i is Sword)
                    return new Sword((Sword)i);
                else if (i is Consumable)
                    return new Consumable((Consumable)i);
                else if (i is WorkTop)
                    return new WorkTop((WorkTop)i);
                else
                    return new Item(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }

    /// <summary>
    /// Retourne l'item correspondant au nom anglais et au metadonnee. (La copie)
    /// Ne fait pas attention a la casse.
    /// </summary>
    public static Item Find(string name, int meta)
    {
        foreach (Item i in Items)
        {
            if (i.NameText.GetText(SystemLanguage.English).ToLower().Replace(" ", "") == name.ToLower() && i.Meta == meta)
            {
                if (i is Pickaxe)
                    return new Pickaxe((Pickaxe)i);
                else if (i is Axe)
                    return new Axe((Axe)i);
                else if (i is Sword)
                    return new Sword((Sword)i);
                else if (i is Consumable)
                    return new Consumable((Consumable)i);
                else if (i is WorkTop)
                    return new WorkTop((WorkTop)i);
                else
                    return new Item(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
