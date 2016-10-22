using System;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] studentsExaminers = Console.ReadLine().Split();
            int students = Convert.ToInt16(studentsExaminers[0]);
            int examiners = Convert.ToInt16(studentsExaminers[1]);

            string[] studentsKnowledge = Console.ReadLine().Split();
            Array.Resize(ref studentsKnowledge, students); 

            string[] examinerRange = new string[examiners];
            for(int i = 0; i < examiners; i++)
            {
                examinerRange[i] = Console.ReadLine();
            }

            int distanceUseful = Convert.ToInt16(Console.ReadLine());

            int[] studentsPoints = new int[students];
            for (int k = 0; k < students; k++ )
            {
                studentsPoints[k] = Convert.ToInt16(studentsKnowledge[k]);
            }

            int[,] examinersRange = new int[examiners,2];
            for (int e = 0; e < examiners; e++ )
            {
                string[] row = examinerRange[e].Split(new[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                examinersRange[e, 0] = Convert.ToInt16(row[0]);
                examinersRange[e, 1] = Convert.ToInt16(row[row.Length - 1]);
            }

            for (int ex = 0; ex < examiners; ex++ )
            {
                int examMin = examinersRange[ex, 0] - 1;
                int examMax = examinersRange[ex, 1] - 1;

                for (int s = examMin; s <= examMax; s++)
                {
                    int maxNote = studentsPoints[s];
                    int opportunities = 0; bool canCopy = false;
                    int lowerMargin = ((s - distanceUseful) > 0) ? (s - distanceUseful) : 0 ;
                    int upperMargin = ((s + distanceUseful) < students) ? (s + distanceUseful) : (students - 1);

                    for( int j = lowerMargin; j <= upperMargin; j++)
                    {
                        if(!Between(j,examMin,examMax))
                        {
                            if (maxNote <= studentsPoints[j])
                            {
                                if (maxNote < studentsPoints[j])
                                {
                                    canCopy = true;
                                    opportunities = 0;
                                }
                                if (canCopy)
                                {
                                    maxNote = studentsPoints[j];
                                    opportunities++;
                                }
                            }
                        }
                    }
                    if (canCopy)
                    { Console.WriteLine(maxNote + " " + opportunities); }
                    else 
                    { Console.WriteLine("-1"); }
                }
            }
        }

            public static bool Between(int num, int min, int max) {
            return min <= num && num <= max;
            
        }
    }
}




//N students were giving an examination. The students were sitting in a line from 1 to N in order. The ith student had knowledge ki in the subject on which the exam was being conducted. Every student is not confident about his own preparation, so each student wants to find some students (possibly zero), from whom they can copy the answers. Student i can copy from student j only if kj > ki.There are M teachers who are the exam checkers - jth checker will check the answer-sheets of some continuosly sitting students from the index Li to Ri inclusive. All the students got this information before hand, so they decided that they won't copy the solutions from a person whose answer-sheet is being checked by the same checker as that of himself. Also, each student can see only up to distance D, so a student at index i, can copy solutions of students in the range i - D to i + D, both inclusive. From all the candidate students from which a student can copy, the student will copy from the most knowledgeable students.

//For each student, print the knowledge of the student(s) from whom he can copy, as well as the number of such students.
//If a certain student can not copy, print "-1".

//Input

//First line contains 2 space-separated positive integers - N and M - the total number of students and the number of exam checkers respectively.

//Second line contains N space-separated integers, where the ith integer represents the knowledge of the ith student.

//M lines follow - each line contains 2 space-separated integers - Lj and Rj, representing the continuous range of indexes whose answer sheet will be checked by the jth checker.

//The (M + 3)th line contains a single integer D - the distance upto which each student can see for copying answers.



//Output

//Print N lines, where ith line represents the answer for ith student.

//On each line, print the knowledge of the student(s) from whom the ith student can copy as per the rule mentioned in the statement, and the number of such students, separated by a space.

//If the student can not copy from any student, print "-1" (without the quotes).


//Constraints

//1 ≤ N ≤ 10^5

//1 ≤ M ≤ N 

//1 ≤ ki ≤ 10^8

//1 ≤ Lj ≤ Rj ≤ N

//1 ≤ D ≤ N






//Subtasks

//Subtask 1: 20 points

//N ≤ 100



//Subtask 2: 20 points

//N ≤ 1000 



//Subtask 3: 60 points

//No additional constraint









//Example

//Input


//10 5
//1 2 1 2 1 2 2 1 2 3
//1 2
//3 4
//5 6
//7 8
//9 10
//2


//Output



//-1
//-1
//2 1
//-1
//2 2
//-1
//-1
//3 1
//-1
//-1