using System.Text;
using AltV.Community.MValueAdapters.Generators.Models;

namespace AltV.Community.MValueAdapters.Generators.Converters;

internal interface ITypeConverter
{
    public abstract void WriteItem(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo);
    public abstract void ReadItem(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo);
    public void WriteCollection(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo);
    public void ReadCollection(StringBuilder stringBuilder, ref int indentation, MValuePropertyInfo propertyInfo);
}
