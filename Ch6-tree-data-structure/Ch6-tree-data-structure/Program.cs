using ch6_tree;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Ch6_tree_data_structure
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonPath = Path.Combine(Environment.CurrentDirectory, @"TreeNodeData\\intTreeNode.json");
            var json = File.ReadAllText(jsonPath, Encoding.UTF8);
            var treeNode = JsonConvert.DeserializeObject<BinaryTreeNode<int>>(json);
            //BinaryTreeNode<int>.InOrderTravel(treeNode);
            var threadBinTreeNode = ThreadBinaryTree<int>.ConvertToThreadBinTree(treeNode);
        }
    }
}
