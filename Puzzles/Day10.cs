namespace AdventOfCode2022.Puzzles;

internal class Day10 : IDay
{
    public int Day => 10;

    public string Name => "Cathode-Ray Tube";

    public object FirstPart()
    {
        int cycle = 0;
        int x = 1;
        int nextCycleCheck = 20;

        int signalStrengthsSum = 0;

        foreach (var line in File.ReadLines($"input/{Day:00}.txt"))
        {
            if (line == "noop")
            {
                IncCycles();
            }
            else
            {
                IncCycles();
                IncCycles();

                x += int.Parse(line[5..]);
            }
        }

        void IncCycles()
        {
            cycle++;
            if (cycle == nextCycleCheck)
            {
                signalStrengthsSum += cycle * x;
                nextCycleCheck += 40;
            }
        }

        return signalStrengthsSum;
    }

    public object SecondPart()
    {
        int cycle = 0;
        int x = 1;

        Console.WriteLine("");
        Console.WriteLine("- Output from {0} {1} -", Day, Name);

        foreach (var line in File.ReadLines($"input/{Day:00}.txt"))
        {
            if (line == "noop")
            {
                IncCycles();
            }
            else
            {
                IncCycles();
                IncCycles();

                x += int.Parse(line[5..]);
            }
        }
        
        Console.WriteLine("");

        void IncCycles()
        {
            cycle++;
            Console.Write(cycle == x || cycle == x + 1 || cycle == x + 2 ? "#" : ".");

            if (cycle == 40)
            {
                Console.WriteLine();
                cycle = 0;
            }
        }

        // Catched from console output
        return "FCJAPJRE";
    }
}
