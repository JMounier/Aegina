﻿using UnityEngine;
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
    private bool isCoop;
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
            string[] world = File.ReadAllLines(this.worldPath + "properties");
            string[] properties = world[0].Split('|');
            this.seed = int.Parse(properties[0]);
            this.isCoop = bool.Parse(properties[1]);
            this.dnc.SetTime(float.Parse(properties[2]));
            Stats.Load(world[1]);
            Success.Update(false);
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
        Success.Update();
    }

    /// <summary>
    /// Sauvegtarde toutes les donnes du monde.
    /// </summary>
    public void SaveWorld()
    {
        Stats.IncrementTimePlayer(60 - (ulong)this.coolDownSave);
        this.coolDownSave = 60;

        File.WriteAllText(this.worldPath + "properties", this.seed.ToString() + "|" +
            this.isCoop.ToString() + "|" + ((int)this.dnc.ActualTime).ToString() + "\n" + Stats.Save());

        foreach (ChunkSave cs in this.chunks)
            cs.Save();
        int i = 0;
        while (i < this.players.Count)
            if (this.players[i].Player == null)
                this.players.RemoveAt(i);
            else
            {
                this.players[i].Save();
                i++;
            }
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
    /// Retourne le ChunkSave du chunk correspondant.
    /// </summary>
    /// <param name="x">La position x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    /// <returns></returns>
    public ChunkSave LoadChunk(int x, int y)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                return cs;
        throw new System.ArgumentException("Chunk wasn't tracked");
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
    /// Sauvegarde la destruction d'un wortop du chunk
    /// </summary>
    public void SaveDestroyedWorktop(int x, int y, Element worktop)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                for (int i = 0; i < cs.WorkTops.Count; i++)
                {
                    if (worktop.Prefab.transform.position == cs.WorkTops[i].Item2)
                    {
                        cs.WorkTops.RemoveAt(i);
                        break;
                    }
                }

    }

    /// <summary>
    /// Sauvegarde la construction d'un wortop du chunk
    /// </summary>
    public void SaveBuildWorktop(int x, int y, Element worktop)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                cs.WorkTops.Add(new Triple<Element, Vector3, Vector3>(worktop, worktop.Prefab.transform.position, worktop.Prefab.transform.rotation.eulerAngles));
    }

    /// <summary>
    /// Ajoute un joueur a la sauvegarde.
    /// A appeller lors de la connexion d'un joueur.
    /// </summary>
    /// <param name="go"></param>
    public void AddPlayer(GameObject go, bool isHost)
    {
        this.players.Add(new PlayerSave(go, this.playersPath, isHost));
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
    public static void CreateWorld(string name, int seed, bool coop)
    {
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name);
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name + "/Chunks");
        Directory.CreateDirectory(Application.dataPath + "/Saves/" + name + "/Players");
        File.WriteAllText(Application.dataPath + "/Saves/" + name + "/properties", seed.ToString() + "|" + coop.ToString() + "|0\n" +
            // Stats
            "0|0|0|0|0||||");
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
public class ChunkSave
{
    private int x;
    private int y;
    private List<int> idSave;
    private List<Triple<Element, Vector3, Vector3>> workTops;
    private string path;
    private int[] cristal;

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
        this.workTops = new List<Triple<Element, Vector3, Vector3>>();
        this.path = pathChunk + x.ToString() + "_" + y.ToString();
        this.cristal = new int[6];

