using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{

    private ItemStack items;
    // Use this for initialization
    void Start()
    {
        this.items = new ItemStack();
    }

    // Update is called once per frame
    void Update()
    {
        this.items.Items.Ent.Life -= Time.deltaTime;
    }

    // Getters & Setters
    public ItemStack Items
    {
        set { this.items = value; }
        get { return this.items; }
    }
}
