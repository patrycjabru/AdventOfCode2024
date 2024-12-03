using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._02
{
    public class Day02 : IDay
    {
        public string GetFirstAnswer()
        {
            var lines = InputReader.ReadInput("02");

            return CalculateSafeReports(lines).ToString();
        }

        public string GetSecondAnswer()
        {
            var lines = InputReader.ReadInput("02");

            return CalculateSafeReportsWithProblemDampener(lines).ToString();
        }

        private int CalculateSafeReportsWithProblemDampener(IEnumerable<string> reports)
        {
            var safeReportsCounter = 0;
            foreach (var report in reports)
            {
                var levels = report.Split(' ').Select(x => int.Parse(x)).ToArray();
                var isSafe = CheckIfRaportIsSafe(levels);
                if (isSafe)
                {
                    safeReportsCounter++;
                }
                else
                {
                    var isAnySubsetSafe = CheckSubsets(levels);
                    if (isAnySubsetSafe)
                    {
                        safeReportsCounter++;
                    }
                }
            }
            return safeReportsCounter;
        }

        private bool CheckSubsets(int[] levels)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                var subset = levels.Where((val, idx) => idx != i).ToArray();
                var isSubsetSafe = CheckIfRaportIsSafe(subset);
                if (isSubsetSafe)
                    return true;
            }
            return false;
        }

        private int CalculateSafeReports(IEnumerable<string> reports)
        {
            var safeReportsCounter = 0;
            foreach (var report in reports)
            {
                var levels = report.Split(' ').Select(x => int.Parse(x)).ToArray();
                var isSafe = CheckIfRaportIsSafe(levels);
                if (isSafe)
                    safeReportsCounter++;
            }
            return safeReportsCounter;
        }

        private bool CheckIfRaportIsSafe(int[] levels)
        {
            var count = levels.Length;

            bool? previousIsIncreasing = null;

            for (int i = 0; i < count - 1; i++)
            {
                var diffrence = levels[i + 1] - levels[i];
                if (Math.Abs(diffrence) == 0 || Math.Abs(diffrence) > 3)
                {
                    return false;
                }

                bool? currentIsIncresing = null;
                if (diffrence > 0)
                {
                    currentIsIncresing = true;
                }
                else
                {
                    currentIsIncresing = false;
                }

                if (previousIsIncreasing != null)
                {
                    if (previousIsIncreasing.Value != currentIsIncresing.Value)
                    {
                        return false;
                    }
                    else
                    {
                        previousIsIncreasing = currentIsIncresing;
                    }
                }
                else
                {
                    previousIsIncreasing = currentIsIncresing;
                }
            }

            return true;
        }
    }
}
