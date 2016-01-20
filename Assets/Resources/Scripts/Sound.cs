using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    // Use for sound
    public AudioClip walk;
    public AudioClip run;
    private AudioSource source;
    private float walkSoundDown = 0;
    private float runSoundDown = 0;

    // Use this for initialization
    void Start ()
    {
        this.source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    // Bruit marcher
    public void walkSound()
    {
        if (this.walkSoundDown <= 0)
        {
            float vol = 1.42f;
            this.source.PlayOneShot(this.walk, vol);
            walkSoundDown = 18.972f;
        }
        else
            this.walkSoundDown -= Time.deltaTime;
    }

    //bruit courir 
    public void RunSound()
    {
        if (runSoundDown <= 0)
        {
            float vol = 1.42f;
            this.source.PlayOneShot(this.run, vol);
            this.runSoundDown = 23.510f;
        }
        else
            this.runSoundDown -= Time.deltaTime;
    }    
}
