using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT2CarGps
{
    public class TrailCountNegativeException : ArgumentException
    {
        public TrailCountNegativeException() : base("The maximum positions in the trail can't be less then 0")
        {

        }
    }
}
