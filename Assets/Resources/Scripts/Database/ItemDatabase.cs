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

    public static readonly Item WoodenPlank = new Item(15, TextDatabase.WoodenPlank, TextDatabase.WoodenPlankDescription, 20, Resources.Load<Texture2D>("Sprites/Items/Elementaries/WoodenPlank"), new Entity(EntityDatabase.WoodenPlank));
    public static readonly Item Glass = new Item(16, TextDatabase.Glass, TextDatabase.GlassDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/Glass"), new Entity(EntityDatabase.Glass));
    public static readonly Item Bowl = new Item(17, TextDatabase.Bowl, TextDatabase.BowlDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/Bowl"), new Entity(EntityDatabase.Bowl));
    public static readonly Item CuttedStone = new Item(18, TextDatabase.CuttedStone, TextDatabase.CuttedStoneDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/CuttedStone"), new Entity(EntityDatabase.CuttedStone));

    // Potions
    public static readonly Consumable WaterPotion = new Consumable(19, TextDatabase.WaterPotion, TextDatabase.WaterPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/AquaPotion"), new Entity(EntityDatabase.AquaPotion), new Effect(Effect.EffectType.Refreshment, 10), Resources.Load<GameObject>("Prefabs/Consumables/Potions/Aqua"));
    public static readonly Consumable SpeedPotion = new Consumable(20, TextDatabase.SpeedPotion, TextDatabase.SpeedPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/BluePotion"), new Entity(EntityDatabase.BluePotion), new Effect(Effect.EffectType.Speed, 2), Resources.Load<GameObject>("Prefabs/Consumables/Potions/Blue"));
    public static readonly Consumable PoisonPotion = new Consumable(21, TextDatabase.PoisonPotion, TextDatabase.PoisonPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/GreenPotion"), new Entity(EntityDatabase.GreenPotion), new Effect(Effect.EffectType.Poison, 2), Resources.Load<GameObject>("Prefabs/Consumables/Potions/Green"));
    public static readonly Consumable RegenerationPotion = new Consumable(22, TextDatabase.RegenerationPotion, TextDatabase.RegenerationPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/PurplePotion"), new Entity(EntityDatabase.PurplePotion), new Effect(Effect.EffectType.Regeneration, 1), Resources.Load<GameObject>("Prefabs/Consumables/Potions/Purple"));
    public static readonly Consumable HealingPotion = new Consumable(23, TextDatabase.HealingPotion, TextDatabase.HealingPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/RedPotion"), new Entity(EntityDatabase.RedPotion), new Effect(Effect.EffectType.InstantHealth, 2), Resources.Load<GameObject>("Prefabs/Consumables/Potions/Red"));
    public static readonly Consumable JumpPotion = new Consumable(24, TextDatabase.JumpPotion, TextDatabase.JumpPotionDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Potions/YellowPotion"), new Entity(EntityDatabase.YellowPotion), new Effect(Effect.EffectType.JumpBoost, 1), Resources.Load<GameObject>("Prefabs/Consumables/Potions/Yellow"));

    // Foods
    public static readonly Consumable MeatBalls = new Consumable(25, TextDatabase.MeatBalls, TextDatabase.MeatBallsDcription, 64, Resources.Load<Texture2D>("Sprites/Items/Foods/MeatBalls"), new Entity(EntityDatabase.MeatBalls), new Effect(Effect.EffectType.Saturation, 3), Resources.Load<GameObject>("Prefabs/Consumables/Foods/MeatBalls"));
    public static readonly Consumable WaterCact = new Consumable(26, TextDatabase.WaterCact, TextDatabase.WaterCactDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Foods/WaterCact"), new Entity(EntityDatabase.WaterCact), new Effect(Effect.EffectType.Refreshment, 3), Resources.Load<GameObject>("Prefabs/Consumables/Foods/WaterCact"));
    public static readonly Consumable Mushroom = new Consumable(27, TextDatabase.Mushroom, TextDatabase.MushroomDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Mushroom"), new Entity(EntityDatabase.MushroomLoot), new Effect(Effect.EffectType.Saturation, 2), Resources.Load<GameObject>("Prefabs/Consumables/Foods/Mushroom"));
    public static readonly Consumable RedMushroom = new Consumable(28, TextDatabase.RedMushroom, TextDatabase.RedMushroom, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/RedMushroom"), new Entity(EntityDatabase.RedMushroomLoot), new Effect(Effect.EffectType.Poison, 1), Resources.Load<GameObject>("Prefabs/Consumables/Foods/RedMushroom"));
    public static readonly Consumable Soup = new Consumable(29, TextDatabase.Soup, TextDatabase.SoupDescription, 20, Resources.Load<Texture2D>("Sprites/Items/Foods/Soup"), new Entity(EntityDatabase.Soup), new Effect(Effect.EffectType.Saturation, 1), Resources.Load<GameObject>("Prefabs/Consumables/Foods/Soup"));
    public static readonly Consumable Apple = new Consumable(30, TextDatabase.Apple, TextDatabase.AppleDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Apple"), new Entity(EntityDatabase.Apple), new Effect(Effect.EffectType.Saturation, 1), Resources.Load<GameObject>("Prefabs/Consumables/Foods/Apple"));
    public static readonly Consumable SlimeGoo = new Consumable(31, TextDatabase.SlimeGoo, TextDatabase.SlimeGooDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Animals/SlimeGoo"), new Entity(EntityDatabase.SlimeGoo), new Effect(Effect.EffectType.Refreshment, 2), Resources.Load<GameObject>("Prefabs/Consumables/SlimeGoo"));

    // WorckTops
    public static readonly WorkTop Forge = new WorkTop(40, TextDatabase.Forge, TextDatabase.ForgeDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Forge"), new Entity(EntityDatabase.ForgeLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Hoven"), 133);
    public static readonly WorkTop Cauldron = new WorkTop(41, TextDatabase.Cauldron, TextDatabase.CauldronDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Cauldron"), new Entity(EntityDatabase.CauldronLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Cauldron"), 130);
    public static readonly WorkTop Workbench = new WorkTop(42, TextDatabase.Workbench, TextDatabase.WorkbenchDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Workbench"), new Entity(EntityDatabase.WorkbenchLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Workbench"), 132);
    public static readonly WorkTop Firepit = new WorkTop(43, TextDatabase.Firepit, TextDatabase.FirepitDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Firepit"), new Entity(EntityDatabase.FirepitLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Firepit"), 131);
    public static readonly WorkTop Torch = new WorkTop(44, TextDatabase.Torch, TextDatabase.TorchDescription, 12, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Torch"), new Entity(EntityDatabase.TorchLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Torch"), 134);
    public static readonly WorkTop Chest = new WorkTop(45, TextDatabase.Chest, TextDatabase.ChestDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Chest"), new Entity(EntityDatabase.ChestLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Chest"), 135);
    public static readonly WorkTop WolfTrap = new WorkTop(46, TextDatabase.WolfTrap, TextDatabase.WolfTrapDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/WolfTrap"), new Entity(EntityDatabase.WolfTrapLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/WolfTrap"), 136);
	public static readonly WorkTop Piques = new WorkTop(47, TextDatabase.Spiques, TextDatabase.SpiquesDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/Spiques"), new Entity(EntityDatabase.PiquesLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/Spiques"), 137);
	public static readonly WorkTop WoodenWall = new WorkTop(48, TextDatabase.WoodenWall, TextDatabase.WoodenWallDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/WoodenWall"), new Entity(EntityDatabase.WoodenWallLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/WoodenWall"), 138);
	public static readonly WorkTop StoneWall = new WorkTop(49, TextDatabase.StoneWall, TextDatabase.StoneWallDescription, 1, Resources.Load<Texture2D>("Sprites/Items/WorkStation/StoneWall"), new Entity(EntityDatabase.StoneWallLoot), Resources.Load<GameObject>("PrefabsNoNetID/Previsualisations/StoneWall"), 139);

    // Tools |change the stonepickaxe if willing|
    public static readonly Pickaxe StonePickaxe = new Pickaxe(50, TextDatabase.StonePickaxe, TextDatabase.StonePickaxeDescription, 100, 100, Resources.Load<Texture2D>("Sprites/Items/Tools/IronPickaxe"), new Entity(EntityDatabase.IronPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/IronPickaxe"));
    public static readonly Pickaxe CopperPickaxe = new Pickaxe(51, TextDatabase.CopperPickaxe, TextDatabase.CopperPickaxeDescription, 150, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperPickaxe"), new Entity(EntityDatabase.CopperPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/CopperPickaxe"));
    public static readonly Pickaxe IronPickaxe = new Pickaxe(52, TextDatabase.IronPickaxe, TextDatabase.IronPickaxeDescription, 250, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/IronPickaxe"), new Entity(EntityDatabase.IronPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/IronPickaxe"));
    public static readonly Pickaxe GoldPickaxe = new Pickaxe(53, TextDatabase.GoldPickaxe, TextDatabase.GoldPickaxeDescription, 50, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldPickaxe"), new Entity(EntityDatabase.GoldPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/GoldPickaxe"));
    public static readonly Pickaxe MithrilPickaxe = new Pickaxe(54, TextDatabase.MithrilPickaxe, TextDatabase.MithrilPickaxeDescription, 200, 200, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilPickaxe"), new Entity(EntityDatabase.MithrilPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/MithrilPickaxe"));
    public static readonly Pickaxe FloatiumPickaxe = new Pickaxe(55, TextDatabase.FloatiumPickaxe, TextDatabase.FloatiumPickaxeDescription, 200, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumPickaxe"), new Entity(EntityDatabase.FloatiumPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/FloatiumPickaxe"));
    public static readonly Pickaxe SunkiumPickaxe = new Pickaxe(56, TextDatabase.SunkiumPickaxe, TextDatabase.SunkiumPickaxeDescription, 500, 300, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumPickaxe"), new Entity(EntityDatabase.SunkiumPickaxe), Resources.Load<GameObject>("Prefabs/Tools/Pickaxes/SunkiumPickaxe"));

    public static readonly Axe StoneAxe = new Axe(60, TextDatabase.StoneAxe, TextDatabase.StoneAxeDescription, 100, 100, Resources.Load<Texture2D>("Sprites/Items/Tools/StoneAxe"), new Entity(EntityDatabase.StoneAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/StoneAxe"));
    public static readonly Axe CopperAxe = new Axe(61, TextDatabase.CopperAxe, TextDatabase.CopperAxeDescription, 150, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperAxe"), new Entity(EntityDatabase.CopperAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/CopperAxe"));
    public static readonly Axe IronAxe = new Axe(62, TextDatabase.IronAxe, TextDatabase.IronAxeDescription, 250, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/IronAxe"), new Entity(EntityDatabase.IronAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/IronAxe"));
    public static readonly Axe GoldAxe = new Axe(63, TextDatabase.GoldAxe, TextDatabase.GoldAxeDescription, 50, 150, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldAxe"), new Entity(EntityDatabase.GoldAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/GoldAxe"));
    public static readonly Axe MithrilAxe = new Axe(64, TextDatabase.MithrilAxe, TextDatabase.MithrilAxeDescription, 200, 200, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilAxe"), new Entity(EntityDatabase.MithrilAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/MithrilAxe"));
    public static readonly Axe FloatiumAxe = new Axe(65, TextDatabase.FloatiumAxe, TextDatabase.FloatiumAxeDescription, 200, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumAxe"), new Entity(EntityDatabase.FloatiumAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/FloatiumAxe"));
    public static readonly Axe SunkiumAxe = new Axe(66, TextDatabase.SunkiumAxe, TextDatabase.SunkiumAxeDescription, 500, 300, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumAxe"), new Entity(EntityDatabase.SunkiumAxe), Resources.Load<GameObject>("Prefabs/Tools/Axes/SunkiumAxe"));

    public static readonly Sword StoneSword = new Sword(70, TextDatabase.StoneSword, TextDatabase.StoneSwordDescription, 100, 140, Resources.Load<Texture2D>("Sprites/Items/Tools/StoneSword"), new Entity(EntityDatabase.StoneSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/StoneSword"));
    public static readonly Sword CopperSword = new Sword(71, TextDatabase.CopperSword, TextDatabase.CopperSwordDescription, 150, 180, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperSword"), new Entity(EntityDatabase.CopperSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/CopperSword"));
    public static readonly Sword IronSword = new Sword(72, TextDatabase.IronSword, TextDatabase.IronSwordDescription, 250, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/IronSword"), new Entity(EntityDatabase.IronSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/IronSword"));
    public static readonly Sword GoldSword = new Sword(73, TextDatabase.GoldSword, TextDatabase.GoldSwordDescription, 50, 180, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldSword"), new Entity(EntityDatabase.GoldSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/GoldSword"));
    public static readonly Sword MithrilSword = new Sword(74, TextDatabase.MithrilSword, TextDatabase.MithrilSwordDescription, 200, 200, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilSword"), new Entity(EntityDatabase.MithrilSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/MithrilSword"));
    public static readonly Sword FloatiumSword = new Sword(75, TextDatabase.FloatiumSword, TextDatabase.FloatiumSwordDescription, 200, 250, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumSword"), new Entity(EntityDatabase.FloatiumSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/FloatiumSword"));
    public static readonly Sword SunkiumSword = new Sword(76, TextDatabase.SunkiumSword, TextDatabase.SunkiumSwordDescription, 500, 300, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumSword"), new Entity(EntityDatabase.SunkiumSword), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Swords/SunkiumSword"));

    public static readonly Item Cact = new Item(80, TextDatabase.Cactus, TextDatabase.CactusDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Cactus"), new Entity(EntityDatabase.Cact));
    public static readonly Item Petal = new Item(81, TextDatabase.Petal, TextDatabase.PetalDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Petal"), new Entity(EntityDatabase.Petal));
    public static readonly Item Stick = new Item(82, TextDatabase.Stick, TextDatabase.StickDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Elementaries/Stick"), new Entity(EntityDatabase.Stick));
    public static readonly Item Gigot = new Item(83, TextDatabase.Gigot, TextDatabase.GigotDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Animals/Gigot"), new Entity(EntityDatabase.Gigot));
    public static readonly Item Fang = new Item(84, TextDatabase.Fang, TextDatabase.FangDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Animals/Fang"), new Entity(EntityDatabase.Fang));
    public static readonly Item Hide = new Item(85, TextDatabase.Hide, TextDatabase.HideDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Animals/Hide"), new Entity(EntityDatabase.Hide));
    public static readonly Item Bone = new Item(86, TextDatabase.Bone, TextDatabase.BoneDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Foods/Bone"), new Entity(EntityDatabase.Bone));
    public static readonly Item Pumpkin = new Item(87, TextDatabase.Pumpkin, TextDatabase.PumpkinDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Plants/Pumpkin"), new Entity(EntityDatabase.PumpkinLoot));
    public static readonly Item AnimalFat = new Item(88, TextDatabase.AnimalFat, TextDatabase.AnimalFatDescription, 64, Resources.Load<Texture2D>("Sprites/Items/Animals/AnimalFat"), new Entity(EntityDatabase.AnimalFat));

    public static readonly Item SlimeCore = new Item(89, TextDatabase.SlimeCore, TextDatabase.SlimeCoreDescription, 5, Resources.Load<Texture2D>("Sprites/Items/Ores/SlimeCore"), new Entity(EntityDatabase.SlimeCore));
	public static readonly Item BoarCore = new Item(90, TextDatabase.BoarCore, TextDatabase.BoarCoreDescription, 5, Resources.Load<Texture2D>("Sprites/Items/Ores/BoarCore"), new Entity(EntityDatabase.BoarCore));
	public static readonly Item PampiCore = new Item(91, TextDatabase.PampiCore, TextDatabase.PampiCoreDescription, 5, Resources.Load<Texture2D>("Sprites/Items/Ores/PampiCore"), new Entity(EntityDatabase.PampiCore));

    public static readonly BattleAxe CopperBattleAxe = new BattleAxe(100, TextDatabase.CopperBattleAxe, TextDatabase.CopperBattleAxeDescription, 150, 360, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperBattleAxe"), new Entity(EntityDatabase.CopperBattleAxe), Resources.Load<GameObject>("Prefabs/Tools/Weapons/BattleAxe/CopperBattleAxe"));
    public static readonly BattleAxe IronBattleAxe = new BattleAxe(101, TextDatabase.IronBattleAxe, TextDatabase.IronBattleAxeDescription, 250, 400, Resources.Load<Texture2D>("Sprites/Items/Tools/IronBattleAxe"), new Entity(EntityDatabase.IronBattleAxe), Resources.Load<GameObject>("Prefabs/Tools/Weapons/BattleAxe/IronBattleAxe"));
    public static readonly BattleAxe GoldBattleAxe = new BattleAxe(102, TextDatabase.GoldBattleAxe, TextDatabase.GoldBattleAxeDescription, 50, 360, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldBattleAxe"), new Entity(EntityDatabase.GoldBattleAxe), Resources.Load<GameObject>("Prefabs/Tools/Weapons/BattleAxe/GoldBattleAxe"));
    public static readonly BattleAxe MithrilBattleAxe = new BattleAxe(103, TextDatabase.MithrilBattleAxe, TextDatabase.MithrilBattleAxeDescription, 200, 400, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilBattleAxe"), new Entity(EntityDatabase.MithrilBattleAxe), Resources.Load<GameObject>("Prefabs/Tools/Weapons/BattleAxe/MithrilBattleAxe"));
    public static readonly BattleAxe FloatiumBattleAxe = new BattleAxe(104, TextDatabase.FloatiumBattleAxe, TextDatabase.FloatiumBattleAxeDescription, 200, 500, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumBattleAxe"), new Entity(EntityDatabase.FloatiumBattleAxe), Resources.Load<GameObject>("Prefabs/Tools/Weapons/BattleAxe/FloatiumBattleAxe"));
    public static readonly BattleAxe SunkiumBattleAxe = new BattleAxe(105, TextDatabase.SunkiumBattleAxe, TextDatabase.SunkiumBattleAxeDescription, 500, 600, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumBattleAxe"), new Entity(EntityDatabase.SunkiumBattleAxe), Resources.Load<GameObject>("Prefabs/Tools/Weapons/BattleAxe/SunkiumBattleAxe"));

    public static readonly Spear CopperSpear = new Spear(110, TextDatabase.CopperSpear, TextDatabase.CopperSpearDescription, 150, 120, Resources.Load<Texture2D>("Sprites/Items/Tools/CopperSpear"), new Entity(EntityDatabase.CopperSpear), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Spear/CopperSpear"));
    public static readonly Spear IronSpear = new Spear(111, TextDatabase.IronSpear, TextDatabase.IronSpearDescription, 250, 190, Resources.Load<Texture2D>("Sprites/Items/Tools/IronSpear"), new Entity(EntityDatabase.IronSpear), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Spear/IronSpear"));
    public static readonly Spear GoldSpear = new Spear(112, TextDatabase.GoldSpear, TextDatabase.GoldSpearDescription, 50, 120, Resources.Load<Texture2D>("Sprites/Items/Tools/GoldSpear"), new Entity(EntityDatabase.GoldSpear), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Spear/GoldSpear"));
    public static readonly Spear MithrilSpear = new Spear(113, TextDatabase.MithrilSpear, TextDatabase.MithrilSpearDescription, 200, 140, Resources.Load<Texture2D>("Sprites/Items/Tools/MithrilSpear"), new Entity(EntityDatabase.MithrilSpear), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Spear/MithrilSpear"));
    public static readonly Spear FloatiumSpear = new Spear(114, TextDatabase.FloatiumSpear, TextDatabase.FloatiumSpearDescription, 200, 190, Resources.Load<Texture2D>("Sprites/Items/Tools/FloatiumSpear"), new Entity(EntityDatabase.FloatiumSpear), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Spear/FloatiumSpear"));
    public static readonly Spear SunkiumSpear = new Spear(115, TextDatabase.SunkiumSpear, TextDatabase.SunkiumSpearDescription, 500, 240, Resources.Load<Texture2D>("Sprites/Items/Tools/SunkiumSpear"), new Entity(EntityDatabase.SunkiumSpear), Resources.Load<GameObject>("Prefabs/Tools/Weapons/Spear/SunkiumSpear"));

	public static readonly TopArmor LeatherTopArmor = new TopArmor(120, TextDatabase.LeatherTopArmor, TextDatabase.LeatherTopArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/LeatherTopArmor"), new Entity(EntityDatabase.LeatherTopArmor), 50, Resources.Load<Material>("Models/Items/Armors/Materials/LeatherArmor"), Resources.Load<Material>("Models/Items/Armors/Materials/IronA"));
	public static readonly TopArmor IronTopArmor = new TopArmor(121, TextDatabase.IronTopArmor, TextDatabase.IronTopArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/IronTopArmor"), new Entity(EntityDatabase.IronTopArmor), 100, Resources.Load<Material>("Models/Items/Armors/Materials/ArmorIron"), Resources.Load<Material>("Models/Items/Armors/Materials/ArmorIron"));
	public static readonly TopArmor MithrilTopArmor = new TopArmor(122, TextDatabase.MithrilTopArmor, TextDatabase.MithrilTopArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/MithrilTopArmor"), new Entity(EntityDatabase.MithrilTopArmor), 150, Resources.Load<Material>("Models/Items/Armors/Materials/Mithril"), Resources.Load<Material>("Models/Items/Armors/Materials/Floatium"));
	public static readonly TopArmor SunkiumTopArmor = new TopArmor(123, TextDatabase.SunkiumTopArmor, TextDatabase.SunkiumTopArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/SunkiumTopArmor"), new Entity(EntityDatabase.SunkiumTopArmor), 200, Resources.Load<Material>("Models/Items/Armors/Materials/SunkiumCenter"), Resources.Load<Material>("Models/Items/Armors/Materials/IronA"));

    public static readonly BottomArmor LeatherBottomArmor = new BottomArmor(124, TextDatabase.LeatherBottomArmor, TextDatabase.LeatherBottomArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/LeatherBottomArmor"), new Entity(EntityDatabase.LeatherBottomArmor), 50, Resources.Load<Material>("Models/Items/Armors/Materials/LeatherArmor"), Resources.Load<Material>("Models/Items/Armors/Materials/IronA"));
    public static readonly BottomArmor IronBottomArmor = new BottomArmor(125, TextDatabase.IronBottomArmor, TextDatabase.IronBottomArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/IronBottomArmor"), new Entity(EntityDatabase.IronBottomArmor), 100, Resources.Load<Material>("Models/Items/Armors/Materials/ArmorIron"), Resources.Load<Material>("Models/Items/Armors/Materials/ArmorIron"));
    public static readonly BottomArmor MithrilBottomArmor = new BottomArmor(126, TextDatabase.MithrilBottomArmor, TextDatabase.MithrilBottomArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/MithrilBottomArmor"), new Entity(EntityDatabase.MithrilBottomArmor), 150, Resources.Load<Material>("Models/Items/Armors/Materials/Mithril"), Resources.Load<Material>("Models/Items/Armors/Materials/Floatium"));
    public static readonly BottomArmor SunkiumBottomArmor = new BottomArmor(127, TextDatabase.SunkiumBottomArmor, TextDatabase.SunkiumBottomArmorDescription, Resources.Load<Texture2D>("Sprites/Items/Armors/SunkiumBottomArmor"), new Entity(EntityDatabase.SunkiumBottomArmor), 200, Resources.Load<Material>("Models/Items/Armors/Materials/SunkiumCenter"), Resources.Load<Material>("Models/Items/Armors/Materials/IronA"));


    public static readonly Consumable InstableCore = new Consumable(666, TextDatabase.Instable, TextDatabase.Instable, 1, Resources.Load<Texture2D>("Sprites/Items/Ores/InstableCore"), new Entity(EntityDatabase.InstableCore),new Effect(Effect.EffectType.Resistance,2),Resources.Load<GameObject>("Prefabs/Consumables/InstableCore"));

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

            yield return Cact;
            yield return Petal;
            yield return Stick;
            yield return Gigot;
            yield return Fang;
            yield return Hide;
            yield return WoodenPlank;
            yield return Glass;
            yield return CuttedStone;
            yield return Bowl;
            yield return Pumpkin;
            yield return Bone;
            yield return AnimalFat;

            yield return SlimeCore;
            yield return BoarCore;
            yield return PampiCore;

            foreach (TopArmor topArmor in TopArmors)
                yield return topArmor;

            foreach (BottomArmor bottomArmor in BottomArmors)
                yield return bottomArmor;

            foreach (Pickaxe pickaxe in Pickaxes)
                yield return pickaxe;

            foreach (Axe axe in Axes)
                yield return axe;

            foreach (Sword sword in Swords)
                yield return sword;

            foreach (Consumable cons in Consumables)
                yield return cons;

            foreach (WorkTop work in Worktops)
                yield return work;

            foreach (Spear spear in Spears)
                yield return spear;

            foreach (BattleAxe battleAxe in BattleAxes)
                yield return battleAxe;
        }
    }

    public static IEnumerable<TopArmor> TopArmors
    {
        get
        {
            yield return LeatherTopArmor;
            yield return IronTopArmor;
            yield return MithrilTopArmor;
            yield return SunkiumTopArmor;
        }
    }

    public static IEnumerable<BottomArmor> BottomArmors
    {
        get
        {
            yield return LeatherBottomArmor;
            yield return IronBottomArmor;
            yield return MithrilBottomArmor;
            yield return SunkiumBottomArmor;
        }
    }



    /// <summary>
    /// Liste tous les haches du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Pickaxe> Pickaxes
    {
        get
        {
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
    /// Liste tous les epee du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Sword> Swords
    {
        get
        {
            yield return StoneSword;
            yield return IronSword;
            yield return GoldSword;
            yield return MithrilSword;
            yield return CopperSword;
            yield return SunkiumSword;
            yield return FloatiumSword;
        }
    }
    /// <summary>
    /// Liste tous les epee du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Spear> Spears
    {
        get
        {
            yield return IronSpear;
            yield return GoldSpear;
            yield return MithrilSpear;
            yield return CopperSpear;
            yield return SunkiumSpear;
            yield return FloatiumSpear;
        }
    }
    /// <summary>
    /// Liste tous les epee du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<BattleAxe> BattleAxes
    {
        get
        {
            yield return IronBattleAxe;
            yield return GoldBattleAxe;
            yield return MithrilBattleAxe;
            yield return CopperBattleAxe;
            yield return SunkiumBattleAxe;
            yield return FloatiumBattleAxe;
        }
    }

    /// <summary>
    /// Liste tous les consomables du jeu. (Utilisez avec foreach)
    /// </summary>
    public static IEnumerable<Consumable> Consumables
    {
        get
        {
            yield return WaterPotion;
            yield return SpeedPotion;
            yield return PoisonPotion;
            yield return RegenerationPotion;
            yield return HealingPotion;
            yield return JumpPotion;
            yield return MeatBalls;
            yield return WaterCact;
            yield return Mushroom;
            yield return RedMushroom;
            yield return Soup;
            yield return Apple;
            yield return InstableCore;
            yield return SlimeGoo;
        }
    }

    public static IEnumerable<WorkTop> Worktops
    {
        get
        {
            yield return Cauldron;
            yield return Forge;
            yield return Firepit;
            yield return Torch;
            yield return Workbench;
            yield return Chest;
            yield return WolfTrap;
			yield return Piques;
			yield return WoodenWall;
			yield return StoneWall;
        }
    }
     
    /// <summary>
    /// Retourne l'item correspondant a l'identifiant. (La copie)
    /// </summary>
    public static Item Find(int id)
    {
        foreach (Item i in Items)
        {
            if (i.ID == id)
            {
				if (i is Pickaxe)
					return new Pickaxe ((Pickaxe)i);
				else if (i is Axe)
					return new Axe ((Axe)i);
				else if (i is Sword)
					return new Sword ((Sword)i);
				else if (i is Consumable)
					return new Consumable ((Consumable)i);
				else if (i is WorkTop)
					return new WorkTop ((WorkTop)i);
				else if (i is TopArmor)
					return new TopArmor ((TopArmor)i);
                else if (i is BottomArmor)
                    return new BottomArmor((BottomArmor)i);
                else
                    return new Item(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }

    /// <summary>
    /// Retourne l'item correspondant au nom anglais. (La copie)
    /// Ne fait pas attention a la casse.
    /// </summary>
    public static Item Find(string name)
    {
        foreach (Item i in Items)
        {
            if (i.NameText.GetText(SystemLanguage.English).ToLower().Replace(" ", "") == name.ToLower())
            {
                if (i is Pickaxe)
                    return new Pickaxe((Pickaxe)i);
                else if (i is Axe)
                    return new Axe((Axe)i);
                else if (i is Sword)
                    return new Sword((Sword)i);
                else if (i is Consumable)
                    return new Consumable((Consumable)i);
                else if (i is WorkTop)
                    return new WorkTop((WorkTop)i);
				else if (i is TopArmor)
					return new TopArmor ((TopArmor)i);
                else if (i is BottomArmor)
                    return new BottomArmor((BottomArmor)i);
                else
                    return new Item(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
