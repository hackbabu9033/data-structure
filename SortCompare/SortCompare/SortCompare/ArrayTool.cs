using System;
using System.Collections.Generic;
using System.Text;

namespace Alg4Exercise
{
    public static class ArrayTool
    {
        private static Random random = new Random();
        public static int[] CreateTestArray(int length,int min,int max)
        {
            var testArray = new int[length];

            for (int i = 0; i < length; i++)
            {
                testArray[i] = random.Next(min, max);
            }
            return testArray;
        }
    }
}
