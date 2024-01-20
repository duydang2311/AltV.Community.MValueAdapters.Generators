using System.Text;

namespace AltV.Community.MValueAdapters.Generators.Models;

internal class NamingConventionHelpers
{
    public static string ToUpper(string name)
    {
        return name.ToUpper();
    }

    public static string ToLower(string name)
    {
        return name.ToLower();
    }

    public static string ToPascalCase(string name)
    {
        var result = new StringBuilder(name.Length);
        var nextUpper = true;
        foreach (var c in name)
        {
            if (char.IsLetterOrDigit(c))
            {
                if (nextUpper)
                {
                    result.Append(char.ToUpper(c));
                    nextUpper = false;
                }
                else
                {
                    result.Append(c);
                }
            }
            else
            {
                nextUpper = true;
            }
        }

        return result.ToString();
    }

    public static string ToCamelCase(string name)
    {
        var pc = ToPascalCase(name);

        if (pc.Length == 0) return pc;

        var firstChar = char.ToLower(pc[0]);
        if (pc.Length == 1) return firstChar.ToString();

        return $"{firstChar}{pc.Substring(1)}";

    }
}
