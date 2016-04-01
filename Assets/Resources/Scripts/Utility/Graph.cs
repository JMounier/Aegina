using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class Graph
{
    private Dictionary<string, Node> nodes;

    public Graph(params Vector3[] origins)
    {
        this.nodes = new Dictionary<string, Node>();

        foreach (Vector3 o in origins)
        {
            Vector3 origin = new Vector3(Convert.ToSingle(Math.Round(o.x * 2, 0) / 2), 7f, Convert.ToSingle(Math.Round(o.z * 2, 0) / 2));
            Queue<Node> file = new Queue<Node>();
            Node node = new Node(isValidPosition(origin), origin);
            file.Enqueue(node);
            this.nodes.Add(VectorToString(origin), node);
            while (file.Count > 0)
            {
                node = file.Dequeue();
                foreach (Vector3 posNeighbour in Neighbours(node))
                {
                    string posNeighbourString = VectorToString(posNeighbour);
                    if (!this.nodes.ContainsKey(posNeighbourString) && isOverGround(posNeighbour))
                    {
                        Node neighbour = new Node(isValidPosition(posNeighbour), posNeighbour);
                        node.Neighbours.Add(neighbour);
                        neighbour.Neighbours.Add(node);
                        this.nodes.Add(posNeighbourString, neighbour);
                        file.Enqueue(neighbour);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Transforme une position sous forme d'un Tuple.
    /// </summary>
    /// <param name="pos">La position en vecteur.</param>
    /// <returns></returns>
    public static string VectorToString(Vector3 pos)
    {
        return Math.Round(pos.x * 2, 0).ToString() + ":" + Math.Round(pos.z * 2, 0).ToString();
    }

    /// <summary>
    /// Transforme une position sous forme d'un Vecteur.
    /// </summary>
    /// <param name="pos">La position en Tuple</param>
    /// <returns></returns>
    public static Vector3 StringToVector(string pos)
    {
        string[] cut = pos.Split(':');
        return new Vector3(float.Parse(cut[0]) / 2, 7f, float.Parse(cut[1]) / 2);
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
    public List<Vector3> AStarPath(Node origin, Node goal, float around = 1f)
    {
        if (!goal.IsValid)
            return new List<Vector3>();

        List<Node> memory = new List<Node>();
        Heap tas = new Heap();
        origin.LastCost = 0;
        tas.Insert(0, origin);
        memory.Add(origin);
        while (!tas.IsEmpty)
        {
            Tuple<int, object> elem = tas.ExtractMin();
            Node node = (Node)elem.Item2;

            // But atteind
            if (node == goal)
            {
                tas.Clear();
                List<Vector3> path = new List<Vector3>();
                while (node.Father != null)
                {
                    path.Insert(0, node.Position);
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
                    neighbour.LastCost = node.LastCost + 1;
                    neighbour.Father = node;
                    tas.Insert((int)(neighbour.LastCost + Vector3.Distance(neighbour.Position, goal.Position)), neighbour);
                    memory.Add(neighbour);
                }
            }
        }
        foreach (Node n in memory)
            n.Reset();
        memory.Clear();
        //Debug.Log("Not find : " + goal.Position);
        return new List<Vector3>();
    }

    private static IEnumerable<Vector3> Neighbours(Node node)
    {
        yield return node.Position - new Vector3(.5f, 0, 0);
        yield return node.Position + new Vector3(.5f, 0, 0);
        yield return node.Position - new Vector3(0, 0, .5f);
        yield return node.Position + new Vector3(0, 0, .5f);
    }

    public static bool isValidPosition(Vector3 pos)
    {
        bool isOverGround = false;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(.5f, 1f, .5f)))
        {
            if (!col.isTrigger)
                if (col.name.Contains("Island"))
                    isOverGround = true;
                else
                    return false;
        }
        return isOverGround;
    }

    public static bool isOverGround(Vector3 pos)
    {
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(.5f, 1f, .5f)))
            if (col.isTrigger == false && col.name.Contains("Island"))
                return true;
        return false;
    }

    private class Heap
    {
        private int size;
        private List<Tuple<int, object>> heap;

        // Constructor
        public Heap()
        {
            this.size = 0;
            this.heap = new List<Tuple<int, object>>();
        }

        // Methods
        public void Insert(int key, object value)
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
            this.heap.Insert(g, new Tuple<int, object>(key, value));
            this.size++;
        }

        public Tuple<int, object> ExtractMin()
        {
            if (this.size == 0)
                throw new Exception("The heap is empty");
            this.size--;
            Tuple<int, object> max = this.heap[0];
            this.heap.RemoveAt(0);
            return max;
        }

        public Tuple<int, object> ExtractMax()
        {
            if (this.size == 0)
                throw new Exception("The heap is empty");
            this.size--;
            Tuple<int, object> min = this.heap[this.size];
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
        public List<Tuple<int, object>> List
        {
            get { return this.heap; }
        }
    }
}

public class Node
{
    private bool isValid;
    private int lastCost;
    private Vector3 pos;
    private Node father;
    private List<Node> neighbours;

    public Node(bool isValid, Vector3 pos)
    {
        this.isValid = isValid;
        this.lastCost = int.MaxValue;
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

    public int LastCost
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
        this.lastCost = int.MaxValue;
        this.father = null;
    }
}
