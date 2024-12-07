using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._05
{
    public class Day05 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("05");

            var structuredInput = GetRulesAndPageSets(input);
            var sumOfMiddlePageNumbers = GetSumOfMiddlePageSets(structuredInput.rules, structuredInput.pages);

            return sumOfMiddlePageNumbers.ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("05");

            var structuredInput = GetRulesAndPageSets(input);
            return GetSumOfMiddlePagesInIncorectlyOrderedSets(structuredInput.rules, structuredInput.pages).ToString();
        }

        private int GetSumOfMiddlePagesInIncorectlyOrderedSets(Dictionary<string, HashSet<string>> rules, List<List<string>> pages)
        {
            var sum = 0;
            foreach (var pageSet in pages) {
                var isValid = IsPageSetValid(rules, pageSet);
                if (!isValid) 
                {
                    Sort(pageSet, rules);
                    var middleIndex = pageSet.Count / 2;
                    var middlePageNumber = int.Parse(pageSet[middleIndex]);
                    sum += middlePageNumber;
                }
            }

            return sum;
        }

        private void Sort(List<string> pageSet, Dictionary<string, HashSet<string>> rules)
        {
            for (int pageIndex = 0; pageIndex < pageSet.Count; pageIndex++)
            {
                var page = pageSet[pageIndex];
                for (int pageIndex2 = pageIndex + 1; (pageIndex2 < pageSet.Count); pageIndex2++)
                {
                    var page2 = pageSet[pageIndex2];
                    if (rules[page2].Contains(page))
                    {
                        pageSet[pageIndex2] = page;
                        pageSet[pageIndex] = page2;
                        page = pageSet[pageIndex];
                    }
                }
            }
        }

        private int GetSumOfMiddlePageSets(Dictionary<string, HashSet<string>> rules, List<List<string>> pages)
        {
            var sum = 0;
            foreach (var pageSet in pages)
            {
                var isValid = IsPageSetValid(rules, pageSet);
                if (isValid)
                {
                    var middleIndex = pageSet.Count / 2;
                    var middlePageNumber = int.Parse(pageSet[middleIndex]);
                    sum += middlePageNumber;
                }
            }
            return sum;
        }

        private bool IsPageSetValid(Dictionary<string, HashSet<string>> rules, List<string> pageSet)
        {
            for (int pageIndex = 0; pageIndex < pageSet.Count; pageIndex++)
            {
                var page = pageSet[pageIndex];
                for (int pageIndex2 = pageIndex + 1; (pageIndex2 < pageSet.Count); pageIndex2++)
                {
                    var page2 = pageSet[pageIndex2];
                    if (rules[page2].Contains(page))
                        return false;
                }
            }
            return true;
        }

        private (Dictionary<string, HashSet<string>> rules, List<List<string>> pages) GetRulesAndPageSets(IEnumerable<string> input)
        {
            var rules = new Dictionary<string, HashSet<string>>();
            var pages = new List<List<string>>();

            var section = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line)) {
                    section = 1;
                    continue;
                }

                // parse rules
                if (section == 0)
                {
                    var numbers = line.Split('|');
                    if (rules.ContainsKey(numbers[0])) //optimize
                    {
                        rules[numbers[0]].Add(numbers[1]);
                    }
                    else
                    {
                        rules.Add(numbers[0], new HashSet<string>()
                        {
                            numbers[1]
                        });
                    }
                }

                //parse page sets
                if (section == 1)
                {
                    var splitted = line.Split(',');
                    pages.Add(new List<string>(splitted));
                }
            }

            return (rules, pages);
        }
    }
}
