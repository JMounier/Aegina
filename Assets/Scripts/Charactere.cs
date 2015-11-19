using UnityEngine;
using System.Collections;

public class Charactere : MonoBehaviour {

    protected int Health;
    public GameObject Camera;
    public int HealthMax = 500;
    public float MoveSpeed = 1f;
    

	// Use this for initialization
	void Start ()
    {
        this.Health = this.HealthMax;
	}
	
	// Update is called once per frame
	void Update ()
    {        
        bool foward = Input.GetButton("foward");
        bool back = Input.GetButton("back");
        if (foward)
        {
            this.transform.Translate(0, 0, this.MoveSpeed, Camera.transform);
        }
        else if (back)
        {
            this.transform.Translate(0, 0, -this.MoveSpeed, Camera.transform);
        }
    }
}
