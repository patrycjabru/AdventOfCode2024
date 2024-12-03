using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._01
{
    public class Day01 : IDay
    {
        public string GetFirstAnswer()
        {
            var lines = InputReader.ReadInput("01");

            return CalculateSum(lines).ToString();
        }

        public string GetSecondAnswer()
        {
            var lines = InputReader.ReadInput("01");

            return CalculateSimilarityScore(lines).ToString();
        }

        private int CalculateSimilarityScore(IEnumerable<string> input)
        {
            var firstColumn = new SortedList<int, int>();
            var secondColumn = new SortedList<int, int>();

            var count = 0;

            foreach (var line in input)
            {
                var numbers = line.Split("   ");
                if (numbers.Length != 2)
                    throw new Exception("Invalid input reading");

                var firstNumber = int.Parse(numbers[0]);
                var secondNumber = int.Parse(numbers[1]);

                firstColumn.TryAdd(firstNumber, 0);
                if (secondColumn.ContainsKey(secondNumber))
                {
                    secondColumn[secondNumber]++;
                }
                else
                {
                    secondColumn.Add(secondNumber, 1);
                }
            }

            var similarityScore = 0;
            foreach (var element in firstColumn)
            {
                if (secondColumn.ContainsKey(element.Key))
                {
                    similarityScore += element.Key * secondColumn[element.Key];
                }
            }

            return similarityScore;
        }

        private int CalculateSum(IEnumerable<string> input)
        {
            var firstColumn = new List<int>();
            var secondColumn = new List<int>();

            var count = 0;

            foreach (var line in input)
            {
                var numbers = line.Split("   ");
                if (numbers.Length != 2)
                    throw new Exception("Invalid input reading");

                firstColumn.Add(int.Parse(numbers[0]));
                secondColumn.Add(int.Parse(numbers[1]));
                count++;
            }
            firstColumn.Sort();
            secondColumn.Sort();

            var sum = 0;
            for (var index = 0; index < count; index++)
            {
                sum += Math.Abs(secondColumn[index] - firstColumn[index]);
            }
            return sum;
        }
    }
}
