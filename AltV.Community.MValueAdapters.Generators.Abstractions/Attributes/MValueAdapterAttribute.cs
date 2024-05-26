using AltV.Community.MValueAdapters.Generators.Abstractions;

namespace AltV.Community.MValueAdapters.Generators;

[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, Inherited = false)]
public sealed class MValueAdapterAttribute : System.Attribute
{
    public NamingConvention NamingConvention { get; set; }
}
