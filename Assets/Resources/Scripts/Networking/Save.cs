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

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            this.dnc = gameObject.GetComponent<DayNightCycle>();
            this.nameWorld = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().World;
            this.chunks = new List<ChunkSave>();
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
        this.coolDownSave -= Time.deltaTime;
        if (this.coolDownSave <= 0)
        {
            this.SaveWorld();
            this.coolDownSave = 60;
        }
    }

    /// <summary>
    /// Sauvegtarde toutes les donnes du monde.
    /// </summary>
    public void SaveWorld()
    {
        File.WriteAllText(this.worldPath + "properties", this.seed.ToString() + "|" + ((int)this.dnc.ActualTime).ToString());
        foreach (ChunkSave cs in this.chunks)
            cs.Save();
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
    /// Supprime a la sauvegarde un chunk.
    /// A appeller lorsque l'on souhaite degenere un chunk.
    /// </summary>
    /// <param name="x">La position x du chunk.</param>
    /// <param name="y">La position y du chunk.</param>
    public void RemoveChunk(int x, int y)
    {
        for (int i = 0; i < this.chunks.Count; i++)
            if (this.chunks[i].X == x && this.chunks[i].Y == y)
            {
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
