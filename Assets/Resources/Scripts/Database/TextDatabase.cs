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
    public static readonly Text CopperPickaxe = new Text("Pioche en copper", "Copper pickaxe");
    public static readonly Text IronPickaxe = new Text("Pioche en fer", "Iron pickaxe");
    public static readonly Text GoldPickaxe = new Text("Pioche en or", "Gold pickaxe");
    public static readonly Text MithrilPickaxe = new Text("Pioche en mithril", "Mithril pickaxe");
    public static readonly Text FloatiumPickaxe = new Text("Pioche en floatium", "Floatium pickaxe");
    public static readonly Text SunkiumPickaxe = new Text("Pioche en sunkium", "Sunkium pickaxe");

    public static readonly Text WoodenAxe = new Text("Hache en bois", "Wooden axe");
    public static readonly Text StoneAxe = new Text("Hache en pierre", "Stone axe");
    public static readonly Text CopperAxe = new Text("Hache en copper", "Copper axe");
    public static readonly Text IronAxe = new Text("Hache en fer", "Iron axe");
    public static readonly Text GoldAxe = new Text("Hache en or", "Gold axe");
    public static readonly Text MithrilAxe = new Text("Hache en mithril", "Mithril axe");
    public static readonly Text FloatiumAxe = new Text("Hache en floatium", "Floatium axe");
    public static readonly Text SunkiumAxe = new Text("Hache en sunkium", "Sunkium axe");

    // Description

    public static readonly Text WoodenPickaxeDescription = new Text("Un ensemble de morceaux de bois ressemblant à une pioche", "A set of wood pieces looking like a pickaxe");
    public static readonly Text StonePickaxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une pioche", "A rudimentary stone tool looking like a pickaxe");
    public static readonly Text CopperPickaxeDescription = new Text("Une pioche de qualite douteuse", "Questionable quality pickaxe");
    public static readonly Text IronPickaxeDescription = new Text("Une bonne pioche", "A good pickaxe");
    public static readonly Text GoldPickaxeDescription = new Text("Une pioche pour riche", "An pickaxe to rich");
    public static readonly Text MithrilPickaxeDescription = new Text("Une pioche legendaire", "A legendary pickaxe");
    public static readonly Text FloatiumPickaxeDescription = new Text("Une pioche de solidite infaillible", "A foolproof solidity pickaxe");
    public static readonly Text SunkiumPickaxeDescription = new Text("Une pioche de qualite", "A quality pickaxe");


    public static readonly Text WoodenAxeDescription = new Text("Un ensemble de morceaux de bois ressemblant à une hache", "A set of wood pieces looking like an axe");
    public static readonly Text StoneAxeDescription = new Text("Un outil rudimentaire de pierre ressemblant à une hache", "A rudimentary stone tool looking like an axe");
    public static readonly Text CopperAxeDescription = new Text("Une hache de qualite douteuse", "Questionable quality axe");
    public static readonly Text IronAxeDescription = new Text("Une bonne hache", "A good axe");
    public static readonly Text GoldAxeDescription = new Text("Une hache pour riche", "An axe to rich");
    public static readonly Text MithrilAxeDescription = new Text("Une hache legendaire", "A legendary axe");
    public static readonly Text FloatiumAxeDescription = new Text("Une hache de solidite infaillible", "A foolproof solidity axe");
    public static readonly Text SunkiumAxeDescription = new Text("Une hache de qualite", "A quality axe");

    // Miscellanous

    public static readonly Text Quit = new Text("Quitter", "Quit");
    public static readonly Text Continue = new Text("Continuer", "Continue");
    public static readonly Text Sound = new Text("Son", "Sound");
    public static readonly Text Back = new Text("Retour", "Back");
    public static readonly Text Language = new Text("Langue", "Language");
    public static readonly Text French = new Text("Français", "French");
    public static readonly Text English = new Text("Anglais", "English");
    public static readonly Text Play = new Text("Jouer(H)", "Play(H)");
    public static readonly Text Join = new Text("Rejoindre(C)", "Join(C)");
    public static readonly Text Activate = new Text("Activer", "Activate");
    public static readonly Text Validate = new Text("Valider", "Validate");
    public static readonly Text Cancel = new Text("Annuler", "Cancel");
    public static readonly Text Loading = new Text("Chargement...", "Loading...");
    public static readonly Text Settings = new Text("Options", "Settings");
}



