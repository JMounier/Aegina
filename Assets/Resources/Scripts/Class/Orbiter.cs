using UnityEngine;
using System.Collections;

public class Orbiter {

	GameObject center;

	public Orbiter(Vector3 pos, Vector3 rot)
	{
		center = GameObject.Instantiate (Resources.Load<GameObject> ("PrefabsNoNetID/Others/Orbiter"));
		this.move (pos);
		this.rotate (rot);
	}

	public void move(Vector3 pos)
	{
		center.transform.position = pos;
	}

	public void rotate(Vector3 rot)
	{
		center.transform.rotation = Quaternion.Euler (rot);
	}

	public void clear()
	{
		GameObject.Destroy (center);
	}

	public void show(bool enable)
	{
		center.GetComponent<Renderer> ().enabled = enable;
	}

	public void start()
	{
		center.GetComponent<Renderer> ().enabled = false;
	}
	public GameObject Center
	{
		get{ return this.center;}
	}

	public Vector3 RotAxis
	{
		get { return this.center.transform.up; }
	}
}
