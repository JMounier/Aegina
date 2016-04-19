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
	[SyncVar]
	private Team team = Team.Blue;
    private bool isOp = false;
    private List<Tuple<float,float>>[] posRespawn;

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
            GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().IsLoad();
            posRespawn = new List<Tuple<float, float>>[5];
            for (int i = 0; i < 5; i++)
            {
                posRespawn[i] = new List<Tuple<float, float>>();
                posRespawn[i].Add(new Tuple<float, float>(0, 0));
            }
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
                GUI.Box(new Rect(Screen.width / 2 - Screen.width * 0.075f, y, Screen.width * 0.15f, Screen.height * 0.04f), name, this.skin.GetStyle("button"));
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

    public void NewRespawnPoint(Team team,Tuple<float,float> coord)
    {
        this.posRespawn[(int)team].Add(coord);
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
                player.GetComponent<Social>().RpcReceiveMsg("<b>[" + this.namePlayer + "]</b> " + msg);
    }

    /// <summary>
    /// Synchronise le nom du joueur.
    /// </summary>
    /// <param name="name"></param>
    [Command]
    public void CmdSetName(string name)
    {
        this.namePlayer = name;
        GameObject.Find("Map").GetComponent<Save>().AddPlayer(gameObject, isLocalPlayer);
        this.isOp = GameObject.Find("Map").GetComponent<Save>().LoadPlayer(gameObject).IsOp;
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
    /// Cmds the set team.
    /// </summary>
    /// <param name="team">Team.</param>
	[Command]
	public void CmdSetTeam(Team team){
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

    public List<Tuple<float,float>>[] PosRespawn
    {
        get { return posRespawn; }
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
	public Team Team{
		get{ return this.team;}
	}

}