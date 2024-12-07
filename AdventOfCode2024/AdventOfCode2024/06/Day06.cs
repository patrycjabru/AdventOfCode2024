using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._06
{
    public class Day06 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("06").ToArray();

            var inputAsArray = ConvertInputToArray(input);

            SimulateGuardRoute(inputAsArray);

            var distinctPositionsCount = CountDistinctPositions(inputAsArray);

            return distinctPositionsCount.ToString();
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }
        private object CountDistinctPositions(char[,] inputAsArray)
        {
            var count = 0;
            foreach (var element in inputAsArray)
            {
                if (element == 'X')
                {
                    count++;
                }
            }
            return count;
        }
        private void SimulateGuardRoute(char[,] inputAsArray)
        {
            int direction = 0; // 0 - up, 1 - right, 2 - down, 3 - left

            var (guardPositionColumn, guardPositionRow) = FindStartingPosition(inputAsArray);

            while (true)
            {
                if (guardPositionColumn < 0 || guardPositionRow < 0 || guardPositionColumn >= inputAsArray.GetLength(0) || guardPositionRow >= inputAsArray.GetLength(1))
                {
                    break;
                }
                switch (direction)
                {
                    case 0:
                        if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
                        {
                            direction = 1;
                            guardPositionRow++;
                            guardPositionColumn++;
                            break;
                        }
                        else
                        {
                            inputAsArray[guardPositionRow, guardPositionColumn] = 'X';
                            guardPositionRow--;
                            break;
                        }
                    case 1:
                        if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
                        {
                            direction = 2;
                            guardPositionColumn--;
                            guardPositionRow++;
                            break;
                        }
                        else
                        {
                            inputAsArray[guardPositionRow, guardPositionColumn] = 'X';
                            guardPositionColumn++;
                            break;
                        }
                    case 2:
                        if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
                        {
                            direction = 3;
                            guardPositionRow--;
                            guardPositionColumn--;
                            break;
                        }
                        else
                        {
                            inputAsArray[guardPositionRow, guardPositionColumn] = 'X';
                            guardPositionRow++;
                            break;
                        }
                    case 3:
                        if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
                        {
                            direction = 0;
                            guardPositionColumn++;
                            guardPositionRow--;
                            break;
                        }
                        else
                        {
                            inputAsArray[guardPositionRow, guardPositionColumn] = 'X';
                            guardPositionColumn--;
                            break;
                        }
                    default:
                        {
                            throw new Exception();
                        }
                }
            }
        }

        private (int i, int j) FindStartingPosition(char[,] inputAsArray)
        {
            for (int i = 0; i < inputAsArray.GetLength(0); i++)
            {
                for (int j = 0; j < inputAsArray.GetLength(1); j++)
                {
                    if (inputAsArray[i,j] == '^')
                    {
                        return (j, i);
                    }
                }
            }

            throw new Exception("Starting point not found");
        }

        private char[,] ConvertInputToArray(IEnumerable<string> input)
        {
            char[,] result = input
                .Select((str, rowIndex) => str.Select((c, colIndex) => new { rowIndex, colIndex, c }))
                .SelectMany(x => x)
                .Aggregate(
                    new char[input.Count(), input.Max(s => s.Length)],
                (arr, x) => {
                    arr[x.rowIndex, x.colIndex] = x.c; return arr;
                });

            return result;
        }
    }
}
