using System;

namespace Entrance_Exam___ENTEXAM
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = Convert.ToInt16(Console.ReadLine()); // T number of test cases
                checkMaxiumValue(ref T, 5); // Constraint : 1 ≤ T ≤ 5
                string[] Notes;
                int rNote;
            for(int t = 0; t < T; t++) // for running all the test cases
            {
                string[] arrayNKEM = Console.ReadLine().Split();

                // N stands for the number of students that applied to enter 
                int N = Convert.ToInt16(arrayNKEM[0]);
                checkMaxiumValue(ref N, 10000);             // Constraint : 1 ≤ K < N ≤ 10000
                // K stands for the number of students that will pass the exam
                int K = Convert.ToInt16(arrayNKEM[1]);
                checkMaxiumValue(ref K, N);             // Constraint : 1 ≤ K < N ≤ 10000
                // E stands for the number of exams that will be taken
                int E = Convert.ToInt16(arrayNKEM[2]);
                checkMaxiumValue(ref E, 4); // Constraint : 1 ≤ E ≤ 4
                // , M for the maxium score available on each exam
                int M = Convert.ToInt16(arrayNKEM[3]);
                checkMaxiumValue(ref M, 1000000000); // 1 ≤ M ≤ 1000000000 

                int[] studentNotes = new int[N];

                for( int n = 0; n < N; n++) // for each student, we will have a row of exams
                {
                        if(n != (N-1))
                        {   // since the last one lacks a note, he won't run this procedure. 
                            Notes = Console.ReadLine().Split();
                            Array.Resize(ref Notes, 4);
                            foreach (string note in Notes)     //for(int e = 0; e < E; e++)
                                {
                                    if (int.TryParse(note, out rNote))
                                        studentNotes[n] += rNote;
                                }
                        }
                        else
                        {   // last input is the student notes , so here goes the comparison to check the best note. 
                            Array.Sort(studentNotes);
                            Array.Reverse(studentNotes); // we sort the array in inverse order, so we have the notes from max to min
                            
                            Notes = Console.ReadLine().Split();
                            Array.Resize(ref Notes, 4);
                            foreach (string note in Notes) // we add our notes after the sort, so that we're last
                                { 
                                if (int.TryParse(note, out rNote)) 
                                    studentNotes[n] += rNote; 
                                }
                            rNote = (studentNotes[(K-1)] - studentNotes[n]) + 1;
                            if (rNote > M)
                            { Console.WriteLine("Impossible"); }
                            else
                                if (rNote > 0)
                                { Console.WriteLine(rNote); }
                                else Console.WriteLine('0');
                        }
                }
            }


        }

        static void checkMaxiumValue(ref int value, int maxValue)
        {
            if (value > maxValue)
                value = maxValue;
        }
    }
}


//The faculty of application management and consulting services (FAMCS) of the Berland State University (BSU) has always been popular among Berland's enrollees. This year, N students attended the entrance exams, but no more than K will enter the university. In order to decide who are these students, there are series of entrance exams. All the students with score strictly greater than at least (N-K) students' total score gets enrolled.

//In total there are E entrance exams, in each of them one can score between 0 and M points, inclusively. The first E-1 exams had already been conducted, and now it's time for the last tribulation.

//Sergey is the student who wants very hard to enter the university, so he had collected the information about the first E-1 from all N-1 enrollees (i.e., everyone except him). Of course, he knows his own scores as well.

//In order to estimate his chances to enter the University after the last exam, Sergey went to a fortune teller. From the visit, he learnt about scores that everyone except him will get at the last exam. Now he wants to calculate the minimum score he needs to score in order to enter to the university. But now he's still very busy with minimizing the amount of change he gets in the shops, so he asks you to help him.

//Input

//The first line of the input contains an integer T denoting the number of test cases. The description of T test cases follows.

//The first line of each test case contains four space separated integers N, K, E, M denoting the number of students, the maximal number of students who'll get enrolled, the total number of entrance exams and maximal number of points for a single exam, respectively.

//The following N-1 lines will contain E integers each, where the first E-1 integers correspond to the scores of the exams conducted. The last integer corresponds to the score at the last exam, that was predicted by the fortune-teller.

//The last line contains E-1 integers denoting Sergey's score for the first E-1 exams.

//Output

//For each test case, output a single line containing the minimum score Sergey should get in the last exam in order to be enrolled. If Sergey doesn't have a chance to be enrolled, output "Impossible" (without quotes).

//Constraints

//1 ≤ T ≤ 5
//1 ≤ K < N ≤ 104
//1 ≤ M ≤ 109
//1 ≤ E ≤ 4
//Example

//Input:
//1
//4 2 3 10
//7 7 7
//4 6 10
//7 10 9
//9 9

//Output:
//4
//Explanation

//Example case 1. If Sergey gets 4 points at the last exam, his score will be equal to 9+9+4=22. This will be the second score among all the enrollees - the first one will get 21, the second one will get 20 and the third will have the total of 26. Thus, Sergey will enter the university.