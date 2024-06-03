//реализация графа с обходом в глубину
using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Stack<T>
    {
        public T[] array; //дин. массив для хранения эл-в
        public int count; //текущее кол-во эл-в в стеке
        public Stack()
        {
            // инициализация внутреннего хранилища стека
            count = 0;
            array = new T[0];
        }

        public int Size()
        {
            // размер текущего стека		  
            return count;
        }

        public T Pop()
        {
            //удаление элемента
            //если стек не пустой - пересоздаём массив без последнего эл-та, выдаём удалённый эл-т
            if (count > 0)
            {
                T val = array[count - 1];

                T[] temp = new T[count - 1];
                Array.Copy(array, temp, count - 1);
                array = new T[count - 1];
                Array.Copy(temp, array, count - 1);
                count--;
                return val;
            }
            else return default(T); // null, если стек пустой
        }
        public void Push(T val)
        {
            //вставка элемента
            //копируем массив, добавляем в конец
            T[] temp = new T[count];
            Array.Copy(array, temp, count);
            array = new T[count + 1];
            Array.Copy(temp, array, count);
            array[count] = val;
            count++;
        }
        public T Peek()
        {
            if (count > 0)
            {
                T val = array[count - 1];
                return val;
            }
            else return default(T); // null, если стек пустой
        }
        public T PopFirst()
        {
            //удаление элемента
            //если стек не пустой - пересоздаём массив без последнего эл-та, выдаём удалённый эл-т
            if (count > 0)
            {
                T val = array[0];

                T[] temp = new T[count - 1];
                Array.Copy(array, 1, temp, 0, count - 1);
                array = new T[count - 1];
                Array.Copy(temp, array, count - 1);
                count--;
                return val;
            }
            else return default(T); // null, если стек пустой
        }
        public T PeekFirst()
        {
            if (count > 0)
            {
                T val = array[0];
                return val;
            }
            else return default(T); // null, если стек пустой
        }
    }
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
            vertex = new Vertex <T>[size];
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

        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.
            List<Vertex<T>> path = new List<Vertex<T>>();
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < max_vertex; i++)
            {
                vertex[i].Hit = false;
            }

            int current = VFrom;
            stack.Push(current);
            while (stack.Size() > 0)
            {
                vertex[current].Hit = true;
                bool isVisitedAll = false;
                while (!isVisitedAll)
                {
                    if (IsEdge(current, VTo))
                    {
                        stack.Push(VTo);
                        while (stack.Size() > 0) path.Add(vertex[stack.PopFirst()]);
                        return path;
                    }
                    else
                    {
                        for (int i = 0; i < max_vertex; i++)
                        {
                            if (IsEdge(current, i) && !vertex[i].Hit)
                            {
                               stack.Push(i);
                               current = i;
                               vertex[current].Hit = true;
                                break;
                            }
                            if (i == max_vertex - 1) isVisitedAll = true;
                        }
                    }
                }
                stack.Pop();
                current = stack.PeekFirst();
                }
            return path;
        }
    }
}