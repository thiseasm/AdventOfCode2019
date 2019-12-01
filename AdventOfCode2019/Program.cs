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
            var Date = DateTime.Now.Day;
            switch (Date)
            {
                case 1:
                    return new Day1();
                default:
                    return new Day1();
            }
        }
    }
}
