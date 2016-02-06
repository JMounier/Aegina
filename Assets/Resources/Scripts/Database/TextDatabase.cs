using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// La base de donnée de tous les texte.
/// </summary>
public class TextDatabase
{
    // Default 
    public static readonly Text Default = new Text();

    // Ressource 
        // Name
    public static readonly Text Log = new Text("Bois", "Wood");
    public static readonly Text Stone = new Text("Pierre", "Stone");
    public static readonly Text Sand = new Text("Sable", "Sand");
    public static readonly Text Copper = new Text("Minerai de cuivre", "Copper ore");
    public static readonly Text Iron = new Text("Minerai de fer", "Iron ore");
    public static readonly Text Gold = new Text("Minerai d'or", "Gold ore");
    public static readonly Text Mithril = new Text("Minerai de mytril", "Mytril ore");
    public static readonly Text Floatium = new Text("Minerai de floatium", "Floatium ore");
    public static readonly Text Sunkium = new Text("Minerai de sunkium", "Sunkium ore");

    public static readonly Text CopperIngot = new Text("Lingot de cuivre", "Copper ingot");
    public static readonly Text IronIngot = new Text("Lingot de fer", "Iron ingot");
    public static readonly Text GoldIngot = new Text("Lingot d'or", "Gold ingot");
    public static readonly Text MithrilIngot = new Text("Lingot de mytril", "Mytril ingot");
    public static readonly Text FloatiumIngot = new Text("Lingot de floatium", "Floatium ingot");
    public static readonly Text SunkiumIngot = new Text("Lingot de sunkium", "Sunkium ingot");

        // Description
    public static readonly Text LogDescription = new Text("Un morceau de bois pouvant servir pour créer d'autres objets", "A piece of wood usable to make other objects");
    public static readonly Text StoneDescription = new Text("Une pierre pouvant servir pour créer d'autres objets", "A piece of stone usable to make other objects");
    public static readonly Text SandDescription = new Text("Du sable... Vous pouvez faire un chateau de sable avec.", "some sand... you can make a sand castle... ");
    public static readonly Text CopperDescription = new Text("Un minerai de cuivre pouvant être fondu en lingot", "A copper ore meltable in ingot");
    public static readonly Text IronDescription = new Text("Un minerai de fer pouvant être fondu en lingot", "An iron ore meltable in ingot");
    public static readonly Text GoldDescription = new Text("Un minerai d'or pouvant être fondu en lingot", "A gold ore meltable in ingot");
    public static readonly Text MithrilDescription = new Text("Un minerai de mithril pouvant être fondu en lingot", "A mithril ore meltable in ingot");
    public static readonly Text FloatiumDescription = new Text("Un minerai de floatium pouvant être fondu en lingot", "A floatium ore meltable in ingot");
    public static readonly Text SunkiumDescription = new Text("Un minerai de sunkium pouvant être fondu en lingot", "A sunkium ore meltable in ingot");

    public static readonly Text CopperIngotDescription = new Text("Un lingot de cuivre pouvant être utiliser pour construire d'autres objets", "A copper ingot usable to make other objects");
    public static readonly Text IronIngotDescription = new Text("Un lingot de fer pouvant être utiliser pour construire d'autres objets", "An iron ingot usable to make other objects");
    public static readonly Text GoldIngotDescription = new Text( "Un lingot d'or pouvant être utiliser pour construire d'autres objets", "A gold ingot usable to make other objetcs");
    public static readonly Text MithrilIngotDescription = new Text("Un lingot de mithril pouvant être utiliser pour construire d'autres objets", "A mithril ingot usable to make other objects");
    public static readonly Text FloatiumIngotDescription = new Text("Un lingot de floatium pouvant être utiliser pour construire d'autres objets", "A floatium ingot usable to make other objects");
    public static readonly Text SunkiumIngotDescription = new Text("Un lingot de sunkium pouvant être utiliser pour construire d'autres objets", "A sunkium ingot usable to make other objects");

    // Tools
        // Name
    public static readonly Text WoodenPickaxe = new Text("Pioche en bois", "Wooden pickaxe");
    public static readonly Text StonePickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text CopperPickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text IronPickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text GoldPickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text MithrilPickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text FloatiumPickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text SunkiumPickaxe = new Text("Pioche en pierre", "Stone pickaxe");

    public static readonly Text WoodenAxe = new Text("Hache en bois", "Wooden ax");
    public static readonly Text StoneAxe = new Text("Hache en pierre", "Stone ax");
    public static readonly Text CopperAxe = new Text("Hache en pierre", "Stone Axe");
    public static readonly Text IronAxe = new Text("Hache en pierre", "Stone Axe");
    public static readonly Text GoldAxe = new Text("Hache en pierre", "Stone Axe");
    public static readonly Text MithrilAxe = new Text("Hache en pierre", "Stone Axe");
    public static readonly Text FloatiumAxe = new Text("Hache en pierre", "Stone Axe");
    public static readonly Text SunkiumAxe = new Text("Hache en pierre", "Stone Axe");

    // Description

    public static readonly Text WoodenPickaxeDescription = new Text("Un ensemble de morceaux de bois ressemblant à une pioche", "A set of wood pieces looking like a pickaxe");
    public static readonly Text StonePickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text CopperPickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text IronPickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text GoldPickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text MithrilPickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text FloatiumPickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text SunkiumPickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");


    public static readonly Text WoodenAxeDescription = new Text("Un ensemble de morceaux de bois ressemblant à une hache", "A set of wood pieces looking like an ax");
    public static readonly Text StoneAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like an ax");
    public static readonly Text CopperAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like a Axe");
    public static readonly Text IronAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like a Axe");
    public static readonly Text GoldAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like a Axe");
    public static readonly Text MithrilAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like a Axe");
    public static readonly Text FloatiumAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like a Axe");
    public static readonly Text SunkiumAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like a Axe");

    
}



