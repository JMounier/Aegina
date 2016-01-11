
using System.Collections;

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class NetworkManagerHUD : MonoBehaviour
    {
        public NetworkManager manager;
        [SerializeField]
        public bool showGUI = true;
        [SerializeField]
        public int offsetX;
        [SerializeField]
        public int offsetY;
        private GUISkin skin;
        private Menu menu; 
        private GameObject MainCam;

        // Runtime variable
        bool showServer = false;

        void Awake()
        {
            manager = GetComponent<NetworkManager>();
            skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
            offsetX = Screen.width / 2 - 100;
            offsetY = Screen.height / 2 - 100;
            MainCam = GameObject.Find("MainCamera");
        }

        void Update()
        {
            if (!showGUI)
                return;

            if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    manager.StartServer();
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    MainCam.SetActive(false);
                    manager.StartHost();
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    manager.StartClient();
                }
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
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Host(H)", skin.GetStyle("button")))
                {
                    MainCam.SetActive(false);
                    manager.StartHost();
                }
                ypos += spacing;

                if (GUI.Button(new Rect(xpos, ypos, 105, 20), "LAN Client(C)",skin.GetStyle("button")))
                {
                    manager.StartClient();
                }
                manager.networkAddress = GUI.TextField(new Rect(xpos + 110, ypos, 95, 20), manager.networkAddress);
                ypos += spacing;

                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Server Only(S)", skin.GetStyle("button")))
                {
                    manager.StartServer();
                }
                ypos += spacing;
            }
            else
            {
                if (NetworkServer.active)
                {
                    GUI.Label(new Rect(10, 40, 300, 20), "Server: port=" + manager.networkPort);
                }
                if (NetworkClient.active)
                {
                    GUI.Label(new Rect(10  , 64, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
                }
            }

            if (NetworkClient.active && !ClientScene.ready)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready", skin.GetStyle("button")))
                {
                    MainCam.SetActive(false);
                    ClientScene.Ready(manager.client.connection);

                    if (ClientScene.localPlayers.Count == 0)
                    {
                        ClientScene.AddPlayer(0);
                    }
                }
                ypos += spacing;
            }

            if (NetworkServer.active || NetworkClient.active)
            {
                if (GUI.Button(new Rect(10, 88, 200, 20), "Stop",skin.GetStyle("button")))
                {
                    MainCam.SetActive(true);
                    manager.StopHost();
                }
            }

            if (!NetworkServer.active && !NetworkClient.active)
            {
                ypos += 10;

                if (manager.matchMaker == null)
                {
                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Enable Match Maker (M)",skin.GetStyle("button")))
                    {
                        manager.StartMatchMaker();
                    }
                    ypos += spacing;
                }
                else
                {
                    if (manager.matchInfo == null)
                    {
                        if (manager.matches == null)
                        {
                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Create Internet Match", skin.GetStyle("button")))
                            {
                                manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
                            }
                            ypos += spacing;

                            GUI.Label(new Rect(xpos, ypos, 100, 20), "Room Name:");
                            manager.matchName = GUI.TextField(new Rect(xpos + 100, ypos, 100, 20), manager.matchName);
                            ypos += spacing;

                            ypos += 10;

                            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Find Internet Match", skin.GetStyle("button")))
                            {
                                manager.matchMaker.ListMatches(0, 20, "", manager.OnMatchList);
                            }
                            ypos += spacing;
                        }
                        else
                        {
                            foreach (var match in manager.matches)
                            {
                                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Join Match:" + match.name, skin.GetStyle("button")))
                                {
                                    manager.matchName = match.name;
                                    manager.matchSize = (uint)match.currentSize;
                                    manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
                                }
                                ypos += spacing;
                            }
                        }
                    }

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Change MM server", skin.GetStyle("button")))
                    {
                        showServer = !showServer;
                    }
                    if (showServer)
                    {
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Local", skin.GetStyle("button")))
                        {
                            manager.SetMatchHost("localhost", 1337, false);
                            showServer = false;
                        }
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Internet", skin.GetStyle("button")))
                        {
                            manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
                            showServer = false;
                        }
                        ypos += spacing;
                        if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Staging", skin.GetStyle("button")))
                        {
                            manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
                            showServer = false;
                        }
                    }

                    ypos += spacing;

                    GUI.Label(new Rect(xpos, ypos, 300, 20), "MM Uri: " + manager.matchMaker.baseUri);
                    ypos += spacing;

                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Disable Match Maker", skin.GetStyle("button")))
                    {
                        manager.StopMatchMaker();
                    }
                    ypos += spacing;
                }
            }
        }
    }
};

