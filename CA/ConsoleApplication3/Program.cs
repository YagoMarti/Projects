using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {

        static void Main(string[] args)
        {
            count(3, "0123456789");
        }

        public static int count(int K, string good)
        {
            long optionsPerDigit = (long) Math.Pow(10, good.Length);

            int luckyNumbers = 0;
            int number = numberOfThem(2);

            int[] goodNumbers = Array.ConvertAll(good.ToArray(), c => (int)Char.GetNumericValue(c));
            int[,] numberCombinations = new int[K*2,goodNumbers.Count()];
            int[][] numberTrial = new int[K*2][];



                // k
            for (int i = 0; i < (goodNumbers.Length - (K * 2)); i++)
            {
                var res = new int[(K * 2)];
                for (int t = i, c = 0; t < i + (K * 2) - 1; t++, c++)
                    res[c] = goodNumbers[t];
                for (int j = i + (K * 2) - 1; j < goodNumbers.Length; j++)
                {
                    res[(K * 2) - 1] = goodNumbers[j];
                    
                }
            }
            


                return luckyNumbers;
        }

        public static int numberOfThem(int index)
        {
            index = index * 2;
            if (index < 11)
                numberOfThem(index);
            else
                return index;
        }
    }
}
