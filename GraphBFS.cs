//реализация графа с обходом в ширину
using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Queue<T>
    {
        public T[] array; //дин. массив для хранения эл-в
        public int count; //текущее кол-во эл-в в очереди
        public Queue()
        {
            // инициализация внутреннего хранилища очереди
            count = 0;
            array = new T[0];
        }

        public void Enqueue(T item)
        {
            // вставка в хвост
            //копируем массив, добавляем в конец
            T[] temp = new T[count];
            Array.Copy(array, temp, count);
            array = new T[count + 1];
            Array.Copy(temp, array, count);
            array[count] = item;
            count++;
        }

        public T Dequeue()
        {
            // выдача из головы
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
            else return default(T); // если очередь пустая
        }

        public int Size()
        {
            return count; // размер очереди
        }

        public void Rotate(int N)
        {
            for (int i = 0; i < N; i++)
            {
                this.Enqueue(this.Dequeue());
            }
        }
    }

    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            // добавление нового дочернего узла существующему ParentNode
            // ищем ParentNode через обход в ширину
            if (Root != null)
            {
                bool isFind = false;
                Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
                queue.Enqueue(Root);

                while (queue.Size() > 0 && isFind == false)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();

                    // если нашли нужный - добавляем в детей
                    if (current.NodeValue.ToString() == ParentNode.NodeValue.ToString())
                    {
                        if (current.Children == null)
                        {
                            current.Children = new List<SimpleTreeNode<T>>();
                        }
                        current.Children.Add(NewChild);
                        NewChild.Parent = current;
                        isFind = true;
                    }
                    else
                    {
                        if (current.Children != null)
                        {
                            for (int i = 0; i < current.Children.Count; i++)
                            {
                                queue.Enqueue(current.Children[i]);
                            }
                        }
                    }
                }
            }
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

            public List<Vertex<T>> BreadthFirstSearch(int VFrom, int VTo)
            {
                // узлы задаются позициями в списке vertex.
                // возвращает список узлов -- путь из VFrom в VTo
                // или пустой список, если пути нету
                List<Vertex<T>> path = new List<Vertex<T>>();
                Queue<int> queue = new Queue<int>();
                for (int i = 0; i < max_vertex; i++)
                {
                    vertex[i].Hit = false;
                }

                int current = VFrom;
                queue.Enqueue(VFrom);

                SimpleTree<int> allDist = new SimpleTree<int>(new SimpleTreeNode<int>(current, null));

                while (queue.Size() > 0)
                {
                    current = queue.Dequeue();
                    vertex[current].Hit = true;

                SimpleTreeNode<int> cur = new SimpleTreeNode<int>(current, null);
                    if (IsEdge(current, VTo))
                    {
                    SimpleTreeNode<int> end = new SimpleTreeNode<int>(VTo, null);
                    allDist.AddChild(cur, end);
                    cur = end;
                    while (cur.NodeValue != VFrom)
                    {
                        path.Add(vertex[cur.NodeValue]);
                        cur = cur.Parent;
                    }
                    path.Add(vertex[VFrom]);
                    for (int i = 0; i < path.Count / 2; i++)
                    {
                        Vertex<T> temp = path[i];
                        path[i] = path[path.Count - 1 - i];
                        path[path.Count - 1 - i] = temp;
                    }
                    return path;
                    }
                    else
                    {
                        for (int i = 0; i < max_vertex; i++)
                        {
                            if (IsEdge(current, i) && !vertex[i].Hit)
                            {
                                queue.Enqueue(i);
                                vertex[current].Hit = true;
                                allDist.AddChild(cur, new SimpleTreeNode<int>(i, null));
                            }
                        }
                    }
                }
                return path;
            }
        }
    }
