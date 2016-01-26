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
