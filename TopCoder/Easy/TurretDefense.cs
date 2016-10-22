using System;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] X = new int[] { 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0 };
            int[] Y = new int[] { 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0, 1000, 0 };
            int[] T = new int[] { 2000, 4000, 6000, 8000, 10000, 12000, 14000, 16000, 18000, 20000, 22000, 24000, 26000, 28000, 30000, 32000, 34000, 36000, 38000, 40000, 42000, 44000, 46000, 48000, 50000, 52000, 54000, 56000, 58000, 60000, 62000, 64000, 66000, 68000, 70000, 72000, 74000, 76000, 78000, 80000, 82000, 84000, 86000, 88000, 90000, 92000, 94000, 96000, 98000, 100000 };
            firstMiss(X,Y,T);
        }
        public static int firstMiss(int[] xs, int[] ys, int[] times)
        {
            // we check if the first movement is possible
            if (xs[0] + ys[0] > times[0])
                return 0;

            for (int i = 1; i < times.Length; i++)
            {
                // we compare absolute values, from starting positions to expected positions in the time difference
                if ((absolute(xs[i - 1], xs[i]) + absolute(ys[i - 1], ys[i])) > absolute(times[i - 1], times[i]))
                {
                    // if we can't reach the next shot, we simply break the cycle in the last shot landed
                    return i;
                }
            } // if we land all the shots, we go for the out cycle
            return -1;
        }
        public static int absolute(int start, int end)
        {
            return Math.Abs(start - end);
        }
    }
}


//You have been given a list of incoming targets that must be shot down in the order they are given. Assuming your turret is aimed at some location, it will take time to move the turret and fire at a different location. If you are currently aimed at point (a,b), and you must move to aim at point (c,d), the time required is the absolute difference in first coordinates plus the absolute difference in second coordinates. For example, starting at (3,7) it will take 4 seconds to aim at point (5,5). Along with the coordinates of each incoming target, you are also given their arrival times. The turret must be fired precisely at the arrival time in the direction of the target. Immediately after firing, the turret can be moved to aim at the next location (no time is lost firing). Your method will return the index of the earliest target that cannot be shot down. For example, let's assume

//    xs    = {3,5,6}
//    ys    = {7,5,6}
//    times = {11,15,16}.

//Your turret always begins aimed at (0,0) at time 0. Since the first target is arriving at location (3,7) at time 11 you have 11 seconds to move there and fire. This is 1 second more than is needed. At time 11 you fire the turret, and immediately start moving to aim at (5,5). Since the second target arrives at time 15, you have 4 seconds to aim. This is exactly enough time. At time 15, you fire at the second target. Unfortunately, the third target will arrive before you can shoot it down. You would return 2 since target 2 (third target) is the earliest one you couldn't shoot down. If you could shoot them all down, return -1. As shown above, xs[i],ys[i] are the coordinates, and times[i] is the arrival time of target i.
//Definition
//Class:
//TurretDefense
//Method:
//firstMiss
//Parameters:
//int[], int[], int[]
//Returns:
//int
//Method signature:
//int firstMiss(int[] xs, int[] ys, int[] times)
//(be sure your method is public)
//Limits
//Time limit (s):
//840.000
//Memory limit (MB):
//64
//Notes
//- xs will contain between 2 and 50 elements inclusive.
//- ys and times will contain the same number of elements as xs.
//- Each element of xs will be between 0 and 1000 inclusive.
//- Each element of ys will be between 0 and 1000 inclusive.
//- Each element of times will be between 1 and 1000000 inclusive.
//- The elements of times will be in strictly increasing order.
