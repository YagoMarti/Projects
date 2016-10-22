using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.TopCoder_Medium
{
    public class AirlinerSeats
    {
        public string[] mostAisleSeats(int width, int seats)
        {
            bool continueS = true;
            char[] seatDisposition = new char[width];
            // we discard basic possibilities that do not allow for sorting of any kind. 
            if (seats >= width)
            {
                continueS = false;
                for (int a = 0; a < width; a++)
                { seatDisposition[a] = 'S'; }
            }
            if (continueS && seats == 0)
            {
                continueS = false;
                for (int a = 0; a < width; a++)
                { seatDisposition[a] = '.'; }
            }
            // if we have spare aisles or just enought
            if (continueS && ((seats / 2) < (width - seats)))
            {
                int refWidth = width - 1;
                continueS = false;
                while (seats > 1)
                {
                    seatDisposition[refWidth] = 'S'; refWidth--;
                    seatDisposition[refWidth] = '.'; refWidth--;
                    seatDisposition[refWidth] = 'S'; refWidth--;
                    seats -= 2;
                }
                if (seats == 1)
                {
                    seatDisposition[refWidth] = 'S'; refWidth--;
                }
                while (refWidth >= 0)
                {
                    seatDisposition[refWidth] = '.'; refWidth--;
                }
            }
            // if we lack aisles.. we start with a seat, in this way s . s || s . s etc..  with just 1 we get 2
            if (continueS)
            {
                int refWidth = 0;
                int aisles = width - seats;
                while (aisles > 0)
                {
                    seatDisposition[refWidth] = 'S'; refWidth++;
                    seatDisposition[refWidth] = '.'; refWidth++;
                    seatDisposition[refWidth] = 'S'; refWidth++;
                    aisles -= 1;
                }
                for (int d = refWidth; d < width; d++)
                { seatDisposition[d] = 'S'; }
                continueS = false;
            }

            // transform seat disposition, in string arrays of at most 50 length
            int arrayL = 0;
            if (width % 50 == 0)
            { arrayL = ((int)(width / 50)); }
            else
            { arrayL = (((int)(width / 50)) + 1); }
            string[] answer = new string[arrayL];
            for (int d = 0; d < arrayL; d++)
            {
                answer[d] = new String(seatDisposition.Skip(d * 50).Take(50).ToArray());
            }
            return answer;
        }
    }
}


/*
 Problem Statement

Note: this problem statement contains an image that may not display properly if viewed outside the applet.

When on a long flight, it is often helpful to be in an aisle seat (a seat adjacent to an aisle). This way you don't need to bother another passenger when you need to go to the restroom or take a walk. However, because large airliners are built to hold as many passengers as possible, only a limited number of seats can be aisle seats.

A typical arrangement of 10 seats in a single row with 2 aisles is as follows:
 * SSS SSSS SSSS*

Aisle seats are colored green in the above example (there are four such seats), while center and window seats are colored orange.

All of the seats are equally wide and each aisle has the same width as a single seat. If an airplane's row is wide enough to fit width seats or aisles, and the airline wants exactly seats seats to be fitted in a row, find the arrangement which maximizes the number of aisle seats. A row should be formatted as a string of characters so that seats and aisles are represented by 'S' and '.' (dot) characters, respectively. If there are multiple arrangements which maximize the number of aisle seats, find the lexicographically smallest one (the dot character comes before 'S' in the lexicographical order).

You are to return the required arrangement (or part of it) as a containing no more than 2 s:

    If width is 50 or less, return the entire arrangement as a single inside the .
    If width is between 51 and 100 (inclusive), return the entire arrangement as two s, split after the first 50 characters.
    If width is more than 100, return two s containing the first and last 50 characters of the arrangement, respectively.

Definition
Class:
AirlinerSeats
Method:
mostAisleSeats
Parameters:
int, int
Returns:
string[]
Method signature:
string[] mostAisleSeats(int width, int seats)
(be sure your method is public)
Limits
Time limit (s):
840.000
Memory limit (MB):
64
*/
