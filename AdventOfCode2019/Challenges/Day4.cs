using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019.Challenges
{
    public class Day4 : Day
    {
        public override void Start()
        {
            BruteForce();
        }

        private void BruteForce()
        {
            var passwordRange = ReadSv("Day4.txt", '-');
            var passwordCount = 0;

            for (var index = passwordRange[0]; index <= passwordRange[1]; index++)
            {
                if (CompareWithPattern(index))
                    passwordCount++;
            }
            Console.WriteLine($"The number of password matching the required pattern is: {passwordCount}.");
        }

        private static bool CompareWithPattern(int index)
        {
            const string ascendingRegex = @"^0*1*2*3*4*5*6*7*8*9*$";
            const string consecutiveRegex = @"(.)\1";

            var password = index.ToString();
            return Regex.IsMatch(password, consecutiveRegex) && Regex.IsMatch(password, ascendingRegex);
        }
    }
}
