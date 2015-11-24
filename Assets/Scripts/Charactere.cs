using UnityEngine;
using System.Collections;

public class Charactere : MonoBehaviour
{

    protected int Health;
    protected int Hunger;

    private bool IsJumping = true;

    public GameObject Camera;
    public int HealthMax = 500;
    public int HungerMax = 100;
    public float MoveSpeed = 5f;
    public float MoveSpeedjumping = 6f;
    public float JumpForce = 200f;


    // Use this for initialization
    void Start()
    {
        this.Hunger = this.HungerMax;
        this.Health = this.HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        bool forward = Input.GetButton("Forward");
        bool back = Input.GetButton("Back");
        bool right = Input.GetButton("Right");
        bool left = Input.GetButton("Left");
        bool jump = Input.GetButton("Jump");

        Vector3 move = new Vector3(0,0,0);

        if (jump && !this.IsJumping)
        {
            this.GetComponent<Rigidbody>().AddForce(0, JumpForce, 0);
            this.IsJumping = true;
        }      
        
        float angle = Mathf.Deg2Rad * (360 - this.Camera.transform.rotation.eulerAngles.y);
        if ((right && left) || (!right && !left))
        {
            if (forward && !back)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));                
            }
            else if (back && !forward)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
            }
        }
        else if ((forward && back) || (!forward && !back))
        {
            angle += Mathf.PI / 2;
            if (right && !left)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
            }
            else if (!right && left)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
            }
        }
        else if (forward)
        {
            if (right)
            {
                angle -= Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
            }
            else
            {
                angle += Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
            }
        }
        else
        {
            if (right)
            {
                angle += Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
            }
            else
            {
                angle -= Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
            }
        }

        if (this.IsJumping)
            move *= Time.deltaTime * this.MoveSpeedjumping;
        else
            move *= Time.deltaTime * this.MoveSpeed;

        this.transform.Translate(move, Space.World);
        this.Camera.transform.Translate(move, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && this.IsJumping)
        {
            this.IsJumping = false;
        }
    }
}
