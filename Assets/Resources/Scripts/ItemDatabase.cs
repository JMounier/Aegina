using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ItemDatabase {
        public static readonly Item Bois = new Item("Bois",0,"Un morceau de bois pouvant servir pour créer d'autres objets",64,1,1,1,Item.ItemStack_Type.Ressource);
        public static readonly Item Pierre = new Item("Pierre", 1, "Une pierre pouvant servir pour créer d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item PiocheEnBois = new Item("Pioche en bois", 2, "Un ensemble de morceaux de bois ressemblant à une pioche",1, 2, 5, 1, Item.ItemStack_Type.Outils);
        public static readonly Item PiocheEnPierre= new Item("Pioche en pierre", 3, "Un outil rudimentaire de pierre ressemblant à une pioche",1, 3, 10, 1, Item.ItemStack_Type.Outils);
        public static readonly Item HacheEnBois= new Item("Hache en bois", 4, "Un ensemble de morceaux de bois ressemblant à une hache",1, 3, 1, 5, Item.ItemStack_Type.Outils);
        public static readonly Item HacheEnPierre= new Item("Hache en pierre", 5, "Un outil rudimentaire de pierre ressemblant à une hache",1, 4, 1, 10, Item.ItemStack_Type.Outils);
        public static readonly Item PCEnBois= new Item("PC en Bois", 6, "De l'art peut-être",1, 1, 1, 1, Item.ItemStack_Type.Consommable);
        public static readonly Item PCEnPierre= new Item("PC en pierre", 7, "Comme le pc en bois mais en pierre",1, 1, 1, 1, Item.ItemStack_Type.Consommable);
        public static readonly Item MineraiDeCuivre= new Item("Minerai de cuivre", 8, "Un minerai de cuivre pouvant être fondu en lingot",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item MineraiDeFer = new Item("Minerai de fer", 9, "Un minerai de fer pouvant être fondu en lingot",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item MineraiDOr = new Item("Minerai d'or", 10, "Un minerai d'or pouvant être fondu en lingot",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item MineraiMytril = new Item("Minerai de mytril", 11, "Un minerai de mytril pouvant être fondu en lingot",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item MineraiDeFloatium = new Item("Minerai de floatium", 12, "Un minerai de floatium pouvant être fondu en lingot",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item MineraiDeSunkium = new Item("Minerai de sunkium", 13, "Un minerai de sunkium pouvant être fondu en lingot",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item LingotDeCuivre= new Item("Lingot de cuivre", 14, "Un lingot de cuivre pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item LingotDeFer = new Item("Lingot de fer", 15, "Un lingot de fer pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item LingotDOr = new Item("Lingot d'or", 16, "Un lingot d'or pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item LingotDeMytril = new Item("Lingot de mytril", 17, "Un lingot de mytril pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item LingotDeFloatium = new Item("Lingot de floatium", 18, "Un lingot de floatium pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item LingotDeSunkium = new Item("Lingot de sunkium", 19, "Un lingot de sunkium pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
        public static readonly Item Sable = new Item("Sable", 20, "Du sable ... Vous pouvez faire un chateau de sable avec...",64, 1, 1, 1, Item.ItemStack_Type.Ressource);
    public static IEnumerable<Item> Items
    {
        get
        {
            yield return Bois;
            yield return Pierre;
            yield return PiocheEnBois;
            yield return PiocheEnPierre;
            yield return HacheEnBois;
            yield return HacheEnPierre;
            yield return PCEnBois;
            yield return PCEnPierre;
            yield return MineraiDeCuivre;
            yield return MineraiDeFer;
            yield return MineraiDeFloatium;
            yield return MineraiDeSunkium;
            yield return MineraiDOr;
            yield return MineraiMytril;
            yield return LingotDeCuivre;
            yield return LingotDeFer;
            yield return LingotDeFloatium;
            yield return LingotDeMytril;
            yield return LingotDeSunkium;
            yield return LingotDOr;
            yield return Sable;
        }
    }
    public static Item Find(int id)
    {
        foreach (Item item in Items)
        {
            if (item.id == id)
                return item;
        }
        throw new System.Exception("Items.Find : Item not find");
    }
    public static Item Find(string name)
    {
        foreach (Item item in Items)
        {
            if (item.Name == name)
                return item;
        }
        throw new System.Exception("Items.Find : Item not find");
    }
}
