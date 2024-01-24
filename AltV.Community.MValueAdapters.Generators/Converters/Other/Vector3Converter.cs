using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;
using AltV.Community.MValueAdapters.Generators.Utils;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class Vector3Converter : BaseConverter
{
    public override string[] AdditionalUsings() => ["System.Numerics"];
    
    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((float)value.X);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((float)value.Y);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Z", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((float)value.Z);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(4);

        stringBuilder.AppendLine(indentation, $"float {tmpNames[0]} = 0f, {tmpNames[1]} = 0f, {tmpNames[2]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[3]} = reader.NextName();");
        stringBuilder.AppendLine(indentation, $"switch ({tmpNames[3]})");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Z", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new Vector3({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]});");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((float)item.X);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((float)item.Y);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Z", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((float)item.Z);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(5);

        stringBuilder.AppendLine(indentation, $"float {tmpNames[0]} = 0f, {tmpNames[1]} = 0f, {tmpNames[2]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[3]} = reader.NextName();");
        stringBuilder.AppendLine(indentation, $"switch ({tmpNames[3]})");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("X", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Y", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Z", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[4]} = new Vector3({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]});");
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add({tmpNames[4]});");
    }
}