using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Edge
{
    public int Source;
    public int Destination;
    public int Weight;

    public Edge(int source, int destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}

class Graph
{
    private readonly List<Edge>[] _adjacencyList;
    private readonly int _vertices;

    public Graph(int vertices)
    {
        _vertices = vertices;
        _adjacencyList = new List<Edge>[vertices + 1];
        for (int i = 0; i < _adjacencyList.Length; i++)
        {
            _adjacencyList[i] = new List<Edge>();
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        _adjacencyList[source].Add(new Edge(source, destination, weight));
        _adjacencyList[destination].Add(new Edge(destination, source, weight)); // Because the graph is undirected
    }

    public List<int> Dijkstra(int start, int end)
    {
        var distance = new int[_vertices + 1];
        var previous = new int[_vertices + 1];
        var visited = new bool[_vertices + 1];
        Array.Fill(distance, int.MaxValue);
        Array.Fill(previous, -1);
        distance[start] = 0;

        var priorityQueue = new SortedSet<(int distance, int vertex)>(Comparer<(int distance, int vertex)>.Create((a, b) => {
            int compare = a.distance.CompareTo(b.distance);
            if (compare == 0) compare = a.vertex.CompareTo(b.vertex);
            return compare;
        }));

        priorityQueue.Add((0, start));

        while (priorityQueue.Any())
        {
            var (dist, current) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (visited[current]) continue;
            visited[current] = true;

            if (current == end) break;

            foreach (var edge in _adjacencyList[current])
            {
                if (!visited[edge.Destination])
                {
                    int newDistance = dist + edge.Weight;
                    if (newDistance < distance[edge.Destination])
                    {
                        distance[edge.Destination] = newDistance;
                        previous[edge.Destination] = current;
                        priorityQueue.Add((newDistance, edge.Destination));
                    }
                }
            }
        }

        var path = new List<int>();
        for (int at = end; previous[at] != -1; at = previous[at])
        {
            path.Add(at);
        }
        path.Add(start);
        path.Reverse();

        return path;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var inputLines = File.ReadAllLines("NganNhatX.INP.txt");
        var firstLine = inputLines[0].Split(' ').Select(int.Parse).ToArray();
        int n = firstLine[0], m = firstLine[1], s = firstLine[2], t = firstLine[3], x = firstLine[4];

        var graph = new Graph(n);
        for (int i = 1; i <= m; i++)
        {
            var edgeInfo = inputLines[i].Split(' ').Select(int.Parse).ToArray();
            graph.AddEdge(edgeInfo[0], edgeInfo[1], edgeInfo[2]);
        }

        var pathSx = graph.Dijkstra(s, x);
        var pathXt = graph.Dijkstra(x, t);

        // Remove the last vertex of the first path to avoid duplication of vertex x
        pathSx.RemoveAt(pathSx.Count - 1);
        var fullPath = pathSx.Concat(pathXt).ToList();

        // Calculate the total weight of the path
        int totalWeight = 0;
        for (int i = 0; i < fullPath.Count - 1; i++)
        {
            totalWeight += graph.Dijkstra(fullPath[i], fullPath[i + 1]).First();
        }

        File.WriteAllText("NganNhatX.OUT.txt", $"{totalWeight}\n{string.Join(" ", fullPath)}");
    }
}