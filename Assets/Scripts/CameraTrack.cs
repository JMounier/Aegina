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
        this.MousePosition = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get new distance
        this.Distance -= Input.mouseScrollDelta.y * this.SensibilityScroll;
        this.Distance = Boundary(this.Distance, this.DistanceMin, this.DistanceMax);

        // Get the drag 
        Vector3 deltaMouse = this.MousePosition - Input.mousePosition;
        deltaMouse *= this.Sensibility * this.Distance;
        this.MousePosition = Input.mousePosition;
        
        float angleX = (this.transform.rotation.eulerAngles.x + 180) % 360;
        // Moove the camera
        if ((angleX < AngleXMin && deltaMouse.y < 0) || (angleX > AngleXMax && deltaMouse.y > 0))
           this.transform.Translate(deltaMouse.x, 0, 0);
        else
            this.transform.Translate(deltaMouse.x, deltaMouse.y, 0);

        this.transform.LookAt(this.Personnage.transform);

        // Adjust the distance
        Vector3 posPersonnage = this.Personnage.transform.position;
        Vector3 posCamera = this.transform.position;
        float distance = (float) Math.Sqrt(Math.Pow(posPersonnage.x - posCamera.x, 2) + Math.Pow(posPersonnage.y - posCamera.y, 2) + Math.Pow(posPersonnage.z - posCamera.z, 2));
        this.transform.Translate(0, 0, distance - this.Distance);
    }

    // Check if the value is under or upper the limits
    private float Boundary(float val, float floor, float ceil)
    {
        if (val > ceil) return ceil;
        else if (val < floor) return floor;
        else return val;
    }       
}
