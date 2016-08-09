using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpecMode : NetworkBehaviour {

	private Transform cam;
	private Transform character;
	private bool spectate;
	private bool seeGUI;

	// Use this for initialization
	void Start () {
		this.seeGUI = true;
		this.spectate = false;
		this.character = gameObject.transform.GetChild (1);
		if (isLocalPlayer)
			this.cam = gameObject.transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer || !spectate)
			return;
		// show and hide the GUI
		if (Input.GetButtonDown ("HideGUI") && !gameObject.GetComponent<Social_HUD>().ChatShown)
			this.cam.GetComponent<GUILayer> ().enabled = !this.cam.GetComponent<GUILayer> ().enabled;
	}

	#region Passage Invisible
	/// <summary>
	/// Changes the gamemode (this method is called on serveur only by the social system !!!).
	/// </summary>
	/// <param name="spec">If set to <c>true</c> spec.</param>
	public void ChangeMode(bool notSpec){
		RpcChangeMode (notSpec);
	}


	[ClientRpc]
	/// <summary>
	/// Rpcs the change mode.
	/// ne prend pas en compte le changement d'arme ou d'armure mais fuck it
	/// </summary>
	/// <param name="notSpec">If set to <c>true</c> not spec.</param>
	private void RpcChangeMode(bool notSpec){
		this.spectate = !notSpec;

		foreach (Renderer renderer in this.character.GetComponentsInChildren<Renderer>()) {
			renderer.enabled = (notSpec);
		}

		this.character.GetComponent<Rigidbody> ().useGravity = notSpec;
		this.character.GetComponent<Collider> ().enabled = notSpec;

		if (this.spectate && isLocalPlayer)
			this.cam.GetComponent<GUILayer> ().enabled = true;
	}
	#endregion



	#region Gettters/Setters

	public bool isSpec {
		get {return this.spectate; }
	}

	#endregion
}
