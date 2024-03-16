using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;
using AltV.Community.MValueAdapters.Generators.Utils;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class HeadOverlayConverter : BaseConverter
{
    public override string[] AdditionalUsings() => ["AltV.Net.Data"];

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Index", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.Index);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Opacity", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.Opacity);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ColorType", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.ColorType);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ColorIndex", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.ColorIndex);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SecondColorIndex", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.SecondColorIndex);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(5);

        stringBuilder.AppendLine(indentation, $"byte {tmpNames[0]} = 0, {tmpNames[2]} = 0, {tmpNames[3]} = 0, {tmpNames[4]} = 0;");
        stringBuilder.AppendLine(indentation, $"float {tmpNames[1]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"switch (reader.NextName())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Index", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Opacity", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ColorType", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ColorIndex", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[3]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SecondColorIndex", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[4]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new HeadOverlay({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]}, {tmpNames[3]}, {tmpNames[4]});");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Index", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.Index);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Opacity", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.Opacity);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ColorType", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.ColorType);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ColorIndex", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.ColorIndex);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SecondColorIndex", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.SecondColorIndex);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(9);

        stringBuilder.AppendLine(indentation, $"byte {tmpNames[0]} = 0, {tmpNames[2]} = 0, {tmpNames[3]} = 0, {tmpNames[4]} = 0;");
        stringBuilder.AppendLine(indentation, $"float {tmpNames[1]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"switch (reader.NextName())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Index", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Opacity", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ColorType", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ColorIndex", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[3]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SecondColorIndex", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[4]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(new HeadOverlay({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]}, {tmpNames[3]}, {tmpNames[4]}));");
    }
}
