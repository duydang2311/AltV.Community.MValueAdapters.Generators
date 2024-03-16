using System.Text;
using AltV.Community.MValueAdapters.Generators.Abstractions;
using AltV.Community.MValueAdapters.Generators.Models;
using AltV.Community.MValueAdapters.Generators.Utils;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class Vector2Converter : BaseConverter
{
    public override string[] AdditionalUsings() => ["System.Numerics"];

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.X);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.Y);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(3);

        stringBuilder.AppendLine(indentation, $"float {tmpNames[0]} = 0f, {tmpNames[1]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[2]} = reader.NextName();");
        stringBuilder.AppendLine(indentation, $"switch ({tmpNames[2]})");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new Vector2({tmpNames[0]}, {tmpNames[1]});");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.X);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.Y);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(4);

        stringBuilder.AppendLine(indentation, $"float {tmpNames[0]} = 0f, {tmpNames[1]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[2]} = reader.NextName();");
        stringBuilder.AppendLine(indentation, $"switch ({tmpNames[2]})");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[3]} = new Vector2({tmpNames[0]}, {tmpNames[1]});");
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add({tmpNames[3]});");
    }
}
