using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;
using AltV.Community.MValueAdapters.Generators.Utils;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class ClothConverter : BaseConverter
{
    public override string[] AdditionalUsings() => ["AltV.Net.Data"];

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.Drawable);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.Texture);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Palette", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name}.Palette);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(4);
        stringBuilder.AppendLine(indentation, $"ushort {tmpNames[0]} = 0;");
        stringBuilder.AppendLine(indentation, $"byte {tmpNames[1]} = 0, {tmpNames[2]} = 0;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[3]} = reader.NextName();");
        stringBuilder.AppendLine(indentation, $"switch ({tmpNames[3]})");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (ushort)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Palette", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new Cloth({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]});");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.Drawable);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.Texture);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Palette", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((double)item.Palette);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(5);

        stringBuilder.AppendLine(indentation, $"ushort {tmpNames[0]} = 0;");
        stringBuilder.AppendLine(indentation, $"byte {tmpNames[1]} = 0, {tmpNames[2]} = 0;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[3]} = reader.NextName();");
        stringBuilder.AppendLine(indentation, $"switch ({tmpNames[3]})");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (ushort)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Palette", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = (byte)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"var {tmpNames[4]} = new Cloth({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]});");
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add({tmpNames[4]});");
    }
}
