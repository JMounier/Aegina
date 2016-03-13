using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{
    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            EntityDatabase.RandChunk(Bridges.TwoL).Generate(0, 0, Directions.North, BiomeDatabase.RandBiome, gameObject);
            EntityDatabase.RandChunk(Bridges.TwoL).Generate(0, 1, Directions.East, BiomeDatabase.RandBiome, gameObject, true);
            EntityDatabase.RandChunk(Bridges.TwoL).Generate(1, 1, Directions.South, BiomeDatabase.RandBiome, gameObject);
            EntityDatabase.RandChunk(Bridges.TwoL).Generate(1, 0, Directions.West, BiomeDatabase.RandBiome, gameObject);
            new Mob(EntityDatabase.Boar).Spawn(new Vector3(0,7,0));
        }
    }
}
