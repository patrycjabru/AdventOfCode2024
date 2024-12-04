using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._04
{
    public class Day04 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("04").ToArray();

            var inputAsArray = ConvertInputToArray(input);

            return CountXMAS(inputAsArray).ToString();
        }
        
        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
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
        private int CountXMAS(char[,] input)
        {
            var counter = 0;
            for (var i = 0; i < input.GetLength(0); i++)
            {
                for ( var j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i,j] == 'X')
                    {
                        var countInNeighbourhood = CheckNeighbourhood(i, j, input);
                        counter += countInNeighbourhood;
                    }
                }
            }
            return counter;
        }

        private int CheckNeighbourhood(int i, int j, char[,] input)
        {
            var counter = 0;
            //down
            if (j <= input.GetLength(1) - 4 &&
                input[i,j + 1] == 'M' &&
                input[i,j + 2] == 'A' &&
                input[i,j + 3] == 'S')
            {
                counter++;
            }
            
            //up
            if (j >= 3 &&
                input[i, j - 1] == 'M' &&
                input[i, j - 2] == 'A' &&
                input[i, j - 3] == 'S')
            {
                counter++;
            }

            //right
            if (i <= input.GetLength(0) - 4 &&
                input[i + 1, j] == 'M' &&
                input[i + 2, j] == 'A' &&
                input[i + 3, j] == 'S')
            {
                counter++;
            }

            //left
            if (i >= 3 &&
               input[i - 1, j] == 'M' &&
               input[i - 2, j] == 'A' &&
               input[i - 3, j] == 'S')
            {
                counter++;
            }

            //down-right
            if (j <= input.GetLength(1) - 4 &&
                i <= input.GetLength(0) - 4 &&
                input[i + 1, j + 1] == 'M' &&
                input[i + 2, j + 2] == 'A' &&
                input[i + 3, j + 3] == 'S')
            {
                counter++;
            }

            //down-left
            if (j <= input.GetLength(1) - 4 &&
                i >= 3 &&
                input[i - 1, j + 1] == 'M' &&
                input[i - 2, j + 2] == 'A' &&
                input[i - 3, j + 3] == 'S')
            {
                counter++;
            }

            //up-right
            if (j >= 3 &&
                i <= input.GetLength(0) - 4 &&
                input[i + 1, j - 1] == 'M' &&
                input[i + 2, j - 2] == 'A' &&
                input[i + 3, j - 3] == 'S')
            {
                counter++;
            }

            //up-left
            if (j >= 3 &&
                i >= 3 &&
                input[i - 1, j - 1] == 'M' &&
                input[i - 2, j - 2] == 'A' &&
                input[i - 3, j - 3] == 'S')
            {
                counter++;
            }

            return counter;
        }
    }
}
