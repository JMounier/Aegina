using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour {

    private Controller controllerScript;
    private Inventory inventoryScript;

    // Use this for initialization
    void Start ()
    {
        this.controllerScript = gameObject.GetComponentInParent<Controller>();
        this.inventoryScript = gameObject.GetComponentInParent<Inventory>();
    }	
	
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            this.controllerScript.IsJumping = false;
    }

    // Detect gameObject arround
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Loot") && col.GetType() == typeof(MeshCollider))        
            inventoryScript.DetectLoot(col.gameObject);        
    }
}
