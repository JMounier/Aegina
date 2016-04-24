using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class Story_Hud : NetworkBehaviour
{
    private float incrémentation;
    private float posYpercent;
    private Queue successToDisplay;
    private Success ActualSucces;
    private GUISkin skin;
    // Use this for initialization
    void Start()
    {
        successToDisplay = new Queue();
        incrémentation = 0;
        posYpercent = 0;
        skin = Resources.Load<GUISkin>("Sprites/GUIskin/skin");

    }

    // Update is called once per frame
    void Update()
    {
        if (successToDisplay.Count > 0 || ActualSucces != null)
        {
            if (incrémentation == 0)
            {
                if (successToDisplay.Count > 0)
                {
                    ActualSucces = (Success)successToDisplay.Dequeue();
                    incrémentation = 2;
                }
                else 
                    ActualSucces = null;                
            }
            else
            {
                posYpercent += incrémentation;
                if (incrémentation < 0 && posYpercent <= 0)
                    incrémentation = 0;
                else if (posYpercent > 150)
                    incrémentation = -3;
            }
        }
    }
    void OnGUI()
    {
        if (incrémentation != 0)
        {
            Rect rect = new Rect(Screen.width / 20, -Screen.height / 10 + (Mathf.Clamp(posYpercent, 0, 100) / 100) * Screen.height / 9, Screen.width / 7, Screen.height / 9);
            GUI.Box(rect, "", skin.GetStyle("Inventory"));
            rect.width /= 2;
            rect.x += Screen.width / 100 + Screen.width / 14;
            rect.y += Screen.height / 100;
            rect.height -= Screen.height / 50;
            rect.width -= Screen.width / 50;
            GUI.Box(rect, "Achievement Get", skin.GetStyle("Description"));
            rect.x -= Screen.width / 14;
            rect.width = rect.height;
            GUI.DrawTexture(rect, ActualSucces.Icon);
        }
    }


    /// <summary>
    /// Must be server!!!!
    /// Display the achivement on the client.
    /// </summary>
    /// <param name="succes"></param>
    public void Display(Success success)
    {
        RpcDisplay(success.ID);
    }

    [ClientRpc]
    private void RpcDisplay(int id)
    {
        if (!isLocalPlayer)
            return;
        successToDisplay.Enqueue(SuccessDatabase.Find(id));

    }
}
