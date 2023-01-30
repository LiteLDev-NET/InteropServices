using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET.InteropServices;

public interface ICopyable<TSelf> where TSelf : ICopyable<TSelf>
{
    /// <summary>
    /// static method, just invoke copy_ctor and return the obj.
    /// </summary>
    TSelf ConstructInstanceByCopy(TSelf _Right);
}
