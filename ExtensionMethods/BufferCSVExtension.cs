using System.Text;

namespace transit_parser.ExtensionMethods;

public static class BufferCSVExtension
{
    public static char[] ParseSection(this char[] lineBuffer, int position = 1)
    {
        var lastCommaPosition = 0;
        var count = 0;

        for (var i = 0; i < lineBuffer.Length; i++)
        {
            if (lineBuffer[i] != ',') continue;

            count++;

            if (count == position)
            {
                return lineBuffer[lastCommaPosition..i];
            }

            lastCommaPosition = i + 1;
        }

        return Array.Empty<char>();
    }

    public static string AsString(this char[] characters)
    {
        return new string(characters);
    }
}