using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET
{
    public class MemoryCorruptedException : Exception
    {
        public MemoryCorruptedException(string message)
        : base(message)
        {
        }
    }
}
