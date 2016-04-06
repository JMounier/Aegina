﻿using UnityEngine;
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

    // Cool downs
    private float cdMove = 0;
    private float cdAttack = 0;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.goal = null;
            this.path = new List<Node>();
            this.anim = gameObject.GetComponent<Animator>();
            this.anim.SetInteger("Action", 0);
            this.chunk = gameObject.transform.parent.parent.GetComponent<SyncChunk>();
        }
    }

    // Update is called once per frame
    void Update()
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
            if (d < dist)
            {
                nearPlayer = player;
                dist = d;
            }
        }
        // Check le focus
        if (dist < this.myMob.Vision)
            focus = true;

        // Focus
        if (focus)
        {
            this.cdAttack += Time.deltaTime;
            // Attack
            if (dist < 1f && this.cdAttack > 1 / this.myMob.AttackSpeed)
            {
                this.cdAttack = 0;
                Vector3 viewRot = nearPlayer.transform.FindChild("Character").position - transform.position;
                if (viewRot != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(viewRot);
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 5);
                }
                this.path.Clear();
                this.anim.SetInteger("Action", 3);
                nearPlayer.GetComponent<SyncCharacter>().Life -= this.myMob.Damage;
            }
            // Run to the player
            else if (dist >= 1f && (this.path.Count == 0 || this.cdAttack > 3f))
            {
                this.cdAttack = 1f;
                this.goal = this.chunk.GetComponent<SyncChunk>().MyGraph.GetNode(nearPlayer.transform.FindChild("Character").position);
                if (this.goal != null && this.goal.IsValid)
                    this.path = this.chunk.GetComponent<SyncChunk>().MyGraph.AStarPath(this.node, this.goal, .71f);
                else
                {
                    this.anim.SetInteger("Action", 0);
                    this.focus = false;
                }
            }
        }

        else if (this.path.Count == 0)
        {
            this.anim.SetInteger("Action", 0);
            // Trouve un nouveau but
            this.goal = null;

            float randAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
            this.goal = this.chunk.GetComponent<SyncChunk>().MyGraph.GetNode(new Vector3(Mathf.Cos(randAngle),
                0, Mathf.Sin(randAngle)) * UnityEngine.Random.Range(10f, 30f) + this.transform.position);

            // Calcule le chemin => A*           
            if (this.goal != null && this.goal.IsValid)
                this.path = this.chunk.GetComponent<SyncChunk>().MyGraph.AStarPath(this.node, this.goal, .71f);
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

    /// <summary>
    /// De[;ace le mob en ligne droite vers la position.
    /// </summary>
    /// <param name="pos">La position que le mob doit atteindre.</param>
    private void Move(Vector3 pos)
    {
        Vector3 viewRot = new Vector3(pos.x, gameObject.transform.position.y, pos.z) - transform.position;
        if (viewRot != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(viewRot);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
        if (!this.focus)
        {
            this.anim.SetInteger("Action", 1);
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 180000f * Time.deltaTime * this.myMob.WalkSpeed);
        }
        else
        {
            this.anim.SetInteger("Action", 2);
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 180000f * Time.deltaTime * this.myMob.RunSpeed);
        }
    }

    // Getters & Setters
    public Mob MyMob
    {
        get { return this.myMob; }
        set { this.myMob = value; }
    }
}