﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode2024._11
{
    public class Day11 : IDay
    {
        public string GetFirstAnswer()
        {
            return Count(25);
        }

        public string GetSecondAnswer()
        {
            return Count(75);
        }

        private string Count(int iterations)
        {
            var input = InputReader.ReadInput("11");
            var stoneNumbers = input.First().Split(' ');

            var stones = CreateStones(stoneNumbers);

            for (int i = 0; i < iterations; i++)
            {
                stones = TransformWithDict(stones);
            }
            long counter = 0;
            foreach (var stone in stones)
            {
                counter += stone.Value.Count;
            }
            return counter.ToString();
        }

        private Dictionary<string, StoneOnString> TransformWithDict(Dictionary<string, StoneOnString> stones)
        {
            var newDict = new Dictionary<string, StoneOnString>();
            foreach (KeyValuePair<string, StoneOnString> s in stones)
            {
                var stone = s.Value;
                if (stone.CheckIfZero())
                {
                    stone.Number = "1";
                    AddToDict(newDict, stone);
                }
                else if (stone.HasEvenNumberOfDigits())
                {
                    var newNumbers = stone.SplitNumber();
                    var stone1 = new StoneOnString(newNumbers.Item1);
                    stone1.Count = stone.Count;
                    var stone2 = new StoneOnString(newNumbers.Item2);
                    stone2.Count = stone.Count;
                    AddToDict(newDict, stone1);
                    AddToDict(newDict, stone2);
                }
                else
                {
                    stone.MultiplyNumberBy2024();
                    AddToDict(newDict, stone);
                }
            }
            return newDict;
        }

        private void AddToDict(Dictionary<string, StoneOnString> newDict, StoneOnString stone)
        {
            if (newDict.TryGetValue(stone.Number, out StoneOnString newStone))
            {
                newStone.Count += stone.Count;
            }
            else
            {
                newDict.Add(stone.Number, stone);
            }
        }

        private Dictionary<string, StoneOnString> CreateStones(IEnumerable<string> stonesNumbers)
        {
            var stones = new Dictionary<string, StoneOnString>();
            foreach (var stone in stonesNumbers)
            {
                if (stones.ContainsKey(stone))
                {
                    stones[stone].Count++;
                }
                else
                {
                    stones.Add(stone, new StoneOnString(stone));
                }
            }

            return stones;
        }
    }
}