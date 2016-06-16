using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncCristalAttack : NetworkBehaviour
{
    [SyncVar]
    bool grounded;

    float CD;


    // Use this for initialization
    void Start()
    {
        this.grounded = false;
        int CD = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            this.grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            CD -= Time.fixedDeltaTime;
            if (CD < 0)
            {
                CmdAttack();
                CD = 5;
            }
        }
        else
        {
            this.transform.Translate(new Vector3(0, -1, 0));
        }
    }

    // Command

    [Command]
    void CmdAttack()
    {
        Collider[] cibles = Physics.OverlapSphere(this.transform.position, 5);
        foreach (Collider cible in cibles)
            if (cible.gameObject.tag == "Player")
                cible.GetComponent<SyncCharacter>().ReceiveDamage(50, this.transform.position);
    }

    // Getters Setters

    public bool Grounded
    {
        get { return this.grounded; }
        set { this.grounded = value; }
    }
}
