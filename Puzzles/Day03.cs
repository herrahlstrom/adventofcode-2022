using System.Diagnostics;

namespace AdventOfCode2022.Puzzles;

internal class Day03 : IDay
{
    public int Day => 3;

    public string Name => "Rucksack Reorganization";

    public static int GetPrio(char c)
    {
        int ascii = (int)c;
        return ascii switch
        {
            >= 97 and <= 122 => ascii - 96,
            >= 65 and <= 90 => ascii - 38,
            _ => throw new UnreachableException(),
        };
    }

    public object FirstPart()
    {
        int result = 0;
        foreach (var racksack in File.ReadLines($"input/{Day:00}.txt"))
        {
            int compartmentSize = racksack.Length / 2;

            HashSet<char> commonCharacters = new();
            for (int i = 0; i < compartmentSize; i++)
            {
                if (racksack.IndexOf(racksack[i], compartmentSize) > 0 &&
                    commonCharacters.Add(racksack[i]))
                {
                    result += GetPrio(racksack[i]);
                }
            }
        }
        return result;
    }

    public object SecondPart()
    {
        int result = 0;

        foreach (var group in File.ReadLines($"input/{Day:00}.txt").Chunk(3))
        {
            HashSet<char> commonCharacters = new();

            foreach (char c in group[0])
            {
                if (group[1].Contains(c) &&
                    group[2].Contains(c) &&
                    commonCharacters.Add(c))
                {
                    result += GetPrio(c);
                }
            }
        }
        return result;
    }
}