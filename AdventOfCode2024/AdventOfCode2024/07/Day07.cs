﻿using System;
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
            throw new NotImplementedException();
        }

        private long GetCalibrationNumber(IEnumerable<string> input)
        {
            long calibrationNumber = 0;
            foreach (var line in input)
            {
                var numbers = line.Split(' ');
                var result = long.Parse(numbers[0].Split(':')[0]);
                var numbersForIngridients = numbers[1..numbers.Length];
                var ingridients = numbersForIngridients.Select(x => int.Parse(x));

                var isResultTrue = CheckEquation(result, ingridients.ToArray());

                if (isResultTrue)
                {
                    calibrationNumber += result;
                }
            }

            return calibrationNumber;
        }

        private bool CheckEquation(long result, int[] ingridients)
        {
            var combinations = GetOperatorsCombinations(ingridients.Count() - 1);

            
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
                    ingridientsIndex++;
                }
                if (realResult == result)
                {
                    return true;
                }
            }

            //Console.WriteLine("==========");
            //Console.WriteLine(result);
            //foreach (var i in ingridients)
            //{
            //    Console.Write(" " + i.ToString());
            //}
            //Console.WriteLine();

            return false;

        }

        private List<string> GetOperatorsCombinations(int length)
        {
            if (operatorsCombinations.ContainsKey(length))
            {
                return operatorsCombinations[length];
            }

            var combinations = new List<string>();
            GenerateCombinations(length, "", combinations);

            operatorsCombinations.Add(length, combinations);

            return combinations;
        }

        static void GenerateCombinations(int length, string currentCombination, List<string> combinations)
        {
            if (currentCombination.Length == length)
            {
                combinations.Add(currentCombination);
                return;
            }

            GenerateCombinations(length, currentCombination + "+", combinations);
            GenerateCombinations(length, currentCombination + "*", combinations);
        }
    }
}