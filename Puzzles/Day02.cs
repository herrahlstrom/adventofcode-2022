using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2022.Puzzles;

internal class Day02 : IDay
{
   private List<(Shape opponent, char unknown)> m_strategyGuide = new();
   private enum RoundResult { Loss = 0, Draw = 3, Win = 6 }
   private enum Shape { Rock = 1, Paper = 2, Scissors = 3 }

   public int Day => 2;

   public string Name => "Rock Paper Scissors";

   public string? FirstPart()
   {
      int points = 0;

      foreach (var round in m_strategyGuide)
      {
         var you = round.unknown switch
         {
            'X' => Shape.Rock,
            'Y' => Shape.Paper,
            'Z' => Shape.Scissors,
            _ => throw new UnreachableException()
         };

         var roundResult = (round.opponent, you) switch
         {
            (Shape.Rock, Shape.Rock) => RoundResult.Draw,
            (Shape.Rock, Shape.Paper) => RoundResult.Win,
            (Shape.Rock, Shape.Scissors) => RoundResult.Loss,
            (Shape.Paper, Shape.Rock) => RoundResult.Loss,
            (Shape.Paper, Shape.Paper) => RoundResult.Draw,
            (Shape.Paper, Shape.Scissors) => RoundResult.Win,
            (Shape.Scissors, Shape.Rock) => RoundResult.Win,
            (Shape.Scissors, Shape.Paper) => RoundResult.Loss,
            (Shape.Scissors, Shape.Scissors) => RoundResult.Draw,
            _ => throw new UnreachableException()
         };

         points += (int)roundResult + (int)you;
      }
      return $"{points}";
   }

   public void ReadInput()
   {
      InputReader.ReadLines("input/02.txt", line =>
      {
         var opponent = line[0] switch
         {
            'A' => Shape.Rock,
            'B' => Shape.Paper,
            'C' => Shape.Scissors,
            _ => throw new UnreachableException()
         };

         m_strategyGuide.Add(new(opponent, line[2]));
      });
   }

   public string? SecondPart()
   {
      int points = 0;

      foreach (var (opponent, unknown) in m_strategyGuide)
      {
         var expectedResult = unknown switch
         {
            'X' => RoundResult.Loss,
            'Y' => RoundResult.Draw,
            'Z' => RoundResult.Win,
            _ => throw new UnreachableException()
         };

         var you = (opponent, expectedResult) switch
         {
            (Shape.Rock, RoundResult.Loss) => Shape.Scissors,
            (Shape.Rock, RoundResult.Draw) => Shape.Rock,
            (Shape.Rock, RoundResult.Win) => Shape.Paper,
            (Shape.Paper, RoundResult.Loss) => Shape.Rock,
            (Shape.Paper, RoundResult.Draw) => Shape.Paper,
            (Shape.Paper, RoundResult.Win) => Shape.Scissors,
            (Shape.Scissors, RoundResult.Loss) => Shape.Paper,
            (Shape.Scissors, RoundResult.Draw) => Shape.Scissors,
            (Shape.Scissors, RoundResult.Win) => Shape.Rock,
            _ => throw new UnreachableException()
         };

         points += (int)expectedResult + (int)you;
      }
      return $"{points}";
   }
}
