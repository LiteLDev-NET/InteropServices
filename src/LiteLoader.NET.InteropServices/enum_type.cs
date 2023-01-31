using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct enum_type<T> where T : struct, Enum
{
    private T value;

    private enum_type(T value)
    {
        this.value = value;
    }

    public T Value { get => value; set => this.value = value; }

    public static implicit operator enum_type<T>(T value)
    {
        return new(value);
    }

    public static explicit operator T(enum_type<T> value)
    {
        return value.value;
    }
}
