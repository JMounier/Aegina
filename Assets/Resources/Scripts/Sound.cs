using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{

    public AudioClip walk; 

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f; 

	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (true) // Controller.isMoving je sais pas comment faire pour recuperer ce bool qui est initialisé dans controller
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(walk, vol);
        }
    }
}





