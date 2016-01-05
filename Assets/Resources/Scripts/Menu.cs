using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    public bool menuShown = false;
    public bool optionShown = false;
    private Inventory inventory;
    private GUISkin skin;

    // Use this for initialization
    void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
    }

    // Update is called once per frame
    void Update()
    { 
    }
	void OnGUI()
        {
        if (menuShown)
        {
            GUI.Box(new Rect(Screen.width /2-Screen.width/6, Screen.height / 2 -200,Screen.width/3,325), "MENU", skin.GetStyle("windows"));
            MenuShown();
        }
        if (optionShown)
            OptionShown();

        }
	
    void MenuShown()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), "Continuer",skin.GetStyle("button")))
        {
            menuShown = false;
        }
        if (GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2 -40,80,40), "Quitter",skin.GetStyle("button")))
        {
            inventory.SaveInventory();
            //quiter
        }
        if (GUI.Button(new Rect(Screen.width/2 - 40,Screen.height/2 + 40, 80, 40), "Options", skin.GetStyle("button")))
        {
            menuShown = false;
            optionShown = true;
        }
    }
    void OptionShown()
    {

    }
}
