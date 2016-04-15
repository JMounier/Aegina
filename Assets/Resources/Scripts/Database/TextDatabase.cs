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

    public static readonly Text Forge = new Text("Forge", "Forge");
    public static readonly Text Cauldron = new Text("Chaudron", "Cauldron");
    public static readonly Text Workbench = new Text("Table de travail", "Workbench");
    public static readonly Text Firepit = new Text("Feu", "Firepit");

    public static readonly Text AquaPotion = new Text("Potion aqua", "Aqua potion");
    public static readonly Text BluePotion = new Text("Potion bleu", "Blue potion");
    public static readonly Text GreenPotion = new Text("Potion verte", "Green potion");
    public static readonly Text PurplePotion = new Text("Potion violette", "Purple potion");
    public static readonly Text RedPotion = new Text("Potion rouge", "Red potion");
    public static readonly Text YellowPotion = new Text("Potion jaune", "Yellow potion");

    public static readonly Text WoodenPlank = new Text("Planche de bois", "Wooden plank");
    public static readonly Text Glass = new Text("Verre", "Glass");
    public static readonly Text Bowl = new Text("Bol", "Bowl");
    public static readonly Text CuttedStone = new Text("Pierre taillé", "Cutted stone");
    public static readonly Text Stick = new Text("C'est un bout de bois", "It's a piece of wood");
    public static readonly Text Hide = new Text("Du cuir, j'avais justement besoin de nouveaux vêtements", "Some hide, I needed some new clothes");
    public static readonly Text Gigot = new Text("Cuisiné ça pourrais être bon", "Cooked it could be good");
    public static readonly Text Fang = new Text("C'est une dent", "It's a fang");


    public static readonly Text Apple = new Text("Une pomme bien rouge et juteuse", "A bright red and juicy apple");
    public static readonly Text Cact = new Text("Un petit cactus avec une fleur dessus. Il semble contenir une bonne quantite d'eau", "A little cactus with a flozer on it. It seem to contain a good amount of water");
    public static readonly Text CactPetal = new Text("Un petale de fleur. C'est jolie mais peut utile. Vous n'allez pas le manger tout de meme ?", "A flower petal. It's beautiful but useless. You will not eat it, is it ? ");
    public static readonly Text Mushroom = new Text("Un champignon à l'air comestible", "Mushroom which seems edible");
    public static readonly Text RedMushroom = new Text("Un champignon pourrait être comestible", "Mushroom which could be edible");
    public static readonly Text WaterCact = new Text("De l'eau dans un cactus pour etancher la soif", "Water in a cactus to quench his thirst");
    public static readonly Text MeatBall = new Text("Viannnnnde", "Meaaaaat");

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

    public static readonly Text ForgeDescription = new Text("Une forge rudimentaire faite avec quelques pierres. Cette forge permet de faire fondre des metaux", " A rudimentary forge made with some stone. This forge can melt metal");
    public static readonly Text CauldronDescription = new Text("Un chaudron de sorciere, ideal pour faire des potions", "A witch cauldron ideal to make potion");
    public static readonly Text WorkbenchDescription = new Text("Une table de travail pouvant servir pour... travailler", "A workbench usable to... work");
    public static readonly Text FirepitDescription = new Text("Feu !!! Viande !!! Repas !!!", "Fire !!! Meat !!! Meal !!!");

    public static readonly Text AquaPotionDescription = new Text("Une potion couleur aqua, etrange", "An aqua color potion, strange");
    public static readonly Text BluePotionDescription = new Text("Une potion couleur bleu, etrange", "A blue color potion, strange");
    public static readonly Text GreenPotionDescription = new Text("Une potion couleur verte, etrange", "A green color potion, strange");
    public static readonly Text PurplePotionDescription = new Text("Une potion couleur purple, etrange", "A purple color potion, strange");
    public static readonly Text RedPotionDescription = new Text("Une potion couleur rouge, etrange", "A red color potion, strange");
    public static readonly Text YellowPotionDescription = new Text("Une potion couleur jaune, etrange", "A yellow color potion, strange");

    public static readonly Text WoodenPlankDescription = new Text("Une planche de bois pouvant servir a creer des meubles", "A Wooden plank usable to make furnitures");
    public static readonly Text GlassDescription = new Text("Du verre, la lumiere passe au travers", "Glass, light pass throught");
    public static readonly Text BowlDescription = new Text("Un bol pratique pour contenir de la nourriture", "A bowl handy to contain food");
    public static readonly Text CuttedStoneDescription = new Text("Une pierre taillé utilisable pour des construction", "A cutted stone usable for construction");

    // Tools
    // Name
    public static readonly Text WoodenPickaxe = new Text("Pioche en bois", "Wooden pickaxe");
    public static readonly Text StonePickaxe = new Text("Pioche en pierre", "Stone pickaxe");
    public static readonly Text CopperPickaxe = new Text("Pioche en cuivre", "Copper pickaxe");
    public static readonly Text IronPickaxe = new Text("Pioche en fer", "Iron pickaxe");
    public static readonly Text GoldPickaxe = new Text("Pioche en or", "Gold pickaxe");
    public static readonly Text MithrilPickaxe = new Text("Pioche en mithril", "Mithril pickaxe");
    public static readonly Text FloatiumPickaxe = new Text("Pioche en floatium", "Floatium pickaxe");
    public static readonly Text SunkiumPickaxe = new Text("Pioche en sunkium", "Sunkium pickaxe");

    public static readonly Text WoodenAxe = new Text("Hache en bois", "Wooden axe");
    public static readonly Text StoneAxe = new Text("Hache en pierre", "Stone axe");
    public static readonly Text CopperAxe = new Text("Hache en cuivre", "Copper axe");
    public static readonly Text IronAxe = new Text("Hache en fer", "Iron axe");
    public static readonly Text GoldAxe = new Text("Hache en or", "Gold axe");
    public static readonly Text MithrilAxe = new Text("Hache en mithril", "Mithril axe");
    public static readonly Text FloatiumAxe = new Text("Hache en floatium", "Floatium axe");
    public static readonly Text SunkiumAxe = new Text("Hache en sunkium", "Sunkium axe");

    public static readonly Text WoodenSword = new Text("epee en bois", "Wooden Sword");
    public static readonly Text StoneSword = new Text("epee en pierre", "Stone Sword");
    public static readonly Text CopperSword = new Text("epee en cuivre", "Copper Sword");
    public static readonly Text IronSword = new Text("epee en fer", "Iron Sword");
    public static readonly Text GoldSword = new Text("epee en or", "Gold Sword");
    public static readonly Text MithrilSword = new Text("epee en mithril", "Mithril Sword");
    public static readonly Text FloatiumSword = new Text("epee en floatium", "Floatium Sword");
    public static readonly Text SunkiumSword = new Text("epee en sunkium", "Sunkium Sword");

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

    public static readonly Text WoodenSwordDescription = new Text("Un ensemble de morceaux de bois ressemblant à une epee", "A set of wood pieces looking like a sword");
    public static readonly Text StoneSwordDescription = new Text("Un outil rudimentaire de pierre ressemblant à une epee", "A rudimentary stone tool looking like a sword");
    public static readonly Text CopperSwordDescription = new Text("Une epee de qualite douteuse", "Questionable quality sword");
    public static readonly Text IronSwordDescription = new Text("Une bonne epee", "A good sword");
    public static readonly Text GoldSwordDescription = new Text("Une epee pour riche", "An sword to rich");
    public static readonly Text MithrilSwordDescription = new Text("Une epee legendaire", "A legendary sword");
    public static readonly Text FloatiumSwordDescription = new Text("Une epee de solidite infaillible", "A foolproof solidity sword");
    public static readonly Text SunkiumSwordDescription = new Text("Une epee de qualite", "A quality sword");

    // Miscellanous

    public static readonly Text Quit = new Text("Quitter", "Quit");
    public static readonly Text Continue = new Text("Continuer", "Continue");
    public static readonly Text Sound = new Text("Son", "Sound");
    public static readonly Text Back = new Text("Retour", "Back");
    public static readonly Text Language = new Text("Langue", "Language");
    public static readonly Text French = new Text("Français", "French");
    public static readonly Text English = new Text("Anglais", "English");
    public static readonly Text Play = new Text("Jouer", "Play");
    public static readonly Text Join = new Text("Rejoindre", "Join");
    public static readonly Text Activate = new Text("Activer", "Activate");
    public static readonly Text Validate = new Text("Valider", "Validate");
    public static readonly Text Cancel = new Text("Annuler", "Cancel");
    public static readonly Text Loading = new Text("Chargement...", "Loading...");
    public static readonly Text Settings = new Text("Options", "Settings");
    public static readonly Text EnterName = new Text("Entrez un nom :", "Enter a name :");
    public static readonly Text Character = new Text("Personnage", "Character");
    public static readonly Text WorldName = new Text("Nom du monde", "World name");
    public static readonly Text Create = new Text("Créer", "Create");
    public static readonly Text Upgrade = new Text("Amélioration", "Upgrade");
    public static readonly Text Seed = new Text("Seed (facultatif)", "Seed (optional)");
    public static readonly Text Delete = new Text("Supprimer", "Delete");
    public static readonly Text AttackPower = new Text("Pouvoir d'attaque", "Attaque power");
    public static readonly Text GrowingPower = new Text("Pouvoir de croissance", "Growing power");
    public static readonly Text PortalPower = new Text("Pouvoir de portail", "Portal Power");
    public static readonly Text Respawn = new Text("Reaparaitre", "Respawn");
    public static readonly Text Start = new Text("Debuter", "Start");
    public static readonly Text PVP = new Text("Joueur contre Joueur", "Player versus Player");
    public static readonly Text WorktopAbsent = new Text("l'etablie necessaire ne se trouve pas à porté", "Necessary woktop is not present \n");
    public static readonly Text DontHaveItem = new Text("Tu ne possède pas les objets necessaire pour cette recette", "You don't have the necessary item for this recipe \n");
    public static readonly Text InventoryFull = new Text("Ton inventaire est plein", "Your inventory is full");

}



