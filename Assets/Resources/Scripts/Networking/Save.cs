using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class Save : NetworkBehaviour
{

    // Usefull
    private float coolDownSave = 60;

    // Composants
    private DayNightCycle dnc;

    //Variables
    private string nameWorld;
    private int seed;


    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.dnc = gameObject.GetComponent<DayNightCycle>();
            this.nameWorld = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().World;

            // Set world properties
            string[] properties = System.IO.File.ReadAllText(Application.dataPath + "/Saves/" + this.nameWorld + "/properties").Split('|');
            this.seed = int.Parse(properties[0]);
            this.dnc.SetTime(float.Parse(properties[1]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.coolDownSave -= Time.deltaTime;
        if (this.coolDownSave <= 0)
            this.SaveWorld();
    }

    public void SaveWorld()
    {
        File.WriteAllText(Application.dataPath + "/Saves/" + this.nameWorld + "/properties", this.seed.ToString() + "|" + ((int)this.dnc.ActualTime).ToString());
    }

    public static void CreateWorld(string name, int seed)
    {
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name);
        File.WriteAllText(Application.dataPath + "/Saves/" + name + "/properties", seed.ToString() + "|0");
    }

    // Getter & Setters
    public int Seed
    {
        get { return this.seed; }
    }
}
