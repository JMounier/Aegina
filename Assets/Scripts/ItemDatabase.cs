using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
    public List<Prefab> items = new List<Prefab>();
	void Start()
    {
        items.Add(new Prefab("Bois",0," un morceau de bois pouvant servir pour créer d'autres objets",64,1,1,1,Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Pierre", 1, " une pierre pouvant servir pour créer d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Pioche en bois", 2, "un ensemble de morceaux de bois ressemblant à une pioche",1, 2, 5, 1, Prefab.Item_Type.outils));
        items.Add(new Prefab("Pioche en pierre", 3, "un outil rudimentaire de pierre ressemblant à une pioche",1, 3, 10, 1, Prefab.Item_Type.outils));
        items.Add(new Prefab("Hache en bois", 4, "un ensemble de morceaux de bois ressemblant à une hache",1, 3, 1, 5, Prefab.Item_Type.outils));
        items.Add(new Prefab("Hache en pierre", 5, "un outil rudimentaire de pierre ressemblant à une hache",1, 4, 1, 10, Prefab.Item_Type.outils));
        items.Add(new Prefab("PC en Bois", 6, "de l'art peut-être",1, 1, 1, 1, Prefab.Item_Type.Consommable));
        items.Add(new Prefab("PC en pierre", 7, "comme le pc en bois mais en pierre",1, 1, 1, 1, Prefab.Item_Type.Consommable));
        items.Add(new Prefab("Minerai de cuivre", 8, "un minerai de cuivre pouvant être fondu en lingot",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Minerai de fer", 9, "un minerai de fer pouvant être fondu en lingot",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Minerai d'or", 10, "un minerai d'or pouvant être fondu en lingot",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Minerai de mytril", 11, "un minerai de mytril pouvant être fondu en lingot",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Minerai de floatium", 12, "un minerai de floatium pouvant être fondu en lingot",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Minerai de sunkium", 13, "un minerai de sunkium pouvant être fondu en lingot",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Lingot de cuivre", 14, "un lingot de cuivre pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Lingot de fer", 15, "un lingot de fer pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Lingot d'or", 16, "un lingot d'or pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Lingot de mytril", 17, "un lingot de mytril pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Lingot de floatium", 18, "un lingot de floatium pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Lingot de sunkium", 19, "un lingot de sunkium pouvant être utiliser pour construire d'autres objets",64, 1, 1, 1, Prefab.Item_Type.Ressource));
        items.Add(new Prefab("Sable", 20, "du sable ... Vous pouvez faire un chateau de sable avec...",64, 1, 1, 1, Prefab.Item_Type.Ressource));
    }
}
