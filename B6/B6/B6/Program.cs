using System;
using System.Collections.Generic;
using System.IO;

class Program
{

    static bool IsBipartite(List<int>[] graph, int n)
    {
        int[] color = new int[n + 1];
        Queue<int> queue = new Queue<int>();

        for (int i = 1; i <= n; i++)
        {
            if (color[i] == 0)
            {
                color[i] = 1;
                queue.Enqueue(i);

                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();

                    foreach (int neighbor in graph[current])
                    {
                        if (color[neighbor] == 0)
                        {
                            color[neighbor] = -color[current];
                            queue.Enqueue(neighbor);
                        }
                        else if (color[neighbor] == color[current])
                        {
                            return false; 
                        }
                    }
                }
            }
        }

        return true; 
    }
    static void Main()
    {
        string[] lines = File.ReadAllLines("PhanDoi.INP.txt");
        int n = int.Parse(lines[0]);
        List<int>[] graph = new List<int>[n + 1];

        for (int i = 0; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 1; i <= n; i++)
        {
            string[] vertices = lines[i].Split(' ');
            foreach (string vertex in vertices)
            {
                int j = int.Parse(vertex);
                graph[i].Add(j);
                graph[j].Add(i);
            }
        }
        bool result = IsBipartite(graph, n);
        File.WriteAllText("PhanDoi.OUT.txt", result ? "YES" : "NO");
    }
}
