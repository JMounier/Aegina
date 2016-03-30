using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;

public class Save : NetworkBehaviour
{

    // Usefull
    private float coolDownSave = 60;

    // Composants
    private DayNightCycle dnc;

    // Chemins
    private string worldPath;
    private string chunksPath;
    private string playersPath;

    //Variables
    private string nameWorld;
    private int seed;
    private List<ChunkSave> chunks;
    private List<PlayerSave> players;

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.dnc = gameObject.GetComponent<DayNightCycle>();
            this.nameWorld = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().World;

            this.chunks = new List<ChunkSave>();
            this.players = new List<PlayerSave>();

            this.worldPath = Application.dataPath + "/Saves/" + this.nameWorld + "/";
            this.chunksPath = worldPath + "/Chunks/";
            this.playersPath = worldPath + "/Players/";

            // Set world properties
            string[] properties = File.ReadAllText(this.worldPath + "properties").Split('|');
            this.seed = int.Parse(properties[0]);
            this.dnc.SetTime(float.Parse(properties[1]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        this.coolDownSave -= Time.deltaTime;
        if (this.coolDownSave <= 0)
            this.SaveWorld();
    }

    /// <summary>
    /// Sauvegtarde toutes les donnes du monde.
    /// </summary>
    public void SaveWorld()
    {
        this.coolDownSave = 60;
        File.WriteAllText(this.worldPath + "properties", this.seed.ToString() + "|" + ((int)this.dnc.ActualTime).ToString());
        foreach (ChunkSave cs in this.chunks)
            cs.Save();
        foreach (PlayerSave ps in this.players)
            if (ps.Player == null)
                this.players.Remove(ps);
            else
                ps.Save();
    }

    /// <summary>
    /// Ajoute a la sauvegarde un chunk.
    /// A appeller lorsque l'on souhaite genere un chunk.
    /// </summary>
    /// <param name="x">La position x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    public void AddChunk(int x, int y)
    {
        this.chunks.Add(new ChunkSave(x, y, this.chunksPath));
    }

    /// <summary>
    /// Sauvegarde un joueur et l'enleve du tracking.
    /// A appeller lorsque l'on souhaite degenere un chunk.
    /// </summary>
    /// <param name="x">La position x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    public void RemoveChunk(int x, int y)
    {
        for (int i = 0; i < this.chunks.Count; i++)
            if (this.chunks[i].X == x && this.chunks[i].Y == y)
            {
                this.chunks[i].Save();
                this.chunks.RemoveAt(i);
                break;
            }
    }

    /// <summary>
    /// Retourne la liste des id des elements modifie sur le chunk.
    /// </summary>
    /// <param name="x">La position x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    /// <returns></returns>
    public List<int> LoadChunk(int x, int y)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                return cs.ListIdSave;
        return new List<int>();
    }

    /// <summary>
    /// Sauvegarde un la destruction d'un element dans sont chunk.
    /// </summary>
    /// <param name="x">La positoin x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    /// <param name="idSave">L'id correspondant a l'element detruit.</param>
    public void SaveDestroyedElement(int x, int y, int idSave)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                cs.SaveDestroyedElement(idSave);
    }

    /// <summary>
    /// Ajoute un joueur a la sauvegarde.
    /// A appeller lors de la connexion d'un joueur.
    /// </summary>
    /// <param name="go"></param>
    public void AddPlayer(GameObject go)
    {
        this.players.Add(new PlayerSave(go, this.playersPath));
    }

    /// <summary>
    /// Sauvegarde un joueur et l'enleve du tracking.
    /// A appeller lors de la deconnexion de celui-ci.
    /// </summary>
    /// <param name="go"></param>
    public void RemovePlayer(GameObject go)
    {
        foreach (PlayerSave player in this.players)
            if (player.Player.Equals(go))
            {
                player.Save();
                this.players.Remove(player);
                break;
            }
    }

    /// <summary>
    /// Charge le joueur en retournant son PlayerSave.
    /// </summary>
    /// <param name="go"></param>
    public PlayerSave LoadPlayer(GameObject go)
    {
        foreach (PlayerSave player in this.players)
            if (player.Player.Equals(go))
                return player;
        throw new System.Exception("Player not added or doesn't exist");
    }

    /// <summary>
    /// Charge le joueur en retournant son PlayerSave.
    /// </summary>
    /// <param name="go"></param>
    public void SavePlayerInventory(GameObject go, string inventory)
    {
        foreach (PlayerSave player in this.players)
            if (player.Player.Equals(go))
                player.Inventory = inventory;
    }

    /// <summary>
    /// Cree un monde.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="seed"></param>
    public static void CreateWorld(string name, int seed)
    {
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name);
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name + "/Chunks");
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name + "/Players");
        File.WriteAllText(Application.dataPath + "/Saves/" + name + "/properties", seed.ToString() + "|0");
    }

    // Getter & Setters
    public int Seed
    {
        get { return this.seed; }
    }
}

/// <summary>
/// Represente la sauvegarde d'un chunk.
/// </summary>
class ChunkSave
{
    private int x;
    private int y;
    private List<int> idSave;
    private string path;

