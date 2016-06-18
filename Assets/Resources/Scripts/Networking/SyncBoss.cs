using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncBoss : NetworkBehaviour
{
    private float life;
    private float cd;
    private Animator anim;
    private int damage;
    private bool fight;
    private AttackType atkType;
    private Vector3 cible;

    public enum AttackType { Idle, Sweep, Slam, Invocation, Elbow };
    // Use this for initialization
    void Start()
    {
        // might change for game balance
        if (isServer)
        {
            this.Restart();
            this.anim = gameObject.GetComponent<Animator>();
            this.cible = new Vector3(0, gameObject.transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        if (this.life <= 0)
            return;
        if (this.cd > 0)
        {
            this.cd -= Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(this.cible - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
        else if (fight && this.atkType == AttackType.Idle)
        {
            // AI Chose atk
            float min = float.PositiveInfinity;
            AttackType atk = AttackType.Invocation;
            this.cible = new Vector3(0, gameObject.transform.position.y, 0);
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                float dist = Vector3.Distance(player.transform.FindChild("Character").position, gameObject.transform.position);
                if (Mathf.Abs(dist - 15f) < 2 && Mathf.Abs(dist - 15f) < min)
                {
                    atk = AttackType.Sweep;
                    min = Mathf.Abs(dist - 15f);
                    this.cible = player.transform.FindChild("Character").position;
                    this.cible.y = gameObject.transform.position.y;
                }
                if (Mathf.Abs(dist - 14f) < 2 && Mathf.Abs(dist - 14f) < min)
                {
                    atk = AttackType.Slam;
                    min = Mathf.Abs(dist - 14f);
                    this.cible = player.transform.FindChild("Character").position;
                    this.cible.y = gameObject.transform.position.y;
                }
                if (Mathf.Abs(dist - 11f) < 2 && Mathf.Abs(dist - 11f) < min)
                {
                    atk = AttackType.Elbow;
                    min = Mathf.Abs(dist - 11f);
                    this.cible = player.transform.FindChild("Character").position;
                    this.cible.y = gameObject.transform.position.y;
                }
            }
            // Make atk           
            switch (atk)
            {
                case AttackType.Sweep:
                    this.atkType = AttackType.Sweep;
                    this.damage = 75;
                    this.cd = 1.5f;
                    this.anim.SetInteger("Action", 3);
                    break;
                case AttackType.Slam:
                    this.atkType = AttackType.Slam;
                    this.damage = 100;
                    this.cd = 3;
                    this.anim.SetInteger("Action", 2);
                    break;
                case AttackType.Invocation:
                    this.atkType = AttackType.Invocation;
                    this.damage = 0;
                    this.cd = 2;
                    this.anim.SetInteger("Action", 4);
                    break;
                case AttackType.Elbow:
                    this.atkType = AttackType.Elbow;
                    this.damage = 125;
                    this.cd = 2;
                    this.anim.SetInteger("Action", 1);
                    break;
                default:
                    break;
            }
        }
        else
        {
            this.atkType = AttackType.Idle;
            this.damage = 0;
            this.cd = 1 + .004f * this.life;
            this.anim.SetInteger("Action", 0);
        }
    }

    public void ShockWave()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<BossFight>().ShockWave();
        }
    }

    public void UseCristal()
    {
        ReceiveDamage(50);
    }

    public void ReceiveDamage(int damage)
    {
        this.life -= Mathf.Clamp(damage, 0, 500);
        if (life == 0)
        {
            this.anim.SetInteger("Action", 5);
            Stats.BossKill = true;
            if (!SuccessDatabase.SunkiumAge.Achived)
                Success.Update(true, true);
        }
    }

    public void Restart()
    {
        this.atkType = AttackType.Idle;
        this.life = 500;
        this.cd = 0;
        this.fight = false;
    }

    #region Getters/Setters
    public float Life
    {
        get { return this.life; }
    }

    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }

    public bool Fight
    {
        get { return this.fight; }
        set { this.fight = value; }
    }

    public AttackType AtkType
    {
        get { return this.atkType; }
        set { this.atkType = value; }
    }

    public Vector3 Cible
    {
        set { this.cible = value; }
    }
    #endregion
}
