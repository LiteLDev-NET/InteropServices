using LiteLoader.NET.InteropServices.Native;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LiteLoader.NET.InteropServices;

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
public unsafe struct pointer<T> : IConstructableCppClass<pointer<T>> where T : IConstructableCppClass<T>, new()
{
    [FieldOffset(0)]
    private nint ptr;

    internal pointer(nint p)
    {
        ptr = p;
    }

    public nint NativePointer { get => ptr; set => ptr = value; }
    public bool OwnsNativeInstance { get { return true; } set { return; } }

    public static implicit operator pointer<T>(T val)
    {
        return new pointer<T>(val.NativePointer);
    }

    public static explicit operator T(pointer<T> val)
    {
        return val.Dereference();
    }

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
        if (ICppClassHelper<T>.isValueType)
        {
            var instance = ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(
                ref _EmptyTval!, ptr, false);
            ICppClassHelper<T>._Value_type_funcptr_def.dtor(ref instance);
        }
        else
        {
            var instance = ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(
                _EmptyTval, ptr, false);
            ICppClassHelper<T>._Ref_type_funcptr_def.dtor(instance);
        }
        MemoryAPI.operator_delete(ptr.ToPointer());
    }

    public unsafe void DeleteArray()
    {
        byte* pointer = (byte*)ptr.ToPointer();
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
        if (ICppClassHelper<T>.isValueType)
        {
            return ICppClassHelper<T>._Value_type_funcptr_def.construct_instance(ref _EmptyTval, ptr, false);
        }
        else
        {
            return ICppClassHelper<T>._Ref_type_funcptr_def.construct_instance(_EmptyTval, ptr, false);
        }
    }
}
