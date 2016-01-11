using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour {
    private Inventory inventaire;
    private Menu menu;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            return;
        inventaire = GetComponentInParent<Inventory>();
        menu = GetComponentInParent<Menu>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        if (Input.GetButtonDown("Inventory") && !menu.menuShown && !menu.optionShown)
        {
            inventaire.InventoryShown = !inventaire.InventoryShown;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (inventaire.InventoryShown)
                inventaire.InventoryShown = false;
            else if (menu.optionShown)
            {
                menu.optionShown = false;
                menu.menuShown = true;
            }
            else if (menu.sonshown)
            {
                menu.sonshown = false;
                menu.optionShown = true;
            }
            else if (menu.langueshown)
            {
                menu.langueshown = false;
                menu.optionShown = true;
            }
            else
                menu.menuShown = !menu.menuShown;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventaire.Cursors = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventaire.Cursors = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventaire.Cursors = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventaire.Cursors = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventaire.Cursors = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventaire.Cursors = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventaire.Cursors = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            inventaire.Cursors = 7;
        }
        
    }
    void OnGUI()
    {
        if (!inventaire.Draggingitem && Event.current.button == 1 & Event.current.type == EventType.mouseDown)
            inventaire.UsingItem();
    }
}
