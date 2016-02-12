
using System.Collections;

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class NetworkManagerHUD : MonoBehaviour
    {
        public enum TypeLaunch { Host, Client, Server, Stop };
        private NetworkManager manager;
        private bool showGUI = true;
        private int offsetX;
        private int offsetY;
        private GUISkin skin;
        private Menu menu;
        private string playerName;
        private bool menuShown = false;
        private bool optionShown = false;
        private bool sonShown = false;
        private bool langueShown = false;

        void Awake()
        {
            this.manager = GetComponent<NetworkManager>();
            this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
            this.offsetX = Screen.width / 2 - 100;
            this.offsetY = Screen.height / 2 - 100;
            this.playerName = PlayerPrefs.GetString("PlayerName", "Enter a name");
            if (playerName == "Enter a name")
                this.showGUI = false;
        }

        void Update()
        {
            if (!showGUI)
                return;

            if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
            {
                if (!Cursor.visible)
                    Cursor.visible = true;
                if (Input.GetKeyDown(KeyCode.H))
                    Launch(TypeLaunch.Host);
                else if (Input.GetKeyDown(KeyCode.C))
                    Launch(TypeLaunch.Client);
                else if (Input.GetKeyDown(KeyCode.O))
                    this.menuShown = true;
            }
        }

        void OnGUI()
        {
            if (!menuShown && !optionShown && !sonShown && !langueShown)
            {

                if (!showGUI)
                {
                    this.playerName = GUI.TextField(new Rect(this.offsetX - 100, this.offsetY + 80, 200, 20), this.playerName, 15);
                    if (this.playerName != "" && this.playerName != "Enter a name" && GUI.Button(new Rect(this.offsetX + 150, this.offsetY + 80, 75, 20), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
                    {
                        this.showGUI = true;
                        PlayerPrefs.SetString("PlayerName", this.playerName);
                    }
                    return;
                }

                int xpos = 10 + offsetX;
                int ypos = 40 + offsetY;
                int spacing = 30;

                if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
                {
                    GUI.Box(new Rect(Screen.width / 2 - Screen.width / 5, Screen.height / 2 - 200, 13 * Screen.width / 30, 400), "MENU", this.skin.GetStyle("windows"));
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Jouer.GetText(), this.skin.GetStyle("button")))
                        this.Launch(TypeLaunch.Host);

                    ypos += spacing;

                    if (GUI.Button(new Rect(xpos, ypos, 105, 20), TextDatabase.Rejoindre.GetText(), this.skin.GetStyle("button")))
                        this.Launch(TypeLaunch.Client);

                    this.manager.networkAddress = GUI.TextField(new Rect(xpos + 110, ypos, 95, 20), this.manager.networkAddress);
                    ypos += spacing;
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Options(O)", skin.GetStyle("button")))
                    {
                        this.menuShown = true;
                    }
                    ypos += spacing;
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
                        Application.Quit();
                }
            }
            else if (!NetworkClient.active && !NetworkServer.active)
            {
                GUI.Box(new Rect(Screen.width / 2 - Screen.width / 5, Screen.height / 2 - 200, 13 * Screen.width / 30, 400), "MENU", this.skin.GetStyle("windows"));
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

        private void DrawMenu()
        {
            GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 - 200, Screen.width / 3, 325), "MENU", this.skin.GetStyle("windows"));
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), TextDatabase.Continuer.GetText(), this.skin.GetStyle("button")))
            {
                this.menuShown = false;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), TextDatabase.Quit.GetText(), this.skin.GetStyle("button")))
            {
                Application.Quit();

                // TO DO => StopServer / Save Map OR Deco
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), "Options", this.skin.GetStyle("button")))
            {
                this.menuShown = false;
                this.optionShown = true;
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
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), TextDatabase.Son.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.sonShown = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), TextDatabase.Langue.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.langueShown = true;
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
            }
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
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 40, 80, 40), TextDatabase.Francais.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 0);
                Text.SetLanguage(SystemLanguage.French);
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 120, 80, 40), TextDatabase.Anglais.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 1);
                Text.SetLanguage(SystemLanguage.English);
            }
        }

    } 
}

