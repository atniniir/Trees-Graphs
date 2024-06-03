//реализация сблансированного дерева
using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode
    {
        public int NodeKey; // ключ узла
        public BSTNode Parent; // родитель или null для корня
        public BSTNode LeftChild; // левый потомок
        public BSTNode RightChild; // правый потомок	
        public int Level; // глубина узла

        public BSTNode(int key, BSTNode parent)
        {
            NodeKey = key;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public class BalancedBST
    {
        public BSTNode Root; // корень дерева

        public BalancedBST()
        {
            Root = null;
        }

        public void GenerateTree(int[] a)
        {
            // создаём дерево с нуля из неотсортированного массива a
            // сортируем массив - заносим в корень результат функции
            Array.Sort(a);
            Root = CreateNode(null, a);
        }

        public bool IsBalanced(BSTNode root_node)
        {
            if (TreeHeight(root_node) == -1)
            {
                return false;
            }
            return true; // сбалансировано ли дерево с корнем root_node
        }

        public int TreeHeight(BSTNode root_node)
        {
            if (root_node != null)
            {
                // проверяем, сбалансированно ли левое поддерево
                int leftChildHeight = TreeHeight(root_node.LeftChild);
                if (leftChildHeight == -1) return -1;

                // проверяем, сбалансированно ли правое поддерево
                int rightChildHeight = TreeHeight(root_node.RightChild);
                if (rightChildHeight == -1) return -1;

                // проверяем, сбалансирован ли текущий узел
                int heightDifference = leftChildHeight - rightChildHeight;

                if (Math.Abs(heightDifference) > 1)
                    return -1;
                else
                    return Math.Max(leftChildHeight, rightChildHeight) + 1;
            }
            return 0;
        }


        public BSTNode CreateNode(BSTNode parent, int[] a)
        {
            if (a.Length > 0)
            {
                BSTNode node = new BSTNode(a[a.Length / 2], parent);
                if (parent == null)
                {
                    node.Level = 1;
                }
                else
                {
                    node.Level = node.Parent.Level + 1;
                }
                int[] a_left = new int[a.Length / 2];
                int[] a_right = new int[a.Length / 2];
                Array.Copy(a, 0, a_left, 0, a.Length / 2);
                Array.Copy(a, a.Length / 2 + 1, a_right, 0, a.Length / 2);
                node.LeftChild = CreateNode(node, a_left);
                node.RightChild = CreateNode(node, a_right);
                return node;
            }
            else return null;
        }
    }
}
