using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class Cristal_HUD : NetworkBehaviour
{
    private SyncCore cristal;
    private bool cristal_shown = false;
    private int pos_x;
    private int pos_y;
    private int width;
    private int height;
    private int space;
    private GUISkin skin;
    private Inventory inventory;

    // Use this for initialization
    void Start()
    {
        this.pos_x = Screen.width / 4;
        this.pos_y = Screen.height / 4;
        this.width = Screen.width / 2;
        this.height = Screen.height / 2;
        this.space = Screen.height / 20;
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/Skin");
        this.inventory = GetComponentInParent<Inventory>();
    }
    // Update is called once per frame
    void Update()
    {
        this.pos_x = Screen.width / 4;
        this.pos_y = Screen.height / 4;
        this.width = Screen.width / 2;
        this.height = Screen.height / 2;
        this.space = Screen.height / 25;
    }
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;
        if (this.cristal_shown)
        {
            Draw_cristal();
        }
    }

    /// <summary>
    /// affiche l'interface d'interaction avec le cristal
    /// </summary>
    private void Draw_cristal()
    {
        Rect rect = new Rect(pos_x, pos_y, width, height);
        GUI.Box(rect, "", this.skin.GetStyle("inventory"));
        int j = 0;
        for (int i = 0; i < this.cristal.Needs.Length; i++)
        {
            if (this.cristal.Needs[i].Quantity != 0)
            {
                rect = new Rect(this.pos_x + j % 3 * space + space, this.pos_y + height - 4 * space - 10 + j / 3 * space, space, space);
                GUI.Box(rect, "", this.skin.GetStyle("slot"));

                // Dessin de l'item + quantite
                rect.x += 6;
                rect.y += 6;
                rect.width -= 12;
                rect.height -= 12;
                GUI.DrawTexture(rect, this.cristal.Needs[i].Items.Icon);
                if (this.cristal.Needs[i].Quantity > 1)
                    GUI.Box(rect, this.cristal.Needs[i].Quantity.ToString(), this.skin.GetStyle("quantity"));
                j += 1;
            }
        }
        if (this.cristal.T == Team.Neutre)
        {
            rect = new Rect(this.pos_x + space, this.pos_y + height - 2 * space, 3 * space, space);
            if (GUI.Button(rect, TextDatabase.Activate.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Activate();
                }
            }
        }
        else
        {
            rect = new Rect(this.pos_x + 5 * space, this.pos_y + height - 2 * space, 3 * space-5, space);
            if (GUI.Button(rect, TextDatabase.Upgrade.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Activate();
                }
            }
            rect = new Rect(this.pos_x + 8 * space, this.pos_y + height - 2 * space, 3 * space - 5, space);
            
            if (GUI.Button(rect, TextDatabase.Upgrade.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Activate();
                }
            }

            rect = new Rect(this.pos_x + 11 * space, this.pos_y + height - 2 * space, 3 * space-5, space);
            if (GUI.Button(rect, TextDatabase.Upgrade.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Activate();
                }
            }

            
        }

        for (int i = 0; i < 5; i++)
        {
            rect = new Rect(this.pos_x + 5 * space, this.pos_y + height - (4 + 2 * i) * space + i * space / 2, 3 * space - 5, space);
            GUI.Box(rect, "", this.cristal.LevelAtk >= i + 1 ? skin.GetStyle("Slot") : skin.GetStyle("Inventory"));
        }

        for (int i = 0; i < 5; i++)
        {
            rect = new Rect(this.pos_x + 8 * space, this.pos_y + height - (4 + 2 * i) * space + i * space / 2, 3 * space - 5, space);
            GUI.Box(rect, "", this.cristal.LevelProd >= i + 1 ? skin.GetStyle("Slot") : skin.GetStyle("Inventory"));
        }
        for (int i = 0; i < 5; i++)
        {
            rect = new Rect(this.pos_x + 11 * space, this.pos_y + height - (4 + 2 * i) * space + i * space / 2, 3 * space - 5, space);
            GUI.Box(rect, "", this.cristal.LevelPortal >= i + 1 ? skin.GetStyle("Slot") : skin.GetStyle("Inventory"));
        }
        for (int i = 0; i < 3; i++)
        {
            rect = new Rect(this.pos_x + (5 + 3 * i) * space, this.pos_y + height - 12 * space, 3 * space - 5, space);
            GUI.Box(rect, i == 0 ? TextDatabase.AttackPower.GetText(): i == 1 ? TextDatabase.GrowingPower.GetText() : TextDatabase.PortalPower.GetText(), skin.GetStyle("Tooltip"));
        }
    }

    /// <summary>
    /// Active le cristal au couleurs de la team du joueur
    /// </summary>
    private void Activate()
    {
        this.CmdSetTeam(Team.Blue, this.cristal.gameObject);
        if (this.cristal.LevelTot == 0)
        {
            System.Random ran = new System.Random();
            int a = ran.Next(0, 10);
            if (a == 0)
            {
                this.CmdSetLevelPort(2,this.cristal.gameObject);
            }
            else if (a <= 3)
            {
                this.CmdSetLevelProd(2, this.cristal.gameObject);
            }
            else
            {
                this.CmdSetLevelAtk(2, this.cristal.gameObject);
            }

        }
    }

    // Getters Setters

    /// <summary>
    /// Si l'interface du cristal est affiche
    /// </summary>
    public bool Cristal_shown
    {
        get { return this.cristal_shown; }
        set { this.cristal_shown = value; }
    }

    /// <summary>
    /// Le cristal gerer par le hud (celui actuellement proche du joueur)
    /// </summary>
    public SyncCore Cristal
    {
        get { return this.cristal; }
        set { this.cristal = value; }
    }

    #region Cmd
    [Command]
    public void CmdSetTeam(Team team,GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetTeam(team);
    }
    [Command]
    public void CmdSetLevelProd(int level, GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetLevelProd(level);
    }
    [Command]
    public void CmdSetLevelAtk(int level, GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetLevelAtk(level);
    }
    [Command]
    public void CmdSetLevelPort(int level, GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetLevelPort(level);
    }
    #endregion
}
