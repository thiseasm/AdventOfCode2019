using System;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Challenges
{
    public class Day2 : Day
    {
        private static readonly IntComputer _computer = IntComputer.Instance;
        public override void Start()
        {
            RestoreGravityAssist();
            CompleteGravityAssist();
        }

        private void RestoreGravityAssist()
        {
            var opCode = ReadCsv("Day2.txt");
            opCode[1] = 12;
            opCode[2] = 2;

            var result = _computer.FeedIntComputerV2(opCode);
            Console.WriteLine($"The output of the IntComputer for the [1202 error] at position [0] is: {result}");
        }

        private void CompleteGravityAssist()
        {
            const int expectedOutput = 19690720;
            var result = 0;

            for (var noun = 0; noun <= 99; noun++)
            {
                for (var verb = 0; verb <= 99; verb++)
                {
                    var opCode = ReadCsv("Day2.txt");
                    opCode[1] = noun;
                    opCode[2] = verb;
                    result = _computer.FeedIntComputerV2(opCode);
                    if (result != expectedOutput) continue;

                    Console.WriteLine($"The noun and verb that produce output: [{expectedOutput}] are [noun]:{noun} and [verb]:{verb}");
                    break;
                }

                if (result == expectedOutput) break;
            }
            
        }

        
        
    }
}
