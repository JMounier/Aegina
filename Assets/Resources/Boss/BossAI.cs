using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {


	private int life;
	private float cd;

	// Use this for initialization
	void Start () {
		// might change for game balance

		this.Restart();
	}
	
	// Update is called once per frame
	void Update () {

		if (this.life <= 0)
			return;

		if (this.cd > 0)
			this.cd -= Time.deltaTime;

		if (this.cd <= 0) 
		{
			// choose which attack + animation to do 
		}
	}

	public void UseCristal()
	{
		GetDamage(50);
	}

	public void GetDamage(int damage)
	{
		this.life -= damage;
	}

	public void Restart()
	{
		this.life = 500;

		this.cd = 0;
	}

	#region Getters/Setters
	public int Life
	{
		get { return this.life;}
	}
	#endregion
}
