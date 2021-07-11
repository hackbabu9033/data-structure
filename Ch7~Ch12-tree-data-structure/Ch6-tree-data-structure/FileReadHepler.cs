using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ch6_tree
{
    public static class FileReadHepler
    {
        public static string GetJsonFileContent(string fileName)
        {
            var filePath = Environment.CurrentDirectory + $@"\TreeNodeData\{fileName}";
            var result = string.Empty;
            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath, Encoding.UTF8);
            }
            return result;
        }
    }
}
