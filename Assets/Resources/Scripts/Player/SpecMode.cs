using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpecMode : NetworkBehaviour {

	private Transform cam;
	private GameObject character;
	private bool spectate;

	// Use this for initialization
	void Start () {
		this.spectate = false;
		this.character = gameObject.transform.GetChild (1).gameObject;
		if (isLocalPlayer)
			this.cam = gameObject.transform.GetChild (0);
	}

	#region Passage Invisible
	/// <summary>
	/// Changes the gamemode (this method is called on serveur only by the social system !!!).
	/// </summary>
	/// <param name="spec">If set to <c>true</c> spec.</param>
	public void ChangeMode(bool notSpec){

		this.spectate = !notSpec;

		foreach (Renderer renderer in this.character.GetComponentsInChildren<Renderer>()) {
			renderer.enabled = (notSpec);
		}
		this.character.GetComponent<Rigidbody> ().useGravity = notSpec;
		this.character.GetComponent<Collider> ().enabled = notSpec;

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
		this.character.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		this.character.GetComponent<Collider> ().enabled = notSpec;

		if (isLocalPlayer)
			InputManager.seeGUI = notSpec;
	}
	#endregion



	#region Gettters/Setters

	public bool isSpec {
		get {return this.spectate; }
	}

	#endregion
}
