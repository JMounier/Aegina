using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public enum AudioClips { Void, Walk1, Walk2, Walk3, Run1, Run2, Run3, Button, Bag, Forest, Desert, Plop };

public class Sound : NetworkBehaviour
{

    private AudioSource source;
    private List<float[]> coolDown = new List<float[]>();
    private static AudioClip[] AudioclipArray = new AudioClip[12];
    private float volume = 0.1f;

    // Use this for initialization
    void Awake()
    {
        AudioclipArray[0] = Resources.Load<AudioClip>("Sounds/Void");
        AudioclipArray[1] = Resources.Load<AudioClip>("Sounds/Player/Walk1");
        AudioclipArray[2] = Resources.Load<AudioClip>("Sounds/Player/Walk2");
        AudioclipArray[3] = Resources.Load<AudioClip>("Sounds/Player/Walk3");
        AudioclipArray[4] = Resources.Load<AudioClip>("Sounds/Player/Run1");
        AudioclipArray[5] = Resources.Load<AudioClip>("Sounds/Player/Run2");
        AudioclipArray[6] = Resources.Load<AudioClip>("Sounds/Player/Run3");
        AudioclipArray[7] = Resources.Load<AudioClip>("Sounds/Button/Button");
        AudioclipArray[8] = Resources.Load<AudioClip>("Sounds/Button/Bag");
        AudioclipArray[9] = Resources.Load<AudioClip>("Sounds/Music/Forest");
        AudioclipArray[10] = Resources.Load<AudioClip>("Sounds/Music/Desert");
        AudioclipArray[11] = Resources.Load<AudioClip>("Sounds/Button/Plop");
        this.source = gameObject.GetComponentInChildren<AudioSource>();
        this.volume = PlayerPrefs.GetFloat("Sound_intensity", 0.1f);
        this.source.volume = this.volume;
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            this.PlaySound(AudioClips.Void, 0f, Random.Range(30, 150), 42);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

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

        if (this.IsReady(42))
        {
            this.PlaySound(2f, Random.Range(420, 840), 42, AudioClips.Desert, AudioClips.Forest);
        }
    }
    /// <sumary>
    /// Joue un son avec un volume choisi.
    /// </sumary>
    public void PlaySound(AudioClips clip, float vol)
    {
        this.source.PlayOneShot(AudioclipArray[(int)clip], vol);
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
            this.source.PlayOneShot(AudioclipArray[(int)clips[Random.Range(0, clips.Length)]], vol);
            this.coolDown.Add(new float[2] { idCoolDown, coolDown });
        }
    }

    /// <sumary>
    /// Joue un son random parmi les audioclip avec un volume choisi et cree un cooldown avec un id choisi.
    /// </sumary>
    [ClientRpc]
    private void RpcPlaySound(AudioClips clip, float vol, float coolDown, float idCoolDown)
    {
        PlaySound(clip, vol, coolDown, idCoolDown);
    }

    /// <sumary>
    /// Joue un son synchronise random parmi les audioclip avec un volume choisi et cree un cooldown avec un id choisi.
    /// </sumary>
    [Command]
    public void CmdPlaySound(AudioClips clip, float vol, float coolDown, float idCoolDown)
    {
        RpcPlaySound(clip, vol, coolDown, idCoolDown);
    }

    /// <sumary>
    /// Permet de savoir si le son peut etre joue.
    /// </sumary>
    public bool IsReady(AudioClips clip)
    {
        foreach (float[] item in this.coolDown)
        {
            if (AudioclipArray[(int)clip].GetInstanceID() == item[0])
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
    public float Volume
    {
        get { return this.volume; }
        set
        {
            this.volume = Mathf.Clamp(value, 0f, 1f);
            this.source.volume = this.volume;
        }
    }
}
