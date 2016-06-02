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

    // Skin 
    public static readonly Text WhiteBody = new Text("Corps blanc", "White skin");
    public static readonly Text BasicBody = new Text("Corps basic", "Basic skin");
    public static readonly Text BlackBody = new Text("Corps noir", "Black skin");
    public static readonly Text DarkBody = new Text("Corps bronzé", "Dark skin");
    public static readonly Text AlienBody = new Text("C'est pas humain ca !", "This isn't a human !");
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

    public static readonly Text BlackEyes = new Text("Yeux noir", "Black Eyes");
    public static readonly Text GreenEyes = new Text("Yeux vert", "Green Eyes");
    public static readonly Text BrwonEyes = new Text("Yeux marron", "Brown Eyes");
    public static readonly Text BlueEyes = new Text("Yeux bleu", "Blue Eyes");
    public static readonly Text PurpleEyes = new Text("Yeux violet", "Purple Eyes");

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


}