        if (File.Exists(this.path))
        {
            string[] lines = File.ReadAllLines(this.path);
            foreach (string str in lines[0].Split('|'))
            {
                int id;
                if (int.TryParse(str, out id))
                    this.idSave.Add(id);
            }
            foreach (string str in lines[1].Split('|'))
            {
                string[] components = str.Split(':');
                int id;
                if (int.TryParse(components[0], out id))
                {
                    float posX = float.Parse(components[1]);
                    float posY = float.Parse(components[2]);
                    float posZ = float.Parse(components[3]);
                    float rotX = float.Parse(components[4]);
                    float rotY = float.Parse(components[5]);
                    float rotZ = float.Parse(components[6]);
                    this.workTops.Add(new Triple<Element, Vector3, Vector3>(EntityDatabase.Find(id) as Element,
                        new Vector3(posX, posY, posZ), new Vector3(rotX, rotY, rotZ)));
                }
            }
            if (lines.Length > 2)
            {
                string[] cristalCaract = lines[2].Split('|');
                for (int i = 0; i < 6; i++)
                    this.cristal[i] = int.Parse(cristalCaract[i]);
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
            file.Write("\n");
            foreach (Triple<Element, Vector3, Vector3> wt in this.workTops)
            {
                file.Write(wt.Item1.ID.ToString() + ":" + wt.Item2.x.ToString() + ":" + wt.Item2.y.ToString() + ":" + wt.Item2.z.ToString() + ":"
                      + wt.Item3.x.ToString() + ":" + wt.Item3.y.ToString() + ":" + wt.Item3.x.ToString() + "|");
            }
            file.Write("\n");
            file.Write(cristal[0].ToString() + "|" + cristal[1].ToString() + "|" + cristal[2].ToString() + "|" + cristal[3].ToString() + "|" +
             cristal[4].ToString() + "|" + cristal[5].ToString());
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

    public List<Triple<Element, Vector3, Vector3>> WorkTops
    {
        get { return this.workTops; }
        set { this.workTops = value; }
    }
    public int[] CristalCaracteristics
    {
        set { this.cristal = value; }
        get { return this.cristal; }
    }
}

/// <summary>
/// Represente la sauvegarder d'un joueur.
/// </summary>
public class PlayerSave
{
    private float x, y, life, hunger, thirst, speed, cdSpeed, jump, cdJump, regen, cdRegen, poison, cdPoison;
    private string inventory, namePlayer, path;
    private bool isOp;

    GameObject player;

    public PlayerSave(GameObject go, string pathPlayer, bool isServer)
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
            this.regen = float.Parse(properties[9]);
            this.cdRegen = float.Parse(properties[10]);
            this.poison = float.Parse(properties[11]);
            this.cdPoison = float.Parse(properties[12]);

            this.inventory = save[1];
            this.isOp = bool.Parse(save[2]) || isServer;
        }
        else
        {
            Vector3 newPos = new Vector3(Random.Range(-10f, 10f), 7, Random.Range(-10f, 10f));
            while (!Graph.isValidPosition(newPos))
                newPos = new Vector3(Random.Range(-10f, 10f), 7, Random.Range(-10f, 10f));
            this.x = newPos.x;
            this.y = newPos.z;
            this.life = 100;
            this.hunger = 100;
            this.thirst = 100;
            this.speed = 0;
            this.cdSpeed = 0;
            this.jump = 0;
            this.cdJump = 0;
            this.regen = 0;
            this.cdRegen = 0;
            this.poison = 0;
            this.cdPoison = 0;
            this.inventory = "";
            this.isOp = isServer;
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
        this.regen = this.Player.GetComponent<SyncCharacter>().Regen;
        this.cdRegen = this.Player.GetComponent<SyncCharacter>().CdRegen;
        this.poison = this.Player.GetComponent<SyncCharacter>().Poison;
        this.cdPoison = this.Player.GetComponent<SyncCharacter>().CdPoison;
        this.isOp = this.Player.GetComponent<Social>().IsOp;

        using (StreamWriter file = new StreamWriter(this.path))
        {
            file.WriteLine(this.x.ToString() + '|' + this.y.ToString() + '|' + this.life.ToString() + '|' + this.hunger.ToString() + '|' + this.thirst.ToString() +
                '|' + this.speed.ToString() + '|' + this.cdSpeed.ToString() + '|' + this.jump.ToString() + '|' + this.cdJump.ToString()
                + '|' + this.regen.ToString() + '|' + this.cdRegen.ToString() + '|' + this.poison.ToString() + '|' + this.cdPoison.ToString());
            file.WriteLine(this.inventory);
            file.WriteLine(this.isOp.ToString());
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

    public float Regen
    {
        get { return this.regen; }
        set { this.regen = value; }
    }

    public float CdRegen
    {
        get { return this.cdRegen; }
        set { this.cdRegen = value; }
    }

    public float Poison
    {
        get { return this.poison; }
        set { this.poison = value; }
    }

    public float CdPoison
    {
        get { return this.cdPoison; }
        set { this.cdPoison = value; }
    }

    public string Inventory
    {
        get { return this.inventory; }
        set { this.inventory = value; }
    }

    public bool IsOp
    {
        get { return this.isOp; }
        set { this.isOp = value; }
    }

    public GameObject Player
    {
        get { return this.player; }
        set { this.player = value; }
    }
}