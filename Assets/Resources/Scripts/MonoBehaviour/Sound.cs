﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioClips { Walk1, Walk2, Run1, Run2, Run3, Button, Bag};

public class Sound : MonoBehaviour
{

    private AudioSource source;
    private List<float[]> coolDown = new List<float[]>();
    private static AudioClip[] AudioclipArray = new AudioClip[7]
    { Resources.Load<AudioClip>("Sounds/Player/Walk1"),
     Resources.Load<AudioClip>("Sounds/Player/Walk2"),
     Resources.Load<AudioClip>("Sounds/Player/Run1"),
     Resources.Load<AudioClip>("Sounds/Player/Run2"),
     Resources.Load<AudioClip>("Sounds/Player/Run3"),
     Resources.Load<AudioClip>("Sounds/Button/Button"),
     Resources.Load<AudioClip>("Sounds/Button/Bag")};

    // Use this for initialization
    void Start()
    {
        this.source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        int n = this.coolDown.Count;
        while (i < n)
        {
            this.coolDown[i][1] -= Time.deltaTime;
            if (coolDown[i][1] <= 0)
            {
                this.coolDown.RemoveAt(i);
                n--;
            }
            i++;
        }
    }

    /// <sumary>
    /// Joue un son avec un volume choisi.
    /// </sumary>
    public void PlaySound(AudioClips clip, float vol)
    {
        this.source.PlayOneShot(AudioclipArray[(int) clip], vol);
    }

    /// <sumary>
    /// Joue un son avec un volume choisi et cree un cooldown.
    /// </sumary>
    public void PlaySound(AudioClips clip, float vol, float coolDown)
    {
        this.source.PlayOneShot(AudioclipArray[(int)clip], vol);
        this.coolDown.Add(new float[2] { AudioclipArray[(int)clip].GetInstanceID(), coolDown });
    }

    /// <sumary>
    /// Joue un son avec un volume choisi et cree un cooldown avec un id choisi.
    /// </sumary>
    public void PlaySound(AudioClips clip, float vol, float coolDown, float idCoolDown)
    {
        this.source.PlayOneShot(AudioclipArray[(int)clip], vol);
        this.coolDown.Add(new float[2] { idCoolDown, coolDown });
    }

    /// <sumary>
    /// Joue un son random parmi les audioclip avec un volume choisi et cree un cooldown avec un id choisi.
    /// </sumary>
    public void PlaySound(float vol, float coolDown, float idCoolDown, params AudioClips[] clips)
    {
        if (clips.Length > 0)
        {            
            this.source.PlayOneShot(AudioclipArray[(int) clips[Random.Range(0, clips.Length)]], vol);
            this.coolDown.Add(new float[2] { idCoolDown, coolDown });
        }
    }

    /// <sumary>
    /// Permet de savoir si le son peut etre joue.
    /// </sumary>
    public bool IsReady(AudioClip clip)
    {
        foreach (float[] item in this.coolDown)
        {
            if (clip.GetInstanceID() == item[0])
                return false;
        }
        return true;
    }

    /// <sumary>
    /// Permet de savoir si les different morceaux de son peuvent etre joue.
    /// </sumary>
    public bool IsReady(float idCoolDown)
    {
        foreach (float[] item in this.coolDown)
        {
            if (idCoolDown == item[0])
                return false;
        }
        return true;
    }

}
