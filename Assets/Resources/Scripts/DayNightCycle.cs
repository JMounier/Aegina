using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public Light moon;
    private Color32 defaultmooncolor;
    private float actual_time;
    public float cycleTime = 60;
    public float gamma = 0.80f;
    public float nightIntensity = 0.1f;
    public int diameter = 100;
    public int height = 20;
    public bool bloodmoon;
    // public Color Test = new Color();



    // Use this for initialization
    void Start()
    {
        //  gameObject.GetComponentInChildren<Light>().gameObject.

        // init Astres (moon and sun)
        foreach (Light light in gameObject.transform.GetComponentInParent<Transform>().GetComponentsInChildren<Light>())
        {
            if (light.name == "Sun")
                this.sun = light;
            else if (light.name == "Moon")
                this.moon = light;
            else print("error");
        }
        sun.gameObject.transform.TransformPoint(sun.transform.position);
        moon.gameObject.transform.TransformPoint(moon.transform.position);
        sun.color = SkysColor(0);
        defaultmooncolor = SkysColor(0.70f);
        moon.color = defaultmooncolor;
        moon.intensity = nightIntensity;


        // init time
        this.actual_time = 0f; // a voir si en chargeant un monde on revient pas a t = 0.
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Test = SkysColor(actual_time / cycleTime); // teste la couleur
        actual_time = (actual_time + Time.deltaTime) % cycleTime;

        // float phasedTime = Mathf.Abs((actual_time - cycleTime / 3) % cycleTime) / cycleTime/*dephasage pour avoir le zenith au debut*/;

        /*  var de la couleur en fonction du temps (la skybox le fait tout seul)
        // color settings
        if (actual_time <= cycleTime / 3)
            sun.color = SkysColor((cycleTime * 2 / 3 - actual_time) * 3 / cycleTime);
        else if (actual_time >= 2 * cycleTime / 3)
            sun.color = SkysColor(actual_time * 3 / cycleTime);
        */


        // intensity setting
        sun.intensity = Mathf.Max(nightIntensity, (-4 * (actual_time % cycleTime / cycleTime * 2) * (actual_time % cycleTime / cycleTime * 2) + 4 * (actual_time % cycleTime / cycleTime * 2)) * 1);
        
        // position de la lune et du soleil
        Vector3[] position = Orbit(actual_time);
        sun.transform.position = position[0];
        moon.transform.position = position[1];
        sun.transform.LookAt(gameObject.transform);
        moon.transform.LookAt(gameObject.transform);

        // Blood moon
        if (bloodmoon)
            moon.color = Color.red;
        else if (moon.color != defaultmooncolor)
        {
            moon.color = defaultmooncolor;
        }
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
            colors.Add(waveLengthToRGB(i,/*ceci  prend une valeur entre 0 et 255*/((3.25f * t - 4f) * x * x + (-4f * t + 4f) * x) * 255f, gamma));
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

    public Vector3[] Orbit(float time)
    {
        float x = Mathf.Cos(time / cycleTime * 2 * Mathf.PI);
        float y = Mathf.Sin(time / cycleTime * 2 * Mathf.PI);
        Vector3 pos1 = new Vector3(x * diameter, y * height, 0);
        Vector3 pos2 = new Vector3(-x * diameter, -y * height, 0);
        Vector3[] poss = new Vector3[2];
        poss[0] = pos1;
        poss[1] = pos2;
        return poss;
    }

    // getters setters
    public float time
    {
        get { return this.actual_time; }
    }
}
