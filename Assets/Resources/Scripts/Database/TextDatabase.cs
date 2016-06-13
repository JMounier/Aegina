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
    public static readonly Text Torch = new Text("Torche", "Torch");
    public static readonly Text Chest = new Text("Coffre", "Chest");
    public static readonly Text WolfTrap = new Text("Piège à loup", "Wolf trap");
    public static readonly Text Spiques = new Text("Pique", "Pique trap");
    public static readonly Text WoodenWall = new Text("Mur en bois", "Wooden wall");
    public static readonly Text StoneWall = new Text("Mur en pierre", "Stone wall");




    public static readonly Text WaterPotion = new Text("Fiole d'eau", "Water phial");
    public static readonly Text SpeedPotion = new Text("Potion de vitesse", "Potion of speed");
    public static readonly Text PoisonPotion = new Text("Potion de poison", "Potion of poison");
    public static readonly Text RegenerationPotion = new Text("Potion de regeneration", "Potion of regeneration");
    public static readonly Text HealingPotion = new Text("Potion de santé", "Potion of healing");
    public static readonly Text JumpPotion = new Text("Potion de saut", "Potion of jump");

    public static readonly Text WoodenPlank = new Text("Planche de bois", "Wooden plank");
    public static readonly Text Glass = new Text("Verre", "Glass");
    public static readonly Text Bowl = new Text("Bol", "Bowl");
    public static readonly Text CuttedStone = new Text("Pierre taillé", "Cutted stone");
    public static readonly Text Stick = new Text("Bout de bois", "Stick");
    public static readonly Text Hide = new Text("Cuir", "Hide");
    public static readonly Text Gigot = new Text("Gigot", "Gigot");
    public static readonly Text Fang = new Text("Dent", "Fang");
    public static readonly Text Bone = new Text("Os", "Bone");
    public static readonly Text Pumpkin = new Text("Citrouille", "Pumpkin");


    public static readonly Text Apple = new Text("Pomme", "Apple");
    public static readonly Text Soup = new Text("Soupe", "Soup");
    public static readonly Text Cactus = new Text("Cactus", "Castus");
    public static readonly Text Petal = new Text("Petal", "Petal");
    public static readonly Text RedMushroom = new Text("Champignon rouge", "Red Mushroom");
    public static readonly Text Mushroom = new Text("Champignon", "Mushroom");
    public static readonly Text WaterCact = new Text("Eau", "Water cactus");
    public static readonly Text MeatBalls = new Text("Boullettes de viande", "MeatBalls");
    public static readonly Text AnimalFat = new Text("Graisse animal", "Animal fat");

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
    public static readonly Text GoldIngotDescription = new Text("Un lingot d'or pouvant être utiliser pour construire d'autres objets", "A gold ingot usable to make other objetcs");
    public static readonly Text MithrilIngotDescription = new Text("Un lingot de mithril pouvant être utiliser pour construire d'autres objets", "A mithril ingot usable to make other objects");
    public static readonly Text FloatiumIngotDescription = new Text("Un lingot de floatium pouvant être utiliser pour construire d'autres objets", "A floatium ingot usable to make other objects");
    public static readonly Text SunkiumIngotDescription = new Text("Un lingot de sunkium pouvant être utiliser pour construire d'autres objets", "A sunkium ingot usable to make other objects");

    public static readonly Text ForgeDescription = new Text("Une forge rudimentaire faite avec quelques pierres. Cette forge permet de faire fondre des metaux", " A rudimentary forge made with some stone. This forge can melt metal");
    public static readonly Text CauldronDescription = new Text("Un chaudron de sorciere, ideal pour faire des potions", "A witch cauldron ideal to make potion");
    public static readonly Text WorkbenchDescription = new Text("Une table de travail pouvant servir pour... travailler", "A workbench usable to... work");
    public static readonly Text FirepitDescription = new Text("Feu !!! Viande !!! Repas !!!", "Fire !!! Meat !!! Meal !!!");
    public static readonly Text TorchDescription = new Text("Et la lumière fut !", "And there was light !");
    public static readonly Text ChestDescription = new Text("Permet de stocker plein de chose inutiles !", "Able to stock a lot of useless stuff !");
    public static readonly Text WolfTrapDescription = new Text("Utile pour chasser les créatures.", "Useful to hunt creatures.");
    public static readonly Text SpiquesDescription = new Text("Un piège vicieux mais efficace", "The solution to protect your belongings from vicious robbers");
    public static readonly Text WoodenWallDescription = new Text("Quelques piquets suffisent pour defendre votre maison", "In an other world, skulls were put on this kind of wall to frighten foreigners");
    public static readonly Text StoneWallDescription = new Text("De quoi construire un vrai chateau", "An other brique in the wall");


    public static readonly Text WaterPotionDescription = new Text("Une fiole pleine d'eau !", "A full water phial!");
    public static readonly Text SpeedPotionDescription = new Text("Une potion pour courir plus vite !", "A potion to run faster !");
    public static readonly Text PoisonPotionDescription = new Text("Je ne boirai pas ça si j'étais toi", "I will not drink it if I were you");
    public static readonly Text RegenerationPotionDescription = new Text("Cicatrise les plaies", "Heals wounds");
    public static readonly Text HealingPotionDescription = new Text("Fait disparaître les blessures et les carences", "Removes injuries and deficiencies");
    public static readonly Text JumpPotionDescription = new Text("Fait pousser des ailes", "Grows wings");

    public static readonly Text WoodenPlankDescription = new Text("Une planche de bois pouvant servir a creer des meubles", "A Wooden plank usable to make furnitures");
    public static readonly Text GlassDescription = new Text("Du verre, la lumiere passe au travers", "Glass, light pass throught");
    public static readonly Text BowlDescription = new Text("Un bol pratique pour contenir de la nourriture", "A bowl handy to contain food");
    public static readonly Text CuttedStoneDescription = new Text("Une pierre taillé utilisable pour des construction", "A cutted stone usable for construction");
    public static readonly Text StickDescription = new Text("C'est un bout de bois", "It's a piece of wood");
    public static readonly Text HideDescription = new Text("Du cuir, j'avais justement besoin de nouveaux vêtements", "Some hide, I needed some new clothes");
    public static readonly Text GigotDescription = new Text("Cuisiné ça pourrais être bon", "Cooked it could be good");
    public static readonly Text FangDescription = new Text("C'est une dent", "It's a fang");

    public static readonly Text AppleDescription = new Text("Une pomme bien rouge et juteuse", "A bright red and juicy apple");
    public static readonly Text SoupDescription = new Text("Une soupe bien chaude", "A hot soup");
    public static readonly Text CactusDescription = new Text("Un petit cactus avec une fleur dessus. Il semble contenir une bonne quantite d'eau", "A little cactus with a flozer on it. It seem to contain a good amount of water");
    public static readonly Text PetalDescription = new Text("Un petale de fleur. C'est jolie mais peut utile. Vous n'allez pas le manger tout de meme ?", "A flower petal. It's beautiful but useless. You will not eat it, is it ? ");
    public static readonly Text RedMushroomDescription = new Text("Un champignon qui pourrait être comestible", "Mushroom which could be edible");
    public static readonly Text MushroomDescription = new Text("Un champignon qui à l'air comestible", "Mushroom which seems edible");
    public static readonly Text WaterCactDescription = new Text("De l'eau dans un cactus pour étancher la soif", "Water in a cactus to quench his thirst");
    public static readonly Text MeatBallsDcription = new Text("Viannnnnde", "Meaaaaat");
    public static readonly Text BoneDescription = new Text("Berk ! Vous avez vraiment ramasse ca ?", "Yuck! You really pick that?");
    public static readonly Text PumpkinDescription = new Text("Mmm... Il doit y avoir moyen de cuisiner quelque chose avec !", "Mmm ... There must be a way to cook something with!");
    public static readonly Text AnimalFatDescription = new Text("De la graisse animal... degoutant", " Animal fat... gross");


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

    public static readonly Text WoodenSword = new Text("Epee en bois", "Wooden Sword");
    public static readonly Text StoneSword = new Text("Epee en pierre", "Stone Sword");
    public static readonly Text CopperSword = new Text("Epee en cuivre", "Copper Sword");
    public static readonly Text IronSword = new Text("Epee en fer", "Iron Sword");
    public static readonly Text GoldSword = new Text("Epee en or", "Gold Sword");
    public static readonly Text MithrilSword = new Text("Epee en mithril", "Mithril Sword");
    public static readonly Text FloatiumSword = new Text("Epee en floatium", "Floatium Sword");
    public static readonly Text SunkiumSword = new Text("Epee en sunkium", "Sunkium Sword");

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
    public static readonly Text Yes = new Text("Oui", "Yes");
    public static readonly Text No = new Text("Non", "No");
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
    public static readonly Text AttackPower = new Text("Guerre", "War");
    public static readonly Text GrowingPower = new Text("Récolte", "Harvest");
    public static readonly Text PortalPower = new Text("Divin", "Divine");
    public static readonly Text Respawn = new Text("Reaparaitre", "Respawn");
    public static readonly Text Start = new Text("Debuter", "Start");
    public static readonly Text PVP = new Text("JCJ", "PVP");
    public static readonly Text WorktopAbsent = new Text("L'établie necessaire ne se trouve pas à porté", "Necessary woktop is not present \n");
    public static readonly Text DontHaveItem = new Text("Tu ne possède pas les objets necessaire pour cette recette", "You don't have the necessary item for this recipe \n");
    public static readonly Text InventoryFull = new Text("Ton inventaire est plein", "Your inventory is full");
    public static readonly Text Tutorial = new Text("Suivre le didacticiel ?", "Follow the tutorial ?");
    public static readonly Text QuitTuto = new Text("Quitter le tutoriel ?", "Quit the tuto ?");
    public static readonly Text FieldOfView = new Text("Champ de vision", "Field of view");
    public static readonly Text Sensibility = new Text("Sensibilite", "Sensibility");
    public static readonly Text Heal = new Text("Soin", "Heal");
    public static readonly Text Help = new Text("Aide", "Help");
    public static readonly Text CombatHelp = new Text("Combat", "Fight");
    public static readonly Text InventaireHelp = new Text("Inventaire", "Inventory");
    public static readonly Text Resume = new Text("retour au jeu", "Resume");

    // Tutoriel

    public static readonly Text Move = new Text(
        "Pour se réhabituer à ses jambes, Ille commença à se déplacer",
        "Ille begin to move to get used to his legs");
    public static readonly Text MoveObjectif = new Text("Se deplacer", "Move");
    public static readonly Text Run = new Text(
        "S'étant habitué à ses jambes, Ille décida de se mettre à courir",
        "Getting used to his legs, Ille decided to start running");
    public static readonly Text RunObjectif = new Text("Cours (bouge avec la touche shift enfoncé)", "Run (hold the shift key and move)");
    public static readonly Text Jump = new Text(
        "Ille estait tellement en forme qu'il pensait même pouvoir se permettre de sauter",
        "Ille is so fit that he thinks he can even afford to jump");
    public static readonly Text JumpObjectif = new Text("Saute (appuie sur la barre espace)", "Jump (press the spacebar)");
    public static readonly Text PickItem1 = new Text(
        "Juste après avoir sauté, Ille entendi un sinistre grognement.",
        "Just after he jumped, Ille heard a sinister growl.");
    public static readonly Text PickItem2 = new Text(
        "Ille avait faim. Cependant dans cette contrée sauvage, il devina qu'il devait chasser sa nourriture. Mais pour chasser Ille avait besoin d'une arme.",
        "Ille is hungry. However, in this wilderness, he guesses he must hunt for food.Therefore he needs a weapon to hunt.");
    public static readonly Text PickItem3 = new Text(
        "Justement il remarqua un bâton sur le chemin, l'arme parfaite pour lui",
        "At this moment he noticed a stick on the way, the perfect weapon for him");
    public static readonly Text PickItemObjectif = new Text("Prendre le bâton (clic droit enfoncé près de l'objet)", "Take the Stick (hold the rightclick down near the stick)");
    public static readonly Text Equip = new Text(
        "Après avoir mis le bâton dans sa poche, Ille se dit qu'il devrait peut être le sortir de celle-ci pour que son arme soit utilisable",
        "After putting the stick in his pocket, Ille take it out to use it");
    public static readonly Text EquipObjectif = new Text("Equiper le baton (ouvrez l'inventaire avec I et faite glisser le bâton dans la barre de racourcis en bas de l'écran, fermez l'inventaire (I ou escape) et sélectionnez ensuite le bâton avec la molette de la souris)", "Equip the stick (open the inventory with I, drag the stick in the bar on bottom of the screen, close the inventory (I or escape) and scrol to select the Stick )");
    public static readonly Text KillThePig = new Text(
        "Ille se mit à la recherche de nourriture et en se retournant tomba nez à nez avec un animal. Ni une ni deux Ille attaqua son petit déjeuner",
        "He starts looking for food and, while turning around, came face to face with an animal. It's time for breakfast..");
    public static readonly Text KillThePigObjectif = new Text("Attaque un cochon avec le clic gauche et récupère de la viande", "Kill a wild boar with the leftclick and get meat");
    public static readonly Text CraftABrochette = new Text(
        "Cependant, Ille avait un vague souvenir que la viande crue n'était pas très digeste au petit déjeuner. Faute de feu, il décida quand même de manger sa viande préparée en brochette",
        "However, Ille has a vague recollection that raw meat is not an easily digestible breakfast. Without fire, he still decides to eat his meat prepared on skewers");
    public static readonly Text CraftABrochetteObejctif = new Text("Ouvrez l'inventaire et cliqué sur la potion à gauche puis cliqué sur la brochette et enfin valider pour créer", "Open the inventory, click on the potion at left, click at the skewer and valid to craft");
    public static readonly Text EatSomething = new Text(
        "Le repas de Ille est près. Il ne lui suffisait plus que de consommer sa brochette et le grondement de son ventre cesserait",
        "Ille's meal is ready. He just need to consumed his skewer and the rumble of his stomach would stop");
    public static readonly Text EatSomethingObjectif = new Text("sélectionne la brochette et maintient le clic droit pour manger", "select the skewer and hold the right click to eat");
    public static readonly Text DrinkSomething1 = new Text(
        "Après avoir manger Ille devait également trouver à boire. Ille examina les environ mais ne trouva pas d'eau.",
        "After having eaten Ille had to find something to drink. Ille looked around but he found no water.");
    public static readonly Text DrinkSomething2 = new Text(
        " Cependant Ille vit des petit cactus à l'horizon et se dit qu'il pourait peut être trouver de l'eau à l'intérieur ",
        "However Ille saw little cactus on the horizon and thought that he could find some water in them");
    public static readonly Text DrinSomethingObjectif = new Text("récupère des cactus et transforme les en eau grâce à l'interface de création d'objets", "Gather some cactus and transforms them into water with the crafting interface");
    public static readonly Text CinematiqueWhereIAm1 = new Text(
        "Maintenant que Ille avait trouvé de quoi survivre, il pouvait enfin se reposer. Jusqu'à présent  Ille n'avait pensé qu'à manger mais une fois le ventre rempli il se mit à penser et à se questionner",
        "Now that Ille has found how to survive he can finally rest. So far Ille only thougt to eat but once his stomach fill he begin to think and to question himself ");
    public static readonly Text CinematiqueWhereIAm2 = new Text(
        "\" Mais où suis-je? Quel est cet endroit ?\" En effet cet archipel d'îles volantes ne lui etait pas familier ",
        "\" where am I? What is this place? \" Indeed this archipelago of floating island was not a familiar landscape for Ille.");
    public static readonly Text CinematiqueWhereIAm3 = new Text(
        "\"Attends !!! Des îles volantes ?? Mais ce n'est pas possible, ça ne peut pas exister \" Et pourtant ce qui s'étendait à perte de vue n'estait pas une illusion. Ille ne se trouve plus chez lui, peut être même plus dans son monde.",
        "\"Wait !!! Floating islands ?? But this is not possible, it can't exist \"Yet what lies out of sight is not an illusion. Ille is no longer at home, perhaps even not in his world.");
    public static readonly Text CinematiqueWhereIAm4 = new Text(
        "Quand Ille eu assimilé ce qui lui arrive, il prit une décision \" Il faut absolument que je comprenne où je suis et pour ça il faut que j'explore ces drôles d'îles\" Et c'est ainsi que Ille commença son aventure.",
        "When Ille assimilated what happens to him, he makes a decision \" It is imperative that I understand where I am and for that I must explore these strange islands \" And thus Ille begins his adventure.");
    public static readonly Text CristalView = new Text(
        "en regardant aux alentours Ille vit un étrange éclat. Il decida donc de trouver sa source",
        "looking around Ille saw a strange glow. So he decided to find its source");
    public static readonly Text CristalViewObjectif = new Text("trouve un cristal et examine le avec un clic droit", "Find a cristal and inspect it with a rightclick");
    public static readonly Text FirstCristal1 = new Text(
        "Le reflet vient d'un gigantesque cristal taillé comme un prisme, mais le plus impressionnant etait que celui-ci lévitait... Malgré sa stupeur, Ille se ressaisit assez vite ",
        "The reflection comes from a huge crystal cut as a prism but the most impressive thing is that it levitates ... Despite his stupor, Ille is recovering fast enough");
    public static readonly Text FirstCristal2 = new Text(
        "\" hum pas si étonnant que ça vu que je me trouve actuellement sur une île volant dans le ciel\". Après s'être calmé et posé, il vit que le cristal réagissait à sa présence et plus précisément au métal de sa boucle de ceinture qui semblait attirée vers le cristal.",
        "\"um not so surprising considering the fact that I find myself on an island floating in the sky\" Having calmed down he sees that the crystal reacts to his presence and specifically to its metal belt buckle that seems drawn to the crystal.");
    public static readonly Text FirstCristal3 = new Text(
        "Ille décida donc d'aller chercher du métal pour tester ces étranges réactions et s'éloigna rapidement ne voulant pas perdre sa précieuse ceinture.",
        "Ille decides to go for the metal to test these strange reactions and moves away quickly not wanting to lose his precious belt");
    public static readonly Text FirstCrisatlObjectif = new Text("fabrique une hache,équippe la pour récolter du bois ,fabrique un établi, pose le avec le clic droit, fabrique une pioche et part récupérer les minerai pour enfin activer le cristal", "Craft an ace, equip it to gather wood,Craft a worktop, pose it with a rightclick, craft a pickaxe and go find ore to finally activate the crystal");

    // Story

    public static readonly Text StoneAgeStory1 = new Text(
        "En activant le cristal Ille sentit un puissant pouvoir se dégager de celui-ci et un lien se former entre lui et le cristal",
        "By activating the crystal Ille felt a mighty power from it that form a link between him and the crystal");
    public static readonly Text StoneAgeStory2 = new Text(
        "Le pouvoir du cristal était exceptionnelle et se mit à traverser le corps de Ille",
        "The power of the crystal was exceptional and began to cross through the body of Ille");
    public static readonly Text StoneAgeStory3 = new Text(
        "Ille comprit alors ce que le cristal avait fait. Tant que celui-ci serait activer Ille sera immortelle. La mort ne serait qu’une étape passagère avant que le cristal le ramène à la vie",
        "Ille understood what the crystal had done. As it would be enable Ille will be immortal.Death would be a passing phase before the crystal brought him back to life");
    public static readonly Text StoneAgeStory4 = new Text(
        "Dans ce monde le cristal avait besoin de lui et Ille avait besoin du cristal pour survivre",
        "In this world the crystal needed him and Ille needed the crystal to survive");
    public static readonly Text StoneAgeStory5 = new Text(
        " En sentant ce pouvoir Ille se dit que la clef pour retourner chez lui se trouvait dans ce cristal",
        "Feeling that power Ille thought that the key to return home was in that crystal");
    public static readonly Text StoneAgeStory6 = new Text(
        "Cependant le cristal n’avait pas encore retrouvé tout ces pouvoirs. Celui-ci se mit à communiqué avec Ille",
        "However the crystal had not retrieve all his powers. It began to communicate with Ille");
    public static readonly Text StoneAgeStory7 = new Text(
        "Pour retrouvé ses pouvoirs Ille devait lui ramener une ressources particulière qu’il ne pourrait trouvé que sur le cadavre d’un chef de meute sanglier ",
        "To regained his powers Ille has to bring him special resources he could find on the carcass of a wild boar pack leader");
    public static readonly Text StoneAgeStory8 = new Text(
        "Ille ne pouvait pas tout de suite affronter un chef de meute sanglier. Il devait d’abord se préparer",
        "Ille could not immediately confront a boar pack leader.He must first prepare");
    public static readonly Text StoneAgeStory9 = new Text(
        "Ille décida qu’il allait fabriquer un coffre car ses poches commençait à se remplir. ensuite il fabriquerait une arme pour combattre les sangliers  et se créer une armure",
        "Ille decided he was going to make a chest because his pockets began to be full. then he would manufacture a weapon to fight wild boar and create armor");
    public static readonly Text StoneAgeStory10 = new Text(
        "Quand tout sera prêt Ille pourra enfin combattre un chef de meute sanglier",
        "When everything will be ready Ille will finally fight a boar pack leader");


    public static readonly Text CopperAgeStory1 = new Text(
        "Ille avait réussi. Le chef de meute sanglier gisait maintenant à ces pieds et avec son cadavre la ressources qu’il recherchait",
        "Ille had succeeded.The pack leader boar was now lying on his feet and with his corps the resources he sought");
    public static readonly Text CopperAgeStory2 = new Text(
        "Il voulait s’empresser d’offrir cette ressource au cristal mais tout ne se passa pas comme prévue",
        "He wanted to rush to offer this resource to the crystal but all did not work out as planned");
    public static readonly Text CopperAgeStory3 = new Text(
        "Le cristal ne pouvait pas absorber la ressource telle qu’elle. Celui-ci avait également besoin de minerai pour l’aider",
        "The crystal could not absorb the resource now. It also needed ore to help him absorb them");
    public static readonly Text CopperAgeStory4 = new Text(
        "Ces minerais devait être bien plus pur que ceux que l’on trouve dans la nature",
        "These minerals should be more pure than that found in nature");
    public static readonly Text CopperAgeStory5 = new Text(
        "Ille n’avait pas d’autre choix que de préparer les minerais lui-même. et pour cela il lui faudrait une forge", "");
    public static readonly Text CopperAgeStory6 = new Text(
        "De plus Ille avait été blessé pendant son combat et il fallait qu’il récupère.",
        "Ille had no choice but to prepare the ore itself.and for that he would need a forge");
    public static readonly Text CopperAgeStory7 = new Text(
        "Heureusement le cristal lui avait donné une recette pour fabriquer une potion qui pourrait le soigner mais cela demanderait la fabrication d’un chaudron",
        "Fortunately the crystal gave him a recipe for a potion that would heal him but it would require a cauldron");
    public static readonly Text CopperAgeStory8 = new Text(
        "sans attendre Ille décida de se mettre au boulot",
        "without waiting Ille decided to get to work");

    public static readonly Text IronAgeStory1 = new Text(
        "Ille était de nouveau comme neuf et le cristal avait récupérer une partie de ses pouvoirs",
        "Ille was like new and the crystal was recovering some of his powers");
    public static readonly Text IronAgeStory2 = new Text(
        "Cependant rentre chez lui demanderait au cristal un pouvoir encore plus grand. Il avait donc besoin de récupérer encore plus de force",
        "However to return home the crystal need an even greater power.It therefore must recover even more strength");
    public static readonly Text IronAgeStory3 = new Text(
        "Cette fois ci le cristal avait besoin d’une ressource garder par le chef de meute pampi une créatures craintif qui serait extrêmement difficile à tuer d’autant plus qu’elle possède une force monstrueuse",
        "This time the crystal needed a resource keeped by the pack leader Pampi a timid creatures that would be extremely difficult to kill especially as it has a monstrous strength");
    public static readonly Text IronAgeStory4 = new Text(
        "Pour se préparer Ille prévoyait d’utiliser de sa nouvelle forge pour se créer de nouvelles armes et armures",
        "To be prepared Ille planned to use its new forge to create new weapons and armor");
    public static readonly Text IronAgeStory5 = new Text(
        "Ille prévoyait également d’aller combattre des pampas pour comprendre leur comportement",
        "Ille also planned to go fight the pampas to understand their behavior");
    public static readonly Text IronAgeStory6 = new Text(
        "Enfin, afin d’éviter que le chef de meute pampi s’enfuit, Ille allait créer des pièges pour l'immobiliser et le vaincre plus facilement",
        "Finally, to prevent the pack leader Pampi to flee, Ille would create traps to immobilize and defeat it more easily");

    public static readonly Text GoldAgeStory1 = new Text(
        "Une seconde fois le cadavre d’un chef de meute se trouvait à ses pieds. Mais Ille ne se sentait pas aussi bien que la dernière fois. Le comportement et les bruits de ses pampa était beaucoup trop humain",
        "Again the body of a pack leader was at his feet. However Ille did not feel as good as last time. The behavior and the sounds of Pampas was much too human");
    public static readonly Text GoldAgeStory2 = new Text(
        "Lors de ces derniers instant le chef de meute pampi lui avait jeter un regard empli de peur qui l’avait rendu mal à l’aise. Il avait même eu l’impression que celui-ci avait appelé à l’aide lors de ces derniers instants",
        "During his last moment the pack leader Pampi had cast to him a glance full of fear that had made ​​him uneasy. He even felt that he had called for help during his last moments");
    public static readonly Text GoldAgeStory3 = new Text(
        "Ille avait-il vraiment le droit de priver ces être de la vie. Ce n’était pas juste comme quand Ille chassait pour manger ",
        "Had Ille the right to deprive these being of theirs life. It was not just like when Ille hunted for eating");
    public static readonly Text GoldAgeStory4 = new Text(
        "Là il avait tuer de nombreux pampa dans le seul but de s'entraîner puis le chef de meute pampi, qui ne demandait qu’a vivre en paix, pour récupérer sa ressource",
        "He had killed many pampas with the sole purpose of training and the pack leader Pampi , which has sought to live in peace ,only to recover its resource");
    public static readonly Text GoldAgeStory5 = new Text(
        "Cependant tout cela était nécessaire pour qu’Ille retourne chez lui et il décida donc de laisser ses remords de coté et d’aller revoir le cristal",
        "However all that was needed for Ille to return home so he decided to leave his remorse aside and go see the crystal");
    public static readonly Text GoldAgeStory6 = new Text(
        "Le cristal lui appris alors que pour récupérer une nouvelles parties de ses pouvoirs, en plus d’absorber la ressource il aurait besoin d’être connecter à d’autres cristaux de nature différentes",
        "The crystal taught him that in addition to absorbing the resource it needs to be connected to other crystals of different kind to get back a new parts of his powers");
    public static readonly Text GoldAgeStory7 = new Text(
        "Pour cela Ille devrait trouver des cristaux de guerre et de récolte et leur redonner du pouvoir comme ce qu’il avait fait avec le cristal divin",
        "For this Ille should find crystals of war and harvest and give them power as he had done with the divine crystal");

    public static readonly Text MithrilAgeStory1 = new Text(
        "Après avoir récupéré une seconde partie de ses ces pouvoirs le cristal révéla à Ille qu’il ne lui restait plus qu’a lui apporter une dernière ressources pour que le cristal récupère tout ses pouvoirs",
        "After getting a second part of its powers the crystal revealed to Ille that he only nead to find a last resource and the crystal could recovers all its powers");
    public static readonly Text MithrilAgeStory2 = new Text(
        "Cette ressource était garder par les slimes de terribles monstres capable de résister aux attaques des armes normales et dont les attaques pouvait passer à travers les armures basiques",
        "This resource was keeped by the terrible slime monsters capable of withstanding attacks of normal weapons and whose attacks could get through basic armor");
    public static readonly Text MithrilAgeStory3 = new Text(
        "Le cristal conseilla à Ille de s'équiper avec des armes et armures plus résistante puis de s’entrainer sur les slimes avant de s’attaquer au chef de meute slime qui détient la dernière ressource",
        "The crystal advised Ille to equip himselves with weapons and armor more resistant then to train with normal slimes before attacking the slime pack leader who holds the last resource");

    public static readonly Text FloatiumAgeStory1 = new Text(
        "En ramenant la ressource le cristal lui appris que pour absorber celle-ci il aurait besoin que deux autres cristaux de récolte et de guerre ai récupéré tout leurs pouvoirs",
        "By bringing back the resource the crystal taught him that ,to absorb it, he would need another two crystal of harvest and war that have recovered all their powers");
    public static readonly Text FloatiumAgeStory2 = new Text(
        "Ille se sentit énervé d’être utilisé comme ça mais vu que le cristal allait bientôt retrouver tout ces pouvoir et pourrait le ramener chez lui Ille décida de prendre sur lui même et se mit en route",
        "Ille felt annoyed to be used like that but since the crystal would soon regain all the power and might bring him home Ille decided to take upon himself and set off");

    public static readonly Text PreSunkiumAgeStory = new Text(
        "Le moment était venu, il ne lui restait plus qu’a améliorer une dernière fois le cristal divin et celui-ci pourrait enfin le ramener chez lui",
        "It was time, he only has to improve the divine crystal one last time and he could finally return home");

    // Boss

    public static readonly Text PreBossBattle1 = new Text(
        "Juste après avoir amélioré une dernière fois le cristal divin Ille se senti transporter dans un autre lieu.", "");
    public static readonly Text PreBossBattle2 = new Text(
        "En relevant la tête Ille vit qu’il se trouvait sur un socle au beau milieu d’un espace d’un noir insondable.", "");
    public static readonly Text PreBossBattle3 = new Text(
        "“Où suis-je ?” demanda Ille", "");
    public static readonly Text PreBossBattle4 = new Text(
        "“Nous sommes dans les abysses d’Aegina. là où tout finis. Je me suis dis que ce serai un endroit convenable pour la fin de ton périple”", "");
    public static readonly Text PreBossBattle5 = new Text(
        "En entendant cette voix Ille se retourna et vit un immense cristal flottant dans le vide. La voix lui était vaguement familière et semblait venir du cristal.", "");
    public static readonly Text PreBossBattle6 = new Text(
        "“C’est vrai que je ne me suis jamais vraiment présenté. Je suis Gundam le seigneur des cristaux.”", "");
    public static readonly Text PreBossBattle7 = new Text(
        "“Durant ton séjour dans ce monde tu n’as pas arrêter de m’aider à amplifier mes pouvoirs dans le but de revenir chez toi.”", "");
    public static readonly Text PreBossBattle8 = new Text(
        "“Tu as tué de nombreux sangliers pampa et <troisième créatures agrésive> ainsi que leur chef et je t’en remercie.”", "");
    public static readonly Text PreBossBattle9 = new Text(
        "“Ces créatures était les gardiens des <ressources> et les gardaient pour empêcher les cristaux d’amplifier leurs pouvoirs”", "");
    public static readonly Text PreBossBattle10 = new Text(
        "“Mais grâce à toi j’ai assez de pouvoir pour me connecter à tous les cristaux d’Aegina et les activer.”", "");
    public static readonly Text PreBossBattle11 = new Text(
        "“Je suis devenue le dieu d’Aegina.”", "");
    public static readonly Text PreBossBattle12 = new Text(
        "“Cependant avant que je m’attelle à cette tâche et que je punisse ces sombres créatures qui ont tenté de m’entraver il reste un dernier détail à régler.”", "");
    public static readonly Text PreBossBattle13 = new Text(
        "“Ce dernier détail c’est toi Ille.”", "");
    public static readonly Text PreBossBattle14 = new Text(
        "“Je te remercie vraiment de ce que tu as fait mais tu ne te rend pas compte à qu’elle point ce que tu me demandes et difficile”", "");
    public static readonly Text PreBossBattle15 = new Text(
        "“Même un dieu à ces limites car je suis le dieu d’Aegina et non celui de ton monde”", "");
    public static readonly Text PreBossBattle16 = new Text(
        "“Te ramener dans ton monde,comme tu es actuellement, demanderais une très grande partie de l’énergie d’Aegina et je ne peut pas me permettre d’utiliser tous le pouvoir que je viens à peine d’obtenir.”", "");
    public static readonly Text PreBossBattle17 = new Text(
        "“Cependant tu m’as grandement aidé et j’ai donc trouvé un compromis”", "");
    public static readonly Text PreBossBattle18 = new Text(
        "“Je vais te tuer et renvoyé ton âme dans ton monde”", "");
    public static readonly Text PreBossBattle19 = new Text
        ("“Ce n’était pas ce qui était convenue” s’exclama Ille.", "");
    public static readonly Text PreBossBattle20 = new Text(
        "“Mais voyons Ille je ne t’ai jamais rien promis”", "");
    public static readonly Text PreBossBattle21 = new Text(
        "“Je ne peux malheureusement pas te laisser en vie... tu tenterais sûrement de t’opposer à moi... et personne n’a le droit de s’opposer à dieu.”", "");
    public static readonly Text PreBossBattle22 = new Text(
        "“je ne me laisserais pas faire je vais me battre pour survivre comme je l’ai toujours fait” s'écria Ille en se préparant au combat.", "");

    // Fin

    public static readonly Text FinEgoïsme = new Text("", "");

    public static readonly Text FinSacrifice = new Text("", "");

    public static readonly Text FinPardon = new Text("", "");



    // Skin

    public static readonly Text WhiteBody = new Text("Corps blanc", "White skin");
    public static readonly Text BasicBody = new Text("Corps basic", "Basic skin");
    public static readonly Text BlackBody = new Text("Corps noir", "Black skin");
    public static readonly Text DarkBody = new Text("Corps bronzé", "Tanned skin");
    public static readonly Text AlienBody = new Text("C'est pas humain ca !", "This isn't human !");
    public static readonly Text AquaBody = new Text("Avatar", "Avatar");

    public static readonly Text BrownOveralls = new Text("Salopette marron", "Brown overalls");
    public static readonly Text BlueOveralls = new Text("Salopette bleue", "Blue overalls");
    public static readonly Text BlackOveralls = new Text("Salopette noire", "Black overalls");
    public static readonly Text RedOveralls = new Text("Salopette rouge", "Red overalls");
    public static readonly Text GreenOveralls = new Text("Salopette verte", "Green overalls");
    public static readonly Text WhiteOveralls = new Text("Salopette blanche", "White overalls");
    public static readonly Text BrownPant = new Text("Pantalon marron", "Brown pant");
    public static readonly Text WhitePant = new Text("Pantalon blanc", "White pant");

    public static readonly Text BrownGloves = new Text("Gants marron", "Brown gloves");
    public static readonly Text BlueGloves = new Text("Gants bleus", "Blue gloves");
    public static readonly Text GreenGloves = new Text("Gants verts", "Green gloves");
    public static readonly Text PurpleGloves = new Text("Gants violets", "Purple gloves");
    public static readonly Text RedGloves = new Text("Gants rouges", "Red gloves");
    public static readonly Text WhiteGloves = new Text("Gants blancs", "White gloves");

    public static readonly Text BlackEyes = new Text("Yeux noirs", "Black Eyes");
    public static readonly Text GreenEyes = new Text("Yeux verts", "Green Eyes");
    public static readonly Text BrwonEyes = new Text("Yeux marron", "Brown Eyes");
    public static readonly Text BlueEyes = new Text("Yeux bleus", "Blue Eyes");
    public static readonly Text PurpleEyes = new Text("Yeux violets", "Purple Eyes");
    public static readonly Text RedEyes = new Text("Yeux rouges", "Red Eyes");
    public static readonly Text OrangeEyes = new Text("Yeux orange", "Orange Eyes");

    public static readonly Text NoneHair = new Text("Pas de cheveux", "Any hair");
    public static readonly Text BlackHair = new Text("Cheveux brun", "Black hair");
    public static readonly Text BrownHair = new Text("Cheveux marron", "Brown hair");
    public static readonly Text RedHair = new Text("Cheveux roux", "Red hair");
    public static readonly Text BlondHair = new Text("Cheveux blond", "Blond hair");
    public static readonly Text WhiteHair = new Text("Cheveux blanc", "White hair");

    public static readonly Text NoneBeard = new Text("Pas de barbe", "Any beard");
    public static readonly Text BrownBeard = new Text("Barbe marron", "Brown beard");
    public static readonly Text BlackBeard = new Text("Barbe brune", "Black beard");
    public static readonly Text RedBeard = new Text("Barbe rousse", "Red beard");
    public static readonly Text WhiteBeard = new Text("Barbe blanche", "White beard");
    public static readonly Text BlondBeard = new Text("Barbe blonde", "Blond beard");

    public static readonly Text NoneHat = new Text("Pas de chapeau", "Any hat");
    public static readonly Text AmericanTopHat = new Text("Le chapeau de l'oncle sam", "Oncle sam's hat");
    public static readonly Text BlackTopHat = new Text("Vous avez la classe", "Swag over 9000");
    public static readonly Text StrawRed = new Text("Bonjour luffy", "Hello luffy");
    public static readonly Text Strawblack = new Text("Chapeau de paille noir", "Black straw hat");
    public static readonly Text StrawWhite = new Text("Chapeau de paille blanc", "White straw hat");
    public static readonly Text StrawPurple = new Text("Chapeau de paille violet", "Purple straw hat");
    public static readonly Text StrawYellow = new Text("Chapeau de paille jaune", "Yellow straw hat");
    public static readonly Text CowBoyBrown = new Text("Chapeau de coyboy marron", "Brown cowboy hat");

    public static readonly Text NoneTshirt = new Text("A poil","Naked");
    public static readonly Text RedTshirt = new Text("T-Shirt rouge", "Red T-Shirt");
    public static readonly Text BlueTshirt = new Text("T-Shirt bleu", "Blue T-Shirt");
    public static readonly Text GreenTshirt = new Text("T-Shirt vert", "Green T-Shirt");
    public static readonly Text PurpleTshirt = new Text("T-Shirt violet", "Purple T-Shirt");
    public static readonly Text YellowTshirt = new Text("T-Shirt jaune", "Yellow T-Shirt");

    // succes

    public static readonly Text PlayTheGame = new Text("Lancer une partie"," Lauch the game");
    public static readonly Text Tuto = new Text("Réussir le tutoriel", "Finish the tutorial");
    public static readonly Text FirstBlood = new Text("Tuer un autre joueur", "Kill an other player");
    public static readonly Text FirstCap = new Text("Activer un cristal", "Activate a cristal");
    public static readonly Text FirstDeath = new Text("Mourir une fois", "dir once");
    public static readonly Text FirstHunt = new Text("Tuer une créature", "Kill a mob");
    public static readonly Text CraftChest = new Text("Créer un coffre", "Craft a chest");
    public static readonly Text CraftStoneWeapon = new Text("créer une arme en pierre", "Craft a stone weapon");
    public static readonly Text HuntMassBoar = new Text("Tuer 10 sangliers", "Kill 10 wild boars");
    public static readonly Text CraftFirstArmor = new Text("Créer une armure en cuir", "Craft a leather armor");
    public static readonly Text HuntBoarChief = new Text("Tuer un chef de meute sanglier", "Kill a pack leader wild boar");
    public static readonly Text CraftForge = new Text("Créer une forge", "Craft a forge");
    public static readonly Text DivineCristalLVL3 = new Text("Améliorer un cristal divin au niveau 3", "Upgrade a divine cristal at level 3");
    public static readonly Text CraftCauldron = new Text("Créer un chaudron", "Craft a cauldron");
    public static readonly Text DrinkHealPotion = new Text("Boire une potion de vie", "Drink an health potion");
    public static readonly Text CraftTrap = new Text("Créer un piège", "Craft a trap");
    public static readonly Text EquipInIron = new Text("Créer un équipement complet en fer", "Craft a complete equipement in iron");
    public static readonly Text HuntMassPampi = new Text("Tuer 25 pampis", "Kill 25 pampis");
    public static readonly Text HuntPampiChief = new Text("Tuer un chef de meute pampi", "Kill a pack leader pampi");
    public static readonly Text DivineCrisatlLVL4 = new Text("Améliorer un cristal divin au niveau 4", "Uprade a divine cristal at level 4");
    public static readonly Text OtherCrisatlLVL3 = new Text("Améliorer un cristal de récolte et un cristal de guerre au niveau 3", "Upgrade a harvest cristal and a war cristal at level 3");
    public static readonly Text MithrilArmor = new Text("Créer une armure de mithril", "Craft a mithril armor");
    public static readonly Text FloatiumWeapon = new Text("Créer une arme en floatium", "Craft a mithril floatium");
    public static readonly Text HuntMassSlime = new Text("Tuer 50 slimes", "Kill 50 slimes");
    public static readonly Text HuntSlimeChief = new Text("Tuer un chef de meute slime", "Kill a pack leader slime");
    public static readonly Text SunkiumEquip = new Text("Créer un équipement complet en sunkium", "Craft a complete equipement in sunkium");
    public static readonly Text OtherCristalLVL5 = new Text("Améliorer un cristal de récolte et un cristal de guerre au niveau 5", "Upgrade a harvest cristal and a war cristal at level 5");
    public static readonly Text DivineCrisatlLVL5 = new Text("Améliorer un cristal divin au niveau 5", "Uprade a divine cristal at level 5");
    public static readonly Text KillTheBoss = new Text("Tuer le boss", "Kill the boss");
    public static readonly Text FirstEnd = new Text("Choisir la fin\"retour chez soi\"", "Choose the \"back to home\" end");
    public static readonly Text SecondEnd = new Text("choisir la fin \"sacrifice\"", "Choose the \"sacrifice\" end");
    public static readonly Text StoneAge = new Text("Avancer dans l'histoire", "Advance in the story");
    public static readonly Text CopperAge = new Text("Avancer dans l'histoire", "Advance in the story");
    public static readonly Text GoldAge = new Text("Avancer dans l'histoire", "Advance in the story");
    public static readonly Text MithrilAge = new Text("Avancer dans l'histoire", "Advance in the story");
    public static readonly Text FloatiumAge = new Text("Avancer dans l'histoire", "Advance in the story");
    public static readonly Text SunkiumAge = new Text("Avancer dans l'histoire", "Advance in the story");
    public static readonly Text IronAge = new Text("Avancer dans l'histoire", "Advance in the story");


}



