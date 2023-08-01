using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core.CustomExceptions
{
    public class InvalidUsernamePasswordException : Exception
    {
        public InvalidUsernamePasswordException()
        {
        }

        public InvalidUsernamePasswordException(string? message) : base(message)
        {
        }

        public InvalidUsernamePasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUsernamePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
