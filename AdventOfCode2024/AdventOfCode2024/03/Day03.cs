using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024._03
{
    public class Day03 : IDay
    {
        bool isEnabled = true;

        public string GetFirstAnswer()
        {
            var lines = InputReader.ReadInput("03");

            return GetSumOfProducts(lines, GetSumOfProductsFromLine).ToString();
        }

        public string GetSecondAnswer()
        {
            var lines = InputReader.ReadInput("03");

            return GetSumOfProducts(lines, GetSumOfProductsFromLineWithConditions).ToString();
        }

        private int GetSumOfProducts(IEnumerable<string> lines, Func<string, int> getSumOfProductsInLine)
        {
            var sum = 0;
            foreach (var line in lines)
            {
                var sumOfMultiplicationsInLine = getSumOfProductsInLine(line);
                sum += sumOfMultiplicationsInLine;
            }
            return sum;
        }

        private int GetSumOfProductsFromLineWithConditions(string line)
        {
            var sum = 0;

            var pattern = @"(?:mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\))";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(line);

            foreach (Match match in matches) 
            { 
                if (match.Value.StartsWith('m'))
                {
                    if (isEnabled)
                    {
                        var x = match.Groups[1].Value;
                        var y = match.Groups[2].Value;

                        var product = int.Parse(x) * int.Parse(y);
                        sum += product;
                    }
                }
                else if (match.Value == "do()")
                {
                    isEnabled = true;
                }
                else
                {
                    isEnabled = false;
                }
            }
            return sum;
        }

        private int GetSumOfProductsFromLine(string line)
        {
            var sum = 0;

            var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(line);

            foreach (Match match in matches)
            {
                var x = match.Groups[1].Value;
                var y = match.Groups[2].Value;

                var product = int.Parse(x) * int.Parse(y);
                sum += product;
            }

            return sum;
        }
    }
}
