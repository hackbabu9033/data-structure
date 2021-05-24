using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace data_structure_test.Helper
{
    public static class FileReadHepler
    {
        public static string GetFileContent(string fileName)
        {
            var filePath = Environment.CurrentDirectory + $@"\MockData\{fileName}";
            var result = string.Empty;
            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath, Encoding.UTF8);
            }
            return result;
        }
    }
}
