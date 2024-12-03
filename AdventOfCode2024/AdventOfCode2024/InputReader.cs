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
