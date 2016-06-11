using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class Success_HUD : NetworkBehaviour
{
    private int pos_x_success, pos_y_success, size;
    private float incrémentation;
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
        this.incrémentation = 0;
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

        if (successToDisplay.Count > 0 || ActualSucces != null)
        {
            if (incrémentation == 0)
            {
                if (successToDisplay.Count > 0)
                {
                    ActualSucces = (Success)successToDisplay.Dequeue();
                    incrémentation = 2;
                }
                else
                    ActualSucces = null;
            }
            else
            {
                posYpercent += incrémentation;
                if (incrémentation < 0 && posYpercent <= 0)
                    incrémentation = 0;
                else if (posYpercent > 150)
                    incrémentation = -3;
            }
        }
    }
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        if (incrémentation != 0)
        {
            Rect rect = new Rect(Screen.width / 20, -Screen.height / 10 + (Mathf.Clamp(posYpercent, 0, 100) / 100) * Screen.height / 9, Screen.width / 7, Screen.height / 9);
            GUI.Box(rect, "", skin.GetStyle("Inventory"));
            rect.width /= 2;
            rect.x += Screen.width / 100 + Screen.width / 14;
            rect.y += Screen.height / 100;
            rect.height -= Screen.height / 50;
            rect.width -= Screen.width / 50;
            GUI.Box(rect, "Achievement Get", skin.GetStyle("Description"));
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
        Text tooltip = null;

        Queue file = new Queue();
        file.Enqueue(SuccessDatabase.Root);
        while (file.Count > 0)
        {
            Success node = (Success)file.Dequeue();
            Rect rect2 = new Rect(node.PosX * this.size + 6 * this.size + this.pos_x_success, node.PosY * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
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
                                Rect rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                                GUI.DrawTexture(rect3, this.hLine);
                            }
                        else if (x == son.PosX)
                            while (y != son.PosY)
                            {
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                Rect rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                                GUI.DrawTexture(rect3, this.vLine);
                            }
                        else
                        {
                            Texture c = GetCoude(true, x, y, son);
                            x += (son.PosX - x) > 0 ? 1 : -1;
                            Rect rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                            GUI.DrawTexture(rect3, c);

                            while (Math.Abs(y - son.PosY) > 1)
                            {
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                                GUI.DrawTexture(rect3, this.vLine);
                            }
                            if (x == son.PosX)
                            {
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                                GUI.DrawTexture(rect3, this.vLine);
                            }
                            else
                            {
                                c = GetCoude(false, x, y, son);
                                y += (son.PosY - y) > 0 ? 1 : -1;
                                rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                                GUI.DrawTexture(rect3, c);
                                while (x != son.PosX)
                                {
                                    x += (son.PosX - x) > 0 ? 1 : -1;
                                    rect3 = new Rect(x * this.size + 6 * this.size + this.pos_x_success, y * this.size + 5 * this.size + this.pos_y_success, this.size, this.size);
                                    GUI.DrawTexture(rect3, this.hLine);
                                }
                            }
                        }
                    }
            }
            else
                icon = node.Shadow;

            GUI.Box(rect2, "", this.skin.GetStyle("slot"));
            rect2.x += this.size / 5;
            rect2.y += this.size / 5;
            rect2.width -= this.size / 2.5f;
            rect2.height -= this.size / 2.5f;
            GUI.DrawTexture(rect2, icon);
            if (rect2.Contains(Event.current.mousePosition))
                tooltip = node.Description;
        }
        if (tooltip != null)
            GUI.Box(new Rect(Event.current.mousePosition.x - Screen.width / 20, Event.current.mousePosition.y + Screen.height / 20, 100, 35 + 20 * (tooltip.GetText().Length / 35 + 1)),
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
    }

    [ClientRpc]
    private void RpcDisplay(int id)
    {
        if (!isLocalPlayer)
            return;
        Success suc = SuccessDatabase.Find(id);
        if (!isServer)
        {
            suc.Achived = true;
            foreach (Success sucSon in suc.Sons)
                sucSon.NbParentLeft--;
        }
        successToDisplay.Enqueue(suc);
    }

    // Setters & Getters
    public bool Activate
    {
        get { return this.activate; }
        set { this.activate = value; }
    }
}
