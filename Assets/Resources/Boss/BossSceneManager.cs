using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSceneManager : MonoBehaviour
{

    private GameObject spawnWall;
    private GameObject SpecCamPos;

	private int trycount;

    private List<GameObject> OrbitingStuff;

    private List<GameObject> PlayersInFight;

    private int Specpos;

	private bool won;
    // Use this for initialization
    void Start()
    {

		this.won = false;

        this.spawnWall = gameObject.transform.GetChild(0).gameObject;
        this.SpecCamPos = gameObject.transform.GetChild(1).gameObject;

        this.OrbitingStuff = new List<GameObject>();

        this.OrbitingStuff.Add(this.SpecCamPos.transform.GetChild(0).gameObject);

		this.trycount = 0;


        this.spawnWall.SetActive(false);
        this.Specpos = 0;

        foreach (GameObject obj in OrbitingStuff)
        {
            obj.transform.LookAt(obj.transform.parent);
        }

		this.NotSpecAnyMore ();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (GameObject obj in OrbitingStuff)
            obj.transform.RotateAround(obj.transform.parent.position, obj.transform.parent.up, 0.5f);

		//check the win in succes
    }

	private void OnGUI()
	{
		if (this.won)
		{
			//draw victory
		}
		else
		{
			//draw the boss life
			//draw the try count
		}
	}

    /// <summary>
    /// only call this method when the player is dead;
    /// </summary>
    /// <param name="delta"></param>
    public void SwitchView(int delta)
    {
        this.SpecCamPos.transform.GetChild(this.Specpos).gameObject.SetActive(false);
        this.Specpos = (this.Specpos + 4) % 4;
        this.SpecCamPos.transform.GetChild(this.Specpos).gameObject.SetActive(true);
    }

    public void NotSpecAnyMore()
    {
        this.spawnWall.SetActive(false);
        this.SpecCamPos.transform.GetChild(this.Specpos).gameObject.SetActive(false);
        this.Specpos = 0;
    }

	public void IncreaseTryCount()
	{
		this.trycount++;
	}

    #region Getters/Setters
    public GameObject SpawnWall
    {
        get { return this.spawnWall; }
    }

	public bool Won
	{
		get { return this.won;}
		set { this.won = this.won || value;}
	}

    #endregion

}
