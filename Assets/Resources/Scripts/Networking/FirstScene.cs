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
	private Camera cam;
	[SerializeField]
	private float camSpeed = 0.05f;

	[SerializeField]
	private GameObject fistStep;
	[SerializeField]
	private float acceptance = 1;

	private GameObject step;



	[SerializeField]
	private AudioSource source;

    private AudioClip clipMenu;
    private AudioClip clipButton;
    private float cdMusic;
    private float volume;


    // Use this for initialization
    void Start()
    {
        // D/N
        //this.sun = gameObject.GetComponentInChildren<Light>();
        this.sun.gameObject.transform.TransformPoint(sun.transform.position);

        this.actual_time = 0f;
        this.cdMusic = 0f;
        this.volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);

		// Camera and Path
		this.cam.transform.position = this.fistStep.transform.position;
		this.step = this.fistStep.GetComponent<FSPath> ().NextStep;
		//this.camSpeed = this.step.GetComponent<FSPath> ().Speed;
		this.cam.transform.LookAt (this.step.transform);

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

		// Camera

		cam.transform.Translate(Vector3.forward * this.camSpeed);

		//choose the rot;
		Quaternion lastrot = this.cam.transform.rotation;
		this.cam.transform.LookAt (this.step.transform);
		Quaternion newrot = this.cam.transform.rotation;
		this.cam.transform.rotation = Quaternion.Lerp (lastrot, newrot, 0.01f);

		if (Vector3.Distance (this.cam.transform.position, this.step.transform.position) <= this.acceptance) {
			this.step = this.step.GetComponent<FSPath> ().NextStep;
			if (this.step.GetComponent<FSPath> () == null)
				this.step = this.fistStep;
			//this.camSpeed = this.step.GetComponent<FSPath> ().Speed;
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
