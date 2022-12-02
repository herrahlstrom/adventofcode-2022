using AdventOfCode2022;
using System.Diagnostics;

try
{
    IDay day = args.Length >= 1 && int.TryParse(args[0], out int numArg)
        ? GetPuzzles().Where(x => x.Day == numArg).First()
        : GetPuzzles().OrderBy(x => x.Day).Last();

    Console.WriteLine("{0,2} | {1}", day.Day, day.Name);
    ReadInput(day);
    PrintPuzzle("First puzzle", day.FirstPart);
    PrintPuzzle("Second puzzle", day.SecondPart);
}
catch (Exception ex)
{
    Console.Error.WriteLine("{0}: {1}", ex.GetType(), ex.Message);
}

static IEnumerable<IDay> GetPuzzles()
{
    return typeof(IDay).Assembly.GetTypes()
        .Where(t => t.IsClass)
        .Where(p => typeof(IDay).IsAssignableFrom(p))
        .Select(Activator.CreateInstance).OfType<IDay>();
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
