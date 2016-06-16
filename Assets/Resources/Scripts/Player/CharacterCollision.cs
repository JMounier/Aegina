using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour
{

    private Controller controllerScript;
    private Inventory inventoryScript;

    // Use this for initialization
    void Start()
    {
        this.controllerScript = gameObject.GetComponentInParent<Controller>();
        this.inventoryScript = gameObject.GetComponentInParent<Inventory>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")        
            this.controllerScript.IsJumping = false;       
    }

    void Update()
    {
        foreach (Collider col in Physics.OverlapSphere(gameObject.transform.position, 1))
            if (col.CompareTag("Loot") && (col.GetType() == typeof(MeshCollider) || col.GetType() == typeof(BoxCollider) || col.GetType() == typeof(CapsuleCollider)))
                inventoryScript.DetectLoot(col.gameObject);            
    }
}
