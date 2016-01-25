using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Loot : NetworkBehaviour
{

    private ItemStack items;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.items.Items.Ent.Life -= Time.deltaTime;
    }

    // Call with collision
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name.Contains("Character") && this.items.Items.Ent.LifeMax - this.items.Items.Ent.Life > 1)
        {
            col.gameObject.GetComponentInParent<Inventory>().AddItemStack(this.items);
            if (this.items.Quantity == 0)
                this.items.Items.Ent.Life = 0;
        }
    }

    // Getters & Setters
    public ItemStack Items
    {
        set { this.items = value; }
        get { return this.items; }
    }
}
