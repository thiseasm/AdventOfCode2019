using System;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Challenges
{
    public class Day5 : Day
    {
        private static IntComputer _computer = IntComputer.Instance;
        public override void Start()
        {
            var opCode = ReadCsv("Day5.txt");
            Console.WriteLine("Press [1] to run Diagnostics or [5] to extend the Thermal Radiators");
            RunTEST(opCode);
        }


        private static void RunTEST(int[] opCode)
        {
            _computer.FeedAdvancedOpCode(opCode);
        }
    }
}
