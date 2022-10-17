using AdventOfCode2022.Infrastructure;

namespace AdventOfCode2022;

public class Day00 : IDay
{
    public int DayId => 00;

    public string Name => "Dummy puzzle";

    public string? FirstPart()
    {
        Task.Delay(Random.Shared.Next(100, 2000)).GetAwaiter().GetResult();
        return Random.Shared.Next(10000, 100000).ToString("0");
    }

    public void ReadInput()
    {
        Task.Delay(Random.Shared.Next(100, 2000)).GetAwaiter().GetResult();
    }

    public string? SecondPart()
    {
        Task.Delay(Random.Shared.Next(100, 2000)).GetAwaiter().GetResult();
        return Random.Shared.Next(10000, 100000).ToString("0");
    }
}