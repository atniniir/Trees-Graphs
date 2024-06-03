//реализация двоичного дерева с обходами в глубину и ширину
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
    public class BSTNode<T>
    {
        public int NodeKey; // ключ узла
        public T NodeValue; // значение в узле
        public BSTNode<T> Parent; // родитель или null для корня
        public BSTNode<T> LeftChild; // левый потомок
        public BSTNode<T> RightChild; // правый потомок	

        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    // промежуточный результат поиска
    public class BSTFind<T>
    {
        // null если в дереве вообще нету узлов
        public BSTNode<T> Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind() { Node = null; }
    }

    public class BST<T>
    {
        BSTNode<T> Root; // корень дерева, или null

        public BST(BSTNode<T> node)
        {
            Root = node;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            // ищем в дереве узел и сопутствующую информацию по ключу
            BSTFind<T> res = new BSTFind<T>();

            if (Root == null)
            {
                return null;
            }
            else
            {
                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(Root);
                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();

                    // если ключ уже есть - возвращаем false
                    // если ключ больше - идём вправо
                    // иначе (т.е. меньше) - идём влево
                    if (key == current.NodeKey)
                    {
                        res.Node = current;
                        res.NodeHasKey = true;
                        res.ToLeft = false;
                        return res;
                    }
                    if (key > current.NodeKey)
                    {
                        if (current.RightChild != null)
                        {
                            queue.Enqueue(current.RightChild);
                        }
                        else
                        {
                            res.Node = current;
                            res.NodeHasKey = false;
                            res.ToLeft = true;
                            return res;
                        }

                    }
                    else
                    {
                        if (current.LeftChild != null)
                        {
                            queue.Enqueue(current.LeftChild);
                        }
                        else
                        {
                            res.Node = current;
                            res.NodeHasKey = false;
                            res.ToLeft = true;
                            return res;
                        }
                    }

                }
            }
            return null;
        }

        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево
            // если дерево пусто - добавляем в корень
            // иначе просматриваем дерево 
            if (Root == null)
            {
                Root = new BSTNode<T>(key, val, null);
            }
            else
            {
                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(Root);
                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();
                    // если ключ уже есть - возвращаем false
                    // если ключ больше - идём вправо
                    // иначе (т.е. меньше) - идём влево
                    if (key == current.NodeKey)
                    {
                        return false;
                    }
                    if (key > current.NodeKey)
                    {
                        if (current.RightChild != null)
                        {
                            queue.Enqueue(current.RightChild);
                        }
                        else
                        {
                            current.RightChild = new BSTNode<T>(key, val, current);
                            return true;
                        }

                    }
                    else
                    {
                        if (current.LeftChild != null)
                        {
                            queue.Enqueue(current.LeftChild);
                        }
                        else
                        {
                            current.LeftChild = new BSTNode<T>(key, val, current);
                            return true;
                        }
                    }

                }
            }
            return true;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            // ищем максимальное/минимальное в поддереве
            if (FromNode == null)
            {
                return null;
            }
            else
            {
                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(FromNode);
                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();
                    // FindMax - true, идём вправо; иначе влево

                    if (FindMax == true)
                    {
                        if (current.RightChild != null)
                        {
                            queue.Enqueue(current.RightChild);
                        }
                        else
                        {
                            return current;
                        }
                    }
                    else
                    {
                        if (current.LeftChild != null)
                        {
                            queue.Enqueue(current.LeftChild);
                        }
                        else
                        {
                            return current;
                        }
                    }

                }
            }
            return null;
        }

        public bool DeleteNodeByKey(int key)
        {
            // удаляем узел по ключу
            // ищем узел по ключу, если есть - удаляем
            if (Root == null)
            {
                return false;
            }
            else
            {
                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(Root);
                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();

                    // если ключ уже есть - возвращаем false
                    // если ключ больше - идём вправо
                    // иначе (т.е. меньше) - идём влево
                    if (key == current.NodeKey)
                    {
                        //если нет потомков - удаляем ссылку у родителя
                        if (current.LeftChild == null && current.RightChild == null)
                        {
                            //если родителя нет - значит, корень
                            if (current.Parent == null)
                            {
                                Root = null;
                            }
                            else
                            {
                                if (current.Parent.LeftChild == current)
                                {
                                    current.Parent.LeftChild = null;
                                }
                                else
                                {
                                    current.Parent.RightChild = null;
                                }
                            }

                            return true;
                        }
                        //если только один лист, то заменяем им
                        if (current.LeftChild == null || current.RightChild == null)
                        {
                            if (current.LeftChild == null)
                            {
                                if (current.Parent == null)
                                {
                                    Root = current.RightChild;
                                }
                                else
                                {
                                    if (current.Parent.LeftChild == current)
                                    {
                                        current.Parent.LeftChild = current.RightChild;
                                    }
                                    else
                                    {
                                        current.Parent.RightChild = current.RightChild;
                                    }
                                }
                            }
                            else
                            {
                                if (current.Parent == null)
                                {
                                    Root = current.LeftChild;
                                }
                                else
                                {
                                    if (current.Parent.LeftChild == current)
                                    {
                                        current.Parent.LeftChild = current.LeftChild;
                                    }
                                    else
                                    {
                                        current.Parent.RightChild = current.LeftChild;
                                    }
                                }
                            }
                            return true;
                        }
                        //если есть оба потомка - ищем наименьшего потомка бОльшего ключа
                        //ищем крайний левый лист у правого потомка
                        //если он лист, то заменяем; иначе берём правый
                        else
                        {
                            BSTNode<T> replacement = current.RightChild;
                            while (replacement.LeftChild != null)
                            {
                                replacement = replacement.LeftChild;
                            }
                            if (replacement.RightChild == null)
                            {
                                if (current.Parent == null)
                                {
                                    Root.NodeValue = replacement.NodeValue;
                                    Root.NodeKey = replacement.NodeKey;
                                }
                                else
                                {
                                    if (current.Parent.LeftChild == current)
                                    {
                                        current.Parent.LeftChild.NodeValue = replacement.NodeValue;
                                        current.Parent.LeftChild.NodeKey = replacement.NodeKey;
                                    }
                                    else
                                    {
                                        current.Parent.RightChild.NodeValue = replacement.NodeValue;
                                        current.Parent.RightChild.NodeKey = replacement.NodeKey;
                                    }

                                }
                                replacement.Parent.LeftChild = null;
                            }
                            else
                            {
                                if (current.Parent == null)
                                {
                                    Root.NodeValue = replacement.NodeValue;
                                    Root.NodeKey = replacement.NodeKey;
                                }
                                else
                                {
                                    if (current.Parent.LeftChild == current)
                                    {
                                        current.Parent.LeftChild.NodeValue = replacement.RightChild.NodeValue;
                                        current.Parent.LeftChild.NodeKey = replacement.RightChild.NodeKey;
                                    }
                                    else
                                    {
                                        current.Parent.RightChild.NodeValue = replacement.RightChild.NodeValue;
                                        current.Parent.RightChild.NodeKey = replacement.RightChild.NodeKey;
                                    }
                                }
                                replacement.RightChild = null;
                            }
                            return true;
                        }
                    }
                    if (key > current.NodeKey)
                    {
                        if (current.RightChild != null)
                        {
                            queue.Enqueue(current.RightChild);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (current.LeftChild != null)
                        {
                            queue.Enqueue(current.LeftChild);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public int Count()
        {
            // количество узлов в дереве
            int count = 0;

            if (Root != null)
            {
                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(Root);
                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();
                    count++;
                    if (current.LeftChild != null)
                    {
                        queue.Enqueue(current.LeftChild);
                    }

                    if (current.RightChild != null)
                    {
                        queue.Enqueue(current.RightChild);
                    }
                }
            }
            return count;
        }

        public void WriteOut()
        {
            // вывод прямого обхода
            if (Root != null)
            {
                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(Root);
                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();
                    Console.WriteLine(current.NodeKey);

                    if (current.LeftChild != null)
                    {
                        queue.Enqueue(current.LeftChild);
                    }

                    if (current.RightChild != null)
                    {
                        queue.Enqueue(current.RightChild);
                    }
                }
            }
        }

        public List<BSTNode<T>> WideAllNodes()
        {
            if (Root != null)
            {
                List<BSTNode<T>> nodes = new List<BSTNode<T>>();

                Queue<BSTNode<T>> queue = new Queue<BSTNode<T>>();
                queue.Enqueue(Root);

                while (queue.Size() > 0)
                {
                    BSTNode<T> current = queue.Dequeue();
                    nodes.Add(current);

                    if (current.LeftChild != null)
                    {
                        queue.Enqueue(current.LeftChild);
                    }
                    if (current.RightChild != null)
                    {
                        queue.Enqueue(current.RightChild);
                    }
                }
                return nodes;
            }
            return null;
        }

        public List<BSTNode<T>> DeepAllNodes(int order)
        {
            
            if (Root != null)
            {
                List<BSTNode<T>> nodes = new List<BSTNode<T>>();
                switch (order)
                {
                    case 0: InOrder(nodes, Root); break;
                    case 1: PostOrder(nodes, Root); break;
                    case 2: PreOrder(nodes, Root); break;
                }
                return nodes;
            }
            return null;
        }

        public void InOrder (List<BSTNode<T>> pool, BSTNode<T> current)
        {
            if (current != null)
            {
                InOrder(pool, current.LeftChild);
                pool.Add(current);
                InOrder(pool, current.RightChild);
            }
        }

        public void PostOrder(List<BSTNode<T>> pool, BSTNode<T> current)
        {
            if (current != null)
            {
                PostOrder(pool, current.LeftChild);
                PostOrder(pool, current.RightChild);
                pool.Add(current);
            }
        }

        public void PreOrder(List<BSTNode<T>> pool, BSTNode<T> current)
        {
            if (current != null)
            {
                pool.Add(current);
                PreOrder(pool, current.LeftChild);
                PreOrder(pool, current.RightChild);
            }
        }
    }
}