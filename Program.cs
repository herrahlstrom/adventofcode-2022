using System.Diagnostics;
using AdventOfCode2022.Infrastructure;

IDay day;

try
{
    day = GetPuzzle(args);
}
catch (Exception ex)
{
    Console.Error.WriteLine("{0}: {1}", ex.GetType(), ex.Message);
    return;
}

Console.WriteLine("{0,2} | {1}", day.DayId, day.Name);
ReadInput(day);
PrintPuzzle("First puzzle", day.FirstPart);
PrintPuzzle("Second puzzle", day.SecondPart);

static IDay GetPuzzle(string[] args)
{
    IEnumerable<IDay> days = typeof(IDay).Assembly.GetTypes()
        .Where(t => t.IsClass)
        .Where(p => typeof(IDay).IsAssignableFrom(p))
        .Select(Activator.CreateInstance).OfType<IDay>();

    return args.Length >= 1 && int.TryParse(args[0], out int dayId)
        ? days.FirstOrDefault(x => x.DayId == dayId) ?? throw new ArgumentOutOfRangeException($"Day id {dayId} does not exists")
        : days.MaxBy(x => x.DayId) ?? throw new InvalidOperationException($"No day puzzle was found");
}

static void PrintPuzzle(string name, Func<string?> puzzleFunc)
{
    WriteHeader(name);

    try
    {
        Stopwatch sw = Stopwatch.StartNew();
        string? result = puzzleFunc.Invoke();
        if (result is not null)
        {
            Console.WriteLine("Result: {0}", result);
        }
        sw.Stop();
        Console.WriteLine("Time elapsed: {0:N0} ms.", sw.ElapsedMilliseconds);
    }
    catch (NotImplementedException)
    {
        Console.WriteLine("Not implemented");
    }
}

static void ReadInput(IDay day)
{
    WriteHeader("Read input");

    Stopwatch sw = Stopwatch.StartNew();
    day.ReadInput();
    sw.Stop();
    Console.WriteLine("Time elapsed: {0:N0} ms.", sw.ElapsedMilliseconds);
}

static void WriteHeader(string name)
{
    Console.WriteLine();
    Console.WriteLine("+-{0}-+", new string('-', name.Length));
    Console.WriteLine("| {0} |", name);
    Console.WriteLine("+-{0}-+", new string('-', name.Length));
}