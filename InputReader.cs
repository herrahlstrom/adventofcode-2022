namespace AdventOfCode2022;

internal static class InputReader
{
    public static IEnumerable<string> ReadLines(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new StreamReader(stream);

        while (reader.ReadLine() is string line)
        {
            yield return line;
        }
    }
}
