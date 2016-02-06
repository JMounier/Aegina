using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Social : NetworkBehaviour
{

    private TextMesh nameTextMesh;
    [SyncVar]
    private string namePlayer;
    private bool chatShown = false;
    private int posX, posY;
    private GUISkin skin;

    private string msg = "";
    private string[] log = new string[10] { "", "", "", "", "", "", "", "", "", "" };
    private int logIndex = 0;

    private string[] chat = new string[5] { "\n", "\n", "\n", "\n", "\n" };

    // Use this for initialization
    void Start()
    {

        this.posY = Screen.height - 100;
        this.posX = 10;
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");

        this.nameTextMesh = gameObject.GetComponentInChildren<CharacterCollision>().transform.GetComponentInChildren<TextMesh>();
        if (isLocalPlayer)
        {
            this.nameTextMesh.gameObject.SetActive(false);
            this.CmdSetName(PlayerPrefs.GetString("PlayerName", ""));
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
                    if (Vector3.Distance(player.GetComponentInChildren<CharacterCollision>().transform.position, gameObject.GetComponentInChildren<CharacterCollision>().transform.position) < 10)
                    {
                        other.GetComponent<Social>().nameTextMesh.gameObject.SetActive(true);
                        other.GetComponent<Social>().nameTextMesh.transform.rotation = Quaternion.Euler(gameObject.GetComponentInChildren<Camera>().transform.eulerAngles);
                    }
                    else
                        other.GetComponent<Social>().nameTextMesh.gameObject.SetActive(false);
                }
            }
        }
        else if (this.nameTextMesh.text == "")
        {
            this.nameTextMesh.text = this.namePlayer;
        }
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        if (this.chatShown)
        {
            GUI.SetNextControlName("Chat");
            this.msg = GUI.TextField(new Rect(this.posX, this.posY, 400, 20), this.msg, 85);

            if (GUI.GetNameOfFocusedControl() == string.Empty)
                GUI.FocusControl("Chat");

            if (Event.current.type == EventType.used && Event.current.keyCode == KeyCode.Return)
            {
                this.chatShown = false;
                this.Validate();
                GetComponent<Controller>().Pause = false;
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
        GUI.Box(new Rect(this.posX, this.posY - 155, 500, 140), textChat, this.skin.GetStyle("chat"));
    }

    /// <summary>
    /// Valid le message entre par l'utilisateur
    /// </summary>
    public void Validate()
    {
        if (msg != "" && isLocalPlayer)
        {
            this.CmdSendMsg(this.msg, gameObject);
            for (int i = 8; i > -1; i--)
                this.log[i + 1] = this.log[i];
            this.log[0] = this.msg;
            this.msg = "";
            this.LogIndex = -1;
        }
    }

    /// <summary>
    /// Actualise le nom du joueur 
    /// </summary>
    /// <param name="name"></param>
    [Command]
    private void CmdSetName(string name)
    {
        this.namePlayer = name;
    }

    /// <summary>
    /// Demande au serveur de traiter un message
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="name"></param>
    [Command]
    private void CmdSendMsg(string msg, GameObject sender)
    {
        msg = msg.Trim();
        if (msg[0] == '/')
        {
            string[] cmd = msg.Split();
            try
            {
                switch (cmd[0].ToLower())
                {
                    case "/time":
                        int time = cmd[1].ToLower() == "day" ? 300 : cmd[1].ToLower() == "night" ? 900 : int.Parse(cmd[1]);
                        GameObject.Find("Map").GetComponent<DayNightCycle>().SetTime(time);
                        break;
                    case "/give":
                        sender.GetComponent<Inventory>().AddItemStack(new ItemStack(ItemDatabase.Find(int.Parse(cmd[1])), int.Parse(cmd[2])));
                        break;
                    case "/nick":
                        PlayerPrefs.SetString("PlayerName", cmd[1]);
                        this.CmdSetName(cmd[1]);
                        break;
                    case "/msg":
                    case "/m":
                        if (cmd.Length < 3)
                            throw new System.Exception();
                        string text = "";
                        for (int i = 2; i < cmd.Length; i++)
                            text += cmd[i] + " ";
                        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                            if (player.GetComponent<Social>().namePlayer.ToLower() == cmd[1].ToLower())
                            {
                                player.GetComponent<Social>().RpcReceiveMsg(sender.GetComponent<Social>().namePlayer + " -> You : " + text);
                                return;
                            }
                        sender.GetComponent<Social>().RpcReceiveMsg("Server : \"" + cmd[1] + "\" is not a player.");
                        break;
                    default:
                        sender.GetComponent<Social>().RpcReceiveMsg("Server : \"" + cmd[0] + "\" doesn't exist.");
                        break;
                }
            }
            catch
            {
                sender.GetComponent<Social>().RpcReceiveMsg("Server : The arguments of your command are not valid.");
            }

        }
        else
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                player.GetComponent<Social>().RpcReceiveMsg(sender.GetComponent<Social>().namePlayer + " : " + msg);
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
}
