using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019.Challenges
{
    public abstract class Day
    {
        public abstract void Start();

        protected int[] ReadFile(string file)
        {
            var inputPath = Path.Combine(Properties.Resources.InputsFolder, file);
            var inputsRaw = File.ReadAllLines(inputPath);
            var result = new List<int>();

            foreach (var input in inputsRaw)
            {
                result.Add(int.Parse(input.Trim()));
            }

            return result.ToArray();
        }

        protected int[] ReadCSV(string file)
        {
            var inputPath = Path.Combine(Properties.Resources.InputsFolder, file);
            var inputRaw = File.ReadAllText(inputPath).Split(",");
            var result = new List<int>();

            foreach (var input in inputRaw)
            {
                result.Add(int.Parse(input.Trim()));
            }

            return result.ToArray();
        }
    }
}
