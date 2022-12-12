using System;
using System.Linq;

namespace AdventOfCode2022.Puzzles
{
    internal class Day12 : IDay
    {
        public int Day => 12;

        public string Name => "Hill Climbing Algorithm";

        public object FirstPart()
        {
            char[,] map = ReadMap(out Point start, out Point target);
            return GetPath(start, target, map).Length;
        }

        public object SecondPart()
        {
         /* A better solution would be to start from end position and calculate all distances,
          * then just take the lowest number from any 'a'. But this solution is fast enought,
          * and I don'w feel to rewrite. for now at least...
          */

            char[,] map = ReadMap(out Point start, out Point target);
            var bestLength = GetPath(start, target, map).Length;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 'a')
                    {
                        var length = GetPath(new Point(x, y), target, map, bestLength).Length;
                        if (length > 0 && length < bestLength)
                        {
                            bestLength = length;
                        }
                    }
                }
            }

            return bestLength;
        }

        private static bool CanMove(char[,] map, Point point, Point target)
        {
            var inMap = target.X >= 0 &&
               target.X < map.GetLength(0) &&
               target.Y >= 0 &&
               target.Y < map.GetLength(1);
            if (!inMap) { return false; }

            var a = map[point.X, point.Y];
            if (a == 'S') { a = 'a'; }
            var b = map[target.X, target.Y];
            if (b == 'E') { b = 'z'; }

            return a >= b - 1;
        }

        private static Point[] GetPath(Point start, Point target, char[,] map, int maxStep = int.MaxValue)
        {
            Point[,][] trail = new Point[map.GetLength(0), map.GetLength(1)][];
            trail[start.X, start.Y] = Array.Empty<Point>();
            var queue = new Queue<Point>();
            queue.Enqueue(start);

            var directions = new[] { TryGoRight, TryGoDown, TryGoLeft, TryGoUp };

            while (queue.TryDequeue(out Point? p) && p is { } current)
            {
                if (current == target)
                {
                    return trail[target.X, target.Y];
                }

                foreach (var dir in directions)
                {
                    if (dir.Invoke(map, current, out Point next) && (trail[next.X, next.Y] is null))
                    {
                        Point[] nextTrail = trail[current.X, current.Y].Append(next).ToArray();
                        if (nextTrail.Length >= maxStep)
                        {
                            continue;
                        }
                        trail[next.X, next.Y] = nextTrail;
                        queue.Enqueue(next);
                    }
                }
            }

            return Array.Empty<Point>();
        }
        private static bool TryGoDown(char[,] map, Point point, out Point below)
        {
            below = point with { Y = point.Y + 1 };
            return CanMove(map, point, below);
        }
        private static bool TryGoLeft(char[,] map, Point point, out Point left)
        {
            left = point with { X = point.X - 1 };
            return CanMove(map, point, left);
        }
        private static bool TryGoRight(char[,] map, Point point, out Point right)
        {
            right = point with { X = point.X + 1 };
            return CanMove(map, point, right);
        }
        private static bool TryGoUp(char[,] map, Point point, out Point above)
        {
            above = point with { Y = point.Y - 1 };
            return CanMove(map, point, above);
        }
        private char[,] ReadMap(out Point start, out Point target)
        {
            start = Point.Empty;
            target = Point.Empty;

            var lines = File.ReadAllLines($"input/{Day:00}.txt");
            var map = new char[lines[0].Length, lines.Length];

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    var c = lines[y][x];
                    if (c == 'S')
                    {
                        start = new Point(x, y);
                    }
                    else if (c == 'E')
                    {
                        target = new Point(x, y);
                    }
                    map[x, y] = lines[y][x];
                }
            }

            return map;
        }

        record Point(int X, int Y)
        {
            public static Point Empty { get; } = new Point(0, 0);
        }
    }
}
