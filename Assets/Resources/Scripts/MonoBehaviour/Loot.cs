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
        if (col.gameObject.name.Contains("Character"))
        {
            col.gameObject.GetComponentInParent<Inventory>().AddItemStack(this.items);
        }
    }

    // Getters & Setters
    public ItemStack Items
    {
        set { this.items = value; }
        get { return this.items; }
    }
}
