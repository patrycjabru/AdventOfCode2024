using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._09
{
    public class Section
    {
        public List<int> FileIds { get; set; } = new List<int>();
        public int Size { get; set; }
        public List<int> FileSizes { get; set; } = new List<int>();

        public int PositionId { get; set; }

        public int GetFreeSpace()
        {
            var filesSizes = 0;
            foreach (var size in FileSizes)
            {
                filesSizes += size;
            }

            return Size - filesSizes;
        }
    }
}
