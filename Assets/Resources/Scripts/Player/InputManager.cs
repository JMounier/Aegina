using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class InputManager : NetworkBehaviour
{
    private Controller controller;
    private Inventory inventaire;
    private Menu menu;
    private Social_HUD social;
    private Cristal_HUD cristalHUD;
    private Success_HUD sucHUD;
    private Tutoriel tutoriel;

    private Sound soundAudio;
    private Animator anim;
    private SyncCharacter syncCharacter;

    private GameObject character;
    private GameObject cam;
    private GameObject nearElement;

    private float cdConsume = 1f;
    private float cdAttack = 0;

    private bool validplace;
    private bool lastvalidplace;
    public enum TypeAttack { None, Horizontal, Vertical, Aerial, Charge }
    private TypeAttack attack;
    // Use this for initialization
    void Start()
    {
        this.nearElement = null;
        this.character = GetComponentInChildren<CharacterCollision>().gameObject;
        this.social = GetComponent<Social_HUD>();
        this.attack = TypeAttack.None;
        if (!isLocalPlayer)
            return;
        this.cam = GetComponentInChildren<Camera>().gameObject;
        this.inventaire = GetComponent<Inventory>();
        this.menu = GetComponent<Menu>();
        this.controller = GetComponent<Controller>();

        this.sucHUD = GetComponent<Success_HUD>();
        this.cristalHUD = GetComponent<Cristal_HUD>();
        this.tutoriel = GetComponent<Tutoriel>();
        this.anim = gameObject.GetComponent<Animator>();
        this.syncCharacter = gameObject.GetComponent<SyncCharacter>();
        this.validplace = false;
        this.lastvalidplace = false;

        this.soundAudio = gameObject.GetComponent<Sound>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer || this.syncCharacter.Life < 0)
            return;
        // Visibilite et blocage du cursor
        Cursor.visible = this.controller.Pause;
        Cursor.lockState = this.controller.Pause ? CursorLockMode.None : CursorLockMode.Locked;

        if (!this.character.activeInHierarchy)
            return;
        #region Near Element
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
            gameObject.GetComponent<Sound>().PlaySound(AudioClips.Void, 0, 0.2f, 616);
            // Supprimer l'ancien outline
            if (lastNearElement != null)
            {
                foreach (MeshRenderer mr in lastNearElement.GetComponentsInChildren<MeshRenderer>())
                    foreach (Material mat in mr.materials)
                        mat.shader = Shader.Find("Standard");
                this.inventaire.Chest = null;
            }

            // Mettre le nouveau outline
            if (this.nearElement != null)
            {
                foreach (MeshRenderer mr in this.nearElement.GetComponentsInChildren<MeshRenderer>())
                    foreach (Material mat in mr.materials)
                        mat.shader = Shader.Find("Outlined");
                if (this.nearElement.name.Contains("Chest"))
                    this.inventaire.Chest = nearElement.GetComponent<SyncChest>();
            }
        }
        #endregion

        #region Previsu
        if (this.inventaire.UsedItem.Items is WorkTop)
        {
            WorkTop wt = this.inventaire.UsedItem.Items as WorkTop;
            wt.Previsu.transform.position = (this.character.transform.position - this.character.transform.forward);
            wt.Previsu.transform.LookAt(new Vector3(this.character.transform.position.x, wt.Previsu.transform.position.y, this.character.transform.position.z));

            CmdupdateValid(wt.Previsu.transform.position);

            if (validplace != this.lastvalidplace)
            {
                Material mat = Resources.Load<Material>("Models/WorkStations/Materials/Previsu" + (validplace ? 1 : 2));
                foreach (MeshRenderer mesh in wt.Previsu.GetComponentsInChildren<MeshRenderer>())
                {
                    Material[] mats = new Material[mesh.materials.Length];
                    for (int i = 0; i < mats.Length; i++)
                        mats[i] = mat;
                    mesh.materials = mats;
                }
                this.lastvalidplace = validplace;
            }

        }
        #endregion

        #region Gestion Menu
        if (Input.GetButtonDown("Inventory") && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown && !this.cristalHUD.Cristal_shown && !this.sucHUD.Activate && !this.tutoriel.EndTutoShown && !this.tutoriel.Tutoshown && !this.menu.ControlShown)
        {
            this.inventaire.InventoryShown = !this.inventaire.InventoryShown;
            this.controller.Pause = !this.controller.Pause;
            this.soundAudio.PlaySound(AudioClips.Bag, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Return) && !this.menu.MenuShown && !this.menu.OptionShown && !this.inventaire.InventoryShown && !this.cristalHUD.Cristal_shown && !this.sucHUD.Activate && !this.tutoriel.EndTutoShown && !this.tutoriel.Tutoshown && !this.menu.ControlShown)
        {
            this.social.ChatShown = true;
            this.controller.Pause = true;
        }

        if (Input.GetButtonDown("Success") && !this.menu.MenuShown && !this.menu.OptionShown && !this.inventaire.InventoryShown && !this.cristalHUD.Cristal_shown && !this.social.ChatShown && !this.tutoriel.EndTutoShown && !this.tutoriel.Tutoshown && !this.menu.ControlShown)
        {
            this.sucHUD.Activate = !this.sucHUD.Activate;
            this.controller.Pause = this.sucHUD.Activate;
        }
        if (cdAttack <= 0 && (Input.GetKey(KeyCode.C)))
        {
            cdAttack = 1.5f;
            this.controller.IsJumping = true;
            this.character.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 5000, 20000));
        }
        #endregion

        #region Fire2 
        bool useConsumable = false;
        if (Input.GetButton("Fire2") && !this.inventaire.InventoryShown && !this.menu.MenuShown && !this.menu.OptionShown && !this.social.ChatShown && !this.cristalHUD.Cristal_shown && !this.sucHUD.Activate && !this.tutoriel.EndTutoShown && !this.tutoriel.Tutoshown && !this.menu.ControlShown)
        {
            if (this.inventaire.UsedItem.Items is WorkTop)
            {
                WorkTop wt = this.inventaire.UsedItem.Items as WorkTop;
                if (validplace && this.soundAudio.IsReady(615))
                {
                    CmdSpawnElm(wt.ElementID, wt.Previsu.transform.position, wt.Previsu.transform.rotation);
                    this.CmdPut(wt.ID);
                    this.inventaire.UsedItem.Quantity -= 1;
                    if (this.inventaire.UsedItem.Quantity <= 0)
                        this.inventaire.UsedItem = new ItemStack();
                    this.soundAudio.PlaySound(AudioClips.Void, 0, 1, 615);
                }
            }
            else if (this.inventaire.UsedItem.Items is Consumable)
            {
                this.anim.SetInteger("Action", 7);
                this.cdConsume -= Time.deltaTime;
                useConsumable = true;
                if (this.cdConsume < 0)
                {
                    this.cdConsume = 1f;
                    this.anim.SetInteger("Action", 0);
                    Consumable consum = this.inventaire.UsedItem.Items as Consumable;
                    gameObject.GetComponent<SyncCharacter>().Affect(consum.E);
                    this.CmdConsumme(consum.ID);
                    this.inventaire.UsedItem.Quantity--;
                    if (this.inventaire.UsedItem.Quantity == 0)
                        this.inventaire.UsedItem = new ItemStack();
                }
            }
            else if (this.inventaire.UsedItem.Items is Sword)
            {
                if (cdAttack <= 0 && !this.controller.Pause)
                {
                    this.anim.SetInteger("Action", 6);// à remplacer par l'action de l'attaque verticale
                    this.attack = TypeAttack.Horizontal;
                    this.cdAttack = 5f;
                }
            }
            else if (this.nearElement != null && this.nearElement.GetComponent<SyncCore>() != null)
            {
                this.cristalHUD.Cristal = this.nearElement.GetComponent<SyncCore>();
                if (this.cristalHUD.Cristal.Team == this.social.Team || this.cristalHUD.Cristal.Team == Team.Neutre)
                {
                    this.cristalHUD.Cristal_shown = true;
                    this.controller.Pause = true;
                }
            }
            else if (this.nearElement != null && this.nearElement.GetComponent<SyncElement>() != null)
            {
                this.CmdInteractElement(this.nearElement.gameObject, this.inventaire.UsedItem.Items.ID);
            }
        }
        if (!useConsumable)
            this.cdConsume = 1;
        #endregion

        #region Fire1
        // Gestion de l'attaque
        if (cdAttack > 0)
            cdAttack -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && cdAttack <= 0 && !this.controller.Pause)
        {
            this.cdAttack = 5f;
            if (this.controller.IsSprinting)
                this.attack = TypeAttack.Charge;
            else if (this.controller.IsJumping)
                this.attack = TypeAttack.Aerial;
            else
                this.attack = TypeAttack.Vertical;
        }
        if (this.cdAttack > 4.8f)
            this.anim.SetInteger("Action", 6);//a modifié en fonction de l'attaque
        if (this.cdAttack < 4.8f && this.cdAttack > 4.7f)
        {

            if (this.attack == TypeAttack.Charge || this.attack == TypeAttack.Aerial)
                this.cdAttack = 3.5f;
            else
                this.cdAttack = .6f;
            if (this.inventaire.UsedItem.Items is Tool)
            {
                this.soundAudio.PlaySound(AudioClips.playerAttack, 1);
                this.soundAudio.CmdPlaySound(AudioClips.playerAttack, 1);
            }

            // Calcul le damage
            float damage = 0f;
            Item item = this.inventaire.UsedItem.Items;
            if (item is Tool)
                damage = 5f * ((item as Tool).Damage) / 100f;
            else
                damage = 5f;
            CmdAttack(damage, attack);
        }
        #endregion

        #region Cancel
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
            else if (this.sucHUD.Activate)
            {
                this.sucHUD.Activate = false;
                this.controller.Pause = false;
            }
            else if (this.menu.ControlShown)
            {
                this.menu.ControlShown = false;
                this.menu.OptionShown = true;
            }
            else if (this.tutoriel.EndTutoShown)
            {
                this.tutoriel.EndTutoShown = false;
                this.controller.Pause = false;
            }
            else if (this.tutoriel.Tutoshown)
            {
                this.tutoriel.Tutoshown = false;
                this.controller.Pause = false;
            }
            else
            {
                this.menu.MenuShown = !this.menu.MenuShown;
                this.controller.Pause = !this.controller.Pause;
            }
        }
        #endregion

        #region ToolBar
        // Gere la barre d'outil.
        if (!Input.GetKey("left ctrl") && !this.controller.Pause && !this.inventaire.InventoryShown)
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
        #endregion
    }


    [Command]
    private void CmdAttack(float damage, TypeAttack atk)
    {
        SyncChunk actual_chunk = null;
        foreach (Collider col in Physics.OverlapBox(this.character.transform.position, new Vector3(5, 100, 5)))
            if (col.gameObject.name.Contains("Island"))
            {
                actual_chunk = col.transform.parent.GetComponent<SyncChunk>();
                break;
            }

        if (actual_chunk != null && actual_chunk.IsCristal && actual_chunk.Cristal.Team == this.social.Team)
            damage += actual_chunk.Cristal.LevelAtk;

        Collider[] cibles = null;
        if (atk == TypeAttack.Horizontal)
            cibles = Physics.OverlapBox(this.character.transform.position - this.character.transform.forward / 2 + new Vector3(0, 0.5f), new Vector3(0.5f, 0.1f, 0.25f), this.character.transform.rotation);
        else if (atk == TypeAttack.Vertical)
            cibles = Physics.OverlapBox(this.character.transform.position - this.character.transform.forward / 2 + new Vector3(0, 0.5f), new Vector3(0.1f, 0.5f, 0.25f), this.character.transform.rotation);
        else if (atk == TypeAttack.Aerial)
        {
            cibles = Physics.OverlapBox(this.character.transform.position - this.character.transform.forward / 2 + new Vector3(0, -2f), new Vector3(0.1f, 2f, 0.25f), this.character.transform.rotation);
            this.syncCharacter.RpcApplyForce(0, -30000f - this.syncCharacter.Jump, 0);
        }
        else if (atk == TypeAttack.Charge)
        {
            cibles = Physics.OverlapBox(this.character.transform.position - this.character.transform.forward / 2 + new Vector3(0, 0.5f, 1.5f), new Vector3(0.2f, 0.2f, 1.75f), this.character.transform.rotation);
            this.controller.IsJumping = true;
            this.syncCharacter.RpcApplyRelativeForce(0, 5000f, -20000f);
        }
        foreach (Collider cible in cibles)
        {
            bool notacible = false;
            if (cible.gameObject.name == "Character" && cible.gameObject.GetComponentInParent<Social_HUD>().Team != this.social.Team)
                cible.gameObject.GetComponentInParent<SyncCharacter>().ReceiveDamage(damage, -(damage / 10f) * this.character.transform.forward);
            else if (cible.gameObject.tag == "Mob")
                cible.gameObject.GetComponentInParent<SyncMob>().ReceiveDamage(damage, -(damage / 10f) * this.character.transform.forward);
            else if (cible.gameObject.name.Contains("Islandcore"))
                cible.gameObject.GetComponent<SyncCore>().AttackCristal((int)damage, this.social.Team);
            else
                notacible = true;
            if (!notacible && atk == TypeAttack.Horizontal)
                break;
        }
    }

    #region  Interact Element

    /// <summary>
    /// spawn an Element on server.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="obj"></param>
    [Command]
    private void CmdSpawnElm(int id, Vector3 pos, Quaternion rot)
    {
        GameObject chunk = null;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(.25f, 1f, .25f)))
        {
            if (col.name.Contains("Island") && col.name != "IslandCore")
            {
                chunk = col.transform.parent.gameObject;
                break;
            }
        }
        if (chunk != null)
        {
            Element elm = (EntityDatabase.Find(id) as Element);
            elm.Spawn(pos, rot, chunk.transform.FindChild("Elements"), -1);
            elm.Prefab.GetComponent<SyncElement>().UpdateGraph();
        }
    }

    [Command]
    private void CmdupdateValid(Vector3 pos)
    {
        GameObject chunk = null;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(.25f, 1f, .25f)))
        {
            if (col.name.Contains("Island") && col.name != "IslandCore")
            {
                chunk = col.transform.parent.gameObject;
                break;
            }
        }
        if (chunk != null)
        {
            Node n = chunk.GetComponent<SyncChunk>().MyGraph.GetNode(pos);
            RpcUpValid(n != null && n.IsValid);
        }
        else
            RpcUpValid(false);
    }

    [ClientRpc]
    private void RpcUpValid(bool validity)
    {
        if (isLocalPlayer)
            this.validplace = validity;
    }

    [Command]
    private void CmdInteractElement(GameObject element, int toolId)
    {
        if (element == null)
            return;
        Element elmt = element.GetComponent<SyncElement>().Elmt;

        if ((elmt.Tool == Element.DestructionTool.None)
            || (elmt.Tool == Element.DestructionTool.Pickaxe && ItemDatabase.Find(toolId) is Pickaxe)
            || (elmt.Tool == Element.DestructionTool.Axe && ItemDatabase.Find(toolId) is Axe))
            this.RpcDoInteract(elmt.Tool, elmt.DistanceInteract);
    }

    [Command]
    private void CmdDamageElm(GameObject element, float damage)
    {
        element.GetComponent<SyncElement>().Elmt.GetDamage(damage);
    }

    [ClientRpc]
    private void RpcDoInteract(Element.DestructionTool tool, float interactDistance)
    {
        if (!isLocalPlayer || !this.soundAudio.IsReady(616))
            return;

        if (Vector3.Distance(this.character.transform.position, this.nearElement.transform.position) < interactDistance)
        {
            Vector3 or = 2 * this.character.transform.position - this.nearElement.transform.position;
            this.character.transform.LookAt(new Vector3(or.x, this.character.transform.position.y, or.z));
            switch (tool)
            {
                case Element.DestructionTool.Axe:
                    if (this.anim.GetInteger("Action") != 5)
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 0, 0.3f * 4f, 613);
                        this.anim.SetInteger("Action", 5);
                    }
                    else if (soundAudio.IsReady(613))
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 1, 1.25f, 613);
                        this.soundAudio.PlaySound(AudioClips.chopping, 1);
                        this.soundAudio.CmdPlaySound(AudioClips.chopping, 1);
                        this.CmdDamageElm(this.nearElement.gameObject, (this.inventaire.UsedItem.Items as Tool).ChoppingEfficiency / 100 * 7.5f);
                    }
                    break;
                case Element.DestructionTool.Pickaxe:
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
                        this.CmdDamageElm(this.nearElement.gameObject, (this.inventaire.UsedItem.Items as Tool).MiningEfficiency / 100 * 7.5f);
                    }
                    break;
                default:
                    if (this.anim.GetInteger("Action") != 8)
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 0, 0.4f, 614);
                        this.anim.SetInteger("Action", 8);
                    }
                    else if (soundAudio.IsReady(614))
                    {
                        this.soundAudio.PlaySound(AudioClips.Void, 1, 0.6f, 614);
                        this.soundAudio.PlaySound(AudioClips.picking, 1);
                        this.soundAudio.CmdPlaySound(AudioClips.picking, 1);
                        this.CmdDamageElm(this.nearElement.gameObject, int.MaxValue);
                    }
                    break;
            }
            if (this.nearElement == null)
                this.anim.SetInteger("Action", 0);
        }
        else
        {
            this.controller.Objectiv = this.nearElement;
            this.controller.InteractDistance = interactDistance;
        }
    }

    #endregion


    public void IAmDead()
    {
        this.menu.MenuShown = false;
        this.menu.SonShown = false;
        this.menu.OptionShown = false;
        this.cristalHUD.Cristal_shown = false;
        this.inventaire.InventoryShown = false;
        this.social.ChatShown = false;
        this.menu.LangueShown = false;
        this.controller.Pause = true;
    }

    #region Getters/Setters
    [Command]
    private void CmdConsumme(int id)
    {
        Stats.AddUsed(id);
    }

    [Command]
    private void CmdPut(int id)
    {
        Stats.AddPut(id);
    }
    public GameObject NearElement
    {
        get { return this.nearElement; }
    }

    #endregion
}
