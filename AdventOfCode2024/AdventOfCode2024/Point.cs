using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Point
    {
        public Point(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public int row;
        public int column;

        public override bool Equals(object? other)
        {
            if (other !=  null && other is Point)
            {
                var otherPoint = (Point)other;
                if (otherPoint.row == this.row && otherPoint.column == this.column)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(row, column);
        }
    }
}
