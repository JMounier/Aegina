using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

    public int CycleTime;
    public int TimePercent;
    private float Actual_time;
     

	// Use this for initialization
	void Start () {

        CycleTime = 300;
        this.Actual_time = 0f;

	}
	
	// Update is called once per frame
	void Update () {

        this.Actual_time += Time.deltaTime; // avancement de temps





        // changement de cycle
        if (this.Actual_time >= this.CycleTime)
            this.Actual_time -= CycleTime;
        // calcul du pourcentage
        TimePercent = (int) this.Actual_time/(CycleTime)* 100;
	
	}
}
