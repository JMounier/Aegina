﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSceneManager : MonoBehaviour
{

    private GameObject spawnWall;
    private GameObject SpecCamPos;
    private GameObject Spawn;

    private List<GameObject> OrbitingStuff;

    private List<GameObject> PlayersInFight;

    private int Specpos;

    // Use this for initialization
    void Start()
    {

        this.spawnWall = gameObject.transform.GetChild(0).gameObject;
        this.SpecCamPos = gameObject.transform.GetChild(1).gameObject;
        this.Spawn = gameObject.transform.GetChild(2).gameObject;

        this.OrbitingStuff = new List<GameObject>();

        this.OrbitingStuff.Add(this.SpecCamPos.transform.GetChild(0).gameObject);




        this.spawnWall.SetActive(false);
        this.Specpos = 0;

        foreach (GameObject obj in OrbitingStuff)
        {
            obj.transform.LookAt(obj.transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in OrbitingStuff)
            obj.transform.RotateAround(obj.transform.parent.position, obj.transform.parent.up, 0.5f);
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

    #region Getters/Setters
    public GameObject SpawnWall
    {
        get { return this.spawnWall; }
    }
    #endregion

}
