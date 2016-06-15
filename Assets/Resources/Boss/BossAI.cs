using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {


	private int life;
	private BossSceneManager bSM;
	private float cd;

	// Use this for initialization
	void Start () {
		// might change for game balance
		this.life = 500;
		this.bSM = GameObject.FindGameObjectWithTag ("BossFight").GetComponent<BossSceneManager> ();
		this.cd = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
