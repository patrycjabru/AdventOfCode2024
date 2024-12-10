using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._10
{
    public class Day10 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("10");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            int[,] convertedInput;
            var startingPoints = GetStartingPoints(inputAsArray, out convertedInput);

            var topCounter = 0;
            foreach (var point in startingPoints)
            {
                topCounter += CountWaysToTop(point, convertedInput);
            }

            return topCounter.ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("10");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            int[,] convertedInput;
            var startingPoints = GetStartingPoints(inputAsArray, out convertedInput);

            var topCounter = 0;
            foreach (var point in startingPoints)
            {
                topCounter += CountUniqueWaysToTop(point, convertedInput);
            }

            return topCounter.ToString();
        }

        public List<Point> GetStartingPoints(char[,] input, out int[,] convertedInput)
        {
            convertedInput = new int[input.GetLength(0), input.GetLength(1)];
            var startingPoints = new List<Point>();
            for (var i = 0; i < input.GetLength(0); i++)
            {
                for (var j = 0; j < input.GetLength(1); j++)
                {
                    convertedInput[i,j] = InputReader.CharToDigit(input[i,j]);
                    if (input[i, j] == '0')
                    {
                        startingPoints.Add(new Point(i, j));
                    }
                }
            }
            return startingPoints;
        }

        public int CountWaysToTop(Point startingPoint, int[,] input)
        {
            ICollection<Point> peaks = new HashSet<Point>();
            CheckWay(startingPoint.row, startingPoint.column - 1, input, 0, peaks);
            CheckWay(startingPoint.row, startingPoint.column + 1, input, 0, peaks);
            CheckWay(startingPoint.row - 1, startingPoint.column, input, 0, peaks);
            CheckWay(startingPoint.row + 1, startingPoint.column, input, 0, peaks);

            return peaks.Count;
        }

        public int CountUniqueWaysToTop(Point startingPoint, int[,] input)
        {
            ICollection<Point> peaks = new List<Point>();
            CheckWay(startingPoint.row, startingPoint.column - 1, input, 0, peaks);
            CheckWay(startingPoint.row, startingPoint.column + 1, input, 0, peaks);
            CheckWay(startingPoint.row - 1, startingPoint.column, input, 0, peaks);
            CheckWay(startingPoint.row + 1, startingPoint.column, input, 0, peaks);

            return peaks.Count;
        }

        public void CheckWay(int row, int column, int[,] input, int previousValue, ICollection<Point> peaks)
        {
            if (row < 0 || column < 0 || row >= input.GetLength(0) || column >= input.GetLength(1))
                return;

            var pointValue = input[row, column];

            if (pointValue == previousValue + 1)
            {
                if (pointValue == 9)
                {
                    var peak = new Point(row, column);
                    peaks.Add(peak);
                    return;
                }
                CheckWay(row, column - 1, input, pointValue, peaks);
                CheckWay(row, column + 1, input, pointValue, peaks);
                CheckWay(row - 1, column, input, pointValue, peaks);
                CheckWay(row + 1, column, input, pointValue, peaks);
            }
        }
    }
}
