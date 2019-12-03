using System;

namespace AdventOfCode2019.Challenges
{
    public class Day2 : Day
    {
        public override void Start()
        {
            RestoreGravityAssist();
            CompleteGravityAssist();
        }

        private void RestoreGravityAssist()
        {
            var opCode = ReadCSV("Day2.txt");
            opCode[1] = 12;
            opCode[2] = 2;

            var result = FeedIntComputer(opCode);
            Console.WriteLine($"The output of the IntComputer for the [1202 error] at position [0] is: {result}");
        }

        private void CompleteGravityAssist()
        {
            var expectedOutput = 19690720;
            var result = 0;

            for (var noun = 0; noun <= 99; noun++)
            {
                for (var verb = 0; verb <= 99; verb++)
                {
                    var opCode = ReadCSV("Day2.txt");
                    opCode[1] = noun;
                    opCode[2] = verb;
                    result = FeedIntComputer(opCode);
                    if (result == expectedOutput)
                    {
                        Console.WriteLine($"The noun and verb that produce output: [{expectedOutput}] are [noun]:{noun} and [verb]:{verb}");
                        break;
                    }
                }

                if (result == expectedOutput) break;
            }
            
        }

        private int FeedIntComputer(int[] opCode)
        {
            var currentPosition = 0;
            var command = opCode[currentPosition];

            while (command != 99)
            {
                var firstPosition = opCode[currentPosition + 1];
                var secondPosition = opCode[currentPosition + 2];

                var firstValue = opCode[firstPosition];
                var secondValue = opCode[secondPosition];

                var result = command == 1 ? Add(firstValue, secondValue) : Multiply(firstValue, secondValue);
                var resultPosition = opCode[currentPosition + 3];
                opCode[resultPosition] = result;

                currentPosition += 4;
                command = opCode[currentPosition];
            }
            return opCode[0];
        }

        private int Add(int number1, int number2) { return number1 + number2; }
        private int Multiply(int number1, int number2) { return number1 * number2; }
        
    }
}
