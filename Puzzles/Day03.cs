using System.Diagnostics;

namespace AdventOfCode2022.Puzzles;

internal class Day03 : IDay
{
    private List<string> m_rucksacks = new();

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

    public string? FirstPart()
    {
        int result = 0;
        foreach (var racksack in m_rucksacks)
        {
            int compartmentSize = racksack.Length / 2;

            HashSet<char> commonCharacters = new();
            for (int i = 0; i < compartmentSize; i++)
            {
                if ((racksack.IndexOf(racksack[i], compartmentSize) > 0) && commonCharacters.Add(racksack[i]))
                {
                    result += GetPrio(racksack[i]);
                }
            }
        }
        return $"{result}";
    }

    public void ReadInput()
    {
        InputReader.ReadLines("input/03.txt", line =>
        {
            m_rucksacks.Add(line);
        });
    }

    public string? SecondPart()
    {
        int result = 0;
        for (int i = 0; i < m_rucksacks.Count; i += 3)
        {
            var rs1 = m_rucksacks[i];
            var rs2 = m_rucksacks[i + 1];
            var rs3 = m_rucksacks[i + 2];
            HashSet<char> commonCharacters = new();

            for (int j = 0; j < rs1.Length; j++)
            {
                char c = rs1[j];

                if (rs2.Contains(c) && rs3.Contains(c) && commonCharacters.Add(c))
                {
                    result += GetPrio(c);
                }
            }
        }
        return $"{result}";
    }
}