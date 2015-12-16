using UnityEngine;
using System.Collections;
using System;

public class CameraTrack : MonoBehaviour {

    public float distance = 5;
    public float distanceMin = 1.3f;
    public float distanceMax = 10;
    public float yMin = 0.15f;
    public float yMax = 0.95f;
    public float yMinFPS = -40;
    public float yMaxFPS = 60;
    public float sensitivity = 5;
    public float sensitivityScroll = 0.75f;
    public Vector3 translateReferentiel = new Vector3(0,0.75f,0);

    private float rotationY = 0F;
    private float rotationX = 0F;
    private Controller controllerScript;
    private GameObject personnage;

    void Start()
    {
        this.personnage = GameObject.Find(gameObject.transform.parent.GetChild(1).name);
        this.controllerScript = personnage.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Get new distance
        this.distance -= Input.mouseScrollDelta.y * this.sensitivityScroll;
        this.distance = Mathf.Clamp(this.distance, this.distanceMin, this.distanceMax);

        // TPS
        if (this.distance > this.distanceMin)
        {
            // Set value
            Vector3 posPersonnage = this.personnage.transform.position;
            Vector3 posCamera = gameObject.transform.position;

            // Get the drag 
            Vector2 deltaMouse = new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            deltaMouse *= this.sensitivity * this.distance * Time.deltaTime;
            
            // Moove the camera
            if (posCamera.y + deltaMouse.y > this.yMax * this.distance + posPersonnage.y)
                gameObject.transform.Translate(deltaMouse.x, this.yMax * this.distance + posPersonnage.y - posCamera.y, 0);
            else if (posCamera.y + deltaMouse.y < this.yMin * this.distance + posPersonnage.y)
                gameObject.transform.Translate(deltaMouse.x, this.yMin * this.distance + posPersonnage.y - posCamera.y, 0);
            else
                gameObject.transform.Translate(deltaMouse.x, deltaMouse.y, 0);
           
            this.transform.LookAt(this.personnage.transform.position + this.translateReferentiel);

            // Adjust the distance                   
            float distance = (float)Math.Sqrt(Math.Pow(posPersonnage.x - posCamera.x, 2) + Math.Pow(posPersonnage.y - posCamera.y, 2) + Math.Pow(posPersonnage.z - posCamera.z, 2));
            this.transform.Translate(0, 0, distance - this.distance);
            this.controllerScript.DoRotationTPS();
        }

        // FPS
        else
        {
            gameObject.transform.position = this.personnage.transform.position + this.translateReferentiel;

            this.rotationX += Input.GetAxis("Mouse X") * this.sensitivity * 100 * Time.deltaTime;

            this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivity * 100 * Time.deltaTime;
            this.rotationY = Mathf.Clamp(this.rotationY, this.yMinFPS, this.yMaxFPS);
        
            gameObject.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0);

            this.controllerScript.DoRotationFPS(this.rotationX);
        }               
    }   
}