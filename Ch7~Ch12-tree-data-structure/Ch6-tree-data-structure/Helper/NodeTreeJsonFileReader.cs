using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ch6_Ch7_tree_data_structure.Helper
{
    public static class NodeTreeJsonFileReader
    {
        public static BinaryTreeNode<T> GetTreeNodeFromData<T>(string fileName) where T:IComparable<T>
        {
            var jsonPath = Path.Combine(Environment.CurrentDirectory, $@"TreeNodeData\\{fileName}");
            var json = File.ReadAllText(jsonPath, Encoding.UTF8);
            var treeNode = JsonConvert.DeserializeObject<BinaryTreeNode<T>>(json);
            return treeNode;
        }
    }
}
