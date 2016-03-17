using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Craft_HUD : MonoBehaviour {
    private Inventory inventory;
    private List<Craft> CraftDispo;
    private Craft.Type type;
    private bool fire, workbench, forge, brewer;
	// Use this for initialization
	void Start ()
    {
        this.inventory = gameObject.GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (inventory.InventoryShown)
        {
            Categoryze();
        }
	}
    /// <summary>
    /// Affiche l'interface de craft
    /// </summary>
    private void Categoryze()
    {
        sub_Categoryze(this.type);
    }
    /// <summary>
    /// affiche la liste des crafts disponibles du type selectionne
    /// </summary>
    /// <param name="type"></param>
    private void sub_Categoryze(Craft.Type type)
    {

    }
    /// <summary>
    /// affiche les elements necessaires
    /// </summary>
    /// <param name="craft"></param>
    private void Craft_used_HUD(Craft craft,int pos)
    {

    }
    /// <summary>
    /// recherche les atelier proche
    /// </summary>
    /// <returns></returns>
    private bool[] what_is_near()
    {
        return new bool[0];
    }
}
