namespace transit_parser.ExtensionMethods;

public static class BufferCSVExtension
{
    public static string ParseSection(this char[] lineBuffer, int position = 1)
    {
        var lastCommaPosition = 0;
        var count = 0;

        for (var i = 0; i < lineBuffer.Length; i++)
        {
            if (lineBuffer[i] != ',') continue;

            count++;

            if (count == position)
            {
                return new string(lineBuffer[lastCommaPosition..i]);
            }

            lastCommaPosition = i + 1;
        }

        return string.Empty;
    }
}