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
    private float finalcoutndown;

    // Use this for initialization
    void Start()
    {

        this.spawnWall = gameObject.transform.GetChild(0).gameObject;
        this.SpecCamPos = gameObject.transform.GetChild(1).gameObject;

        this.OrbitingStuff = new List<GameObject>();

        this.OrbitingStuff.Add(this.SpecCamPos.transform.GetChild(0).gameObject);

        this.trycount = 0;
        this.finalcoutndown = 105;

        this.spawnWall.SetActive(true);
        this.Specpos = 0;

        foreach (GameObject obj in OrbitingStuff)
        {
            obj.transform.LookAt(obj.transform.parent);
        }

        this.NotSpecAnyMore();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Tutoriel>().Story(TextDatabase.PreBossBattle1, TextDatabase.PreBossBattle2, TextDatabase.PreBossBattle3, TextDatabase.PreBossBattle4, TextDatabase.PreBossBattle5, TextDatabase.PreBossBattle6, TextDatabase.PreBossBattle7, TextDatabase.PreBossBattle8, TextDatabase.PreBossBattle9, TextDatabase.PreBossBattle10, TextDatabase.PreBossBattle11, TextDatabase.PreBossBattle12, TextDatabase.PreBossBattle13, TextDatabase.PreBossBattle14, TextDatabase.PreBossBattle15, TextDatabase.PreBossBattle16, TextDatabase.PreBossBattle17, TextDatabase.PreBossBattle18, TextDatabase.PreBossBattle19, TextDatabase.PreBossBattle20);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (GameObject obj in OrbitingStuff)
            obj.transform.RotateAround(obj.transform.parent.position, obj.transform.parent.up, 0.5f);
        if (finalcoutndown > 0)
            finalcoutndown -= Time.deltaTime;
        else if (finalcoutndown <= 0 && finalcoutndown > -5)
        {
            this.spawnWall.SetActive(false);
            finalcoutndown = -42;
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

    public float FinalCountdown
    {
        set { this.finalcoutndown = value; }
    }
    #endregion

}
