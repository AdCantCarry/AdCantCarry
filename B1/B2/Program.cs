using System;
using System.IO;

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

    static int[] CalculateDegrees(int n, int[,] adjMatrix)
    {
        int[] degrees = new int[n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                degrees[i] += adjMatrix[i, j];
            }
        }
        return degrees;
    }

    static void WriteDegrees(string path, int n, int[] degrees)
    {
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(n);

        for (int i = 0; i < n; i++)
        {
            sw.Write(degrees[i] + " ");
        }

        sw.Close();
    }
    static void Main()
    {
        string inputPath = "BacVaoRa.INP";
        string outputPath = "BacVaoRa.OUT";
        int n;
        int[,] adjMatrix;
        ReadAdjMatrix(inputPath, out n, out adjMatrix);
        int[] degrees = CalculateDegrees(n, adjMatrix);
        WriteDegrees(outputPath, n, degrees);

        Console.WriteLine("Done!");
        Console.WriteLine("Ket qua:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Dinh {i + 1}: {degrees[i]}");
        }
    }
}
