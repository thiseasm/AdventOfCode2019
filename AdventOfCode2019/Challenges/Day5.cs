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
            // TEST needs input 1
            RunDiagnosticForTEST(opCode);
            // Thermal radiators require input 5
            RunDiagnosticForTEST(opCode);
        }


        private void RunDiagnosticForTEST(int[] opCode)
        {
            _computer.FeedAdvancedOpCode(opCode);
        }
    }
}
