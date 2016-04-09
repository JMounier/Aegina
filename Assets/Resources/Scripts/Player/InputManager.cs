using UnityEngine;
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
    private Animator anim;
    private SyncCharacter syncCharacter;

    private GameObject character;
    private GameObject cam;
    private GameObject nearElement;

    private float cdConsume = 1f;
    private float cdAttack = 0;

    // Use this for initialization
    void Start()
    {
        this.nearElement = null;
        this.character = GetComponentInChildren<CharacterCollision>().gameObject;

        if (!isLocalPlayer)
            return;
        this.cam = GetComponentInChildren<Camera>().gameObject;
        this.inventaire = GetComponent<Inventory>();
        this.menu = GetComponent<Menu>();
        this.controller = GetComponent<Controller>();
        this.social = GetComponent<Social>();
        this.cristalHUD = GetComponent<Cristal_HUD>();
        this.anim = gameObject.GetComponent<Animator>();
        this.syncCharacter = gameObject.GetComponent<SyncCharacter>();

        this.soundAudio = gameObject.GetComponent<Sound>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
            return;

        // Recherche du plus proche
        float dist = float.PositiveInfinity;
        GameObject lastNearElement = this.nearElement;
        this.nearElement = null;

        Collider[] cols = Physics.OverlapSphere(this.cam.transform.position, 0.45f);
        GameObject forbidden = null;
        if (cols.Length > 0)
            forbidden = cols[0].gameObject;

        foreach (Collider col in Physics.OverlapSphere(this.character.transform.position, 3.5f))
            if (col.transform.parent != null && col.transform.parent.CompareTag("Elements") && col.gameObject != forbidden
                && Vector3.Distance(this.character.transform.position, col.transform.parent.transform.position) < dist)
            {
                this.nearElement = col.transform.parent.gameObject;
                dist = Vector3.Distance(this.character.transform.position, col.transform.parent.transform.position);
            }


        if (this.nearElement != lastNearElement)
        {
            // Supprimer l'ancien outline
            if (lastNearElement != null)
                foreach (Material mat in lastNearElement.GetComponentInChildren<MeshRenderer>().materials)
                    mat.shader = Shader.Find("Standard");

            // Mettre le nouveau outline
            if (this.nearElement != null)
                foreach (Material mat in this.nearElement.GetComponentInChildren<MeshRenderer>().materials)
                    mat.shader = Shader.Find("Outlined");
        }

        // Gestion Input
        if (Input.GetButtonDown("Inventory") && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown && !this.cristalHUD.Cristal_shown)
        {
            this.inventaire.InventoryShown = !this.inventaire.InventoryShown;
            this.controller.Pause = !this.controller.Pause;
            this.soundAudio.PlaySound(AudioClips.Bag, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Return) && !this.menu.MenuShown && !this.menu.OptionShown && !this.inventaire.InventoryShown && !this.cristalHUD.Cristal_shown)
        {
            this.social.ChatShown = true;
            this.controller.Pause = true;
        }

        bool useConsumable = false;
        if (Input.GetButton("Fire2") && !this.inventaire.InventoryShown && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown)
        {
            if (this.inventaire.UsedItem.Items is Consumable)
            {
                this.anim.SetInteger("Action", 7);
                this.cdConsume -= Time.deltaTime;
                useConsumable = true;
                if (this.cdConsume < 0)
                {
                    this.cdConsume = 0.5f;
                    Consumable consum = this.inventaire.UsedItem.Items as Consumable;
                    switch (consum.E.ET)
                    {
                        case Effect.EffectType.Speed:
                            this.syncCharacter.Speed = consum.E.Power * 2;
                            this.syncCharacter.CdSpeed = consum.E.Power * 30;
                            break;
                        case Effect.EffectType.Slowness:
                            break;
                        case Effect.EffectType.Haste:
                            break;
                        case Effect.EffectType.MiningFatigue:
                            break;
                        case Effect.EffectType.Strength:
                            break;
                        case Effect.EffectType.InstantHealth:
                            this.syncCharacter.Life += 10 * consum.E.Power;
                            break;
                        case Effect.EffectType.InstantDamage:
                            break;
                        case Effect.EffectType.JumpBoost:
                            this.syncCharacter.Jump = consum.E.Power * 5000;
                            this.syncCharacter.CdJump = consum.E.Power * 30;
                            break;
                        case Effect.EffectType.Regeneration:
                            this.syncCharacter.Regen = consum.E.Power;
                            this.syncCharacter.CdRegen = consum.E.Power * 15;
                            break;
                        case Effect.EffectType.Resistance:
                            break;
                        case Effect.EffectType.Hunger:
                            break;
                        case Effect.EffectType.Weakness:
                            break;
                        case Effect.EffectType.Poison:
                            this.syncCharacter.Poison = consum.E.Power;
                            this.syncCharacter.CdPoison = consum.E.Power * 15;
                            break;
                        case Effect.EffectType.Saturation:
                            this.syncCharacter.Hunger += 10 * consum.E.Power;
                            break;
                        case Effect.EffectType.Thirst:
                            break;
                        case Effect.EffectType.Refreshment:
                            this.syncCharacter.Thirst += 10 * consum.E.Power;
                            break;
                        default:
                            break;
                    }
                    this.inventaire.UsedItem.Quantity--;
                    if (this.inventaire.UsedItem.Quantity == 0)
                        this.inventaire.UsedItem = new ItemStack();
                }
            }

            else if (this.nearElement != null && this.nearElement.GetComponent<SyncCore>() != null)
            {
                this.cristalHUD.Cristal = this.nearElement.GetComponent<SyncCore>();
                this.cristalHUD.Cristal_shown = true;
                this.controller.Pause = true;
            }
            else if (this.nearElement != null && this.nearElement.GetComponent<SyncElement>() != null)
                this.CmdInteractElement(this.nearElement.gameObject, this.inventaire.UsedItem.Items.ID);
        }

        if (!useConsumable)
            this.cdConsume = 1;
        //gestion de l'attaque
        if (cdAttack > 0)
            cdAttack -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cdAttack <= 0 && !this.inventaire.InventoryShown && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown)
        {
            float damage = 0f;
            Item item = this.inventaire.UsedItem.Items;
            if (item is Tool)
            {
                damage = 5f * ((item as Tool).Damage) / 100f;
            }
            else
            {
                damage = 5f;
            }
            CmdAttack(damage);
            cdAttack = 1f;
        }
        if (Input.GetButtonDown("Cancel"))
        {
            this.soundAudio.PlaySound(AudioClips.Button, 1f);

            if (this.cristalHUD.Cristal_shown)
            {
                this.cristalHUD.Cristal_shown = false;
                this.controller.Pause = false;
            }
            else if (this.inventaire.InventoryShown)
            {
                this.inventaire.InventoryShown = false;
                this.controller.Pause = false;
            }
            else if (this.social.ChatShown)
            {
                this.social.ChatShown = false;
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

        // Visibilite et blocage du cursor
        Cursor.visible = this.controller.Pause;
        Cursor.lockState = this.controller.Pause ? CursorLockMode.None : CursorLockMode.Locked;

        // Gere la barre d'outil.
        if (!Input.GetKey("left ctrl"))
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                this.inventaire.Cursors++;

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                this.inventaire.Cursors--;
        }

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
    [Command]
    private void CmdAttack(float damage)
    {
        RaycastHit cible = new RaycastHit();
        if (Physics.Raycast(new Vector3(this.character.transform.position.x,7,this.character.transform.position.z), -this.character.transform.forward, out cible, 1))
        {
            if (cible.collider.gameObject.name == "Character")
            {
                //to do : verification de la team
                cible.collider.gameObject.GetComponentInParent<SyncCharacter>().ReceiveDamage(damage);
            }
            else if (cible.collider.gameObject.tag == "Mob")
            {
                cible.collider.gameObject.GetComponentInParent<SyncMob>().ReceiveDamage(damage);
            }
        }
    }
    [Command]
    private void CmdInteractElement(GameObject element, int toolId)
    {
        if (element == null)
            return;
        Element elmt = element.GetComponent<SyncElement>().Elmt;
        if (elmt.Type == Element.TypeElement.Small)
        {
            this.RpcDoInteract(Element.TypeElement.Small);
        }
        else if (elmt.Type == Element.TypeElement.Rock && ItemDatabase.Find(toolId) is Pickaxe)
        {
            this.RpcDoInteract(Element.TypeElement.Rock);
        }
        else if (elmt.Type == Element.TypeElement.Tree && ItemDatabase.Find(toolId) is Axe)
        {
            this.RpcDoInteract(Element.TypeElement.Tree);
        }
    }

    [Command]
    private void CmdDamageElm(GameObject element, float damage)
    {
        element.GetComponent<SyncElement>().Elmt.GetDamage(damage);
    }
    [ClientRpc]
    private void RpcDoInteract(Element.TypeElement type)
    {
        if (!isLocalPlayer)
            return;
        if (Vector3.Distance(this.character.transform.position, this.nearElement.transform.position) < 1.5f)
        {
            switch (type)
            {
                case Element.TypeElement.Tree:
                    if (this.anim.GetInteger("Action") != 5)
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 0, 0.1f, 613);
                        this.anim.SetInteger("Action", 5);
                    }
                    else if (soundAudio.IsReady(613))
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 1, 0.1f, 613);
                        this.soundAudio.PlaySound(AudioClips.chopping, 1);
                        this.soundAudio.CmdPlaySound(AudioClips.chopping, 1);
                        this.CmdDamageElm(this.nearElement.gameObject, (this.inventaire.UsedItem.Items as Tool).ChoppingEfficiency / 100 * 5);

                    }
                    break;
                case Element.TypeElement.Rock:
                    if (this.anim.GetInteger("Action") != 4)
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 0, 0.3f * 4f, 612);
                        this.anim.SetInteger("Action", 4);
                    }
                    else if (soundAudio.IsReady(612))
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 1, 1.25f, 612);
                        this.soundAudio.PlaySound(AudioClips.mining, 1);
                        this.soundAudio.CmdPlaySound(AudioClips.mining, 1);
                        this.CmdDamageElm(this.nearElement.gameObject, (this.inventaire.UsedItem.Items as Tool).MiningEfficiency / 100 * 5);
                    }
                    break;
                default:
                    if (this.anim.GetInteger("Action") != 8)
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 0, 0.2f, 614);
                        this.anim.SetInteger("Action", 8);
                    }
                    else if (soundAudio.IsReady(614))
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 1, 0.5f, 614);
                        this.soundAudio.PlaySound(AudioClips.picking, 1);
                        this.soundAudio.CmdPlaySound(AudioClips.picking, 1);
                        this.CmdDamageElm(this.nearElement.gameObject, int.MaxValue);
                    }
                    break;
            }
            if (this.nearElement == null)
                this.anim.SetInteger("Action", 0);
        }
        else if (true)
        {
            this.controller.Objectiv = this.nearElement;
            this.controller.OType = type;
        }
    }

    #region Getters/Setters
    public GameObject NearElement
    {
        get { return this.nearElement; }
    }
    #endregion
}
