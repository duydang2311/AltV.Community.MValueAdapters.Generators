using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class EnumConverter : BaseConverter
{
    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value((double)value.{propertyInfo.Name});");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = ({propertyInfo.TypeName})reader.NextDouble();");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value((double)item);");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(({propertyInfo.TypeName})reader.NextDouble();");
    }
}
