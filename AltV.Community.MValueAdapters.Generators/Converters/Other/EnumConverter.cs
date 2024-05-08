using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class EnumConverter : BaseConverter
{
    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value(({propertyInfo.UnderlyingEnumTypeName})value.{propertyInfo.Name});");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = ({propertyInfo.TypeName})reader.{Next(propertyInfo)}();");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value(({propertyInfo.UnderlyingEnumTypeName})item);");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(({propertyInfo.TypeName})reader.{Next(propertyInfo)}());");
    }

	private string Next(MValuePropertyInfo propertyInfo)
	{
		if (IsUnderlyingText(propertyInfo)) return "NextString";
		else if (IsUnderlyingBool(propertyInfo)) return "NextBool";
		else
			return "NextDouble";
	}

	private bool IsUnderlyingText(MValuePropertyInfo propertyInfo)
	{
		return propertyInfo.UnderlyingEnumTypeName == "string" || propertyInfo.UnderlyingEnumTypeName == "char";
	}

	private bool IsUnderlyingBool(MValuePropertyInfo propertyInfo)
	{
		return propertyInfo.UnderlyingEnumTypeName == "bool";
	}
}
