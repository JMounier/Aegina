using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Database of all items.
/// </summary>
public static class ItemDatabase
{
    // Default
    public static readonly Item Default = new Item();

    // Ressources
    public static readonly Item Log = new Item(0, new string[] { "Bois","Wood" }, new string[] { "Un morceau de bois pouvant servir pour créer d'autres objets" ,"A piece of wood usable to make other objects"}, 64);
    public static readonly Item Stone = new Item(1, new string[] { "Pierre","Stone" }, new string[] { "Une pierre pouvant servir pour créer d'autres objets","A piece of stone usable to make other objects" }, 64);
    public static readonly Item Sand = new Item(2, new string[] { "Sable","Sand" }, new string[] { "Du sable... Vous pouvez faire un chateau de sable avec.","some sand... you can make a sand castle... " }, 64);
    public static readonly Item Copper = new Item(3, new string[] { "Minerai de cuivre","Copper ore" }, new string[] { "Un minerai de cuivre pouvant être fondu en lingot","A copper ore meltable in ingot" }, 64);
    public static readonly Item Iron = new Item(4, new string[] { "Minerai de fer","Iron ore" }, new string[] { "Un minerai de fer pouvant être fondu en lingot","An iron ore meltable in ingot" }, 64);
    public static readonly Item Gold = new Item(5, new string[] { "Minerai d'or","Gold ore" }, new string[] { "Un minerai d'or pouvant être fondu en lingot","A gold ore meltable in ingot" }, 64);
    public static readonly Item Mytril = new Item(6, new string[] { "Minerai de mytril","Mytril ore" }, new string[] { "Un minerai de mytril pouvant être fondu en lingot","A mytril ore meltable in ingot" }, 64);
    public static readonly Item Floatium = new Item(7, new string[] { "Minerai de floatium","Floatium ore" }, new string[] { "Un minerai de floatium pouvant être fondu en lingot","A floatium ore meltable in ingot" }, 64);
    public static readonly Item Sunkium = new Item(8, new string[] { "Minerai de sunkium","Sunkium ore" }, new string[] { "Un minerai de sunkium pouvant être fondu en lingot","A sunkium ore meltable in ingot" }, 64);
    
    public static readonly Item CopperIngot = new Item(9, new string[] { "Lingot de cuivre","Copper ingot" }, new string[] { "Un lingot de cuivre pouvant être utiliser pour construire d'autres objets","A copper ingot usable to make other objects" }, 64);
    public static readonly Item IronIngot = new Item(10, new string[] { "Lingot de fer","Iron ingot" }, new string[] { "Un lingot de fer pouvant être utiliser pour construire d'autres objets","An iron ingot usable to make other objects" }, 64);
    public static readonly Item GoldIngot = new Item(11, new string[] { "Lingot d'or","Gold ingot" }, new string[] { "Un lingot d'or pouvant être utiliser pour construire d'autres objets","A gold ingot usable to make other objetcs" }, 64);
    public static readonly Item MytrilIngot = new Item(12, new string[] { "Lingot de mytril","Mytril ingot" }, new string[] { "Un lingot de mytril pouvant être utiliser pour construire d'autres objets","A mytril ingot usable to make other objects" }, 64);
    public static readonly Item FloatiumIngot = new Item(13, new string[] { "Lingot de floatium","Floatium ingot" }, new string[] { "Un lingot de floatium pouvant être utiliser pour construire d'autres objets","A floatium ingot usable to make other objects" }, 64);
    public static readonly Item SunkiumIngot = new Item(14, new string[] { "Lingot de sunkium","Sunkium ingot" }, new string[] { "Un lingot de sunkium pouvant être utiliser pour construire d'autres objets","A sunkium ingot usable to make other objects" }, 64);
    
    // Tools
    public static readonly Pickaxe WoodenPickaxe = new Pickaxe(50, new string[] { "Pioche en bois","Wooden pickaxe" }, new string[] { "Un ensemble de morceaux de bois ressemblant à une pioche","A set of wood pieces looking like a pickaxe" }, 50, 2);
    public static readonly Pickaxe StonePickaxe = new Pickaxe(51, new string[] { "Pioche en pierre","Stone pickaxe" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une pioche","A rudimentary stone tool looking like a pickaxe" }, 200, 3);

    public static readonly Axe WoodenAxe = new Axe(60, new string[] { "Hache en bois","Wooden ax" }, new string[] { "Un ensemble de morceaux de bois ressemblant à une hache","A set of wood pieces looking like an ax" }, 50, 2);
    public static readonly Axe StoneAxe = new Axe(61, new string[] { "Hache en pierre","Stone ax" }, new string[] { "Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like an ax" }, 200, 3);
      
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
            yield return Mytril;

            yield return CopperIngot;
            yield return IronIngot;
            yield return GoldIngot;
            yield return MytrilIngot;
            yield return FloatiumIngot;
            yield return SunkiumIngot;

            // Tools
            yield return WoodenPickaxe;
            yield return StonePickaxe;

            yield return WoodenAxe;
            yield return StoneAxe;       
        }
    }

    public static Item Find(int id)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id && i.Meta == 0)
                return i;
        }
        throw new System.Exception("Items.Find : Item not find");
    }

    public static Item Find(int id, int meta)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id && i.Meta == meta)
                return i;
        }
        throw new System.Exception("Items.Find : Item not find");
    }

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
