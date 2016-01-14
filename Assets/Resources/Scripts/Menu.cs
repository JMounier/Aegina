using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour {

    public enum Language { French, English};

    private bool menuShown = false;
    private bool optionShown = false;
    private bool sonShown = false;
    private bool langueShown = false;
    private Inventory inventory;
    private GUISkin skin;
    private Language langue;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        this.inventory = GetComponentInParent<Inventory>();
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
        this.langue = (Language) PlayerPrefs.GetInt("langue", 0);
    }
	void OnGUI()
        {
        if (!isLocalPlayer)
            return;
        if (menuShown)
        {
            this.DrawMenu();
        }
        if (this.optionShown)
            this.DrawOption();
        if (this.langueShown)
            this.DrawLangue();
        if (this.sonShown)
            this.DrawSon();
        }
	
    private void DrawMenu()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "MENU", this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), this.langue == 0 ? "Continuer": "Resume" , this.skin.GetStyle("button")))
        {
            this.menuShown = false;
        }
        if (GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2 -40,80,40), this.langue == 0 ? "Quitter":"Quit", this.skin.GetStyle("button")))
        {
            this.inventory.SaveInventory();
            Application.Quit();
        }
        if (GUI.Button(new Rect(Screen.width/2 - 40,Screen.height/2 + 40, 80, 40), "Options", this.skin.GetStyle("button")))
        {
            this.menuShown = false;
            this.optionShown = true;
        }
    }
    private void DrawOption()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "OPTIONS", this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), this.langue == 0 ? "Retour" : "Back", this.skin.GetStyle("button")))
        {
            this.menuShown = true;
            this.optionShown = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), this.langue == 0 ? "Son":"Sound", this.skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.sonShown = true;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), this.langue == 0 ? "Langue": "Language", this.skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.langueShown = true;
        }
    }
    private void DrawSon()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), this.langue == 0 ? "SON" :"SOUND", this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), this.langue == 0 ? "Retour" : "Back", this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.sonShown = false;
        }
    }
    private void DrawLangue()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), this.langue == 0 ? "LANGUE":"LANGUAGE", this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), this.langue == 0 ? "Retour":"Back", this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.langueShown = false;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), this.langue == 0?"Français":"French", this.skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 0);
            this.langue = Language.French;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), this.langue == 0 ? "Anglais":"English", this.skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 1);
            this.langue = Language.English;
        }
    }

    // Getters & Setters
    public bool MenuShown
    {
        get { return this.menuShown; }
        set { this.menuShown = value; }
    }

    public bool OptionShown
    {
        get { return this.optionShown; }
        set { this.optionShown = value; }
    }

    public bool SonShown
    {
        get { return this.sonShown; }
        set { this.sonShown = value; }
    }

    public bool LangueShown
    {
        get { return this.langueShown; }
        set { this.langueShown = value; }
    }

    public Language Langue
    {
        get { return this.langue; }
    }
}
