using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour
{
    // Children
    private GameObject cam;
    private GameObject character;

    // Use for Character
    private float walkSpeed = 2.5f;
    private float sprintSpeed = 6f;
    private float jumpingBoost = .5f;
    private float jumpForce = 16000f;
    private float coolDownJump = 0;

    [SyncVar]
    private bool isJumping = true;
    [SyncVar]
    private bool isMoving = false;
    [SyncVar]
    private bool isSprinting = false;

    private Animator anim;

    // Use for Camera
    private float distance = 5;
    private float distanceMin = 1.3f;
    private float distanceMax = 10;
    private float yMin = 0.15f;
    private float yMax = 0.95f;
    private float yMinFPS = -40;
    private float yMaxFPS = 60;
    private float sensitivity = 5;
    private float sensitivityScroll = 0.75f;
    private Vector3 translateReferentiel = new Vector3(0, 0.75f, 0);

    private float rotationY = 0F;
    private float rotationX = 0F;
    private bool pause = false;

    // Use for Sound
    private Sound soundAudio;

    // Use this for initialization
    void Start()
    {
        this.anim = gameObject.GetComponent<Animator>();
        this.cam = gameObject.GetComponentInChildren<Camera>().gameObject;
        this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;

        this.soundAudio = this.character.GetComponent<Sound>();

        if (!isLocalPlayer)
        {
            this.cam.SetActive(false);
            this.character.GetComponent<AudioListener>().enabled = false;
        }

        gameObject.transform.position.Set(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is local
        if (!isLocalPlayer)
        {           
                if (!this.isJumping && this.isMoving)
                {
                    if (this.isSprinting && this.soundAudio.IsReady(2))
                        this.soundAudio.PlaySound(0.1f, 0.2f, 2, AudioClips.Run1, AudioClips.Run2, AudioClips.Run3);

                    else if (this.soundAudio.IsReady(1))
                        this.soundAudio.PlaySound(0.1f, 0.4f, 1, AudioClips.Walk1, AudioClips.Walk2);
                }          

            return;
        }

        /*
             --------------------------
            | Deplacement de la camera |
             -------------------------- 
        */

        // Get new distance
        if (!this.pause)
            this.distance -= Input.mouseScrollDelta.y * this.sensitivityScroll;
        this.distance = Mathf.Clamp(this.distance, this.distanceMin, this.distanceMax);

        // TPS
        if (this.distance > this.distanceMin)
        {
            // Set value
            Vector3 posPersonnage = this.character.transform.position;
            Vector3 posCamera = this.cam.transform.position;

            // Get the drag 
            Vector2 deltaMouse = Vector2.zero;
            if (!this.pause)
                deltaMouse = new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
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

            if (!this.pause)
            {
                this.rotationX += Input.GetAxis("Mouse X") * this.sensitivity * 100 * Time.deltaTime;
                this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivity * 100 * Time.deltaTime;
            }

            this.rotationY = Mathf.Clamp(this.rotationY, this.yMinFPS, this.yMaxFPS);

            this.cam.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0);
            this.character.transform.eulerAngles = new Vector3(0, this.rotationX + 180, 0);
        }

        /*
             ---------------------------
            | Deplacement du personnage |
             --------------------------- 
        */

        // Recupere les inputs
        bool forward = !this.pause && Input.GetButton("Forward");
        bool back = !this.pause && Input.GetButton("Back");
        bool right = !this.pause && Input.GetButton("Right");
        bool left = !this.pause && Input.GetButton("Left");
        bool jump = !this.pause && Input.GetButton("Jump");
        this.isSprinting = !this.pause && Input.GetButton("Sprint");

        Vector3 move = new Vector3(0, 0, 0);

        // Jump
        if (jump && !this.isJumping && this.coolDownJump <= 0)
        {
            this.character.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
            this.isJumping = true;
            this.coolDownJump = 0.2f;
        }
        else if (!this.isJumping && this.coolDownJump > 0)
        {
            this.coolDownJump -= Time.deltaTime;
        }

        // Moves
        float angle = Mathf.Deg2Rad * (360 - this.cam.transform.rotation.eulerAngles.y);
        int rotation = 0;
        if ((right && left) || (!right && !left))
        {
            if (forward && !back)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                rotation = 180;
            }
            else if (back && !forward)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                rotation = 0;
            }
        }
        else if ((forward && back) || (!forward && !back))
        {
            angle += Mathf.PI / 2;
            if (right && !left)
            {
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                rotation = -90;
            }
            else if (!right && left)
            {
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                rotation = 90;
            }
        }
        else if (forward)
        {
            if (right)
            {
                angle -= Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                rotation = -135;
            }
            else
            {
                angle += Mathf.PI / 4;
                move.Set(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
                rotation = 135;
            }
        }
        else
        {
            if (right)
            {
                angle += Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                rotation = -45;
            }
            else
            {
                angle -= Mathf.PI / 4;
                move.Set(Mathf.Sin(angle), 0, -Mathf.Cos(angle));
                rotation = 45;
            }
        }


        this.isMoving = move.x != 0 || move.z != 0;

        // Apply the moves with the animation
        if (this.isJumping)
        {
            anim.SetInteger("Action", 3);
            if (this.isSprinting)
                move *= Time.deltaTime * (this.sprintSpeed + this.jumpingBoost);
            else
                move *= Time.deltaTime * (this.walkSpeed + this.jumpingBoost);
        }
        else
        {
            if (this.isMoving)
            {
                if (this.isSprinting)
                {
                    move *= Time.deltaTime * this.sprintSpeed;
                    anim.SetInteger("Action", 2);
                    if (this.soundAudio.IsReady(2))
                    {
                        this.soundAudio.PlaySound(0.1f, 0.2f, 2, AudioClips.Run1, AudioClips.Run2, AudioClips.Run3);
                    }
                }
                else
                {
                    move *= Time.deltaTime * this.walkSpeed;
                    anim.SetInteger("Action", 1);
                    if (this.soundAudio.IsReady(1))
                    {
                        this.soundAudio.PlaySound(0.1f, 0.4f, 1, AudioClips.Walk1, AudioClips.Walk2);
                    }
                }
            }
            else
                anim.SetInteger("Action", 0);
        }
        this.CmdPlaySound(this.isMoving, this.isSprinting, this.isJumping);
        gameObject.transform.Translate(move, Space.World);

        if (this.isMoving)
        {
            Vector3 rotCam = new Vector3(this.character.transform.eulerAngles.x, this.cam.transform.eulerAngles.y + rotation, this.character.transform.eulerAngles.z);
            this.character.transform.rotation = Quaternion.Lerp(this.character.transform.rotation, Quaternion.Euler(rotCam), Time.deltaTime * 5);
        }
    }


    /// <sumary>
    /// Informe le serveur de jouer un son.
    /// </sumary>
    [Command]
    private void CmdPlaySound(bool isMoving, bool isSprinting, bool isJumping)
    {
        this.isMoving = isMoving;
        this.isSprinting = isSprinting;
        this.isJumping = isJumping;
    }

    // Setters | Getters

    /// <sumary>
    /// Si le personnage est en saut.
    /// </sumary>
    public bool IsJumping
    {
        get { return this.isJumping; }
        set { this.isJumping = value; }
    }

    /// <sumary>
    /// Si le joueur peut bouger son personnage et la camera.
    /// </sumary>
    public bool Pause
    {
        get { return this.pause; }
        set { this.pause = value; }
    }

    /// <sumary>
    /// La vitesse de marche du personnage.
    /// </sumary>
    public float WalkSpeed
    {
        get { return this.walkSpeed; }
        set { this.walkSpeed = value; }
    }

    /// <sumary>
    /// La vittesse de course du personnage.
    /// </sumary>
    public float SprintSpeed
    {
        get { return this.sprintSpeed; }
        set { this.sprintSpeed = value; }
    }

    /// <sumary>
    /// Le gain de vitesse du personnage lorsqu'il saute.
    /// </sumary>
    public float JumpingBoost
    {
        get { return this.jumpingBoost; }
        set { this.jumpingBoost = value; }
    }

    /// <sumary>
    /// La force du saut du personnage.
    /// </sumary>
    public float JumpForce
    {
        get { return this.jumpForce; }
        set { this.jumpForce = value; }
    }
}
