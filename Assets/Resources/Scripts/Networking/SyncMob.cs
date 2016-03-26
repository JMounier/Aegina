using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Networking;

public class SyncMob : NetworkBehaviour
{

    private Mob myMob;
    private Vector3 goal;
    private List<Vector3> path;
    private Animator anim;
    private bool focus = false;

    // Cool downs
    private float cdAttack = 0;
    private float cdWait = 60;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.goal = gameObject.transform.position;
            this.path = new List<Vector3>();
            this.anim = gameObject.GetComponent<Animator>();
            this.anim.SetInteger("Action", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;
        /*
                --------------------------
               |    Deplacement du mob    |
                -------------------------- 
        */
        if (this.cdAttack > 0)
            this.cdAttack -= Time.deltaTime;
        if (this.cdWait > -60f)
            this.cdWait -= Time.deltaTime;

        // Recher du joueur le plus proche
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

        // Check si il doit prendre ou lacher l'agro
        if (!this.focus && dist < this.myMob.Vision)
        {
            this.focus = true;
            this.path.Clear();
        }
        else if (this.focus && dist > this.MyMob.Vision)
        {
            this.focus = false;
            this.path.Clear();
            this.cdWait = UnityEngine.Random.Range(-10f, 0f);
            this.anim.SetInteger("Action", 1);
        }

        // Objectif atteind        
        if (dist < 1.2f)
        {
            if (cdAttack <= 0)
            {
                this.anim.SetInteger("Action", 3);
                nearPlayer.GetComponent<SyncCharacter>().Life -= this.myMob.Damage;
                this.cdAttack = this.myMob.AttackSpeed;
            }
            this.path.Clear();
        }

        else if (this.path.Count == 0)
        {
            if (!this.focus && this.cdWait > 0)
            {
                // Trouve un nouveau but
                Vector3 newGoal = Vector3.zero;
                bool isPossible = false;
                while (!isPossible)
                {
                    float randAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
                    newGoal = new Vector3(Mathf.Cos(randAngle), 0, Mathf.Sin(randAngle)) * UnityEngine.Random.Range(10f, 25f) + this.transform.position;
                    isPossible = PathFinding.isValidPosition(newGoal, .5f, gameObject);
                }
                this.goal = newGoal;

                // Calcule le chemin => A*
                this.path = PathFinding.AStarPath(gameObject, this.goal, 1f, 1f);
            }
            else if (this.focus)
            {
                float distance = Vector3.Distance(nearPlayer.transform.FindChild("Character").position, gameObject.transform.position);

                if (distance > 2)
                    this.path = PathFinding.AStarPath(gameObject, nearPlayer.transform.FindChild("Character").position, .5f, 1f, nearPlayer.transform.FindChild("Character").gameObject);
                else
                    Move(nearPlayer.transform.FindChild("Character").position);

                this.cdWait = UnityEngine.Random.Range(20f, 60f);
                this.anim.SetInteger("Action", 2);
            }
            else if (this.cdWait < -20)
            {
                this.cdWait = UnityEngine.Random.Range(20f, 60f);
                this.anim.SetInteger("Action", 1);
            }
        }

        // Move the mob
        if (this.path.Count > 0 && this.cdWait > 0)
        {
            this.Move(this.path[0]);
            if (Vector3.Distance(gameObject.transform.position, this.path[0]) < .5f)
                this.path.RemoveAt(0);
        }
        else if (this.cdWait < 0)
        {
            this.anim.SetInteger("Action", 0);
            this.path.Clear();
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
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (!this.focus)
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 180000f * Time.deltaTime * this.myMob.WalkSpeed);
        else
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 180000f * Time.deltaTime * this.myMob.RunSpeed);
    }

    // Getters & Setters
    public Mob MyMob
    {
        get { return this.myMob; }
        set { this.myMob = value; }
    }
}