    /// <summary>
    /// Creer un nouveau ChunkSave et charge sa sauvegarde si existante sinon la cree.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="pathChunk"></param>
    public ChunkSave(int x, int y, string pathChunk)
    {
        this.x = x;
        this.y = y;
        this.idSave = new List<int>();
        this.path = pathChunk + x.ToString() + "_" + y.ToString();
        if (File.Exists(this.path))
        {
            foreach (string str in File.ReadAllText(this.path).Split('|'))
            {
                int id;
                if (int.TryParse(str, out id))
                    this.idSave.Add(id);
            }
        }
        else
            File.Create(this.path);
    }

    /// <summary>
    /// Sauvegarde un element comme detruit sur ce chunk.
    /// </summary>
    /// <param name="id">L'identifiant de sauvegarde de l'element</param>
    public void SaveDestroyedElement(int id)
    {
        int d = 0;
        int f = this.idSave.Count - 1;
        int m;
        while (d <= f)
        {
            m = (d + f) / 2;
            if (id > this.idSave[m])
                d = m + 1;
            else
                f = m - 1;
        }
        this.idSave.Insert(d, id);
    }

    /// <summary>
    /// Sauvegarde le chunk.
    /// </summary>
    public void Save()
    {
        using (StreamWriter file = new StreamWriter(this.path))
        {
            foreach (int id in this.idSave)
                file.Write(id.ToString() + '|');
        }
    }

    // Getters & Setters
    /// <summary>
    /// La position X du chunk.
    /// </summary>
    public int X
    {
        get { return this.x; }
        set { this.x = value; }
    }

    /// <summary>
    /// La position Y du chunk.
    /// </summary>
    public int Y
    {
        get { return this.y; }
        set { this.y = value; }
    }

    /// <summary>
    /// La liste des id des elements modifies sur le chunk.
    /// </summary>
    public List<int> ListIdSave
    {
        get { return this.idSave; }
    }
}

/// <summary>
/// Represente la sauvegarder d'un joueur.
/// </summary>
public class PlayerSave
{
    private float x, y, life, hunger, thirst, speed, cdSpeed, jump, cdJump;
    private string inventory, namePlayer, path;
    GameObject player;

    public PlayerSave(GameObject go, string pathPlayer)
    {
        this.namePlayer = go.GetComponent<Social>().PlayerName;
        this.path = pathPlayer + namePlayer;
        this.player = go;

        if (File.Exists(this.path))
        {
            string[] save = File.ReadAllLines(this.path);
            string[] properties = save[0].Split('|');

            this.x = float.Parse(properties[0]);
            this.y = float.Parse(properties[1]);
            this.life = float.Parse(properties[2]);
            this.hunger = float.Parse(properties[3]);
            this.thirst = float.Parse(properties[4]);
            this.speed = float.Parse(properties[5]);
            this.cdSpeed = float.Parse(properties[6]);
            this.jump = float.Parse(properties[7]);
            this.cdJump = float.Parse(properties[8]);

            this.inventory = save[1];
        }
        else
        {
            this.x = Random.Range(-10f, 10f);
            this.y = Random.Range(-10f, 10f);
            this.life = 100;
            this.hunger = 100;
            this.thirst = 100;
            this.speed = 0;
            this.cdSpeed = 0;
            this.jump = 0;
            this.cdJump = 0;

            this.inventory = "";
        }
    }

    public void Save()
    {
        this.x = this.Player.transform.FindChild("Character").position.x;
        this.y = this.Player.transform.FindChild("Character").position.z;
        this.life = this.Player.GetComponent<SyncCharacter>().Life;
        this.hunger = this.Player.GetComponent<SyncCharacter>().Hunger;
        this.thirst = this.Player.GetComponent<SyncCharacter>().Thirst;
        this.speed = this.Player.GetComponent<SyncCharacter>().Speed;
        this.cdSpeed = this.Player.GetComponent<SyncCharacter>().CdSpeed;
        this.jump = this.Player.GetComponent<SyncCharacter>().Jump;
        this.cdJump = this.Player.GetComponent<SyncCharacter>().CdJump;

        using (StreamWriter file = new StreamWriter(this.path))
        {
            file.WriteLine(this.x.ToString() + '|' + this.y.ToString() + '|' + this.life.ToString() + '|' + this.hunger.ToString() + '|' + this.thirst.ToString() +
                '|' + this.speed.ToString() + '|' + this.cdSpeed.ToString() + '|' + this.jump.ToString() + '|' + this.cdJump.ToString());
            file.WriteLine(this.inventory);
        }
    }

    public float X
    {
        get { return this.x; }
        set { this.x = value; }
    }

    public float Y
    {
        get { return this.y; }
        set { this.y = value; }
    }

    public float Life
    {
        get { return this.life; }
        set { this.life = value; }
    }

    public float Hunger
    {
        get { return this.hunger; }
        set { this.hunger = value; }
    }

    public float Thirst
    {
        get { return this.thirst; }
        set { this.thirst = value; }
    }

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    public float CdSpeed
    {
        get { return this.cdSpeed; }
        set { this.cdSpeed = value; }
    }

    public float Jump
    {
        get { return this.jump; }
        set { this.jump = value; }
    }

    public float CdJump
    {
        get { return this.cdJump; }
        set { this.cdJump = value; }
    }

    public string Inventory
    {
        get { return this.inventory; }
        set { this.inventory = value; }
    }

    public GameObject Player
    {
        get { return this.player; }
        set { this.player = value; }
    }
}