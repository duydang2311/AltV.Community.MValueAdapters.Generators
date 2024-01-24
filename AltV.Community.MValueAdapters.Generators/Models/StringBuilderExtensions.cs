// ReSharper disable once CheckNamespace
namespace System.Text;

internal static class StringBuilderExtensions
{
    internal static void AppendLine(this StringBuilder sb, int indentation, string text)
    {
        var line = $"{new string(' ', indentation * 4)}{text}";
        sb.AppendLine(line);
    }
}
