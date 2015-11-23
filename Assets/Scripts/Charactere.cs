using UnityEngine;
using System.Collections;

public class Charactere : MonoBehaviour
{

    protected int Health;
    protected int Hunger;

    private bool IsJumping = false;
    private Vector3 LastPosition;
    public GameObject Camera;
    public int HealthMax = 500;
    public int HungerMax = 100;
    public float MoveSpeed = 1f;
    public float JumpForce = 2f;

    // Use this for initialization
    void Start()
    {
        this.Hunger = this.HungerMax;
        this.Health = this.HealthMax;
        this.LastPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool forward = Input.GetButton("Forward");
        bool back = Input.GetButton("Back");
        bool right = Input.GetButton("Right");
        bool left = Input.GetButton("Left");
        bool jump = Input.GetButton("Jump");

        if (jump && !IsJumping)
        {
            this.GetComponent<Rigidbody>().AddForce(0, JumpForce, 0);
            this.IsJumping = true;
        }
        if (IsJumping)
        {
            if (this.LastPosition.y == this.transform.position.y)
            {
                this.IsJumping = false;
            }            
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

        this.LastPosition = this.transform.position;
    }
}
