using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Networking;

public class SyncMob : NetworkBehaviour
{

    private Mob myMob;
    private List<Node> path;
    private Animator anim;
    private Node node;
    private Node goal;
    private SyncChunk chunk;

    // Status
    private bool focus = false;
    private bool flee = false;

    // Cool downs
    private float cdMove = 0;
    private float cdAttack = 0;
    private float cdDisable = 0;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.goal = null;
            this.path = new List<Node>();
            this.anim = gameObject.GetComponent<Animator>();
            this.anim.SetInteger("Action", 0);
            if (gameObject.transform.parent == null)
                this.myMob.Life = 0;
            else
                this.chunk = gameObject.transform.parent.parent.GetComponent<SyncChunk>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isServer)
            return;

        if (gameObject.transform.position.y < -10)
            this.myMob.Life = 0;

        /*
                --------------------------
               |    Deplacement du mob    |
                --------------------------
        */
        this.node = chunk.MyGraph.GetNode(gameObject.transform.position);
        if (this.node == null)
        {
            Move(gameObject.transform.position + gameObject.transform.forward);
            return;
        }

        // Recherche du joueur le plus proche
        GameObject nearPlayer = null;
        float dist = float.PositiveInfinity;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            float d = Vector3.Distance(player.transform.FindChild("Character").position, gameObject.transform.position);
            if (d < dist && player.GetComponent<SyncCharacter>().Life > 0)
            {
                nearPlayer = player;
                dist = d;
            }
        }

        // Check le focus
        if (dist < this.myMob.VisionFocus && !focus)
        {
            this.focus = true;
            this.flee = false;
            this.path.Clear();
        }
        if (focus && (nearPlayer == null || nearPlayer.GetComponent<SyncCharacter>().Life <= 0))
        {
            this.focus = false;
            this.path.Clear();
        }
        if (!focus && !flee && dist < this.myMob.VisionFleeing)
        {
            this.flee = true;
            this.path.Clear();
        }
        // Flee
        if (this.flee)
        {
            if (dist > this.myMob.VisionFleeing + 2)
                this.flee = false;
            else if (this.path.Count == 0)
            {
                this.anim.SetInteger("Action", 0);
                // Trouve un nouveau but
                this.goal = null;

                float norme = UnityEngine.Random.Range(1f, 7f);
                this.goal = this.chunk.GetComponent<SyncChunk>().MyGraph.GetNode((this.transform.position - nearPlayer.transform.FindChild("Character").position) / dist * norme + this.transform.position);

                // Calcule le chemin => A*           
                if (this.goal != null && this.goal.IsValid)
                    this.path = this.chunk.GetComponent<SyncChunk>().MyGraph.AStarPath(this.node, this.goal, .71f);
                for (int i = this.path.Count - 1; i > 1; i--)
                    this.path.RemoveAt(i);
            }
        }
        // Focus
        if (this.focus)
        {
            // Attack
            if (dist < 1f && this.cdAttack < 1 / this.myMob.AttackSpeed)
            {
                this.cdAttack += Time.deltaTime;
                this.View(nearPlayer.transform.FindChild("Character").position);
                this.path.Clear();
                this.anim.SetInteger("Action", 3);
            }
            else if (dist < 1f)
            {
                nearPlayer.GetComponent<SyncCharacter>().ReceiveDamage(this.myMob.Damage, new Vector3(), false);
                this.cdAttack = 0;
            }
            // Run to the player
            else if (dist >= 1f && this.path.Count == 0)
            {
                this.cdAttack = 0;
                this.goal = this.chunk.GetComponent<SyncChunk>().MyGraph.GetNode(nearPlayer.transform.FindChild("Character").position);
                if (this.goal != null && this.goal.IsValid)
                {
                    this.path = this.chunk.GetComponent<SyncChunk>().MyGraph.AStarPath(this.node, this.goal, .71f);
                    if (this.path.Count != 0)
                        this.path = new List<Node>() { this.path[0] };
                    else
                        while (this.path.Count == 0)
                            ChooseRandomGoal();
                }
                else
                {
                    this.anim.SetInteger("Action", 0);
                    this.focus = false;
                    this.View(nearPlayer.transform.FindChild("Character").position);
                }
            }
        }
        else if (this.path.Count == 0)
        {
            Transform trap = null;
            foreach (Collider col in Physics.OverlapSphere(gameObject.transform.position, 8))
                if (col.gameObject.name.Contains("Trap"))
                    trap = col.transform.parent;
            if (trap != null && UnityEngine.Random.Range(0f, 1f) > this.myMob.Smart)
            {
                this.goal = this.chunk.GetComponent<SyncChunk>().MyGraph.GetNode(trap.position);
                if (this.goal != null && this.goal.IsValid)
                    this.path = this.chunk.GetComponent<SyncChunk>().MyGraph.AStarPath(this.node, this.goal, .71f);
            }
            else
                ChooseRandomGoal();
        }

        // Move the mob       
        if (this.path.Count > 0)
        {

            this.cdMove += Time.deltaTime;
            this.Move(this.path[0].Position);
            if (this.cdMove >= 1.5f)
            {
                this.path.Clear();
                this.cdMove = 0f;
            }
            else if (this.node == this.path[0])
            {
                this.path.RemoveAt(0);
                this.cdMove = 0;
            }
        }
    }

    // Check if the mob is traped
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Trap"))
        {
            col.transform.parent.gameObject.GetComponent<SyncElement>().Elmt.Life -= 50;
            this.myMob.Life = 0;
        }
    }

    private void ChooseRandomGoal()
    {
        this.anim.SetInteger("Action", 0);
        // Trouve un nouveau but
        this.goal = null;

        this.goal = this.chunk.GetComponent<SyncChunk>().MyGraph.ChoseRandomNode();

        // Calcule le chemin => A*           
        if (this.goal != null && this.goal.IsValid)
            this.path = this.chunk.GetComponent<SyncChunk>().MyGraph.AStarPath(this.node, this.goal, .71f);
    }

    /// <summary>
    /// Deplace le mob en ligne droite vers la position.
    /// </summary>
    /// <param name="pos">La position que le mob doit atteindre.</param>
    private void Move(Vector3 pos)
    {
        if (cdDisable > 0)
        {
            cdDisable -= Time.deltaTime;
        }
        else
        {
            this.View(pos);

            if (!this.focus && !this.flee)
            {
                this.anim.SetInteger("Action", 1);
                gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * this.myMob.WalkSpeed * 2000f, ForceMode.Force);
            }
            else
            {
                this.anim.SetInteger("Action", 2);
                gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * this.myMob.RunSpeed * 2000f, ForceMode.Force);
            }
        }
    }

    /// <summary>
    /// Fait regarder le personnage dans la direction pos.
    /// </summary>
    /// <param name="pos"></param>
    private void View(Vector3 pos)
    {
        Vector3 viewRot = new Vector3(pos.x, gameObject.transform.position.y, pos.z) - transform.position;
        if (viewRot != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(viewRot);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
    }

    // Getters & Setters
    /// <summary>
    /// Fait subir des degat au mob. (Must be server !)
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage, Vector3 knockback)
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(knockback.x * 10000f, 10000f, knockback.z * 10000f));
        this.myMob.Life -= damage;
        if (this.myMob.Life <= 0)
            Stats.AddHunt(this.myMob);
        this.cdDisable = 0.5f;
    }

    /// <summary>
    /// Le mob lier au gameobject. (Must be server !)
    /// </summary>
    public Mob MyMob
    {
        get { return this.myMob; }
        set { this.myMob = value; }
    }
}