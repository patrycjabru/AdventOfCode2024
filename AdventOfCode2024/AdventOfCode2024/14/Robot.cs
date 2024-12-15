using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._14
{
    public class Robot
    {
        public Point StartingPosition { get; set; }

        public int VelocityToRight { get; set; }

        public int VelocityToBottom { get; set; }

        public Point CurrentPosition { get; set; }

        public int Quadrant { get; set; } // 0 - left top, 1 - right top, 2 - right bottom, 3 - left bottom, 4 - on axis
    }
}
