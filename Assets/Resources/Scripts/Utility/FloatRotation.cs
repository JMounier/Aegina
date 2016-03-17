using UnityEngine;
using System.Collections;

public class FloatRotation : MonoBehaviour {


	void Start(){	
		// rot frize so manual settings
		gameObject.transform.Rotate (Vector3.left, 90f);
	}
	// Update is called once per frame
	void Update () {
		// cristal like rot
		gameObject.transform.Rotate(Vector3.forward, 0.5f);
	}
}
