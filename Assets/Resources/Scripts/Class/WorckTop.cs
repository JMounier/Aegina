using UnityEngine;
using System.Collections;

public class WorckTop : Item {

    private GameObject previsu;

    public WorckTop() : base()
    {
        this.previsu = null;
    }

    public WorckTop(WorckTop item) : base(item)
    {
        this.previsu = item.previsu;
    }

    public WorckTop(int id, Text name, Text description, int size, Texture2D icon, Entity ent, GameObject previsualisation) : base(id,name,description,size,icon,ent)
    {
        this.previsu = previsualisation;
    }

    public WorckTop(int id, int meta, Text name, Text description, int size, Texture2D icon, Entity ent, GameObject previsualisation) : base(id,meta,name,description,size,icon,ent)
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
