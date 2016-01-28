using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour
{

    private Controller controller;
    private Inventory inventaire;
    private Menu menu;

    private Sound soundAudio;
    private AudioClip soundButton;
    private AudioClip soundBag;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.inventaire = GetComponent<Inventory>();
        this.menu = GetComponent<Menu>();
        this.controller = GetComponent<Controller>();

        this.soundAudio = gameObject.GetComponentInChildren<Sound>();
        this.soundBag = Resources.Load<AudioClip>("Sounds/button/Bag");
        this.soundButton = Resources.Load<AudioClip>("Sounds/button/Button");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetButtonDown("Inventory") && !this.menu.MenuShown && !this.menu.OptionShown)
        {
            this.inventaire.InventoryShown = !this.inventaire.InventoryShown;
            this.controller.Pause = !this.controller.Pause;
            this.soundAudio.PlaySound(this.soundBag, 1f);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            this.soundAudio.PlaySound(this.soundButton, 1f);
            if (this.inventaire.InventoryShown)
            {
                this.inventaire.InventoryShown = false;
                this.controller.Pause = false;
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
            }
        }
        // Gere la barre d'outil.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.inventaire.Cursors = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.inventaire.Cursors = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.inventaire.Cursors = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.inventaire.Cursors = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.inventaire.Cursors = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.inventaire.Cursors = 5;
        }       
    }
    void OnGUI()
    {
        if (isLocalPlayer && !this.inventaire.Draggingitem && Event.current.button == 1 & Event.current.type == EventType.mouseDown)
            this.inventaire.UsingItem();
    }
}
