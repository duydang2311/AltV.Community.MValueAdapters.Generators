﻿using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class DateTimeConverter : BaseConverter
{
    public override string[] AdditionalUsings() => ["System"];

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value((double)new DateTimeOffset(value.{propertyInfo.Name}).ToUnixTimeMilliseconds());");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = DateTime.UnixEpoch.AddMilliseconds((long)reader.NextDouble()).ToLocalTime();");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value((double)new DateTimeOffset(item).ToUnixTimeMilliseconds());");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(DateTime.UnixEpoch.AddMilliseconds((long)reader.NextDouble()).ToLocalTime());");
    }
}
