
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
        private int worldindex = 0;
        private int ipindex = 0;
        private string world = "";
        private string newWorldName = "World";
        private string seedstr = "";
        private string newipname = "Serveur";
        private string typegame = "";

        private bool showGUI = true;
        private bool optionShown = false;
        private bool sonShown = false;
        private bool langueShown = false;
        private bool characterShown = false;
        private bool worldsShown = false;
        private bool listServShown = false;
        private bool worldcreateShown = false;
        private bool ipserveurshown = false;
        private bool loading = false;
        private float incr;
        private float incg;
        private float incb;

        void Awake()
        {
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
            Text.SetLanguage(langue);
            this.firstScene = GameObject.Find("Map").GetComponent<FirstScene>();
            if (playerName == "")
                this.showGUI = false;

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
            while (PlayerPrefs.GetString("ip" + j.ToString(), "") != "")
            {
                ipList.Add(PlayerPrefs.GetString("ip" + j.ToString()));
                j++;
            }
        }

        void OnGUI()
        {
            if (loading)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), TextDatabase.Loading.GetText(), skin.GetStyle("loading"));
                Color skinColor = this.skin.GetStyle("loading").normal.textColor;
                float r = skinColor.r + incr;
                if (r > 1)
                {
                    incr = Random.Range(-0.02f, 0);
                }
                if (r < 0)
                {
                    incr = Random.Range(0, 0.02f);
                }
                float g = skinColor.g + incg;
                if (g > 1)
                {
                    incg = Random.Range(-0.02f, 0);
                }
                if (g < 0)
                {
                    incg = Random.Range(0, 0.02f);
                }
                float b = skinColor.b + incb;
                if (b > 1)
                {
                    incb = Random.Range(-0.02f, 0);
                }
                if (b < 0)
                {
                    incb = Random.Range(0, 0.02f);
                }
                this.skin.GetStyle("loading").normal.textColor = new Color(r, g, b);
            }
            if (!showGUI)
            {
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                GUI.Box(new Rect(this.posX, this.posY - this.spacing, this.width, this.height * 2.5f), "<color=white>" + TextDatabase.EnterName.GetText() + "</color>", this.skin.GetStyle("chat"));
                this.playerName = GUI.TextField(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), this.RemoveSpecialCharacter(this.playerName, "abcdefghijklmnopqrstuvwxyz123456789-_", false), 15, this.skin.textField);
                if (this.playerName != "" && GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
                {
                    this.showGUI = true;
                    PlayerPrefs.SetString("PlayerName", this.playerName);
                }
            }
            else if (!optionShown && !sonShown && !langueShown && !characterShown && !worldcreateShown && !ipserveurshown)
            {
                if (!this.manager.isNetworkActive)
                {
                    // Verification of Sound when we come back to menu
                    GameObject map = GameObject.Find("Map");
                    if (this.firstScene == null && map != null)
                    {
                        this.firstScene = map.GetComponent<FirstScene>();
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }

                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
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
                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Cancel.GetText(), this.skin.GetStyle("button")))
                    {
                        this.firstScene.PlayButtonSound();
                        this.Launch(TypeLaunch.Stop);
                        this.manager.networkAddress = "localhost";
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
                else if (ipserveurshown)
                    this.DrawServeurCreate();
            }
        }

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
            this.loading = true;
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
            this.playerName = GUI.TextField(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), this.RemoveSpecialCharacter(this.playerName, "abcdefghijklmnopqrstuvwxyz123456789-_", false), 15, this.skin.textField);
            if (this.playerName != "" && GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = false;
                this.optionShown = true;
                PlayerPrefs.SetString("PlayerName", this.playerName);
                this.firstScene.PlayButtonSound();
            }
            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.characterShown = false;
                this.optionShown = true;
                this.playerName = PlayerPrefs.GetString("PlayerName", "");
                this.firstScene.PlayButtonSound();
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
                rectarrow = new Rect(this.posX + this.width + 10, this.posY + (Mathf.Min(4, worldsList.Count + 1)) * spacing, this.spacing / 2, this.spacing / 2);
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
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10, this.width, this.height), TextDatabase.EnterName.GetText(), skin.GetStyle("inventory"));
            newWorldName = this.RemoveSpecialCharacter(GUI.TextField(new Rect(this.posX, this.posY, this.width, this.height), newWorldName, this.skin.textField), "abcdefghijklmnopqrstuvwxyz123456789-_ ", false);
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10 + 2 * this.spacing, this.width, this.height), TextDatabase.Seed.GetText(), this.skin.GetStyle("inventory"));
            Rect rect = new Rect(this.posX, this.posY + 2 * this.spacing, this.width, this.height);
            seedstr = GUI.TextField(rect, this.RemoveSpecialCharacter(seedstr, "abcdefghijklmnopqrstuvwxyz123456789", false), 15, skin.textField);
            rect = new Rect(this.posX, this.posY - this.height - 10 + 4 * this.spacing, this.width / 2 - 10, this.height);
            if (GUI.Button(rect, "Coop", this.skin.GetStyle("button")))
            {
                this.typegame = "Coop";
            }
            rect = new Rect(this.posX + this.width / 2, this.posY - this.height - 10 + 4 * this.spacing, this.width / 2 - 10, this.height);
            if (GUI.Button(rect, TextDatabase.PVP.GetText(), this.skin.GetStyle("button")))
            {
                this.typegame = "Joueur Contre Joueur";
            }
            GUI.Box(new Rect(this.posX, this.posY + 4 * this.spacing, this.width, this.height), this.typegame, this.skin.GetStyle("inventory"));
            rect = new Rect(this.posX, this.posY + 5 * this.spacing, this.width, this.height);
            bool possible = true;
            int i = 0;
            worldsList = new List<string>(Directory.GetDirectories(Application.dataPath + "/Saves"));
            for (i = 0; i < worldsList.Count; i++)
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
                        //to do ajout du type de monde lors de la creation
                        Save.CreateWorld(this.world, seed);
                        this.worldsList.Add(this.world);
                        this.typegame = "";
                        this.Launch(TypeLaunch.Host);
                    }
                    this.firstScene.PlayButtonSound();
                }
            }
            else
            {
                GUI.Box(rect, TextDatabase.Create.GetText(), this.skin.GetStyle("slot"));
            }
            rect = new Rect(this.posX, this.posY + 6 * this.spacing, this.width, this.height);
            if (GUI.Button(rect, TextDatabase.Back.GetText(), skin.GetStyle("button")))
            {
                this.worldcreateShown = false;
                this.world = "";
                this.newWorldName = "World";
                this.typegame = "";
                this.firstScene.PlayButtonSound();
            }
        }
        /// <summary>
        /// dessine l'interface de sauvegarde d'ip serveur
        /// </summary>
        private void DrawServeurCreate()
        {
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10, this.width, this.height), TextDatabase.EnterName.GetText(), skin.GetStyle("inventory"));
            newipname = this.RemoveSpecialCharacter(GUI.TextField(new Rect(this.posX, this.posY, this.width, this.height), newipname, this.skin.textField), "0123456789.:abcdefghijklmnopqrstuvwxyz");
            GUI.Box(new Rect(this.posX, this.posY - this.height - 10 + 2 * this.spacing, this.width, this.height), "IP", this.skin.GetStyle("inventory"));
            this.manager.networkAddress = GUI.TextField(new Rect(this.posX, this.posY + 2 * this.spacing, this.width, this.height), this.manager.networkAddress, this.skin.textField);
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
        public void IsLoad()
        {
            loading = false;
            this.skin.GetStyle("loading").normal.textColor = Color.black;
        }
        //Getters
        public string World
        {
            get { return world; }
        }
    }
}

