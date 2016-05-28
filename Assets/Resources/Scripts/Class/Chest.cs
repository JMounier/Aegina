using UnityEngine;
using System.Collections;

public class Chest : Element
{
    private ItemStack[,] content;

    public Chest() : base() { }

    public Chest(Chest chest) : base(chest)
    {
        this.content = chest.content;
    }

    public Chest(int id, GameObject prefab) : base(id, 50, prefab, DestructionTool.Axe, 1, new DropConfig(45, 1)) { }

    // Methods       
    /// <summary>
    /// Reset le cristal lorsqu'il n'a plus de pv
    /// </summary>
    protected override void Kill()
    {
        ItemStack[,] content = base.prefab.GetComponent<SyncChest>().Content;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (content[i, j].Items.ID > -1)
                {
                    Vector3 projection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f));
                    content[i, j].Items.Spawn(base.prefab.transform.position + projection, projection / 2, content[i, j].Quantity);
                }
        base.Kill();
    }

    public override void Spawn(Vector3 pos, Transform parent, int idSave, bool workTopLoad = false)
    {
        base.Spawn(pos, parent, idSave, workTopLoad);
        base.prefab.GetComponent<SyncElement>().Elmt = new Chest(this);
    }

    public override void Spawn(Vector3 pos, Quaternion rot, Transform parent, int idSave, bool workTopLoad = false)
    {
        base.Spawn(pos, rot, parent, idSave, workTopLoad);
        base.prefab.GetComponent<SyncElement>().Elmt = new Chest(this);
    }

    public ItemStack[,] Content
    {
        get
        {
            if (this.content == null)
                return new ItemStack[3, 3] {
                    {new ItemStack(), new ItemStack(), new ItemStack() },
                    { new ItemStack(), new ItemStack(), new ItemStack() },
                    { new ItemStack(), new ItemStack(), new ItemStack() } };
            return this.content;
        }
        set { this.content = value; }
    }
}