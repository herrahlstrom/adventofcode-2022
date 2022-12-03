
namespace AdventOfCode2022.Puzzles;

public class Day01 : IDay
{
    public int Day => 1;

    public string Name => "Calorie Counting";

    public string? FirstPart()
    {
        int maxCalories = 0;

        int calories = 0;
        foreach (var line in InputReader.ReadLines($"input/{Day:00}.txt"))
        {
            if (line == "")
            {
                maxCalories = calories > maxCalories ? calories : maxCalories;
                calories = 0;
                continue;
            }
            calories += int.Parse(line);
        }
        maxCalories = calories > maxCalories ? calories : maxCalories;

        return $"{maxCalories}";
    }

    public string? SecondPart()
    {
        List<int> allCalories = new();

        int calories = 0;
        foreach (var line in InputReader.ReadLines($"input/{Day:00}.txt"))
        {
            if (line == "")
            {
                allCalories.Add(calories);
                calories = 0;
                continue;
            }
            calories += int.Parse(line);
        }
        if (calories > 0)
        {
            allCalories.Add(calories);
        }

        var sumCaloriesTop3Elfs = allCalories
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        return $"{sumCaloriesTop3Elfs}";
    }
}
