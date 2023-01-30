using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteLoader.NET.InteropServices.OrignalData;

[Flags]
public enum AccessType
{
    Public = 0,
    Private = 1,
    Protected = 2,
    Empty = 3
}

[Flags]
public enum FlagBits
{
    None = 0,
    Const = 1,
    Constructor = 2,
    Destructor = 3,
    Operate = 4,
    UnkonwnFunc = 5,
    StaticGlobalVar = 6,
    __Ptr64Spec = 7,
    HasThis = __Ptr64Spec,
    IsPureCall = 8
};

[Flags]
public enum StorageType
{
    Virtual,
    Static,
    Empty
};

[Flags]
public enum SymbolType
{
    Function = 0,
    Constructor = 1,
    Destructor,
    Operator,
    StaticField,
    UnknownVirtFunction = 5
}