using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    
    private bool isJumping = true;

    public GameObject camera;

    public float moveSpeed = 5f;
    public float moveSpeedjumping = 6f;
    public float jumpForce = 200f;
        
    // Update is called once per frame
    void Update()
    {
        // Translation du personnage
        bool forward = Input.GetButton("Forward");
        bool back = Input.GetButton("Back");
        bool right = Input.GetButton("Right");
        bool left = Input.GetButton("Left");
        bool jump = Input.GetButton("Jump");

        float direction = 0;

        Vector3 move = new Vector3(0,0,0);

        if (jump && !this.isJumping)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
            this.isJumping = true;
        }      
        
        float angle = Mathf.Deg2Rad * (360 - this.camera.transform.rotation.eulerAngles.y);
        if ((right && left) || (!right && !left))
        {
            if (forward && !back)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                direction = 0;
            }
            else if (back && !forward)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                direction = 180;
            }
        }
        else if ((forward && back) || (!forward && !back))
        {
            angle += Mathf.PI / 2;
            if (right && !left)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                direction = 90;
            }
            else if (!right && left)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                direction = -90;
            }
        }
        else if (forward)
        {
            if (right)
            {
                angle -= Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                direction = 45;
            }
            else
            {
                angle += Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                direction = -45;
            }
        }
        else
        {
            if (right)
            {
                angle += Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                direction = 135;
            }
            else
            {
                angle -= Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                direction = -135;
            }
        }

        if (this.isJumping)
            move *= Time.deltaTime * this.moveSpeedjumping;
        else
            move *= Time.deltaTime * this.moveSpeed;

        gameObject.transform.Translate(move, Space.World);
        this.camera.transform.Translate(move, Space.World);

        // Rotation du personnage
        if (move.x != 0 || move.y != 0 || move.z != 0)
        {
            float angleEul = (gameObject.transform.eulerAngles.y - camera.transform.eulerAngles.y - direction);

            if (angleEul > 2)
            {
                if (angleEul < 180)
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - 4, gameObject.transform.eulerAngles.z);
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + 4, gameObject.transform.eulerAngles.z);

                }
            }
            else if(angleEul < -2)
            {
                if (angleEul < -180)
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - 4, gameObject.transform.eulerAngles.z);
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + 4, gameObject.transform.eulerAngles.z);

                }
            }
            //gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, camera.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && this.isJumping)
        {
            this.isJumping = false;
        }
    }
}
