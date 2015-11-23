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
    public float JumpForce = 100f;


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



        if (jump && !this.IsJumping)
        {
            this.GetComponent<Rigidbody>().AddForce(0, JumpForce, 0);
            this.IsJumping = true;
            this.MoveSpeed *= 1.2f;
        }      
        
            float angle = Mathf.Deg2Rad * (360 - this.Camera.transform.rotation.eulerAngles.y);
        if ((right && left) || (!right && !left))
        {
            if (forward && !back)
            {
                this.transform.Translate(-Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
            else if (back && !forward)
            {
                this.transform.Translate(Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, -Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
        }
        else if ((forward && back) || (!forward && !back))
        {
            angle += Mathf.PI / 2;
            if (right && !left)
            {
                this.transform.Translate(Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, -Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
            else if (!right && left)
            {
                this.transform.Translate(-Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
        }
        else if (forward)
        {
            if (right)
            {
                angle -= Mathf.PI / 4;
                this.transform.Translate(-Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
            else
            {
                angle += Mathf.PI / 4;
                this.transform.Translate(-Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
        }
        else
        {
            if (right)
            {
                angle += Mathf.PI / 4;
                this.transform.Translate(Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, -Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
            else
            {
                angle -= Mathf.PI / 4;
                this.transform.Translate(Mathf.Sin(angle) * Time.deltaTime * this.MoveSpeed, 0, -Mathf.Cos(angle) * Time.deltaTime * this.MoveSpeed, Space.World);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && this.IsJumping)
        {
            this.IsJumping = false;
            this.MoveSpeed /= 1.2f;
        }
    }
}
