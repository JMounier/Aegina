using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class SyncMob : NetworkBehaviour
{

    private Mob myMob;
    private Vector3 goal;
    private List<Vector3> path;

    // Use this for initialization
    void Start()
    {
        this.goal = gameObject.transform.position;
        this.path = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
                --------------------------
               |    Deplacement du mob    |
                -------------------------- 
        */

        // Objectif atteind
        if (this.path.Count == 0)
        {
            // Trouve un nouveau but
            Vector3 newGoal = goal;
            bool isPossible = false;
            while (!isPossible)
            {
                float randAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
                newGoal = new Vector3(Mathf.Cos(randAngle), 0, Mathf.Sin(randAngle)) * UnityEngine.Random.Range(10f, 25f) + goal;
                isPossible = this.isValid(newGoal, 1f);
            }
            goal = newGoal;
            /*
            // Calcule le chemin => A*
            List<State> closedList = new List<State>();
            Heap tas = new Heap();
            tas.Insert(0, new State(0, 0, 0, new List<Vector3>() { gameObject.transform.position }));
            while (!tas.IsEmpty)
            {
                Tuple<int, object> node = tas.ExtractMin();
                State st = (State)node.Item2;
                Vector3 pos = st.Path[st.Path.Count - 1];
                if (Vector3.Distance(pos, this.goal) < 1)
                {
                    this.path = st.Path;
                    tas.Clear();
                }
                else
                {
                    foreach (State child in Neighbours(st))
                    {
                        Vector3 posChild = child.Path[child.Path.Count - 1];
                        if (isValid(posChild, .5f) && !child.Exist(closedList) && !child.Exist(tas))
                            tas.Insert((int)(child.Cost + Vector3.Distance(posChild, goal)), child);
                    }
                    closedList.Add(st);
                }               
            }
            Debug.Log(gameObject.transform.position);
            Debug.Log(this.path);  
            */       
        }
    }
    
    private IEnumerable<State> Neighbours(State s)
    {
        Vector3 pos = s.Path[s.Path.Count - 1];

        List<Vector3> nPath = new List<Vector3>(s.Path);
        nPath.Add(new Vector3(.5f, 0, 0) + pos);
        yield return new State(s.X + 1, s.Y, s.Cost + 1, nPath);

        nPath = new List<Vector3>(s.Path);
        nPath.Add(new Vector3(-.5f, 0, 0) + pos);
        yield return new State(s.X - 1, s.Y, s.Cost + 1, nPath);

        nPath = new List<Vector3>(s.Path);
        nPath.Add(new Vector3(0, 0, .5f) + pos);
        yield return new State(s.X, s.Y + 1, s.Cost + 1, nPath);

        nPath = new List<Vector3>(s.Path);
        nPath.Add(new Vector3(0, 0, -.5f) + pos);
        yield return new State(s.X, s.Y - 1, s.Cost + 1, nPath);
    }
    
    private bool isValid(Vector3 pos, float range)
    {
        bool isOverGround = false;
        foreach (Collider col in Physics.OverlapSphere(pos, range))
        {
            if (col.name.Contains("Island"))
                isOverGround = true;
            else if (col.transform.parent != null && col.transform.parent.CompareTag("Elements"))
                return false;
        }
        return isOverGround;
    }
    
    // Getters & Setters
    public Mob MyMob
    {
        get { return this.myMob; }
        set { this.myMob = value; }
    }

    #region Heap and State

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
            int i = 0;
            while (i < this.size && key > this.heap[i].Item1)
                i++;
            this.heap.Insert(i, new Tuple<int, object>(key, value));
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
        private List<Vector3> list;
        private int v1;
        private int v2;
        private int v3;

        public State(int v1, int v2, int v3, List<Vector3> list)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.list = list;
        }

        public List<Vector3> Path
        {
            get { return this.list; }
        }

        public int X
        {
            set { this.v1 = value; }
            get { return this.v1; }
        }

        public int Y
        {
            set { this.v2 = value; }
            get { return this.v2; }
        }

        public int Cost
        {
            set { this.v3 = value; }
            get { return this.v3; }
        }

        public bool Exist(List<State> list)
        {
            foreach (State s in list)
                if (s.X == this.X && s.Y == this.Y && s.Cost < this.Cost)
                    return true;
            return false;
        }

        public bool Exist(Heap heap)
        {
            foreach (Tuple<int, object> el in heap.List)
            {
                State s = (State)el.Item2;
                if (s.X == this.X && s.Y == this.Y && s.Cost < this.Cost)
                    return true;
            }

            return false;
        }
    }

    #endregion
}