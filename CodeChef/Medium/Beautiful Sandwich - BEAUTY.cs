using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            // number of parts and number of ingredients. 
            string[] partsAndIngredients = Console.ReadLine().Split();
            // sandwich composition
            char[] sandwich = Console.ReadLine().ToCharArray();
            Array.Resize(ref sandwich, 7);
            // judges on the competition. 
            short judges = Convert.ToInt16(Console.ReadLine());
            
            for(int a = 0; a < judges; a++)
            {
                // allergies of each judge
                char[] allergies = Console.ReadLine().ToCharArray();
                char[] remadeSandwich = sandwich;
                    foreach(char allergie in allergies)
                        {
                            remadeSandwich = Array.FindAll(remadeSandwich, x => x != allergie);
                        }
                    double accumValue = 0;
                    short letterValue = 0;
                for(int b = 0; b < remadeSandwich.Length; b++)
                {
                    if(b < 1)
                    {
                        letterValue++;
                    }
                    else if (remadeSandwich[b] == remadeSandwich[(b - 1)])
                    {
                        letterValue++;
                    }
                    else
                    {
                        accumValue += letterValue * letterValue;
                        letterValue = 1;
                    }
                }
                accumValue += letterValue * letterValue;
                Console.WriteLine((int)accumValue);
            }
        }
    }
}
