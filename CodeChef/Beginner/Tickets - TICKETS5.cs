using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets___TICKETS5
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = Convert.ToInt16(Console.ReadLine());
            string[] cases = new string[T];
            for (int i = 0; i < T; i++)
                cases[i] = Console.ReadLine();

            char Char1, Char2;
            
            bool flag = true;

            for (int i = 0; i < T; i++)
            {
                char[] charArray = cases[i].ToCharArray();
                Char1 = charArray[0];
                Char2 = charArray[1];
                if (Char1 == Char2)
                    flag = false;
                else
                {
                    for( int k = 0; k < charArray.Length; k++)
                    {
                        if ((k % 2 == 0) && charArray[k] != Char1)
                            flag = false;
                        else if ((k % 2 == 1) && charArray[k] != Char2)
                            flag = false;
                    }
                }
                if (flag)
                    Console.WriteLine("YES");
                else
                    Console.WriteLine("NO");
                flag = true;
            }
        }
    }
}




//Every day, Mike goes to his job by a bus, where he buys a ticket. On the ticket, there is a letter-code that can be represented as a string of upper-case Latin letters.

//Mike believes that the day will be successful in case exactly two different letters in the code alternate. Otherwise, he believes that the day will be unlucky. Please see note section for formal definition of alternating code.

//You are given a ticket code. Please determine, whether the day will be successful for Mike or not. Print "YES" or "NO" (without quotes) corresponding to the situation.

//Input

//The first line of the input contains an integer T denoting the number of test cases. The description of T test cases follows.

//The first and only line of each test case contains a single string S denoting the letter code on the ticket.

//Output

//For each test case, output a single line containing "YES" (without quotes) in case the day will be successful and "NO" otherwise.

//Note

//Two letters x, y where x != y are said to be alternating in a code, if code is of form "xyxyxy...".
//Constraints

//1 ≤ T ≤ 100
//S consists only of upper-case Latin letters
//Subtask 1 (50 points):

//|S| = 2
//Subtask 2 (50 points):

//2 ≤ |S| ≤ 100
//Example

//Input:
//2
//ABABAB
//ABC

//Output:
//YES
//NO