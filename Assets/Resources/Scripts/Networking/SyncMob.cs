using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class SyncMob : NetworkBehaviour
{

    private Mob myMob;
    private Vector3 goal;
    private List<Vector3> path;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.goal = gameObject.transform.position;
            this.path = new List<Vector3>();
            this.anim = gameObject.GetComponent<Animator>();
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

        // Objectif atteind             
        if (this.path.Count == 0)
        {
            // Trouve un nouveau but
            Vector3 newGoal = goal;
            bool isPossible = false;
            while (!isPossible)
            {
                float randAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
                newGoal = new Vector3(Mathf.Cos(randAngle), 0, Mathf.Sin(randAngle)) * UnityEngine.Random.Range(10f, 25f) + goal;
                isPossible = PathFinding.isValidPosition(newGoal, .5f, gameObject);
            }
            goal = newGoal;

            // Calcule le chemin => A*
            this.path = PathFinding.AStarPath(gameObject, this.goal);
        }

        // Move the mob
        this.anim.SetInteger("Action", 1);
        Vector3 pos = this.path[0];
        Vector3 viewRot = new Vector3(pos.x, gameObject.transform.position.y, pos.z) - transform.position;
        if (viewRot != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(viewRot);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 180000f * Time.deltaTime * this.myMob.WalkSpeed);
        if (Vector3.Distance(gameObject.transform.position, pos) < .75f)
            this.path.RemoveAt(0);
    }



    // Getters & Setters
    public Mob MyMob
    {
        get { return this.myMob; }
        set { this.myMob = value; }
    }
}