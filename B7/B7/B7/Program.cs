using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Dijkstra
{
    const int INF = int.MaxValue;
    static void ReadData(string fileName, out int n, out int m, out int s, out int t, out int[,] graph)
    {

        using (StreamReader sr = new StreamReader(fileName))
        {
    
            string[] tokens = sr.ReadLine().Split();
            n = int.Parse(tokens[0]);
            m = int.Parse(tokens[1]);
            s = int.Parse(tokens[2]);
            t = int.Parse(tokens[3]);

  
            graph = new int[n + 1, n + 1];

         
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    graph[i, j] = INF;
                }
            }


            for (int i = 0; i < m; i++)
            {
                tokens = sr.ReadLine().Split();
                int u = int.Parse(tokens[0]);
                int v = int.Parse(tokens[1]);
                int w = int.Parse(tokens[2]);

                graph[u, v] = w;
                graph[v, u] = w; 
            }
        }
    }


    static void WriteData(string fileName, int dist, List<int> path)
    {
 
        using (StreamWriter sw = new StreamWriter(fileName))
        {
          
            sw.WriteLine(dist);

     
            for (int i = 0; i < path.Count; i++)
            {
                sw.Write(path[i]);
                if (i < path.Count - 1)
                {
                    sw.Write(" ");
                }
            }
        }
    }


    static void FindShortestPath(int n, int s, int t, int[,] graph, out int dist, out List<int> path)
    {
   
        int[] d = new int[n + 1];


        bool[] visited = new bool[n + 1];

       
        int[] parent = new int[n + 1];

     
        for (int i = 1; i <= n; i++)
        {
            d[i] = INF;
            visited[i] = false;
            parent[i] = -1;
        }

        d[s] = 0;


        for (int i = 0; i < n; i++)
        {
            
            int u = -1;
            int min = INF;
            for (int j = 1; j <= n; j++)
            {
                if (!visited[j] && d[j] < min)
                {
                    u = j;
                    min = d[j];
                }
            }

   
            visited[u] = true;

         
            if (u == t) break;

            
            for (int v = 1; v <= n; v++)
            {
                if (!visited[v] && graph[u, v] != INF && d[u] + graph[u, v] < d[v])
                {
                    d[v] = d[u] + graph[u, v];
                    parent[v] = u;
                }
            }
        }

        
        dist = d[t];

        
        path = new List<int>();

        if (dist == INF) return;

        int current = t;
        while (current != -1)
        {
            path.Add(current);
            current = parent[current];
        }

        path.Reverse();
    }

    static void Main(string[] args)
    {
       
        int n, m, s, t;
        int[,] graph;
        ReadData("Dijkstra.INP.txt", out n, out m, out s, out t, out graph);

        
        int dist;
        List<int> path;
        FindShortestPath(n, s, t, graph, out dist, out path);

     
        WriteData("Dijkstra.OUT.txt", dist, path);
    }
}
