using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using System.Linq;

public class Graph
{
    private Dictionary<string, Node> nodes;
    private List<Node> origins;
    private Queue<Node> file;
    private List<string> keys;

    #region Methods
    public Graph(params Vector3[] origins)
    {
        this.nodes = new Dictionary<string, Node>();
        this.origins = new List<Node>();

        this.file = new Queue<Node>();

        foreach (Vector3 o in origins)
        {
            Vector3 origin = new Vector3(Convert.ToSingle(Math.Round(o.x * 2, 0) / 2), 7f, Convert.ToSingle(Math.Round(o.z * 2, 0) / 2));
            Node node = new Node(isValidPosition(origin), origin);
            this.origins.Add(node);
            this.file.Enqueue(node);
            this.nodes.Add(VectorToString(origin), node);
        }
    }

    public void GenerateGraph(int nbTurn)
    {
        while (this.file.Count > 0 && nbTurn > 0)
        {
            Node node = this.file.Dequeue();
            foreach (Vector3 posNeighbour in Neighbours(node))
            {
                string posNeighbourString = VectorToString(posNeighbour);
                if (isOverGround(posNeighbour))
                {
                    if (!this.nodes.ContainsKey(posNeighbourString))
                    {
                        Node neighbour = new Node(isValidPosition(posNeighbour), posNeighbour);
                        node.Neighbours.Add(neighbour);
                        neighbour.Neighbours.Add(node);
                        this.nodes.Add(posNeighbourString, neighbour);
                        this.file.Enqueue(neighbour);
                    }
                    else if (!node.Neighbours.Contains(this.nodes[posNeighbourString]))
                    {
                        node.Neighbours.Add(this.nodes[posNeighbourString]);
                        this.nodes[posNeighbourString].Neighbours.Add(node);
                    }
                }
            }
            nbTurn--;
        }
        if (file.Count == 0)
            this.keys = Enumerable.ToList(this.nodes.Keys);
    }
    public Node GetNode(Vector3 pos)
    {
        try
        {
            return this.nodes[VectorToString(pos)];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Find the best path between a gameobject and a position. (The position must be valid)
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="goal">Must be valid !</param>
    /// <param name="around"></param>
    /// <returns></returns>
    public List<Node> AStarPath(Node origin, Node goal, float around = 0f)
    {
        List<Node> memory = new List<Node>();
        Heap<Node> tas = new Heap<Node>();
        origin.LastCost = 0;
        tas.Insert(0, origin);
        memory.Add(origin);
        while (!tas.IsEmpty)
        {
            Tuple<float, Node> elem = tas.ExtractMin();
            Node node = elem.Item2;

            // But atteind
            if (Vector3.Distance(node.Position, goal.Position) <= around)
            {
                tas.Clear();
                List<Node> path = new List<Node>();
                while (node.Father != null)
                {
                    path.Insert(0, node);
                    node = node.Father;
                }
                foreach (Node n in memory)
                    n.Reset();
                memory.Clear();
                return path;
            }

            foreach (Node neighbour in node.Neighbours)
            {
                if (neighbour.IsValid && neighbour.LastCost > node.LastCost + 1)
                {
                    neighbour.LastCost = node.LastCost + Vector3.Distance(node.Position, neighbour.Position);
                    neighbour.Father = node;
                    tas.Insert(neighbour.LastCost + Vector3.Distance(neighbour.Position, goal.Position), neighbour);
                    memory.Add(neighbour);
                }
            }
        }
        foreach (Node n in memory)
            n.Reset();
        memory.Clear();
        return new List<Node>();
    }

    public void DebugDrawGraph()
    {
        // Draw graph
        Queue<Node> file = new Queue<Node>();
        foreach (Node origin in this.origins)
        {
            origin.LastCost = 0;
            file.Enqueue(origin);
        }
        while (file.Count > 0)
        {
            Node node = file.Dequeue();
            foreach (Node neighbour in node.Neighbours)
            {
                if (!node.IsValid || !neighbour.IsValid)
                    Debug.DrawLine(node.Position, neighbour.Position, Color.red);
                else
                    Debug.DrawLine(node.Position, neighbour.Position, Color.green);

                if (neighbour.LastCost > 0)
                {
                    neighbour.LastCost = 0;
                    file.Enqueue(neighbour);
                }
            }
        }

        // Clear graph
        foreach (Node origin in this.origins)
        {
            origin.Reset();
            file.Enqueue(origin);
        }
        while (file.Count > 0)
        {
            Node node = file.Dequeue();
            foreach (Node neighbour in node.Neighbours)
                if (neighbour.LastCost == 0)
                {
                    neighbour.Reset();
                    file.Enqueue(neighbour);
                }
        }
    }

    /// <summary>
    /// Recalcul la validite d'une partie du graph, a appeler lors de la destruction d'un element par exemple.
    /// </summary>
    /// <param name="pos"></param>
    public void Reset(Node node, bool validity)
    {
        bool test = isValidPosition(node.Position);
        if (node.IsValid != validity && test == validity)
        {
            node.IsValid = validity;
            foreach (Node neighbour in node.Neighbours)
                Reset(neighbour, validity);
        }
    }

    public Node ChoseRandomNode()
    {
        if (this.keys.Count == 0)
            return null;
        return this.nodes[this.keys[UnityEngine.Random.Range(0, this.keys.Count)]];
    }
    #endregion

    #region Statics methods
    private static IEnumerable<Vector3> Neighbours(Node node)
    {
        yield return node.Position - new Vector3(.5f, 0, 0);
        yield return node.Position + new Vector3(.5f, 0, 0);
        yield return node.Position - new Vector3(0, 0, .5f);
        yield return node.Position + new Vector3(0, 0, .5f);
        yield return node.Position - new Vector3(.5f, 0, .5f);
        yield return node.Position + new Vector3(.5f, 0, .5f);
        yield return node.Position - new Vector3(-.5f, 0, .5f);
        yield return node.Position + new Vector3(-.5f, 0, .5f);
    }

    public static bool isValidPosition(Vector3 pos)
    {
        bool isOverGround = false;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(.25f, 1f, .25f)))
        {
            if (!col.isTrigger)
            {
                if (col.name.Contains("Island"))
                    isOverGround = true;
                else if (!col.name.Contains("Character") && !col.gameObject.CompareTag("Loot"))
                    return false;
            }

        }
        return isOverGround;
    }

