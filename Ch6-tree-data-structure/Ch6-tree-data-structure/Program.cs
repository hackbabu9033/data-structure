using ch6_tree;
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
            var treeNodeDatas = NodeTreeJsonFileReader.GetTreeNodeFromData<int>("deleteThreadTreeNodeSampleData.json");
            var treeNode = NodeTreeJsonFileReader.GetTreeNodeFromData<int>("deleteThreadTreeNodeSampleData.json");
            BinaryTreeNode<int>.InOrderTravel(treeNodeDatas);
            BinaryTreeNode<int>.PreOrderTravel(treeNodeDatas);
            var threadBinTreeNode = ThreadBinaryTree<int>.ConvertToThreadBinTree(treeNode);
        }
    }
}
