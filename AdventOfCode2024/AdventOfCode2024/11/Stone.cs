using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._11
{
    public class StoneOnString
    {
        public string Number { get; set; }

        public long Count { get; set; }

        public StoneOnString(string number)
        {
            Count = 1;
            this.Number = long.Parse(number).ToString();
        }

        public bool CheckIfZero()
        {
            return Number == "0";
        }

        public bool HasEvenNumberOfDigits()
        {
            return Number.Length % 2 == 0;
        }

        public void MultiplyNumberBy2024()
        {
            Number = (long.Parse(Number) * 2024).ToString();
        }

        public (string, string) SplitNumber()
        {
            var midPoint = Number.Length / 2;

            var firstNumber = Number.Substring(0, midPoint);
            var secondNumber = Number.Substring(midPoint);

            return (firstNumber, secondNumber);
        }

        public override bool Equals(object? other)
        {
            if (other != null && other is StoneOnString)
            {
                var otherStone = (StoneOnString)other;
                if (otherStone.Number == Number)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number);
        }
    }
}
