using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour
{
    // Children
    private GameObject cam;
    private GameObject character;

    // Components
    private SyncCharacter syncChar;

    // Use for Character
    private int walkSpeed = 9000;
    private int sprintSpeed = 15000;
    private int jumpingBoost = 500;
    private int jumpForce = 15000;
    private float coolDownJump = 0;

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
    private bool ismoving = false;
    private bool isSprinting = false;
    private bool isJumping = false;
    private float cdDisable = 0;
    // Use for Sound
    private Sound soundAudio;

    // Pathfinding
    private InputManager im;
    private GameObject objectiv;
    private float interactDistance;

    // Use for loading
    private bool loading = true;

	//spec mode
	private SpecMode specm;

    // Use this for initialization
    void Start()
    {
        this.anim = gameObject.GetComponent<Animator>();
        this.cam = gameObject.GetComponentInChildren<Camera>().gameObject;
        this.character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
        this.syncChar = gameObject.GetComponent<SyncCharacter>();

        this.im = gameObject.GetComponent<InputManager>();
        this.soundAudio = gameObject.GetComponent<Sound>();

        this.objectiv = null;
        this.loading = true;

		this.specm = gameObject.GetComponent<SpecMode> ();

        if (!isLocalPlayer)
        {
            this.cam.SetActive(false);
            this.character.GetComponent<AudioListener>().enabled = false;
        }

        gameObject.transform.position.Set(0, 10, 0);
    }

    void Update()
    {
        if (isServer)
        {
            int x = (int)Mathf.Round(this.character.transform.position.x / Chunk.Size);
            int y = (int)Mathf.Round(this.character.transform.position.z / Chunk.Size);

            MapGeneration mg = GameObject.Find("Map").GetComponent<MapGeneration>();
            this.loading = mg != null && !mg.isLoaded(x, y);
			gameObject.transform.FindChild("Character").GetComponent<Rigidbody>().useGravity = (!this.loading && this.syncChar.Life > 0) && !this.specm.isSpec;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        /*
             --------------------------
            | Deplacement de la camera |
             -------------------------- 
        */

        // Get new distance
        if (!this.pause)
            if (Input.GetKey("left ctrl"))
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
            this.cam.transform.Translate(deltaMouse.x, Mathf.Clamp(deltaMouse.y, this.yMin * this.distance + posPersonnage.y - posCamera.y, this.yMax * this.distance + posPersonnage.y - posCamera.y), 0);

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
        isSprinting = !this.pause && Input.GetButton("Sprint");
        ismoving = forward || back || right || left || jump;
        Vector3 move = new Vector3(0, 0, 0);

        // Jump
        if (jump && !this.isJumping && this.coolDownJump <= 0)
        {
            this.character.GetComponent<Rigidbody>().AddForce(0, jumpForce + this.syncChar.Jump, 0);
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
        if ((right == left))
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
        else if ((forward == back))
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


        bool isMoving = move.x != 0 || move.z != 0;

        // Apply the moves with the animation
        if (this.isJumping)
        {
            this.objectiv = null;
            this.interactDistance = float.PositiveInfinity;
            anim.SetInteger("Action", 3);
            if (isSprinting)
                move *= this.sprintSpeed + this.jumpingBoost + this.syncChar.Speed;
            else
                move *= this.walkSpeed + this.jumpingBoost + this.syncChar.Speed;
        }
        else
        {
            if (isMoving)
            {
                this.objectiv = null;
                this.interactDistance = float.PositiveInfinity;
                if (isSprinting)
                {
                    move *= this.sprintSpeed + this.syncChar.Speed;
                    this.anim.SetInteger("Action", 2);
                    if (this.soundAudio.IsReady(2))
                    {
                        AudioClips[] runs = new AudioClips[] { AudioClips.Run1, AudioClips.Run2, AudioClips.Run3 };
                        AudioClips runRand = runs[Random.Range(0, runs.Length)];
                        this.soundAudio.PlaySound(runRand, 1f, .2f, 2);
                        this.soundAudio.CmdPlaySound(runRand, 1f);
                    }
                }
                else
                {
                    move *= this.walkSpeed + this.syncChar.Speed;
                    this.anim.SetInteger("Action", 1);
                    if (this.soundAudio.IsReady(1))
                    {
                        AudioClips[] walks = new AudioClips[] { AudioClips.Walk1, AudioClips.Walk2, AudioClips.Walk3 };
                        AudioClips walkRand = walks[Random.Range(0, walks.Length)];
                        this.soundAudio.PlaySound(walkRand, 1f, .325f, 1);
                        this.soundAudio.CmdPlaySound(walkRand, 1f);
                    }
                }
            }
            else if (this.objectiv != null)
            {
                Vector3 pos = this.objectiv.transform.position;

                this.anim.SetInteger("Action", 1);
                Vector3 viewRot = new Vector3(-pos.x, this.character.transform.position.y, -pos.z) - new Vector3(-this.character.transform.position.x, this.character.transform.position.y, -this.character.transform.position.z);
                if (viewRot != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(viewRot);
                    this.character.transform.rotation = Quaternion.Lerp(this.character.transform.rotation, targetRotation, Time.deltaTime * 5);
                }

                move = -this.character.transform.forward;
                move *= this.walkSpeed + this.syncChar.Speed;

                if (Vector3.Distance(this.character.transform.position, pos) < this.interactDistance)
                {
                    this.objectiv = null;
                    this.interactDistance = float.PositiveInfinity;
                }
            }
            else if (this.anim.GetInteger("Action") <= 3 || !Input.GetButton("Fire2") || this.im.NearElement == null)
                this.anim.SetInteger("Action", 0);
        }

        if (this.cdDisable > 0)
            this.cdDisable -= Time.deltaTime;
        else
        {
            this.character.GetComponent<Rigidbody>().velocity = new Vector3(0, this.character.GetComponent<Rigidbody>().velocity.y, 0);
            this.character.GetComponent<Rigidbody>().AddForce(move);
            this.cam.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.cam.GetComponent<Rigidbody>().AddForce(move);

            if (isMoving)
            {
                Vector3 rotCam = new Vector3(this.character.transform.eulerAngles.x, this.cam.transform.eulerAngles.y + rotation, this.character.transform.eulerAngles.z);
                this.character.transform.rotation = Quaternion.Lerp(this.character.transform.rotation, Quaternion.Euler(rotCam), Time.deltaTime * 5);
            }
        }
    }

    // Setters | Getters
    public float Sensitivity
    {
        get { return this.sensitivity; }
        set { this.sensitivity = value; }
    }
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
    /// <summary>
    /// Gets a value indicating whether this instance ismoving.
    /// </summary>
    /// <value><c>true</c> if this instance ismoving; otherwise, <c>false</c>.</value>
    public bool Ismoving
    {
        get { return this.ismoving; }
    }

    public bool IsSprinting
    {
        get { return this.isSprinting; }
    }

    /// <sumary>
    /// La vitesse de marche du personnage.
    /// </sumary>
    public int WalkSpeed
    {
        get { return this.walkSpeed; }
        set { this.walkSpeed = value; }
    }

    /// <sumary>
    /// La vittesse de course du personnage.
    /// </sumary>
    public int SprintSpeed
    {
        get { return this.sprintSpeed; }
        set { this.sprintSpeed = value; }
    }

    /// <sumary>
    /// Le gain de vitesse du personnage lorsqu'il saute.
    /// </sumary>
    public int JumpingBoost
    {
        get { return this.jumpingBoost; }
        set { this.jumpingBoost = value; }
    }

    /// <sumary>
    /// La force du saut du personnage.
    /// </sumary>
    public int JumpForce
    {
        get { return this.jumpForce; }
        set { this.jumpForce = value; }
    }

    public GameObject Objectiv
    {
        get { return this.objectiv; }
        set { this.objectiv = value; }
    }

    public float InteractDistance
    {
        get { return this.interactDistance; }
        set { this.interactDistance = value; }
    }

    public bool Loading
    {
        get { return isServer && this.loading; }
    }

    public float CdDisable
    {
        set { this.cdDisable = value; }
        get { return this.cdDisable; }
    }
}
