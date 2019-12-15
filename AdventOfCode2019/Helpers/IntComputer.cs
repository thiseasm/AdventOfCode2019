using System;
using System.Collections.Generic;
using System.Net;

namespace AdventOfCode2019.Helpers
{
    public sealed class IntComputer
    {
        private static IntComputer _instance;
        private static readonly object _padlock = new object();
        private static int _inputIndex;
        private static int[] _inputs;

        private IntComputer()
        {
        }

        public static IntComputer Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new IntComputer();
                }
            }
        }

        public void SetInputs(int[] inputs)
        {
            _inputs = inputs;
            _inputIndex = 0;
        }

        public void SetInput(int input)
        {
            _inputs  = new[] {input};
            _inputIndex = 0;
        }

        //public int[] FeedIntComputerWithReturn(int[] opCode)
        //{

        //} 

        public int FeedIntComputerV2(int[] opCode)
        {
            var currentPosition = 0;
            var command = DecodeCommand(opCode[currentPosition]);
            var output = 0;

            while (command[0] != 99)
            {
                var parameters = new int[4];
                var nextPosition = CalculateParameters(currentPosition, command, opCode, parameters);

                output = ExecuteCommand(opCode, command, parameters, currentPosition);

                currentPosition = nextPosition;
                command = DecodeCommand(opCode[currentPosition]);

            } 

            return output;
        }

        private static int CalculateParameters(int currentPosition, IReadOnlyList<int> command, int[] opCode, int[] parameters)
        {
            var nextPosition = currentPosition;
            var typeOfOperation = command[0];
            switch (typeOfOperation)
            {
                case 1:
                case 2:
                    Array.ConstrainedCopy(opCode, currentPosition, parameters, 0, 4);
                    nextPosition += 4;
                    break;
                case 3:
                case 4:
                    Array.ConstrainedCopy(opCode, currentPosition, parameters, 0, 2);
                    nextPosition += 2;
                    break;
                case 5:
                case 6:
                    Array.ConstrainedCopy(opCode, currentPosition + 1, parameters, 0, 2);
                    nextPosition = ExecuteJumpOperators(command, parameters, opCode) ?? nextPosition + 3;
                    break;
                case 7:
                case 8:
                    Array.ConstrainedCopy(opCode, currentPosition + 1, parameters, 0, 3);
                    ExecuteLogicOperators(command, parameters, opCode);
                    nextPosition += 4;
                    break;
                default:
                    Console.WriteLine("Wrong command given");
                    break;
            }

            return nextPosition;
        }

        private static int ExecuteCommand(int[] opCode, IReadOnlyList<int> command, IReadOnlyList<int> parameters, int currentPosition)
        {
            switch (command[0])
            {
                case 1:
                {
                    var result = Add(command, opCode, parameters);
                    var resultPosition = command[3] == 1 ? currentPosition += 3 : parameters[3];
                    opCode[resultPosition] = result;
                    break;
                }
                case 2:
                {
                    var result = Multiply(command, opCode, parameters);
                    var resultPosition = command[3] == 1 ? currentPosition += 3 : parameters[3];
                    opCode[resultPosition] = result;
                    break;
                }
                case 3:
                {
                    var resultPosition = command[1] == 1 ? currentPosition += 1 : parameters[1];
                    opCode[resultPosition] = _inputs[_inputIndex];
                    if (_inputIndex < _inputs.Length - 1) _inputIndex++;
                    break;
                }
                case 4:
                {
                    var resultPosition = command[1] == 1 ? currentPosition += 1 : parameters[1];
                    return opCode[resultPosition];
                    //Console.Write($"Output of Command [4] is : {opCode[resultPosition]}" + Environment.NewLine);
                }
            }

            return 0;
        }

        private static void ExecuteLogicOperators(IReadOnlyList<int> command, IReadOnlyList<int> parameters, IList<int> opCode)
        {
            if (command[0] == 7)
            {
                var operand1 = FindParameter(command[1], parameters[0], opCode);
                var operand2 = FindParameter(command[2], parameters[1], opCode);
                opCode[parameters[2]] = operand1 < operand2 ? 1 : 0;
            }
            else if (command[0] == 8)
            {
                var operand1 = FindParameter(command[1], parameters[0], opCode);
                var operand2 = FindParameter(command[2], parameters[1], opCode);
                opCode[parameters[2]] = operand1 == operand2 ? 1 : 0;
            }
        }

        private static int? ExecuteJumpOperators(IReadOnlyList<int> command, IReadOnlyList<int> parameters,IList<int> opCode)
        {
            if (command[0] == 5)
            {
                var nonZeroValue = FindParameter(command[1], parameters[0], opCode);
                if (nonZeroValue != 0)
                    return FindParameter(command[2], parameters[1], opCode);
            }
            else if (command[0] == 6)
            {
                var zeroValue = FindParameter(command[1], parameters[0], opCode);
                if (zeroValue == 0)
                    return FindParameter(command[2], parameters[1], opCode);
            }

            return null;
        }

        private static int Multiply(IReadOnlyList<int> command, IReadOnlyList<int> opCode, IReadOnlyList<int> parameters)
        {
            var operand1 = FindParameter(command[1], parameters[1], opCode);
            var operand2 = FindParameter(command[2], parameters[2], opCode);
            var result = Multiply(operand1, operand2);
            return result;
        }

        private static int Add(IReadOnlyList<int> command, IReadOnlyList<int> opCode, IReadOnlyList<int> parameters)
        {
            var operand1 = FindParameter(command[1], parameters[1], opCode);
            var operand2 = FindParameter(command[2], parameters[2], opCode);
            var result = Add(operand1, operand2);
            return result;
        }

        private static int FindParameter(int mode,int parameter, IReadOnlyList<int> opCode)
        {
            return mode == 1 ? parameter : opCode[parameter];
        }

        private static int FindParameter(int mode,int parameter, IList<int> opCode)
        {
            return mode == 1 ? parameter : opCode[parameter];
        }

        private static List<int> DecodeCommand(int commandRaw)
        {
            var parameters = new List<int>();
            var digits = new List<int>();

            var firstPart = (commandRaw % 10).ToString();
            commandRaw /= 10;
            var secondPart = (commandRaw % 10).ToString();
            digits.Add(int.Parse(secondPart + firstPart));
            commandRaw /= 10;

            while(commandRaw > 0)
            {
                digits.Add(commandRaw % 10);
                commandRaw /= 10;
            }

            while (digits.Count < 4)
            {
                digits.Add(0);
            }
            
            parameters.AddRange(digits);
            return parameters;
        }

        public int FeedIntComputer(IList<int> opCode)
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

        private static int Add(int number1, int number2) { return number1 + number2; }
        private static int Multiply(int number1, int number2) { return number1 * number2; }
    }
}
