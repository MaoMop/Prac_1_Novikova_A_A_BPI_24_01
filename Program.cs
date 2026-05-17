using System;
using System.Collections.Generic;

class Program
{
    const int INF = 1000000;

    static void Main()
    {
        int n = 7;
        int[,] d = new int[n, n]; // Матрица весов ребер

        // Инициализация графа
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j) d[i, j] = 0;
                else d[i, j] = INF;
            }
        }

        AddEdge(d, 1, 2, 10);
        AddEdge(d, 1, 3, 12);
        AddEdge(d, 2, 3, 1);
        AddEdge(d, 2, 4, 11);
        AddEdge(d, 2, 5, 3);
        AddEdge(d, 3, 6, 8);
        AddEdge(d, 3, 7, 10);
        AddEdge(d, 4, 6, 1);
        AddEdge(d, 5, 7, 9);
        AddEdge(d, 6, 7, 2);

        Console.Write("Сколько пар вершин проверить? ");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\nПара №{i + 1}");

            Console.Write("Начальная вершина: ");
            int from = int.Parse(Console.ReadLine());

            Console.Write("Конечная вершина: ");
            int to = int.Parse(Console.ReadLine());

            
            BellmanFord(n, d, from, to);
        }
    }

    static void AddEdge(int[,] d, int from, int to, int weight)
    {
        from--;
        to--;
        d[from, to] = weight;
    }


    static void BellmanFord(int n, int[,] d, int startVertex, int endVertex)
    {
        int src = startVertex - 1;
        int dest = endVertex - 1;

        int[] dist = new int[n];
        int[] parent = new int[n];

        // Инициализация
        for (int i = 0; i < n; i++)
        {
            dist[i] = INF;
            parent[i] = -1; // -1 означает, что предка пока нет
        }
        dist[src] = 0;

        // Основной цикл Беллмана-Форда (n-1 итерация)
        for (int iter = 0; iter < n - 1; iter++)
        {
            for (int u = 0; u < n; u++)
            {
                for (int v = 0; v < n; v++)
                {
                    if (d[u, v] != INF && dist[u] != INF)
                    {
                        if (dist[u] + d[u, v] < dist[v])
                        {
                            dist[v] = dist[u] + d[u, v];
                            parent[v] = u; // Запоминаем, откуда пришли
                        }
                    }
                }
            }
        }


        PrintPath(startVertex, endVertex, dist[dest], parent);
    }

 
    static void PrintPath(int from, int to, int distance, int[] parent)
    {
        Console.Write($"{from} -> {to}: ");

        if (distance == INF)
        {
            Console.WriteLine("пути нет");
            return;
        }

        Console.Write($"длина = {distance}, путь: ");

        
        List<int> path = new List<int>();
        int current = to - 1;

        while (current != -1)
        {
            path.Add(current + 1); 
            current = parent[current];
        }

        
        path.Reverse();

        Console.WriteLine(string.Join(" ", path));
    }
}