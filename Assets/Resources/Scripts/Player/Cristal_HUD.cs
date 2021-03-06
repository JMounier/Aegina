﻿using UnityEngine;
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
    private int spaceH;
    private GUISkin skin;
    private Inventory inventory;
    private Transform character;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;
        this.pos_x = Screen.width / 3;
        this.pos_y = Screen.height / 4;
        this.width = 17 * Screen.width / 40;
        this.height = Screen.height / 2;
        this.space = Screen.height / 20;
        this.spaceH = Screen.width / 50;
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/Skin");
        this.inventory = GetComponentInParent<Inventory>();
        this.character = this.transform.FindChild("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        this.pos_x = Screen.width / 3;
        this.pos_y = Screen.height / 4;
        this.width = 15 * Screen.width / 40;
        this.height = Screen.height / 2;
        this.space = Screen.height / 25;
        this.spaceH = Screen.width / 40;
        this.skin.GetStyle("Description").fontSize = (int)(0.01 * Screen.width);
        this.skin.GetStyle("button").fontSize = (int)(0.01 * Screen.width);
    }

    void OnGUI()
    {
        if (!isLocalPlayer || !InputManager.seeGUI)
            return;
        if (this.cristal_shown)
            Draw_cristal();
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
            if (this.cristal.Needs[i].Quantity != 0 && cristal.Upgrade < 3)
            {
                rect = new Rect(this.pos_x + j % 3 * spaceH + spaceH, this.pos_y + height - 4 * space - 10 + j / 3 * space, space, space);
                GUI.Box(rect, "", this.skin.GetStyle("slot"));

                // Dessin de l'item + quantite
                rect.x += 6;
                rect.y += 6;
                rect.width -= 12;
                rect.height -= 12;
                GUI.DrawTexture(rect, this.cristal.Needs[i].Items.Icon);
                if (this.cristal.Needs[i].Quantity > 1)
                {
                    rect.x -= 3;
                    rect.y -= 3;
                    rect.width += 6;
                    rect.height += 6;
                    GUI.Box(rect, this.cristal.Needs[i].Quantity.ToString(), this.skin.GetStyle("quantity"));
                }
                j += 1;
            }

        if (this.cristal.T == Team.Neutre)
        {
            rect = new Rect(this.pos_x + spaceH, this.pos_y + height - 3 * space, 3 * space, space);
            if (GUI.Button(rect, TextDatabase.Activate.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Activate();
                }
            }
        }
        else if (this.cristal.Upgrade < 3)
        {
            rect = new Rect(this.pos_x + 5 * spaceH, this.pos_y + height - 3 * space + 5, 3 * spaceH - 5, space + 5);
            if (GUI.Button(rect, TextDatabase.Upgrade.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Upgrade(0);
                    this.cristal.Upgrade += 1;
                }
            }
            rect = new Rect(this.pos_x + 8 * spaceH, this.pos_y + height - 3 * space + 5, 3 * spaceH - 5, space + 5);

            if (GUI.Button(rect, TextDatabase.Upgrade.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Upgrade(1);
                    this.cristal.Upgrade += 1;
                }
            }

            rect = new Rect(this.pos_x + 11 * spaceH, this.pos_y + height - 3 * space + 5, 3 * spaceH - 5, space + 5);
            if ((cristal.LevelPortal < 4 || SuccessDatabase.OtherLVL5.Achived) && GUI.Button(rect, TextDatabase.Upgrade.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(this.cristal.Needs))
                {
                    this.inventory.DeleteItems(this.cristal.Needs);
                    Upgrade(2);
                    this.cristal.Upgrade += 1;
                }
            }

        }

        for (int i = 0; i < 5; i++)
        {
            rect = new Rect(this.pos_x + 5 * spaceH, this.pos_y + height - (4 + 2 * i) * space + i * space / 2, 3 * spaceH - 5, space);
            GUI.Box(rect, "", this.cristal.LevelAtk >= i + 1 ? skin.GetStyle("Slot") : skin.GetStyle("Inventory"));
        }

        for (int i = 0; i < 5; i++)
        {
            rect = new Rect(this.pos_x + 8 * spaceH, this.pos_y + height - (4 + 2 * i) * space + i * space / 2, 3 * spaceH - 5, space);
            GUI.Box(rect, "", this.cristal.LevelProd >= i + 1 ? skin.GetStyle("Slot") : skin.GetStyle("Inventory"));
        }
        for (int i = 0; i < 5; i++)
        {
            rect = new Rect(this.pos_x + 11 * spaceH, this.pos_y + height - (4 + 2 * i) * space + i * space / 2, 3 * spaceH - 5, space);
            GUI.Box(rect, "", this.cristal.LevelPortal >= i + 1 ? skin.GetStyle("Slot") : skin.GetStyle("Inventory"));
        }
        for (int i = 0; i < 3; i++)
        {
            rect = new Rect(this.pos_x + (5 + 3 * i) * spaceH, this.pos_y + height - 11 * space - 15, 3 * spaceH - 5, space);
            GUI.Box(rect, "<color=#FFFFFF>" + (i == 0 ? TextDatabase.AttackPower.GetText() : i == 1 ? TextDatabase.GrowingPower.GetText() : TextDatabase.PortalPower.GetText()) + "</color>", skin.GetStyle("Description"));
        }
    }

    /// <summary>
    /// Active le cristal au couleurs de la team du joueur
    /// </summary>
    private void Activate()
    {
        this.CmdSetTeam(gameObject.GetComponent<Social_HUD>().Team, this.cristal.gameObject);
        if (this.cristal.LevelTot == 0)
        {
            int x = (int)Mathf.Round(gameObject.transform.FindChild("Character").position.x / Chunk.Size);
            int y = (int)Mathf.Round(gameObject.transform.FindChild("Character").position.z / Chunk.Size);
            if (x == 0 && y == 0)
                this.CmdSetLevelPort(2, this.cristal.gameObject);
            else
            {
                System.Random ran = new System.Random();
                int a = ran.Next(0, 10);
                if (a == 0)
                {
                    this.CmdSetLevelPort(2, this.cristal.gameObject);
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
        CmdSaveCristal(this.cristal.gameObject);
    }

    private void Upgrade(int stats)
    {
        if (stats == 0)
        {
            this.CmdSetLevelAtk(this.cristal.GetComponent<SyncCore>().LevelAtk + 1, this.cristal.gameObject);
        }
        else if (stats == 1)
        {
            this.CmdSetLevelProd(this.cristal.GetComponent<SyncCore>().LevelProd + 1, this.cristal.gameObject);
        }
        else
        {
            SyncCore crystal = this.cristal.GetComponent<SyncCore>();
            this.CmdSetLevelPort(crystal.LevelPortal + 1, this.cristal.gameObject);
        }
        CmdSaveCristal(this.cristal.gameObject);
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
    public void CmdSetTeam(Team team, GameObject cristal)
    {
        Stats.IncrementCapturedCristal();
        cristal.GetComponent<SyncCore>().CmdSetTeam(team);
    }

    [Command]
    public void CmdSetLevelProd(int level, GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetLevelProd(level);
        Stats.ChangeCristalLevel(0, (uint)level);
    }

    [Command]
    public void CmdSetLevelAtk(int level, GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetLevelAtk(level);
        Stats.ChangeCristalLevel(1, (uint)level);
    }

    [Command]
    public void CmdSetLevelPort(int level, GameObject cristal)
    {
        cristal.GetComponent<SyncCore>().CmdSetLevelPort(level);
        Stats.ChangeCristalLevel(2, (uint)level);
    }

    [Command]
    private void CmdSaveCristal(GameObject cristal)
    {
        ChunkSave cs = GameObject.Find("Map").GetComponent<Save>().LoadChunk((int)Mathf.Round(cristal.transform.position.x / Chunk.Size), (int)Mathf.Round(cristal.transform.position.z / Chunk.Size));
        cs.CristalCaracteristics = new float[8] {
            (int)cristal.GetComponent<SyncCore>().Team,
            cristal.GetComponent<SyncCore>().LevelAtk,
            cristal.GetComponent<SyncCore>().LevelProd,
            cristal.GetComponent<SyncCore>().LevelPortal,
            cristal.GetComponent<SyncCore>().Upgrade,
            cristal.GetComponent<SyncCore>().Life,
            cristal.transform.position.x,
            cristal.transform.position.z,
            };
    }
    #endregion
}
