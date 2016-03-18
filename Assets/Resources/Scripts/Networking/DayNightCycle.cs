using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class DayNightCycle : NetworkBehaviour
{
    private Light sun;

    private float gamma = 0.80f;
    private int diameter = 100;

    [SyncVar]
    private float actual_time;
    [SyncVar]
    private float cycleTime = 1200;

    // Use this for initialization
    void Start()
    {
        // init Astres (moon and sun)
        this.sun = gameObject.GetComponentInChildren<Light>();
        this.sun.gameObject.transform.TransformPoint(sun.transform.position);
        this.sun.color = SkysColor(0);
        NetworkServer.SpawnObjects();
        // init time
        this.actual_time = 0f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.actual_time = (this.actual_time + Time.deltaTime) % this.cycleTime;
        // intensity setting
        this.sun.intensity = -4 * (this.actual_time % this.cycleTime / this.cycleTime * 2) * (this.actual_time % this.cycleTime / this.cycleTime * 2) + 4 * (this.actual_time % this.cycleTime / this.cycleTime * 2);

        // position du soleil
        this.sun.transform.position = Orbit(this.actual_time);
        this.sun.transform.LookAt(gameObject.transform);
    }

    // Methods 

    /// <summary>
    /// Fonction qui converti une onde du visible en la couleur RGB de l'objet.
    /// </summary>
    private static int[] WaveLengthToRGB(int wavelength, float intensityMax, float gamma)
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

    /// <summary>
    /// Adapte la couleur du ciel en fonction du temps.
    /// </summary>
    public Color SkysColor(float t)
    {
        int r, g, b;
        List<int[]> colors = new List<int[]>();
        for (int i = 380; i < 781; i += 10)
        {
            float x = (i - 380) / 400f;
            colors.Add(WaveLengthToRGB(i, ((3.25f * t - 4f) * x * x + (-4f * t + 4f) * x) * 255f, gamma));
        }
        r = 0;
        g = 0;
        b = 0;
        foreach (int[] color in colors)
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

    /// <summary>
    /// Calcul la trajectoire du soleil.
    /// </summary>
    private Vector3 Orbit(float time)
    {
        float x = Mathf.Cos(time / this.cycleTime * 2 * Mathf.PI);
        float y = Mathf.Sin(time / this.cycleTime * 2 * Mathf.PI);
        Vector3 pos1 = new Vector3(x * this.diameter, y * this.diameter, 0);
        return pos1;
    }

    // getters setters

    /// <summary>
    /// Retourne le temps actuel du monde.
    /// </summary>    
    public float ActualTime
    {
        get { return this.actual_time; }
    }

    /// <summary>
    /// Retourne si il fait jour ou nuit.
    /// </summary>
    public bool isDay
    {
        get
        {
            if (this.actual_time < this.cycleTime / 2)
                return true;
            return false;

        }
    }

    /// <summary>
    /// Changer l'heure actuel de la journee. (Must be Server!)
    /// </summary>    
    public void SetTime(float time)
    {
        this.actual_time = time % this.cycleTime;
    }
}