#include <memory>
#include <Windows.h>

typedef unsigned long EXCEPTION_CODE;

EXCEPTION_CODE __new(void*& ptr, size_t size)
{
    __try
    {
        ptr = ::operator new(size);
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

EXCEPTION_CODE __new_array(void*& ptr, size_t size)
{
    __try
    {
        ptr = ::operator new[](size);
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

EXCEPTION_CODE __delete(void** ptr)
{
    __try
    {
        ::operator delete(*ptr);
        *ptr = nullptr;
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

EXCEPTION_CODE __delete_with_size(void*& ptr, size_t size)
{
    __try
    {
        ::operator delete(ptr, size);
        ptr = nullptr;
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

EXCEPTION_CODE __delete_array(void*& ptr)
{
    __try
    {
        ::operator delete[](ptr);
        ptr = nullptr;
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

extern "C"
{
    __declspec(dllexport) EXCEPTION_CODE MemoryAPI_operator_new(void*& ptr, size_t size)
    {
        return __new(ptr, size);
    }
    
    __declspec(dllexport) EXCEPTION_CODE MemoryAPI_operator_new_array(void*& ptr, size_t size)
    {
        return __new_array(ptr, size);
    }

    __declspec(dllexport) EXCEPTION_CODE MemoryAPI_operator_delete(void* ptr)
    {
        return __delete(&ptr);
    }

    __declspec(dllexport) EXCEPTION_CODE MemoryAPI_operator_delete_with_size(void* ptr, size_t size)
    {
        if (ptr == nullptr)
            return 0;

        return  __delete_with_size(ptr, size);
    }

    __declspec(dllexport) EXCEPTION_CODE MemoryAPI_operator_delete_array(void* ptr)
    {
        return __delete_array(ptr);
    }

    __declspec(dllexport) void MemoryAPI_memmove(void* dst, void* src, size_t size)
    {
        ::memmove(dst, src, size);
    }
}