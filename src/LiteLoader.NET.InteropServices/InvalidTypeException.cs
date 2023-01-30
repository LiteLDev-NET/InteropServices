using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message)
        : base(message)
        {
        }
    }
}
