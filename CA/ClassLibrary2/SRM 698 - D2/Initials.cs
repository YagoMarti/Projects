using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.SRM_698___R1
{
    public class Initials
    {
        public String getInitials(String name)
        {
            string[] parts = name.Split();
            String result = "";
            foreach (string part in parts)
            {
                result += part.ToCharArray()[0];
            }
            return result;
        }
    }
}


/*
Problem Statement
    	When dealing with long names and phrases, we sometimes use the initial letters of words to form its acronym. For example, we use "JFK" instead of "John Fitzgerald Kennedy", "lgtm" instead of "looks good to me", and so on. 



You are given a String name. This String contains a phrase: one or more words separated by single spaces. Please compute and return the acronym of this phrase. (As above, the acronym should consist of the first letter of each word, in order.)
 
Definition
    	
Class:	Initials
Method:	getInitials
Parameters:	String
Returns:	String
Method signature:	String getInitials(String name)
(be sure your method is public)
    
 
Constraints
-	name will contain between 1 and 50 characters, inclusive.
-	Each character in s will be a space or a lowercase English letter ('a' - 'z').
-	The first and last character in s will not be a space.
-	No two continuous spaces can appear in s.
*/