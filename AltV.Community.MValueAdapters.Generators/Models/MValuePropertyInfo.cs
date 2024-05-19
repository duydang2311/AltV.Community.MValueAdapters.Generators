namespace AltV.Community.MValueAdapters.Generators.Models;

internal class MValuePropertyInfo
{
    internal readonly PropertyType PropertyType;
    internal readonly string Name;
    internal readonly string TypeName;
    internal readonly string? CustomName;
    internal readonly bool Nullable;
    internal readonly bool NullableCollection;
    internal bool IsEnum => UnderlyingEnumTypeName is not null;
    // If set, prop is considered enum
    internal readonly string? UnderlyingEnumTypeName;
    internal string? AdditionalUsing;

    internal MValuePropertyInfo(PropertyData propertyData, string name, string? customName = null, string? underlyingEnumTypeName = null, string? additionalUsing = null)
    {
        PropertyType = propertyData.Type;
        TypeName = propertyData.TypeName;
        Nullable = propertyData.Nullable;
        NullableCollection = propertyData.NullableCollection;
        UnderlyingEnumTypeName = underlyingEnumTypeName;
        Name = name;
        CustomName = customName;
        AdditionalUsing = additionalUsing;
    }
}
