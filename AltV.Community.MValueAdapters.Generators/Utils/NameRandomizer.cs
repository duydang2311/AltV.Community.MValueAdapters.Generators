using System;
using System.Collections.Generic;

namespace AltV.Community.MValueAdapters.Generators.Utils;

public static class NameRandomizer
{
    public static string Get()
    {
        string guid;

        do guid = Guid.NewGuid().ToString().Replace("-", "");
        while (_usedNames.Contains(guid));

        _usedNames.Add(guid);
        return '_' + guid;
    }

    public static string[] Get(int count)
    {
        var propertyNames = new string[count];
        for (var i = 0; i < count; i++) propertyNames[i] = Get();
        return propertyNames;
    }

    private static readonly HashSet<string> _usedNames = [];
}
