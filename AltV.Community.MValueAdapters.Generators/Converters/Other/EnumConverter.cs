using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal class EnumConverter : BaseConverter
{
    protected override void GenerateItemWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value({GetWriterValuePrefix(propertyInfo)}value{GetWriterValueSuffix(propertyInfo)});");
    }

    protected override void GenerateItemReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"c.{propertyInfo.Name} = ({propertyInfo.TypeName})reader{GetReaderSuffix(propertyInfo)};");
    }

    protected override void GenerateCollectionWriteCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"writer.Value({GetWriterValuePrefix(propertyInfo)}item{GetWriteCollectionItemSuffix(propertyInfo)});");
    }

    protected override void GenerateCollectionReadCode(StringBuilder stringBuilder, ref int indentation, MValueClassInfo classInfo, MValuePropertyInfo propertyInfo)
    {
        stringBuilder.AppendLine(indentation, $"{propertyInfo.Name}Builder.Add(({propertyInfo.TypeName})reader.{GetReaderSuffix(propertyInfo)});");
    }

	/// <summary>
	/// Returns a possible prefix/cast for the ItemWriteCode.
	/// Basically only performs a "double" or "bool" cast, if
	/// the underlying type is neither a number nor a bool.
	/// </summary>
	/// <param name="propertyInfo"></param>
	/// <returns></returns>
	private string GetWriterValuePrefix(MValuePropertyInfo propertyInfo)
	{
		if (propertyInfo.UnderlyingEnumTypeName == "char") return "";
		else if (propertyInfo.UnderlyingEnumTypeName == "string") return "";
		else if (propertyInfo.UnderlyingEnumTypeName == "bool") return "(bool)";
		else
			return "(double)";
	}

	/// <summary>
	/// Returns the suffix for the value writer.
	/// </summary>
	/// <param name="propertyInfo"></param>
	/// <returns></returns>
	private string GetWriterValueSuffix(MValuePropertyInfo propertyInfo)
	{
		if (propertyInfo.UnderlyingEnumTypeName == "char") return $".{propertyInfo.Name}.ToString()";
		else
			return $".{propertyInfo.Name}";
	}

	/// <summary>
	/// Returns the suffix for the write collection code of the item
	/// </summary>
	/// <param name="propertyInfo"></param>
	/// <returns></returns>
	private string GetWriteCollectionItemSuffix(MValuePropertyInfo propertyInfo)
	{
		if (propertyInfo.UnderlyingEnumTypeName == "char") return $".ToString()";
		else
			return "";
	}

	/// <summary>
	/// Returns the string for the reader to target next type.
	/// For texts (string / char), "FirstOrDefault" is appended.
	/// </summary>
	/// <param name="propertyInfo"></param>
	/// <returns></returns>
	private string GetReaderSuffix(MValuePropertyInfo propertyInfo)
	{
		if (propertyInfo.UnderlyingEnumTypeName == "char") return ".NextString()";
		else if (propertyInfo.UnderlyingEnumTypeName == "string") return ".NextString().FirstOrDefault()";
		else if (propertyInfo.UnderlyingEnumTypeName == "bool") return ".NextBool()";
		else
			return ".NextDouble()";
	}
}
