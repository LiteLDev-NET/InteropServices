using LiteLoader.NET.InteropServices.OrignalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET.InteropServices;

[System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class VirtualCppClassAttribute : Attribute
{
    public int vtableLength;

    public VirtualCppClassAttribute(int vtableLength)
    {
        this.vtableLength = vtableLength;
    }
}
