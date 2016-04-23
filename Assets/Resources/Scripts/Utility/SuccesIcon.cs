using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuccesIcon : MonoBehaviour {

	static private Texture2D vide = Resources.Load<Texture2D> ("Sprites/Interfaces/Void");


	[SerializeField]
	private int succesID;

	[SerializeField]
	private bool done;

	// update is called once per frame by the SuccesHUD if it is enable.
	public void update () {
		if (this.done)
			return;
		//get l'etat actuel dans la database
		Success suc = SuccessDatabase.Find (this.succesID);
		//this.done = suc.Achived;
		if (this.done) {
			gameObject.GetComponent<RawImage> ().texture = suc.Icon;
		} else if (suc.Isseen ()) {
			//Do something use the icon but in black and transparant ? use a shader of some sort
		} else {
			// a changer si lag car trop de changement de valeur
			gameObject.GetComponent<RawImage> ().texture = vide;
		}
	}
}
