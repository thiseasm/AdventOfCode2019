using System;
using System.Collections.Generic;
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
            var validCount = 0;

            for (var index = passwordRange[0]; index <= passwordRange[1]; index++)
            {
                if (!CompareWithPattern(index)) continue;

                passwordCount++;
                if (!CompareWithStricterPattern(index))
                    validCount++;

            }
            Console.WriteLine($"The number of passwords matching the required pattern is: {passwordCount}.");
            Console.WriteLine($"The number of passwords containing only two consecutive values is: {validCount}.");
        }

        private static bool CompareWithPattern(int index)
        {
            const string ascendingRegex = @"^0*1*2*3*4*5*6*7*8*9*$";
            const string consecutiveRegex = @"(.)\1";

            var password = index.ToString();
            return Regex.IsMatch(password, consecutiveRegex) && Regex.IsMatch(password, ascendingRegex);
        }

        private static bool CompareWithStricterPattern(int value)
        {
            const string threeOrMoreRegex = @"(.)\1\1{1,}";
            var password = value.ToString();
            var matches = Regex.IsMatch(password, threeOrMoreRegex);
            if (!matches) return false;

            if (password.Any(digit => password.Count(x => x == digit) == 2))
            {
                matches = false;
            }

            return matches;
        }
    }
}
