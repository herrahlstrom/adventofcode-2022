using AdventOfCode2022;
using ConsoleTables;
using System.Diagnostics;

var resultTable = new ConsoleTable("   PUZZLE", "FIRST".PadLeft(10), "SECOND".PadLeft(10), "ELAPSED".PadLeft(17));

foreach (IDay day in GetPuzzles(args.FirstOrDefault()))
{
    Stopwatch sw1 = Stopwatch.StartNew();
    object firstResult = day.FirstPart();
    sw1.Stop();

    Stopwatch sw2 = Stopwatch.StartNew();
    object secondResult = day.SecondPart();
    sw2.Stop();

    resultTable.AddRow(
        $"{day.Day:00} {day.Name}",
        $"{firstResult,10}",
        $"{secondResult,10}",
        $"{sw1.ElapsedMilliseconds,4} ms + {sw2.ElapsedMilliseconds,4} ms");
    }

resultTable.Write();

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
