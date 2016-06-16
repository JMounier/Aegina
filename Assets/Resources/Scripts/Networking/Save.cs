using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;

public class Save : NetworkBehaviour
{

    // Usefull
    private float coolDownSave = 60;
    private List<Vector2>[] respawn;

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
            this.respawn = new List<Vector2>[4] { new List<Vector2>(), new List<Vector2>(), new List<Vector2>(), new List<Vector2>() };

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

            Success.Reset();

            // Find cristals
            string[] chunksName = Directory.GetFiles(this.chunksPath);
            foreach (string chunk in chunksName)
            {
                if (!chunk.EndsWith(".meta"))
                {
                    Tuple<Vector2, Team> info = GetCristalInfo(chunk);
                    if (info != null && info.Item2 != Team.Neutre)
                        this.respawn[(int)info.Item2].Add(info.Item1);
                }
            }

            for (int i = 0; i < 4; i++)
                if (this.respawn[i].Count == 0)
                    this.respawn[i].Add(new Vector2(0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;

        this.coolDownSave -= Time.deltaTime;
        if (this.coolDownSave <= 0 || Stats.TimePlayer() == 0)
            this.SaveWorld();
        Success.Update();
    }

    /// <summary>
    /// Sauvegtarde toutes les donnes du monde.
    /// </summary>
    public void SaveWorld()
    {
        if (gameObject.GetComponentInChildren<BossSceneManager>() != null)
            return;
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
                this.chunks[i].RespawnElements();
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
        throw new System.ArgumentException("Chunk (" + x.ToString() + " : " + y.ToString() + ") wasn't tracked");
    }

    /// <summary>
    /// Sauvegarde un la destruction d'un element dans sont chunk.
    /// </summary>
    /// <param name="x">La positoin x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    /// <param name="idSave">L'id correspondant a l'element detruit.</param>
    public void SaveDestroyedElement(int x, int y, int idSave, Vector3 pos)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                cs.SaveDestroyedElement(idSave, pos);
    }

    /// <summary>
    /// Sauvegarde la destruction d'un wortop du chunk
    /// </summary>
    public void SaveDestroyedWorktop(int x, int y, Element worktop)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                if (worktop.ID == 135)
                    for (int i = 0; i < cs.Chests.Count; i++)
                    {
                        if ((worktop.Prefab.transform.position - cs.Chests[i].Item2).magnitude < .5f)
                        {
                            cs.Chests.RemoveAt(i);
                            break;
                        }
                    }
                else
                    for (int i = 0; i < cs.WorkTops.Count; i++)
                        if ((worktop.Prefab.transform.position - cs.WorkTops[i].Item2).magnitude < .5f)
                        {
                            cs.WorkTops.RemoveAt(i);
                            break;
                        }
    }

    /// <summary>
    /// Sauvegarde la construction d'un wortop du chunk
    /// </summary>
    public void SaveBuildWorktop(int x, int y, Element worktop)
    {
        foreach (ChunkSave cs in this.chunks)
            if (cs.X == x && cs.Y == y)
                if (worktop.ID == 135)
                    cs.Chests.Add(new Quadruple<Element, Vector3, Vector3, ItemStack[,]>(worktop, worktop.Prefab.transform.position, worktop.Prefab.transform.rotation.eulerAngles, null));
                else
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
        if (gameObject.GetComponentInChildren<BossSceneManager>() == null)
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
        File.WriteAllText(Application.dataPath + "/Saves/" + name + "/properties", seed.ToString() + "|" + coop.ToString() + "|0\n" + Stats.Empty());
    }

    // Getter & Setters
    public int Seed
    {
        get { return this.seed; }
    }

    public bool IsCoop
    {
        get { return this.isCoop; }
    }

    public List<Vector2>[] Respawn
    {
        get { return this.respawn; }
    }

    private Tuple<Vector2, Team> GetCristalInfo(string chunk)
    {
        string[] lines = File.ReadAllLines(chunk);
        if (lines.Length > 2)
        {
            string[] cristalCaract = lines[2].Split('|');
            Team t = (Team)int.Parse(cristalCaract[0]);
            Vector2 v = new Vector2(float.Parse(cristalCaract[6]), float.Parse(cristalCaract[7]));
            return new Tuple<Vector2, Team>(v, t);
        }
        return null;
    }
}

