//реализация дерева с проверкой, является ли оно четным
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
        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            // удаление существующего узла NodeToDelete
            // ищем NodeToDelete через обход в ширину
            if (Root != null)
            {
                bool isFind = false;
                Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
                queue.Enqueue(Root);

                while (queue.Size() > 0 && isFind == false)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();

                    // если нашли нужный - удаляем связность
                    if (current.NodeValue.ToString() == NodeToDelete.NodeValue.ToString())
                    {
                        //если есть родитель - убираем на него ссылку, а у него ребёнка
                        if (NodeToDelete.Parent != null)
                        {
                            current.Parent.Children.Remove(NodeToDelete);
                            NodeToDelete.Parent = null;
                        }
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
        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            // выдача всех узлов дерева в определённом порядке
            // поиск в ширину: заносим корень в очередь; 
            // извлекаем из очереди - заносим в список - добавляем в очередь детей
            // продолжаем, пока очередь не окажется пуста
            if (Root != null)
            {
                List<SimpleTreeNode<T>> nodes = new List<SimpleTreeNode<T>>();

                Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
                queue.Enqueue(Root);

                while (queue.Size() > 0)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();
                    nodes.Add(current);

                    if (current.Children != null)
                    {
                        for (int i = 0; i < current.Children.Count; i++)
                        {
                            queue.Enqueue(current.Children[i]);
                        }
                    }
                }
                return nodes;
            }
            return null;
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            // поиск узлов по значению
            if (Root != null)
            {
                List<SimpleTreeNode<T>> nodes = new List<SimpleTreeNode<T>>();

                Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
                queue.Enqueue(Root);

                while (queue.Size() > 0)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();
                    if (current.NodeValue.ToString() == val.ToString())
                    {
                        nodes.Add(current);
                    }

                    if (current.Children != null)
                    {
                        for (int i = 0; i < current.Children.Count; i++)
                        {
                            queue.Enqueue(current.Children[i]);
                        }
                    }
                }
                return nodes;
            }
            return null;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            // перемещение узла вместе с его поддеревом -- 
            // в качестве дочернего для узла NewParent
            DeleteNode(OriginalNode);
            AddChild(NewParent, OriginalNode);
        }

        public int Count()
        {
            // количество всех узлов в дереве
            return GetAllNodes().Count;
        }

        public int LeafCount()
        {
            // количество листьев в дереве
            int count = 0;
            if (Root != null)
            {
                Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
                queue.Enqueue(Root);

                while (queue.Size() > 0)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();

                    if (current.Children == null || current.Children.Count == 0)
                    {
                        count++;
                    }
                    else
                    {
                        for (int i = 0; i < current.Children.Count; i++)
                        {
                            queue.Enqueue(current.Children[i]);
                        }
                    }
                }
            }
            return count;
        }
        public void WriteOut()
        {
            List<SimpleTreeNode<T>> temp = GetAllNodes();

            for (int i = 0; i < temp.Count; i++)
            {
                Console.Write(temp[i].NodeValue + " ");
            }
            Console.WriteLine();
        }

        public int CountNodes(SimpleTreeNode<T> root)
        {
            // количество узлов в поддереве
            int count = 0;
            if (root != null)
            {
                Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
                queue.Enqueue(root);
                count++;

                while (queue.Size() > 0)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();

                    if (current.Children != null)
                    {
                        count+= current.Children.Count;
                     for (int i = 0; i < current.Children.Count; i++)
                        {
                            queue.Enqueue(current.Children[i]);
                        }
                    }
                }
            }
            return count;
        }

        public List<T> EvenTrees()
        {
            List<T> evenTrees = new List<T>();
            Queue<SimpleTreeNode<T>> queue = new Queue<SimpleTreeNode<T>>();
            if (Root != null)
            {
                queue.Enqueue(Root);

                while (queue.Size() > 0)
                {
                    SimpleTreeNode<T> current = queue.Dequeue();

                    if (current.Children != null)
                    {
                        for (int i = 0; i < current.Children.Count; i++)
                        {
                           if (CountNodes(current.Children[i]) % 2 == 0)
                            {
                                evenTrees.Add(current.NodeValue);
                                evenTrees.Add(current.Children[i].NodeValue);
                            }
                            else
                            {
                                queue.Enqueue(current.Children[i]);
                            }
                        }
                    }
                }
            }
            return evenTrees;
        }
    }
}
