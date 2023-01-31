namespace LiteLoader.NET.InteropServices.OrignalData;

[System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class AccessTypeAttribute : Attribute
{
    public AccessType accessType;

    public AccessTypeAttribute(AccessType accessType)
    {
        this.accessType = accessType;
    }
}

[System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class FakeSymbolAttribute : Attribute
{
    public string fakeSymbol;

    public FakeSymbolAttribute(string fakeSymbol)
    {
        this.fakeSymbol = fakeSymbol;
    }
}

[System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class RVAAttribute : Attribute
{
    public ulong rva;

    public RVAAttribute(ulong rva)
    {
        this.rva = rva;
    }
}

[System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class StorageClassAttribute : Attribute
{
    public StorageType storageType;

    public StorageClassAttribute(StorageType storageType)
    {
        this.storageType = storageType;
    }
}

[System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class SymbolAttribute : Attribute
{
    public string symbol;

    public SymbolAttribute(string symbol)
    {
        this.symbol = symbol;
    }
}
