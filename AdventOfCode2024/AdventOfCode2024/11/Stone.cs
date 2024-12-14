using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._11
{
    public class Stone
    {
        public long Number { get; set; }

        public bool HasEvenNumberOfDigits()
        {
            if (Number < 0)
                throw new ArgumentOutOfRangeException();
            if (Number == 0)
                return false;
            var numberOfDigits = (long)Math.Floor(Math.Log10(Number)) + 1;
            return numberOfDigits % 2 == 0;
        }

        public void MultiplyNumberBy2024()
        {
            Number *= 2024;
        }

        public (long, long) SplitNumber()
        {
            var text = Number.ToString();
            var length = text.Length;
            var middlePoint = length / 2;
            return (long.Parse(text.Substring(0, middlePoint)), long.Parse(text.Substring(middlePoint)));
        }
    }
}
