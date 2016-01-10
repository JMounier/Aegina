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
    public static readonly Item Log = new Item(0, "Bois", "Un morceau de bois pouvant servir pour créer d'autres objets", 64);
    public static readonly Item Stone = new Item(1, "Pierre", "Une pierre pouvant servir pour créer d'autres objets", 64);
    public static readonly Item Sand = new Item(2, "Sable", "Du sable... Vous pouvez faire un chateau de sable avec.", 64);
    public static readonly Item Copper = new Item(3, "Minerai de cuivre", "Un minerai de cuivre pouvant être fondu en lingot", 64);
    public static readonly Item Iron = new Item(4, "Minerai de fer", "Un minerai de fer pouvant être fondu en lingot", 64);
    public static readonly Item Gold = new Item(5, "Minerai d'or", "Un minerai d'or pouvant être fondu en lingot", 64);
    public static readonly Item Mytril = new Item(6, "Minerai de mytril", "Un minerai de mytril pouvant être fondu en lingot", 64);
    public static readonly Item Floatium = new Item(7, "Minerai de floatium", "Un minerai de floatium pouvant être fondu en lingot", 64);
    public static readonly Item Sunkium = new Item(8, "Minerai de sunkium", "Un minerai de sunkium pouvant être fondu en lingot", 64);
    
    public static readonly Item CopperIngot = new Item(9, "Lingot de cuivre", "Un lingot de cuivre pouvant être utiliser pour construire d'autres objets", 64);
    public static readonly Item IronIngot = new Item(10, "Lingot de fer", "Un lingot de fer pouvant être utiliser pour construire d'autres objets", 64);
    public static readonly Item GoldIngot = new Item(11, "Lingot d'or", "Un lingot d'or pouvant être utiliser pour construire d'autres objets", 64);
    public static readonly Item MytrilIngot = new Item(12, "Lingot de mytril", "Un lingot de mytril pouvant être utiliser pour construire d'autres objets", 64);
    public static readonly Item FloatiumIngot = new Item(13, "Lingot de floatium", "Un lingot de floatium pouvant être utiliser pour construire d'autres objets", 64);
    public static readonly Item SunkiumIngot = new Item(14, "Lingot de sunkium", "Un lingot de sunkium pouvant être utiliser pour construire d'autres objets", 64);
    
    // Tools
    public static readonly Pickaxe WoodenPickaxe = new Pickaxe(50, "Pioche en bois", "Un ensemble de morceaux de bois ressemblant à une pioche", 50, 2);
    public static readonly Pickaxe StonePickaxe = new Pickaxe(51, "Pioche en pierre", "Un outil rudimentaire de pierre ressemblant à une pioche", 200, 3);

    public static readonly Axe WoodenAxe = new Axe(60, "Hache en bois", "Un ensemble de morceaux de bois ressemblant à une hache", 50, 2);
    public static readonly Axe StoneAxe = new Axe(61, "Hache en pierre", "Un outil rudimentaire de pierre ressemblant à une hache", 200, 3);
      
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
