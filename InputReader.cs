namespace AdventOfCode2022;

internal static class InputReader
{
    public static void ReadLines(string path, Action<string> lineRead)
    {
        using var stream = File.OpenRead(path);
        using var reader = new StreamReader(stream);

        while (reader.ReadLine() is string line)
        {
            lineRead.Invoke(line);
        }
    }
}
