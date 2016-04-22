using UnityEngine;
using System.Collections;

public class Tutoriel : MonoBehaviour {

	private Controller controler;
	private Inventory inventaire;
	private Cristal_HUD cristal;
	private InputManager IM;
	private int progress = -1;
	private Text textObjectif;
	private Text textNarator;
	private bool istutorial = false;
	private bool finished_tutorial = false;
	private GUISkin skin;
	private bool endtuto = false;
	private float Cooldown = 10;//le cooldown correspond au temps necessaire au narrateur pour parler. Ils seront modifier après des test.
	// Use this for initialization
	void Start () {
		this.controler = this.transform.GetComponent<Controller> ();
		this.inventaire = this.transform.GetComponent<Inventory> ();
		this.cristal = this.transform.GetComponent<Cristal_HUD> ();
		this.IM = this.transform.GetComponent<InputManager> ();
		this.skin = Resources.Load<GUISkin> ("Sprites/GUIskin/skin");
		//load de progress et finished_tutorial
		if (!finished_tutorial && progress == -1) {
			istutorial = true;
			controler.Pause = true;
		}
			
	}

	void OnGUI(){
		if (istutorial) 
			TutorialHUD ();

		if (Cooldown > 0 && progress >-1) {
			Cooldown -= Time.deltaTime;
			NarratorHUD ();
		}
		if (progress > -1)
			ObjectifHUD ();
		if (endtuto)
			endtutoHUD ();
	}
	// Update is called once per frame
	void Update () {
		skin.GetStyle ("Objectifs").fontSize = (int)(Screen.height * 0.025f);
		skin.GetStyle ("Narrateur").fontSize = (int)(Screen.height * 0.030f);
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
				if (Cooldown <= 0 && Input.GetButton("Sprint") && controler.Ismoving) {
					progress = 2;
					Cooldown = 10;
					//déclenchement du texte et du son

				}
				break;
			case 2:
				textObjectif = TextDatabase.JumpObjectif;
				textNarator = TextDatabase.Jump;
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
					Cooldown = 20;
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
				if (Cooldown <= 0 && inventaire.UsedItem.Items.Name == ItemDatabase.Stick.Name) {
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
					Cooldown = 15;
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
				if (Cooldown <= 0 && !this.inventaire.InventoryContains(ItemDatabase.MeatBalls)&& Input.GetButton("Fire2")) /* récupérer la consomation d'un objet : attendre la fin de stats */ {
					progress = 9;
					Cooldown = 60;
					//déclenchement Cinématique
				}
				break;
			case 9:
				textObjectif = new Text ();
				textNarator = TextDatabase.CinematiqueWhereIAm1;
				if (Cooldown <= 45)
					textNarator = TextDatabase.CinematiqueWhereIAm2;
				if (Cooldown <= 35){
					textNarator = TextDatabase.CinematiqueWhereIAm3;

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
					Cooldown = 20;
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
	private void NarratorHUD (){
		Rect rect = new Rect (Screen.width / 4, Screen.height - this.inventaire.ToolbarSize * 1.6f - Screen.height / 3, Screen.width / 2, Screen.height / 3);
		GUI.Box (rect, textNarator.GetText (), skin.GetStyle ("Narrateur"));
	}
	private void ObjectifHUD(){
		int num = textObjectif.GetText ().Length / 28;
		Rect rect = new Rect (5, 5, 2 * Screen.width / 9, (1+num) * Screen.height/35 );
		GUI.Box(rect,textObjectif.GetText(),skin.GetStyle("Objectifs"));
		rect.x = rect.width-Screen.width/100;
		rect.y = rect.height-Screen.width/100;
		rect.width = Screen.width / 50;
		rect.height = rect.width;
		if (this.textObjectif.GetText() != "" && GUI.Button(rect,Resources.Load<Texture2D>("Sprites/CraftsIcon/Invalid"),skin.GetStyle("quantity"))) {
			this.controler.Pause = true;
			this.IM.IAmDead ();
			endtuto = true;
		}
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
			this.Cooldown = 10;
		}
		rect.x += 2 * Screen.width / 15;
		if (GUI.Button (rect, TextDatabase.No.GetText(), skin.GetStyle ("button"))) {
			this.finished_tutorial = true;
			this.istutorial = false;
			this.controler.Pause = false;
		}

	}
	private void endtutoHUD(){
		Rect rect = new Rect (Screen.width / 3, Screen.height / 2 - Screen.height / 20, Screen.width / 3, Screen.height / 10-5);
		GUI.Box (rect, TextDatabase.Quit.GetText ()+ " tuto ?", skin.GetStyle ("inventory"));
		rect.y += Screen.height / 10;
		rect.width -= 4*Screen.width / 15;
		rect.x += Screen.width / 15;
		if (GUI.Button (rect, TextDatabase.Yes.GetText(), skin.GetStyle ("button"))) {
			this.progress = -1;
			this.endtuto = false;
			this.controler.Pause = false;
		}
		rect.x += 2 * Screen.width / 15;
		if (GUI.Button (rect, TextDatabase.No.GetText(), skin.GetStyle ("button"))) {
			this.endtuto = false;
			this.controler.Pause = false;
		}
	}

	public bool EndTutoShown{
		get { return endtuto;}
		set { this.endtuto = value;}
	}
}
