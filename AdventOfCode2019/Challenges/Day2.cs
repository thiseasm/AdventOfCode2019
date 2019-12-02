using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Challenges
{
    public class Day2 : Day
    {
        public override void Start()
        {
            ExecuteOpCode();
        }

        private void ExecuteOpCode()
        {
            var opCode = ReadCSV("Day2.txt");
            opCode[1] = 12;
            opCode[2] = 2;
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
            Console.WriteLine($"The output of the [intcomputer] at position [0] is: {opCode[0]}");
        }

        private int Add(int number1, int number2) { return number1 + number2; }
        private int Multiply(int number1, int number2) { return number1 * number2; }
        
    }
}
