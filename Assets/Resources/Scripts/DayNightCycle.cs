using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DayNightCycle : MonoBehaviour
{
    private Light sun;
    private Light moon;
    private Color32 defaultmooncolor;
    private float actual_time;
    private float cycleTime = 60;
    private float gamma = 0.80f;
    private float nightIntensity = 0.1f;
    private int diameter = 100;
    private int height = 100;
    private bool bloodmoon = false ;
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
        }
        this.sun.gameObject.transform.TransformPoint(sun.transform.position);
        this.moon.gameObject.transform.TransformPoint(moon.transform.position);
        this.sun.color = SkysColor(0);
        this.defaultmooncolor = SkysColor(0);
        this.moon.color = this.defaultmooncolor;
        this.moon.intensity = this.nightIntensity;

        // init time
        this.actual_time = 0f; // a voir si en chargeant un monde on revient pas a t = 0.
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Test = SkysColor(actual_time / cycleTime); // teste la couleur
        this.actual_time = (this.actual_time + Time.deltaTime) % this.cycleTime;

        // float phasedTime = Mathf.Abs((actual_time - cycleTime / 3) % cycleTime) / cycleTime/*dephasage pour avoir le zenith au debut*/;

        /*  var de la couleur en fonction du temps (la skybox le fait tout seul)
        // color settings
        if (actual_time <= cycleTime / 3)
            sun.color = SkysColor((cycleTime * 2 / 3 - actual_time) * 3 / cycleTime);
        else if (actual_time >= 2 * cycleTime / 3)
            sun.color = SkysColor(actual_time * 3 / cycleTime);
        */

        // intensity setting
        this.sun.intensity = Mathf.Max(nightIntensity, (-4 * (this.actual_time % this.cycleTime / this.cycleTime * 2) * (this.actual_time % this.cycleTime / this.cycleTime * 2) + 4 * (this.actual_time % this.cycleTime / this.cycleTime * 2)) * 1);
        
        // position de la lune et du soleil
        Vector3[] position = Orbit(actual_time);
        this.sun.transform.position = position[0];
        this.moon.transform.position = position[1];
        this.sun.transform.LookAt(gameObject.transform);
        this.moon.transform.LookAt(gameObject.transform);

        // Blood moon
        if (this.bloodmoon)
            this.moon.color = Color.red;
        else if (moon.color != defaultmooncolor)
        {
            this.moon.color = defaultmooncolor;
        }
    }

    // Methods

    private static int[] WaveLengthToRGB(int wavelength, float intensityMax, float gamma) // de 380 a 780 (le visible)
    {
        float factor;
        float red, green, blue;
        // convertion en couleur
        if ((wavelength >= 380) && (wavelength < 440))
        {
            red = -(wavelength - 440) / (440 - 380);
            green = 0.0f;
            blue = 1.0f;
        }
        else if ((wavelength >= 440) && (wavelength < 490))
        {
            red = 0.0f;
            green = (wavelength - 440) / (490 - 440);
            blue = 1.0f;
        }
        else if ((wavelength >= 490) && (wavelength < 510))
        {
            red = 0.0f;
            green = 1.0f;
            blue = -(wavelength - 510) / (510 - 490);
        }
        else if ((wavelength >= 510) && (wavelength < 580))
        {
            red = (wavelength - 510) / (580 - 510);
            green = 1.0f;
            blue = 0.0f;
        }
        else if ((wavelength >= 580) && (wavelength < 645))
        {
            red = 1.0f;
            green = -(wavelength - 645) / (645 - 580);
            blue = 0.0f;
        }
        else if ((wavelength >= 645) && (wavelength < 781))
        {
            red = 1.0f;
            green = 0.0f;
            blue = 0.0f;
        }
        else
        {
            red = 0.0f;
            green = 0.0f;
            blue = 0.0f;
        }

        // Let the intensity fall off near the vision limits
        if ((wavelength >= 380) && (wavelength < 420))
        {
            factor = 0.3f + 0.7f * (wavelength - 380) / (420 - 380);
        }
        else if ((wavelength >= 420) && (wavelength < 701))
        {
            factor = 1.0f;
        }
        else if ((wavelength >= 701) && (wavelength < 781))
        {
            factor = 0.3f + 0.7f * (780 - wavelength) / (780 - 700);
        }
        else factor = 0.0f;

        // gestion du retour

        int[] rgb = new int[3];
        // Don't want 0^x = 1 for x <> 0
        rgb[0] = (red == 0.0) ? 0 : (int)Mathf.Round(intensityMax * Mathf.Pow(red * factor, gamma));
        rgb[1] = (green == 0.0) ? 0 : (int)Mathf.Round(intensityMax * Mathf.Pow(green * factor, gamma));
        rgb[2] = (blue == 0.0) ? 0 : (int)Mathf.Round(intensityMax * Mathf.Pow(blue * factor, gamma));

        return rgb;
    }

    private Color SkysColor(float t)
    {
        int r, g, b;
        List<int[]> colors = new List<int[]>();
        for (int i = 380; i < 781; i += 10)
        {
            float x = (i - 380) / 400f;
            colors.Add(WaveLengthToRGB(i,/*ceci  prend une valeur entre 0 et 255*/((3.25f * t - 4f) * x * x + (-4f * t + 4f) * x) * 255f, gamma));
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

    private Vector3[] Orbit(float time)
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
    public float Time
    {
        get { return this.actual_time; }
    }
    public bool BloodMoon
    {
        get { return this.bloodmoon; }
        set { bloodmoon = value; }
    }
}
