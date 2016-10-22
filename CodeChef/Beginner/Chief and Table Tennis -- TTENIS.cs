using System;

namespace Chef_and_Table_Tennis
{
    class Program
    {
        static void Main(string[] args)
        {
            int repetitions = 0;
            int T = Convert.ToInt16(Console.ReadLine()); if (T > 1000) { T = 1000; } // number of test cases
            do
            {
                int pointsChef = 0, pointsOpponent = 0, loops = 0;
                char[] score = Console.ReadLine().ToCharArray(); // we get a string input ( binary ) 
                if (score != null)
                {
                    if (score.Length > 100) { Array.Resize(ref score, 100); }

                    while ((pointsChef <= 11 || pointsOpponent <= 11) && loops < score.Length) // while there are points, we assign them until we have a possible winner ( anyone hitting 11 points ) 
                    {
                        if (score[loops] == '1')
                            pointsChef++;
                        else pointsOpponent++;
                        loops++;
                    }
                    if (Math.Abs(pointsChef - pointsOpponent) <= 2 && loops < score.Length) // we check for a 2 points difference, in case the difference is of just a point, we keep checking the score
                    {
                        do
                        {
                            if (score[loops] == '1')
                                pointsChef++;
                            else pointsOpponent++;
                            loops++;
                        }
                        while (Math.Abs(pointsChef - pointsOpponent) <= 2 && loops < score.Length);
                    }
                }
                repetitions++;
                if (pointsChef > pointsOpponent) // since we all vote for the CHEF, if he wins, we all WIN , otherwise, we've lost. 
                    Console.WriteLine("WIN"); 
                else Console.WriteLine("LOSE");
                pointsChef = pointsOpponent = 0;
            }
            while (repetitions < T);
        }
    }
}




//Chef likes to play table tennis. He found some statistics of matches which described who won the points in order. A game shall be won by the player first scoring 11 points except in the case when both players have 10 points each, then the game shall be won by the first player subsequently gaining a lead of 2 points. Could you please help the Chef find out who the winner was from the given statistics? (It is guaranteed that statistics represent always a valid, finished match.)

//Input

//The first line of the input contains an integer T, denoting the number of test cases. The description of T test cases follows. Each test case consist a binary string S, which describes a match. '0' means Chef lose a point, whereas '1' means he won the point.

//Output

//For each test case, output on a separate line a string describing who won the match. If Chef won then print "WIN" (without quotes), otherwise print "LOSE" (without quotes).

//Constraints

//1 ≤ T ≤ 1000
//1 ≤ length(S) ≤ 100
 

//Example

//Input:
//2
//0101111111111
//11100000000000

//Output:
//WIN
//LOSE
//Explanation

//Example case 1. Chef won the match 11:2, his opponent won just two points, the first and the third of the match.

//Example case 2. Chef lost this match 11:3, however he started very well the match, he won the first three points in a row, but maybe he got tired and after that lost 11 points in a row.