/// <summary>
/// Represente la sauvegarde d'un chunk.
/// </summary>
public class ChunkSave
{
    private int x;
    private int y;
    private List<Tuple<int, Vector3>> idSave;
    private List<Triple<Element, Vector3, Vector3>> workTops;
    private List<Quadruple<Element, Vector3, Vector3, ItemStack[,]>> chests;
    private string path;
    private float[] cristal;

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
        this.idSave = new List<Tuple<int, Vector3>>();
        this.workTops = new List<Triple<Element, Vector3, Vector3>>();
        this.chests = new List<Quadruple<Element, Vector3, Vector3, ItemStack[,]>>();
        this.path = pathChunk + x.ToString() + "_" + y.ToString();
        this.cristal = new float[8];

        if (File.Exists(this.path))
        {
            string[] lines = File.ReadAllLines(this.path);
            if (lines.Length > 1)
            {
                foreach (string str in lines[0].Split('|'))
                {
                    int id;
                    if (int.TryParse(str, out id))
                        this.idSave.Add(new Tuple<int, Vector3>(id, Vector3.zero));
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
                        if (id == 135)
                        {
                            ItemStack[,] content = new ItemStack[3, 3];
                            for (int i = 0; i < 9; i++)
                                content[i / 3, i % 3] = new ItemStack(ItemDatabase.Find(int.Parse(components[7 + i * 2])), int.Parse(components[8 + i * 2]));
                            this.chests.Add(new Quadruple<Element, Vector3, Vector3, ItemStack[,]>(EntityDatabase.Find(id) as Element,
                            new Vector3(posX, posY, posZ), new Vector3(rotX, rotY, rotZ), content));
                        }
                        else
                            this.workTops.Add(new Triple<Element, Vector3, Vector3>(EntityDatabase.Find(id) as Element,
                                new Vector3(posX, posY, posZ), new Vector3(rotX, rotY, rotZ)));
                    }
                }
                if (lines.Length > 2)
                {
                    string[] cristalCaract = lines[2].Split('|');
                    for (int i = 0; i < 6; i++)
                        this.cristal[i] = int.Parse(cristalCaract[i]);
                    for (int i = 6; i < 8; i++)
                        this.cristal[i] = float.Parse(cristalCaract[i]);
                }
            }
            else
            {
                File.Create(this.path);
                this.cristal[5] = 1000;
            }
        }
        else
        {
            File.Create(this.path);
            this.cristal[5] = 1000;
        }
    }

    /// <summary>
    /// Sauvegarde un element comme detruit sur ce chunk.
    /// </summary>
    /// <param name="id">L'identifiant de sauvegarde de l'element</param>
    public void SaveDestroyedElement(int id, Vector3 pos)
    {
        int d = 0;
        int f = this.idSave.Count - 1;
        int m;
        while (d <= f)
        {
            m = (d + f) / 2;
            if (id > this.idSave[m].Item1)
                d = m + 1;
            else
                f = m - 1;
        }
        this.idSave.Insert(d, new Tuple<int, Vector3>(id, pos));
    }

    /// <summary>
    /// Sauvegarde le chunk.
    /// </summary>
    public void Save()
    {
        try
        {
            using (StreamWriter file = new StreamWriter(this.path))
            {
                foreach (Tuple<int, Vector3> id in this.idSave)
                    file.Write(id.Item1.ToString() + '|');
                file.Write("\n");
                foreach (Triple<Element, Vector3, Vector3> wt in this.workTops)
                    file.Write(wt.Item1.ID.ToString() + ":" + wt.Item2.x.ToString() + ":" + wt.Item2.y.ToString() + ":" + wt.Item2.z.ToString() + ":"
          + wt.Item3.x.ToString() + ":" + wt.Item3.y.ToString() + ":" + wt.Item3.x.ToString() + "|");

                foreach (Quadruple<Element, Vector3, Vector3, ItemStack[,]> wt in this.chests)
                {
                    file.Write(wt.Item1.ID.ToString() + ":" + wt.Item2.x.ToString() + ":" + wt.Item2.y.ToString() + ":" + wt.Item2.z.ToString() + ":"
                        + wt.Item3.x.ToString() + ":" + wt.Item3.y.ToString() + ":" + wt.Item3.x.ToString() + ":");
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            file.Write(wt.Item1.Prefab.GetComponent<SyncChest>().Content[i, j].Items.ID.ToString() + ":" + wt.Item1.Prefab.GetComponent<SyncChest>().Content[i, j].Quantity.ToString() + (i == 2 && j == 2 ? "" : ":"));
                    file.Write("|");
                }
                file.Write("\n");
                file.Write(cristal[0].ToString() + "|" + cristal[1].ToString() + "|" + cristal[2].ToString() + "|" + cristal[3].ToString() + "|" +
                 cristal[4].ToString() + "|" + cristal[5].ToString() + "|" + cristal[6].ToString() + "|" + cristal[7].ToString());
            }
        }
        catch { }
    }

    public void RespawnElements()
    {
        int i = 0;
        while(i < this.idSave.Count)
        {
            float r =Random.Range(0f, 1f);
            Debug.Log(this.idSave[i].Item2);
            if (r < .1f + .02 * this.cristal[2] && Graph.isValidPosition(this.idSave[i].Item2))
            {
                this.idSave.RemoveAt(i);
                Debug.Log("Ok");
            }
            else
                i++;
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
    public List<Tuple<int, Vector3>> ListIdSave
    {
        get { return this.idSave; }
    }

    public List<Triple<Element, Vector3, Vector3>> WorkTops
    {
        get { return this.workTops; }
        set { this.workTops = value; }
    }

    public List<Quadruple<Element, Vector3, Vector3, ItemStack[,]>> Chests
    {
        get { return this.chests; }
        set { this.chests = value; }
    }
    public float[] CristalCaracteristics
    {
        set
        {
            this.cristal = value;
            GameObject.Find("Map").GetComponent<Save>().Respawn[(int)this.cristal[0]].Add(new Vector2(this.cristal[6], this.cristal[7]));
            if (GameObject.Find("Map").GetComponent<Save>().Respawn[(int)this.cristal[0]][0] == Vector2.zero)
                GameObject.Find("Map").GetComponent<Save>().Respawn[(int)this.cristal[0]].RemoveAt(0);
        }
        get { return this.cristal; }
    }
}

/// <summary>
/// Represente la sauvegarder d'un joueur.
/// </summary>
public class PlayerSave
{
    private float x, y, life, hunger, thirst, speed, cdSpeed, jump, cdJump, regen, cdRegen, poison, cdPoison;
    private int tutoProgress;
    private string inventory, namePlayer, path;
    private bool isOp;
    private Team team;

    GameObject player;

    public PlayerSave(GameObject go, string pathPlayer, bool isServer)
    {
        this.namePlayer = go.GetComponent<Social_HUD>().PlayerName;
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
            this.tutoProgress = int.Parse(properties[13]);

            this.inventory = save[1];
            string[] social = save[2].Split('|');
            this.isOp = bool.Parse(social[0]) || isServer;
            this.team = (Team)uint.Parse(social[1]);
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
            this.tutoProgress = 0;
            this.inventory = "";
            this.isOp = isServer;

            // Choisir une equipe
            if (GameObject.Find("Map").GetComponent<Save>().IsCoop)
                this.team = Team.Blue;
            else
            {
                int blue = 0;
                int red = 0;
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (p.GetComponent<Social_HUD>().Team == Team.Blue)
                        blue++;
                    else if (p.GetComponent<Social_HUD>().Team == Team.Red)
                        red++;
                }
                if (blue > red)
                    this.team = Team.Red;
                else
                    this.team = Team.Blue;
            }
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
        this.tutoProgress = this.Player.GetComponent<Tutoriel>().Progress;
        this.isOp = this.Player.GetComponent<Social_HUD>().IsOp;
        this.team = this.Player.GetComponent<Social_HUD>().Team;

        using (StreamWriter file = new StreamWriter(this.path))
        {
            file.WriteLine(this.x.ToString() + '|' + this.y.ToString() + '|' + this.life.ToString() + '|' + this.hunger.ToString() + '|' + this.thirst.ToString() +
                '|' + this.speed.ToString() + '|' + this.cdSpeed.ToString() + '|' + this.jump.ToString() + '|' + this.cdJump.ToString()
                + '|' + this.regen.ToString() + '|' + this.cdRegen.ToString() + '|' + this.poison.ToString() + '|' + this.cdPoison.ToString() + '|' + this.tutoProgress.ToString());
            file.WriteLine(this.inventory);
            file.WriteLine(this.isOp.ToString() + '|' + ((uint)this.team).ToString());
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

    public int TutoProgress
    {
        get { return this.tutoProgress; }
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

    public Team PlayerTeam
    {
        get { return this.team; }
    }

    public GameObject Player
    {
        get { return this.player; }
        set { this.player = value; }
    }
}