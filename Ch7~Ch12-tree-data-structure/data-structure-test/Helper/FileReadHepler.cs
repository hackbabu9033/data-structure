using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace data_structure_test.Helper
{
    public static class FileReadHepler
    {
        public static string GetFileContent(string filePath)
        {
            var result = string.Empty;
            if (File.Exists(filePath))
            {
                result = File.ReadAllText(Environment.CurrentDirectory + $@"\{filePath}", Encoding.UTF8);
            }
            return result;
        }
    }
}
