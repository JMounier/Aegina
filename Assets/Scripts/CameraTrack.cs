using UnityEngine;
using System.Collections;
using System;

public class CameraTrack : MonoBehaviour {

    public GameObject Personnage;    
    public float Distance = 5;
    public float DistanceMin = 0.01f;
    public float DistanceMax = 10;
    public float YMin = 0.15f;
    public float YMax = 1;
    public float YMinFPS = -60;
    public float YMaxFPS = 60;
    public float Sensitivity = 0.1f;
    public float SensitivityScroll = 0.75f;

    private float RotationY = 0F;
    private float RotationX = 0F;
    private Vector3 LastPosition;
	
    void Start()
    {
        this.LastPosition = Personnage.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        // Get new distance
        this.Distance -= Input.mouseScrollDelta.y * this.SensitivityScroll;
        this.Distance = Mathf.Clamp(this.Distance, this.DistanceMin, this.DistanceMax);

        // TPS
        if (this.Distance > this.DistanceMin)
        {
            // Set value
            Vector3 posPersonnage = this.Personnage.transform.position;
            Vector3 posCamera = this.transform.position;

            // Get the drag 
            Vector2 deltaMouse = new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            deltaMouse *= this.Sensitivity * this.Distance;

            // Moove the camera
            if (posCamera.y + deltaMouse.y > this.YMax * this.Distance + posPersonnage.y)
                this.transform.Translate(deltaMouse.x, this.YMax * this.Distance + posPersonnage.y - posCamera.y, 0);
            else if (posCamera.y + deltaMouse.y < this.YMin * this.Distance + posPersonnage.y)
                this.transform.Translate(deltaMouse.x, this.YMin * this.Distance + posPersonnage.y - posCamera.y, 0);
            else
                this.transform.Translate(deltaMouse.x, deltaMouse.y, 0);

            // Adjust the position if the object is moving
            if (this.LastPosition != posPersonnage)
            {
                this.transform.Translate(posPersonnage.x - this.LastPosition.x, 0, posPersonnage.z - this.LastPosition.z, Space.World);
                this.LastPosition = posPersonnage;
            }

            this.transform.LookAt(this.Personnage.transform);

            // Adjust the distance                   
            float distance = (float)Math.Sqrt(Math.Pow(posPersonnage.x - posCamera.x, 2) + Math.Pow(posPersonnage.y - posCamera.y, 2) + Math.Pow(posPersonnage.z - posCamera.z, 2));
            this.transform.Translate(0, 0, distance - this.Distance);
        }

        // FPS
        else
        {
            this.transform.position = this.Personnage.transform.position;
            this.RotationX += Input.GetAxis("Mouse X") * Sensitivity * 100;

            this.RotationY += Input.GetAxis("Mouse Y") * Sensitivity * 100;
            this.RotationY = Mathf.Clamp(this.RotationY, YMinFPS, YMaxFPS);

            transform.localEulerAngles = new Vector3(-this.RotationY, this.RotationX, 0);            
        }               
    }
}