//реализация пирамиды (кучи)
using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Heap
    {

        public int[] HeapArray; // хранит неотрицательные числа-ключи

        public Heap() { HeapArray = null; }

        public void MakeHeap(int[] a, int depth)
        {
            // создаём массив кучи HeapArray из заданного
            // размер массива выбираем на основе глубины depth
            int length = (int)Math.Pow(2, depth + 1) - 1;
            HeapArray = new int[length];
            Array.Copy(a, HeapArray, a.Length);
            for (int i = HeapArray.Length / 2; i >= 0; i--)
            {
                heapify(i);
            }
        }

        public int GetMax()
        {
            // вернуть значение корня и перестроить кучу
            if (HeapArray[0] != 0)
            {
                int result = HeapArray[0];
                HeapArray[0] = HeapArray[HeapArray.Length - 1];
                HeapArray[HeapArray.Length - 1] = 0;
                heapify(0);
                return result;
            }
            return -1; // если куча пуста
        }

        public bool Add(int key)
        {
            // добавляем новый элемент key в кучу и перестраиваем её
            if (HeapArray[HeapArray.Length-1] == 0)
            {
                int i = HeapArray.Length - 1;
                int parent = (i - 1) / 2;
                HeapArray[i] = key;
                while (i > 0 && HeapArray[parent] < HeapArray[i])
                {
                    int temp = HeapArray[i];
                    HeapArray[i] = HeapArray[parent];
                    HeapArray[parent] = temp;

                    i = parent;
                    parent = (i - 1) / 2;
                } 
                return true;
            }
            else return false; // если куча вся заполнена
        }

        public void heapify(int i)
        {
            int leftChild;
            int rightChild;
            int largestChild;

            for (; ; )
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                largestChild = i;

                if (leftChild < HeapArray.Length && HeapArray[leftChild] > HeapArray[largestChild])
                {
                    largestChild = leftChild;
                }

                if (rightChild < HeapArray.Length && HeapArray[rightChild] > HeapArray[largestChild])
                {
                    largestChild = rightChild;
                }

                if (largestChild == i)
                {
                    break;
                }

                int temp = HeapArray[i];
                HeapArray[i] = HeapArray[largestChild];
                HeapArray[largestChild] = temp;
                i = largestChild;
            }
        }

        public void WriteOut()
        {
            for (int i = 0; i < HeapArray.Length; i++)
            {
                Console.WriteLine(HeapArray[i]);
            }
        }
    }
}