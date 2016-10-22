using System;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            string ammountRead = Console.ReadLine();
            short ammount;
            long combinations;
            do
            {
                ammount = Convert.ToInt16(ammountRead);
                switch (Console.ReadLine())
                {
                    case "CP":
                        { 
                            combinations = 1; // keeping it in the same size
                            // to smaller recipients 
                            // since a cup is the smallest divisible part, there aren't smaller combinations 
                            combinations += 0;
                            // to bigger recipients
                            if (ammount >= 2)
                            {
                                int cpQ = (int)ammount / 2;
                                for(int cp = cpQ; cp > 0; cp--)
                                {
                                    int pnQ = (int)cpQ / 2;
                                    for (int pn = pnQ; pn >= 0; pn--)
                                    {
                                        int qG = (int)pnQ / 4;
                                        for (int g = qG; g >= 0; g--)
                                        {
                                            int gPL = (int)(qG / 2); // ammount of pl units we can get
                                            for (int pl = gPL; pl >= 0; pl--)
                                            {
                                                int gCN = (int)(gPL / 5);
                                                for (int cn = gCN; cn >= 0; cn--)
                                                {
                                                    int Q = ammount - ((cn * 64) + (pl * 32) + (g * 16) + (q * 4) + ( pn * 2));
                                                    for (int q = 0; q <= Q * 2; q++) // g to q options ( includes cp and pn ) 
                                                        combinations += (q * 2) + 1;
                                                }
                                            }
                                        }
                                    }
                                }  
                            }
                            Console.WriteLine(combinations);
                        } break;
                    case "PN":
                        {
                            combinations = 1; // keeping it in the same size
                            // to smaller recipients --  each PN can be a PN, or 2 CP ...
                            combinations += ammount;
                            // to bigger recipients
                            if (ammount >= 2)
                            {
                                int pnQ = (int)ammount / 2;
                                for(int pn = pnQ; pn >= 0; pn--)
                                {
                                    int qG = (int)pnQ / 4; 
                                    for (int g = qG; g >= 0; g--)
                                    {
                                        int gPL = (int)(qG / 2); // ammount of pl units we can get
                                        for (int pl = gPL; pl >= 0; pl--)
                                        {
                                            int gCN = (int)(gPL / 5);
                                            for (int cn = gCN; cn >= 0; cn--)
                                            {
                                                int Q = ammount - ((cn * 40) + (pl * 8) + (g * 4));
                                                for (int q = 0; q <= Q * 2; q++) // g to q options ( includes cp and pn ) 
                                                    combinations += (q * 2) + 1;
                                            }
                                        }
                                    }
                                }
                            }

                            Console.WriteLine(combinations);
                        } break;
                    case "Q":
                        {
                            combinations = 1; // keeping it in the same size
                            // to smaller recipients -- each Q can be 2PN, or 4CP, or 1PN and 2CP ... 
                            for (int q = ammount; q > 0; q--) // g to q options ( includes cp and pn ) 
                                combinations += (q * 2) + 1;
                            // to bigger recipients
                            if (ammount >= 4)
                            {
                                int qG = (int)ammount / 4; 
                                for(int g = qG; g >= 0; g--)
                                {
                                    int gPL = (int)(qG / 2); // ammount of pl units we can get
                                    for (int pl = gPL; pl >= 0; pl--)
                                    {
                                        int gCN = (int)(gPL / 5);
                                        for (int cn = gCN; cn >= 0; cn--)
                                        {
                                            int Q = ammount - ((cn * 40) + (pl * 8) + (g * 4));
                                            for (int q = 0; q <= Q * 2; q++) // g to q options ( includes cp and pn ) 
                                                combinations += (q * 2) + 1;
                                        }
                                    }
                                }
                            }
                            Console.WriteLine(combinations);
                        } break;
                    case "G":
                        {
                            combinations = 1; // keeping it in the same size
                            // to smaller recipients -- each G can be 4Q , 8 PN .... 
                            for (int g = ammount; g > 0; g--)
                            {
                                for (int q = 0; q <= g * 4; q++) // g to q options ( includes cp and pn ) 
                                    combinations += (q * 2) + 1;
                            }
                            // to bigger recipients
                            if (ammount >= 2)
                            {
                                int gPL = (int)(ammount / 2); // ammount of pl units we can get
                                    for(int pl = gPL; pl > 0; pl--)
                                    {
                                        int gCN = (int) (gPL / 5);
                                        for (int cn = gCN; cn >= 0; cn--)
                                        {
                                            int G = ammount - ((cn * 10) + (pl * 2));
                                            for (int q = 0; q <= G * 4; q++) // g to q options ( includes cp and pn ) 
                                                combinations += (q * 2) + 1;
                                        }
                                    }
                            }
                            Console.WriteLine(combinations);
                        } break;
                    case "PL":
                        {
                            combinations = 1; // keeping it in the same size
                            // to smaller recipients -- each PL can be 2G ... 
                            for (int pl = ammount; pl > 0; pl-- )
                            {
                                for (int g = pl*2; g >= 0; g--)
                                {
                                    for (int q = 0; q <= g * 4; q++) // g to q options ( includes cp and pn ) 
                                        combinations += (q * 2) + 1;
                                }
                            }
                            // to bigger recipients
                            if (ammount >= 5)
                            {
                                int plCN = (int)(ammount / 5);
                                for (int cn = plCN; cn > 0; cn--)
                                {
                                    int PL = ammount - (cn * 5); // 
                                    for (int pl = PL; pl > 0; pl--)
                                    {
                                        for (int g = pl * 2; g >= 0; g--)
                                        {
                                            for (int q = 0; q <= g * 4; q++) // g to q options ( includes cp and pn ) 
                                                combinations += (q * 2) + 1;
                                        }
                                    }
                                } 
                            }
                            Console.WriteLine(combinations);  
                        } break;
                    case "CN":
                        {
                            combinations = 1; // keeping it in the same size
                            // to smaller recipients -- each CN can be 5 PL
                            for(int cn = ammount; cn > 0; cn--)
                            {
                                for (int pl = cn * 5; pl >= 0; pl--) // permutating cn to pl options
                                {
                                    for (int g = pl * 2; g >= 0; g--) // pl to g options
                                    {
                                        for (int q = 0; q <= g * 4; q++) // g to q options ( includes cp and pn ) 
                                            combinations += (q * 2) + 1;
                                    }
                                }
                            }
                            Console.WriteLine(combinations);  
                        } break;
                }
                ammountRead = Console.ReadLine();
            }
            while (ammountRead != "#"); // so the program runs until we catch the break value. 
        }
    }
}



//2 Milkmen Aditya and Rahul were doing very good business in their village as partners and had many milk containers of the following sizes.

//The codes for each of the sizes are given in the braces they are supposed to be entered in the input as specified in INPUT section.


//Can (CN)     10 gallons

//Pail (PL)    2 gallons

//Gallon (G)

//Quart (Q) 1/4 gallon

//Pint (PN)   1/8 gallon

//Cup (CP)    1/16 gallon

//Now Rohan who lives in the same village took up a assignment to know in how many ways can the milkmen store X gallons of milk using any combination of these containers. For instance, the milkmen can store one Quart four ways:


//1: 1 quart

//2: 2 pints

//3: 1 pint + 2 cups

//4: 4 cups

//One gallon can be stored 26 different ways.

//In all data, X is a positive integer number and 1 <= X gallons <= 50. Rohans program must compute the number of combinations for each separate input value in less than ten seconds (which means that your program might run as long as 10*n seconds for n input values).

//Input

//Your program should read values from the file first the Quantity and then followed by the code for each of the sizes as specified above in the second line (and compute and print the number of combinations) until encountering a value of #.

//Output

//Your output should give the number of ways specified for the input.

//A example is given below:


//Sample Input

//1
//G
//#

//Sample Output

//26