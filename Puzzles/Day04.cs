using System;
using System.Linq;

namespace AdventOfCode2022.Puzzles;
internal class Day04 : IDay
{
    public int Day => 4;

    public string Name => "Camp Cleanup";

    public long FirstPart()
    {
        int result = 0;
        foreach (var line in File.ReadLines($"input/{Day:00}.txt"))
        {
            var pair = GetRanges(line);

            if (pair.a.start <= pair.b.start && pair.a.end >= pair.b.end ||
                pair.b.start <= pair.a.start && pair.b.end >= pair.a.end)
            {
                result++;
            }

        }

        return result;
    }

    public long SecondPart()
    {
        int result = 0;
        foreach (var line in File.ReadLines($"input/{Day:00}.txt"))
        {
            var pair = GetRanges(line);

            if (pair.a.start <= pair.b.end && pair.a.end >= pair.b.start ||
                pair.b.start <= pair.a.end && pair.b.end >= pair.a.start)
            {
                result++;
            }

        }

        return result;
    }

    private ((int start, int end) a, (int start, int end) b) GetRanges(string line)
    {
        var lineSpan = line.AsSpan();

        int leftDash = lineSpan.IndexOf('-');
        int pairSplitter = lineSpan.IndexOf(',');
        int rightDash = lineSpan.LastIndexOf('-');

        int aStart = int.Parse(lineSpan.Slice(0, leftDash));
        int aEnd = int.Parse(lineSpan.Slice(leftDash + 1, pairSplitter - leftDash - 1));
        int bStart = int.Parse(lineSpan.Slice(pairSplitter + 1, rightDash - pairSplitter - 1));
        int bEnd = int.Parse(lineSpan.Slice(rightDash + 1));

        return ((aStart, aEnd), (bStart, bEnd));
    }
}
