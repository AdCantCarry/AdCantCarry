using System;
using System.Collections.Generic;

class Program
{
    static int N, M, K;
    static List<(int, int, int, int)> roads;
    static List<int>[] graph;
    static int[] distToSchool, distToOffice;

    static void Main()
    {
        ReadInput();
        InitializeGraph();
        Dijkstra();
        int earliestMomArrival = FindEarliestMomArrival();
        Console.WriteLine(earliestMomArrival);
    }

    static void ReadInput()
    {
        string[] input = Console.ReadLine().Split();
        N = int.Parse(input[0]);
        M = int.Parse(input[1]);
        K = int.Parse(input[2]);

        roads = new List<(int, int, int, int)>();
        for (int i = 0; i < M; i++)
        {
            input = Console.ReadLine().Split();
            int u = int.Parse(input[0]);
            int v = int.Parse(input[1]);
            int a = int.Parse(input[2]);
            int b = int.Parse(input[3]);
            roads.Add((u, v, a, b));
        }
    }

    static void InitializeGraph()
    {
        graph = new List<int>[N + 1];
        for (int i = 1; i <= N; i++)
            graph[i] = new List<int>();

        foreach (var road in roads)
        {
            int u = road.Item1;
            int v = road.Item2;
            graph[u].Add(v);
        }
    }

    static void Dijkstra()
    {
        const int Inf = int.MaxValue;
        distToSchool = new int[N + 1];
        distToOffice = new int[N + 1];
        for (int i = 1; i <= N; i++)
        {
            distToSchool[i] = Inf;
            distToOffice[i] = Inf;
        }

        distToSchool[K] = 0;
        distToOffice[1] = 0;

        PriorityQueue<(int, int)> pq = new PriorityQueue<(int, int)>();
        pq.Enqueue((0, K), distToSchool[K]);

        while (pq.Count > 0)
        {
            var (u, type) = pq.Dequeue();
            int[] dist = (type == 0) ? distToSchool : distToOffice;

            foreach (int v in graph[u])
            {
                int newDist = dist[u] + roads.Find(r => r.Item1 == u && r.Item2 == v).Item3;
                if (newDist < dist[v])
                {
                    dist[v] = newDist;
                    pq.Enqueue((v, type), newDist);
                }
            }
        }
    }

    static int FindEarliestMomArrival()
    {
        int earliestMomArrival = int.MaxValue;
        for (int i = 1; i <= N; i++)
        {
            int momArrival = distToOffice[i] + distToSchool[i];
            earliestMomArrival = Math.Min(earliestMomArrival, momArrival);
        }
        return earliestMomArrival;
    }
}

class PriorityQueue<T>
{
    private SortedDictionary<T, int> heap;

    public PriorityQueue()
    {
        heap = new SortedDictionary<T, int>();
    }

    public int Count => heap.Count;

    public void Enqueue(T item, int priority)
    {
        heap[item] = priority;
    }

    public T Dequeue()
    {
        var first = heap.First();
        heap.Remove(first.Key);
        return first.Key;
    }
}
