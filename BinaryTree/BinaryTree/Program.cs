namespace BinaryTree
{
    internal class Program
    {
        public class Node
        {
            public int Value { get; set; }
            public int Height { get; set; }
            public Node LeftNode { get; set; }
            public Node RightNode { get; set; }
            public Node(int value)
            {
                Value = value;
            }
        }
        public class BinaryTree
        {
            public Node RootNode { get; set; }
            public bool IsEmpty()
            {
                return RootNode == null;
            }
            private int treeSize(Node node)
            {
                if (node is null)
                {
                    return 0;
                }
                return treeSize(node.LeftNode) + 1 + treeSize(node.RightNode);
            }
            public int Size()
            {
                return treeSize(RootNode);
            }
            public void AddItem(int value)
            {
                RootNode = AddItemRecAVL(RootNode, value);
            }
            public Node MinValueNode(Node node)
            {
                var current = node;
                while (current.LeftNode != null)
                {
                    current = current.LeftNode;
                }
                return current;
            }
            public Node DeleteNode(Node root, int key)
            {
                if (root is null)
                {
                    return root;
                }
                if (key < root.Value)
                {
                    root.LeftNode = DeleteNode(root.LeftNode, key);
                }
                else if (key > root.Value)
                {
                    root.RightNode = DeleteNode(root.RightNode, key);
                }
                else
                {
                    if ((root.LeftNode is null) || (root.RightNode is null))
                    {
                        Node tempNode = null;
                        if (tempNode == root.LeftNode)
                        {
                            tempNode = root.RightNode;
                        }
                        else
                        {
                            tempNode = root.LeftNode;
                        }
                        if (tempNode == null)
                        {
                            tempNode = root;
                            root = null;
                        }
                        else
                        {
                            root = tempNode;
                        }
                    }
                    else
                    {
                        Node temp = MinValueNode(root.RightNode);
                        root.Value = temp.Value;
                        root.RightNode = DeleteNode(root.RightNode, temp.Value);
                    }
                }

                if (root == null)
                {
                    return root;
                }

                root.Height = Math.Max(GetHeight(root.LeftNode), GetHeight(root.RightNode)) + 1;
                var balance = GetBalance(root);

                switch (balance)
                {
                    case > 1 when GetBalance(root.LeftNode) >= 0:
                        return RotateRight(root);
                    case > 1 when GetBalance(root.LeftNode) < 0:
                        root.LeftNode = RotateLeft(root.LeftNode);
                        return RotateRight(root);
                    case < -1 when GetBalance(root.RightNode) <= 0:
                        return RotateLeft(root);
                    case < -1 when GetBalance(root.RightNode) > 0:
                        root.RightNode = RotateRight(root.RightNode);
                        return RotateLeft(root);
                    default:
                        return root;
                }
            }
            public void Delete(int value)
            {
                RootNode = DeleteNode(RootNode, value);
            }
            public bool IsFull()
            {
                return IsFullRecursive(RootNode);
            }
            public Node Search(int value)
            {
                return SearchRecursive(RootNode, value);
            }
            public void PrintPreOrder()
            {
                Console.WriteLine("PreOrder Tree:");
                PrintPreOrderRecursive(RootNode);
                Console.WriteLine();
            }
            public void PrintInOrder()
            {
                Console.WriteLine("InOrder Tree:");
                PrintInOrderRecursive(RootNode);
                Console.WriteLine();
            }
            public void PrintPostOrder()
            {
                Console.WriteLine("PostOrder Tree:");
                PrintPostOrderRecursive(RootNode);
                Console.WriteLine();
            }
            public BinaryTree CopyBBST()
            {
                var copyTree = new BinaryTree();
                copyTree.RootNode = CopyNode(RootNode);
                return copyTree;
            }
            private int GetHeight(Node node)
            {
                if (node is null)
                {
                    return 0;
                }
                return node.Height;
            }
            private Node RotateLeft(Node node)
            {
                var newRootNode = node.RightNode;
                node.RightNode = newRootNode.LeftNode;
                newRootNode.LeftNode = node;

                node.Height = Math.Max(GetHeight(node.LeftNode), GetHeight(node.RightNode)) + 1;
                newRootNode.Height = Math.Max(GetHeight(newRootNode.LeftNode), GetHeight(newRootNode.RightNode)) + 1;

                return newRootNode;
            }
            private Node RotateRight(Node node)
            {
                var newRootNode = node.LeftNode;
                node.LeftNode = newRootNode.RightNode;
                newRootNode.RightNode = node;

                node.Height = Math.Max(GetHeight(node.LeftNode), GetHeight(node.RightNode)) + 1;
                newRootNode.Height = Math.Max(GetHeight(newRootNode.LeftNode), GetHeight(newRootNode.RightNode)) + 1;

                return newRootNode;
            }
            private int GetBalance(Node node)
            {
                if (node == null)
                {
                    return 0;
                }

                return GetHeight(node.LeftNode) - GetHeight(node.RightNode);
            }
            private Node Balance(Node node)
            {
                if (node == null)
                {
                    return null;
                }

                if (GetBalance(node) > 1)
                {
                    if (GetBalance(node.LeftNode) < 0)
                        node.LeftNode = RotateLeft(node.LeftNode);
                    return RotateRight(node);
                }

                if (GetBalance(node) < -1)
                {
                    if (GetBalance(node.RightNode) > 0)
                        node.RightNode = RotateRight(node.RightNode);
                    return RotateLeft(node);
                }

                return node;
            }

            private Node AddItemRecAVL(Node node, int value)
            {
                if (node == null)
                {
                    return new Node(value);
                }
                if (value < node.Value)
                {
                    node.LeftNode = AddItemRecAVL(node.LeftNode, value);
                }
                else if (value > node.Value)
                {
                    node.RightNode = AddItemRecAVL(node.RightNode, value);
                }

                node.Height = Math.Max(GetHeight(node.LeftNode), GetHeight(node.RightNode)) + 1;

                return Balance(node);
            }
            private Node CopyNode(Node node)
            {
                if (node == null)
                {
                    return null;
                }

                var copy = new Node(node.Value);
                copy.LeftNode = CopyNode(node.LeftNode);
                copy.RightNode = CopyNode(node.RightNode);

                return copy;
            }
            private bool IsFullRecursive(Node node)
            {
                if (node is null)
                {
                    return true;
                }
                if (node.LeftNode is null && node.RightNode is null)
                {
                    return true;
                }
                if (node.LeftNode is not null && node.RightNode is not null)
                {
                    return IsFullRecursive(node.LeftNode) && IsFullRecursive(node.RightNode);
                }

                return false;
            }
            private Node SearchRecursive(Node node, int value)
            {
                if (node is null || node.Value == value)
                {
                    return node;
                }

                return SearchRecursive(value < node.Value ? node.LeftNode : node.RightNode, value);
            }
            private void PrintPreOrderRecursive(Node node)
            {
                if (node is not null)
                {
                    Console.Write(node.Value + " ");
                    PrintPreOrderRecursive(node.LeftNode);
                    PrintPreOrderRecursive(node.RightNode);
                }
            }
            private void PrintInOrderRecursive(Node node)
            {
                if (node is not null)
                {
                    PrintInOrderRecursive(node.LeftNode);
                    Console.Write(node.Value + " ");
                    PrintInOrderRecursive(node.RightNode);
                }
            }
            private void PrintPostOrderRecursive(Node node)
            {
                if (node != null)
                {
                    PrintPostOrderRecursive(node.LeftNode);
                    PrintPostOrderRecursive(node.RightNode);
                    Console.Write(node.Value + " ");
                }
            }
        }
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();
            tree.AddItem(3);
            tree.AddItem(1);
            tree.AddItem(5);
            tree.AddItem(2);
            tree.AddItem(15);
            tree.AddItem(4);
            tree.AddItem(6);
            tree.AddItem(9);
            tree.AddItem(10);
            var a = tree.Search(3);
            var b = tree.Search(10);
            var c = tree.Search(11);
            tree.PrintInOrder();
            tree.PrintPostOrder();
            tree.PrintPreOrder();
            var treeCopy = tree.CopyBBST();
        }
    }
}
