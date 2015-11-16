using UnityEngine;
using System.Collections;
using System;

public class CameraTrack : MonoBehaviour {

    public GameObject Personnage;    
    public float Distance;
    public float DistanceMin;
    public float DistanceMax;
    public float AngleXMin;
    public float AngleXMax;
    public float Sensibility;
    public float SensibilityScroll;

    private Vector3 MousePosition;    

    // Use this for initialization
    void Start ()
    {
        Vector3 positionPersonnage = this.Personnage.transform.position;
        Vector3 positionCamera = new Vector3(positionPersonnage.x, positionPersonnage.y, positionPersonnage.z + this.Distance);
        this.transform.position = positionCamera;
        this.transform.LookAt(this.Personnage.transform);

        this.MousePosition = Input.mousePosition;

    }
	
	// Update is called once per frame
	void Update ()
    {
        this.Distance -= Input.mouseScrollDelta.y * this.SensibilityScroll;
        Vector3 deltaMouse = this.MousePosition - Input.mousePosition;
        deltaMouse *= this.Sensibility * this.Distance;

        this.MousePosition = Input.mousePosition;
        this.transform.Translate(deltaMouse.x, deltaMouse.y, 0);

        this.transform.LookAt(this.Personnage.transform);

        Vector3 posPersonnage = this.Personnage.transform.position;
        Vector3 posCamera = this.transform.position;

        float distance = (float) Math.Sqrt(Math.Pow(posPersonnage.x - posCamera.x, 2) + Math.Pow(posPersonnage.y - posCamera.y, 2) + Math.Pow(posPersonnage.z - posCamera.z, 2));
        this.transform.Translate(0, 0, distance - this.Distance);
    }
}
