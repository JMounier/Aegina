using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SuccesHUD : NetworkBehaviour{

	[SerializeField]
	private bool activate;
	[SerializeField]
	private GameObject successinterface;
	[SerializeField]
	private GameObject successLocation;
	private List<SuccesIcon> listsuccess;
			
	void Start(){
		this.activate = false;

		if (!isLocalPlayer)
			return;

		this.listsuccess = new List<SuccesIcon> ();

		// ajoute tous les succes de l'interface a la liste
		for (int i = 0; i < this.successLocation.transform.childCount; i++) {
			Transform column = this.successLocation.transform.GetChild (i);
			for (int j = 0; j < column.childCount; j++) {
				Transform obj = column.GetChild (j);
				if (obj.tag == "Succes")
					this.listsuccess.Add (obj.GetComponent<SuccesIcon> ());
			}
		}
	}

	/// <summary>
	/// Updates the success interface.
	/// </summary>
	private void Update(){

		if (!this.activate) {
			this.successinterface.SetActive (false);
			return;
		}
		this.successinterface.SetActive (true);
		foreach (SuccesIcon si in listsuccess) {
			si.update ();
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether the succes window is enable.
	/// </summary>
	/// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
	public bool Enable{
		get { return this.activate;}
		set{ this.activate = value;}
	}
}
