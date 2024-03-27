using System;
using System.Collections.Generic;
using System.IO;

internal class Program
{
    static int n, s;
    static List<int>[] adjList;
    static bool[] visited;

    static void ReadInput(string path)
    {
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string[] firstLine = sr.ReadLine().Split(' ');
                n = int.Parse(firstLine[0]);
                s = int.Parse(firstLine[1]);

                adjList = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    adjList[i] = new List<int>();
                }

                for (int i = 1; i <= n; i++)
                {
                    string line = sr.ReadLine();
                    if (line == "") continue;

                    string[] nums = line.Split(' ');
                    foreach (string num in nums)
                    {
                        int v = int.Parse(num);
                        adjList[i].Add(v);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading input file: {ex.Message}");
            Environment.Exit(1);
        }
    }

    static void Initialize()
    {
        visited = new bool[n + 1];
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = false;
        }
    }

    static void DFS(int source)
    {
        visited[source] = true;
        Console.Write(source + " ");

        foreach (int v in adjList[source])
        {
            if (!visited[v])
            {
                DFS(v);
            }
        }
    }

    static void Main(string[] args)
    {
        string path = "DFS.INP.txt";
        ReadInput(path);
        Initialize();
        DFS(s);
        Console.WriteLine();
    }
}
