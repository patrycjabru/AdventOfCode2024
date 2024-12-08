using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public static class InputReader
    {
        public static IEnumerable<string> ReadInput(string directoryName)
        {
            var fullPath = InputReader.GetInputPath(directoryName);
            return File.ReadLines(fullPath);
        }

        public static char[,] ConvertInputToTwodimmensionalArray(IEnumerable<string> input)
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

        private static string GetInputPath(string directoryName)
        {
            return Path.Combine(GetSourceDirectory(), directoryName, "input.txt");
        }

        private static string GetSourceDirectory()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }
    }
}
