using System;

namespace AltV.Community.MValueAdapters.Generators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class MValueIgnoreAttributeName : Attribute { }
