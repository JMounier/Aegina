using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour
{


    private bool menuShown = false;
    private bool optionShown = false;
    private bool sonShown = false;
    private bool langueShown = false;
    private Inventory inventory;
    private GUISkin skin;
    private Controller controller;
    private NetworkManager NM;
    private Sound soundAudio;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        this.inventory = GetComponentInParent<Inventory>();
        this.controller = GetComponentInParent<Controller>();
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
        Text.SetLanguage(PlayerPrefs.GetInt("langue", 0) == 0 ? SystemLanguage.French : SystemLanguage.English);
        this.NM = FindObjectOfType<NetworkManager>();

        this.soundAudio = GetComponentInChildren<Sound>();
    }
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;
        if (menuShown)        
            this.DrawMenu();        
        else if (this.optionShown)
            this.DrawOption();
        else if(this.langueShown)
            this.DrawLangue();
        else if(this.sonShown)
            this.DrawSon();
    }

    /// <summary>
    ///  Dessine l'interface du menu.
    /// </summary>
    private void DrawMenu()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "MENU", this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), TextDatabase.Continuer.GetText(), this.skin.GetStyle("button")))
        {
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
            this.menuShown = false;
            this.controller.Pause = !this.controller.Pause;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), TextDatabase.Quit.GetText(), this.skin.GetStyle("button")))
        {
            this.inventory.SaveInventory();
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
            PlayerPrefs.SetFloat("Sound_intensity", soundAudio.Volume);
            this.NM.StopHost();

            // TO DO => StopServer / Save Map OR Deco
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), "Options", this.skin.GetStyle("button")))
        {
            this.menuShown = false;
            this.optionShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
    }

    /// <summary>
    ///  Dessine l'interface des options.
    /// </summary>
    private void DrawOption()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "OPTIONS", this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), TextDatabase.Retour.GetText(), this.skin.GetStyle("button")))
        {
            this.menuShown = true;
            this.optionShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), TextDatabase.Son.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.sonShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), TextDatabase.Langue.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.langueShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
    }

    /// <summary>
    ///  Dessine l'interface des options sonores.
    /// </summary>
    private void DrawSon()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), TextDatabase.SON.GetText(), this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), TextDatabase.Retour.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.sonShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        GUI.Box(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), "Volume",this.skin.GetStyle("chat"));
        soundAudio.Volume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 80, 80, 40), soundAudio.Volume, 0f, 1f,this.skin.GetStyle("horizontalslider"),this.skin.GetStyle("horizontalsliderthumb"));
    }

    /// <summary>
    ///  Dessine l'interface des options linguistiques.
    /// </summary>
    private void DrawLangue()
    {
        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), TextDatabase.LANGUE.GetText(), this.skin.GetStyle("windows"));
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), TextDatabase.Retour.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.langueShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), TextDatabase.Francais.GetText(), this.skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 0);
            Text.SetLanguage(SystemLanguage.French);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), TextDatabase.Anglais.GetText(), this.skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 1);
            Text.SetLanguage(SystemLanguage.English);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
    }

    // Getters & Setters

    /// <summary>
    ///  Si le menu est affiché.
    /// </summary>
    public bool MenuShown
    {
        get { return this.menuShown; }
        set { this.menuShown = value; }
    }

    /// <summary>
    ///  Si l'inteface options est affiché.
    /// </summary>
    public bool OptionShown
    {
        get { return this.optionShown; }
        set { this.optionShown = value; }
    }

    /// <summary>
    ///  Si l'inteface options sonores est affiché.
    /// </summary>
    public bool SonShown
    {
        get { return this.sonShown; }
        set { this.sonShown = value; }
    }

    /// <summary>
    ///  Si l'inteface options linguistique est affiché.
    /// </summary>
    public bool LangueShown
    {
        get { return this.langueShown; }
        set { this.langueShown = value; }
    }
    
}
