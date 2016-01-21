﻿
using System.Collections;

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class NetworkManagerHUD : MonoBehaviour
    {
        private enum TypeLaunch { Host, Client, Server, Stop };
        private NetworkManager manager;
        private bool showGUI = true;
        private int offsetX;
        private int offsetY;
        private GUISkin skin;
        private Menu menu;
        private GameObject mainCam;

        void Awake()
        {
            this.manager = GetComponent<NetworkManager>();
            this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
            this.offsetX = Screen.width / 2 - 100;
            this.offsetY = Screen.height / 2 - 100;
            this.mainCam = GameObject.Find("MainCamera");
        }

        void Update()
        {
            if (!showGUI)
                return;

            if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
            {
                if (Input.GetKeyDown(KeyCode.S))
                    Launch(TypeLaunch.Server);
                else if (Input.GetKeyDown(KeyCode.H))
                    Launch(TypeLaunch.Host);
                else if (Input.GetKeyDown(KeyCode.C))
                    Launch(TypeLaunch.Client);
            }
        }

        void OnGUI()
        {
            if (!showGUI)
                return;

            int xpos = 10 + offsetX;
            int ypos = 40 + offsetY;
            int spacing = 24;

            if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Host(H)", this.skin.GetStyle("button")))
                    this.Launch(TypeLaunch.Host);

                ypos += spacing;

                if (GUI.Button(new Rect(xpos, ypos, 105, 20), "LAN Client(C)", this.skin.GetStyle("button")))
                    this.Launch(TypeLaunch.Client);

                manager.networkAddress = GUI.TextField(new Rect(xpos + 110, ypos, 95, 20), this.manager.networkAddress);
                ypos += spacing;

                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Server Only(S)", skin.GetStyle("button")))
                    this.Launch(TypeLaunch.Server);

                ypos += spacing;
            }
            else if (NetworkServer.active)
            {
                GUI.Label(new Rect(10, 40, 300, 20), "Server: port=" + this.manager.networkPort);
                GUI.Label(new Rect(10, 64, 300, 20), "Client: address=" + this.manager.networkAddress + " port=" + this.manager.networkPort);
            }

            if (NetworkServer.active || NetworkClient.active)
            {
                if (GUI.Button(new Rect(10, 88, 200, 20), "Stop", skin.GetStyle("button")))
                    this.Launch(TypeLaunch.Stop);
            }
        }

        private void Launch(TypeLaunch type)
        {
            if (type == TypeLaunch.Host)
            {
                this.manager.StartHost();
                this.mainCam.SetActive(false);
            }
            else if (type == TypeLaunch.Client)
            {
                this.manager.StartClient();
                this.mainCam.SetActive(false);
            }
            else if (type == TypeLaunch.Server)
            {
                this.manager.OnStartServer();
            }
            else if (type == TypeLaunch.Stop)
            {
                this.mainCam.SetActive(true);
                this.manager.StopHost();
            }
        }
    }
}
