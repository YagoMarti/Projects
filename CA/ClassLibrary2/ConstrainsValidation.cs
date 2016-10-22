using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class ConstrainsValidation
    {
        public Int32 betweenValues(ref int val, int min, int max)
        {
            if (val < min) val = min;
            if (val > max) val = max;
            return val;
        }
    }
}
