
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class NetworkManagerHUD : MonoBehaviour
    {
        private const float TransitionDelay = .3f;
        private enum CategoryCloth { None, Body, Hair, Eyes, Beard, Pant, TShirt, Gloves, Hat };
        private MovieTexture loadingVideo;
        private Texture loadingImage;

        public enum TypeLaunch { Host, Client, Server, Stop };
        private NetworkManager manager;
        private GUISkin skin;
        private FirstScene firstScene;
        int posX, posY, width, height, spacing;
        private string playerName;
        private List<string> worldsList;
        private List<string> ipList = new List<string>();
        private int worldindex = 0;
        private int ipindex = 0;
        private string world = "";
        private string newWorldName = "World";
        private string seedstr = "";
        private string newipname = "Serveur";
        private bool typeCoop = true;

        private bool optionShown = false;
        private bool sonShown = false;
        private bool langueShown = false;
        private bool characterShown = false;
        private bool worldsShown = false;
        private bool listServShown = false;
        private bool worldcreateShown = false;
        private bool ipserveurshown = false;
        private float incr;
        private float incg;
        private float incb;

        private CategoryCloth categoryCloth;
        private int typeCloth;
        private Skin skinCharacter;
        private float smoothAparition;

        private GameObject character;

        #region Monobehaviour methods
        void Awake()
        {
            this.character = GameObject.Find("CharacterModel");
            this.loadingVideo = Resources.Load<MovieTexture>("Sprites/SplashImages/LoadingVideo");
            this.loadingImage = Resources.Load<Texture>("Sprites/SplashImages/LoadingImage");

            this.incr = Random.Range(-0.02f, 0.02f);
            this.incg = Random.Range(-0.02f, 0.02f);
            this.incb = Random.Range(-0.02f, 0.02f);
            this.manager = GetComponent<NetworkManager>();
            this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
            this.skin.GetStyle("chat").fontSize = (int)(Screen.height * 0.025f);
            this.skin.GetStyle("chat").alignment = TextAnchor.MiddleCenter;
            this.skin.GetStyle("chat").fontStyle = FontStyle.Bold;
            this.skin.textField.fontSize = (int)(Screen.height * 0.025f);
            this.skin.textField.fontStyle = FontStyle.Bold;

            this.skin.GetStyle("button").fontSize = (int)(Screen.height * 0.025f);
            this.posX = (int)(Screen.width / 2.6f);
            this.posY = (int)(Screen.height / 2.5f);
            this.width = Screen.width / 4;
            this.height = Screen.height / 30;
            this.spacing = this.height * 2;
            this.playerName = PlayerPrefs.GetString("PlayerName", "");
            SystemLanguage langue = PlayerPrefs.GetInt("langue", 1) == 1 ? SystemLanguage.English : SystemLanguage.French;
            this.categoryCloth = CategoryCloth.None;
            Text.SetLanguage(langue);
            this.firstScene = GameObject.Find("Map").GetComponent<FirstScene>();

            string skintStr = PlayerPrefs.GetString("Skin", "");
            try
            {
                this.skinCharacter = Skin.Load(skintStr);
                this.skinCharacter.Apply(this.character);
            }
            catch
            {
                this.characterShown = true;
                this.skinCharacter = new Skin(Clothing.NormalBrownBeard, Clothing.NormalBrownHair, Clothing.NoneHat, Clothing.BasicBody, Clothing.BrownPant, Clothing.NoneTshirt, Clothing.BrownGloves, Clothing.BlackEye);
            }

            if (!Directory.Exists(Application.dataPath + "/Saves"))
                Directory.CreateDirectory(Application.dataPath + "/Saves");
            this.skin.GetStyle("loading").normal.textColor = Color.black;
        }
        void Update()
        {
            this.skin.GetStyle("chat").fontSize = (int)(Screen.height * 0.025f);
            this.skin.textField.fontSize = (int)(Screen.height * 0.025f);
            this.skin.GetStyle("button").fontSize = (int)(Screen.height * 0.025f);
            this.posX = (int)(Screen.width / 2.6f);
            this.posY = (int)(Screen.height / 2.5f);
            this.width = Screen.width / 4;
            this.height = Screen.height / 25;
            this.spacing = this.height * 2;
            if (!this.manager.isNetworkActive)
                loadingVideo.Stop();
        }

        void Start()
        {
            this.loadingVideo.loop = true;
            this.firstScene.OnChar = this.characterShown;
            worldsList = new List<string>(Directory.GetDirectories(Application.dataPath + "/Saves"));
            for (int i = 0; i < worldsList.Count; i++)
                worldsList[i] = worldsList[i].Remove(0, Application.dataPath.Length + 7);

            int j = 0;
            while (PlayerPrefs.GetString("ip" + j.ToString(), "") != "")
            {
                ipList.Add(PlayerPrefs.GetString("ip" + j.ToString()));
                j++;
            }
        }

        void OnGUI()
        {
            if (!optionShown && !sonShown && !langueShown && !characterShown && !worldcreateShown && !ipserveurshown)
            {
                if (!this.manager.isNetworkActive)
                {
                    // Verification of Sound when we come back to menu
                    GameObject map = GameObject.Find("Map");
                    if (this.firstScene == null && map != null)
                    {
                        this.character = GameObject.Find("CharacterModel");
                        this.skinCharacter.ForceApply(this.character);
                        this.firstScene = map.GetComponent<FirstScene>();
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }

                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Play.GetText(), this.skin.GetStyle("button")))
                    {
                        this.worldsShown = !this.worldsShown;
                        this.listServShown = false;
                        this.firstScene.PlayButtonSound();
                    }

                    if (GUI.Button(new Rect(this.posX, this.posY + (worldsShown ? Mathf.Min(4, worldsList.Count + 1) * this.spacing : 0) + this.spacing, this.width, this.height), TextDatabase.Join.GetText(), this.skin.GetStyle("button")))
                    {
                        this.listServShown = !this.listServShown;
                        this.worldsShown = false;
                        this.firstScene.PlayButtonSound();
                    }

                    if (GUI.Button(new Rect(this.posX, this.posY + (worldsShown ? Mathf.Min(4, worldsList.Count + 1) * this.spacing : listServShown ? Mathf.Min(4, ipList.Count + 1) * this.spacing : 0) + this.spacing * 2, this.width, this.height), TextDatabase.Settings.GetText(), skin.GetStyle("button")))
                    {
                        this.optionShown = true;
                        this.listServShown = false;
                        this.worldsShown = false;
                        this.firstScene.PlayButtonSound();
                    }
                    if (GUI.Button(new Rect(this.posX, this.posY + (worldsShown ? Mathf.Min(4, worldsList.Count + 1) * this.spacing : listServShown ? Mathf.Min(4, ipList.Count + 1) * this.spacing : 0) + this.spacing * 3, this.width, this.height), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
                    {
                        this.firstScene.PlayButtonSound();
                        Application.Quit();
                    }
                    if (this.worldsShown)
                        this.DrawWorlds();
                    else if (this.listServShown)
                        this.DrawIplist();
                }

                else if (!this.manager.IsClientConnected())
                {
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.loadingVideo);
                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, Screen.height / 1.1f, this.width, this.height), TextDatabase.Cancel.GetText(), this.skin.GetStyle("button")))
                    {
                        this.firstScene.PlayButtonSound();
                        this.Launch(TypeLaunch.Stop);
                        this.manager.networkAddress = "localhost";
                    }
                }
                else if (!this.manager.clientLoadedScene)
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.loadingImage);
            }
            else
            {
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));

                if (this.optionShown)
                    this.DrawOption();
                else if (this.langueShown)
                    this.DrawLangue();
                else if (this.sonShown)
                    this.DrawSon();
                else if (this.characterShown)
                    this.DrawCharacter();
                else if (this.worldcreateShown)
                    this.DrawWorldCreate();
                else if (ipserveurshown)
                    this.DrawServeurCreate();
            }
        }
        #endregion

        private string RemoveSpecialCharacter(string str, string authorized, bool casseSensitive = true)
        {
            string newstr = "";
            foreach (char c in str)
            {
                foreach (char c2 in authorized)
                    if (c == c2 || (!casseSensitive && c.ToString().ToLower() == c2.ToString().ToLower()))
                    {
                        newstr += c;
                        break;
                    }
            }
            return newstr;
        }

        public void Launch(TypeLaunch type)
        {
            switch (type)
            {
                case TypeLaunch.Host:
                    this.manager.StartHost();
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.loadingImage);
                    break;
                case TypeLaunch.Client:
                    this.manager.StartClient();
                    loadingVideo.Play();
                    break;
                case TypeLaunch.Server:
                    this.manager.OnStartServer();
                    break;
                default:
                    this.manager.StopHost();
                    break;
            }
        }

        #region Options
        /// <summary>
        ///  Dessine l'interface des options.
        /// </summary>
        private void DrawOption()
        {
            if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Sound.GetText(), this.skin.GetStyle("button")))
            {
                this.sonShown = true;
                this.optionShown = false;
                this.firstScene.PlayButtonSound();
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Language.GetText(), this.skin.GetStyle("button")))
            {
                this.langueShown = true;
                this.optionShown = false;
                this.firstScene.PlayButtonSound();
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Character.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = true;
                this.optionShown = false;
                this.firstScene.OnChar = true;
                this.firstScene.PlayButtonSound();
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.firstScene.PlayButtonSound();
            }
        }

        /// <summary>
        ///  Dessine l'interface des options sonores.
        /// </summary>
        private void DrawSon()
        {
            this.firstScene.Volume = GUI.HorizontalSlider(new Rect(this.posX, this.posY, this.width, this.height), this.firstScene.Volume, 0f, 1f, this.skin.GetStyle("horizontalslider"), this.skin.GetStyle("horizontalsliderthumb"));

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.sonShown = false;
                PlayerPrefs.SetFloat("Sound_intensity", this.firstScene.Volume);
                this.firstScene.PlayButtonSound();
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.sonShown = false;
                this.firstScene.Volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);
                this.firstScene.PlayButtonSound();
            }
        }

        /// <summary>
        ///  Dessine l'interface des options du personnage.
        /// </summary>
        private void DrawCharacter()
        {
            this.smoothAparition += Time.deltaTime;
            this.skin.textField.alignment = TextAnchor.MiddleCenter;
            this.playerName = GUI.TextField(new Rect(this.posX, this.posY + this.spacing * 6.2f, this.width, this.height), this.RemoveSpecialCharacter(this.playerName, "abcdefghijklmnopqrstuvwxyz123456789-_", false), 15, this.skin.textField);
            if (this.playerName != "" && GUI.Button(new Rect(this.posX, this.posY + this.spacing * 7f, this.width / 2.1f, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = false;
                this.firstScene.OnChar = false;
                this.optionShown = true;
                PlayerPrefs.SetString("PlayerName", this.playerName);
                PlayerPrefs.SetString("Skin", Skin.Save(this.skinCharacter));

                this.firstScene.PlayButtonSound();
            }
            if (GUI.Button(new Rect(this.posX + this.width / 1.9f, this.posY + this.spacing * 7, this.width / 2.1f, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = false;
                this.firstScene.OnChar = false;
                this.optionShown = true;
                this.playerName = PlayerPrefs.GetString("PlayerName", "");
                this.firstScene.PlayButtonSound();
            }

            #region Category
            if (GUI.Button(new Rect(this.posX, this.posY - this.spacing * 2, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/HatIcon")))
            {
                this.categoryCloth = CategoryCloth.Hat;
                this.smoothAparition = 0;
            }
            if (GUI.Button(new Rect(this.posX, this.posY, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/BeardIcon")))
            {
                this.smoothAparition = 0;
                this.categoryCloth = CategoryCloth.Beard;
            }
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/BodyIcon")))
            {
                this.smoothAparition = 0;
                this.categoryCloth = CategoryCloth.Body;
            }
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 4f, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/EyeIcon")))
            {
                this.smoothAparition = 0;
                this.categoryCloth = CategoryCloth.Eyes;
            }
            if (GUI.Button(new Rect(this.posX + this.spacing * 5, this.posY - this.spacing * 2, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/HairIcon")))
            {
                this.smoothAparition = 0;
                this.categoryCloth = CategoryCloth.Hair;
            }
            if (GUI.Button(new Rect(this.posX + this.spacing * 5, this.posY + this.spacing * 2, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/GlovesIcon")))
            {
                this.smoothAparition = 0;
                this.categoryCloth = CategoryCloth.Gloves;
            }
            if (GUI.Button(new Rect(this.posX + this.spacing * 5, this.posY + this.spacing * 4f, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/TshirtIcon")))
            {
                this.smoothAparition = 0;
                this.categoryCloth = CategoryCloth.TShirt;
            }
            if (GUI.Button(new Rect(this.posX + this.spacing * 2.5f, this.posY + this.spacing * 4.5f, this.width / 5, this.width / 5), Resources.Load<Texture2D>("Sprites/Cosmetics/PantIcon")))
            {
                this.categoryCloth = CategoryCloth.Pant;
                this.smoothAparition = 0;
            }
            #endregion

            Text tooltip = null;
            switch (this.categoryCloth)
            {
                #region Hat
                case (CategoryCloth.Hat):
                    int y = 0;
                    int x = 0;
                    Rect rect = new Rect(Screen.width / 25f, -(10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/NoneIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hat.TypeHat.None;
                    }
                    rect.x += (10 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/HatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hat.TypeHat.TopHat;
                    }
                    rect.x += (10 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/StrawHatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hat.TypeHat.StrawHat;
                    }
                    rect.x += (10 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/CowBoyIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hat.TypeHat.Cowboy;
                    }

                    foreach (Hat h in Clothing.Hats)
                        if (this.typeCloth == (int)h.GetTypeHat)
                        {
                            x = this.typeCloth;
                            Texture2D fill = new Texture2D(this.width / 3, this.width / 3);
                            Color fillcolor = h.Color;
                            fillcolor.a = Mathf.Clamp01(this.smoothAparition - y * TransitionDelay);
                            for (int i = 0; i < this.width / 3; i++)
                                for (int j = 0; j < this.width / 3; j++)
                                    fill.SetPixel(i, j, fillcolor);
                            fill.Apply();
                            rect = new Rect((10 + Screen.width / 20) * x + Screen.width / 25f, y * (10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                            if (rect.Contains(Event.current.mousePosition))
                                tooltip = h.Description;
                            if (this.smoothAparition > y * TransitionDelay && GUI.Button(rect, fill))
                            {
                                this.skinCharacter.Hat = h;
                                this.skinCharacter.Apply(this.character);
                            }
                            y += 1;
                        }
                    break;
                #endregion
                #region Beard
                case (CategoryCloth.Beard):
                    y = 0;
                    x = 0;
                    rect = new Rect(Screen.width / 25f, -(10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/NoneIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Beard.TypeBeard.None;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/HatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Beard.TypeBeard.Beard;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/StrawHatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Beard.TypeBeard.BeardOnly;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/StrawHatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Beard.TypeBeard.BeardMoustachSplit;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/CowBoyIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Beard.TypeBeard.Moustach;
                    }

                    foreach (Beard b in Clothing.Beards)
                        if (this.typeCloth == (int)b.GetTypeBeard)
                        {
                            x = this.typeCloth;
                            Texture2D fill = new Texture2D(this.width / 3, this.width / 3);
                            Color fillcolor = b.Color;
                            fillcolor.a = Mathf.Clamp01(this.smoothAparition - y * TransitionDelay);
                            for (int i = 0; i < this.width / 3; i++)
                                for (int j = 0; j < this.width / 3; j++)
                                    fill.SetPixel(i, j, fillcolor);
                            fill.Apply();
                            rect = new Rect((5 + Screen.width / 20) * x + Screen.width / 25f, y * (10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                            if (rect.Contains(Event.current.mousePosition))
                                tooltip = b.Description;
                            if (this.smoothAparition > y * TransitionDelay && GUI.Button(rect, fill))
                            {
                                this.skinCharacter.Beard = b;
                                this.skinCharacter.Apply(this.character);
                            }
                            y = (y + 1) % 5;
                            if (y == 0)
                                x++;
                        }
                    break;
                #endregion
                #region Body
                case (CategoryCloth.Body):
                    y = 0;
                    x = 0;
                    foreach (Body b in Clothing.Bodies)
                    {
                        Texture2D fill = new Texture2D(this.width / 3, this.width / 3);
                        Color fillcolor = b.Color;
                        fillcolor.a = Mathf.Clamp01(this.smoothAparition - (x + y * 3) * TransitionDelay);
                        for (int i = 0; i < this.width / 3; i++)
                            for (int j = 0; j < this.width / 3; j++)
                                fill.SetPixel(i, j, fillcolor);
                        fill.Apply();
                        rect = new Rect((10 + Screen.width / 20) * x + Screen.width / 25f, y * (10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                        if (rect.Contains(Event.current.mousePosition))
                            tooltip = b.Description;
                        if (this.smoothAparition > (x + y * 3) * TransitionDelay && GUI.Button(rect, fill))
                        {
                            this.skinCharacter.Body = b;
                            this.skinCharacter.Apply(this.character);
                        }
                        x = (x + 1) % 3;
                        if (x == 0)
                            y++;
                    }
                    break;
                #endregion
                #region Hair
                case (CategoryCloth.Hair):
                    y = 0;
                    x = 0;
                    rect = new Rect(Screen.width / 25f, -(10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/NoneIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hair.TypeHair.None;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/HatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hair.TypeHair.Normal;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/StrawHatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hair.TypeHair.Crete;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/StrawHatIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hair.TypeHair.LongHair;
                    }
                    rect.x += (5 + Screen.width / 20);

                    if (GUI.Button(rect, Resources.Load<Texture2D>("Sprites/Cosmetics/CowBoyIcon")))
                    {
                        this.smoothAparition = 0;
                        typeCloth = (int)Hair.TypeHair.Meche;
                    }

                    foreach (Hair h in Clothing.Hairs)
                    {
                        if (this.typeCloth == (int)h.GetTypeHair)
                        {
                            x = this.typeCloth;
                            Texture2D fill = new Texture2D(this.width / 3, this.width / 3);
                            Color fillcolor = h.Color;
                            fillcolor.a = Mathf.Clamp01(this.smoothAparition - y * TransitionDelay);
                            for (int i = 0; i < this.width / 3; i++)
                                for (int j = 0; j < this.width / 3; j++)
                                    fill.SetPixel(i, j, fillcolor);
                            fill.Apply();
                            rect = new Rect((5 + Screen.width / 20) * x + Screen.width / 25f, y * (10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                            if (rect.Contains(Event.current.mousePosition))
                                tooltip = h.Description;
                            if (this.smoothAparition > y * TransitionDelay && GUI.Button(rect, fill))
                            {
                                this.skinCharacter.Hair = h;
                                this.skinCharacter.Apply(this.character);
                            }
                            y = (y + 1) % 5;
                            if (y == 0)
                                x++;
                        }
                    }
                    break;
                #endregion
                #region Gloves
                case (CategoryCloth.Gloves):
                    y = 0;
                    x = 0;
                    foreach (Gloves g in Clothing.Gloves)
                    {
                        Texture2D fill = new Texture2D(this.width / 3, this.width / 3);
                        Color fillcolor = g.Color;
                        fillcolor.a = Mathf.Clamp01(this.smoothAparition - (x + y * 3) * TransitionDelay);
                        for (int i = 0; i < this.width / 3; i++)
                            for (int j = 0; j < this.width / 3; j++)
                                fill.SetPixel(i, j, fillcolor);
                        fill.Apply();
                        rect = new Rect((10 + Screen.width / 20) * x + Screen.width / 25f, y * (10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                        if (rect.Contains(Event.current.mousePosition))
                            tooltip = g.Description;
                        if (this.smoothAparition > (x + y * 3) * TransitionDelay && GUI.Button(rect, fill))
                        {
                            this.skinCharacter.Gloves = g;
                            this.skinCharacter.Apply(this.character);
                        }
                        x = (x + 1) % 3;
                        if (x == 0)
                            y++;
                    }
                    break;
                #endregion
                #region Eyes
                case (CategoryCloth.Eyes):
                    y = 0;
                    x = 0;
                    foreach (Eyes e in Clothing.Eyes)
                    {
                        Texture2D fill = new Texture2D(this.width / 3, this.width / 3);
                        Color fillcolor = e.Color;
                        fillcolor.a = Mathf.Clamp01(this.smoothAparition - (x + y * 3) * TransitionDelay);
                        for (int i = 0; i < this.width / 3; i++)
                            for (int j = 0; j < this.width / 3; j++)
                                fill.SetPixel(i, j, fillcolor);
                        fill.Apply();
                        rect = new Rect((10 + Screen.width / 20) * x + Screen.width / 25f, y * (10 + Screen.height / 10) + Screen.height / 2.25f, Screen.height / 11, Screen.height / 11);
                        if (rect.Contains(Event.current.mousePosition))
                            tooltip = e.Description;
                        if (this.smoothAparition > (x + y * 3) * TransitionDelay && GUI.Button(rect, fill))
                        {
                            this.skinCharacter.Eyes = e;
                            this.skinCharacter.Apply(this.character);
                        }
                        x = (x + 1) % 3;
                        if (x == 0)
                            y++;
                    }
                    break;
                #endregion
                #region Pant
                case (CategoryCloth.Pant):
                    break;
                #endregion
                #region TShirt
                case (CategoryCloth.TShirt):
                    break;
                #endregion             
                default:
                    break;
            }

            if (tooltip != null)
                GUI.Box(new Rect(Event.current.mousePosition.x - Screen.width / 20, Event.current.mousePosition.y + Screen.height / 20, 100, 35 + 20 * (tooltip.GetText().Length / 35 + 1)),
                               tooltip.GetText(), this.skin.GetStyle("Skin"));
        }
        /// <summary>
        ///  Dessine l'interface des options linguistiques.
        /// </summary>
        private void DrawLangue()
        {
            if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.French.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 0);
                Text.SetLanguage(SystemLanguage.French);
                this.firstScene.PlayButtonSound();
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.English.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 1);
                Text.SetLanguage(SystemLanguage.English);
                this.firstScene.PlayButtonSound();
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.langueShown = false;
                this.firstScene.PlayButtonSound();
            }
        }
        #endregion

        #region Worlds / Servers
        /// <summary>
        /// Dessine l'interface de la liste des mondes
        /// </summary>
        private void DrawWorlds()
        {
            if (worldsList.Count != 0)
            {
                int i;
                for (i = 0; i < Mathf.Min(worldsList.Count, 3); i++)
                {
                    Rect rect = new Rect(this.posX, this.posY + (1 + i) * spacing - (3 - i) * 6, this.width, this.height * 2 + 5);
                    if (GUI.Button(rect, worldsList[(i + worldindex) % worldsList.Count], world == worldsList[(i + worldindex) % worldsList.Count] ? skin.GetStyle("second_button_actif") : skin.GetStyle("second_button")))
                    {
                        world = worldsList[(i + worldindex) % worldsList.Count];
                        this.firstScene.PlayButtonSound();
                    }
                }
                Rect rect1 = new Rect(this.posX, this.posY + (1 + i) * spacing, this.width / 3 - 5, this.height);
                if (GUI.Button(rect1, TextDatabase.Start.GetText(), skin.GetStyle("button")))
                {
                    if (world != "")
                    {
                        worldsShown = false;
                        Launch(TypeLaunch.Host);
                    }
                    this.firstScene.PlayButtonSound();
                }
                rect1 = new Rect(this.posX + this.width * .33f + 5, this.posY + (1 + i) * spacing, this.width / 3 - 5, this.height);
                if (GUI.Button(rect1, TextDatabase.Create.GetText(), skin.GetStyle("button")))
                {
                    worldsShown = false;
                    worldcreateShown = true;
                    this.firstScene.PlayButtonSound();
                }
                rect1 = new Rect(this.posX + this.width * .66f + 10, this.posY + (1 + i) * spacing, this.width / 3 - 5, this.height);
                if (GUI.Button(rect1, TextDatabase.Delete.GetText(), skin.GetStyle("button")))
                {
                    worldsList.Remove(world);
                    foreach (string file in Directory.GetFiles(Application.dataPath + "/saves/" + world + "/Chunks"))
                    {
                        File.Delete(file);
                    }
                    foreach (string file in Directory.GetFiles(Application.dataPath + "/saves/" + world + "/Players"))
                    {
                        File.Delete(file);
                    }
                    foreach (string file in Directory.GetFiles(Application.dataPath + "/saves/" + world))
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(Application.dataPath + "/saves/" + world + "/Chunks");
                    Directory.Delete(Application.dataPath + "/saves/" + world + "/Players");
                    Directory.Delete(Application.dataPath + "/Saves/" + world);
                    if (worldsList.Count == 0)
                        worldsShown = false;
                }
            }
            else
            {
                worldsShown = false;
                worldcreateShown = true;
            }
            if (worldsList.Count > 3)
            {
                Rect rectarrow = new Rect(this.posX + this.width + 10, this.posY + spacing, this.spacing / 2, this.spacing / 2);
                if (GUI.Button(rectarrow, "/\\", skin.GetStyle("button")))
                {
                    worldindex -= 1;
                    if (worldindex < 0)
                        worldindex += worldsList.Count;
                    this.firstScene.PlayButtonSound();
                }
                rectarrow = new Rect(this.posX + this.width + 10, this.posY + (Mathf.Min(3, worldsList.Count)) * spacing, this.spacing / 2, this.spacing / 2);
                if (GUI.Button(rectarrow, "\\/", skin.GetStyle("button")))
                {
                    worldindex = (worldindex + 1) % worldsList.Count;
                    this.firstScene.PlayButtonSound();
                }
            }
        }
        /// <summary>
        /// Dessine l'interface de la liste des ips
        /// </summary>
        private void DrawIplist()
        {
            if (ipList.Count != 0)
            {
                int i = 0;
                for (i = 0; i < Mathf.Min(3, ipList.Count); i++)
                {
                    Rect rect = new Rect(this.posX, this.posY + (2 + i) * spacing - (3 - i) * 6, this.width, this.height * 2 + 5);
                    if (GUI.Button(rect, PlayerPrefs.GetString(ipList[(i + ipindex) % ipList.Count]) + "\n" + ipList[(i + ipindex) % ipList.Count], this.manager.networkAddress == ipList[(i + ipindex) % ipList.Count] ? skin.GetStyle("second_button_actif") : skin.GetStyle("second_button")))
                    {
                        this.manager.networkAddress = ipList[(i + ipindex) % ipList.Count];
                        this.firstScene.PlayButtonSound();
                    }
                }
                Rect rect1 = new Rect(this.posX, this.posY + (2 + i) * spacing, this.width / 3 - 5, this.height);
                if (GUI.Button(rect1, TextDatabase.Start.GetText(), skin.GetStyle("button")))
                {
                    if (this.manager.networkAddress != "")
                    {
                        listServShown = false;
                        Launch(TypeLaunch.Client);
                    }
                    this.firstScene.PlayButtonSound();
                }
                rect1 = new Rect(this.posX + this.width * .33f + 5, this.posY + (2 + i) * spacing, this.width / 3 - 5, this.height);
                if (GUI.Button(rect1, TextDatabase.Create.GetText(), skin.GetStyle("button")))
                {
                    listServShown = false;
                    ipserveurshown = true;
                    this.firstScene.PlayButtonSound();
                }
                rect1 = new Rect(this.posX + this.width * .66f + 10, this.posY + (2 + i) * spacing, this.width / 3 - 5, this.height);
                if (GUI.Button(rect1, TextDatabase.Delete.GetText(), skin.GetStyle("button")))
                {
                    int temp = this.ipList.IndexOf(this.manager.networkAddress);
                    this.ipList.Remove(this.manager.networkAddress);
                    for (int k = temp; k < ipList.Count; k++)
                    {
                        PlayerPrefs.SetString("ip" + k, PlayerPrefs.GetString("ip" + (k + 1)));
                    }
                    PlayerPrefs.SetString(this.manager.networkAddress, "");
                    PlayerPrefs.SetString("ip" + ipList.Count, "");
                    if (ipList.Count == 0)
                        this.listServShown = false;

                }
            }
            else
            {
                listServShown = false;
                ipserveurshown = true;
            }
            if (ipList.Count > 3)
            {
                Rect rectarrow = new Rect(this.posX + this.width + 10, this.posY + 2 * spacing, this.spacing / 2, this.spacing / 2);
                if (GUI.Button(rectarrow, "/\\", skin.GetStyle("button")))
                {
                    ipindex -= 1;
                    if (ipindex < 0)
                        ipindex += ipList.Count;
                    this.firstScene.PlayButtonSound();
                }
                rectarrow = new Rect(this.posX + this.width + 10, this.posY + (Mathf.Min(4, ipList.Count + 1)) * spacing, this.spacing / 2, this.spacing / 2);
                if (GUI.Button(rectarrow, "\\/", skin.GetStyle("button")))
                {
                    ipindex = (ipindex + 1) % ipList.Count;
                    this.firstScene.PlayButtonSound();
                }
            }


        }

        /// <summary>
        /// dessine l'interface de la creation de monde
        /// </summary>
        private void DrawWorldCreate()
        {
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10, this.width, this.height + this.spacing), TextDatabase.EnterName.GetText(), skin.GetStyle("inventory"));
            newWorldName = this.RemoveSpecialCharacter(GUI.TextField(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), newWorldName, 15, this.skin.textField), "abcdefghijklmnopqrstuvwxyz0123456789-_ ", false);
            GUI.Box(new Rect(this.posX, this.posY - 10 + 2 * this.spacing, this.width, this.height + this.spacing), TextDatabase.Seed.GetText(), this.skin.GetStyle("inventory"));
            Rect rect = new Rect(this.posX, this.posY + 3 * this.spacing + this.height, this.width, this.height);
            seedstr = GUI.TextField(rect, this.RemoveSpecialCharacter(seedstr, "abcdefghijklmnopqrstuvwxyz0123456789", false), 6, skin.textField);
            rect = new Rect(this.posX, this.posY - 10 + 5 * this.spacing - this.height, this.width, this.height);

            if (GUI.Button(rect, "Coop/" + TextDatabase.PVP.GetText(), this.skin.GetStyle("button")))
                this.typeCoop = !typeCoop;

            GUI.Box(new Rect(this.posX, this.posY + 4 * this.spacing + 2f * this.height, this.width, this.height + this.spacing), this.typeCoop ? "Coop" : TextDatabase.PVP.GetText(), this.skin.GetStyle("inventory"));
            rect = new Rect(this.posX, this.posY + 6 * this.spacing + 1.5f * this.height, (this.width - 10) / 2, this.height);
            bool possible = true;
            int i = 0;
            worldsList = new List<string>(Directory.GetDirectories(Application.dataPath + "/Saves"));

            for (i = 0; i < worldsList.Count; i++)
                worldsList[i] = worldsList[i].Remove(0, Application.dataPath.Length + 7);

            i = 0;
            while (possible && i < worldsList.Count)
            {
                possible = newWorldName != worldsList[i];
                i++;
            }

            if (possible)
            {
                if (GUI.Button(rect, TextDatabase.Create.GetText(), skin.GetStyle("button")))
                {
                    if (newWorldName != "")
                    {
                        this.world = this.newWorldName;
                        this.newWorldName = "World";
                        this.worldcreateShown = false;
                        System.Random genSeed = new System.Random();
                        int seed = 0;
                        if (seedstr == "")
                            seed = genSeed.Next(int.MaxValue);
                        else
                            seed = MapGeneration.SeedToInt(seedstr);

                        Save.CreateWorld(this.world, seed, this.typeCoop);
                        this.worldsList.Add(this.world);
                        this.Launch(TypeLaunch.Host);
                    }
                    this.firstScene.PlayButtonSound();
                }
            }
            else
                GUI.Box(rect, TextDatabase.Create.GetText(), this.skin.GetStyle("button"));
            rect = new Rect(this.posX + this.width / 2, this.posY + 6 * this.spacing + 1.5f * this.height, (this.width - 10) / 2, this.height);
            if (GUI.Button(rect, TextDatabase.Back.GetText(), skin.GetStyle("button")))
            {
                this.worldcreateShown = false;
                this.world = "";
                this.newWorldName = "World";
                this.typeCoop = true;
                this.firstScene.PlayButtonSound();
            }
        }

        /// <summary>
        /// dessine l'interface de sauvegarde d'ip serveur
        /// </summary>
        private void DrawServeurCreate()
        {
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10, this.width, this.height), TextDatabase.EnterName.GetText(), skin.GetStyle("inventory"));
            newipname = GUI.TextField(new Rect(this.posX, this.posY, this.width, this.height), RemoveSpecialCharacter(newipname, "abcdefghijklmnopqrstuvwxyz0123456789-_ ", false), this.skin.textField);
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10 + 2 * this.spacing, this.width, this.height), "IP", this.skin.GetStyle("inventory"));
            this.manager.networkAddress = GUI.TextField(new Rect(this.posX, this.posY + 2 * this.spacing, this.width, this.height), RemoveSpecialCharacter(this.manager.networkAddress, "0123456789.:abcdefghijklmnopqrstuvwxyz"), this.skin.textField);
            Rect rect = new Rect(this.posX, this.posY + 3 * this.spacing, this.width, this.height);
            if (GUI.Button(rect, TextDatabase.Create.GetText(), skin.GetStyle("button")))
            {
                if (newipname != "" && this.manager.networkAddress != "" && !this.ipList.Contains(this.manager.networkAddress))
                {
                    this.ipserveurshown = false;
                    PlayerPrefs.SetString("ip" + ipList.Count, this.manager.networkAddress);
                    PlayerPrefs.SetString(this.manager.networkAddress, this.newipname);
                    this.newipname = "Serveur";
                    ipList.Add(this.manager.networkAddress);
                }
                this.firstScene.PlayButtonSound();
            }
            rect = new Rect(this.posX, this.posY + 4 * this.spacing, this.width, this.height);
            if (GUI.Button(rect, TextDatabase.Back.GetText(), skin.GetStyle("button")))
            {
                this.ipserveurshown = false;
                this.newipname = "Serveur";
                this.firstScene.PlayButtonSound();
            }
        }

        //Getters
        public string World
        {
            get { return world; }
        }
        #endregion
    }
}
