using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour
{

    private Controller controllerScript;
    private Inventory inventoryScript;
    private BossFight bossFight;

    // Use this for initialization
    void Start()
    {
        this.controllerScript = gameObject.GetComponentInParent<Controller>();
        this.inventoryScript = gameObject.GetComponentInParent<Inventory>();
        this.bossFight = gameObject.GetComponentInParent<BossFight>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")        
            this.controllerScript.IsJumping = false;
        if (collision.collider.tag == "Boss")
        {
            this.bossFight.receiveDamageByBoss();
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(500, collision.transform.position, 500);
            gameObject.GetComponentInParent<Controller>().CdDisable = 0.5f;
        }
    }

    void Update()
    {
        foreach (Collider col in Physics.OverlapSphere(gameObject.transform.position, 1))
            if (col.CompareTag("Loot") && (col.GetType() == typeof(MeshCollider) || col.GetType() == typeof(BoxCollider) || col.GetType() == typeof(CapsuleCollider)))
                inventoryScript.DetectLoot(col.gameObject);            
    }
}
