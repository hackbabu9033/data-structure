using ch6_tree;
using Ch6_tree_data_structure.Extension;
using Ch6_tree_data_structure.Helper;
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
            var treeNode = NodeTreeJsonFileReader.GetTreeNodeFromData<int>("intTreeNode.json");
            var threadBinTreeNode = ThreadBinaryTree<int>.ConvertToThreadBinTree(treeNode);
            var node = treeNode.Search(13);
            treeNode.Insert(25);
        }
    }
}
