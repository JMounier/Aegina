using UnityEngine;
using System.Collections;

public class WorkTop : Item {

    private GameObject previsu;

    public WorkTop() : base()
    {
        this.previsu = null;
    }

    public WorkTop(WorkTop item) : base(item)
    {
        this.previsu = item.previsu;
    }

    public WorkTop(int id, Text name, Text description, int size, Texture2D icon, Entity ent, GameObject previsualisation) : base(id,name,description,size,icon,ent)
    {
        this.previsu = previsualisation;
    }

    public WorkTop(int id, int meta, Text name, Text description, int size, Texture2D icon, Entity ent, GameObject previsualisation) : base(id,meta,name,description,size,icon,ent)
    {
        this.previsu = previsualisation;
    }




    #region Getters/Setters

    public GameObject Previsu
    {
        get { return this.previsu; }
        set { this.previsu = value; }
    }
    #endregion
}
