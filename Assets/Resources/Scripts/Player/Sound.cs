using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public enum AudioClips { Void, Walk1, Walk2, Walk3, Run1, Run2, Run3, chopping, mining, playerAttack, cooking, playerDeath, drinking, eating, forge, picking, workbensh, Button, Bag, Plop, Forest, Desert, Winter, Autumn };

public class Sound : NetworkBehaviour
{

    private AudioSource source;
    private List<float[]> coolDown = new List<float[]>();
    private static AudioClip[] AudioclipArray;
    private float volume = 0.1f;

    // Use this for initialization
    void Awake()
    {
        AudioclipArray = new AudioClip[] {
            Resources.Load<AudioClip>("Sounds/Void"),

            Resources.Load<AudioClip>("Sounds/Player/Walk1"),
            Resources.Load<AudioClip>("Sounds/Player/Walk2"),
            Resources.Load<AudioClip>("Sounds/Player/Walk3"),
            Resources.Load<AudioClip>("Sounds/Player/Run1"),
            Resources.Load<AudioClip>("Sounds/Player/Run2"),
            Resources.Load<AudioClip>("Sounds/Player/Run3"),
            Resources.Load<AudioClip>("Sounds/Player/Chopping"),
            Resources.Load<AudioClip>("Sounds/Player/Mining"),
            Resources.Load<AudioClip>("Sounds/Player/Attack"),
            Resources.Load<AudioClip>("Sounds/Player/CookingPot"),
            Resources.Load<AudioClip>("Sounds/Player/Death"),
            Resources.Load<AudioClip>("Sounds/Player/Drink"),
            Resources.Load<AudioClip>("Sounds/Player/Eat"),
            Resources.Load<AudioClip>("Sounds/Player/Forge"),
            Resources.Load<AudioClip>("Sounds/Player/Pickup"),
            Resources.Load<AudioClip>("Sounds/Player/Workbensh"),

            Resources.Load<AudioClip>("Sounds/Button/Button"),
            Resources.Load<AudioClip>("Sounds/Button/Bag"),
            Resources.Load<AudioClip>("Sounds/Button/Plop"),

            Resources.Load<AudioClip>("Sounds/Music/Forest"),
            Resources.Load<AudioClip>("Sounds/Music/Desert"),
            Resources.Load<AudioClip>("Sounds/Music/Winter"),
            Resources.Load<AudioClip>("Sounds/Music/Autumn")
            };

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
            else
            {
                i++;
            }
        }
        if (this.IsReady(42))
        {
            AudioClips clip = this.Getbiome();
            if (clip == AudioClips.Void)
                this.PlaySound(2f, Random.Range(30, 60), 42, clip);
            else
                this.PlaySound(2f, Random.Range(420, 840), 42, clip);
        }

    }
    /// <sumary>
    /// Joue un son avec un volume choisi.
    /// </sumary>
    public void PlaySound(AudioClips clip, float vol)
    {
        this.source.PlayOneShot(AudioclipArray[(int)clip], vol);
    }

    /// <summary>
    /// Choisi la musique du joueur avec son volume.
    /// </summary>
    [ClientRpc]
    public void RpcChooseSound(AudioClips clip, float vol)
    {
        if (clip == AudioClips.Forest || clip == AudioClips.Desert || clip == AudioClips.Winter)
        {
            this.source.Stop();
            this.PlaySound(vol, Random.Range(420, 840), 42, clip);
        }
        else
            PlaySound(clip, vol);
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

    public AudioClips Getbiome()
    {
        GameObject character = gameObject.GetComponentInChildren<CharacterCollision>().gameObject;
        foreach (Collider col in Physics.OverlapBox(character.transform.position, new Vector3(1, 100, 1)))
            if (col.gameObject.name.Contains("Island") && col.CompareTag("Ground"))
            {
                switch (col.gameObject.GetComponentInParent<SyncChunk>().BiomeId)
                {
                    case 0:
                        return AudioClips.Forest;
                    case 1:
                        return AudioClips.Desert;
                    case 2:
                        return AudioClips.Winter;
                    default:
                        return AudioClips.Autumn;
                }
            }
        return AudioClips.Void;
    }

    /// <sumary>
    /// Demande aux autre client de jouer un son avec un volume precis.
    /// </sumary>
    [ClientRpc]
    private void RpcPlaySound(AudioClips clip, float vol)
    {
        if (!isLocalPlayer)
            PlaySound(clip, vol);
    }

    /// <sumary>
    /// Joue un son pour les autres joueurs avec un volume choisi.
    /// </sumary>
    [Command]
    public void CmdPlaySound(AudioClips clip, float vol)
    {
        RpcPlaySound(clip, vol);
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

    /// <sumary>
    /// Permet de changer le volume sonore.
    /// </sumary>
    public float Volume
    {
        get { return this.volume; }
        set
        {
            this.volume = Mathf.Clamp(value, 0f, 1f);
            this.source.volume = this.volume;
        }
    }

	public AudioSource Source
	{
		set { this.source = value;}
	}
}
