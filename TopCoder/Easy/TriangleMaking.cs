
namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            maxPerimeter(41, 64, 16);
        }

        public static int maxPerimeter(int a, int b, int c)
        {
            // traingle inequality rule ... no side is bigger or equal than the sum of the other 2 sides. 
            if (a >= b + c)
                a = b + c - 1;
            if (b >= a + c)
                b = a + c - 1;
            if (c >= b + a)
                c = b + a - 1;

            int maxPerimeter = a + b + c;
            return maxPerimeter;
        }
    }
}



/* 
///  Problem Statement
You have three sticks. Their current lengths are a, b, and c. You can shorten each of those sticks arbitrarily. Your goal is to produce three sticks with the following properties:

    The length of each stick is a positive integer.
    The three sticks can be used to build a triangle. The triangle must be non-degenerate. (I.e., it must have a positive area.)
    The perimeter of the triangle must be as large as possible.

You are given the s a, b, and c. Compute and return the largest possible perimeter of the triangle constructed from your three sticks.
Definition
Class:
TriangleMaking
Method:
maxPerimeter
Parameters:
int, int, int
Returns:
int
Method signature:
int maxPerimeter(int a, int b, int c)
(be sure your method is public)
Limits
Time limit (s):
2.000
Memory limit (MB):
256
Notes
- The return value is always defined. In other words, for any a, b, and c there is at least one way to build a valid triangle.
*/