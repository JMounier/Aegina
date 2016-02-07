using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Loot : NetworkBehaviour
{
    private ItemStack items;

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            this.items.Items.Ent.Life -= Time.deltaTime;
            if (this.items.Quantity == this.items.Items.Size && this.items.Items.Ent.Prefab.GetComponent<SphereCollider>().enabled)
                this.items.Items.Ent.Prefab.GetComponent<SphereCollider>().enabled = false;
        }
    }

    // Detection loot
    void OnTriggerStay(Collider col)
    {
        if (isServer && col.CompareTag("Loot"))
        {
            Loot autre = col.GetComponent<Loot>();

            if (autre.items.Items.ID == this.items.Items.ID && autre.items.Items.Ent.LifeMax - autre.items.Items.Ent.Life > 1 && this.items.Quantity > 0
               && this.items.Items.Ent.LifeMax - this.items.Items.Ent.Life > 1 && autre.items.Items.Ent.Prefab.GetHashCode() < this.items.Items.Ent.Prefab.GetHashCode())
            {
                int diff = Mathf.Max(this.items.Quantity + autre.items.Quantity - this.items.Items.Size, 0);
                this.items.Quantity += autre.items.Quantity - diff;
                if (diff == 0)
                    autre.items.Items.Ent.Life = 0;
                autre.items.Quantity = diff;
            }
        }
    }

    // Getters & Setters
    /// <sumary>
    /// L'item stack lie au loot.
    /// </sumary>
    public ItemStack Items
    {
        set { this.items = value; }
        get { return this.items; }
    }
}
