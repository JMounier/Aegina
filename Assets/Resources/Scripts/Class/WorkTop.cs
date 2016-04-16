using UnityEngine;
using System.Collections;

public class WorkTop : Item
{

    private GameObject previsu;

    private int elementID;

    public WorkTop() : base()
    {
        this.previsu = null;
        this.elementID = -1;
    }

    public WorkTop(WorkTop item) : base(item)
    {
        this.previsu = item.previsu;
        this.elementID = item.elementID;
    }

    public WorkTop(int id, Text name, Text description, int size, Texture2D icon, Entity ent, GameObject previsualisation, int elementID) : base(id, name, description, size, icon, ent)
    {
        this.previsu = previsualisation;
        this.elementID = elementID;
    }

    public WorkTop(int id, int meta, Text name, Text description, int size, Texture2D icon, Entity ent, GameObject previsualisation, int elementID) : base(id, meta, name, description, size, icon, ent)
    {
        this.previsu = previsualisation;
        this.elementID = elementID;
    }

    public bool IsValid()
    {
        if (this.previsu.transform.parent == null)
            return true;
        Node n = this.previsu.transform.parent.parent.GetComponent<SyncChunk>().MyGraph.GetNode(this.previsu.transform.position);
        return n != null && n.IsValid;
    }
    public static Transform GetHierarchy(Vector3 pos)
    {
        Transform parent = null;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(5, 100, 5)))        
            if (col.name.Contains("Island") && col.tag == "Ground")
            {
                parent = col.transform.parent.FindChild("Elements");
                break;
            }        
        return parent;
    }

    #region Getters/Setters

    public GameObject Previsu
    {
        get { return this.previsu; }
        set { this.previsu = value; }
    }

    public int ElementID
    {
        get { return this.elementID; }
        set { this.elementID = value; }
    }
    #endregion
}
