using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sound : MonoBehaviour
{

    private AudioSource source;
    private List<float[]> coolDown = new List<float[]>();

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
    public void PlaySound(AudioClip clip, float vol)
    {
        this.source.PlayOneShot(clip, vol);
    }

    /// <sumary>
    /// Joue un son avec un volume choisi et cree un cooldown.
    /// </sumary>
    public void PlaySound(AudioClip clip, float vol, float coolDown)
    {
        this.source.PlayOneShot(clip, vol);
        this.coolDown.Add(new float[2] { clip.GetInstanceID(), coolDown });
    }

    /// <sumary>
    /// Joue un son avec un volume choisi et cree un cooldown avec un id choisi.
    /// </sumary>
    public void PlaySound(AudioClip clip, float vol, float coolDown, float idCoolDown)
    {
        this.source.PlayOneShot(clip, vol);
        this.coolDown.Add(new float[2] { idCoolDown, coolDown });
    }

    /// <sumary>
    /// Joue un son random parmi les audioclip avec un volume choisi et cree un cooldown avec un id choisi.
    /// </sumary>
    public void PlaySound(float vol, float coolDown, float idCoolDown, params AudioClip[] clips)
    {
        if (clips.Length > 0)
        {            
            this.source.PlayOneShot(clips[Random.Range(0, clips.Length)], vol);
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
