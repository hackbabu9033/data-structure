using ch6_tree;
using Ch6_tree_data_structure;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;
using FileReadHepler = data_structure_test.Helper.FileReadHepler;
using System.Collections;
using System.Collections.Generic;

namespace data_structure_test
{
    [TestFixture]
    public class BinTreeToThreadBintreeConvertTest
    {
        [Test]
        public void DeleteNodeFromTree_GivenLeafNodeCase()
        {
            // arrange

            // action

            // assert
        }

        [Test]
        public void DeleteNodeFromTree_SingleRightChildNode()
        {
            // arrange
            var inputFileContent = FileReadHepler.GetFileContent(@"Stub\DeleteNodeFromTree_NodeHasRightChildNode_InputData.json");
            var inputTree = JsonConvert.DeserializeObject<BinaryTreeNode<int>>(inputFileContent);
            var root = ThreadBinaryTree<int>.ConvertToThreadBinTree(inputTree);
            var deleteNodePar = root.Rlink;
            var deleteNode = deleteNodePar.Llink;

            var expectedFileContent = FileReadHepler.GetFileContent(@"Excecpation\DeleteNodeFromTree_NodeHasRightChildNode.json");
            var binaryTreeNode = JsonConvert.DeserializeObject<BinaryTreeNode<int>>(expectedFileContent);
            var expected = ThreadBinaryTree<int>.ConvertToThreadBinTree(binaryTreeNode);

            // action
            var result = ThreadBinaryTree<int>.DeleteNodeFromTree(root, deleteNodePar, deleteNode);

            // assert
            Assert.AreEqual(true, ThreadTreeCompare<int>(expected, result));
        }

        [Test]
        public void DeleteNodeFromTree_GivenTwoChildNode()
        {
            // arrange
            var inputFileContent = FileReadHepler.GetFileContent(@"Stub\DeleteNodeFromTree_NodeHasTwoChildNodes_input.json");
            var inputTree = JsonConvert.DeserializeObject<BinaryTreeNode<int>>(inputFileContent);
            var root = ThreadBinaryTree<int>.ConvertToThreadBinTree(inputTree);
            var deleteNodePar = root;
            var deleteNode = deleteNodePar.Rlink;

            var expectedFileContent = FileReadHepler.GetFileContent(@"Excecpation\DeleteNodeFromTree_NodeHasTwoChildNodes.json");
            var binaryTreeNode = JsonConvert.DeserializeObject<BinaryTreeNode<int>>(expectedFileContent);
            var expected = ThreadBinaryTree<int>.ConvertToThreadBinTree(binaryTreeNode);

            // action
            var result = ThreadBinaryTree<int>.DeleteNodeFromTree(root, deleteNodePar, deleteNode);

            // assert
            Assert.AreEqual(true, ThreadTreeCompare<int>(expected, result));
        }

        private bool ThreadTreeCompare<T>(ThreadBinaryTree<int> expected, ThreadBinaryTree<int> result)
        {
            var expectedTreeStack = new Stack<ThreadBinaryTree<int>>();
            var resultTreeStack = new Stack<ThreadBinaryTree<int>>();
            expectedTreeStack.Push(expected);
            resultTreeStack.Push(result);
            while (resultTreeStack.Count > 0 && expectedTreeStack.Count > 0)
            {
                var expectedNode = expectedTreeStack.Pop();
                var resultNode = resultTreeStack.Pop();
                if (resultNode.Data != expectedNode.Data)
                {
                    return false;
                }

                if (resultNode.LBit != expectedNode.LBit && resultNode.RBit != expectedNode.RBit)
                {
                    return false;
                }

                if (resultNode.LBit)
                    resultTreeStack.Push(resultNode.Llink);

                if (resultNode.RBit)
                    resultTreeStack.Push(resultNode.Rlink);

                if (expectedNode.LBit)
                    expectedTreeStack.Push(expectedNode.Llink);

                if (expectedNode.RBit)
                    expectedTreeStack.Push(expectedNode.Rlink);
            }
            return true;
        }
    }
}
