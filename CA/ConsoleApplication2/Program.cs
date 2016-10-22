using System;
using System.Linq;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            mostAisleSeats(27,29);
        }

        public static string[] mostAisleSeats(int width, int seats)
        {
            bool continueS = true;
            char[] seatDisposition = new char[width];
            // we discard basic possibilities that do not allow for sorting of any kind. 
            if (seats >= width)
                {
                    continueS = false; 
                for(int a = 0; a < width; a++)
                    { seatDisposition[a] = 'S'; }
            }
            if(continueS && seats == 0)
                {
                    continueS = false;
                    for (int a = 0; a < width; a++)
                    { seatDisposition[a] = '.'; }
            }
            // if we have spare aisles or just enought
            if(continueS && ( (seats / 2) < (width - seats) ) )
            {
                int refWidth = width - 1;
                continueS = false;
                while (seats > 1)
                {
                    seatDisposition[refWidth] = 'S'; refWidth--;
                    seatDisposition[refWidth] = '.'; refWidth--;
                    seatDisposition[refWidth] = 'S'; refWidth--;
                    seats -= 2;
                }
                if (seats == 1)
                    { 
                    seatDisposition[refWidth] = 'S'; refWidth--; 
                }
                while (refWidth >= 0)
                {
                seatDisposition[refWidth] = '.'; refWidth--; 
                } 
            }
            

            // if we lack aisles.. we start with a seat, in this way s . s || s . s etc..  with just 1 we get 2
            if (continueS)
            {
                int refWidth = 0;
                int aisles = width - seats;
                while(aisles > 0)
                {
                    seatDisposition[refWidth] = 'S'; refWidth++;
                    seatDisposition[refWidth] = '.'; refWidth++;
                    seatDisposition[refWidth] = 'S'; refWidth++;
                    aisles -= 1;
                }
                for (int d = refWidth; d < width; d++ )
                { seatDisposition[d] = 'S';  }
                    continueS = false;
            }

            // transform seat disposition, in string arrays of at most 50 length
            string[] answer = new string[( ((int)(width / 50)) + 1)];

            for (int d = 0; d <= ((int)(width / 50)); d++ )
            {
                answer[d] = new String(seatDisposition.Skip(d * 50).Take(50).ToArray());
            }
            return null;
        }
        
    }
}
