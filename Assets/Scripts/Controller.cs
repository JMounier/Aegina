using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour
{

    private bool isJumping = true;
    private Animator anim;
    private bool isMoving = false;
    private float rotation = 0f;

    private GameObject cam;
    private GameObject character;

    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;
    public float jumpingBoost = .5f;
    public float jumpForce = 200f;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.tag == "MainCamera")
                this.cam = child.gameObject;
            
            else if (child.tag == "Character")
                this.character = child.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)        
            return;
        
        // Translation du personnage
        bool forward = Input.GetButton("Forward");
        bool back = Input.GetButton("Back");
        bool right = Input.GetButton("Right");
        bool left = Input.GetButton("Left");
        bool jump = Input.GetButton("Jump");
        bool sprint = Input.GetButton("Sprint");
        
        Vector3 move = new Vector3(0, 0, 0);

        if (jump && !this.isJumping)
        {
            this.character.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
            this.isJumping = true;
            Debug.Log("OK");
        }

        float angle = Mathf.Deg2Rad * (360 - this.cam.transform.rotation.eulerAngles.y);
        if ((right && left) || (!right && !left))
        {
            if (forward && !back)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                this.rotation = 0;
            }
            else if (back && !forward)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                this.rotation = 180;
            }
        }
        else if ((forward && back) || (!forward && !back))
        {
            angle += Mathf.PI / 2;
            if (right && !left)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                this.rotation = 90;
            }
            else if (!right && left)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                this.rotation = -90;
            }
        }
        else if (forward)
        {
            if (right)
            {
                angle -= Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                this.rotation = 45;
            }
            else
            {
                angle += Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                this.rotation = -45;
            }
        }
        else
        {
            if (right)
            {
                angle += Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                this.rotation = 135;
            }
            else
            {
                angle -= Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                this.rotation = -135;
            }
        }


        this.isMoving = move.x != 0 || move.z != 0;

        if (this.isJumping)
        {
            if (sprint)
                move *= Time.deltaTime * (this.sprintSpeed + this.jumpingBoost);
            else
                move *= Time.deltaTime * (this.walkSpeed + this.jumpingBoost);
        }
        else
        {
            if (this.isMoving)
            {
                if (sprint)
                {
                    move *= Time.deltaTime * this.sprintSpeed;
                    anim.SetInteger("Action", 2);
                }
                else
                {
                    move *= Time.deltaTime * this.walkSpeed;
                    anim.SetInteger("Action", 1);
                }
            }
            else
                anim.SetInteger("Action", 0);

        }

        this.character.transform.Translate(move, Space.World);
        this.cam.transform.Translate(move, Space.World);
               
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && this.isJumping)
            this.isJumping = false;
    }

    public void DoRotationTPS()
    {
        if (this.isMoving)
        {
            float angleEul = (this.character.transform.eulerAngles.y - cam.transform.eulerAngles.y - this.rotation);

            while (angleEul < 0)
                angleEul += 360;
            while (angleEul > 360)
                angleEul -= 360;

            float delta = Time.deltaTime * 200;
            if (angleEul < 180 && angleEul - delta > 0)
                this.character.transform.eulerAngles = new Vector3(this.character.transform.eulerAngles.x, this.character.transform.eulerAngles.y - delta, this.character.transform.eulerAngles.z);

            else if (angleEul - delta > 0)
                this.character.transform.eulerAngles = new Vector3(this.character.transform.eulerAngles.x, this.character.transform.eulerAngles.y + delta, this.character.transform.eulerAngles.z);

        }

    }

    public void DoRotationFPS(float rotationY)
    {
        gameObject.transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

}
