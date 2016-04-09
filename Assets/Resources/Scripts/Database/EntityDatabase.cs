using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EntityDatabase
{
    // Default
    public static readonly Entity Default = new Entity();

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

    public static readonly Entity StoneAxe = new Entity(21, 60, Resources.Load<GameObject>("Prefabs/Loots/StoneAxe"));
    public static readonly Entity CopperAxe = new Entity(22, 60, Resources.Load<GameObject>("Prefabs/Loots/CopperAxe"));
    public static readonly Entity IronAxe = new Entity(23, 60, Resources.Load<GameObject>("Prefabs/Loots/IronAxe"));
    public static readonly Entity GoldAxe = new Entity(24, 60, Resources.Load<GameObject>("Prefabs/Loots/GoldAxe"));
    public static readonly Entity MithrilAxe = new Entity(25, 60, Resources.Load<GameObject>("Prefabs/Loots/MithrilAxe"));
    public static readonly Entity FloatiumAxe = new Entity(26, 60, Resources.Load<GameObject>("Prefabs/Loots/FloatiumAxe"));
    public static readonly Entity SunkiumAxe = new Entity(27, 60, Resources.Load<GameObject>("Prefabs/Loots/SunkiumAxe"));

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

    public static readonly Entity Forge = new Entity(65, 60, Resources.Load<GameObject>("Prefabs/Loots/Forge"));
    public static readonly Entity Cauldron = new Entity(66, 60, Resources.Load<GameObject>("Prefabs/Loots/Cauldron"));
    public static readonly Entity Workbench = new Entity(67, 60, Resources.Load<GameObject>("Prefabs/Loots/Workbench"));
    public static readonly Entity Firepit = new Entity(68, 60, Resources.Load<GameObject>("Prefabs/Loots/Firepit"));

    public static readonly Entity AquaPotion = new Entity(69, 60, Resources.Load<GameObject>("Prefabs/Loots/AquaPotion"));
    public static readonly Entity BluePotion = new Entity(70, 60, Resources.Load<GameObject>("Prefabs/Loots/BluePotion"));
    public static readonly Entity GreenPotion = new Entity(71, 60, Resources.Load<GameObject>("Prefabs/Loots/GreenPotion"));
    public static readonly Entity PurplePotion = new Entity(72, 60, Resources.Load<GameObject>("Prefabs/Loots/PurplePotion"));
    public static readonly Entity RedPotion = new Entity(73, 60, Resources.Load<GameObject>("Prefabs/Loots/RedPotion"));
    public static readonly Entity YellowPotion = new Entity(74, 60, Resources.Load<GameObject>("Prefabs/Loots/YellowPotion"));

    public static readonly Entity WoodenPlank = new Entity(75, 60, Resources.Load<GameObject>("Prefabs/Loots/WoodenPlank"));
    public static readonly Entity Glass = new Entity(76, 60, Resources.Load<GameObject>("Prefabs/Loots/Glass"));
    public static readonly Entity Bowl = new Entity(77, 60, Resources.Load<GameObject>("Prefabs/Loots/Bowl"));
    public static readonly Entity CuttedStone = new Entity(78, 60, Resources.Load<GameObject>("Prefabs/Loots/CuttedStone"));

    // SmallElements
    public static readonly Element Branch = new Element(90, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/Branch"), Element.TypeElement.Small, 0);
    public static readonly Element ForestFlower = new Element(91, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/ForestFlower"), Element.TypeElement.Small, 0);
    public static readonly Element IceFlower = new Element(92, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/IceFlower"), Element.TypeElement.Small, 0);
    public static readonly Element SmallCactus = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/SmallCactus"), Element.TypeElement.Small, 0);
    public static readonly Element LittleRock = new Element(93, 100, Resources.Load<GameObject>("Prefabs/Elements/SmallElements/LittleRock"), Element.TypeElement.Small, 0, new DropConfig(ItemDatabase.Find(1), 1));

    // Tree
    public static readonly Element Fir = new Element(100, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Fir"), Element.TypeElement.Tree, 0, new DropConfig(ItemDatabase.Find(0), 1, 5));
    public static readonly Element SnowFir = new Element(101, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowFir"), Element.TypeElement.Tree, 0);
    public static readonly Element Cactus = new Element(102, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Cactus"), Element.TypeElement.Tree, 0);
    public static readonly Element Oak = new Element(103, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/Oak"), Element.TypeElement.Tree, 0);
    public static readonly Element SnowOak = new Element(104, 100, Resources.Load<GameObject>("Prefabs/Elements/Trees/SnowOak"), Element.TypeElement.Tree, 0);

    // Rock
    public static readonly Element StoneRock = new Element(110, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/Stone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(1), 4, 8));
    public static readonly Element CopperRock = new Element(111, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/CopperStone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(3), 1, 4), new DropConfig(ItemDatabase.Find(1), 2, 4));
    public static readonly Element IronRock = new Element(112, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/IronStone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(4), 1, 4), new DropConfig(ItemDatabase.Find(1), 2, 4));
    public static readonly Element GoldRock = new Element(113, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/GoldStone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(5), 1, 4), new DropConfig(ItemDatabase.Find(1), 2, 4));
    public static readonly Element MithrilRock = new Element(114, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/MithrilStone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(6), 1, 4), new DropConfig(ItemDatabase.Find(1), 2, 4));
    public static readonly Element FloatiumRock = new Element(115, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/FloatiumStone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(7), 1, 3), new DropConfig(ItemDatabase.Find(1), 2, 4));
    public static readonly Element SunkiumRock = new Element(116, 100, Resources.Load<GameObject>("Prefabs/Elements/Rocks/SunkiumStone"), Element.TypeElement.Rock, 0, new DropConfig(ItemDatabase.Find(8), 1, 3), new DropConfig(ItemDatabase.Find(1), 2, 4));

    // IslandCore
    public static readonly IslandCore IslandCore = new IslandCore(142, Resources.Load<GameObject>("Prefabs/Elements/Cristals/IslandCore"), Team.Neutre, 0, 0, 0);

    // Mobs
    public static readonly Mob Boar = new Mob(500, 100, Resources.Load<GameObject>("Prefabs/Mobs/Boar"), 5, 7, 10, 1f, 1.5f, 1f);

    // Chunk
    public static readonly Chunk Chunk1_One = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_One"), Bridges.One);
    public static readonly Chunk Chunk1_TwoI = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk1_TwoL = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk1_Three = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_Three"), Bridges.Three);
    public static readonly Chunk Chunk1_All = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk1_All"), Bridges.All);

    public static readonly Chunk Chunk2_One = new Chunk(1005, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_One"), Bridges.One);
    public static readonly Chunk Chunk2_TwoI = new Chunk(1006, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk2_TwoL = new Chunk(1007, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk2_Three = new Chunk(1008, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_Three"), Bridges.Three);
    public static readonly Chunk Chunk2_All = new Chunk(1009, Resources.Load<GameObject>("Prefabs/Chunks/Chunk2_All"), Bridges.All);

    public static readonly Chunk Chunk3_One = new Chunk(1010, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_One"), Bridges.One);
    public static readonly Chunk Chunk3_TwoI = new Chunk(1011, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk3_TwoL = new Chunk(1012, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk3_Three = new Chunk(1013, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_Three"), Bridges.Three);
    public static readonly Chunk Chunk3_All = new Chunk(1014, Resources.Load<GameObject>("Prefabs/Chunks/Chunk3_All"), Bridges.All);

    public static readonly Chunk Chunk4_One = new Chunk(1015, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_One"), Bridges.One);
    public static readonly Chunk Chunk4_TwoI = new Chunk(1016, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk4_TwoL = new Chunk(1017, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk4_Three = new Chunk(1018, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_Three"), Bridges.Three);
    public static readonly Chunk Chunk4_All = new Chunk(1019, Resources.Load<GameObject>("Prefabs/Chunks/Chunk4_All"), Bridges.All);

    public static readonly Chunk Chunk5_One = new Chunk(1020, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_One"), Bridges.One);
    public static readonly Chunk Chunk5_TwoI = new Chunk(1021, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_TwoI"), Bridges.TwoI);
    public static readonly Chunk Chunk5_TwoL = new Chunk(1022, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_TwoL"), Bridges.TwoL);
    public static readonly Chunk Chunk5_Three = new Chunk(1023, Resources.Load<GameObject>("Prefabs/Chunks/Chunk5_Three"), Bridges.Three);

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

            //Workbenches
            yield return Forge;
            yield return Cauldron;
            yield return Workbench;
            yield return Firepit;

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
            // Flowers
            yield return Branch;
            yield return ForestFlower;
            yield return IceFlower;
            yield return SmallCactus;
            yield return LittleRock;

            // Tree
            yield return Fir;
            yield return SnowFir;
            yield return Cactus;
            yield return Oak;
            yield return SnowOak;

            // Rocks
            yield return StoneRock;
            yield return CopperRock;
            yield return IronRock;
            yield return GoldRock;
            yield return MithrilRock;
            yield return FloatiumRock;
            yield return SunkiumRock;

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
            yield return Chunk4_All;

            yield return Chunk5_One;
            yield return Chunk5_TwoI;
            yield return Chunk5_TwoL;
            yield return Chunk5_Three;
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
                else if (i is Element)
                    return new Element((Element)i);
                else
                    return new Entity(i);
            }
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
