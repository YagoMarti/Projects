using System;
using System.Linq;

using System;
using System.Linq;

class Substitute
{
    public static int getValue(string key, string code)
    {
        // the key is always a 10 UPPERCASE string, letter 'A'-'Z' , distinct from each other. the 10th letter assumes a 0 value ( 10 )
        char[] keys = key.ToCharArray();
        // code contains at least one letter of the keys. it's composed from 1 to 9 characters, also UPPERCASE 
        char[] codes = key.ToCharArray();
        string printValue = ""; // to store the return value, can't be a in becouse we need the numbers in a sequence

        for (int a = 0; a < code.Length; a++)
        {
            if (keys.Any(x => x == code[a]))
            {
                int index = Array.IndexOf(keys, keys.First(x => x == code[a])) + 1;
                if (index == 10) { index = 0; }
                printValue += index;
            }
        }
        return Convert.ToInt32(printValue);
    }
}