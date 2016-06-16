using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EntityDatabase
{
    // Default
    public static readonly Entity Default = new Entity();

    // Loot

    // Loot
    public static readonly Entity Log = new Entity(0, 60, Resources.Load<GameObject>("Prefabs/Loots/Log"));
    public static readonly Entity Stone = new Entity(1, 60, Resources.Load<GameObject>("Prefabs/Loots/Stone"));
    public static readonly Entity Sand = new Entity(2, 60, Resources.Load<GameObject>("Prefabs/Loots/Sand"));
    public static readonly Entity Copper = new Entity(3, 60, Resources.Load<GameObject>("Prefabs/Loots/Copper"));
    public static readonly Entity Iron = new Entity(4, 60, Resources.Load<GameObject>("Prefabs/Loots/Iron"));
    public static readonly Entity Gold = new Entity(5, 60, Resources.Load<GameObject>("Prefabs/Loots/Gold"));
    public static readonly Entity Mithril = new Entity(6, 60, Resources.Load<GameObject>("Prefabs/Loots/Mithril"));
    public static readonly Entity Floatium = new Entity(7, 60, Resources.Load<GameObject>("Prefabs/Loots/Floatium"));
    public static readonly Entity Sunkium = new Entity(8, 60, Resources.Load<GameObject>("Prefabs/Loots/Sunkium"));

    public static readonly Entity CopperIngot = new Entity(9, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperIngot"));
    public static readonly Entity IronIngot = new Entity(10, 60, Resources.Load<GameObject>("Prefabs/Loots/IronIngot"));
    public static readonly Entity GoldIngot = new Entity(11, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldIngot"));
    public static readonly Entity MithrilIngot = new Entity(12, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilIngot"));
    public static readonly Entity FloatiumIngot = new Entity(13, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumIngot"));
    public static readonly Entity SunkiumIngot = new Entity(14, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumIngot"));

    public static readonly Entity Cact = new Entity(15, 60, Resources.Load<GameObject>("Prefabs/Loots/Cactus"));
    public static readonly Entity Petal = new Entity(16, 60, Resources.Load<GameObject>("Prefabs/Loots/Petal"));
    public static readonly Entity Bone = new Entity(17, 60, Resources.Load<GameObject>("Prefabs/Loots/Bone"));
    public static readonly Entity PumpkinLoot = new Entity(18, 60, Resources.Load<GameObject>("Prefabs/Loots/Pumpkin"));

    public static readonly Entity StoneAxe = new Entity(21, 60, Resources.Load<GameObject>("Prefabs/Loots/StoneAxe"));
    public static readonly Entity CopperAxe = new Entity(22, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperAxe"));
    public static readonly Entity IronAxe = new Entity(23, 60, Resources.Load<GameObject>("Prefabs/Loots/IronAxe"));
    public static readonly Entity GoldAxe = new Entity(24, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldAxe"));
    public static readonly Entity MithrilAxe = new Entity(25, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilAxe"));
    public static readonly Entity FloatiumAxe = new Entity(26, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumAxe"));
    public static readonly Entity SunkiumAxe = new Entity(27, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumAxe"));

    public static readonly Entity LeatherTopArmor = new Entity(30, 60, Resources.Load<GameObject>("Prefabs/Loots/LeatherTopArmor"));
    public static readonly Entity IronTopArmor = new Entity(31, 60, Resources.Load<GameObject>("Prefabs/Loots/IronTopArmor"));
    public static readonly Entity MithrilTopArmor = new Entity(32, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilTopArmor"));
    public static readonly Entity SunkiumTopArmor = new Entity(33, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumTopArmor"));

    public static readonly Entity LeatherBottomArmor = new Entity(34, 60, Resources.Load<GameObject>("Prefabs/Loots/LeatherBottomArmor"));
    public static readonly Entity IronBottomArmor = new Entity(35, 60, Resources.Load<GameObject>("Prefabs/Loots/IronBottomArmor"));
    public static readonly Entity MithrilBottomArmor = new Entity(36, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilBottomArmor"));
    public static readonly Entity SunkiumBottomArmor = new Entity(37, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumBottomArmor"));

    public static readonly Entity CopperBattleAxe = new Entity(38, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperBattleAxe"));
    public static readonly Entity IronBattleAxe = new Entity(39, 60, Resources.Load<GameObject>("Prefabs/Loots/IronBattleAxe"));
    public static readonly Entity GoldBattleAxe = new Entity(40, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldBattleAxe"));
    public static readonly Entity MithrilBattleAxe = new Entity(41, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilBattleAxe"));
    public static readonly Entity FloatiumBattleAxe = new Entity(42, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumBattleAxe"));
    public static readonly Entity SunkiumBattleAxe = new Entity(43, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumBattleAxe"));

    public static readonly Entity CopperSpear = new Entity(44, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperSpear"));
    public static readonly Entity IronSpear = new Entity(45, 60, Resources.Load<GameObject>("Prefabs/Loots/IronSpear"));
    public static readonly Entity GoldSpear = new Entity(46, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldSpear"));
    public static readonly Entity MithrilSpear = new Entity(47, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilSpear"));
    public static readonly Entity FloatiumSpear = new Entity(48, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumSpear"));
    public static readonly Entity SunkiumSpear = new Entity(49, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumSpear"));

    public static readonly Entity CopperPickaxe = new Entity(52, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperPickaxe"));
    public static readonly Entity IronPickaxe = new Entity(53, 60, Resources.Load<GameObject>("Prefabs/Loots/IronPickaxe"));
    public static readonly Entity GoldPickaxe = new Entity(54, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldPickaxe"));
    public static readonly Entity MithrilPickaxe = new Entity(55, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilPickaxe"));
    public static readonly Entity FloatiumPickaxe = new Entity(56, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumPickaxe"));
    public static readonly Entity SunkiumPickaxe = new Entity(57, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumPickaxe"));

    public static readonly Entity StoneSword = new Entity(58, 60, Resources.Load<GameObject>("Prefabs/Loots/StoneSword"));
    public static readonly Entity CopperSword = new Entity(59, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperSword"));
    public static readonly Entity IronSword = new Entity(60, 60, Resources.Load<GameObject>("Prefabs/Loots/IronSword"));
    public static readonly Entity GoldSword = new Entity(61, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldSword"));
    public static readonly Entity MithrilSword = new Entity(62, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilSword"));
    public static readonly Entity FloatiumSword = new Entity(63, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumSword"));
    public static readonly Entity SunkiumSword = new Entity(64, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumSword"));

    public static readonly Entity ForgeLoot = new Entity(65, 60, Resources.Load<GameObject>("Prefabs/Loots/Forge"));
    public static readonly Entity CauldronLoot = new Entity(66, 60, Resources.Load<GameObject>("Prefabs/Loots/Cauldron"));
    public static readonly Entity WorkbenchLoot = new Entity(67, 60, Resources.Load<GameObject>("Prefabs/Loots/Workbench"));
    public static readonly Entity FirepitLoot = new Entity(68, 60, Resources.Load<GameObject>("Prefabs/Loots/Firepit"));
    public static readonly Entity TorchLoot = new Entity(50, 60, Resources.Load<GameObject>("Prefabs/Loots/Torche"));
    public static readonly Entity ChestLoot = new Entity(51, 60, Resources.Load<GameObject>("Prefabs/Loots/Chest"));
    public static readonly Entity WolfTrapLoot = new Entity(52, 60, Resources.Load<GameObject>("Prefabs/Loots/WolfTrap"));
    public static readonly Entity PiquesLoot = new Entity(53, 60, Resources.Load<GameObject>("Prefabs/Loots/Spiques"));
    public static readonly Entity WoodenWallLoot = new Entity(54, 60, Resources.Load<GameObject>("Prefabs/Loots/WoodenWall"));
    public static readonly Entity StoneWallLoot = new Entity(55, 60, Resources.Load<GameObject>("Prefabs/Loots/StoneWall"));


    public static readonly Entity AquaPotion = new Entity(69, 60, Resources.Load<GameObject>("Prefabs/Loots/AquaPotion"));
    public static readonly Entity BluePotion = new Entity(70, 60, Resources.Load<GameObject>("Prefabs/Loots/BluePotion"));
    public static readonly Entity GreenPotion = new Entity(71, 60, Resources.Load<GameObject>("Prefabs/Loots/GreenPotion"));
    public static readonly Entity PurplePotion = new Entity(72, 60, Resources.Load<GameObject>("Prefabs/Loots/PurplePotion"));
    public static readonly Entity RedPotion = new Entity(73, 60, Resources.Load<GameObject>("Prefabs/Loots/RedPotion"));
    public static readonly Entity YellowPotion = new Entity(74, 60, Resources.Load<GameObject>("Prefabs/Loots/YellowPotion"));

    public static readonly Entity MeatBalls = new Entity(75, 60, Resources.Load<GameObject>("Prefabs/Loots/MeatBalls"));
    public static readonly Entity WaterCact = new Entity(76, 60, Resources.Load<GameObject>("Prefabs/Loots/WaterCact"));
    public static readonly Entity MushroomLoot = new Entity(85, 60, Resources.Load<GameObject>("Prefabs/Loots/Mushroom"));
    public static readonly Entity RedMushroomLoot = new Entity(86, 60, Resources.Load<GameObject>("Prefabs/Loots/RedMushroom"));
    public static readonly Entity Soup = new Entity(87, 60, Resources.Load<GameObject>("Prefabs/Loots/Soup"));

    public static readonly Entity WoodenPlank = new Entity(77, 60, Resources.Load<GameObject>("Prefabs/Loots/WoodenPlank"));
    public static readonly Entity Glass = new Entity(78, 60, Resources.Load<GameObject>("Prefabs/Loots/Glass"));
    public static readonly Entity Bowl = new Entity(79, 60, Resources.Load<GameObject>("Prefabs/Loots/Bowl"));
    public static readonly Entity CuttedStone = new Entity(80, 60, Resources.Load<GameObject>("Prefabs/Loots/CuttedStone"));
    public static readonly Entity Stick = new Entity(81, 60, Resources.Load<GameObject>("Prefabs/Loots/Stick"));
    public static readonly Entity Gigot = new Entity(82, 60, Resources.Load<GameObject>("Prefabs/Loots/Gigot"));
    public static readonly Entity Hide = new Entity(83, 60, Resources.Load<GameObject>("Prefabs/Loots/Hide"));
    public static readonly Entity Fang = new Entity(84, 60, Resources.Load<GameObject>("Prefabs/Loots/Fang"));
    public static readonly Entity AnimalFat = new Entity(85, 60, Resources.Load<GameObject>("Prefabs/Loots/AnimalFat"));
    public static readonly Entity Apple = new Entity(86, 60, Resources.Load<GameObject>("Prefabs/Loots/Apple"));
    public static readonly Entity BoarCore = new Entity(87, 60, Resources.Load<GameObject>("Prefabs/Loots/Boarcore"));
    public static readonly Entity SlimeCore = new Entity(88, 60, Resources.Load<GameObject>("Prefabs/Loots/Slimecore"));
    public static readonly Entity PampiCore = new Entity(89, 60, Resources.Load<GameObject>("Prefabs/Loots/Pampicore"));
    public static readonly Entity InstableCore = new Entity(666, 60, Resources.Load<GameObject>("Prefabs/Loots/Instable"));


    // SmallElements
    public static readonly Element Branch = new Element(90, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/Branch"), Element.DestructionTool.None, .5f, new DropConfig(82, 1));
    public static readonly Element ForestFlower = new Element(91, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/ForestFlower"), Element.DestructionTool.None, .5f, new DropConfig(81, 1));
    public static readonly Element IceFlower = new Element(92, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/IceFlower"), Element.DestructionTool.None, .5f, new DropConfig(81, 1));
    public static readonly Element SmallCactus = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/SmallCactus"), Element.DestructionTool.None, .5f, new DropConfig(81, 1), new DropConfig(80, 1));
    public static readonly Element Mushroom = new Element(94, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/Mushroom"), Element.DestructionTool.None, .5f, new DropConfig(27, 1, 2));
    public static readonly Element RedMushroom = new Element(95, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/RedMushroom"), Element.DestructionTool.None, .5f, new DropConfig(28, 1, 2));
    public static readonly Element Pumpink = new Element(96, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/Pumpkin"), Element.DestructionTool.None, .5f, new DropConfig(87, 1, 3));
    public static readonly Element LittleRock = new Element(97, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/LittleRock"), Element.DestructionTool.None, .5f, new DropConfig(1, 1));

    // Tree
    public static readonly Element Fir = new Element(100, 50, Resources.Load<GameObject>("Prefabs/Elements/Trees/Fir"), Element.DestructionTool.Axe, 1.25f, new DropConfig(0, 4, 8));
    public static readonly Element SnowFir = new Element(101, 50, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowFir"), Element.DestructionTool.Axe, 1.25f, new DropConfig(0, 4, 8));
    public static readonly Element Cactus = new Element(102, 15, Resources.Load<GameObject>("Prefabs/Elements/Trees/Cactus"), Element.DestructionTool.Axe, .5f, new DropConfig(80, 3, 6), new DropConfig(81, 1, 3));
    public static readonly Element Oak = new Element(103, 50, Resources.Load<GameObject>("Prefabs/Elements/Trees/Oak"), Element.DestructionTool.Axe, .6f, new DropConfig(0, 4, 8), new DropConfig(30, 0, 1));
    public static readonly Element SnowOak = new Element(104, 50, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowOak"), Element.DestructionTool.Axe, .6f, new DropConfig(0, 4, 8));
    public static readonly Element FallOak1 = new Element(105, 50, Resources.Load<GameObject>("Prefabs/Elements/Trees/FallOak1"), Element.DestructionTool.Axe, .6f, new DropConfig(0, 4, 8), new DropConfig(30, 0, 1));
    public static readonly Element FallOak2 = new Element(106, 50, Resources.Load<GameObject>("Prefabs/Elements/Trees/FallOak2"), Element.DestructionTool.Axe, .6f, new DropConfig(0, 4, 8), new DropConfig(30, 0, 1));

    // Rock
    public static readonly Element StoneRock = new Element(110, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/Stone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(1, 4, 8), new DropConfig(2, 0, 1));
    public static readonly Element CopperRock = new Element(111, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/CopperStone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(3, 1, 4), new DropConfig(1, 2, 4), new DropConfig(2, 0, 1));
    public static readonly Element IronRock = new Element(112, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/IronStone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(4, 1, 4), new DropConfig(1, 2, 4), new DropConfig(2, 0, 1));
    public static readonly Element GoldRock = new Element(113, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/GoldStone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(5, 1, 4), new DropConfig(1, 2, 4), new DropConfig(2, 0, 1));
    public static readonly Element MithrilRock = new Element(114, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/MithrilStone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(6, 1, 4), new DropConfig(1, 2, 4), new DropConfig(2, 0, 1));
    public static readonly Element FloatiumRock = new Element(115, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/FloatiumStone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(7, 1, 3), new DropConfig(1, 2, 4), new DropConfig(2, 0, 1));
    public static readonly Element SunkiumRock = new Element(116, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/SunkiumStone"), Element.DestructionTool.Pickaxe, 1.3f, new DropConfig(8, 1, 3), new DropConfig(1, 2, 4), new DropConfig(2, 0, 1));
    public static readonly Element Squeleton = new Element(117, 50, Resources.Load<GameObject>("Prefabs/Elements/Rocks/Bones"), Element.DestructionTool.Pickaxe, 1f, new DropConfig(86, 1, 3));

    // WorkTop
    public static readonly Element Cauldron = new Element(130, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/Cauldron"), Element.DestructionTool.Pickaxe, .5f, new DropConfig(41, 1));
    public static readonly Element Firepit = new Element(131, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/FirePit"), Element.DestructionTool.Indestructible, 0, new DropConfig(43, 1));
    public static readonly Element Workbench = new Element(132, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/Workbench"), Element.DestructionTool.Axe, 1f, new DropConfig(42, 1));
    public static readonly Element Forge = new Element(133, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/Hoven"), Element.DestructionTool.Pickaxe, 1.5f, new DropConfig(40, 1));
    public static readonly Element Torch = new Element(134, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/Torch"), Element.DestructionTool.None, .5f, new DropConfig(44, 1));
    public static readonly Chest Chest = new Chest(135, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/Chest"));
    public static readonly Element WolfTrap = new Element(136, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/WolfTrap"), Element.DestructionTool.Indestructible, .5f);
    public static readonly Element Piques = new Element(137, 100, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/Spiques"), Element.DestructionTool.Pickaxe, .5f, new DropConfig(47, 1));
    public static readonly Element WoodenWall = new Element(138, 200, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/WoodenWall"), Element.DestructionTool.Axe, 1f, new DropConfig(15, 1, 2), new DropConfig(15, 1, 2), new DropConfig(0, 1), new DropConfig(0, 0, 1));
    public static readonly Element StoneWall = new Element(139, 300, Resources.Load<GameObject>("Prefabs/Elements/PuttedObjects/StoneWall"), Element.DestructionTool.Pickaxe, 1f, new DropConfig(18, 1, 2), new DropConfig(18, 1, 2), new DropConfig(1, 1), new DropConfig(1, 0, 1));

    // IslandCore
    public static readonly IslandCore IslandCore = new IslandCore(142, Resources.Load<GameObject>("Prefabs/Elements/Cristals/IslandCore"));

    // Mobs
    public static readonly Mob Boar = new Mob(500, 20, Resources.Load<GameObject>("Prefabs/Mobs/Boar"), 15, .5f, 7, 6f, 7f, 1.3f, 2.8f, 1.5f, 1f,
		new int[3] { 0, 2, 3 }, new DropConfig(83, 1), new DropConfig(83, 0, 1), new DropConfig(84, 1), new DropConfig(84, 0, 1), new DropConfig(86, 0, 1), new DropConfig(88, 1, 2),new DropConfig(85,0,2));
    public static readonly Mob BoarChief = new Mob(501, 35, Resources.Load<GameObject>("Prefabs/Mobs/BoarChief"), 1, 1f, 15, 8f, 0f, 1.8f, 3.2f, 2f, 1.5f,
		new int[3] { 0, 2, 3 }, new DropConfig(83, 2), new DropConfig(83, 1, 2), new DropConfig(84, 1), new DropConfig(84, 0, 1), new DropConfig(86, 1, 2), new DropConfig(88, 2, 3), new DropConfig(90,1), new DropConfig(85,0,2));
    public static readonly Mob Pampa = new Mob(502, 15, Resources.Load<GameObject>("Prefabs/Mobs/Pampa"), 15, .7f, 8, 1.5f, 8f, 1f, 3.8f, 1.8f, 1f,
        new int[1] { 1 }, new DropConfig(83, 1), new DropConfig(83, 0, 1), new DropConfig(80, 1), new DropConfig(80, 0, 1), new DropConfig(86, 0, 1));
    public static readonly Mob PampaChief = new Mob(503, 25, Resources.Load<GameObject>("Prefabs/Mobs/PampaChief"), 1, 1f, 16, 2f, 9f, 1.3f, 4.2f, 2.5f, 1f,
		new int[1] { 1 }, new DropConfig(83, 2), new DropConfig(83, 1, 2), new DropConfig(80, 2), new DropConfig(80, 1, 2), new DropConfig(86, 1, 2), new DropConfig(91,1));
    public static readonly Mob SnowBunny = new Mob(504, 10, Resources.Load<GameObject>("Prefabs/Mobs/SnowBunny"), 15, .25f, 0, 0f, 6f, 1.5f, 3f, 1f, 0f,
		new int[1] { 2 }, new DropConfig(83, 1), new DropConfig(86, 0, 1), new DropConfig(88, 1),new DropConfig(85,0,1));
    public static readonly Mob Bunny = new Mob(505, 10, Resources.Load<GameObject>("Prefabs/Mobs/Bunny"), 15, .25f, 0, 0f, 6f, 1.5f, 3f, 1f, 0f,
		new int[2] { 0, 3 }, new DropConfig(83, 1), new DropConfig(86, 0, 1), new DropConfig(88, 1),new DropConfig(85,0,1));
    public static readonly Mob Penguin = new Mob(506, 10, Resources.Load<GameObject>("Prefabs/Mobs/Penguin"), 20, 1f, 0, 6f,0f, 0.8f, 1.5f, 1f, 2f,
      new int[1] { 2 }, new DropConfig(83, 1), new DropConfig(86, 0, 1), new DropConfig(88, 1));

    // Chunk
    public static readonly Chunk Chunk0_Empty = new Chunk(1000, Resources.Load<GameObject>("Prefabs/Chunks/Chunk0_Empty"), Bridges.None);

    public static readonly Chunk Chunk1_One = new Chunk(1001, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_One"), Bridges.One);
    public static readonly Chunk Chunk1_TwoI = new Chunk(1002, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk1_TwoL = new Chunk(1003, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk1_Three = new Chunk(1004, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_Three"), Bridges.Three);
    public static readonly Chunk Chunk1_All = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_All"), Bridges.All);

    public static readonly Chunk Chunk2_One = new Chunk(1006, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_One"), Bridges.One);
    public static readonly Chunk Chunk2_TwoI = new Chunk(1007, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk2_TwoL = new Chunk(1008, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk2_Three = new Chunk(1009, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Three"), Bridges.Three);
    public static readonly Chunk Chunk2_All = new Chunk(1010, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_All"), Bridges.All);

    public static readonly Chunk Chunk3_One = new Chunk(1011, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_One"), Bridges.One);
    public static readonly Chunk Chunk3_TwoI = new Chunk(1012, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk3_TwoL = new Chunk(1013, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk3_Three = new Chunk(1014, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_Three"), Bridges.Three);
    public static readonly Chunk Chunk3_All = new Chunk(1015, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_All"), Bridges.All);

    public static readonly Chunk Chunk4_One = new Chunk(1016, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_One"), Bridges.One);
    public static readonly Chunk Chunk4_TwoI = new Chunk(1017, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk4_TwoL = new Chunk(1018, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk4_Three = new Chunk(1019, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_Three"), Bridges.Three);

    public static readonly Chunk Chunk5_One = new Chunk(1020, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_One"), Bridges.One);
    public static readonly Chunk Chunk5_TwoI = new Chunk(1021, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk5_TwoL = new Chunk(1022, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk5_Three = new Chunk(1023, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_Three"), Bridges.Three);

    public static readonly Chunk Chunk6_One = new Chunk(1024, Resources.Load<GameObject>("Prefabs/Chunks/Chunk6_One"), Bridges.One);
    public static readonly Chunk Chunk6_TwoI = new Chunk(1025, Resources.Load<GameObject>("Prefabs/Chunks/Chunk6_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk6_TwoL = new Chunk(1026, Resources.Load<GameObject>("Prefabs/Chunks/Chunk6_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk6_Three = new Chunk(1027, Resources.Load<GameObject>("Prefabs/Chunks/Chunk6_Three"), Bridges.Three);
    public static readonly Chunk Chunk6_All = new Chunk(1028, Resources.Load<GameObject>("Prefabs/Chunks/Chunk6_All"), Bridges.All);

    public static readonly Chunk Chunk7_One = new Chunk(1029, Resources.Load<GameObject>("Prefabs/Chunks/Chunk7_One"), Bridges.One);
    public static readonly Chunk Chunk7_TwoI = new Chunk(1030, Resources.Load<GameObject>("Prefabs/Chunks/Chunk7_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk7_TwoL = new Chunk(1031, Resources.Load<GameObject>("Prefabs/Chunks/Chunk7_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk7_Three = new Chunk(1032, Resources.Load<GameObject>("Prefabs/Chunks/Chunk7_Three"), Bridges.Three);
    public static readonly Chunk Chunk7_All = new Chunk(1033, Resources.Load<GameObject>("Prefabs/Chunks/Chunk7_All"), Bridges.All);

    public static readonly Chunk Chunk8_One = new Chunk(1034, Resources.Load<GameObject>("Prefabs/Chunks/Chunk8_One"), Bridges.One);
    public static readonly Chunk Chunk8_TwoI = new Chunk(1035, Resources.Load<GameObject>("Prefabs/Chunks/Chunk8_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk8_TwoL = new Chunk(1036, Resources.Load<GameObject>("Prefabs/Chunks/Chunk8_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk8_Three = new Chunk(1037, Resources.Load<GameObject>("Prefabs/Chunks/Chunk8_Three"), Bridges.Three);
    public static readonly Chunk Chunk8_All = new Chunk(1038, Resources.Load<GameObject>("Prefabs/Chunks/Chunk8_All"), Bridges.All);

    /// <summary>
    /// Liste tous les entites du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Entity> Entitys
    {
        get
        {
            // Default
            yield return Default;
            // Material
            yield return Log;
            yield return Stone;
            yield return Sand;
            yield return Iron;
            yield return Copper;
            yield return Gold;
            yield return Mithril;
            yield return Floatium;
            yield return Sunkium;

            yield return WoodenPlank;
            yield return Glass;
            yield return Bowl;
            yield return CuttedStone;
            yield return Cact;
            yield return Petal;
            yield return Stick;
            yield return Gigot;
            yield return Hide;
            yield return Fang;
            yield return PumpkinLoot;
            yield return Bone;
            yield return Apple;
            yield return AnimalFat;

            yield return BoarCore;
            yield return SlimeCore;
            yield return PampiCore;
            yield return InstableCore;

            //Ingot
            yield return IronIngot;
            yield return CopperIngot;
            yield return GoldIngot;
            yield return MithrilIngot;
            yield return FloatiumIngot;
            yield return SunkiumIngot;

            //Axe
            yield return IronAxe;
            yield return CopperAxe;
            yield return GoldAxe;
            yield return MithrilAxe;
            yield return FloatiumAxe;
            yield return SunkiumAxe;

            //Pickaxe
            yield return IronPickaxe;
            yield return CopperPickaxe;
            yield return GoldPickaxe;
            yield return MithrilPickaxe;
            yield return FloatiumPickaxe;
            yield return SunkiumPickaxe;

            //Sword
            yield return IronSword;
            yield return CopperSword;
            yield return GoldSword;
            yield return MithrilSword;
            yield return FloatiumSword;
            yield return SunkiumSword;

            //Potions
            yield return AquaPotion;
            yield return BluePotion;
            yield return GreenPotion;
            yield return RedPotion;
            yield return PurplePotion;
            yield return YellowPotion;

            //Food 
            yield return MeatBalls;
            yield return MushroomLoot;
            yield return RedMushroomLoot;
            yield return WaterCact;
            yield return Soup;

            //Armor

            yield return LeatherTopArmor;
            yield return IronTopArmor;
            yield return MithrilTopArmor;
            yield return SunkiumTopArmor;

            yield return LeatherBottomArmor;
            yield return IronBottomArmor;
            yield return MithrilBottomArmor;
            yield return SunkiumBottomArmor;

            //Worktops
            yield return ForgeLoot;
            yield return CauldronLoot;
            yield return WorkbenchLoot;
            yield return FirepitLoot;
            yield return TorchLoot;
            yield return ChestLoot;
            yield return WolfTrapLoot;
            yield return PiquesLoot;
            yield return WoodenWallLoot;
            yield return StoneWallLoot;

            // Chunk
            foreach (Chunk chunk in Chunks)
                yield return chunk;

            // IslandCore
            foreach (Element el in Elements)
                yield return el;

            // Mobs
            foreach (Mob mob in Mobs)
                yield return mob;
        }
    }

    /// <summary>
    /// Liste tous les elements du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Element> Elements
    {
        get
        {
            // Small Elements
            yield return Branch;
            yield return ForestFlower;
            yield return IceFlower;
            yield return SmallCactus;
            yield return LittleRock;
            yield return RedMushroom;
            yield return Mushroom;
            yield return Pumpink;

            // Tree
            yield return Fir;
            yield return SnowFir;
            yield return Cactus;
            yield return Oak;
            yield return SnowOak;
            yield return FallOak1;
            yield return FallOak2;

            // Rocks
            yield return StoneRock;
            yield return CopperRock;
            yield return IronRock;
            yield return GoldRock;
            yield return MithrilRock;
            yield return FloatiumRock;
            yield return SunkiumRock;
            yield return Squeleton;

            // WorkTop
            yield return Cauldron;
            yield return Firepit;
            yield return Forge;
            yield return Workbench;
            yield return Torch;
            yield return Chest;
            yield return WolfTrap;
            yield return Piques;
            yield return WoodenWall;
            yield return StoneWall;

            // IslandCore
            yield return IslandCore;
        }
    }

    /// <summary>
    /// Liste tous les chunks du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Chunk> Chunks
    {
        get
        {
            yield return Chunk0_Empty;

            yield return Chunk1_One;
            yield return Chunk1_TwoI;
            yield return Chunk1_TwoL;
            yield return Chunk1_Three;
            yield return Chunk1_All;

            yield return Chunk2_One;
            yield return Chunk2_TwoI;
            yield return Chunk2_TwoL;
            yield return Chunk2_Three;
            yield return Chunk2_All;

            yield return Chunk3_One;
            yield return Chunk3_TwoI;
            yield return Chunk3_TwoL;
            yield return Chunk3_Three;
            yield return Chunk3_All;

            yield return Chunk4_One;
            yield return Chunk4_TwoI;
            yield return Chunk4_TwoL;
            yield return Chunk4_Three;

            yield return Chunk5_One;
            yield return Chunk5_TwoI;
            yield return Chunk5_TwoL;
            yield return Chunk5_Three;

            yield return Chunk6_One;
            yield return Chunk6_TwoI;
            yield return Chunk6_TwoL;
            yield return Chunk6_Three;
            yield return Chunk6_All;

            yield return Chunk7_One;
            yield return Chunk7_TwoI;
            yield return Chunk7_TwoL;
            yield return Chunk7_Three;
            yield return Chunk7_All;

            yield return Chunk8_One;
            yield return Chunk8_TwoI;
            yield return Chunk8_TwoL;
            yield return Chunk8_Three;
            yield return Chunk8_All;
        }
    }

    /// <summary>
    /// Liste tous les mobs du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Mob> Mobs
    {
        get
        {
            yield return Boar;
            yield return BoarChief;
            yield return Pampa;
            yield return PampaChief;
            yield return SnowBunny;
            yield return Bunny;
            yield return Penguin;
        }
    }

    /// <summary>
    /// Retourne un chunk aleatoire. (Une copie)
    /// </summary>
    public static Chunk RandChunk(Bridges bridge, System.Random rand)
    {
        List<Chunk> chunks = new List<Chunk>();
        foreach (Chunk c in Chunks)
            if (c.Bridge == bridge)
                chunks.Add(c);

        return new Chunk(chunks[rand.Next(chunks.Count)]);
    }

    /// <summary>
    /// Recherche une entite par son identifiant et la retourne. (Une copie)
    /// </summary>
    public static Entity Find(int id)
    {
        foreach (Entity i in Entitys)
        {
            if (i.ID == id)
            {
                if (i is Chunk)
                    return new Chunk((Chunk)i);
                else if (i is IslandCore)
                    return new IslandCore((IslandCore)i);
                else if (i is Chest)
                    return new Chest((Chest)i);
                else if (i is Element)
                    return new Element((Element)i);
                else
                    return new Entity(i);
            }
        }
        throw new System.Exception("Items.Find : Item not found");
    }
}
