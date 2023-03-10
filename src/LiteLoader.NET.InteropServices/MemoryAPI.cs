using System.Runtime.InteropServices;
using System.Security;
using EXCEPTION_CODE = System.UInt64;
using size_t = System.UInt64;

namespace LiteLoader.NET.InteropServices.Native
{
    public unsafe static class MemoryAPI
    {
        [DllImport("LiteLoader.NET.InteropServices.Native.dll",
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "MemoryAPI_operator_new",
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern EXCEPTION_CODE internal_operator_new(out void* ptr, size_t size);

        [DllImport("LiteLoader.NET.InteropServices.Native.dll",
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "MemoryAPI_operator_new_array",
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern EXCEPTION_CODE internal_operator_new_array(out void* ptr, size_t size);

        [DllImport("LiteLoader.NET.InteropServices.Native.dll",
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "MemoryAPI_operator_delete",
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern EXCEPTION_CODE internal_operator_delete(void* ptr);

        [DllImport("LiteLoader.NET.InteropServices.Native.dll",
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "MemoryAPI_operator_delete_with_size",
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern EXCEPTION_CODE internal_operator_delete_with_size(void* ptr, size_t size);

        [DllImport("LiteLoader.NET.InteropServices.Native.dll",
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "MemoryAPI_operator_delete_array",
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern EXCEPTION_CODE internal_operator_delete_array(void* ptr);

        [DllImport("LiteLoader.NET.InteropServices.Native.dll",
            CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "MemoryAPI_memmove",
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        private static extern EXCEPTION_CODE internal_memmove(void* dst, void* src, size_t size);

        public static void* operator_new(size_t size)
        {
            if (internal_operator_new(out var ptr, size) == 0)
            {
                return ptr;
            }

            throw new OutOfMemoryException();
        }

        public static void* operator_new_array(size_t size)
        {
            if (internal_operator_new(out var ptr, size) == 0)
            {
                return ptr;
            }

            throw new OutOfMemoryException();
        }

        public static void operator_delete(void* ptr)
        {
            if (internal_operator_delete(ptr) != 0)
                throw new MemoryCorruptedException(string.Empty);
        }

        public static void operator_delete_with_size(void* ptr, size_t size)
        {
            if (internal_operator_delete_with_size(ptr, size) != 0)
                throw new MemoryCorruptedException(string.Empty);
        }

        public static void operator_delete_array(void* ptr)
        {
            if (internal_operator_delete_array(ptr) != 0)
                throw new MemoryCorruptedException(string.Empty);
        }

        public static void operator_memmove(void* dst, void* src, size_t size)
        {
            if (internal_memmove(dst, src, size) != 0)
                throw new MemoryCorruptedException(string.Empty);
        }
    }
}
