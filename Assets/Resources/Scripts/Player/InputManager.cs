﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour
{

    private Controller controller;
    private Inventory inventaire;
    private Menu menu;
    private Social social;
    private Cristal_HUD cristalHUD;

    private Sound soundAudio;


    private GameObject character;
    private SyncElement nearElement;

    // Use this for initialization
    void Start()
    {
        this.nearElement = null;

        if (!isLocalPlayer)
            return;
        this.character = GetComponentInChildren<CharacterCollision>().gameObject;
        this.inventaire = GetComponent<Inventory>();
        this.menu = GetComponent<Menu>();
        this.controller = GetComponent<Controller>();
        this.social = GetComponent<Social>();
        this.cristalHUD = GetComponent<Cristal_HUD>();

        this.soundAudio = gameObject.GetComponentInChildren<Sound>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!isLocalPlayer)
            return;

        // recherche du plus proche
        float dist = this.nearElement == null ? 0 : Vector3.Distance(this.nearElement.transform.position, this.character.transform.position);
        foreach (Collider col in Physics.OverlapSphere(character.transform.position, 7))
            if (col.transform.parent != null && col.transform.parent.CompareTag("Elements") && (this.nearElement == null || Vector3.Distance(this.character.transform.position, col.transform.parent.transform.position) < dist))
                this.nearElement = col.transform.parent.gameObject.GetComponent<SyncElement>();

        if (Input.GetButtonDown("Inventory") && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown && !this.cristalHUD.Cristal_shown)
        {
            this.inventaire.InventoryShown = !this.inventaire.InventoryShown;
            this.controller.Pause = !this.controller.Pause;
            this.soundAudio.PlaySound(AudioClips.Bag, 1f);
            Cursor.visible = !Cursor.visible;
        }

        if (Input.GetKeyDown(KeyCode.Return) && !this.menu.MenuShown && !this.menu.OptionShown && !this.inventaire.InventoryShown && !this.cristalHUD.Cristal_shown)
        {
            this.social.ChatShown = true;
            this.controller.Pause = true;
        }

        if (Input.GetButtonDown("Fire2") && !this.inventaire.InventoryShown && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown)
        {
            Debug.Log(this.nearElement.Elmt);
            if (this.inventaire.UsedItem.Items is Consumable)
                (this.inventaire.UsedItem.Items as Consumable).Consume();

            else if (this.nearElement != null && this.nearElement.Elmt is IslandCore)
            {
                this.cristalHUD.Cristal_shown = true;
                this.controller.Pause = true;
                Cursor.visible = true;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            this.soundAudio.PlaySound(AudioClips.Button, 1f);

            if (this.cristalHUD.Cristal_shown)
            {
                this.cristalHUD.Cristal_shown = false;
                this.controller.Pause = false;
                Cursor.visible = false;
            }
            else if (this.inventaire.InventoryShown)
            {
                this.inventaire.InventoryShown = false;
                this.controller.Pause = false;
                Cursor.visible = false;
            }
            else if (this.social.ChatShown)
            {
                this.social.ChatShown = false;
                this.controller.Pause = false;
                Cursor.visible = false;
            }
            else if (this.menu.OptionShown)
            {
                this.menu.OptionShown = false;
                this.menu.MenuShown = true;
            }
            else if (this.menu.SonShown)
            {
                this.menu.SonShown = false;
                this.menu.OptionShown = true;
            }
            else if (this.menu.LangueShown)
            {
                this.menu.LangueShown = false;
                this.menu.OptionShown = true;
            }
            else
            {
                this.menu.MenuShown = !this.menu.MenuShown;
                this.controller.Pause = !this.controller.Pause;
                Cursor.visible = !Cursor.visible;
            }
        }

        // Gere la barre d'outil.
        if (Input.GetKeyDown(KeyCode.Alpha1))
            this.inventaire.Cursors = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            this.inventaire.Cursors = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            this.inventaire.Cursors = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4))
            this.inventaire.Cursors = 3;

        if (Input.GetKeyDown(KeyCode.Alpha5))
            this.inventaire.Cursors = 4;

        if (Input.GetKeyDown(KeyCode.Alpha6))
            this.inventaire.Cursors = 5;

        if (Input.GetButtonDown("Drop"))
        {
            if (this.inventaire.UsedItem.Quantity != 0)
            {
                this.inventaire.Drop(new ItemStack(this.inventaire.UsedItem.Items, 1));
                this.inventaire.UsedItem.Quantity--;
                if (inventaire.UsedItem.Quantity == 0)
                    this.inventaire.UsedItem = new ItemStack();
            }
        }
    }  
}
