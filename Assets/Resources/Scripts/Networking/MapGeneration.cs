using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{
    private int worldSeed;
    private NetworkManagerHUD nm;
    private DayNightCycle dnc;
    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            this.nm = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>();
            this.dnc = GameObject.Find("Map").GetComponent<DayNightCycle>();

            string[] properties = System.IO.File.ReadAllText(Application.dataPath + "/Saves/" + this.nm.World + "/properties").Split('|');
            this.worldSeed = int.Parse(properties[0]);
            this.dnc.SetTime(float.Parse(properties[1]));

            this.GenerateChunk(0, 0, Bridges.TwoL, Directions.North);
            this.GenerateChunk(0, 1, Bridges.TwoL, Directions.East, true);
            this.GenerateChunk(1, 1, Bridges.TwoL, Directions.South);
            this.GenerateChunk(1, 0, Bridges.TwoL, Directions.West);
        
            new Mob(EntityDatabase.Boar).Spawn(new Vector3(0, 7, 0));
            new Mob(EntityDatabase.Boar).Spawn(new Vector3(5, 7, 5));
            new Mob(EntityDatabase.Boar).Spawn(new Vector3(0, 7, -5));
            new Mob(EntityDatabase.Boar).Spawn(new Vector3(-5, 7, 0));
            new Mob(EntityDatabase.Boar).Spawn(new Vector3(0, 7, 0));
        }
    }

    private void GenerateChunk(int x, int y, Bridges bridge, Directions dir, bool islandCore = false)
    {
        System.Random rand = new System.Random(chunkSeed(x, y, this.worldSeed));
        EntityDatabase.RandChunk(bridge, rand).Generate(x, y, rand, dir, gameObject, islandCore);
    }

    /// <summary>
    /// Retourne le seed propre a un chunk avec sa position et le seed du monde.
    /// </summary>
    /// <param name="x">Position x du chunk.</param>
    /// <param name="y">Position y du chunk.</param>
    /// <param name="seedWord">Seed du monde.</param>
    /// <returns></returns>
    private int chunkSeed(int x, int y, int seedWord)
    {
        return (467 * x - 131 * y + 1) * seedWord;
    }

    /// <summary>
    /// Retourne le seed du monde.
    /// </summary>
    public int WorldSeed
    {
        get { return this.worldSeed; }
    }
}
