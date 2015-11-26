using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

    public int CycleTime = 1000;
    public int TimePercent;
    public float Dayratio;
    private float Actual_time;

    // Use this for initialization
    void Start () {
        
        this.Actual_time = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        this.Actual_time += Time.deltaTime; // avancement de temps
        
        // changement de cycle
        if (this.Actual_time >= this.CycleTime)
            this.Actual_time -= this.CycleTime;
        // calcul du pourcentage
        TimePercent = (int) this.Actual_time/(this.CycleTime)* 100;

        // changement de skybox
        int half = (int)(this.CycleTime * this.Dayratio);
        if (this.Actual_time <= half) // jour
        { // Ho - L - M - Hi - P - Hi - M - L - SS - T
            int day = half / 10;
            if (this.Actual_time <= day)
                // Ho
                ;
            else if (this.Actual_time <= day * 2)
                // L
                ;
            else if (this.Actual_time <= day * 3)
                // M 
                ;
            else if (this.Actual_time <= day * 4)
                // Hi
                ;
            else if (this.Actual_time <= day * 5)
                // P
                ;
            else if (this.Actual_time <= day * 6)
                // Hi
                ;
            else if (this.Actual_time <= day * 7)
                // M 
                ;
            else if (this.Actual_time <= day * 8)
                // L
                ;
            else if (this.Actual_time <= day * 9)
                // SS 
                ;
            else
                // T
                ;
        }
        else  //nuit
        { // R - L - M - H - P - H - M - L - R - S
            int night = (this.CycleTime-half)/10;
            if (this.Actual_time <= night)
                // R
                ;
            else if (this.Actual_time <= half + night * 2)
                // L
                ;
            else if (this.Actual_time <= half + night * 3)
                // M 
                ;
            else if (this.Actual_time <= half + night * 4)
                // H
                ;
            else if (this.Actual_time <= half + night * 5)
                // P
                ;
            else if (this.Actual_time <= half + night * 6)
                // H
                ;
            else if (this.Actual_time <= half + night * 7)
                // M 
                ;
            else if (this.Actual_time <= half + night * 8)
                // L
                ;
            else if (this.Actual_time <= half + night * 9)
                // R 
                ;
            else
                // S
                ;

        }
	
	}
    public float GetTime()
    {
        return this.Actual_time;
    }
}
