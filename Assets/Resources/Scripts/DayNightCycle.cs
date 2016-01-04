using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public int cycleTime = 60;
    public float ratioDayNight = 0.5f;
    public float intensityNight = 0.1f, intensityDay = 0.75f;
    public Color nightColor, dayColor;

    private float actual_time;

    // Use this for initialization
    void Start()
    {
        this.actual_time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.actual_time = (this.actual_time + Time.deltaTime) % this.cycleTime;
        this.sun.intensity = Mathf.Max(this.intensityNight, this.intensityNight + (this.intensityDay - this.intensityNight) * Mathf.Sin(this.actual_time / this.cycleTime * Mathf.PI / this.ratioDayNight));
        this.sun.color = (Mathf.Sin(this.actual_time / this.cycleTime * Mathf.PI / this.ratioDayNight)) * dayColor + (1 - Mathf.Sin(this.actual_time / this.cycleTime * Mathf.PI / this.ratioDayNight)) * nightColor;
    }
    public float GetTime()
    {
        return this.actual_time;
    }
}
