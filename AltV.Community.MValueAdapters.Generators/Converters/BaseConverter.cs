using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal abstract class BaseConverter : ITypeConverter
{
    protected abstract void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo);
    protected abstract void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo);
    protected abstract void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo);
    protected abstract void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo);

    public void WriteItem(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        if (propertyInfo.Nullable)
        {
            stringBuilder.AppendLine(indentation, $"if (value.{propertyInfo.Name} != null)");
            stringBuilder.AppendLine(indentation++, "{");
        }

        stringBuilder.AppendLine(indentation, $"writer.Name(\"{propertyInfo.CustomName ?? propertyInfo.Name}\");");
        GenerateItemWriteCode(stringBuilder, ref indentation, classInfo, propertyInfo);

        if (propertyInfo.Nullable)
        {
            stringBuilder.AppendLine(--indentation, "}");
        }
    }

    public void ReadItem(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation++, $"case \"{propertyInfo.CustomName ?? propertyInfo.Name}\":");
        GenerateItemReadCode(stringBuilder, ref indentation, classInfo, propertyInfo);
        stringBuilder.AppendLine(indentation--, "continue;");
    }

    public void WriteCollection(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType == PropertyType.Default)
        {
            WriteItem(stringBuilder, ref indentation, classInfo, propertyInfo);
            return;
        }

        if (propertyInfo.Nullable)
        {
            stringBuilder.AppendLine(indentation, $"if (value.{propertyInfo.Name} != null)");
            stringBuilder.AppendLine(indentation++, "{");
        }

        stringBuilder.AppendLine(indentation, $"writer.Name(\"{propertyInfo.CustomName ?? propertyInfo.Name}\");");
        stringBuilder.AppendLine(indentation, "writer.BeginArray();");
        stringBuilder.AppendLine(indentation, $"foreach (var item in value.{propertyInfo.Name})");
        stringBuilder.AppendLine(indentation++, "{");

        if (propertyInfo.NullableCollection)
        {
            stringBuilder.AppendLine(indentation, "if (item != null)");
            stringBuilder.AppendLine(indentation++, "{");
        }

        GenerateCollectionWriteCode(stringBuilder, ref indentation, classInfo, propertyInfo);

        if (propertyInfo.NullableCollection)
        {
            stringBuilder.AppendLine(--indentation, "}");
        }

        stringBuilder.AppendLine(--indentation, "}");
        stringBuilder.AppendLine(indentation, "writer.EndArray();");

        if (propertyInfo.Nullable)
        {
            stringBuilder.AppendLine(--indentation, "}");
        }
    }

    public void ReadCollection(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType == PropertyType.Default)
        {
            WriteItem(stringBuilder, ref indentation, classInfo, propertyInfo);
            return;
        }

        stringBuilder.AppendLine(indentation++, $"case \"{propertyInfo.CustomName ?? propertyInfo.Name}\":");
        stringBuilder.AppendLine(indentation, $"var {propertyInfo.Name}Builder = new List<{propertyInfo.TypeName}{(propertyInfo.NullableCollection ? "?" : "")}>();");
        stringBuilder.AppendLine(indentation, "reader.BeginArray();");
        stringBuilder.AppendLine(indentation, "while (reader.HasNext())");
        stringBuilder.AppendLine(indentation++, "{");
        GenerateCollectionReadCode(stringBuilder, ref indentation, classInfo, propertyInfo);
        stringBuilder.AppendLine(--indentation, "}");
        stringBuilder.AppendLine(indentation, "reader.EndArray();");

        if (propertyInfo.PropertyType == PropertyType.Array)
        {
            stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = {propertyInfo.Name}Builder.ToArray();");
        }
        else if (propertyInfo.PropertyType == PropertyType.List)
        {
            stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new({propertyInfo.Name}Builder);");
        }

        stringBuilder.AppendLine(indentation--, "continue;");
    }
}