    public static bool isOverGround(Vector3 pos)
    {
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(.25f, 1f, .25f)))
            if (col.isTrigger == false && col.name.Contains("Island"))
                return true;
        return false;
    }

    /// <summary>
    /// Transforme une position sous forme d'un Tuple.
    /// </summary>
    /// <param name="pos">La position en vecteur.</param>
    /// <returns></returns>
    private static string VectorToString(Vector3 pos)
    {
        return Math.Round(pos.x * 2, 0).ToString() + ":" + Math.Round(pos.z * 2, 0).ToString();
    }

    /// <summary>
    /// Transforme une position sous forme d'un Vecteur.
    /// </summary>
    /// <param name="pos">La position en Tuple</param>
    /// <returns></returns>
    private static Vector3 StringToVector(string pos)
    {
        string[] cut = pos.Split(':');
        return new Vector3(float.Parse(cut[0]) / 2, 7f, float.Parse(cut[1]) / 2);
    }
    #endregion

    #region Getters/Setters
    public bool IsFileEmpty
    {
        get { return this.file.Count == 0; }
    }
    #endregion
}

public class Node
{
    private bool isValid;
    private float lastCost;
    private Vector3 pos;
    private Node father;
    private List<Node> neighbours;

    public Node(bool isValid, Vector3 pos)
    {
        this.isValid = isValid;
        this.lastCost = float.PositiveInfinity;
        this.pos = new Vector3(pos.x, 7f, pos.z);
        this.father = null;
        this.neighbours = new List<Node>();
    }

    public Node Father
    {
        get { return this.father; }
        set { this.father = value; }
    }

    public List<Node> Neighbours
    {
        get { return this.neighbours; }
        set { this.neighbours = value; }
    }

    public float X
    {
        set { this.pos.x = value; }
        get { return this.pos.x; }
    }

    public float Y
    {
        set { this.pos.y = value; }
        get { return this.pos.y; }
    }

    public float Z
    {
        set { this.pos.z = value; }
        get { return this.pos.z; }
    }

    public Vector3 Position
    {
        get { return this.pos; }
        set { this.pos = value; }
    }

    public float LastCost
    {
        set { this.lastCost = value; }
        get { return this.lastCost; }
    }

    public bool IsValid
    {
        get { return this.isValid; }
        set { this.isValid = value; }
    }

    public void Reset()
    {
        this.lastCost = float.PositiveInfinity;
        this.father = null;
    }
}

public class Heap<Element>
{
    private int size;
    private List<Tuple<float, Element>> heap;

    // Constructor
    public Heap()
    {
        this.size = 0;
        this.heap = new List<Tuple<float, Element>>();
    }

    // Methods
    public void Insert(float key, Element value)
    {
        int g = 0;
        int d = this.size - 1;
        int m;
        while (g < d)
        {
            m = (g + d) / 2;
            if (key > this.heap[m].Item1)
                g = m + 1;
            else
                d = m - 1;
        }
        this.heap.Insert(g, new Tuple<float, Element>(key, value));
        this.size++;
    }

    public Tuple<float, Element> ExtractMin()
    {
        if (this.size == 0)
            throw new Exception("The heap is empty");
        this.size--;
        Tuple<float, Element> max = this.heap[0];
        this.heap.RemoveAt(0);
        return max;
    }

    public Tuple<float, Element> ExtractMax()
    {
        if (this.size == 0)
            throw new Exception("The heap is empty");
        this.size--;
        Tuple<float, Element> min = this.heap[this.size];
        this.heap.RemoveAt(this.size);
        return min;
    }

    public void Clear()
    {
        this.size = 0;
        this.heap.Clear();
    }

    // Getters & Setters

    /// <summary>
    /// The number of element in the heap.
    /// </summary>
    public int Size
    {
        get { return this.size; }
    }

    /// <summary>
    /// Return true if the heap is empty.
    /// </summary>
    public bool IsEmpty
    {
        get { return this.size == 0; }
    }

    /// <summary>
    /// La liste contenant cles et valeur du tas.
    /// </summary>
    public List<Tuple<float, Element>> List
    {
        get { return this.heap; }
    }
}
