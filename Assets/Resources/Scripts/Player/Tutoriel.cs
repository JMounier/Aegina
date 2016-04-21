using UnityEngine;
using System.Collections;

public class Tutoriel : MonoBehaviour {

	private Controller controler;
	private Inventory inventaire;
	private Cristal_HUD cristal;
	private int progress = -1;
	private Text textObjectif;
	private Text textNarator;
	private bool istutorial = false;
	private bool finished_tutorial = false;
	private GUISkin skin;
	private float Cooldown = 0;//le cooldown correspond au temps necessaire au narrateur pour parler. Ils seront modifier après des test.
	// Use this for initialization
	void Start () {
		this.controler = this.transform.GetComponent<Controller> ();
		this.inventaire = this.transform.GetComponent<Inventory> ();
		this.cristal = this.transform.GetComponent<Cristal_HUD> ();
		this.skin = Resources.Load<GUISkin> ("Sprites/GUIskin/skin");
		//load de progress et finished_tutorial
		if (!finished_tutorial && progress == -1) {
			istutorial = true;
			controler.Pause = true;
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		//choix des textes et progression du tutoriel
		if (progress > -1) {
			switch (progress) {
			case 0:
				textObjectif = TextDatabase.MoveObjectif;
				textNarator = TextDatabase.Move;
				if (Cooldown <= 0 && controler.Ismoving && !controler.IsJumping) {
					progress = 1;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 1:
				textObjectif = TextDatabase.RunObjectif;
				textNarator = TextDatabase.Run;
				if (Cooldown <= 0 && controler.IsSprinting) {
					progress = 2;
					Cooldown = 10;
					//déclenchement du texte et du son

				}
				break;
			case 2:
				textObjectif = TextDatabase.RunObjectif;
				textNarator = TextDatabase.Run;
				if (Cooldown <= 0 && controler.IsJumping) {
					progress = 3;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 3:
				textObjectif = TextDatabase.PickItemObjectif;
				textNarator = TextDatabase.PickItem1;
				if (Cooldown <= 0) {
					progress = 4;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 4:
				textNarator = TextDatabase.PickItem2;
				if (Cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.Stick)) {
					progress = 5;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 5:
				textNarator = TextDatabase.Equip;
				textObjectif = TextDatabase.EquipObjectif;
				if (Cooldown <= 0 && inventaire.UsedItem.Items == ItemDatabase.Stick) {
					progress = 6;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 6:
				textNarator = TextDatabase.KillThePig;
				textObjectif = TextDatabase.KillThePigObjectif;
				if (Cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.Gigot)) {
					progress = 7;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 7:
				textNarator = TextDatabase.CraftABrochette;
				textObjectif = TextDatabase.CraftABrochetteObejctif;
				if (Cooldown <= 0 && inventaire.InventoryContains(ItemDatabase.MeatBalls)) {
					progress = 8;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 8:
				textNarator = TextDatabase.EatSomething;
				textObjectif = TextDatabase.EatSomethingObjectif;
				if (Cooldown <= 0) /* récupérer la consomation d'un objet : attendre la fin de stats */ {
					progress = 9;
					Cooldown = 30;
					//déclenchement Cinématique
				}
				break;
			case 9:
				textNarator = TextDatabase.CinematiqueWherIAm1;
				if (Cooldown <= 15){
					textNarator = TextDatabase.CinematiqueWherIAm2;

					if (Cooldown <= 0) {
						progress = 10;
						Cooldown = 10;	
						//déclenchement du texte et du son
					}
				}
				break;
			case 10:
				textNarator = TextDatabase.CristalView;
				textObjectif = TextDatabase.CristalViewObjectif;
				if (Cooldown <= 0 && this.cristal.Cristal_shown) {
					progress = 11;
					Cooldown = 10;
					//déclenchement du texte et du son
				}
				break;
			case 11:
				textNarator = TextDatabase.FirstCristal;
				textObjectif = TextDatabase.FirstCrisatlObjectif;
				if (Cooldown <= 0 && this.cristal.Cristal_shown && this.cristal.Cristal.LevelTot > 0) {
					progress = -1;
					finished_tutorial = true;
					//declenchement de la seconde cynématique
				}
				break;
			default:
				textNarator = new Text ();
				textObjectif = new Text ();
				break;
			}
		}
	}
	void OnGUI(){
		if (istutorial) 
			TutorialHUD ();
		
		if (Cooldown > 0) {
			Cooldown -= Time.deltaTime;
			NarratorHUD ();
		}
		if (progress > -1)
			ObjectifHUD ();
	}
	private void NarratorHUD (){
		
	}
	private void ObjectifHUD(){
		Rect rect = new Rect (5, 5, 2 * Screen.width / 9, 2 * Screen.height / 9 - 10);
		GUI.Box(rect,textObjectif.GetText(),skin.GetStyle("Narrateur&Objectifs"));
	}
	private void TutorialHUD(){
		Rect rect = new Rect (Screen.width / 3, Screen.height / 2 - Screen.height / 20, Screen.width / 3, Screen.height / 10-5);
		GUI.Box (rect, TextDatabase.tutorial.GetText (), skin.GetStyle ("inventory"));
		rect.y += Screen.height / 10;
		rect.width -= 4*Screen.width / 15;
		rect.x += Screen.width / 15;
		if (GUI.Button (rect, TextDatabase.Yes.GetText(), skin.GetStyle ("button"))) {
			this.progress = 0;
			this.istutorial = false;
			this.controler.Pause = false;
		}
		rect.x += 2 * Screen.width / 15;
		if (GUI.Button (rect, TextDatabase.No.GetText(), skin.GetStyle ("button"))) {
			this.finished_tutorial = true;
			this.istutorial = false;
			this.controler.Pause = false;
		}

	}
}
