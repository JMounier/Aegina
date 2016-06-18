using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CristalExplosion : NetworkBehaviour
{
    private float cdExplosion;
    // Use this for initialization
    void Start()
    {
        this.cdExplosion = Random.Range(5, 20);
    }

    // Update is called once per frame
    void Update()
    {
        this.cdExplosion -= Time.deltaTime;
        gameObject.transform.GetChild(0).Rotate(Vector3.forward, Mathf.Pow(20f - this.cdExplosion, 2) * .03125f);

        if (isServer)
        {           
            if (this.cdExplosion < -.1f && !gameObject.GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                NetworkServer.UnSpawn(gameObject);
                GameObject.Destroy(gameObject);
            }
            else if (this.cdExplosion < 0 && !gameObject.GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    float dist = Vector3.Distance(player.transform.FindChild("Character").position, gameObject.transform.position);
                    if (dist < 2.5f)
                        player.GetComponent<SyncCharacter>().ReceiveDamage((2.5f - dist) * 50, Vector3.Normalize(player.transform.FindChild("Character").position - gameObject.transform.position), false);
                }
            }

        }
    }
}
