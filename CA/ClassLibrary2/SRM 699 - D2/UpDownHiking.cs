
namespace ClassLibrary2.SRM_699___D2
{
    public class UpDownHiking
    {
        public  int maxHeight(int N, int A, int B)
        {
            // A stands for how much he can climb in a single day

            // B stands for the descend he can achieve in a single day. 
            int currentAltitude = 0;
            // starting altitude is always 0
            // N are the number of days available
            for (int n = N; n > 0; n-- )
            {
                // checks if we can descend
                if(n * B >= currentAltitude)
                {
                    // checks if we can add the whole altitude
                    if ((n - 1) * B >= currentAltitude + A)
                    {
                        currentAltitude += A;
                    }
                    // checks if we can add a partial altitude
                    else if ((n - 1) * B >= currentAltitude)
                    {
                        currentAltitude += ((n-1) * B) - currentAltitude; 
                    }
                }
            }
            return currentAltitude;
        }
    }
}


/*
Problem Statement
Limak is going to spend N days in the mountains. The days will be numbered 1 through N. In the morning of day 1 Limak starts his hike in the base camp at altitude 0. In the evening of day N Limak must return back to altitude 0. Limak carries a tent and a sleeping bag, so during the hike he can sleep at any altitude.

During each day of his hike, Limak either ascends or descends. (Each day he has to choose one or the other, he cannot both ascend and descend on the same day.) Additionally, there are two constraints:

    He cannot ascend too quickly, to make acclimatization to higher altitudes easier. More precisely, each day spent ascending can increase his altitude by at most A.
    He cannot descend too quickly, otherwise his knees hurt. More precisely, each day spent descending can decrease his altitude by at most B.

For example, suppose that Limak's altitude in the morning is 470, and suppose that A=100 and B=200. In the evening of the same day Limak can be anywhere between the altitudes 270 and 570, inclusive.

You are given the s N, A, and B. Return the largest altitude Limak can reach.
Definition
Class:
UpDownHiking
Method:
maxHeight
Parameters:
int, int, int
Returns:
int
Method signature:
int maxHeight(int N, int A, int B)
(be sure your method is public)
Limits
Time limit (s):
2.000
Memory limit (MB):
256

*/