using LiteLoader.NET.InteropServices.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LiteLoader.NET.InteropServices;

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
public unsafe struct reference<T> : IConstructableCppClass<reference<T>> where T : IConstructableCppClass<T>, new()
{
    [FieldOffset(0)]
    private nint ptr;

    internal reference(nint p)
    {
        ptr = p;
    }

    public nint NativePointer { get => ptr; set => ptr = value; }
    public bool OwnsNativeInstance { get { return true; } set { return; } }

    public static implicit operator reference<T>(T val)
    {
        return new reference<T>(val.NativePointer);
    }
    
    public static explicit operator T (reference<T> val)
    {
        return val.Dereference();
    }

    public reference<T> ConstructInstance(nint ptr, bool ownsInstance)
    {
        var ret = default(reference<T>);
        ret.ptr = ptr;
        return ret;
    }
    public void Destruct()
    {
        return;
    }

    public ulong GetClassSize()
    {
        return 8;
    }

    public void SetNativePointer(nint ptr, bool ownsInstance)
    {
        this.ptr = ptr;
    }

    private static T _EmptyTval = default!;

    public T Dereference()
    {
        if (ICppClassHelper<T>.isValueType)
        {
            if (ICppClassHelper<T>.isICppClass)
            {
                return ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(ref _EmptyTval, Unsafe.Read<nint>(ptr.ToPointer()), false);
            }
            return Unsafe.Read<T>(ptr.ToPointer());
        }
        else
        {
            return ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(_EmptyTval, Unsafe.Read<nint>(ptr.ToPointer()), false);
        }
    }
}