using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

        else if (collision.collider.tag == "Boss")
        {
            this.bossFight.ReceiveDamageByBoss();
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(500, collision.transform.position, 500);
            gameObject.GetComponentInParent<Controller>().CdDisable = 0.5f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("IslandCore"))
            this.bossFight.ReceiveDamageByCristaleProjectile(col.transform.parent.gameObject);
    }

    void Update()
    {
        foreach (Collider col in Physics.OverlapSphere(gameObject.transform.position, 1))
            if (col.CompareTag("Loot") && (col.GetType() == typeof(MeshCollider) || col.GetType() == typeof(BoxCollider) || col.GetType() == typeof(CapsuleCollider)))
                inventoryScript.DetectLoot(col.gameObject);
    }
}
