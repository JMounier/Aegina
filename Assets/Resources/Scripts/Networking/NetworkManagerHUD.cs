
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
        private GUISkin skin;
        private Sound sound;
        private int offsetX;
        private int offsetY;
        private string playerName;
        private bool showGUI = true;
        private bool optionShown = false;
        private bool sonShown = false;
        private bool langueShown = false;

        void Awake()
        {
            this.manager = GetComponent<NetworkManager>();
            this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
            this.skin.GetStyle("chat").fontSize = (int)(Screen.height * 0.025f);
            this.offsetX = Screen.width / 2 - 100;
            this.offsetY = Screen.height / 2 - 100;
            this.playerName = PlayerPrefs.GetString("PlayerName", "Enter a name");
            this.sound = GameObject.Find("Map").GetComponentInChildren<Sound>();
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
                    this.optionShown = true;
            }
        }

        void OnGUI()
        {
            if (!showGUI)
            {
                
                this.playerName = GUI.TextField(new Rect(this.offsetX - 100, this.offsetY + 80, 200, 20), this.playerName, 15);
                if (this.playerName != "" && this.playerName != "Enter a name" && GUI.Button(new Rect(this.offsetX + 150, this.offsetY + 80, 75, 20), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
                {
                    this.showGUI = true;
                    PlayerPrefs.SetString("PlayerName", this.playerName);
                }
            }
            else if (!optionShown && !sonShown && !langueShown)
            {
                int xpos = 10 + offsetX;
                int ypos = 40 + offsetY;
                int spacing = 30;
                                
                if (!this.manager.isNetworkActive)
                {
                    // Verification of Sound when we come back to menu
                    GameObject map = GameObject.Find("Map");
                    if (this.sound == null && map != null)
                        this.sound = map.GetComponentInChildren<Sound>();

                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Play.GetText(), this.skin.GetStyle("button")))
                    {
                        this.Launch(TypeLaunch.Host);
                        this.sound.PlaySound(AudioClips.Button, 1f);
                    }
                    ypos += spacing;

                    if (GUI.Button(new Rect(xpos, ypos, 105, 20), TextDatabase.Join.GetText(), this.skin.GetStyle("button")))
                    {
                        this.Launch(TypeLaunch.Client);
                        this.sound.PlaySound(AudioClips.Button, 1f);
                    }
                    this.manager.networkAddress = GUI.TextField(new Rect(xpos + 110, ypos, 95, 20), this.manager.networkAddress);
                    ypos += spacing;

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Options(O)", skin.GetStyle("button")))
                    {
                        this.optionShown = true;
                        this.sound.PlaySound(AudioClips.Button, 1f);
                    }
                    ypos += spacing;
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
                    {
                        this.sound.PlaySound(AudioClips.Button, 1f);
                        Application.Quit();
                    }
                }

                else if (!this.manager.IsClientConnected())
                {
                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Cancel.GetText(), this.skin.GetStyle("button")))
                    {
                        this.sound.PlaySound(AudioClips.Button, 1f);
                        this.Launch(TypeLaunch.Stop);
                    }
                    GUI.Box(new Rect(xpos + 68, ypos, 200, 60), "<color=white>" + TextDatabase.Loading.GetText() + "</color>", this.skin.GetStyle("chat"));
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

        /// <summary>
        ///  Dessine l'interface des options.
        /// </summary>
        private void DrawOption()
        {
            int xpos = 10 + offsetX;
            int ypos = 40 + offsetY;
            int spacing = 30;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Sound.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.sonShown = true;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Language.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.langueShown = true;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.showGUI = true;
                this.optionShown = false;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }
        }

        /// <summary>
        ///  Dessine l'interface des options sonores.
        /// </summary>
        private void DrawSon()
        {
            int xpos = 10 + offsetX;
            int ypos = 40 + offsetY;
            int spacing = 30;

            this.sound.Volume = GUI.HorizontalSlider(new Rect(xpos, ypos, 200, 20), this.sound.Volume, 0f, 1f, this.skin.GetStyle("horizontalslider"), this.skin.GetStyle("horizontalsliderthumb"));

            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.sonShown = false;
                PlayerPrefs.SetFloat("Sound_intensity", this.sound.Volume);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.sonShown = false;
                this.sound.Volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }
        }

        /// <summary>
        ///  Dessine l'interface des options linguistiques.
        /// </summary>
        private void DrawLangue()
        {
            int xpos = 10 + offsetX;
            int ypos = 40 + offsetY;
            int spacing = 30;


            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.French.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 0);
                Text.SetLanguage(SystemLanguage.French);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.English.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 1);
                Text.SetLanguage(SystemLanguage.English);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            ypos += spacing;

            if (GUI.Button(new Rect(xpos, ypos, 200, 20), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.langueShown = false;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }
        }

    }
}

