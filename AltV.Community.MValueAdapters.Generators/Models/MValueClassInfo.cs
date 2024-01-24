using AltV.Community.MValueAdapters.Generators.Abstractions;

namespace AltV.Community.MValueAdapters.Generators.Models;

internal class MValueClassInfo
{
    internal readonly string Name;
    internal readonly string Namespace;
    internal readonly MValuePropertyInfo[] PropertyInfos;
    internal readonly NamingConvention NamingConvention;
    
    internal MValueClassInfo(string name, string @namespace, MValuePropertyInfo[] propertyInfos, NamingConvention namingConvention)
    {
        Name = name;
        Namespace = @namespace;
        PropertyInfos = propertyInfos;
        NamingConvention = namingConvention;
    }
}
