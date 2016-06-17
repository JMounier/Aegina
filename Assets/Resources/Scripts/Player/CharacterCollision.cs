using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour
{

    private Controller controllerScript;
    private Inventory inventoryScript;
    private BossFight bossFight;
    private SyncRigidbody syncrgb;

    // Use this for initialization
    void Start()
    {
        this.controllerScript = gameObject.GetComponentInParent<Controller>();
        this.inventoryScript = gameObject.GetComponentInParent<Inventory>();
        this.bossFight = gameObject.GetComponentInParent<BossFight>();
        this.syncrgb = gameObject.GetComponentInParent<SyncRigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")        
            this.controllerScript.IsJumping = false;
        if (collision.collider.tag == "Boss")
        {
            this.bossFight.receiveDamageByBoss();
            syncrgb.CmdExplosion(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z,50,50000);
        }
    }

    void Update()
    {
        foreach (Collider col in Physics.OverlapSphere(gameObject.transform.position, 1))
            if (col.CompareTag("Loot") && (col.GetType() == typeof(MeshCollider) || col.GetType() == typeof(BoxCollider) || col.GetType() == typeof(CapsuleCollider)))
                inventoryScript.DetectLoot(col.gameObject);            
    }
}
