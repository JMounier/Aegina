using UnityEngine;
using System.Collections;

public class Spiques : MonoBehaviour
{

    void Update()
    {
        bool isActive = false;
        foreach (Collider col in Physics.OverlapBox(gameObject.transform.position, new Vector3(.25f, .25f, .25f)))
        {
            if (col.transform.parent != null && col.transform.parent.CompareTag("Player"))
            {
                col.GetComponentInParent<SyncCharacter>().Life -= Time.deltaTime * 10;
                isActive = true;
            }
            else if (col.transform.CompareTag("Mob"))
            {
                col.GetComponent<SyncMob>().MyMob.Life -= Time.deltaTime * 10;
                isActive = true;
            }
        }
        gameObject.transform.parent.GetComponent<Animator>().SetBool("Action", isActive);
    }   
}
