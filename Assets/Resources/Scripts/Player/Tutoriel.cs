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
    private float cooldown = 10;
    private Menu menu;

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

        skin.GetStyle("Objectifs").normal.background = skin.textArea.normal.background;
        skin.GetStyle("Objectifs").active.background = skin.textArea.normal.background;
        skin.GetStyle("Objectifs").onHover.background = skin.textArea.normal.background;
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

        else if (progress < 14)
        {
            if (this.cooldown > 0 && !this.controler.Pause)
            {
                this.cooldown -= Time.deltaTime;
                NarratorHUD();
            }
            if (progress > -1)
                ObjectifHUD();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer || progress > 12)
            return;

        skin.GetStyle("Objectifs").fontSize = (int)(Screen.height * 0.025f);
        skin.GetStyle("Narrateur").fontSize = (int)(Screen.height * 0.030f);
        // Choix des textes et progression du tutoriel
        switch (progress)
        {
            case 1:
                textObjectif = TextDatabase.MoveObjectif;
                textNarator = TextDatabase.Move;
                if (this.cooldown <= 0 && controler.Ismoving && !controler.IsJumping)
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 10;
                    //déclenchement du texte et du son
                }
                break;
            case 2:
                textObjectif = TextDatabase.RunObjectif;
                textNarator = TextDatabase.Run;
                if (this.cooldown <= 0 && Input.GetButton("Sprint") && controler.Ismoving)
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 10;
                    //déclenchement du texte et du son
                }
                break;
            case 3:
                textObjectif = TextDatabase.JumpObjectif;
                textNarator = TextDatabase.Jump;
                if (this.cooldown <= 0 && controler.IsJumping)
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 10;
                    //déclenchement du texte et du son
                }
                break;
            case 4:
                textObjectif = TextDatabase.PickItemObjectif;
                textNarator = TextDatabase.PickItem1;
                if (this.cooldown <= 0)
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 20;
                    //déclenchement du texte et du son
                }
                break;
            case 5:
                textNarator = TextDatabase.PickItem2;
                if (this.cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.Stick))
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 10;
                    //déclenchement du texte et du son
                }
                else if (this.cooldown > 0 && this.cooldown <= 10)
                    textNarator = TextDatabase.PickItem3;
                break;
            case 6:
                textNarator = TextDatabase.Equip;
                textObjectif = TextDatabase.EquipObjectif;
                if (this.cooldown <= 0 && inventaire.UsedItem.Items.Name == ItemDatabase.Stick.Name)
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 10;
                    //déclenchement du texte et du son
                }
                break;
            case 7:
                textNarator = TextDatabase.KillThePig;
                textObjectif = TextDatabase.KillThePigObjectif;
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
                textNarator = TextDatabase.CraftABrochette;
                textObjectif = TextDatabase.CraftABrochetteObejctif;
                if (this.cooldown > 0 && this.cooldown < 1)
                {
                    this.cooldown = 0;
                    this.menu.Helpshown = true;
                    this.controler.Pause = true;
                    this.menu.Page = 2 +(Text.GetLanguage() == SystemLanguage.English ? 0 : 3);
                }
                if (this.cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.MeatBalls))
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 10;
                    //déclenchement du texte et du son
                }
                break;
            case 9:
                textNarator = TextDatabase.EatSomething;
                textObjectif = TextDatabase.EatSomethingObjectif;
                if (this.cooldown <= 0 && !this.inventaire.InventoryContains(ItemDatabase.MeatBalls) && Input.GetButton("Fire2")) /* récupérer la consomation d'un objet : attendre la fin de stats */
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 20;
                    //déclenchement du texte
                }
                break;
            case 10:
                textObjectif = TextDatabase.DrinSomethingObjectif;
                textNarator = TextDatabase.DrinkSomething1;
                if (this.cooldown > 0 && this.cooldown <= 10)
                    textNarator = TextDatabase.DrinkSomething2;
                if (this.cooldown <= 0 && this.inventaire.InventoryContains(ItemDatabase.WaterCact))
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 40;
                    //déclenchement du son
                }
                break;
            case 11:
                textObjectif = new Text();
                textNarator = TextDatabase.CinematiqueWhereIAm1;
                if (this.cooldown <= 30)
                    textNarator = TextDatabase.CinematiqueWhereIAm2;
                if (this.cooldown <= 20)
                {
                    textNarator = TextDatabase.CinematiqueWhereIAm3;
                    if (this.cooldown <= 10)
                    {
                        textNarator = TextDatabase.CinematiqueWhereIAm4;
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
                textNarator = TextDatabase.CristalView;
                textObjectif = TextDatabase.CristalViewObjectif;
                if (this.cooldown <= 0 && this.cristal.Cristal_shown)
                {
                    CmdSaveProgress(progress + 1);
                    this.cooldown = 30;
                    //déclenchement du texte et du son
                }
                break;
            case 13:
                textNarator = TextDatabase.FirstCristal1;
                textObjectif = TextDatabase.FirstCrisatlObjectif;
                if (this.cooldown > 0 && this.cooldown <1)
                {
                    this.cooldown = 0;
                    this.menu.Helpshown = true;
                    this.controler.Pause = true;
                    this.menu.Page = 1 + (Text.GetLanguage() == SystemLanguage.English ? 0 : 3);
                }
                if (this.cooldown > 10 && this.cooldown <= 20)
                    textNarator = TextDatabase.FirstCristal2;
                else if (this.cooldown > 0)
                    textNarator = TextDatabase.FirstCristal3;
                else if (this.cooldown <= 0 && this.cristal.Cristal_shown && this.cristal.Cristal.LevelTot > 0)
                {
                    CmdSaveProgress(progress + 1);
                    CmdTutoEnding();
                    //declenchement de la seconde cynématique
                }
                break;
            default:
                textNarator = new Text();
                textObjectif = new Text();
                break;

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
            this.progress = 13;
            CmdSaveProgress(13);
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
            CmdTutoEnding();
        }
        rect.x += 2 * Screen.width / 15;
        if (GUI.Button(rect, TextDatabase.No.GetText(), skin.GetStyle("button")))
        {
            CmdSaveProgress(progress * -1);
            this.controler.Pause = false;
        }
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
}
