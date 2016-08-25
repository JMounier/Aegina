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
    private bool controlShown = false;
    private bool helpageShown = false;
    private Inventory inventory;
    private GUISkin skin;
    private Controller controller;
    private NetworkManager NM;
    private Sound soundAudio;
    private Camera camera;
    private Texture2D[] HelpPage;
    private int page;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        this.inventory = GetComponentInParent<Inventory>();
        this.controller = GetComponentInParent<Controller>();
        this.controller.Sensitivity = PlayerPrefs.GetFloat("Sensitivity", 4f);
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
        this.skin.GetStyle("button").fontSize = (int)(Screen.height * 0.025f);
        this.NM = FindObjectOfType<NetworkManager>();
        this.posX = (int)(Screen.width / 2.6f);
        this.posY = (int)(Screen.height / 2.5f);
        this.width = Screen.width / 4;
        this.height = Screen.height / 30;
        this.spacing = this.height * 2;
        this.soundAudio = GetComponent<Sound>();
        this.soundAudio.Volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);
        this.camera = this.GetComponentInChildren<Camera>();
        this.camera.farClipPlane = PlayerPrefs.GetFloat("FieldOfView", 60f);
        this.page = -1;
        this.HelpPage = Resources.LoadAll<Texture2D>("Sprites/AideDeJeu");
    }

    void Update()
    {
        this.posX = (int)(Screen.width / 2.6f);
        this.posY = (int)(Screen.height / 2.5f);
        this.width = Screen.width / 4;
        this.height = Screen.height / 30;
    }

    void OnGUI()
    {
		if (!isLocalPlayer || !InputManager.seeGUI)
            return;
        if (menuShown)
            this.DrawMenu();
        else if (this.optionShown)
            this.DrawOption();
        else if (this.langueShown)
            this.DrawLangue();
        else if (this.sonShown)
            this.DrawSon();
        else if (this.controlShown)
            this.DrawControl();
        else if (this.helpageShown)
            this.DrawHelp();
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

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
        {
            this.menuShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
            if (isServer)
            {
                GameObject.Find("Map").GetComponent<Save>().SaveWorld();
                this.NM.StopHost();
            }
            else
                CmdDisconnect();
        }
        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width/2 - this.width/20, this.height), TextDatabase.Help.GetText(), skin.GetStyle("button")))
        {
            this.menuShown = false;
            this.helpageShown = true;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }

        if (GUI.Button(new Rect(this.posX + this.width/2 + this.width/20, this.posY + this.spacing * 2, this.width/2 - this.width/20, this.height), "Succes", skin.GetStyle("button")))
        {
            GetComponent<Success_HUD>().Activate = true;
            this.menuShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
    }

    [Command]
    public void CmdDisconnect()
    {
        GameObject.Find("Map").GetComponent<Save>().RemovePlayer(gameObject);
        RpcDisconnect();

    }

    [ClientRpc]
    private void RpcDisconnect()
    {
        if (isLocalPlayer)
            this.NM.StopClient();
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

        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
        {
            this.menuShown = true;
            this.optionShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), "Camera", skin.GetStyle("button")))
        {
            this.optionShown = false;
            this.controlShown = true;
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

    private void DrawControl()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("Aegina"));
        GUI.Box(new Rect(this.posX - this.spacing, this.posY - this.spacing, this.width + 2 * this.spacing, this.height + 8 * this.spacing), "", skin.GetStyle("Inventory"));
        GUI.Box(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Sensibility.GetText(), skin.GetStyle("Description"));
        this.controller.Sensitivity = GUI.HorizontalSlider(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), this.controller.Sensitivity, 0.1f, 10f, this.skin.GetStyle("horizontalslider"), this.skin.GetStyle("horizontalsliderthumb"));

        if (GUI.Button(new Rect(this.posX, this.posY + 4 * this.spacing, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.controlShown = false;
            PlayerPrefs.SetFloat("Sensitivity", this.controller.Sensitivity);
            PlayerPrefs.SetFloat("FieldOfView", this.camera.farClipPlane);
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
        GUI.Box(new Rect(this.posX, this.posY + 2 * this.spacing, this.width, this.height), TextDatabase.FieldOfView.GetText(), skin.GetStyle("Description"));
        this.camera.farClipPlane = GUI.HorizontalSlider(new Rect(this.posX, this.posY + 3 * this.spacing, this.width, this.height), this.camera.farClipPlane, 10f, 200f, this.skin.GetStyle("horizontalslider"), this.skin.GetStyle("horizontalsliderthumb"));
        if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 5, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
        {
            this.optionShown = true;
            this.controlShown = false;
            this.soundAudio.PlaySound(AudioClips.Button, 1f);
        }
    }
    private void DrawHelp()
    {
        if (page == -1)
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("Aegina"));
            if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.CombatHelp.GetText(), this.skin.GetStyle("button")))
                this.page = Text.GetLanguage() == SystemLanguage.English ? 0 : 3;
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 1, this.width, this.height), "Cristal", this.skin.GetStyle("button")))
                this.page = 1 + (Text.GetLanguage() == SystemLanguage.English ? 0 : 3);
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.InventaireHelp.GetText(), this.skin.GetStyle("button")))
                this.page = 2 + (Text.GetLanguage() == SystemLanguage.English ? 0 : 3);
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.helpageShown = false;
                this.soundAudio.PlaySound(AudioClips.Button, 1f);
            }
        }
        else
        {
            Rect rect = new Rect(Screen.width / 4, Screen.height / 7, Screen.width / 2, Screen.height / 2 + 2 * this.spacing);
            GUI.Box(rect, "", skin.GetStyle("inventory"));
            rect.x += Screen.width / 50;
            rect.y += Screen.height / 20;
            rect.width -= Screen.width / 25;
            rect.height -= Screen.height / 10;
            GUI.DrawTexture(rect, HelpPage[page]);
            rect = new Rect(Screen.width / 2 - 3 * Screen.width / 20, 3 * Screen.height / 4 + this.spacing / 2, Screen.width / 10, this.spacing);
            if (GUI.Button(rect, TextDatabase.Back.GetText(), skin.GetStyle("button")))
                this.page = -1;
            rect = new Rect(Screen.width / 2 + Screen.width / 20, 3 * Screen.height / 4 + this.spacing / 2, Screen.width / 10, this.spacing);
            if (GUI.Button(rect,TextDatabase.Resume.GetText(),skin.GetStyle("button")))
            {
                this.page = -1;
                this.helpageShown = false;
                this.soundAudio.PlaySound(AudioClips.Button, 1f);
                this.controller.Pause = false;
            }
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
    public bool ControlShown
    {
        get { return this.controlShown; }
        set { this.controlShown = value; }
    }

    public bool Helpshown
    {
        get { return this.helpageShown; }
        set { this.helpageShown = value; }
    }

    public int Page
    {
        get { return this.page; }
        set { this.page = value; }
    }
}
