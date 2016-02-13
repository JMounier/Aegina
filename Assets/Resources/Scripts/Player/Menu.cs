using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour
{

    int posX, posY, width, height, spacing;
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
        this.NM = FindObjectOfType<NetworkManager>();
        this.posX = (int)(Screen.width / 2.6f);
        this.posY = (int)(Screen.height / 2.5f);
        this.width = Screen.width / 4;
        this.height = Screen.height / 30;
        this.spacing = this.height * 2;
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
        else if (this.langueShown)
            this.DrawLangue();
        else if (this.sonShown)
            this.DrawSon();
    }

    /// <summary>
    ///  Dessine l'interface du menu.
    /// </summary>
    private void DrawMenu()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
        
        if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Continue.GetText(), this.skin.GetStyle("button")))
        {
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
            this.menuShown = false;
            this.controller.Pause = !this.controller.Pause;
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Settings.GetText(), skin.GetStyle("button")))
        {
            this.menuShown = false;
            this.optionShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
        {
            this.inventory.SaveInventory();
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
            if (isServer)
                this.NM.StopHost();
            else
                this.NM.StopClient();
        }
    }


    /// <summary>
    ///  Dessine l'interface des options.
    /// </summary>
    private void DrawOption()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));

        if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Sound.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.sonShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Language.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.langueShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
        {
            this.menuShown = true;
            this.optionShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }                
    }

    /// <summary>
    ///  Dessine l'interface des options sonores.
    /// </summary>
    private void DrawSon()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
        
        this.soundAudio.Volume = GUI.HorizontalSlider(new Rect(this.posX, this.posY, this.width, this.height), this.soundAudio.Volume, 0f, 1f, this.skin.GetStyle("horizontalslider"), this.skin.GetStyle("horizontalsliderthumb"));

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.sonShown = false;
            PlayerPrefs.SetFloat("Sound_intensity", this.soundAudio.Volume);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.sonShown = false;
            this.soundAudio.Volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
    }

    /// <summary>
    ///  Dessine l'interface des options linguistiques.
    /// </summary>
    private void DrawLangue()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));

        if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.French.GetText(), this.skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 0);
            Text.SetLanguage(SystemLanguage.French);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.English.GetText(), this.skin.GetStyle("button")))
        {
            PlayerPrefs.SetInt("langue", 1);
            Text.SetLanguage(SystemLanguage.English);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.langueShown = false;
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
