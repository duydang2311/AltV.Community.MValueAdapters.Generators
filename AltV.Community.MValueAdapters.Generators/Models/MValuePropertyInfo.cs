namespace AltV.Community.MValueAdapters.Generators.Models;

internal class MValuePropertyInfo
{
    internal readonly PropertyType PropertyType;
    internal readonly string Name;
    internal readonly string TypeName;
    internal readonly string? CustomName;
    internal readonly bool Nullable;
    internal readonly bool NullableCollection;

    internal MValuePropertyInfo(PropertyData propertyData, string name, string? customName = null)
    {
        PropertyType = propertyData.Type;
        TypeName = propertyData.TypeName;
        Nullable = propertyData.Nullable;
        NullableCollection = propertyData.NullableCollection;

        Name = name;
        CustomName = customName;
    }
}
