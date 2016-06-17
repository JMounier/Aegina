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

    public enum AttackType { Idle, Sweep, Slam, Invocation, Elbow};
    // Use this for initialization
    void Start()
    {
        // might change for game balance
        if (isServer)
        {
            this.Restart();
            this.anim = gameObject.GetComponent<Animator>();
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
            this.cd -= Time.deltaTime;

        if (this.cd <= 0 && fight)
        {
            switch ((AttackType)Random.Range(0, 5))
            {
                case AttackType.Idle:
                    this.atkType = AttackType.Idle;
                    this.damage = 0;
                    this.cd = 2;
                    this.anim.SetInteger("Action", 0);
                    break;
                case AttackType.Sweep:
                    this.atkType = AttackType.Sweep;
                    this.damage = 75;
                    this.cd = 2;
                    this.anim.SetInteger("Action", 3);
                    break;
                case AttackType.Slam:
                    this.atkType = AttackType.Slam;
                    this.damage = 100;
                    this.cd = 2;
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
        else if (cd < 0)
        {
            this.atkType = AttackType.Idle;
            this.damage = 0;
            this.cd = 2;
            this.anim.SetInteger("Action", 0);
        }
    }

    public void UseCristal()
    {
        ReceiveDamage(50);
    }

    public void ReceiveDamage(int damage)
    {
        this.life -= damage;
        if (life == 0)
        {
            Stats.BossKill = true;
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
    }

    public bool Fight
    {
        get { return this.fight; }
        set { this.fight = value; }
    }
    #endregion
}
