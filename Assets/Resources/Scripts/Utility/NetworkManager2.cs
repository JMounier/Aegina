using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Networking.Match;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

namespace UnityEngine.Networking
{
    public class NetworkManager2 : NetworkManager
    {

        // Use this for initialization
        void Start()
        {
            spawnPrefabs.AddRange(Resources.LoadAll<GameObject>("Prefabs"));
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            conn.playerControllers[0].gameObject.GetComponent<Social_HUD>().CmdSendActivity(Activity.Disconnection);
            base.OnServerDisconnect(conn);
        }
    }
}

