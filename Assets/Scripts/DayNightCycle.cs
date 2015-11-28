using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{

    public int CycleTime = 1000;
    public float RatioDayNight = 0.5f;
    public Cubemap[] ListCubemap = new Cubemap[20];
    public Material MaterialSkybox;

    private float Actual_time;

    // Use this for initialization
    void Start()
    {
        this.Actual_time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Actual_time += Time.deltaTime; // avancement de temps

        // changement de cycle
        if (this.Actual_time >= this.CycleTime)
            this.Actual_time -= this.CycleTime;

        // changement de skybox
        int half = (int)(this.CycleTime * this.RatioDayNight);
        if (this.Actual_time <= half) // jour
        { // Ho - L - M - Hi - P - Hi - M - L - SS - T
            int day = half / 10;
            if (this.Actual_time <= day)                  
            {
                // Ho      
                this.MaterialSkybox.SetInt("_Rotation", 0);
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[0]);
            }
            else if (this.Actual_time <= day * 2)
                // L            
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[1]);
            else if (this.Actual_time <= day * 3)
                // M 
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[2]);
            else if (this.Actual_time <= day * 4)
                // Hi
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[3]);
            else if (this.Actual_time <= day * 5)
                // P
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[4]);
            else if (this.Actual_time <= day * 6)            
            {
                // Hi
                this.MaterialSkybox.SetInt("_Rotation", 180);
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[5]);
            }
            else if (this.Actual_time <= day * 7)
                // M 
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[6]);
            else if (this.Actual_time <= day * 8)
                // L
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[7]);
            else if (this.Actual_time <= day * 9)
                // SS 
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[8]);
            else
                // T
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[9]);
        }
        else  //nuit
        { // R - L - M - H - P - H - M - L - R - S
            int night = (this.CycleTime - half) / 10;
            if (this.Actual_time <= night)            
                // R
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[10]);            
            else if (this.Actual_time <= half + night * 2)
                // L
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[11]);
            else if (this.Actual_time <= half + night * 3)
                // M 
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[12]);
            else if (this.Actual_time <= half + night * 4)
                // H
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[13]);
            else if (this.Actual_time <= half + night * 5)
                // P
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[14]);
            else if (this.Actual_time <= half + night * 6)
            {
                // H
                this.MaterialSkybox.SetInt("_Rotation", 0);
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[15]);
            }
            else if (this.Actual_time <= half + night * 7)
                // M 
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[16]);
            else if (this.Actual_time <= half + night * 8)
                // L
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[17]);
            else if (this.Actual_time <= half + night * 9)
                // R 
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[18]);
            else
                // S
                this.MaterialSkybox.SetTexture("_Tex", this.ListCubemap[19]);
        }

    }
    public float GetTime()
    {
        return this.Actual_time;
    }
}
