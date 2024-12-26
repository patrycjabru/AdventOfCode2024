using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._17
{
    public class Day17 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("17");
            var program = ParseInput(input);

            var outputs = RunProgram(program);

            return string.Join(',', outputs);
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private List<int> RunProgram(Program program)
        {
            var outputs = new List<int>();
            var i = 0;
            while(i < program.Instructions.Count)
            {
                var instruction = program.Instructions[i];
                switch (instruction)
                {
                    case 0:
                        {
                            var numerator = program.RegisterA;
                            var operandValue = GetValueOfComboOperand(program.Instructions[i + 1], program);
                            var denominator = (int)Math.Pow(2, operandValue);
                            long operationResult = (long)numerator / (long)denominator;
                            if (operationResult > int.MaxValue)
                                program.RegisterA = int.MaxValue;
                            else
                                program.RegisterA = (int)operationResult;

                            i += 2;
                            break;
                        }
                    case 1:
                        {
                            var operandValue = program.Instructions[i + 1];
                            program.RegisterB = program.RegisterB ^ operandValue;
                            
                            i += 2; 
                            break;
                        }
                    case 2:
                        {
                            var operandValue = GetValueOfComboOperand(program.Instructions[i + 1], program);
                            program.RegisterB = operandValue % 8;

                            i += 2;
                            break; 
                        }
                    case 3:
                        {
                            if (program.RegisterA == 0)
                            {
                                i += 2;
                                break;
                            }
                            var operandValue = program.Instructions[i + 1];
                            i = operandValue;
                            break;
                        }
                        
                    case 4:
                        {
                            program.RegisterB = program.RegisterB ^ program.RegisterC;

                            i += 2;
                            break;
                        }
                        
                    case 5:
                        {
                            var operandValue = GetValueOfComboOperand(program.Instructions[i + 1], program);
                            outputs.Add(operandValue % 8);

                            i += 2;   
                            break;
                        }
                        
                    case 6:
                        {
                            var numerator = program.RegisterA;
                            var operandValue = GetValueOfComboOperand(program.Instructions[i + 1], program);
                            var denominator = (int)Math.Pow(2, operandValue);
                            long operationResult = (long)numerator / (long)denominator;
                            if (operationResult > int.MaxValue)
                                program.RegisterB = int.MaxValue;
                            else
                                program.RegisterB = (int)operationResult;

                            i += 2;
                            break;
                        }
                    case 7:
                        {
                            var numerator = program.RegisterA;
                            var operandValue = GetValueOfComboOperand(program.Instructions[i + 1], program);
                            var denominator = (int)Math.Pow(2, operandValue);
                            long operationResult = (long)numerator / (long)denominator;
                            if (operationResult > int.MaxValue)
                                program.RegisterC = int.MaxValue;
                            else
                                program.RegisterC = (int)operationResult;

                            i += 2;
                            break;
                        }
                    default:
                        break;
                }
            }
            return outputs;
        }

        private int GetValueOfComboOperand(int operand, Program program)
        {
            switch (operand)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return program.RegisterA;
                case 5:
                    return program.RegisterB;
                case 6:
                    return program.RegisterC;
                default:
                    throw new NotImplementedException();
            }
        }


        private Program ParseInput(IEnumerable<string> input)
        {
            var i = 0;
            var program = new Program();
            foreach (var line in input)
            {
                if (i == 0)
                {
                    program.RegisterA = int.Parse(line.Split(':')[1].Trim());
                }
                if (i == 1)
                {
                    program.RegisterB = int.Parse(line.Split(":")[1].Trim());
                }
                if (i == 2)
                {
                    program.RegisterC = int.Parse(line.Split(":")[1].Trim());
                }
                if (i == 3)
                {

                }
                if (i == 4)
                {
                    var instructionsAsStrings = line.Split(":")[1].Trim().Split(',');
                    program.Instructions = new List<int>();
                    foreach (var instructions in instructionsAsStrings)
                    {
                        program.Instructions.Add(int.Parse(instructions));
                    }
                }
                i++;
            }
            return program;
        }

        
    }
}
