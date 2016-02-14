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
    private GUISkin skin;
    private Inventory inventory;

    // Use this for initialization
    void Start()
    {
		if (!isLocalPlayer)
			return;
        this.pos_x = Screen.width / 4;
        this.pos_y = Screen.height / 4;
        this.width = Screen.width / 2;
        this.height = Screen.height / 2;
        this.skin = Resources.Load<GUISkin>("Sprites/GUIskin/Skin");
        this.inventory = GetComponentInParent<Inventory>();
        this.cristal = new IslandCore();
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
		for (int i = 0; i < this.cristal.Needs.Length; i++)
        {
			if (this.cristal.Needs[i].Quantity != 0)
            {
                rect = new Rect(this.pos_x + j % 3 * 40 + 40, this.pos_y + j / 3 * 40 + 200, 40, 40);
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
            rect = new Rect(this.pos_x + 40, this.pos_y + 280, 80, 40);
            if (GUI.Button(rect, TextDatabase.Activate.GetText(), this.skin.GetStyle("button")))
            {
				if (this.inventory.InventoryContains(cristal.Needs))
                {
					this.inventory.DeleteItems(cristal.Needs);
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
