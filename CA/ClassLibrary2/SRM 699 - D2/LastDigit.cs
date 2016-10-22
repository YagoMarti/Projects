using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.SRM_699___D2
{
    public class LastDigit
    {
        public long findX(long S)
        {
            // Given a number S, find X as of... S = 564 , X = 509 becouse 9 + 50 + 509 = 564
            int[] decomposedS = S.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
            int[] possibleX = new int[decomposedS.Length];
            for (int n = 0; n < decomposedS.Length; n++)
            {
                

            }


                return 0;
        }
    }
}


/*
Problem Statement
Limak chose a positive integer X and wrote it on a blackboard. After that, every day he erased the last digit of the current number. He stopped when he erased all digits.

After the process was over, Limak computed the sum of all numbers that appeared on the blackboard, including the original number X.

For example, if the original number was 509, the numbers that appeared on the blackboard were the numbers 509, 50, and 5. Their sum is 564.

You are given a S. Limak wants you to find a number X such that the above process produces the sum S. It can be shown that for any positive S there is at most one valid X. If there is a valid X, find and return it. Otherwise, return -1.
Definition
Class:
LastDigit
Method:
findX
Parameters:
long
Returns:
long
Method signature:
long findX(long S)
(be sure your method is public)
Limits
Time limit (s):
2.000
Memory limit (MB):
256

*/