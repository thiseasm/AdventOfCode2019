﻿using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Helpers
{
    public sealed class IntComputer
    {
        private static IntComputer _instance;
        private static readonly object _padlock = new object();

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

        public int FeedAdvancedOpCode(int[] opCode)
        {
            var currentPosition = 0;
            var command = DecodeCommand(opCode[currentPosition]);

            while (command[0] != 99)
            {
                var parameters = new int[4];
                var nextPosition = CalculateParameters(currentPosition, command, opCode, parameters);

                ExecuteCommand(opCode, command, parameters, currentPosition);

                currentPosition = nextPosition;
                command = DecodeCommand(opCode[currentPosition]);

            } 

            return opCode[0];
        }

        private static int CalculateParameters(int currentPosition, IReadOnlyList<int> command, int[] opCode, int[] parameters)
        {
            var nextPosition = currentPosition;
            var typeOfOperation = command[0];
            if (typeOfOperation == 1 || typeOfOperation == 2)
            {
                Array.ConstrainedCopy(opCode, currentPosition, parameters, 0, 4);
                nextPosition += 4;
            }
            else if(typeOfOperation == 3 || typeOfOperation == 4)
            {
                Array.ConstrainedCopy(opCode, currentPosition, parameters, 0, 2);
                nextPosition += 2;
            }
            else
            {
                if (typeOfOperation == 5 || typeOfOperation == 6)
                {
                    Array.ConstrainedCopy(opCode, currentPosition + 1, parameters, 0, 2);
                    nextPosition = ExecuteLogicOperators(command, parameters, opCode) ?? nextPosition + 3;
                }
                else
                {
                    Array.ConstrainedCopy(opCode, currentPosition + 1, parameters, 0, 3);
                    ExecuteLogicOperators(command, parameters, opCode);
                    nextPosition += 4;
                }
            }

            return nextPosition;
        }

        private static void ExecuteCommand(int[] opCode, IReadOnlyList<int> command, IReadOnlyList<int> parameters, int currentPosition)
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
                    var input = RequestUserInput();
                    var resultPosition = command[1] == 1 ? currentPosition += 1 : parameters[1];
                    opCode[resultPosition] = input;
                    break;
                }
                case 4:
                {
                    var resultPosition = command[1] == 1 ? currentPosition += 1 : parameters[1];
                    Console.Write($"Output of Command [4] is : {opCode[resultPosition]}" + Environment.NewLine);
                    break;
                }
            }
        }

        private static int? ExecuteLogicOperators(IReadOnlyList<int> command, IReadOnlyList<int> parameters, IList<int> opCode)
        {
            switch (command[0])
            {
                case 5:
                    var nonZeroValue = FindParameter(command[1], parameters[0], opCode);
                    if (nonZeroValue != 0)
                        return command[2] == 1 ? parameters[1] : opCode[parameters[1]];
                    else
                        return null;
                case 6 when parameters[0] == 0:
                    var zeroValue = FindParameter(command[1], parameters[0], opCode);
                    if (zeroValue == 0)
                        return command[2] == 1 ? parameters[1] : opCode[parameters[1]];
                    else
                        return null;
                case 7:
                    var operand1 = FindParameter(command[1], parameters[0], opCode);
                    var operand2 = FindParameter(command[2], parameters[1], opCode);
                    var resultPos = FindParameter(command[3], parameters[2], opCode);
                    opCode[resultPos] = operand1 < operand2 ? 1 : 0;
                    return null;
                case 8:
                    var operand3 = FindParameter(command[1], parameters[0], opCode);
                    var operand4 = FindParameter(command[2], parameters[1], opCode);
                    var resultPosit = FindParameter(command[3], parameters[2], opCode);
                    opCode[resultPosit] = operand3 == operand4 ? 1 : 0;
                    return null;
                default:
                    Console.WriteLine("Invalid command submitted to the IntComputer");
                    return null;
            }
        }

        //Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
        //Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
        //Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        //Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0


        private static int RequestUserInput()
        {
            var input = 0;
            var isInteger = false;
            while (!isInteger)
            {
                Console.Write("Please provide a valid input for the IntComputer:");
                var inputRaw = Console.ReadLine();
                isInteger = int.TryParse(inputRaw, out input);
            }
            return input;
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