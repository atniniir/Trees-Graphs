//реализация двоичного дерева на основе массива ключей
using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class aBST
    {
        public int?[] Tree; // массив ключей

        public aBST(int depth)
        {
            // расчёт размер массива для дерева глубины depth:
            int tree_size = 0;
            int pow = 1;
            for (int i = 0; i <= depth; i++)
            {
                tree_size += pow;
                pow *= 2;
            }
            Tree = new int?[tree_size];
            for (int i = 0; i < tree_size; i++) Tree[i] = null;
        }

        public int? FindKeyIndex(int key)
        {
            // ищем в массиве индекс ключа
            int current_index = 0;
            if (Tree.Length > 0)
            {
                // ищем, пока не дойдём до конца дерева
                // если встретили пустой - возвращаем отр. индекс
                // если знач. равно - возвращаем индекс; если больше - идём в правого потомка, иначе в левого
                while (current_index < Tree.Length)
                {
                    if (Tree[current_index] != null)
                    {
                        if (Tree[current_index] == key)
                        {
                            return current_index;
                        }
                        else
                        {
                            if (key > Tree[current_index])
                            {
                                current_index = current_index * 2 + 2;
                            }
                            else
                            {
                                current_index = current_index * 2 + 1;
                            }
                        }
                    }
                    else
                    {
                        return -current_index;
                    }
                }
            }
            else return 0;
            return null; // не найден
        }

      public int AddKey(int key)
        {
            // добавляем ключ в массив
            // индекс добавленного/существующего ключа или -1 если не удалось
            int? res = FindKeyIndex(key);
            if (res != null)
            {
                int index = Convert.ToInt32(FindKeyIndex(key));
                if (index < 0) index *= -1;
                Tree[index] = key;
                return index;
            }
            return -1;
        }
    }
}
