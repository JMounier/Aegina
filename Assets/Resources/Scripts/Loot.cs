using UnityEngine;
using System.Collections;

/// <summary>
/// Utiliser cette classe pour creer un nouveau loot.
/// </summary>
public class Loot : Entity
{
    private ItemStack items;

    // Constructor
    public Loot() : base()
    {
        this.items = new ItemStack();
    }

    public Loot(int id, GameObject prefab, ItemStack items) : base(id, 60, prefab)
    {
        this.items = items;
    }

    /// <summary>
    /// L'item stack correspondant au loot.
    /// </summary>
    // Getters & Setters
    public ItemStack Items
    {
        get { return this.items; }
        set { this.items = value; }
    }
}
