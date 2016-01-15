using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour {

    private Controller controllerScript;

	// Use this for initialization
	void Start () {
        this.controllerScript = gameObject.GetComponentInParent<Controller>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            this.controllerScript.IsJumping = false;
    }
}
