namespace AdventOfCode2022.Infrastructure
{
    internal interface IDay
    {
        int DayId { get; }
        string Name { get; }
        string? FirstPart();
        void ReadInput();
        string? SecondPart();
    }
}