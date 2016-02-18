using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class Cristal_HUD : NetworkBehaviour
{
    private IslandCore cristal;
    private bool cristal_shown = false;
    private int pos_x;
    private int pos_y;
    private int width;
    private int height;
    private int space;
    private GUISkin skin;
    private ItemStack[] need;
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
        this.cristal = new IslandCore();
        if (cristal.T == Team.Neutre)
        {
            this.need = new ItemStack[3] { new ItemStack(ItemDatabase.Iron, 15), new ItemStack(ItemDatabase.Gold, 1), new ItemStack(ItemDatabase.Copper, 15) };
        }
        else
        {
            this.need = new ItemStack[6] { new ItemStack(ItemDatabase.Iron, 10 * cristal.Level_tot), new ItemStack(ItemDatabase.Gold, 1 * cristal.Level_tot), new ItemStack(ItemDatabase.Copper, 10 * cristal.Level_tot), new ItemStack(ItemDatabase.Floatium, cristal.Level_tot / 2), new ItemStack(ItemDatabase.Mithril, cristal.Level_tot * 2 / 3), new ItemStack(ItemDatabase.Sunkium, cristal.Level_tot / 2) };
        }
    }

    // Update is called once per frame
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
        for (int i = 0; i < this.need.Length; i++)
        {
            if (this.need[i].Quantity != 0)
            {
                rect = new Rect(this.pos_x + j % 3 * space + space, this.pos_y + height - 4 * space - 10 + j/3 * space, space, space);
                GUI.Box(rect, "", this.skin.GetStyle("slot"));

                // Dessin de l'item + quantite
                rect.x += 6;
                rect.y += 6;
                rect.width -= 12;
                rect.height -= 12;
                GUI.DrawTexture(rect, this.need[i].Items.Icon);
                if (this.need[i].Quantity > 1)
                    GUI.Box(rect, this.need[i].Quantity.ToString(), this.skin.GetStyle("quantity"));
                j += 1;
            }
        }
        if (this.cristal.T == Team.Neutre)
        {
            rect = new Rect(this.pos_x + space, this.pos_y + height - 2 * space, 3 * space, space);
            if (GUI.Button(rect, TextDatabase.Activate.GetText(), this.skin.GetStyle("button")))
            {
                if (this.inventory.InventoryContains(need))
                {
                    this.inventory.DeleteItems(need);
                    Activate();
                }
            }
        }
    }

    /// <summary>
    /// Active le cristal au couleurs de la team du joueur
    /// </summary>
    private void Activate()
    {
        this.cristal.T = Team.Blue;
        if (this.cristal.Level_tot == 0)
        {
            System.Random ran = new System.Random();
            int a = ran.Next(0, 10);
            if (a == 0)
            {
                cristal.Level_portal += 2;
            }
            else if (a <= 3)
            {
                cristal.Level_prod += 2;
            }
            else
            {
                cristal.Level_atk += 2;
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
    public IslandCore Cristal
    {
        get { return this.cristal; }
        set { this.cristal = value; }
    }

}
