using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public static class PathFinding
{
    /// <summary>
    /// Find the best path between a gameobject and a position. (The position must be valid)
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="goal"></param>
    /// <param name="around"></param>
    /// <returns></returns>
    public static List<Vector3> AStarPath(GameObject obj, Vector3 goal, float accuracy = 1f, float around = 1f, params GameObject[] ignore)
    {
        List<State> memory = new List<State>();
        memory.Add(new State(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z, 0, null));
        Heap tas = new Heap();
        tas.Insert(0, new State(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z, 0, null));
        int count = 1000;
        while (!tas.IsEmpty && count > 0)
        {
            count--;
            Tuple<int, object> node = tas.ExtractMin();
            State st = (State)node.Item2;
            Vector3 pos = st.Position;

            // But atteind
            if (Vector3.Distance(pos, goal) < around)
            {
                tas.Clear();
                List<Vector3> path = new List<Vector3>();
                while (st.Father != null)
                {
                    path.Insert(0, st.Position);
                    st = st.Father;
                }
                return path;
            }
            else
            {
                foreach (State child in Neighbours(st, accuracy))
                {
                    Vector3 posChild = child.Position;
                    if (isValidPosition(posChild, accuracy, obj, ignore) && !child.Exist(memory))
                    {
                        tas.Insert((int)(child.Cost + Vector3.Distance(posChild, goal)), child);
                        memory.Add(child);
                    }
                }
            }
        }
        Debug.Log("Not find");
        return new List<Vector3>() { goal };
    }

    private static IEnumerable<State> Neighbours(State s, float accuracy)
    {
        Vector3 pos = s.Position;
        yield return new State(s.X + accuracy, s.Y, s.Z, s.Cost + 1, s);
        yield return new State(s.X - accuracy, s.Y, s.Z, s.Cost + 1, s);
        yield return new State(s.X, s.Y, s.Z + accuracy, s.Cost + 1, s);
        yield return new State(s.X, s.Y, s.Z - accuracy, s.Cost + 1, s);
    }

    public static bool isValidPosition(Vector3 pos, float range, GameObject obj, params GameObject[] ignore)
    {
        bool isOverGround = false;
        foreach (Collider col in Physics.OverlapBox(pos, new Vector3(range, 1f, range)))
        {
            if (col.name.Contains("Island"))
                isOverGround = true;
            else if (col != obj.GetComponent<Collider>() && col.isTrigger == false)
            {
                bool contain = false;
                foreach (GameObject o in ignore)
                    if (col == o.GetComponent<Collider>())
                    {
                        contain = true;
                        break;
                    }
                if (!contain)
                    return false;
            }
        }
        return isOverGround;
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
            int d = 0;
            int f = this.size - 1;
            int m;
            while (d <= f)
            {
                m = (d + f) / 2;
                if (key > this.heap[m].Item1)
                    d = m + 1;
                else
                    f = m - 1;
            }
            this.heap.Insert(d, new Tuple<int, object>(key, value));
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

    private class State
    {
        private State father;
        private float x;
        private float y;
        private float z;
        private int cost;

        public State(float x, float y, float z, int cost, State father)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.cost = cost;
            this.father = father;
        }

        public State Father
        {
            get { return this.father; }
        }

        public float X
        {
            set { this.x = value; }
            get { return this.x; }
        }

        public float Y
        {
            set { this.y = value; }
            get { return this.y; }
        }

        public float Z
        {
            set { this.z = value; }
            get { return this.z; }
        }

        public Vector3 Position
        {
            get { return new Vector3(this.x, this.y, this.z); }
        }
        public int Cost
        {
            set { this.cost = value; }
            get { return this.cost; }
        }

        public bool Exist(List<State> list)
        {
            foreach (State s in list)
                if (s.X == this.X && s.Y == this.Y && s.Z == this.Z && s.Cost <= this.Cost)
                    return true;
            return false;
        }
    }
}
