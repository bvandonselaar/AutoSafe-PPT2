using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAS
{
    public class SendNullException : Exception
    {
        public SendNullException()
        {
        }

        public SendNullException(string message) : base(message)
        {
        }

        public SendNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
