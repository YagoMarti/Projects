using System;

namespace ClassLibrary2.TopCoder_Medium
{
    public class FolderSize : ConstrainsValidation
    {
        public int[] calculateWaste(string[] files, int folderCount, int clusterSize)
        {
            // files is a string, of the kind "a b", whereas a is the folder in which the file is saved, b is the size of the file. 

            // folder Count stands for the number of folders... there can be empty folders. 
            int[] result = new int[folderCount];

            foreach(string file in files)
            {
                string[] decompFile = file.Split();
                int usedSpace = Convert.ToInt32( decompFile[1] );
                // cluster size indicates the size of each stored unit, we calculate the waste over it
                result[Convert.ToInt32(decompFile[0])] += clusterSize - (Convert.ToInt32(decompFile[1]) % clusterSize);
            }


            return result;
        }
    }
}


/*
Problem Statement

When files are stored on disk, typically they are stored in "clusters". Each cluster has a fixed size, and the amount of space consumed by a file is always a multiple of the cluster size. Thus, if the cluster size is 100 bytes, a 165 byte file will actually use 200 bytes of storage, resulting in 35 bytes of wasted space.

We want to determine which areas of our disk storage are wasting the most space. You will be given a files, each element of which contains a folder number followed by a space, followed by a file size. You will also be given an folderCount indicating the total number of folders on our disk. The folders are numbered 0 through folderCount - 1. Finally, you will be given an clusterSize, indicating how large each disk cluster is.

You are to return a , containing exactly folderCount elements, each element of which is the total amount of wasted space for that folder. Each element of the return value corresponds to the folder with the same index.
Definition
Class:
FolderSize
Method:
calculateWaste
Parameters:
string[], int, int
Returns:
int[]
Method signature:
int[] calculateWaste(string[] files, int folderCount, int clusterSize)
(be sure your method is public)
Limits
Time limit (s):
840.000
Memory limit (MB):
64
Notes
- While many systems use a cluster size that is a power of two, no such restriction exists here.
- There may be folders that have no files in them. (Wasted space for such a folder is 0.)
Constraints
- clusterSize will be between 1 and 1000000, inclusive
- folderCount will be between 1 and 50, inclusive
- files will contain between 0 and 50 elements, inclusive
- Each element of files will contain between 3 and 50 characters, inclusive
- Each element of files will be in the form " " (quotes added for clarity)
- Each value of will be between 0 and folderCount - 1, inclusive
- Each value of will be between 0 and 1000000, inclusive
*/