using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public enum Activity { Connection, Death };

public class Social : NetworkBehaviour
{

    private TextMesh nameTextMesh;
    [SyncVar]
    private string namePlayer;
    [SyncVar]
    private bool chatShown = false;
    private int posX, posY;
    private GUISkin skin;
    private string msg = "";
    private string[] log = new string[10] { "", "", "", "", "", "", "", "", "", "" };
    private int logIndex = 0;

    private string[] chat = new string[5] { "\n", "\n", "\n", "\n", "\n" };
    private string[] playerList = new string[0];

    // Use this for initialization
    void Start()
    {
        this.nameTextMesh = gameObject.GetComponentInChildren<CharacterCollision>().transform.GetComponentInChildren<TextMesh>();
        if (isLocalPlayer)
        {
            // Chat
            this.posY = (int)(Screen.height * 0.82f);
            this.posX = (int)(Screen.width * 0.01f);
            this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
            this.skin.GetStyle("chat").fontSize = (int)(Screen.height * 0.025f);
            this.skin.GetStyle("chat").alignment = TextAnchor.LowerLeft;
            this.skin.GetStyle("chat").fontStyle = FontStyle.Normal;
            this.skin.GetStyle("chat").normal.textColor = Color.grey;
            this.skin.textField.fontSize = (int)(Screen.height * 0.025f);

            // PlayerName
            this.nameTextMesh.gameObject.SetActive(false);
            this.namePlayer = PlayerPrefs.GetString("PlayerName", "");
            this.CmdSetName(this.namePlayer);
            this.CmdSendActivity(Activity.Connection, gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            // Set rotation of name well
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player != null && !gameObject.Equals(player))
                {
                    Social other = player.GetComponent<Social>();
                    if (other.namePlayer != other.nameTextMesh.text)
                        other.nameTextMesh.text = other.namePlayer;
                    if (Vector3.Distance(player.GetComponentInChildren<CharacterCollision>().transform.position, gameObject.GetComponentInChildren<CharacterCollision>().transform.position) < 10)
                    {
                        other.GetComponent<Social>().nameTextMesh.gameObject.SetActive(true);
                        other.GetComponent<Social>().nameTextMesh.transform.rotation = Quaternion.Euler(gameObject.GetComponentInChildren<Camera>().transform.eulerAngles);
                    }
                    else
                        other.GetComponent<Social>().nameTextMesh.gameObject.SetActive(false);
                }
            }
            
            // Update the list of players
            List<string> playerList = new List<string>();
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                playerList.Add(player.GetComponent<Social>().namePlayer);
            if (playerList.Count < this.playerList.Length)
                foreach (string name in this.playerList)
                    if (!playerList.Contains(name))
                        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                            p.GetComponent<Social>().RpcReceiveMsg("<color=grey>* <i>" + name + "</i> leave the game.</color>");
            this.playerList = playerList.ToArray();
        }
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        // Display chat
        if (this.chatShown)
        {            
            GUI.SetNextControlName("Chat");
            this.msg = GUI.TextField(new Rect(this.posX, this.posY, Screen.width * 0.3f, Screen.height * 0.04f), this.msg, 200, this.skin.textField);

            if (GUI.GetNameOfFocusedControl() == "")
                GUI.FocusControl("Chat");

            if (Event.current.type == EventType.used && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.F1))
            {
                this.chatShown = false;
                GetComponent<Controller>().Pause = false;
                this.Validate();
            }
            else if (Event.current.type == EventType.used && Event.current.keyCode == KeyCode.Escape)
            {
                this.chatShown = false;
                GetComponent<Controller>().Pause = false;
            }
            else if (Event.current.type == EventType.used && Event.current.keyCode == KeyCode.UpArrow)
                this.LogIndex++;
            else if (Event.current.type == EventType.used && Event.current.keyCode == KeyCode.DownArrow)
                this.LogIndex--;
        }
        string textChat = "";
        foreach (string str in this.chat)
            textChat += str + "\n";
        GUI.Box(new Rect(this.posX, this.posY - Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * 0.3f), textChat, this.skin.GetStyle("chat"));

        // Display list of players
        if (Input.GetButton("ListPlayer"))
        {
            float y = 0;
            foreach (string name in this.playerList)
            {
                GUI.Box(new Rect(Screen.width/2 - Screen.width * 0.075f, y, Screen.width * 0.15f, Screen.height * 0.04f), name, this.skin.GetStyle("button"));
                y += Screen.height * 0.04f;
            }
        }
    }

    /// <summary>
    /// Valid le message entre par l'utilisateur
    /// </summary>
    public void Validate()
    {
        if (this.msg.Trim() != "" && isLocalPlayer)
        {
            this.CmdSendMsg(this.msg.Trim(), gameObject);
            for (int i = 8; i > -1; i--)
                this.log[i + 1] = this.log[i];
            this.log[0] = this.msg;
            this.msg = "";
            this.LogIndex = -1;
        }
    }

    /// <summary>
    /// Demande au serveur de traiter un message
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="name"></param>
    [Command]
    private void CmdSendMsg(string msg, GameObject sender)
    {
        if (msg[0] == '/')
        {
            string[] cmd = msg.Split(default(Char[]), StringSplitOptions.RemoveEmptyEntries);
            switch (cmd[0].ToLower())
            {
                // HELP
                case "/help":
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=green>---This is the list of commands---</color>\n" +
                        "/time <value> \n/give <player> <id> [quantity] \n/msg <player> <message> \n/tp <player>\n/kick <player>");
                    break;

                // TIME
                case "/time":
                    try
                    {
                        int time = cmd[1].ToLower() == "day" ? 300 : cmd[1].ToLower() == "night" ? 900 : int.Parse(cmd[1]);
                        GameObject.Find("Map").GetComponent<DayNightCycle>().SetTime(time);
                        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                            player.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " set the time to " + cmd[1] + ".");
                    }
                    catch
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /time <value></color>");
                    }
                    break;

                // GIVE
                case "/give":
                    try
                    {
                        Item give;
                        GameObject recipient = null;
                        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                            if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                                recipient = player;
                        if (recipient == null)
                        {
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                            return;
                        }
                        int id;
                        switch (cmd.Length)
                        {
                            case 3:
                                if (int.TryParse(cmd[2], out id))
                                    give = ItemDatabase.Find(id);
                                else
                                    give = ItemDatabase.Find(cmd[2]);

                                recipient.GetComponent<Inventory>().RpcAddItemStack(give.ID, 1, null);
                                sender.GetComponent<Social>().RpcReceiveMsg("Give 1 of " + give.NameText.GetText(SystemLanguage.English) + ".");
                                if (!sender.Equals(recipient))
                                    recipient.GetComponent<Social>().RpcReceiveMsg("Give 1 of " + give.NameText.GetText(SystemLanguage.English) + ".");
                                break;
                            default:
                                if (int.TryParse(cmd[2], out id))
                                    give = ItemDatabase.Find(id);
                                else
                                    give = ItemDatabase.Find(cmd[2]);

                                recipient.GetComponent<Inventory>().RpcAddItemStack(give.ID, int.Parse(cmd[3]), null);
                                sender.GetComponent<Social>().RpcReceiveMsg("Give " + int.Parse(cmd[3]) + " of " + give.NameText.GetText(SystemLanguage.English) + ".");
                                if (!sender.Equals(recipient))
                                    recipient.GetComponent<Social>().RpcReceiveMsg("Give " + int.Parse(cmd[3]) + " of " + give.NameText.GetText(SystemLanguage.English) + ".");
                                break;
                        }
                    }
                    catch
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /give <player> <id> [quantity]</color>");
                    }
                    break;

                // MSG & M
                case "/msg":
                case "/m":
                    try
                    {
                        if (cmd.Length < 3)
                            throw new System.Exception();
                        if (cmd[1].ToLower() == sender.GetComponent<Social>().namePlayer.ToLower())
                        {
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Do you feel alone ?</color>");
                            return;
                        }
                        string text = "";
                        for (int i = 2; i < cmd.Length; i++)
                            text += cmd[i] + " ";
                        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                            if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                            {
                                player.GetComponent<Social>().RpcReceiveMsg("<color=grey><b>[" + sender.GetComponent<Social>().namePlayer + " -> You]</b></color> " + text);
                                sender.GetComponent<Social>().RpcReceiveMsg("<color=grey><b>[You -> " + player.GetComponent<Social>().namePlayer + "]</b></color> " + text);
                                return;
                            }
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                    }
                    catch
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /msg <player> <message></color>");
                    }
                    break;
                case "/tp":
                    try
                    {
                        if (cmd[1].ToLower() == this.namePlayer)
                        {
                            sender.GetComponent<Social>().RpcReceiveMsg("<color=red>You are already where you are...</color>");
                            return;
                        }
                        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                            if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                            {
                                player.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " teleported on you.");
                                sender.GetComponent<Social>().RpcReceiveMsg("You were teleported on " + cmd[1] + ".");
                                sender.GetComponent<Social>().RpcTeleport(player.GetComponentInChildren<CharacterCollision>().transform.position);
                                return;
                            }
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                    }
                    catch
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /tp <player></color>");
                    }
                    break;
                case "/kick":
                    try
                    {
                        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                            if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                            {
                                player.GetComponent<Social>().RpcKickPlayer();
                                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                                    p.GetComponent<Social>().RpcReceiveMsg(this.namePlayer + " kick " + cmd[1] + ".");
                                return;
                            }
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Player " + cmd[1] + " doesn't find.</color>");
                    }
                    catch
                    {
                        sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Usage: /kick <player></color>");
                    }
                    break;
                default:
                    sender.GetComponent<Social>().RpcReceiveMsg("<color=red>Unknow command. Try /help for a list of commands.</color>");
                    break;
            }
        }
        else
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Social>().RpcReceiveMsg("<b>[" + this.namePlayer + "]</b> " + msg);
    }

    /// <summary>
    /// Synchronise le nom du joueur.
    /// </summary>
    /// <param name="name"></param>
    [Command]
    private void CmdSetName(string name)
    {
        this.namePlayer = name;
        GameObject.Find("Map").GetComponent<Save>().AddPlayer(gameObject);
    }

    /// <summary>
    /// Deconnecte le joueur du serveur.
    /// </summary>
    [ClientRpc]
    private void RpcKickPlayer()
    {
        if (isLocalPlayer && isServer)
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
        else if (isLocalPlayer)
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopClient();
    }
       
    /// <summary>
    /// Envoi une nouvel activite au server
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="name"></param>
    [Command]
    public void CmdSendActivity(Activity act, GameObject player)
    {
        switch (act)
        {
            case Activity.Connection:
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                    if (p.GetComponent<Social>().namePlayer != this.namePlayer)
                        p.GetComponent<Social>().RpcReceiveMsg("<color=grey>* <i>" + this.namePlayer + "</i> joined the game.</color>");
                break;
            case Activity.Death:
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                    p.GetComponent<Social>().RpcReceiveMsg("<color=grey>* <i>" + this.namePlayer + "</i> died.</color>");
                break;
            default:
                throw new System.ArgumentException("Activity is not valid");
        }
    }

    /// <summary>
    /// Envoi un message du serveur vers les clients
    /// </summary>
    /// <param name="msg"></param>
    [ClientRpc]
    private void RpcReceiveMsg(string msg)
    {
        for (int i = 0; i < this.chat.Length - 1; i++)
            this.chat[i] = this.chat[i + 1];
        this.chat[this.chat.Length - 1] = msg;
    }

    /// <summary>
    /// Teleport the player to a position
    /// </summary>
    /// <param name="pos">The position of the teleportation</param>
    [ClientRpc]
    private void RpcTeleport(Vector3 pos)
    {
        gameObject.GetComponentInChildren<CharacterCollision>().GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponentInChildren<CharacterCollision>().transform.position = pos;
    }
    // Setters & Getters

    /// <summary>
    /// L'affichage du chat
    /// </summary>
    public bool ChatShown
    {
        get { return this.chatShown; }
        set { this.chatShown = value; }
    }

    /// <summary>
    /// Change l'indice du log.
    /// </summary>
    private int LogIndex
    {
        get { return this.logIndex; }
        set
        {
            this.logIndex = Mathf.Clamp(value, -1, 9);
            if (this.logIndex > -1 && this.log[this.logIndex] == "")
                LogIndex = this.logIndex - 1;
            else if (this.logIndex == -1)
                this.msg = "";
            else
                this.msg = log[this.logIndex];
        }
    }

    public string PlayerName
    {
        get { return this.namePlayer; }
    }
}