
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
        public enum TypeLaunch { Host, Client, Server, Stop };
        private NetworkManager manager;
        private GUISkin skin;
        private FirstScene firstScene;
        int posX, posY, width, height, spacing;
        private string playerName;
        private List<string> worldsList;
        private List<string> ipList = new List<string>();
        private int idindex, worldindex = 0;
        private string world = "";
        private string newWorldName = "";

        private bool showGUI = true;
        private bool optionShown = false;
        private bool sonShown = false;
        private bool langueShown = false;
        private bool characterShown = false;
        private bool worldsShown = false;
        private bool listServShown = false;
        private bool worldcreateShown = false;

        void Awake()
        {
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
            Text.SetLanguage(langue);
            this.firstScene = GameObject.Find("Map").GetComponent<FirstScene>();
            if (playerName == "")
                this.showGUI = false;
            if (!Directory.Exists(Application.dataPath + "/Saves"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Saves");
            }
        }
        void Update()
        {
            this.skin.GetStyle("chat").fontSize = (int)(Screen.height * 0.025f);
            this.skin.textField.fontSize = (int)(Screen.height * 0.025f);
            this.skin.GetStyle("button").fontSize = (int)(Screen.height * 0.025f);
            this.posX = (int)(Screen.width / 2.6f);
            this.posY = (int)(Screen.height / 2.5f);
            this.width = Screen.width / 4;
            this.height = Screen.height / 30;
            this.spacing = this.height * 2;
        }
        void Start()
        {
            worldsList = new List<string>(Directory.GetDirectories(Application.dataPath + "/Saves"));
            for (int i = 0; i < worldsList.Count; i++)
            {
                worldsList[i] = worldsList[i].Remove(0, Application.dataPath.Length + 7);
            }
            int j = 0;
            while (PlayerPrefs.GetString("ip" + j.ToString(), " ") != " ")
            {
                ipList.Add(PlayerPrefs.GetString("ip" + j.ToString()));
                j++;
            }
            Debug.Log(ipList.Count);
        }

        void OnGUI()
        {
            if (!showGUI)
            {
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                GUI.Box(new Rect(this.posX, this.posY - this.spacing, this.width, this.height * 2.5f), "<color=white>" + TextDatabase.EnterName.GetText() + "</color>", this.skin.GetStyle("chat"));
                this.playerName = GUI.TextField(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), this.RemoveSpecialCharacter(this.playerName), 15, this.skin.textField);
                if (this.playerName != "" && GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
                {
                    this.showGUI = true;
                    PlayerPrefs.SetString("PlayerName", this.playerName);
                }
            }
            else if (!optionShown && !sonShown && !langueShown && !characterShown && !worldcreateShown)
            {
                if (!this.manager.isNetworkActive)
                {
                    // Verification of Sound when we come back to menu
                    GameObject map = GameObject.Find("Map");
                    if (this.firstScene == null && map != null)
                        this.firstScene = map.GetComponent<FirstScene>();

                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Play.GetText(), this.skin.GetStyle("button")))
                    {
                        this.worldsShown = !this.worldsShown;
                        this.listServShown = false;
                        this.firstScene.PlayButtonSound();
                    }

                    if (GUI.Button(new Rect(this.posX, this.posY + (worldsShown? Mathf.Min(4,worldsList.Count + 1)*this.spacing:0)+this.spacing, this.width / 2 - 10, this.height), TextDatabase.Join.GetText(), this.skin.GetStyle("button")))
                    {
                        this.listServShown = !this.listServShown;
                        this.worldsShown = false;
                        this.firstScene.PlayButtonSound();
                    }

                    this.manager.networkAddress = GUI.TextField(new Rect(this.posX + this.width * .5f + 10, this.posY + (worldsShown ? Mathf.Min(4, worldsList.Count + 1) * this.spacing : 0)+ this.spacing, this.width / 2 - 10, this.height), this.manager.networkAddress, this.skin.textField);

                    if (GUI.Button(new Rect(this.posX, this.posY + (worldsShown ? Mathf.Min(4, worldsList.Count + 1) * this.spacing :listServShown? Mathf.Min(4, ipList.Count + 1) * this.spacing: 0) + this.spacing * 2, this.width, this.height), TextDatabase.Settings.GetText(), skin.GetStyle("button")))
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
                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Cancel.GetText(), this.skin.GetStyle("button")))
                    {
                        this.firstScene.PlayButtonSound();
                        this.Launch(TypeLaunch.Stop);
                    }
                    GUI.Box(new Rect(this.posX, this.posY + this.spacing / 2, this.width, this.height * 2.5f), "<color=white>" + TextDatabase.Loading.GetText() + "</color>", this.skin.GetStyle("chat"));
                }
            }
            else
            {
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
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
            }
        }

        private string RemoveSpecialCharacter(string str, bool space = false)
        {
            string newstr = "";
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || (space && c == ' '))
                    newstr += c;
            }
            return newstr;
        }

        private void Launch(TypeLaunch type)
        {
            switch (type)
            {
                case TypeLaunch.Host:
                    this.manager.StartHost();
                    break;
                case TypeLaunch.Client:
                    this.manager.StartClient();
                    break;
                case TypeLaunch.Server:
                    this.manager.OnStartServer();
                    break;
                default:
                    this.manager.StopHost();
                    break;
            }
        }

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
            GUI.Box(new Rect(this.posX, this.posY - this.spacing, this.width, this.height * 2.5f), "<color=white>" + TextDatabase.EnterName.GetText() + "</color>", this.skin.GetStyle("chat"));
            this.playerName = GUI.TextField(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), this.RemoveSpecialCharacter(this.playerName), 15, this.skin.textField);
            if (this.playerName != "" && GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = false;
                this.optionShown = true;
                PlayerPrefs.SetString("PlayerName", this.playerName);
            }
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = false;
                this.optionShown = true;
                this.playerName = PlayerPrefs.GetString("PlayerName", "");
            }
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
                    Rect rect = new Rect(this.posX, this.posY + (1 + i) * spacing, this.width, this.height);
                    if (GUI.Button(rect, worldsList[(i + worldindex) % worldsList.Count], world == worldsList[(i + worldindex) % worldsList.Count] ? skin.GetStyle("slot") : skin.GetStyle("Button")))
                    {
                        world = worldsList[(i + worldindex) % worldsList.Count];
                    }
                }
                Rect rect1 = new Rect(this.posX, this.posY + (1 + i) * spacing, this.width / 2 - 10, this.height);
                if (GUI.Button(rect1,TextDatabase.Play.GetText(),skin.GetStyle("button")))
                {
                    if (world != "")
                    {
                        worldsShown = false;
                        Launch(TypeLaunch.Host);
                    }
                }
                rect1 = new Rect(this.posX + this.width * .5f + 10, this.posY + (1 + i) * spacing, this.width / 2 - 10, this.height);
                if (GUI.Button(rect1,"Create",skin.GetStyle("button")))
                {
                    worldsShown = false;
                    worldcreateShown = true;
                }
            }
            else
            {
                worldsShown = false;
                worldcreateShown = true;
            }
            if (worldsList.Count > 3)
            {
                Rect rectarrow = new Rect(this.posX + this.width + 10, this.posY + spacing, this.spacing / 2, this.spacing / 2 );
                if (GUI.Button(rectarrow,"/\\",skin.GetStyle("button")))
                {
                    worldindex -= 1;
                    if (worldindex < 0)
                        worldindex += worldsList.Count;
                }
                rectarrow = new Rect(this.posX + this.width + 10, this.posY + (Mathf.Min(3,worldsList.Count)) * spacing, this.spacing / 2 , this.spacing / 2 );
                if (GUI.Button(rectarrow, "\\/", skin.GetStyle("button")))
                    worldindex = (worldindex + 1) % worldsList.Count;
            }
        }
        /// <summary>
        /// Dessine l'interface de la liste des ips
        /// </summary>
        private void DrawIplist()
        {

        }
        /// <summary>
        /// dessine l'interface de la creation de monde
        /// </summary>
        private void DrawWorldCreate()
        {
            
            newWorldName = this.RemoveSpecialCharacter(GUI.TextField(new Rect(this.posX , this.posY , this.width, this.height), newWorldName, this.skin.textField),true);
            Rect rect = new Rect(this.posX, this.posY + this.spacing, this.width, this.height);
            if (GUI.Button(rect,"Create",skin.GetStyle("button")))
            {
                if (newWorldName != "")
                {
                    bool possible = true;
                    int i = 0;
                    worldsList = new List<string>(Directory.GetDirectories(Application.dataPath + "/Saves"));
                    for ( i = 0; i < worldsList.Count; i++)
                    {
                        worldsList[i] = worldsList[i].Remove(0, Application.dataPath.Length + 7);
                    }
                    i = 0;
                    while (possible && i < worldsList.Count)
                    {
                        possible = newWorldName != worldsList[i];
                        i++;
                    }
                    if (possible)
                    {
                        world = newWorldName;
                        worldcreateShown = false;
                        Directory.CreateDirectory(Application.dataPath + "/Saves/" + world);
                        //to do : create World
                        worldsList.Add(world);
                        this.Launch(TypeLaunch.Host);
                    }
                }
            }
        }

        //Getters

        public string World
        {
            get { return world; }
        }
    }
}

