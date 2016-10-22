﻿using System;
using System.Linq;

namespace Studying_Alphabet___ALPHABET
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] lettersKnown = Console.ReadLine().ToCharArray(); // a lowercase string 'S' of the letters Jeff can read ( between 1 and 26 ) , each leter will appear only once
                if(lettersKnown.Length > 26) {Array.Resize(ref lettersKnown, 26);}
            int N = Convert.ToInt16(Console.ReadLine());// integer 'N' denoting the number of words in the book ( between 1 and 1000 ) 
                if (N > 1000) { N = 1000; }
            bool result; char[] word;
            for(int i = 0; i < N; i++) // following N number of lines contain a single lowercase string 
                {
                    word = Console.ReadLine().ToCharArray(); // words to analyse
                        if (word.Length > 12) { Array.Resize(ref word, 12); } // Constraint Words <= 12
                    if (word != null)
                        {
                            result = word.All(letter => lettersKnown.Contains(letter)); // checking if all the letters in the word are included in the known letters 
                            if (result)
                                Console.WriteLine("Yes");
                            else
                                Console.WriteLine("No");
                        }
                }
        }
    }
}


//Not everyone probably knows that Chef has younder brother Jeff. Currently Jeff learns to read.

//He knows some subset of the letter of Latin alphabet. In order to help Jeff to study, Chef gave him a book with the text consisting of N words. Jeff can read a word iff it consists only of the letters he knows.

//Now Chef is curious about which words his brother will be able to read, and which are not. Please help him!

//Input

//The first line of the input contains a lowercase Latin letter string S, consisting of the letters Jeff can read. Every letter will appear in S no more than once.

//The second line of the input contains an integer N denoting the number of words in the book.

//Each of the following N lines contains a single lowecase Latin letter string Wi, denoting the ith word in the book.

//Output

//For each of the words, output "Yes" (without quotes) in case Jeff can read it, and "No" (without quotes) otherwise.

//Constraints

//1 ≤ |S| ≤ 26
//1 ≤ N ≤ 1000
//1 ≤ |Wi| ≤ 12
//Each letter will appear in S no more than once.
//S, Wi consist only of lowercase Latin letters.
//Subtasks

//Subtask #1 (31 point): |S| = 1, i.e. Jeff knows only one letter.
//Subtask #2 (69 point) : no additional constraints
//Example

//Input:
//act
//2
//cat
//dog

//Output:
//Yes
//No
//Explanation

//The first word can be read.

//The second word contains the letters d, o and g that aren't known by Jeff.