using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Tutoriel : NetworkBehaviour
{
    private Controller controler;
    private Inventory inventaire;
    private Cristal_HUD cristal;
    private InputManager IM;
    [SyncVar]
    private int progress = 42;
    private Text textObjectif;
    private Text textNarator;
    private GUISkin skin;
    private bool end = false;
    private float cooldown = 10;
    private Menu menu;
    private Queue storydialog = new Queue();
    [SyncVar]
    private int fin = 0;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.controler = this.transform.GetComponent<Controller>();
        this.inventaire = this.transform.GetComponent<Inventory>();
        this.cristal = this.transform.GetComponent<Cristal_HUD>();
        this.menu = this.transform.GetComponent<Menu>();
        this.IM = this.transform.GetComponent<InputManager>();
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
        this.textNarator = new Text();
        this.textObjectif = new Text();

        this.CmdLoadProgress();

        this.skin.GetStyle("Objectifs").normal.background = skin.textArea.normal.background;
        this.skin.GetStyle("Objectifs").active.background = skin.textArea.normal.background;
        this.skin.GetStyle("Objectifs").onHover.background = skin.textArea.normal.background;
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        if (progress == 0)
        {
            if (!this.controler.Pause)
                this.controler.Pause = true;
            TutorialHUD();
        }

        else if (progress < 0)
            EndtutoHUD();

        else if (progress < 14 || cooldown > 0 || storydialog.Count != 0)
        {
            if (this.cooldown > 0 && !this.controler.Pause)
            {
                this.cooldown -= Time.deltaTime;
                NarratorHUD();
            }
            if (progress > -1)
                ObjectifHUD();
        }
        if (end)
            EndHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        this.skin.GetStyle("Objectifs").fontSize = (int)(Screen.height * 0.025f);
        this.skin.GetStyle("Narrateur").fontSize = (int)(Screen.height * 0.030f);
        // Choix des textes et progression du tutoriel
        if (progress > -1 && progress < 14)
            switch (progress)
            {
                case 1:
                    this.textObjectif = TextDatabase.MoveObjectif;
                    this.textNarator = TextDatabase.Move;
                    if (this.cooldown <= 0 && controler.Ismoving && !controler.IsJumping)
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 10;
                        //déclenchement du texte et du son
                    }
                    break;
                case 2:
                    this.textObjectif = TextDatabase.RunObjectif;
                    this.textNarator = TextDatabase.Run;
                    if (this.cooldown <= 0 && Input.GetButton("Sprint") && controler.Ismoving)
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 10;
                        //déclenchement du texte et du son
                    }
                    break;
                case 3:
                    this.textObjectif = TextDatabase.JumpObjectif;
                    this.textNarator = TextDatabase.Jump;
                    if (this.cooldown <= 0 && controler.IsJumping)
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 10;
                        //déclenchement du texte et du son
                    }
                    break;
                case 4:
                    this.textObjectif = TextDatabase.PickItemObjectif;
                    this.textNarator = TextDatabase.PickItem1;
                    if (this.cooldown <= 0)
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 20;
                        //déclenchement du texte et du son
                    }
                    break;
                case 5:
                    this.textNarator = TextDatabase.PickItem2;
                    if (this.cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.Stick))
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 10;
                        //déclenchement du texte et du son
                    }
                    else if (this.cooldown > 0 && this.cooldown <= 10)
                        this.textNarator = TextDatabase.PickItem3;
                    break;
                case 6:
                    this.textNarator = TextDatabase.Equip;
                    this.textObjectif = TextDatabase.EquipObjectif;
                    if (this.cooldown <= 0 && inventaire.UsedItem.Items.Name == ItemDatabase.Stick.Name)
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 10;
                        //déclenchement du texte et du son
                    }
                    break;
                case 7:
                    this.textNarator = TextDatabase.KillThePig;
                    this.textObjectif = TextDatabase.KillThePigObjectif;
                    if (this.cooldown > 0 && this.cooldown < 1)
                    {
                        this.cooldown = 0;
                        this.menu.Helpshown = true;
                        this.menu.Page = Text.GetLanguage() == SystemLanguage.English ? 0 : 3;
                        this.controler.Pause = true;
                    }
                    if (this.cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.Gigot))
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 15;
                        //déclenchement du texte et du son
                    }
                    break;
                case 8:
                    this.textNarator = TextDatabase.CraftABrochette;
                    this.textObjectif = TextDatabase.CraftABrochetteObejctif;
                    if (this.cooldown > 0 && this.cooldown < 1)
                    {
                        this.cooldown = 0;
                        this.menu.Helpshown = true;
                        this.controler.Pause = true;
                        this.menu.Page = 2 + (Text.GetLanguage() == SystemLanguage.English ? 0 : 3);
                    }
                    if (this.cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.MeatBalls))
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 10;
                        //déclenchement du texte et du son
                    }
                    break;
                case 9:
                    this.textNarator = TextDatabase.EatSomething;
                    this.textObjectif = TextDatabase.EatSomethingObjectif;
                    if (this.cooldown <= 0 && !this.inventaire.InventoryContains(ItemDatabase.MeatBalls) && Input.GetButton("Fire2")) /* récupérer la consomation d'un objet : attendre la fin de stats */
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 20;
                        //déclenchement du texte
                    }
                    break;
                case 10:
                    this.textObjectif = TextDatabase.DrinSomethingObjectif;
                    this.textNarator = TextDatabase.DrinkSomething1;
                    if (this.cooldown > 0 && this.cooldown <= 10)
                        this.textNarator = TextDatabase.DrinkSomething2;
                    if (this.cooldown <= 0 && this.inventaire.InventoryContains(ItemDatabase.WaterCact))
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 40;
                        //déclenchement du son
                    }
                    break;
                case 11:
                    this.textObjectif = new Text();
                    this.textNarator = TextDatabase.CinematiqueWhereIAm1;
                    if (this.cooldown <= 30)
                        this.textNarator = TextDatabase.CinematiqueWhereIAm2;
                    if (this.cooldown <= 20)
                    {
                        this.textNarator = TextDatabase.CinematiqueWhereIAm3;
                        if (this.cooldown <= 10)
                        {
                            this.textNarator = TextDatabase.CinematiqueWhereIAm4;
                            if (this.cooldown <= 0)
                            {
                                CmdSaveProgress(progress + 1);
                                this.cooldown = 10;
                                //déclenchement du texte et du son
                            }
                        }
                    }
                    break;
                case 12:
                    this.textNarator = TextDatabase.CristalView;
                    this.textObjectif = TextDatabase.CristalViewObjectif;
                    if (this.cooldown <= 0 && this.cristal.Cristal_shown)
                    {
                        CmdSaveProgress(progress + 1);
                        this.cooldown = 30;
                        //déclenchement du texte et du son
                    }
                    break;
                case 13:
                    this.textNarator = TextDatabase.FirstCristal1;
                    this.textObjectif = TextDatabase.FirstCrisatlObjectif;
                    if (this.cooldown > 0 && this.cooldown < 1)
                    {
                        this.cooldown = 0;
                        this.menu.Helpshown = true;
                        this.controler.Pause = true;
                        this.menu.Page = 1 + (Text.GetLanguage() == SystemLanguage.English ? 0 : 3);
                    }
                    if (this.cooldown > 10 && this.cooldown <= 20)
                        this.textNarator = TextDatabase.FirstCristal2;
                    else if (this.cooldown > 0)
                        this.textNarator = TextDatabase.FirstCristal3;
                    else if (this.cooldown <= 0 && this.cristal.Cristal_shown && this.cristal.Cristal.LevelTot > 0)
                    {
                        CmdSaveProgress(progress + 1);
                        CmdTutoEnding();
                        //declenchement de la seconde cynématique
                    }
                    break;
                default:
                    this.textNarator = new Text();
                    this.textObjectif = new Text();
                    break;
            }
        else if (storydialog.Count > 0)
            if (this.cooldown <= 0)
            {
                this.cooldown = 10;
                this.textNarator = (storydialog.Dequeue() as Text);
            }
    }

    private void NarratorHUD()
    {
        Rect rect = new Rect(Screen.width / 4, Screen.height - this.inventaire.ToolbarSize * 1.6f - Screen.height / 3, Screen.width / 2, Screen.height / 3);
        GUI.Box(rect, textNarator.GetText(), skin.GetStyle("Narrateur"));
    }

    private void ObjectifHUD()
    {
        int num = textObjectif.GetText().Length / 23;
        Rect rect = new Rect(5, 5, 2 * Screen.width / 9, (1 + num) * Screen.height / 35 + 8);
        if (this.textObjectif.GetText() != "")
            GUI.Box(rect, textObjectif.GetText(), skin.GetStyle("Objectifs"));
        rect.x = rect.width - Screen.width / 100;
        rect.y = rect.height - Screen.width / 100;
        rect.width = Screen.width / 50;
        rect.height = rect.width;
        if (this.textObjectif.GetText() != "" && GUI.Button(rect, Resources.Load<Texture2D>("Sprites/CraftsIcon/Invalid"), skin.GetStyle("quantity")))
        {
            this.IM.IAmDead();
            CmdSaveProgress(progress * -1);
        }
    }

    private void TutorialHUD()
    {
        Rect rect = new Rect(Screen.width / 3, Screen.height / 2 - Screen.height / 20, Screen.width / 3, Screen.height / 10 - 5);
        GUI.Box(rect, TextDatabase.Tutorial.GetText(), skin.GetStyle("inventory"));
        rect.y += Screen.height / 10;
        rect.width -= 4 * Screen.width / 15;
        rect.x += Screen.width / 15;
        if (GUI.Button(rect, TextDatabase.Yes.GetText(), skin.GetStyle("button")))
        {
            this.progress = 1;
            CmdSaveProgress(1);
            this.controler.Pause = false;
            this.cooldown = 10;
        }
        rect.x += 2 * Screen.width / 15;
        if (GUI.Button(rect, TextDatabase.No.GetText(), skin.GetStyle("button")))
        {
            this.progress = 14;
            CmdSaveProgress(14);
            this.controler.Pause = false;
            CmdTutoEnding();
        }

    }
    private void EndtutoHUD()
    {
        Rect rect = new Rect(Screen.width / 3, Screen.height / 2 - Screen.height / 20, Screen.width / 3, Screen.height / 10 - 5);
        GUI.Box(rect, TextDatabase.QuitTuto.GetText(), skin.GetStyle("inventory"));
        rect.y += Screen.height / 10;
        rect.width -= 4 * Screen.width / 15;
        rect.x += Screen.width / 15;
        if (GUI.Button(rect, TextDatabase.Yes.GetText(), skin.GetStyle("button")))
        {
            CmdSaveProgress(14);
            this.controler.Pause = false;
            this.textObjectif = new Text();
            CmdTutoEnding();
        }
        rect.x += 2 * Screen.width / 15;
        if (GUI.Button(rect, TextDatabase.No.GetText(), skin.GetStyle("button")))
        {
            CmdSaveProgress(progress * -1);
            this.controler.Pause = false;
        }
    }

    public void Story(params Text[] storytexts)
    {
        foreach (Text text in storytexts)
            storydialog.Enqueue(text);
    }

    private void EndHUD()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Resources.Load<Texture2D>("Sprites/SplashImages/BlackSreen"));
        Text ecrit = new Text(); //text a faire
        if (isServer && this.fin == 0)
        {
            Rect rect = new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 20);
            if (GUI.Button(rect, "Rentrer chez soi", skin.GetStyle("button"))) //text a faire
                this.fin = 1;
            rect.y += Screen.height / 10;
            if (GUI.Button(rect, "Detruire le cristal", skin.GetStyle("button"))) //text a faire
                this.fin = 2;
            rect.y += Screen.height / 10;
            if (Stats.Hunt() < 100 && GUI.Button(rect, "Epargner Gundam", skin.GetStyle("button"))) //text a faire
                this.fin = 3;
        }
        else
        {
            switch (fin)
            {
                case 1:
                    ecrit = TextDatabase.FinEgoïsme;
                    break;
                case 2:
                    ecrit = TextDatabase.FinPardon;
                    break;
                case 3:
                    ecrit = TextDatabase.FinSacrifice;
                    break;

                default:
                    //text a ajouter
                    break;
            }
        }
            GUI.Box(new Rect(Screen.width / 3, Screen.height / 3 + 3* Screen.height/10, Screen.width / 3, Screen.height / 3), "" + ecrit.GetText() + "");
    }

    public void JustDoIt()
    {
        storydialog.Clear();
        this.textNarator = new Text();
    }

    [Command]
    private void CmdTutoEnding()
    {
        Stats.SetTutoComplete();
    }

    [Command]
    private void CmdLoadProgress()
    {
        if (GameObject.Find("Map").GetComponent<Save>().IsCoop)
            this.progress = GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject).TutoProgress;
        else
            this.progress = 42;
    }

    [Command]
    private void CmdSaveProgress(int p)
    {
        this.progress = p;
    }

    public void END()
    {
        this.end = true;
        this.controler.Pause = true;
    }

    //Getters Setters
    public bool Finished_tuto
    {
        get { return progress > 13; }
    }
    public bool Tutoshown
    {
        get { return progress == 0; }
        set
        {
            if (!value)
            {
                this.progress = 14;
                CmdSaveProgress(14);
            }
        }
    }
    public bool EndTutoShown
    {
        get { return progress < 0; }
        set
        {
            if (value != progress < 0)
                CmdSaveProgress(progress * -1);
        }
    }
    public int Progress
    {
        get { return this.progress; }
    }

    public bool End
    {
        get { return end; }
    }
}
