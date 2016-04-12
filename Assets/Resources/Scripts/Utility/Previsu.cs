using UnityEngine;
using System.Collections;

public class Previsu : MonoBehaviour
{

    private bool state;
    private SyncChunk chunk;

    // Use this for initialization
    void Start()
    {
        this.state = true;
        this.chunk = gameObject.transform.parent.parent.GetComponent<SyncChunk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != IsAvailable())
            SwitchStates();
    }

    public static Transform GetHierarchy(Vector3 pos)
    {
        Transform parent = null;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(5, 100, 5)))
            if (col.gameObject.name.Contains("Island"))
            {
                parent = col.transform.parent.FindChild("Elements");
                break;
            }
        return parent;
    }


    private void SwitchStates()
    {
        this.state = !this.state;
        this.SwitchColor(gameObject);
    }

    private void SwitchColor(GameObject obj)
    {
        foreach (MeshRenderer mesh in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            for (int i = 0; i < mesh.materials.Length; i++)
                mesh.materials[i] = Resources.Load<Material>("Models/WorkStations/Materials/Previsu" + ((state) ? 1 : 2));
        }
    }

    public bool IsAvailable()
    {
        return this.chunk.MyGraph.GetNode(gameObject.transform.position).IsValid;
    }
}
