using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._11
{
    public class Day11 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("11");
            var stoneNumbers = input.First().Split(' ');

            var stones = CreateStones(stoneNumbers).ToList();

            for (var i = 0; i < 25; i++)
            {
                TransfromStones(stones);
            }

            return stones.Count.ToString();
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Stone> CreateStones(IEnumerable<string> stones)
        {
            foreach (var stone in stones)
            {
                yield return new Stone()
                {
                    Number = long.Parse(stone)
                };
            }
        }

        private void TransfromStones(List<Stone> stones)
        {
            for (int i = 0; i < stones.Count; i++)
            {
                if (stones[i].Number == 0)
                {
                    stones[i].Number = 1;
                }
                else if (stones[i].HasEvenNumberOfDigits())
                {
                    var oldStone = stones[i];
                    var newNumbers = oldStone.SplitNumber();
                    stones.RemoveAt(i);
                    stones.Insert(i, new Stone() { Number = newNumbers.Item1 });
                    stones.Insert(i + 1, new Stone() { Number = newNumbers.Item2});
                    i++;
                }
                else
                {
                    stones[i].MultiplyNumberBy2024();
                }
            }
        } 
    }
}
