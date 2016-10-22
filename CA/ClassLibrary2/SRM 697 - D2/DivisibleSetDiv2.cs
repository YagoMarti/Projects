using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.SRM_697___D2
{
    public class DivisibleSetDiv2
    {
        public String isPossible(int[] b)
        {
            b = b.ToList().OrderBy(x => x).ToArray();
            int[] a = new int[b.Length];
            // any array of a single element is possible
            if (b.Length < 2)
                return "Possible";
            
            int lowerA = (int) Math.Pow(2, b.Length);

            return "";
        }
    }
}


/*
Problem Statement
    	You are given a int[] b containing a sequence of n positive integers: b[0], ..., b[n-1]. We are now looking for another sequence a[0], ..., a[n-1]. This sequence should have the following properties:
Each a[i] should be a number of the form 2^x[i] where x[i] is some positive integer. In other words, each a[i] is one of the numbers 2, 4, 8, 16, ...
For each i, the value a[i]^b[i] (that is, a[i] to the power b[i]) should be divisible by P, where P is the product of all a[i].
Determine whether there is at least one sequence with the desired properties. Return "Possible" (quotes for clarity) if such a sequence exists and "Impossible" otherwise.
 
Definition
    	
Class:	DivisibleSetDiv2
Method:	isPossible
Parameters:	int[]
Returns:	String
Method signature:	String isPossible(int[] b)
(be sure your method is public)
    
 
Constraints
-	b will contain between 1 and 50 elements, inclusive.
-	Each element in b will be between 1 and 10, inclusive.
*/