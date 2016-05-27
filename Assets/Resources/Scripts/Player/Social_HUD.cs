using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public enum Activity { Connection, Disconnection, Death };

public class Social_HUD : NetworkBehaviour
{
    private TextMesh nameTextMesh;
    [SyncVar]
    private string namePlayer;
    [SyncVar]
    private bool chatShown = false;
    [SyncVar]
    private Team team = Team.Neutre;
    private bool isOp = false;

    private int posX, posY;
    private GUISkin skin;
    private string msg = "";
    private string[] log = new string[10] { "", "", "", "", "", "", "", "", "", "" };
    private int logIndex = 0;

    private string[] chat = new string[5] { "\n", "\n", "\n", "\n", "\n" };

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
            this.skin.textField.alignment = TextAnchor.MiddleCenter;

            this.skin.textField.fontSize = (int)(Screen.height * 0.025f);
            // PlayerName
            this.nameTextMesh.gameObject.SetActive(false);
            this.namePlayer = PlayerPrefs.GetString("PlayerName", "");
            this.CmdSetName(this.namePlayer);
            this.CmdSendActivity(Activity.Connection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set rotation of name well
        if (isLocalPlayer)
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                if (player != null && !gameObject.Equals(player))
                {
                    Social_HUD other = player.GetComponent<Social_HUD>();
                    other.nameTextMesh.text = other.namePlayer;
                    other.nameTextMesh.color = other.team == Team.Blue ? new Color(.12f, 0f, 1f) : new Color(.78f, .15f, .15f);
                    if (other.GetComponent<SyncCharacter>().Life > 0 && Vector3.Distance(player.transform.FindChild("Character").position, gameObject.transform.FindChild("Character").position) < 10)
                    {
                        other.GetComponent<Social_HUD>().nameTextMesh.gameObject.SetActive(true);
                        other.GetComponent<Social_HUD>().nameTextMesh.transform.rotation = Quaternion.Euler(gameObject.GetComponentInChildren<Camera>().transform.eulerAngles);
                    }
                    else
                        other.GetComponent<Social_HUD>().nameTextMesh.gameObject.SetActive(false);
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
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                string n = p.GetComponent<Social_HUD>().PlayerName;
                GUI.Box(new Rect(Screen.width / 2 - Screen.width * 0.075f, y, Screen.width * 0.15f, Screen.height * 0.04f), n, n == this.namePlayer ? this.skin.GetStyle("second_button_actif") : this.skin.GetStyle("second_button"));
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
            this.CmdSendMsg(this.msg.Trim());
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
    private void CmdSendMsg(string msg)
    {
        if (msg[0] == '/')
            Command.LaunchCommand(msg, gameObject);
        else
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                string color = this.team == Team.Blue ? "blue" : "red";
                player.GetComponent<Social_HUD>().RpcReceiveMsg("<b><color=" + color + ">[" + this.namePlayer + "]</color></b> " + msg);
            }
    }

    /// <summary>
    /// Synchronise le nom du joueur.
    /// </summary>
    /// <param name="name"></param>
    [Command]
    public void CmdSetName(string name)
    {
        this.namePlayer = name;
        Save save = GameObject.Find("Map").GetComponent<Save>();
        save.AddPlayer(gameObject, isLocalPlayer);
        PlayerSave ps = save.LoadPlayer(gameObject);
        this.isOp = ps.IsOp;
        this.team = ps.PlayerTeam;        
    }

    /// <summary>
    /// Deconnecte le joueur du serveur.
    /// </summary>
    [ClientRpc]
    public void RpcKickPlayer()
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
    public void CmdSendActivity(Activity act)
    {
        switch (act)
        {
            case Activity.Connection:
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                    if (p.GetComponent<Social_HUD>().namePlayer != this.namePlayer)
                        p.GetComponent<Social_HUD>().RpcReceiveMsg("<color=grey>* <i>" + this.namePlayer + "</i> joined the game.</color>");
                break;
            case Activity.Death:
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                    p.GetComponent<Social_HUD>().RpcReceiveMsg("<color=grey>* <i>" + this.namePlayer + "</i> died.</color>");
                Stats.IncrementDeath();
                break;
            case Activity.Disconnection:
                if (this.namePlayer != null)
                    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                        p.GetComponent<Social_HUD>().RpcReceiveMsg("<color=grey>* <i>" + this.namePlayer + "</i> leave the game.</color>");
                break;
            default:
                throw new System.ArgumentException("Activity is not valid");
        }
    }
    /// <summary>
    /// Cmds the set team.
    /// </summary>
    /// <param name="team">Team.</param>
    [Command]
    public void CmdSetTeam(Team team)
    {
        this.team = team;
    }
    /// <summary>
    /// Envoi un message du serveur vers les clients
    /// </summary>
    /// <param name="msg"></param>
    [ClientRpc]
    public void RpcReceiveMsg(string msg)
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
    public void RpcTeleport(Vector3 pos)
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

    /// <summary>
    /// Le nom du joueur.
    /// </summary>
    public string PlayerName
    {
        get { return this.namePlayer; }
    }

    /// <summary>
    /// Renvoi si le joueur est un operateur.
    /// </summary>
    public bool IsOp
    {
        get { return this.isOp; }
        set { this.isOp = value; }
    }
    /// <summary>
    /// Gets the team.
    /// </summary>
    /// <value>The team.</value>
    public Team Team
    {
        get { return this.team; }
    }

}