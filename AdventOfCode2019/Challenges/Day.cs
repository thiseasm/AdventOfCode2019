using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019.Challenges
{
    public abstract class Day
    {
        public abstract void Start();

        protected int[] ReadFile(string inputPath)
        {
            var inputsRaw = File.ReadAllLines(inputPath);
            var result = new List<int>();

            foreach (var input in inputsRaw)
            {
                result.Add(int.Parse(input.Trim()));
            }

            return result.ToArray();
        }
    }
}
