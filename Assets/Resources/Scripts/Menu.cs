using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour {
    public bool menuShown = false;
    public bool optionShown = false;
    public bool sonshown = false;
    public bool langueshown = false;
    private Inventory inventory;
    private GUISkin skin;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        inventory = GetComponentInParent<Inventory>();
        skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
    }
	void OnGUI()
        {
        if (!isLocalPlayer)
            return;
        if (menuShown)
        {
            MenuShown();
        }
        if (optionShown)
            OptionShown();
        if (langueshown)
            LangueShown();
        if (sonshown)
            Sonshown();
        }
	
    void MenuShown()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "MENU", skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Continuer": "Resume" ,skin.GetStyle("button")))
        {
            menuShown = false;
        }
        if (GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2 -40,80,40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Quitter":"Quit",skin.GetStyle("button")))
        {
            inventory.SaveInventory();
            Application.Quit();
        }
        if (GUI.Button(new Rect(Screen.width/2 - 40,Screen.height/2 + 40, 80, 40), "Options", skin.GetStyle("button")))
        {
            menuShown = false;
            optionShown = true;
        }
    }
    void OptionShown()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "OPTIONS", skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Retour" : "back", skin.GetStyle("button")))
        {
            menuShown = true;
            optionShown = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Son":"Sound", skin.GetStyle("button")))
        {
            optionShown = false;
            sonshown = true;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Langue": "Language", skin.GetStyle("button")))
        {
            optionShown = false;
            langueshown = true;
        }
    }
    void Sonshown()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), PlayerPrefs.GetInt("langue", 1) == 1 ? "SON" :"SOUND", skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Retour" : "back", skin.GetStyle("button")))
        {
            optionShown = true;
            sonshown = false;
        }
    }
    void LangueShown()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), PlayerPrefs.GetInt("langue", 1) == 1 ? "LANGUE":"LANGUAGE", skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Retour":"back", skin.GetStyle("button")))
        {
            optionShown = true;
            langueshown = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40),PlayerPrefs.GetInt("langue",1) == 1?"français":"french", skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 1);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), PlayerPrefs.GetInt("langue", 1) == 1 ? "Anglais":"English", skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 2);
        }
    }
}
