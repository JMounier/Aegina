using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Social : NetworkBehaviour
{

    private TextMesh nameTextMesh;
    [SyncVar]
    private string namePlayer;
    // Use this for initialization
    void Start()
    {
        this.nameTextMesh = gameObject.GetComponentInChildren<CharacterCollision>().transform.GetComponentInChildren<TextMesh>();
        if (isLocalPlayer)
        {
            this.nameTextMesh.gameObject.SetActive(false);
            this.CmdSetName(PlayerPrefs.GetString("PlayerName", ""));
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            // Set rotation of name well
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (!gameObject.Equals(player))
                {
                    Social other = player.GetComponent<Social>();
                    if (Vector3.Distance(player.GetComponentInChildren<CharacterCollision>().transform.position, gameObject.GetComponentInChildren<CharacterCollision>().transform.position) < 10)
                    {
                        other.GetComponent<Social>().nameTextMesh.gameObject.SetActive(true);
                        other.GetComponent<Social>().nameTextMesh.transform.rotation = Quaternion.Euler(gameObject.GetComponentInChildren<Camera>().transform.eulerAngles);
                    }
                    else
                    {
                        other.GetComponent<Social>().nameTextMesh.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if(this.nameTextMesh.text == "")
        {
            this.nameTextMesh.text = this.namePlayer;
        }
    }
    [Command]
    private void CmdSetName(string name)
    {
        this.namePlayer = name;
    }
}
