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
        public string GetFirstAnswer()
        {
            var lines = InputReader.ReadInput("03");

            return GetSumOfProducts(lines).ToString();
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private int GetSumOfProducts(IEnumerable<string> lines)
        {
            var sum = 0;
            foreach (var line in lines)
            {
                var sumOfMultiplicationsInLine = GetSumOfProductsFromLine(line);
                sum += sumOfMultiplicationsInLine;
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
