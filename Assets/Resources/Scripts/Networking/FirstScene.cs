using UnityEngine;
using System.Collections;

public class FirstScene : MonoBehaviour
{

    [SerializeField]
    private Light sun;

    private int diameter = 100;
    private float actual_time;
    private float cycleTime = 120;

    [SerializeField]
    private GameObject campCameraPos;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float camSpeed = 0.05f;

    [SerializeField]
    private GameObject Path;
    [SerializeField]
    private float acceptance = 1;

    private GameObject step;
    private GameObject fistStep;


    [SerializeField]
    private AudioSource source;

    private AudioClip clipMenu;
    private AudioClip clipButton;
    private float cdMusic;
    private float volume;

    private bool onChar;

    private bool goingback;
    private Vector3 backpos;
    private Quaternion backrot;
    private GameObject camAim;
    float speed;
    // Use this for initialization
    void Start()
    {
        this.onChar = false;
        this.goingback = false;
        // D/N
        //this.sun = gameObject.GetComponentInChildren<Light>();
        this.sun.gameObject.transform.TransformPoint(sun.transform.position);

        this.actual_time = 0f;
        this.cdMusic = 0f;
        this.volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);
        this.fistStep = this.Path.transform.GetChild(0).gameObject;
        this.step = this.fistStep;
        // Camera and Path
        for (int i = 1; i < this.Path.transform.childCount; i++)
        {
            this.step.GetComponent<FSPath>().NextStep = Path.transform.GetChild(i).gameObject;
            this.step = Path.transform.GetChild(i).gameObject;
        }

        this.step.GetComponent<FSPath>().NextStep = this.fistStep;
        this.step = this.fistStep;
        this.cam.transform.position = this.step.transform.position;
        this.step = this.step.GetComponent<FSPath>().NextStep;
        this.cam.transform.LookAt(this.step.transform);
        this.camAim = this.campCameraPos.transform.GetChild(0).gameObject;

        this.backpos = this.cam.transform.position;
        this.backrot = this.cam.transform.rotation;
        // Audio Source
        this.source.volume = this.volume;
        this.clipMenu = Resources.Load<AudioClip>("Sounds/Music/Menu");
        this.clipButton = Resources.Load<AudioClip>("Sounds/Button/Button");
    }

    // Update is called once per frame
    void Update()
    {
        // D/N
        this.actual_time = (this.actual_time + Time.deltaTime) % this.cycleTime;
        // intensity setting
        this.sun.intensity = -4 * (this.actual_time % this.cycleTime / this.cycleTime * 2) * (this.actual_time % this.cycleTime / this.cycleTime * 2) + 4 * (this.actual_time % this.cycleTime / this.cycleTime * 2);
        // position du soleil
        this.sun.transform.position = Orbit(this.actual_time);
        this.sun.transform.LookAt(gameObject.transform);

        // Sound 
        this.cdMusic -= Time.deltaTime;
        if (this.cdMusic <= 0)
        {
            this.source.PlayOneShot(this.clipMenu, 1f);
            this.cdMusic = 112f;
        }

        if (this.onChar)
        {
            if (Vector3.Distance(this.cam.transform.position, this.camAim.transform.position) > this.acceptance * this.speed / 1.2f )
            {
                this.cam.transform.rotation = Quaternion.Lerp(this.cam.transform.rotation, this.camAim.transform.rotation, 0.08f);
                cam.transform.Translate((this.camAim.transform.position - cam.transform.position).normalized * this.speed, Space.World);
            }
            else
            {
                this.speed = 0.05f;
                Quaternion lastrot = this.cam.transform.rotation;
                this.cam.transform.LookAt(this.camAim.transform.GetChild(0));
                Quaternion newrot = this.cam.transform.rotation;
                this.cam.transform.rotation = Quaternion.Lerp(lastrot, newrot, 0.1f);
            }
        }
        else if (this.goingback)
        {
            this.speed = 1.2f;
            if (Vector3.Distance(this.cam.transform.position, this.backpos) > this.acceptance)
            {
                cam.transform.Translate((this.backpos - cam.transform.position).normalized * 1.2f, Space.World);
                this.cam.transform.rotation = Quaternion.Lerp(this.cam.transform.rotation, this.backrot, 0.1f);
            }
            else
                this.goingback = false;
        }
        else
        {
            // Camera

            cam.transform.Translate(Vector3.forward * this.camSpeed);

            //choose the rot;
            Quaternion lastrot = this.cam.transform.rotation;
            this.cam.transform.LookAt(this.step.transform);
            Quaternion newrot = this.cam.transform.rotation;
            this.cam.transform.rotation = Quaternion.Lerp(lastrot, newrot, 0.01f);

            if (Vector3.Distance(this.cam.transform.position, this.step.transform.position) <= this.acceptance)
            {
                this.step = this.step.GetComponent<FSPath>().NextStep;
                if (this.step.GetComponent<FSPath>() == null)
                    this.step = this.fistStep;
            }
            this.backpos = this.cam.gameObject.transform.position;
            this.backrot = this.cam.gameObject.transform.rotation;
        }
    }


    private Vector3 Orbit(float time)
    {
        float x = Mathf.Cos(time / this.cycleTime * 2 * Mathf.PI);
        float y = Mathf.Sin(time / this.cycleTime * 2 * Mathf.PI);
        Vector3 pos1 = new Vector3(x * this.diameter, y * this.diameter, 0);
        return pos1;
    }

    public float Volume
    {
        get { return this.volume; }
        set
        {
            this.volume = Mathf.Clamp(value, 0f, 1f);
            this.source.volume = this.volume;
        }
    }

    public bool OnChar
    {
        get { return this.onChar; }
        set
        {
            this.onChar = value;
            this.goingback = !value;
        }
    }

    public void CameraAim(int aim)
    {
        this.camAim = this.campCameraPos.transform.GetChild(aim).gameObject;
    }    

    public void PlayButtonSound()
    {
        this.source.PlayOneShot(this.clipButton, 1f);
    }
}
