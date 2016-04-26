using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuccesIcon : MonoBehaviour {



	[SerializeField]
	private int succesID;

	[SerializeField]
	private bool done = false;
	[SerializeField]
	private bool see = false;

	// update is called once per frame by the SuccesHUD if it is enable.
	public void upGraphics () {
		if (this.done)
			return;
		//get l'etat actuel dans la database
		Success suc = SuccessDatabase.Find (this.succesID);
		this.done = suc.Achived;
		if (this.done) {
			gameObject.transform.GetChild(0).GetComponent<RawImage>().texture = suc.Icon;
			gameObject.transform.GetChild(0).GetComponent<RawImage>().material = null;
		} else if (!this.see && suc.Isseen ()) {
			this.see = true;
			gameObject.transform.GetChild(0).GetComponent<RawImage>().texture = suc.Icon;
			gameObject.transform.GetChild(0).GetComponent<RawImage>().material = SuccessDatabase.Shadow;
		}
	}
}
