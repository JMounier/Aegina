using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Networking.Match;
using System.IO;
using System.Linq;

namespace UnityEngine.Networking
{
	public class NetworkManager2 : NetworkManager
	{

		// Use this for initialization
		void Start ()
		{
			spawnPrefabs.AddRange (Resources.LoadAll<GameObject> ("Prefabs"));
		}
	}
}

