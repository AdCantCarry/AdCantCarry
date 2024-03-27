using System;
namespace Baitapthem
{
    class Program
    {
        static void ReadAdjMatrix(string path, out int n, out int[,] adjMatrix)
        {
            StreamReader sr = new StreamReader(path);
            n = Convert.ToInt32(sr.ReadLine());
            adjMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string line = sr.ReadLine();
                string[] values = line.Split();
                for (int j = 0; j < n; j++)
                {
                    adjMatrix[i, j] = Convert.ToInt32(values[j]);
                }
            }
            sr.Close();
        }
        static void ConvertAdjMatrix2AdjList(int n, int m, List<Tuple<int, int>> AdjMatrix, out List<int>[] adjList)
        {
            adjList = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                adjList[i] = new List<int>();
            }

            foreach (var edge in AdjMatrix)
            {
                int u = edge.Item1;
                int v = edge.Item2;

                adjList[u].Add(v);
                adjList[v].Add(u);
            }
        }
        static void WriteAdjList(int n, List<int>[] adjList, string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(n);
            for (int i = 1; i <= n; i++)
            {
                foreach (var neighbor in adjList[i])
                {
                    sw.Write($"{neighbor} ");
                }
                sw.WriteLine();
            }
            sw.Close();
        }
        static void Main(string[] args)
        {
            int n, m;
            List<Tuple<int, int>> edgeList;
            ReadAdjMatrix(out n, out m, out ReadAdjMatrix);
            List<int>[] adjList;
            ConvertAdjMatrix2AdjList(n, m, ReadAdjMatrix, out adjList);
            WriteAdjList(n, adjList, "Canh2Ke.OUT.txt");
        }
    }
}