using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class ByAdapterConverter(string typeName) : BaseConverter()
{
    private readonly string _typeName = typeName;

    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"new {_typeName}Adapter().ToMValue(value.{propertyInfo.Name}, writer);");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = new {_typeName}Adapter().FromMValue(reader);");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo)
    {
        // TODO: improve performance by reuse adapter instance
        stringBuilder.AppendLine(indentation, $"new {_typeName}Adapter().ToMValue(value.{propertyInfo.Name}, writer);");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo)
    {
        // TODO: improve performance by reuse adapter instance
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(new {_typeName}Adapter().FromMValue(reader));");
    }
}
