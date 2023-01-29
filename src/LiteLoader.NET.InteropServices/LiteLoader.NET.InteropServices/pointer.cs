using LiteLoader.NET.InteropServices.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET.InteropServices;

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
public unsafe struct pointer<T> : IConstructableCppClass<pointer<T>> where T : IConstructableCppClass<T>, new()
{
    [FieldOffset(0)]
    private nint ptr;

    public nint NativePointer { get => ptr; set => ptr = value; }
    public bool OwnsNativeInstance { get { return true; } set { return; } }

    public pointer<T> ConstructInstance(nint ptr, bool ownsInstance)
    {
        var ret = default(pointer<T>);
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

    /// <summary>
    /// C++ operator delete
    /// </summary>
    public void Delete()
    {
        var pointer = ptr.ToPointer();
        if (ICppClassHelper<T>.isValueType)
        {
            if (ICppClassHelper<T>.isICppClass)
            {
                var instance = ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(
                    ref _EmptyTval!, Unsafe.Read<nint>(pointer), false);
                ICppClassHelper<T>._Value_type_funcptr_def.dtor(ref instance);
            }
        }
        else
        {
            var instance = ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(
                _EmptyTval, Unsafe.Read<nint>(pointer), false);
            ICppClassHelper<T>._Ref_type_funcptr_def.dtor(instance);
        }
        MemoryAPI.operator_delete(Unsafe.Read<nint>(pointer).ToPointer());
    }

    public unsafe void DeleteArray()
    {
        byte* pointer = (byte*)ptr.ToPointer();
        if (!ICppClassHelper<T>.isICppClass)
        {
            return;
        }
        uint num = *(uint*)((ulong)(nint)pointer - 4uL);
        if (num + 39 <= num)
        {
            throw new MemoryCorruptedException($"address:{(long)(nint)pointer}.");
        }
        byte* ptr2 = (byte*)(ICppClassHelper<T>.type_size * num + (ulong)(nint)pointer);
        if (ICppClassHelper<T>.isValueType)
        {
            byte* ptr3 = pointer;
            if (pointer < ptr2)
            {
                do
                {
                    var instance = ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(ref _EmptyTval, (nint)pointer, false);
                    ICppClassHelper<T>._Value_type_funcptr_def.dtor(ref _EmptyTval);
                    ptr3 = (byte*)(ICppClassHelper<T>.type_size + (ulong)(nint)ptr3);
                }
                while (ptr3 < ptr2);
            }
        }
        else
        {
            byte* ptr4 = pointer;
            if (pointer < ptr2)
            {
                do
                {
                    var instance = ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(_EmptyTval, (nint)ptr4, false);
                    ICppClassHelper<T>._Ref_type_funcptr_def.dtor(instance);
                    ptr4 = (byte*)(ICppClassHelper<T>.type_size + (ulong)(nint)ptr4);
                }
                while (ptr4 < ptr2);
            }
        }
        MemoryAPI.operator_delete_array(pointer);
    }

    public T Dereference()
    {
        if(ICppClassHelper<T>.isValueType)
        {
            if(ICppClassHelper<T>.isICppClass)
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
