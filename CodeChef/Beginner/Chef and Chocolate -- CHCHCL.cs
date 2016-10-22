using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = Convert.ToInt32(Console.ReadLine());
            string[] data = new string[T];
            for (int i = 0; i < T; i++)
            {
                data[i] = Console.ReadLine().Split();
            }

            for (int i = 0; i < T; i++)
            {
                string[] sides = data[i].Split();
                if ((Convert.ToInt16(sides[0]) * Convert.ToInt16(sides[1])) % 2 == 0)
                    Console.WriteLine("YES");
                else Console.WriteLine("NO");
            }
        }
    }
}
