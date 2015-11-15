using UnityEngine;
using System.Collections;

public class CameraTrack : MonoBehaviour {

    public GameObject Personnage;    
    public float Distance;
    public float DistanceMin;
    public float DistanceMax;
    public float AngleXMin;
    public float AngleXMax;
    
    // Use this for initialization
    void Start ()
    {
        Vector3 positionPersonnage = this.Personnage.transform.position;
        Vector3 positionCamera = new Vector3(positionPersonnage.x, positionPersonnage.y, positionPersonnage.z - this.Distance);
        this.transform.position = positionCamera;

        Quaternion angle = new Quaternion(0,0,0,0);
        this.transform.rotation = angle;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
