using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            int value;
            do
            { // simple running program, just looks for a flag to leave the do while cicle.
                value = Convert.ToInt32(Console.ReadLine());
                if (value == 42)
                    flag = false;
                else Console.WriteLine(value);
            }
            while (flag);
        }
    }
}




//Your program is to use the brute-force approach in order to find the Answer to Life, the Universe, and Everything. More precisely... rewrite small numbers from input to output. Stop processing input after reading in the number 42. All numbers at input are integers of one or two digits.

//Example


//Input:
//1
//2
//88
//42
//99

//Output:
//1
//2
//88