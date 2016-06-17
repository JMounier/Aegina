using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SlimeDeath : MonoBehaviour
{
    [SerializeField]
    private int idMob;
    [SerializeField]
    private int nbMin;
    [SerializeField]
    private int nbMax;

    void OnDestroy()
    {
        if (NetworkServer.active)
            for (int i = 0; i < Random.Range(this.nbMin, this.nbMax); i++)
            {
                Mob mob = EntityDatabase.Find(this.idMob) as Mob;
                new Mob(mob).Spawn(gameObject.transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), gameObject.transform.parent);
            }
    }
}
