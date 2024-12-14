using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class DirectedPoint
    {
        public DirectedPoint(int row, int column, int direction)
        {
            this.row = row;
            this.column = column;
            this.direction = direction;
        }

        public int row;
        public int column;
        public int direction; // 0 - up, 1 - right, 2 - down, 3 - left

        public override bool Equals(object? other)
        {
            if (other !=  null && other is DirectedPoint)
            {
                var otherPoint = (DirectedPoint)other;
                if (otherPoint.row == this.row && otherPoint.column == this.column && otherPoint.direction == this.direction)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(row, column, direction);
        }
    }
}
