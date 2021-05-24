using ch6_tree;
using Ch6_tree_data_structure;
using data_structure_test.Helper;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace data_structure_test
{
    [TestFixture]
    public class BinTreeToThreadBintreeConvertTest
    {
        [Test]
        public void Test1()
        {
            // arrange
            List<BinaryTreeNode<int>> inorderNodes = new List<BinaryTreeNode<int>>();
            var fileContent = FileReadHepler.GetFileContent("ConvertBinTreeToThreadBinTreeTest.json");
            var tree = JsonConvert.DeserializeObject<BinaryTreeNode<int>>(fileContent);
            BinaryTreeNode<int>.GetInOrdertList(tree,inorderNodes);

            // action
            var threadTree = ThreadBinaryTree<int>.ConvertToThreadBinTree(tree);

            // assert
            var threadTreeStack = new Stack<ThreadBinaryTree<int>>();
            var binTreeStack = new Stack<BinaryTreeNode<int>>();
            threadTreeStack.Push(threadTree);
            binTreeStack.Push(tree);
            while (threadTreeStack.Count > 0)
            {

            }
        }
    }
}
