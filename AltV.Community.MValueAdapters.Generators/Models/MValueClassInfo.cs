﻿namespace AltV.Community.MValueAdapters.Generators.Models;

internal class MValueClassInfo
{
    internal readonly string Name;
    internal readonly string Namespace;
    internal readonly MValuePropertyInfo[] PropertyInfos;

    internal MValueClassInfo(string name, string @namespace, MValuePropertyInfo[] propertyInfos)
    {
        Name = name;
        Namespace = @namespace;
        PropertyInfos = propertyInfos;
    }
}
