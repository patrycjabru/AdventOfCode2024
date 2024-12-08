using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._07
{
    public class Day07 : IDay
    {
        private Dictionary<int, List<string>> operatorsCombinations = new();

        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("07");

            return GetCalibrationNumber(input).ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("07");

            return GetCalibrationNumber(input, true).ToString();
        }

        private long GetCalibrationNumber(IEnumerable<string> input, bool additionalOperator = false)
        {
            long calibrationNumber = 0;
            foreach (var line in input)
            {
                var numbers = line.Split(' ');
                var result = long.Parse(numbers[0].Split(':')[0]);
                var numbersForIngridients = numbers[1..numbers.Length];
                var ingridients = numbersForIngridients.Select(x => int.Parse(x));

                var isResultTrue = CheckEquation(result, ingridients.ToArray(), additionalOperator);

                if (isResultTrue)
                {
                    calibrationNumber += result;
                }
            }

            return calibrationNumber;
        }

        private bool CheckEquation(long result, int[] ingridients, bool additionalOperator)
        {
            var combinations = GetOperatorsCombinations(ingridients.Count() - 1, additionalOperator);

            
            foreach (var combination in combinations)
            {
                var ingridientsIndex = 0;
                long realResult = ingridients[0];
                foreach (var c in combination) 
                {
                    if (c == '+')
                    {
                        realResult += ingridients[ingridientsIndex + 1];
                    }
                    else if (c == '*')
                    {
                        realResult *= ingridients[ingridientsIndex + 1];
                    }
                    else if (additionalOperator && c == '|')
                    {
                        realResult = long.Parse(realResult.ToString() + ingridients[ingridientsIndex + 1]);
                    }
                    ingridientsIndex++;
                }
                if (realResult == result)
                {
                    return true;
                }
            }

            return false;

        }

        private List<string> GetOperatorsCombinations(int length, bool additionalOperator)
        {
            if (operatorsCombinations.ContainsKey(length))
            {
                return operatorsCombinations[length];
            }

            var combinations = new List<string>();
            GenerateCombinations(length, "", combinations, additionalOperator);

            operatorsCombinations.Add(length, combinations);

            return combinations;
        }

        static void GenerateCombinations(int length, string currentCombination, List<string> combinations, bool additionalOperator)
        {
            if (currentCombination.Length == length)
            {
                combinations.Add(currentCombination);
                return;
            }

            GenerateCombinations(length, currentCombination + "+", combinations, additionalOperator);
            GenerateCombinations(length, currentCombination + "*", combinations, additionalOperator);
            if (additionalOperator)
            {
                GenerateCombinations(length, currentCombination + "|", combinations, additionalOperator);
            }
        }
    }
}
