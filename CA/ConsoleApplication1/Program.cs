using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens = new string[] { "a", "a", "aa", "aaa", "aaaa", "aaaaa", "aa" };
            string input = "aaaaaaaaaaaaaaaaaaaaaaaaa";
            tokenize(tokens, input);
        }
        static string[] tokenize(string[] tokens, string input)
        {
            List<string> consumed = new List<string>();
            tokens = tokens.OrderByDescending(x => x.Length).ToArray();
                for (int i = 0; i < input.Length; i++)
                {
                    tokens = tokens.Where(x => ((x.Length + i) <= input.Length)).ToArray();
                    List<string> possibilities = tokens.Where(x => x.Substring(0, x.Length) == input.Substring(i, x.Length)).OrderByDescending(x => x.Length).ToList();
                    if (possibilities.Count > 0)
                    {
                        consumed.Add(possibilities.First());
                        i += (possibilities.First().Length - 1);
                        
                    }
                }
                return consumed.ToArray();
        }
        
    }
}



