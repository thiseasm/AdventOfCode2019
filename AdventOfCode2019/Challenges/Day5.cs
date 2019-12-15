using System;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Challenges
{
    public class Day5 : Day
    {
        private static readonly IntComputer _computer = IntComputer.Instance;
        public override void Start()
        {
            RunTEST();
            ExtendThermalRadiators();
        }

        private void ExtendThermalRadiators()
        {
            var opCode = ReadCsv("Day5.txt");
            _computer.SetInput(5);
            var result = _computer.FeedIntComputerV2(opCode);
            Console.WriteLine($"Extending the Thermal Radiators returned: {result}.");
        }


        private void RunTEST()
        {
            var opCode = ReadCsv("Day5.txt");
            _computer.SetInput(1);
            var result = _computer.FeedIntComputerV2(opCode);
            Console.WriteLine($"Running TEST diagnostics returned: {result}.");
        }
    }
}
