using AdventOfCode2022;
using System.Diagnostics;

try
{
    Console.WriteLine("                               +----------------+----------------+------------+");
    Console.WriteLine("                               |          FIRST |         SECOND |    ELAPSED |");
    Console.WriteLine("+------------------------------+----------------+----------------+------------+");

    long totalElapsedMilliseconds = 0;
    foreach (IDay day in GetPuzzles(args.FirstOrDefault()))
    {
        string? firstResult;
        Stopwatch sw1 = Stopwatch.StartNew();
        try
        {
            firstResult = day.FirstPart();
        }
        catch (NotImplementedException) { firstResult = null; }
        finally
        {
            sw1.Stop();
        }

        string? secondResult;
        Stopwatch sw2 = Stopwatch.StartNew();
        try
        {
            secondResult = day.SecondPart();
        }
        catch (NotImplementedException) { secondResult = null; }
        finally
        {
            sw2.Stop();
        }

        var dayElapsedMilliseconds = sw1.ElapsedMilliseconds + sw2.ElapsedMilliseconds;
        var dayElapsedTicks = sw1.ElapsedTicks + sw2.ElapsedTicks;

        totalElapsedMilliseconds += dayElapsedMilliseconds;

        Console.WriteLine("| {0:00}                           | {1,14} | {2,14} |            |", 
            day.Day, 
            firstResult, 
            secondResult);
       
        Console.WriteLine("| {0,-28} | {1,14:P0} | {2,14:P0} | {3,7} ms |",
            day.Name, 
            (float)sw1.ElapsedTicks / dayElapsedTicks,
            (float)sw2.ElapsedTicks / dayElapsedTicks, 
            dayElapsedMilliseconds);
        Console.WriteLine("+------------------------------+----------------+----------------+------------+");
    }

    Console.WriteLine("                                                                 | {0,7} ms |", totalElapsedMilliseconds);
    Console.WriteLine("                                                                 +------------+");

    Console.WriteLine();

}
catch (Exception ex)
{
    Console.Error.WriteLine("{0}: {1}", ex.GetType(), ex.Message);
}

static IEnumerable<IDay> GetPuzzles(string? arg)
{
    IEnumerable<IDay> puzzles = typeof(IDay).Assembly.GetTypes()
        .Where(t => t.IsClass)
        .Where(p => typeof(IDay).IsAssignableFrom(p))
        .Select(Activator.CreateInstance).OfType<IDay>();

    if (int.TryParse(arg, out int day))
    {
        return puzzles.Where(x => x.Day == day);
    }
    else if (arg == "all" || arg == "*")
    {
        return puzzles.OrderBy(x => x.Day);
    }
    else
    {
        return puzzles.OrderByDescending(x => x.Day).Take(1);
    }
}
