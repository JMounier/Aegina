using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class BossSceneManager : NetworkBehaviour {

	[SerializeField]
	private GameObject SpawnWall;

	[SerializeField]
	private List<GameObject> OrbitingStuff;

	[SerializeField]
	private GameObject SpecCamPos;

	[SerializeField]
	private GameObject Spawn;

	private List<GameObject> PlayersInFight;

	private int DeathCount;
	private int Specpos;

	// Use this for initialization
	void Start ()
	{
		this.SpawnWall.SetActive (false);
		this.Specpos = 0;
		foreach (GameObject obj in OrbitingStuff) {
			obj.transform.LookAt (obj.transform.parent);
		}


		if (!isServer)
			return;

		this.PlayersInFight = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		foreach (GameObject obj in OrbitingStuff) {
			obj.transform.LookAt (obj.transform.parent);
			obj.transform.RotateAround (obj.transform.parent.position, obj.transform.parent.up, 0.5f);
		}


		if (!isServer)
			return;

		/*
		if (isdead)
		{
			SpecCamPos.transform.GetChild (Specpos).gameObject.SetActive(false);

			int delta = 0;
			//make something to switchpose as spec
			
			this.Specpos += delta;
			this.Specpos = (this.Specpos + 4)% 4;

			SpecCamPos.transform.GetChild (Specpos).gameObject.SetActive(true);
		}*/
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Player")
			if (!this.SpawnWall.activeInHierarchy)
			{
				this.SpawnWall.SetActive (true);
				CmdEnterFight (collision.gameObject);
			}
	}

	[Command]
	private void CmdEnterFight(GameObject player){
		if (!this.PlayersInFight.Contains(player))
			this.PlayersInFight.Add (player);
	}

	[ClientRpc]
	private void RpcRestart(){
		// tp to the spawn
		this.SpawnWall.SetActive(false);
	}
}
