using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static int n, m, x, y;
    static int[,] grid;
    static int[,] dist;
    static int[] dx = { -1, 0, 1, 0 };
    static int[] dy = { 0, 1, 0, -1 };

    static void Main()
    {
        ReadInput();
        InitializeDistances();
        Dijkstra();
        int result = GetMinSum();
        WriteOutput(result);
    }

    static void ReadInput()
    {
        var input = File.ReadAllLines("RaBien.INP.txt");
        var firstLine = input[0].Split().Select(int.Parse).ToArray();
        n = firstLine[0];
        m = firstLine[1];
        x = firstLine[2];
        y = firstLine[3];

        grid = new int[n, m];
        for (int i = 0; i < n; i++)
        {
            var row = input[i + 1].Split().Select(int.Parse).ToArray();
            for (int j = 0; j < m; j++)
            {
                grid[i, j] = row[j];
            }
        }
    }

    static void InitializeDistances()
    {
        dist = new int[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                dist[i, j] = int.MaxValue;
            }
        }
        dist[x, y] = grid[x, y];
    }

    static void Dijkstra()
    {
        var pq = new PriorityQueue<(int, int), int>();
        pq.Enqueue((x, y), grid[x, y]);

        while (pq.Count > 0)
        {
            var (curX, curY) = pq.Dequeue();
            for (int k = 0; k < 4; k++)
            {
                int nx = curX + dx[k];
                int ny = curY + dy[k];
                if (nx >= 0 && nx < n && ny >= 0 && ny < m)
                {
                    int newDist = dist[curX, curY] + grid[nx, ny];
                    if (newDist < dist[nx, ny])
                    {
                        dist[nx, ny] = newDist;
                        pq.Enqueue((nx, ny), newDist);
                    }
                }
            }
        }
    }

    static int GetMinSum()
    {
        int minSum = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            minSum = Math.Min(minSum, dist[i, 0]);
            minSum = Math.Min(minSum, dist[i, m - 1]);
        }
        for (int j = 0; j < m; j++)
        {
            minSum = Math.Min(minSum, dist[0, j]);
            minSum = Math.Min(minSum, dist[n - 1, j]);
        }
        return minSum;
    }

    static void WriteOutput(int result)
    {
        File.WriteAllText("RaBien.OUT.txt", result.ToString());
    }
}

class PriorityQueue<T, TKey> where TKey : IComparable<TKey>
{
    private SortedDictionary<TKey, Queue<T>> data = new SortedDictionary<TKey, Queue<T>>();

    public void Enqueue(T item, TKey key)
    {
        if (!data.ContainsKey(key))
        {
            data[key] = new Queue<T>();
        }
        data[key].Enqueue(item);
    }

    public T Dequeue()
    {
        var firstKey = data.Keys.First();
        var item = data[firstKey].Dequeue();
        if (data[firstKey].Count == 0)
        {
            data.Remove(firstKey);
        }
        return item;
    }

    public int Count => data.Count;
}
