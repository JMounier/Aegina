using UnityEngine;
using System.Collections;

public class SyncChest : SyncElement
{
    private ItemStack[,] content;

    // Use this for initialization
    protected override void Start()
    {
        if (isServer)
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

    void OnDestroy()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (this.content[i, j].Items.ID > -1)
                {
                    Vector3 projection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f));
                    this.content[i, j].Items.Spawn(gameObject.transform.position + projection, projection / 2, this.content[i, j].Quantity);
                }
    }

    public ItemStack[,] Content
    {
        get { return this.content; }
        set { this.content = value; }
    }
}
