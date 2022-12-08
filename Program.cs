using AdventOfCode2022;
using ConsoleTables;
using System.Diagnostics;

var resultTable = new ConsoleTable("PUZZLE", "FIRST", "SECOND", "ELAPSED")
{
    Options =
    {
        EnableCount = false
    }
};

foreach (IDay day in GetPuzzles(args.FirstOrDefault()))
{
    Stopwatch sw = Stopwatch.StartNew();
    object firstResult = day.FirstPart();
    object secondResult = day.SecondPart();
    sw.Stop();

    resultTable.AddRow(
        $"{day.Day,2} {day.Name}",
        $"{firstResult,10}",
        $"{secondResult,10}",
        $"{sw.ElapsedMilliseconds,4} ms");
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
