using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

class Program
{
    static void BFS(Dictionary<int, List<int>> graph, int start, bool[] visited, List<int> component)
    {
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;
        component.Add(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            if (graph.ContainsKey(current))
            {
                foreach (int neighbor in graph[current])
                {
                    if (!visited[neighbor])
                    {
                        queue.Enqueue(neighbor);
                        visited[neighbor] = true;
                        component.Add(neighbor);
                    }
                }
            }
        }
    }

    static List<List<int>> FindConnectedComponents(Dictionary<int, List<int>> graph, int n)
    {
        List<List<int>> components = new List<List<int>>();
        bool[] visited = new bool[n + 1];

        for (int node = 1; node <= n; node++)
        {
            if (!visited[node])
            {
                List<int> component = new List<int>();
                BFS(graph, node, visited, component);
                components.Add(component);
            }
        }

        return components;
    }

    static void Main()
    {
        try
        {
            using (StreamReader reader = new StreamReader("MienLienThongBFS.INP.txt"))
            {
                int n = int.Parse(reader.ReadLine());
                Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

                for (int i = 1; i <= n; i++)
                {
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        List<int> vertices = new List<int>(Array.ConvertAll(line.Split(), int.Parse));
                        graph[i] = vertices;
                    }
                    else
                    {
                        Console.WriteLine("hi");
                    }
                }

                List<List<int>> components = FindConnectedComponents(graph, n);

                using (StreamWriter writer = new StreamWriter("MienLienThongBFS.OUT.txt"))
                {
                    writer.WriteLine(components.Count);

                    foreach (List<int> component in components)
                    {
                        writer.WriteLine(string.Join(" ", component));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"loi:{ex.Message}");
        }
    }
}
