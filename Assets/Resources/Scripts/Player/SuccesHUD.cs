using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SuccesHUD : NetworkBehaviour
{

    [SerializeField]
    private bool activate;
    [SerializeField]
    private GameObject successinterface;
    [SerializeField]
    private GameObject successLocation;
    [SerializeField]
    private GameObject Light;

    private List<SuccesIcon> listsuccess;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        this.activate = false;
        this.Light.SetActive(false);
        this.listsuccess = new List<SuccesIcon>();

        // ajoute tous les succes de l'interface a la liste
        for (int i = 0; i < this.successLocation.transform.childCount; i++)
        {
            Transform column = this.successLocation.transform.GetChild(i);
            for (int j = 0; j < column.childCount; j++)
            {
                Transform obj = column.GetChild(j);
                if (obj.tag == "Succes")
                    this.listsuccess.Add(obj.GetComponent<SuccesIcon>());
            }
        }
        this.successinterface.SetActive(false);
        if (!isServer)
            Success.Reset();

        CmdUpdateSucces();
    }

    /// <summary>
    /// Updates the success interface.
    /// </summary>
    private void Update()
    {
        if (!this.activate)
        {
            this.successinterface.SetActive(false);
            this.Light.SetActive(false);
            return;
        }
        this.Light.SetActive(true);
        this.successinterface.SetActive(true);
        foreach (SuccesIcon si in listsuccess)
        {
            si.upGraphics();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the succes window is enable.
    /// </summary>
    /// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
    public bool Enable
    {
        get { return this.activate; }
        set { this.activate = value; }
    }

    [Command]
    private void CmdUpdateSucces()
    {
        Queue<Success> succs = new Queue<Success>();
        succs.Enqueue(SuccessDatabase.Root);
        while (succs.Count != 0)
        {
            Success suc = succs.Dequeue();
            if (suc.Achived)
            {
                RpcUpdateSucces(suc.ID);
                foreach (Success succ in suc.Sons)
                    succs.Enqueue(succ);
            }
        }
    }

    [ClientRpc]
    private void RpcUpdateSucces(int id)
    {
        if (isLocalPlayer)
            SuccessDatabase.Find(id).Unlock(false);
    }
}
