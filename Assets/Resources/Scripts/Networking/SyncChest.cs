using UnityEngine;
using System.Collections;

public class SyncChest : SyncElement
{
    private ItemStack[,] content;

    // Use this for initialization
    protected override void Start()
    {
        if (isServer)
            this.content = (base.Elmt as Chest).Content;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public ItemStack[,] Content
    {
        get { return this.content; }
        set { this.content = value; }
    }
}
