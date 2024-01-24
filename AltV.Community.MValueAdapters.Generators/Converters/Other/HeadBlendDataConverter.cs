using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;
using AltV.Community.MValueAdapters.Generators.Utils;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class HeadBlendDataConverter : BaseConverter
{
    public override string[] AdditionalUsings() => ["AltV.Net.Data"];

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeFirstID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.ShapeFirstID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeSecondID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.ShapeSecondID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeThirdID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.ShapeThirdID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinFirstID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.SkinFirstID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinSecondID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.SkinSecondID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinThirdID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.SkinThirdID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeMix", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.ShapeMix);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinMix", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.SkinMix);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ThirdMix", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, $"writer.Value(value.{propertyInfo.Name}.ThirdMix);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(9);

        stringBuilder.AppendLine(indentation, $"uint {tmpNames[0]} = 0, {tmpNames[1]} = 0, {tmpNames[2]} = 0, {tmpNames[3]} = 0, {tmpNames[4]} = 0, {tmpNames[5]} = 0;");
        stringBuilder.AppendLine(indentation, $"float {tmpNames[6]} = 0f, {tmpNames[7]} = 0f, {tmpNames[8]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"switch (reader.NextName())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeFirstID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeSecondID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeThirdID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinFirstID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[3]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinSecondID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[4]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinThirdID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[5]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeMix", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[6]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinMix", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[7]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ThirdMix", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[8]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new HeadBlendData({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]}, {tmpNames[3]}, {tmpNames[4]}, {tmpNames[5]}, {tmpNames[6]}, {tmpNames[7]}, {tmpNames[8]});");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, "writer.BeginObject();");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeFirstID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.ShapeFirstID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeSecondID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.ShapeSecondID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeThirdID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.ShapeThirdID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinFirstID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.SkinFirstID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinSecondID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.SkinSecondID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinThirdID", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.SkinThirdID);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ShapeMix", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.ShapeMix);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("SkinMix", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.SkinMix);");
        stringBuilder.AppendLine(indentation, $"writer.Name(\"{NamingConventionHelpers.GetName("ThirdMix", classInfo.NamingConvention)}\");");
        stringBuilder.AppendLine(indentation, "writer.Value(item.ThirdMix);");
        stringBuilder.AppendLine(indentation, "writer.EndObject();");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        var tmpNames = NameRandomizer.Get(9);

        stringBuilder.AppendLine(indentation, $"uint {tmpNames[0]} = 0, {tmpNames[1]} = 0, {tmpNames[2]} = 0, {tmpNames[3]} = 0, {tmpNames[4]} = 0, {tmpNames[5]} = 0;");
        stringBuilder.AppendLine(indentation, $"float {tmpNames[6]} = 0f, {tmpNames[7]} = 0f, {tmpNames[8]} = 0f;");
        stringBuilder.AppendLine(indentation, "reader.BeginObject();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation, $"switch (reader.NextName())");
        stringBuilder.AppendLine(indentation++, "{");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeFirstID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[0]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeSecondID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[1]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeThirdID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[2]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinFirstID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[3]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinSecondID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[4]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinThirdID", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[5]} = reader.NextUInt();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ShapeMix", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[6]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("SkinMix", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[7]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, $"case \"{NamingConventionHelpers.GetName("ThirdMix", classInfo.NamingConvention)}\":");
        stringBuilder.AppendLine(indentation, $"{tmpNames[8]} = (float)reader.NextDouble();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation++, "default:");
        stringBuilder.AppendLine(indentation, "reader.SkipValue();");
        stringBuilder.AppendLine(indentation--, "continue;");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation--, "}");
        stringBuilder.AppendLine(indentation, "reader.EndObject();");
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(new HeadBlendData({tmpNames[0]}, {tmpNames[1]}, {tmpNames[2]}, {tmpNames[3]}, {tmpNames[4]}, {tmpNames[5]}, {tmpNames[6]}, {tmpNames[7]}, {tmpNames[8]}));");
    }
}
