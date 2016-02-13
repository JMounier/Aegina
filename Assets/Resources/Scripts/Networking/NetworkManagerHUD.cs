
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
        int posX, posY, width, height, spacing;
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
            this.skin.GetStyle("chat").alignment = TextAnchor.MiddleCenter;
            this.skin.textField.fontSize = (int)(Screen.height * 0.025f);
            this.posX = (int)(Screen.width / 2.6f);
            this.posY = (int)(Screen.height / 2.5f);
            this.width = Screen.width / 4;
            this.height = Screen.height / 30;
            this.spacing = this.height * 2;
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
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                this.playerName = GUI.TextField(new Rect(this.posX, this.posY, this.width, this.height), this.playerName, 15, this.skin.textField);
                if (this.playerName != "" && this.playerName != "Enter a name" && GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
                {
                    this.showGUI = true;
                    PlayerPrefs.SetString("PlayerName", this.playerName);
                }
            }
            else if (!optionShown && !sonShown && !langueShown)
            {
                if (!this.manager.isNetworkActive)
                {
                    // Verification of Sound when we come back to menu
                    GameObject map = GameObject.Find("Map");
                    if (this.sound == null && map != null)
                        this.sound = map.GetComponentInChildren<Sound>();

                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Play.GetText(), this.skin.GetStyle("button")))
                    {
                        this.Launch(TypeLaunch.Host);
                        this.sound.PlaySound(AudioClips.Button, 1f);
                    }

                    if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width / 2 - 10, this.height), TextDatabase.Join.GetText(), this.skin.GetStyle("button")))
                    {
                        this.Launch(TypeLaunch.Client);
                        this.sound.PlaySound(AudioClips.Button, 1f);
                    }

                    this.manager.networkAddress = GUI.TextField(new Rect(this.posX + this.width * .5f + 10, this.posY + this.spacing, this.width / 2 - 10, this.height), this.manager.networkAddress, this.skin.textField);

                    if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Settings.GetText(), skin.GetStyle("button")))
                    {
                        this.optionShown = true;
                        this.sound.PlaySound(AudioClips.Button, 1f);
                    }
                    if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 3, this.width, this.height), TextDatabase.Quit.GetText(), skin.GetStyle("button")))
                    {
                        this.sound.PlaySound(AudioClips.Button, 1f);
                        Application.Quit();
                    }
                }

                else if (!this.manager.IsClientConnected())
                {
                    GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.width / 12.8f), "", this.skin.GetStyle("aegina"));
                    if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Cancel.GetText(), this.skin.GetStyle("button")))
                    {
                        this.sound.PlaySound(AudioClips.Button, 1f);
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
            if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.Sound.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.sonShown = true;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Language.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = false;
                this.langueShown = true;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
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
            this.sound.Volume = GUI.HorizontalSlider(new Rect(this.posX, this.posY, this.width, this.height), this.sound.Volume, 0f, 1f, this.skin.GetStyle("horizontalslider"), this.skin.GetStyle("horizontalsliderthumb"));

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.Validate.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.sonShown = false;
                PlayerPrefs.SetFloat("Sound_intensity", this.sound.Volume);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
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
            if (GUI.Button(new Rect(this.posX, this.posY, this.width, this.height), TextDatabase.French.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 0);
                Text.SetLanguage(SystemLanguage.French);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing, this.width, this.height), TextDatabase.English.GetText(), this.skin.GetStyle("button")))
            {
                PlayerPrefs.SetInt("langue", 1);
                Text.SetLanguage(SystemLanguage.English);
                this.sound.PlaySound(AudioClips.Button, 1f);
            }

            if (GUI.Button(new Rect(this.posX, this.posY + this.spacing * 2, this.width, this.height), TextDatabase.Back.GetText(), this.skin.GetStyle("button")))
            {
                this.optionShown = true;
                this.langueShown = false;
                this.sound.PlaySound(AudioClips.Button, 1f);
            }
        }

    }
}

