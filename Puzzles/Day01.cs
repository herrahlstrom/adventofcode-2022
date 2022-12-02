using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2022.Puzzles;

public class Day01 : IDay
{
   private readonly List<Elf> m_elfs = new();

   public int Day => 1;

   public string Name => "Calorie Counting";

   public string? FirstPart()
   {
      var maxCalories = m_elfs.Max(x => x.TotalCalories);
      return $"{maxCalories}";
   }

   public void ReadInput()
   {
      Elf? currentElf = null;
      InputReader.ReadLines("input/01.txt", OnLine);

      void OnLine(string line)
      {
         if (line.Length == 0)
         {
            currentElf = null;
            return;
         }

         if (currentElf is null)
         {
            currentElf = new();
            m_elfs.Add(currentElf);
         }

         int calories = int.Parse(line);
         currentElf.AddCalories(calories);
      }
   }

   public string? SecondPart()
   {
      var sumCaloriesTop3Elfs = m_elfs
          .OrderByDescending(x => x.TotalCalories)
          .Take(3)
          .Sum(x => x.TotalCalories);
      return $"{sumCaloriesTop3Elfs}";
   }

   class Elf
   {
      private readonly List<int> m_calories = new();

      public int TotalCalories { get; private set; } = 0;

      public void AddCalories(int value)
      {
         m_calories.Add(value);
         TotalCalories = m_calories.Sum();
      }
   }
}
