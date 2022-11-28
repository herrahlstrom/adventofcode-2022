namespace AdventOfCode2022.Infrastructure
{
    internal interface IDay
    {
        int Day { get; }
        string Name { get; }
        string? FirstPart();
        void ReadInput();
        string? SecondPart();
    }
}