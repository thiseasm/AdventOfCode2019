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
            var date = 5;
            return date switch
            {
                1 => (Day) new Day1(),
                2 => new Day2(),
                3 => new Day3(),
                4 => new Day4(),
                5 => new Day5(),
                6 => new Day6(),
                7 => new Day7(),
                _ => new Day1()
            };
        }
    }
}
