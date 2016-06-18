using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Success_HUD : NetworkBehaviour
{
    private int pos_x_success, pos_y_success, size;
    private float move_x, move_y;
    private float incrementation;
    private float posYpercent;
    private Queue successToDisplay;
    private Success ActualSucces;
    private GUISkin skin;
    private Texture2D coude, coude2, coude3, coude4, hLine, vLine;
    private bool activate;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.size = Screen.width / 25;
        this.pos_x_success = (Screen.width - 12 * this.size) / 2;
        this.pos_y_success = (int)((Screen.height - 10 * this.size) / 2);

        this.successToDisplay = new Queue();
        this.incrementation = 0;
        this.posYpercent = 0;
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");
        this.coude = Resources.Load<Texture2D>("Sprites/Success/Coude");
        this.coude2 = Resources.Load<Texture2D>("Sprites/Success/Coude2");
        this.coude3 = Resources.Load<Texture2D>("Sprites/Success/Coude3");
        this.coude4 = Resources.Load<Texture2D>("Sprites/Success/Coude4");
        this.hLine = Resources.Load<Texture2D>("Sprites/Success/HLine");
        this.vLine = Resources.Load<Texture2D>("Sprites/Success/VLine");

        this.activate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if ((successToDisplay.Count > 0 || ActualSucces != null) && !gameObject.GetComponent<Controller>().Loading)
        {
            if (incrementation == 0)
            {
                if (successToDisplay.Count > 0)
                {
                    ActualSucces = (Success)successToDisplay.Dequeue();
                    incrementation = 2;
                }
                else
                    ActualSucces = null;
            }
            else
            {
                posYpercent += incrementation;
                if (incrementation < 0 && posYpercent <= 0)
                    incrementation = 0;
                else if (posYpercent > 150)
                    incrementation = -3;
            }
        }
    }
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        if (incrementation != 0)
        {
            Rect rect = new Rect(Screen.width / 20, -Screen.height / 10 + (Mathf.Clamp(posYpercent, 0, 100) / 100) * Screen.height / 9, Screen.width / 5, Screen.height / 9);
            GUI.Box(rect, "", skin.GetStyle("Inventory"));
            rect.width /= 2;
            rect.x += Screen.width / 100 + Screen.width / 14;
            rect.y += Screen.height / 100;
            rect.height -= Screen.height / 50;
            rect.width -= Screen.width / 50;
            GUI.Box(rect, TextDatabase.Achievment.GetText(), skin.GetStyle("Description"));
            rect.x -= Screen.width / 14;
            rect.width = rect.height;
            GUI.DrawTexture(rect, ActualSucces.Icon);
        }
        if (activate)
        {
            this.size = Screen.width / 25;
            this.pos_x_success = (Screen.width - 12 * this.size) / 2;
            this.pos_y_success = (int)((Screen.height - 10 * this.size) / 2);
            DrawInterface();
        }
    }

    private void DrawInterface()
    {
        Rect rect = new Rect(this.pos_x_success - 24, this.pos_y_success - 32, 12 * this.size + 48, 10 * this.size + 48);
        GUI.Box(rect, "", this.skin.GetStyle("inventory"));
        if (Event.current.button == 0 && Event.current.type == EventType.MouseDrag)
        {
            this.move_x += Input.GetAxis("Mouse X") * 10;
            this.move_y -= Input.GetAxis("Mouse Y") * 10;
        }
        Text tooltip = null;

        Queue file = new Queue();
        file.Enqueue(SuccessDatabase.Root);
        while (file.Count > 0)
        {
            Success node = (Success)file.Dequeue();
            Rect rect2 = new Rect(node.PosX * this.size + 6 * this.size + this.pos_x_success + this.move_x, node.PosY * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
            Texture icon = node.Icon;
            if (node.Achived)
            {
                foreach (Success son in node.Sons)
                    if (son.IsSeen)
                    {
                        file.Enqueue(son);
                        int x = node.PosX;
                        int y = node.PosY;

                        if (y == son.PosY)
                            while (x != son.PosX)
                            {
                                x += (son.PosX - x) > 0 ? 1 : -1;
                                Rect rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                                if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                    GUI.DrawTexture(rect3, this.hLine);
                            }
                        else if (x == son.PosX)
                            while (y != son.PosY)
                            {
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                Rect rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                                if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                    GUI.DrawTexture(rect3, this.vLine);
                            }
                        else
                        {
                            Texture c = GetCoude(true, x, y, son);
                            x += (son.PosX - x) > 0 ? 1 : -1;
                            Rect rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                            if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                GUI.DrawTexture(rect3, c);

                            while (Math.Abs(y - son.PosY) > 1)
                            {
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                                if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                    GUI.DrawTexture(rect3, this.vLine);
                            }
                            if (x == son.PosX)
                            {
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                                if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                    GUI.DrawTexture(rect3, this.vLine);
                            }
                            else
                            {
                                c = GetCoude(false, x, y, son);
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                                if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                    GUI.DrawTexture(rect3, c);
                                while (x != son.PosX)
                                {
                                    x += (son.PosX - x) > 0 ? 1 : -1;
                                    rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success + this.move_x, y * this.size + 5 * this.size + this.pos_y_success + this.move_y, this.size, this.size);
                                    if (rect3.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect3.x > -6 * this.size + 6 * this.size + this.pos_x_success
                                     && rect3.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect3.y > -6 * this.size + 6 * this.size + this.pos_y_success)
                                        GUI.DrawTexture(rect3, this.hLine);
                                }
                            }
                        }
                    }
            }
            else
                icon = node.Shadow;
            if (rect2.x < 5 * this.size + 6 * this.size + this.pos_x_success && rect2.x > -6 * this.size + 6 * this.size + this.pos_x_success
                && rect2.y < 2 * this.size + 6 * this.size + this.pos_y_success && rect2.y > -6 * this.size + 6 * this.size + this.pos_y_success)
            {
                GUI.Box(rect2, "", this.skin.GetStyle("slot"));
                rect2.x += this.size / 5;
                rect2.y += this.size / 5;
                rect2.width -= this.size / 2.5f;
                rect2.height -= this.size / 2.5f;
                GUI.DrawTexture(rect2, icon);
            }
            if (rect2.Contains(Event.current.mousePosition))
                tooltip = node.Description;
        }
        if (tooltip != null)
            GUI.Box(new Rect(Event.current.mousePosition.x - Screen.width / 20, Event.current.mousePosition.y + Screen.height / 20, 100, 35 + 20 * (tooltip.GetText().Length / 15 + 1)),
                           tooltip.GetText(), this.skin.GetStyle("Skin"));
    }

    private Texture GetCoude(bool firstX, int x, int y, Success son)
    {
        if (firstX)
        {
            if (son.PosX - x > 0 && son.PosY - y < 0)
                return this.coude;
            else if (son.PosX - x < 0 && son.PosY - y < 0)
                return this.coude2;
            else if (son.PosX - x > 0 && son.PosY - y > 0)
                return this.coude4;
            else
                return this.coude3;
        }

        if (son.PosX - x > 0 && son.PosY - y < 0)
            return this.coude3;
        else if (son.PosX - x < 0 && son.PosY - y < 0)
            return this.coude4;
        else if (son.PosX - x > 0 && son.PosY - y > 0)
            return this.coude2;
        else
            return this.coude;
    }

    /// <summary>
    /// Must be server!!!!
    /// Display the achivement on the client.
    /// </summary>
    /// <param name="succes"></param>
    public void Display(Success success)
    {
        RpcDisplay(success.ID);
        if (success.ID == 70 && SceneManager.GetActiveScene().name == "main")
        {
            GameObject.Find("Map").GetComponent<Save>().SaveWorld();
            GameObject.Find("NetworkManager").GetComponent<NetworkManager2>().ServerChangeScene("BossScene");
        }
    }
    [ClientRpc]
    private void RpcDisplay(int id)
    {
        if (!isLocalPlayer)
            return;
        successToDisplay.Enqueue(SuccessDatabase.Find(id));
        if (id % 10 == 0 || id == 62 || id == 71)
            switch (id / 10)
            {
                case 1:
                    gameObject.GetComponent<Tutoriel>().Story(TextDatabase.StoneAgeStory1, TextDatabase.StoneAgeStory2, TextDatabase.StoneAgeStory3, TextDatabase.StoneAgeStory4, TextDatabase.StoneAgeStory5, TextDatabase.StoneAgeStory6, TextDatabase.StoneAgeStory7, TextDatabase.StoneAgeStory8, TextDatabase.StoneAgeStory9, TextDatabase.StoneAgeStory10);
                    break;
                case 2:
                    gameObject.GetComponent<Tutoriel>().Story(TextDatabase.CopperAgeStory1, TextDatabase.CopperAgeStory2, TextDatabase.CopperAgeStory3, TextDatabase.CopperAgeStory4, TextDatabase.CopperAgeStory5, TextDatabase.CopperAgeStory6, TextDatabase.CopperAgeStory7, TextDatabase.CopperAgeStory8);
                    break;
                case 3:
                    gameObject.GetComponent<Tutoriel>().Story(TextDatabase.IronAgeStory1, TextDatabase.IronAgeStory2, TextDatabase.IronAgeStory3, TextDatabase.IronAgeStory4, TextDatabase.IronAgeStory5, TextDatabase.IronAgeStory6);
                    break;
                case 4:
                    gameObject.GetComponent<Tutoriel>().Story(TextDatabase.GoldAgeStory1, TextDatabase.GoldAgeStory2, TextDatabase.GoldAgeStory3, TextDatabase.GoldAgeStory4, TextDatabase.GoldAgeStory5, TextDatabase.GoldAgeStory6, TextDatabase.GoldAgeStory7);
                    break;
                case 5:
                    gameObject.GetComponent<Tutoriel>().Story(TextDatabase.MithrilAgeStory1, TextDatabase.MithrilAgeStory2, TextDatabase.MithrilAgeStory3);
                    break;
                case 6:
                    if (id != 62)
                        gameObject.GetComponent<Tutoriel>().Story(TextDatabase.FloatiumAgeStory1, TextDatabase.FloatiumAgeStory2);
                    else
                        gameObject.GetComponent<Tutoriel>().Story(TextDatabase.PreSunkiumAgeStory);
                    break;
                case 7:
                    if (id == 71)
                        gameObject.GetComponent<Tutoriel>().END();
                    break;
                default:
                    break;
            }
    }

    public void Unlock(Success success)
    {
        RpcUnlock(success.ID);
    }

    [ClientRpc]
    private void RpcUnlock(int id)
    {
        Success succ = SuccessDatabase.Find(id);

        if (!isLocalPlayer || succ.Achived)
            return;

        succ.Achived = true;

        foreach (Success s in succ.Sons)
            s.NbParentsLeft--;

        if (id % 10 == 0)
            switch (id / 10)
            {
                case 1:
                    gameObject.GetComponent<Craft_HUD>().mastered(1, 2, 40, 50, 60, 71, 72, 73, 80, 84);
                    break;
                case 2:
                    gameObject.GetComponent<Craft_HUD>().mastered(4, 5, 10, 16, 41, 51, 61, 90, 100);
                    break;
                case 3:
                    gameObject.GetComponent<Craft_HUD>().mastered(3, 6, 7, 10, 22, 23, 24, 25, 26, 27, 42, 52, 62, 81, 85, 91, 101);
                    break;
                case 4:
                    gameObject.GetComponent<Craft_HUD>().mastered(8, 9, 12, 21, 43, 53, 63, 92, 102);
                    break;
                case 5:
                    gameObject.GetComponent<Craft_HUD>().mastered(13, 44, 54, 64, 82, 86, 93, 103);
                    break;
                case 6:
                    gameObject.GetComponent<Craft_HUD>().mastered(14, 45, 55, 65, 94, 104);
                    break;
                case 7:
                    gameObject.GetComponent<Craft_HUD>().mastered(15, 46, 56, 66, 83, 87, 95, 105, 666);
                    break;
                default:
                    break;
            }
    }

    // Setters & Getters
    public bool Activate
    {
        get { return this.activate; }
        set
        {
            this.activate = value;
            if (!this.activate)
            {
                this.move_x = 0;
                this.move_y = 0;
            }
        }
    }
}
