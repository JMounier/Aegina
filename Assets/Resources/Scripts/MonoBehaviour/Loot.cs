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
        if (isServer)
        {
            this.items.Items.Ent.Life -= Time.deltaTime;

            if (this.items.Quantity == 0)
                this.items.Items.Ent.Life = 0;
        }
    }

    // Call with collision
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name.Contains("Character"))
            this.CmdAspirate(col.gameObject);
    }

    [Command]
    public void CmdAspirate(GameObject player)
    {
        if (this.items.Items.Ent.LifeMax - this.items.Items.Ent.Life > 1)
        {
            player.GetComponentInParent<Inventory>().RpcAddItemStack(this.items.Items.ID, this.items.Quantity, gameObject);
        }
    }

    // Getters & Setters
    public ItemStack Items
    {
        set { this.items = value; }
        get { return this.items; }
    }

    [Command]
    public void CmdSetQuantity(int quantity)
    {
        Debug.Log(isServer);
        this.items.Quantity = quantity;
    }
}
