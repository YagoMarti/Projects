using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            short T = Convert.ToInt16(Console.ReadLine()); // number of cases
            string[] cases = new string[T*2];
            for(int i = 0; i < T*2; i++)
            {
                cases[i] = Console.ReadLine();
            }
            for(int k = 0; k < T*2; k = k+2)
            {
                short speeders = 0;
                short carsN = Convert.ToInt16(cases[k]);
                string[] speeds = cases[k + 1].Split().Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                int currentSpeed = Convert.ToInt16(speeds[0]);
                for(int s = 0; s < speeds.Count(); s++)
                {
                    if(Convert.ToInt16(speeds[s]) <= currentSpeed)
                    {
                        speeders++;
                        currentSpeed = Convert.ToInt16(speeds[s]);
                    }
                }
                Console.WriteLine(speeders);
            }
        }
    }
}
