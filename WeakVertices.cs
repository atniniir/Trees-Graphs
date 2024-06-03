//реализация графа с методом проверки на уязвимость (есть ли в графе вершины, не входящие в треугольники)
using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit;
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }

    public class SimpleGraph<T>
    {
        public Vertex<T>[] vertex;
        public int[,] m_adjacency;
        public int max_vertex;
        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new Vertex<T>[size];
        }
        public void AddVertex(T value)
        {
            // ваш код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex
            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] == null)
                {
                    vertex[i] = new Vertex<T>(value);
                    break;
                }
            }
        }

        // здесь и далее, параметры v -- индекс вершины
        // в списке  vertex
        public void RemoveVertex(int v)
        {
            // удалениe вершины со всеми её рёбрами
            if (v >= 0 && v < max_vertex)
            {
                vertex[v] = null;
                for (int i = 0; i < max_vertex; i++)
                {
                    m_adjacency[v, i] = 0;
                    m_adjacency[i, v] = 0;
                }
            }
        }
        public bool IsEdge(int v1, int v2)
        {
            // true если есть ребро между вершинами v1 и v2
            if (v1 >= 0 && v1 < max_vertex && v2 >= 0 && v2 < max_vertex)
            {
                if (m_adjacency[v1, v2] != 0 && m_adjacency[v2, v1] != 0) return true;
            }
            return false;
        }
        public void AddEdge(int v1, int v2)
        {
            // добавление ребра между вершинами v1 и v2
            if (v1 >= 0 && v1 < max_vertex && v2 >= 0 && v2 < max_vertex)
            {
                if (vertex[v1] != null && vertex[v2] != null)
                {
                    m_adjacency[v1, v2] = 1;
                    m_adjacency[v2, v1] = 1;
                }
            }
        }
        public void RemoveEdge(int v1, int v2)
        {
            // удаление ребра между вершинами v1 и v2
            if (v1 >= 0 && v1 < max_vertex && v2 >= 0 && v2 < max_vertex)
            {
                m_adjacency[v1, v2] = 0;
                m_adjacency[v2, v1] = 0;
            }
        }
        public void ShowMatrix()
        {
            for (int i = 0; i < max_vertex; i++)
            {
                for (int j = 0; j < max_vertex; j++)
                {
                    {
                        Console.Write(m_adjacency[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public int CountEdges(int v)
        {
            int count = 0;
            for (int i = 0; i < max_vertex; i++)
            {
                if (IsEdge(v, i)) count++;
            }
            return count;
        }

        public List<Vertex<T>> WeakVertices()
        {
            // возвращает список узлов вне треугольников
            List<Vertex<T>> weak = new List<Vertex<T>>();
            bool isStrong = false;

            for (int i = 0; i < max_vertex; i++)
            {
                int num = CountEdges(i);
                if (num < 2)
                {
                    weak.Add(vertex[i]);
                }
                else
                {
                    int[] temp = new int[num];
                    int count = 0;
                    for (int j = 0; j < max_vertex; j++)
                    {
                        if (IsEdge(i, j)) temp[count++] = j;
                    }

                    for (int j = 0; j < num; j++)
                    {
                        if (isStrong) break;
                        for (int k = 0; k < num; k++)
                        {
                            if (IsEdge(temp[j], temp[k]))
                            {
                                isStrong = true;
                                break;
                            }
                        }
                    }
                    if (!isStrong) weak.Add(vertex[i]);
                    isStrong = false;
                }
            }
                return weak;
        }

    }
}
