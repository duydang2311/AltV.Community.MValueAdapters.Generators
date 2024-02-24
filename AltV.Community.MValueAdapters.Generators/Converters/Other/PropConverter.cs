using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;
using AltV.Community.MValueAdapters.Generators.Utils;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class PropConverter : BaseConverter
{
    public override string[] AdditionalUsings() => ["AltV.Net.Data"];

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((long)value.{propertyInfo.Name}.Drawable);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value((long)value.{propertyInfo.Name}.Texture);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(2);

        stringBuilder.AppendLine(indentation, $"ushort {tmpNames[0]} = 0;");
        stringBuilder.AppendLine(indentation, $"byte {tmpNames[1]} = 0;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"switch (reader.NextName())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (ushort)reader.NextLong();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (byte)reader.NextLong();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new Prop({tmpNames[0]}, {tmpNames[1]});");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((long)item.Drawable);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value((long)item.Texture);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(2);

        stringBuilder.AppendLine(indentation, $"ushort {tmpNames[0]} = 0;");
        stringBuilder.AppendLine(indentation, $"byte {tmpNames[1]} = 0;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"switch (reader.NextName())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Drawable", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = (ushort)reader.NextLong();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("Texture", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = (byte)reader.NextLong();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(new Prop({tmpNames[0]}, {tmpNames[1]}));");
    }
}
