//реализация сбалансированного двоичного дерева
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
    public static class BalancedBST
    {
        public static int[] GenerateBBSTArray(int[] a)
        {
            if (a.Length > 0)
            {
                // создаём результирующий массив необходимой длины
                int[] result = new int[a.Length];

                // сортируем исходный массив
                Array.Sort(a);

                Queue<int> queue = new Queue<int>();
                int root = a.Length / 2;
                int step = (a.Length - root);
                int count = 0;
                int height = 0;
                result[count++] = a[root];
                queue.Enqueue(root);
                while (queue.Size() > 0)
                {
                    if (count == Math.Pow(2, height + 1) - 1)
                    {
                        height++;
                        step /= 2;
                    }
                    int current = queue.Dequeue();
                    result[count++] = a[current - step];
                    result[count++] = a[current + step];
                    if (Math.Pow(2, height + 1) - 1 != a.Length) queue.Enqueue(current - step);
                    if (Math.Pow(2, height + 1) - 1 != a.Length) queue.Enqueue(current + step);
                }
                return result;
            }
            return null;
        }

    }
}