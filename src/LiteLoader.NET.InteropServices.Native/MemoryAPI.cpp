#include <memory>
#include <Windows.h>

#pragma unmanaged

typedef unsigned long EXCEPTION_CODE;

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

EXCEPTION_CODE __delete_with_size(void** ptr, size_t size)
{
    __try
    {
        ::operator delete(*ptr, size);
        *ptr = nullptr;
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

EXCEPTION_CODE __delete_array(void** ptr)
{
    __try
    {
        ::operator delete[](*ptr);
        *ptr = nullptr;
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        return GetExceptionCode();
    }
    return 0;
}

#pragma managed

namespace LiteLoader::NET::InteropServices::Native
{
    public ref class MemoryAPI abstract sealed
    {
    public:
        static void* operator_new(size_t size)
        {
            try
            {
                return ::operator new(size);
            }
            catch (const std::bad_alloc&)
            {
                throw gcnew System::OutOfMemoryException();
            }
        }

        static EXCEPTION_CODE operator_delete(void** ptr)
        {
            if (ptr == nullptr)
                return 0;

            if (*ptr == nullptr)
                return 0;

            return  __delete(ptr);
        }

        static EXCEPTION_CODE operator_delete(void* ptr)
        {
            return operator_delete(&ptr);
        }

        static EXCEPTION_CODE operator_delete(void** ptr, size_t size)
        {
            if (ptr == nullptr)
                return 0;

            if (*ptr == nullptr)
                return 0;

            return  __delete_with_size(ptr, size);
        }

        static EXCEPTION_CODE operator_delete(void* ptr, size_t size)
        {
            return operator_delete(&ptr, size);
        }

        static EXCEPTION_CODE operator_delete_array(void** ptr)
        {
            if (ptr == nullptr)
                return 0;

            if (*ptr == nullptr)
                return 0;

            return  __delete_array(ptr);
        }

        static EXCEPTION_CODE operator_delete_array(void* ptr)
        {
            return operator_delete_array(&ptr);
        }
    };
}