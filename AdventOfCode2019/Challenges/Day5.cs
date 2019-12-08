using System;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Challenges
{
    public class Day5 : Day
    {
        private static IntComputer _computer = IntComputer.Instance;
        public override void Start()
        {
            RunDiagnosticForTEST();
        }

        private void RunDiagnosticForTEST()
        {
            var opCode = ReadCsv("Day5.txt");
            _computer.FeedAdvancedOpCode(opCode);
        }
    }
}
