using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapGeneration : NetworkBehaviour
{
    Save save;

    // Use this for initialization    
    void Start()
    {
        if (isServer)
        {
            this.save = gameObject.GetComponent<Save>();
            this.GenerateChunk(0, 0, Bridges.TwoL, Directions.North);
            this.GenerateChunk(0, 1, Bridges.TwoL, Directions.East, true);
            this.GenerateChunk(1, 1, Bridges.TwoL, Directions.South);
            this.GenerateChunk(1, 0, Bridges.TwoL, Directions.West);

            for (int i = 0; i < 15; i++)
            {
                new Mob(EntityDatabase.Boar).Spawn(new Vector3(Random.Range(-15, 15), 7, Random.Range(-15, 15)));
            }          
        }
    }

    private void GenerateChunk(int x, int y, Bridges bridge, Directions dir, bool islandCore = false)
    {
        System.Random rand = new System.Random(chunkSeed(x, y, this.save.Seed));
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
}
