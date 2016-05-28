using UnityEngine;
using System.Collections;

public class SyncChest : SyncElement
{
    private ItemStack[,] content;

    // Use this for initialization
    protected override void Start()
    {
        this.content = new ItemStack[3, 3] { 
            { new ItemStack(), new ItemStack(), new ItemStack() },
            { new ItemStack(), new ItemStack(), new ItemStack() },
            { new ItemStack(), new ItemStack(), new ItemStack() } };
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
