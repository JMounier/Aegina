using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    private Inventory inventaire;
    private Menu menu;

	// Use this for initialization
	void Start () {
        inventaire = GetComponentInParent<Inventory>();
        menu = GetComponentInParent<Menu>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Inventory") && !menu.menuShown && !menu.optionShown)
        {
            inventaire.inventoryShown = !inventaire.inventoryShown;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (inventaire.inventoryShown)
                inventaire.inventoryShown = false;
            else if (menu.optionShown)
            {
                menu.optionShown = false;
                menu.menuShown = true;
            }
            else
                menu.menuShown = !menu.menuShown;
        }
    }
}
