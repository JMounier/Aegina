using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour
{
    // Children
    private GameObject cam;
    private GameObject character;

    // Use for Character
    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;
    public float jumpingBoost = .5f;
    public float jumpForce = 200f;

    private bool isJumping = true;
    private Animator anim;
    private float rotation = 0f;

    // Use for Camera
    public float distance = 5;
    public float distanceMin = 1.3f;
    public float distanceMax = 10;
    public float yMin = 0.15f;
    public float yMax = 0.95f;
    public float yMinFPS = -40;
    public float yMaxFPS = 60;
    public float sensitivity = 5;
    public float sensitivityScroll = 0.75f;
    public Vector3 translateReferentiel = new Vector3(0, 0.75f, 0);

    private float rotationY = 0F;
    private float rotationX = 0F;
    private bool pause = false;
    
    // Use this for initialization
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

        if (!isLocalPlayer)
            this.cam.GetComponent<Camera>().enabled = false;
        else
            this.cam.GetComponent<Camera>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is local
        if (!isLocalPlayer || this.pause)
            return;

        /*
             --------------------------
            | Deplacement de la camera |
             -------------------------- 
        */

        // Get new distance
        this.distance -= Input.mouseScrollDelta.y * this.sensitivityScroll;
        this.distance = Mathf.Clamp(this.distance, this.distanceMin, this.distanceMax);

        // TPS
        if (this.distance > this.distanceMin)
        {
            // Set value
            Vector3 posPersonnage = this.character.transform.position;
            Vector3 posCamera = this.cam.transform.position;

            // Get the drag 
            Vector2 deltaMouse = new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            deltaMouse *= this.sensitivity * this.distance * Time.deltaTime;

            // Moove the camera
            if (posCamera.y + deltaMouse.y > this.yMax * this.distance + posPersonnage.y)
                this.cam.transform.Translate(deltaMouse.x, this.yMax * this.distance + posPersonnage.y - posCamera.y, 0);
            else if (posCamera.y + deltaMouse.y < this.yMin * this.distance + posPersonnage.y)
                this.cam.transform.Translate(deltaMouse.x, this.yMin * this.distance + posPersonnage.y - posCamera.y, 0);
            else
                this.cam.transform.Translate(deltaMouse.x, deltaMouse.y, 0);

            this.cam.transform.LookAt(this.character.transform.position + this.translateReferentiel);

            // Adjust the distance                   
            float distance = (float)Mathf.Sqrt(Mathf.Pow(posPersonnage.x - posCamera.x, 2) + Mathf.Pow(posPersonnage.y - posCamera.y, 2) + Mathf.Pow(posPersonnage.z - posCamera.z, 2));
            this.cam.transform.Translate(0, 0, distance - this.distance);
        }

        // FPS
        else
        {
            this.cam.transform.position = this.character.transform.position + this.translateReferentiel;

            this.rotationX += Input.GetAxis("Mouse X") * this.sensitivity * 100 * Time.deltaTime;

            this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivity * 100 * Time.deltaTime;
            this.rotationY = Mathf.Clamp(this.rotationY, this.yMinFPS, this.yMaxFPS);

            this.cam.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0);
            this.character.transform.eulerAngles = new Vector3(0, this.rotationX, 0);
        }

        /*
             ---------------------------
            | Deplacement du personnage |
             --------------------------- 
        */

        // Recupere les inputs
        bool forward = Input.GetButton("Forward");
        bool back = Input.GetButton("Back");
        bool right = Input.GetButton("Right");
        bool left = Input.GetButton("Left");
        bool jump = Input.GetButton("Jump");
        bool sprint = Input.GetButton("Sprint");

        Vector3 move = new Vector3(0, 0, 0);

        // Jump
        if (jump && !this.isJumping)
        {
            this.character.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
            this.isJumping = true;
        }

        // Moves
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


        bool isMoving = move.x != 0 || move.z != 0;

        // Apply the moves with the animation
        if (this.isJumping)
        {
            if (sprint)
                move *= Time.deltaTime * (this.sprintSpeed + this.jumpingBoost);
            else
                move *= Time.deltaTime * (this.walkSpeed + this.jumpingBoost);
        }
        else
        {
            if (isMoving)
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

        gameObject.transform.Translate(move, Space.World);

        if (isMoving)
            this.DoRotationTPS();
    }

    public void DoRotationTPS()
    {
        float angleEul = (this.character.transform.eulerAngles.y - cam.transform.eulerAngles.y - this.rotation);

        while (angleEul < 0)
            angleEul += 360;
        while (angleEul > 360)
            angleEul -= 360;

        float delta = Time.deltaTime * 300;
        if (angleEul < 180 && angleEul - delta > 0)
            this.character.transform.eulerAngles = new Vector3(this.character.transform.eulerAngles.x, this.character.transform.eulerAngles.y - delta, this.character.transform.eulerAngles.z);

        else if (angleEul - delta > 0)
            this.character.transform.eulerAngles = new Vector3(this.character.transform.eulerAngles.x, this.character.transform.eulerAngles.y + delta, this.character.transform.eulerAngles.z);
    }

    // Setters | Getters

    public bool IsJumping
    {
        get { return this.isJumping; }
        set { this.isJumping = value; }
    }

    public bool Pause
    {
        get { return this.pause; }
        set { this.pause = value; }
    }
}
