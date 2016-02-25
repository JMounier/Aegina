using UnityEngine;
using System.Collections;

public class FirstScene : MonoBehaviour
{
    private Light sun;
    private Camera cam;
    private AudioSource source;
    private AudioClip clipMenu;
    private AudioClip clipButton;
    private float cdMusic;
    private float volume;

    private int diameter = 100;
    private float actual_time;
    private float cycleTime = 120;
    // Use this for initialization
    void Start()
    {
        // D/N
        this.sun = gameObject.GetComponentInChildren<Light>();
        this.sun.gameObject.transform.TransformPoint(sun.transform.position);

        this.actual_time = 0f;
        this.cdMusic = 0f;
        this.volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);

        // Camera
        this.cam = gameObject.GetComponentInChildren<Camera>();

        this.source = gameObject.GetComponentInChildren<AudioSource>();
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

        // Camera
        cam.transform.LookAt(gameObject.transform);
        cam.transform.Translate(Vector3.left / 10);

        // Sound 
        this.cdMusic -= Time.deltaTime;
        if (this.cdMusic <= 0)
        {
            this.source.PlayOneShot(this.clipMenu, 1f);
            this.cdMusic = 112f;
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

    public void PlayButtonSound()
    {
        this.source.PlayOneShot(this.clipButton, 1f);
    }
}
