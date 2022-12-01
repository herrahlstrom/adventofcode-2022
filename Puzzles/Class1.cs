using AdventOfCode2022.Infrastructure;

namespace AdventOfCode2022.Puzzles;

public class Day01 : IDay
{
    private readonly List<Elf> m_elfs = new();

    public string? FirstPart()
    {
        var maxCalories = m_elfs.Max(x => x.TotalCalories);
        return $"{maxCalories}";
    }

    public void ReadInput()
    {
        using var stream = File.OpenRead("input/01.txt");
        using var reader = new StreamReader(stream);

        Elf? currentElf = null;
        while (reader.ReadLine() is string line)
        {
            if (line.Length == 0)
            {
                currentElf = null;
            }
            else if (int.TryParse(line, out int calories))
            {
                if (currentElf is null)
                {
                    currentElf = new();
                    m_elfs.Add(currentElf);
                }

                currentElf.AddCalories(calories);
            }
            else
            {
                throw new InvalidDataException("Invalid input line: " + line);
            }
        }
    }

    public string? SecondPart()
    {
        var sumCaloriesTop3Elfs = m_elfs
            .OrderByDescending(x => x.TotalCalories)
            .Take(3).Sum(x => x.TotalCalories);
        return $"{sumCaloriesTop3Elfs}";
    }

    public int Day => 1;

    public string Name => "Calorie Counting";

    class Elf
    {
        private readonly List<int> m_calories = new();

        public void AddCalories(int value)
        {
            m_calories.Add(value);
            TotalCalories = m_calories.Sum();
        }

        public int TotalCalories { get; private set; } = 0;
    }
}
