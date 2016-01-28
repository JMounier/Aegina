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
    public static readonly Item Log = new Item(0, new string[] { "Bois","Wood" }, new string[] { "Un morceau de bois pouvant servir pour créer d'autres objets" ,"A piece of wood usable to make other objects"}, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Log"), new Entity(EntityDatabase.Log));
    public static readonly Item Stone = new Item(1, new string[] { "Pierre","Stone" }, new string[] { "Une pierre pouvant servir pour créer d'autres objets","A piece of stone usable to make other objects" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Stone"), new Entity());
    public static readonly Item Sand = new Item(2, new string[] { "Sable","Sand" }, new string[] { "Du sable... Vous pouvez faire un chateau de sable avec.","some sand... you can make a sand castle... " }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Sand"), new Entity());
    public static readonly Item Copper = new Item(3, new string[] { "Minerai de cuivre","Copper ore" }, new string[] { "Un minerai de cuivre pouvant être fondu en lingot","A copper ore meltable in ingot" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Copper"), new Entity());
    public static readonly Item Iron = new Item(4, new string[] { "Minerai de fer","Iron ore" }, new string[] { "Un minerai de fer pouvant être fondu en lingot","An iron ore meltable in ingot" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Iron"), new Entity());
    public static readonly Item Gold = new Item(5, new string[] { "Minerai d'or","Gold ore" }, new string[] { "Un minerai d'or pouvant être fondu en lingot","A gold ore meltable in ingot" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Gold"), new Entity());
    public static readonly Item Mithril = new Item(6, new string[] { "Minerai de mytril","Mytril ore" }, new string[] { "Un minerai de mithril pouvant être fondu en lingot","A mithril ore meltable in ingot" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Mithril"), new Entity());
    public static readonly Item Floatium = new Item(7, new string[] { "Minerai de floatium","Floatium ore" }, new string[] { "Un minerai de floatium pouvant être fondu en lingot","A floatium ore meltable in ingot" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Floatium"), new Entity());
    public static readonly Item Sunkium = new Item(8, new string[] { "Minerai de sunkium","Sunkium ore" }, new string[] { "Un minerai de sunkium pouvant être fondu en lingot","A sunkium ore meltable in ingot" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ores/Sunkium"), new Entity());
    
    public static readonly Item CopperIngot = new Item(9, new string[] { "Lingot de cuivre","Copper ingot" }, new string[] { "Un lingot de cuivre pouvant être utiliser pour construire d'autres objets","A copper ingot usable to make other objects" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/CopperIngot"), new Entity());
    public static readonly Item IronIngot = new Item(10, new string[] { "Lingot de fer","Iron ingot" }, new string[] { "Un lingot de fer pouvant être utiliser pour construire d'autres objets","An iron ingot usable to make other objects" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/IronIngot"), new Entity());
    public static readonly Item GoldIngot = new Item(11, new string[] { "Lingot d'or","Gold ingot" }, new string[] { "Un lingot d'or pouvant être utiliser pour construire d'autres objets","A gold ingot usable to make other objetcs" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/GoldIngot"), new Entity());
    public static readonly Item MithrilIngot = new Item(12, new string[] { "Lingot de mytril","Mytril ingot" }, new string[] { "Un lingot de mithril pouvant être utiliser pour construire d'autres objets","A mithril ingot usable to make other objects" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/MithrilIngot"), new Entity());
    public static readonly Item FloatiumIngot = new Item(13, new string[] { "Lingot de floatium","Floatium ingot" }, new string[] { "Un lingot de floatium pouvant être utiliser pour construire d'autres objets","A floatium ingot usable to make other objects" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/FloatiumIngot"), new Entity());
    public static readonly Item SunkiumIngot = new Item(14, new string[] { "Lingot de sunkium","Sunkium ingot" }, new string[] { "Un lingot de sunkium pouvant être utiliser pour construire d'autres objets","A sunkium ingot usable to make other objects" }, 64, Resources.Load<Texture2D>("Sprites/Items/Ingots/SunkiumIngot"), new Entity());
    
    // Tools
    public static readonly Pickaxe WoodenPickaxe = new Pickaxe(50, new string[] { "Pioche en bois","Wooden pickaxe" }, new string[] { "Un ensemble de morceaux de bois ressemblant à une pioche","A set of wood pieces looking like a pickaxe" }, 50, 2, Resources.Load<Texture2D>("Sprites/Items/Tools/WoodenPickaxe"), new Entity());
    public static readonly Pickaxe StonePickaxe = new Pickaxe(51, new string[] { "Pioche en pierre","Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche","A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/StonePickaxe"), new Entity());
    public static readonly Pickaxe CopperPickaxe = new Pickaxe(52, new string[] { "Pioche en pierre", "Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperPickaxe"), new Entity());
    public static readonly Pickaxe IronPickaxe = new Pickaxe(53, new string[] { "Pioche en pierre", "Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/IronPickaxe"), new Entity());
    public static readonly Pickaxe GoldPickaxe = new Pickaxe(54, new string[] { "Pioche en pierre", "Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldPickaxe"), new Entity());
    public static readonly Pickaxe MithrilPickaxe = new Pickaxe(55, new string[] { "Pioche en pierre", "Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilPickaxe"), new Entity());
    public static readonly Pickaxe FloatiumPickaxe = new Pickaxe(56, new string[] { "Pioche en pierre", "Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumPickaxe"), new Entity());
    public static readonly Pickaxe SunkiumPickaxe = new Pickaxe(57, new string[] { "Pioche en pierre", "Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumPickaxe"), new Entity());

    public static readonly Axe WoodenAxe = new Axe(60, new string[] { "Hache en bois","Wooden ax" }, new string[] { "Un ensemble de morceaux de bois ressemblant à une hache","A set of wood pieces looking like an ax" }, 50, 2, Resources.Load<Texture2D>("Sprites/Items/Tools/WoodenAxe"), new Entity());
    public static readonly Axe StoneAxe = new Axe(61, new string[] { "Hache en pierre","Stone ax" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like an ax" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/StoneAxe"), new Entity());
    public static readonly Axe CopperAxe = new Axe(52, new string[] { "Hache en pierre", "Stone Axe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a Axe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Copperaxe"), new Entity());
    public static readonly Axe IronAxe = new Axe(53, new string[] { "Hache en pierre", "Stone Axe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a Axe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Ironaxe"), new Entity());
    public static readonly Axe GoldAxe = new Axe(54, new string[] { "Hache en pierre", "Stone Axe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a Axe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Goldaxe"), new Entity());
    public static readonly Axe MithrilAxe = new Axe(55, new string[] { "Hache en pierre", "Stone Axe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a Axe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Mithrilaxe"), new Entity());
    public static readonly Axe FloatiumAxe = new Axe(56, new string[] { "Hache en pierre", "Stone Axe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a Axe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Floatiumaxe"), new Entity());
    public static readonly Axe SunkiumAxe = new Axe(57, new string[] { "Hache en pierre", "Stone Axe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a Axe" }, 200, 3, Resources.Load<Texture2D>("Sprites/Items/Tools/Sunkiumaxe"), new Entity());

    /// <summary>
    /// Liste tous les items du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Item> Items
    {
        get
        {
            // Default
            yield return new Item(Default);

            // Ressource
            yield return new Item(Log);
            yield return new Item(Stone);
            yield return new Item(Sand);

            yield return new Item(Copper);
            yield return new Item(Iron);
            yield return new Item(Floatium);
            yield return new Item(Sunkium);
            yield return new Item(Gold);
            yield return new Item(Mithril);

            yield return new Item(CopperIngot);
            yield return new Item(IronIngot);
            yield return new Item(GoldIngot);
            yield return new Item(MithrilIngot);
            yield return new Item(FloatiumIngot);
            yield return new Item(SunkiumIngot);

            // Tools
            yield return new Pickaxe(WoodenPickaxe);
            yield return new Pickaxe(StonePickaxe);
            yield return new Pickaxe(CopperPickaxe);
            yield return new Pickaxe(IronPickaxe);
            yield return new Pickaxe(GoldPickaxe);
            yield return new Pickaxe(MithrilPickaxe);
            yield return new Pickaxe(FloatiumPickaxe);
            yield return new Pickaxe(SunkiumPickaxe);

            yield return new Axe(WoodenAxe);
            yield return new Axe(StoneAxe);
            yield return new Axe(IronAxe);
            yield return new Axe(GoldAxe);
            yield return new Axe(MithrilAxe);
            yield return new Axe(CopperAxe);
            yield return new Axe(SunkiumAxe);
            yield return new Axe(FloatiumAxe);

        }
    }

    /// <summary>
    /// Retourne l'item correspondant.
    /// </summary>
    public static Item Find(int id)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id && i.Meta == 0)
                return i;
        }
        throw new System.Exception("Items.Find : Item not find");
    }

    /// <summary>
    /// Retourne l'item correspondant.
    /// </summary>
    public static Item Find(int id, int meta)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id && i.Meta == meta)
                return i;
        }
        throw new System.Exception("Items.Find : Item not find");
    }

    /// <summary>
    /// Retourne l'item correspondant.
    /// </summary>
    public static Item Find(string name)
    {
        foreach (Item i in Items)
        {
            if (i.Name == name)
                return i;
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
