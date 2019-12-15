using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Challenges
{
    public class Day7 : Day
    {
        private static readonly IntComputer _computer = IntComputer.Instance;
        public override void Start()
        {
            FindOptimalCombination();
        }

        private void FindOptimalCombination()
        {
            var phaseSettings = new[] {0, 1, 2, 3, 4};
            var phaseCombinations = GetPermutations(phaseSettings, phaseSettings.Length).ToArray();

            var maximumOutput = 0;

            foreach (var combination in phaseCombinations)
            {
                var outputSignal = 0;
                foreach (var phase in combination.ToArray())
                {
                    var opCode = ReadCsv("Day7.txt");
                    _computer.SetInputs(new [] {phase, outputSignal});
                    outputSignal = _computer.FeedIntComputerV2(opCode);
                }

                if (outputSignal > maximumOutput) maximumOutput = outputSignal;

            }

            Console.WriteLine($"The highest signal that can be sent to the thrusters is: {maximumOutput}.");

        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> array, int length)
        {
            if (length == 1) return array.Select(value => new T[] { value });
            return GetPermutations(array, length - 1).SelectMany(enumerable => array.Where(value => !enumerable.Contains(value)),(left, right) => left.Concat(new T[] { right }));
        }
    }
}
