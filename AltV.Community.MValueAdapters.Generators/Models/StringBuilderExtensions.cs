// ReSharper disable once CheckNamespace
namespace System.Text;

internal static class StringBuilderExtensions
{
    internal static void AppendLine(this StringBuilder sb, int indentation, string text)
    {
        var line = $"{new string('\t', indentation)}{text}";
        sb.AppendLine(line);
    }
}