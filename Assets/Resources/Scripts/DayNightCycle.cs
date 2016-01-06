using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float cycleTime = 60;
    public float Gamma = 0.80f;
    public float NightIntensity = 1f;
    public bool bloodmoon;
    private float actual_time;

    // public Color Test = new Color();


    // Use this for initialization
    void Start()
    {
        actual_time = 0f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Test = SkysColor(actual_time / cycleTime); // teste la couleur

        actual_time = (actual_time + Time.deltaTime) % cycleTime;

        // color settings
        if (actual_time <= cycleTime / 3)
            sun.color = SkysColor(actual_time * 3 / cycleTime);
        else if (actual_time <= 2 * cycleTime / 3)
            sun.color = SkysColor((cycleTime * 2 / 3 - actual_time) * 3 / cycleTime);

        // intensity setting
        float phasedTime = Mathf.Abs((actual_time - cycleTime / 3) % cycleTime) / cycleTime/*dephasage pour avoir le zenith au debut*/;
        sun.intensity = Mathf.Max(NightIntensity, (-4 * phasedTime * phasedTime + 4 * phasedTime) * 8);
        if (bloodmoon & sun.intensity <= NightIntensity)
            sun.color = Color.red;

    }


    // Methods

    private static int[] waveLengthToRGB(int Wavelength, float IntensityMax, float gamma) // de 380 a 780 (le visible)
    {
        float factor;
        float Red, Green, Blue;
        // convertion en couleur
        if ((Wavelength >= 380) && (Wavelength < 440))
        {
            Red = -(Wavelength - 440) / (440 - 380);
            Green = 0.0f;
            Blue = 1.0f;
        }
        else if ((Wavelength >= 440) && (Wavelength < 490))
        {
            Red = 0.0f;
            Green = (Wavelength - 440) / (490 - 440);
            Blue = 1.0f;
        }
        else if ((Wavelength >= 490) && (Wavelength < 510))
        {
            Red = 0.0f;
            Green = 1.0f;
            Blue = -(Wavelength - 510) / (510 - 490);
        }
        else if ((Wavelength >= 510) && (Wavelength < 580))
        {
            Red = (Wavelength - 510) / (580 - 510);
            Green = 1.0f;
            Blue = 0.0f;
        }
        else if ((Wavelength >= 580) && (Wavelength < 645))
        {
            Red = 1.0f;
            Green = -(Wavelength - 645) / (645 - 580);
            Blue = 0.0f;
        }
        else if ((Wavelength >= 645) && (Wavelength < 781))
        {
            Red = 1.0f;
            Green = 0.0f;
            Blue = 0.0f;
        }
        else
        {
            Red = 0.0f;
            Green = 0.0f;
            Blue = 0.0f;
        }

        // Let the intensity fall off near the vision limits
        if ((Wavelength >= 380) && (Wavelength < 420))
        {
            factor = 0.3f + 0.7f * (Wavelength - 380) / (420 - 380);
        }
        else if ((Wavelength >= 420) && (Wavelength < 701))
        {
            factor = 1.0f;
        }
        else if ((Wavelength >= 701) && (Wavelength < 781))
        {
            factor = 0.3f + 0.7f * (780 - Wavelength) / (780 - 700);
        }
        else factor = 0.0f;

        // gestion du retour

        int[] rgb = new int[3];
        // Don't want 0^x = 1 for x <> 0
        rgb[0] = (Red == 0.0) ? 0 : (int)Mathf.Round(IntensityMax * Mathf.Pow(Red * factor, gamma));
        rgb[1] = (Green == 0.0) ? 0 : (int)Mathf.Round(IntensityMax * Mathf.Pow(Green * factor, gamma));
        rgb[2] = (Blue == 0.0) ? 0 : (int)Mathf.Round(IntensityMax * Mathf.Pow(Blue * factor, gamma));

        return rgb;
    }

    public Color SkysColor(float t)
    {
        int r, g, b;
        List<int[]> colors = new List<int[]>();
        for (int i = 380; i < 781; i += 10)
        {
            float x = (i - 380) / 400f;
            colors.Add(waveLengthToRGB(i,/*ceci  prend une valeur entre 0 et 255*/((3.25f * t - 4f) * x * x + (-4f * t + 4f) * x) * 255f, Gamma));
        }
        r = 0;
        g = 0;
        b = 0;
        foreach (int[] color in colors)  // prend les couleur les plus visible
        {
            if (color[0] > r)
                r = color[0];
            if (color[1] > g)
                g = color[1];
            if (color[2] > b)
                b = color[2];
        }
        return new Color32((byte)r, (byte)g, (byte)b, 255);
    }

    // getters setters
    public float time
    {
        get { return this.actual_time; }
    }
}
