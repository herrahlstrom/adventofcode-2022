namespace AdventOfCode2022.Puzzles;

internal class Day06 : IDay
{
    public int Day => 6;

    public string Name => "Tuning Trouble";

    public object FirstPart()
    {
        return FindStartOfPacketMarker(4);
    }

    public object SecondPart()
    {
        return FindStartOfPacketMarker(14);
    }

    private object FindStartOfPacketMarker(int size)
    {
        var map = new HashSet<char>(size);

        var input = File.ReadAllText($"input/{Day:00}.txt");
        for (int i = 0; i < input.Length; i++)
        {
            map.Clear();
            for (int j = 0; j < size; j++)
            {
                if (!map.Add(input[i + j]))
                {
                    break;
                }
            }

            if (map.Count == size)
            {
                return i + size;
            }
        }
        throw new NotSupportedException();
    }
}
