using System;

namespace AltV.Community.MValueAdapters.Generators;

/// <summary>
/// Initializes a new instance of the <see cref=""MValuePropertyNameAttribute""/> class with the specified property name.
/// </summary>
/// <param name=""propertyName"">The alternative name used for serialization and deserialization.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class MValuePropertyNameAttribute(string propertyName) : System.Attribute
{
    /// <summary>
    /// Gets the alternative name used for serialization and deserialization.
    /// </summary>
    public readonly string PropertyName = propertyName;
}
