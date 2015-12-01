using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public Light Sun;
    public int CycleTime = 60;
    public float RatioDayNight = 0.5f;
    public float IntensityNight = 0.1f, IntensityDay = 0.75f;
    public Color NightColor, DayColor;

    private float Actual_time;

    // Use this for initialization
    void Start()
    {
        this.Actual_time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Actual_time = (this.Actual_time + Time.deltaTime) % this.CycleTime;
        // changement de skybox
        // this.RatioDayNight;
        this.Sun.intensity = Mathf.Max(this.IntensityNight, this.IntensityNight + (this.IntensityDay - this.IntensityNight) * Mathf.Sin(this.Actual_time / this.CycleTime * Mathf.PI / this.RatioDayNight));
        // FIXME
        // this.Sun.color = (this.NightColor * (Mathf.Sin(this.Actual_time / this.CycleTime * Mathf.PI / this.RatioDayNight) + 1) / 2 + this.DayColor * (1 - (Mathf.Sin(this.Actual_time / this.CycleTime * Mathf.PI / this.RatioDayNight) + 1) / 2)) / 2;
    }
    public float GetTime()
    {
        return this.Actual_time;
    }
}
