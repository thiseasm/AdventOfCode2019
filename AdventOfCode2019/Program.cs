using System;
using AdventOfCode2019.Challenges;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveChristmas();
        }

        private static void SaveChristmas()
        {
            var day = CalculateDay();
            day.Start();
            Console.ReadKey();
        }
        private static Day CalculateDay()
        {
            var Date = 3;
            return Date switch
            {
                1 => new Day1(),
                2 => new Day2(),
                3 => new Day3(),
                _ => new Day1(),
            };
        }
    }
}
