namespace AdventOfCode2022;

internal interface IDay
{
    int Day { get; }
    string Name { get; }
    string? FirstPart();
    string? SecondPart();
}