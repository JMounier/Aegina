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

    // Tools
    public static readonly Pickaxe WoodenPickaxe = new Pickaxe(50, TextDatabase.WoodenPickaxe, TextDatabase.WoodenPickaxeDescription, 50, 2, Resources.Load<Texture2D>("Sprites/Items/Tools/WoodenPickaxe"), new Entity());
    public static readonly Pickaxe StonePickaxe = new Pickaxe(51, TextDatabase.StonePickaxe, TextDatabase.StonePickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/StonePickaxe"), new Entity());
    public static readonly Pickaxe CopperPickaxe = new Pickaxe(52, TextDatabase.CopperPickaxe, TextDatabase.CopperPickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperPickaxe"), new Entity(EntityDatabase.CopperPickaxe));
    public static readonly Pickaxe IronPickaxe = new Pickaxe(53, TextDatabase.IronPickaxe, TextDatabase.IronPickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/IronPickaxe"), new Entity(EntityDatabase.IronPickaxe));
    public static readonly Pickaxe GoldPickaxe = new Pickaxe(54, TextDatabase.GoldPickaxe, TextDatabase.GoldPickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldPickaxe"), new Entity(EntityDatabase.GoldPickaxe));
    public static readonly Pickaxe MithrilPickaxe = new Pickaxe(55, TextDatabase.MithrilPickaxe, TextDatabase.MithrilPickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilPickaxe"), new Entity(EntityDatabase.MithrilPickaxe));
    public static readonly Pickaxe FloatiumPickaxe = new Pickaxe(56, TextDatabase.FloatiumPickaxe, TextDatabase.FloatiumPickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumPickaxe"), new Entity(EntityDatabase.FloatiumPickaxe));
    public static readonly Pickaxe SunkiumPickaxe = new Pickaxe(57, TextDatabase.SunkiumPickaxe, TextDatabase.SunkiumPickaxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumPickaxe"), new Entity(EntityDatabase.SunkiumPickaxe));

    public static readonly Axe WoodenAxe = new Axe(60, TextDatabase.WoodenAxe, TextDatabase.WoodenAxeDescription, 50, 2, Resources.Load<Texture2D>("Sprites/Items/Tools/WoodenAxe"), new Entity());
    public static readonly Axe StoneAxe = new Axe(61, TextDatabase.StoneAxe, TextDatabase.StoneAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/StoneAxe"), new Entity());
    public static readonly Axe CopperAxe = new Axe(62, TextDatabase.CopperAxe, TextDatabase.CopperAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Copperaxe"), new Entity());
    public static readonly Axe IronAxe = new Axe(63, TextDatabase.IronAxe, TextDatabase.IronAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Ironaxe"), new Entity());
    public static readonly Axe GoldAxe = new Axe(64, TextDatabase.GoldAxe, TextDatabase.GoldAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Goldaxe"), new Entity());
    public static readonly Axe MithrilAxe = new Axe(65, TextDatabase.MithrilAxe, TextDatabase.MithrilAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Mithrilaxe"), new Entity());
    public static readonly Axe FloatiumAxe = new Axe(66, TextDatabase.FloatiumAxe, TextDatabase.FloatiumAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Floatiumaxe"), new Entity());
    public static readonly Axe SunkiumAxe = new Axe(67, TextDatabase.SunkiumAxe, TextDatabase.SunkiumAxeDescription, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Sunkiumaxe"), new Entity());

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

            // Tools
            foreach (Pickaxe pickaxe in Pickaxes)
                yield return pickaxe;

            foreach (Axe axe in Axes)
                yield return axe;
        }
    }

    /// <summary>
    /// Liste tous les haches du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Pickaxe> Pickaxes
    {
        get
        {
            yield return WoodenPickaxe;
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
            yield return WoodenAxe;
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
    /// Retourne l'item correspondant a l'identifiant. (La copie)
    /// </summary>
    public static Item Find(int id)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id && i.Meta == 0)
            {
                if (i is Pickaxe)
                    return new Pickaxe((Pickaxe)i);
                else if (i is Axe)
                    return new Axe((Axe)i);
                else
                    return new Item(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
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
                else
                    return new Item(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
