using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._09
{
    public class Day09_2 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("09").First();

            var processedInput = ProcessInput(input).ToArray();
            Compress(processedInput);
            return CalculateChecksum(processedInput).ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("09").First();

            var processedInput = ProcessToArray(input);
            CompressAndKeepWholeFiles(processedInput);
            var transformed = TransformToStringOfBlocks(processedInput);
            return CalculateChecksum(transformed, false).ToString();
        }

        private int?[] TransformToStringOfBlocks(Section[] processedInput)
        {
            var output = new List<int?>();
            foreach (var section in processedInput)
            {
                for (var f = 0; f < section.FileSizes.Count; f++)
                {
                    for (var i = 0; i < section.FileSizes[f]; i++)
                    {
                        output.Add(section.FileIds[f]);
                    }
                }
                var freeSize = section.GetFreeSpace();
                for (var i = 0; i < freeSize; i++)
                {
                    output.Add(null);
                }
            }
            return output.ToArray();
        }

        private Section[] ProcessToArray(string input)
        {
            var output = new List<Section>();
            var isFile = true;
            for (var i = 0; i < input.Length; i++)
            {
                var size = int.Parse(input[i].ToString());
                if (isFile)
                {
                    output.Add(new Section()
                    {
                        FileIds = new List<int>()
                        {
                            i/2
                        },
                        Size = size,
                        FileSizes = new List<int>()
                        {
                            size
                        },
                        PositionId = i
                    });
                }
                else
                {
                    output.Add(new Section()
                    {
                        Size = size,
                        PositionId = i
                    });
                }
                isFile = !isFile;
            };

            return output.ToArray();
        }

        private void CompressAndKeepWholeFiles(Section[] input)
        {
            for (var i = input.Length - 1; i > 0; i--)
            {
                var section = input[i];
                if (section.FileIds.Count == 0)
                {
                    continue;
                }

                var sizeToFind = input[i].FileSizes[0];
                var freeSlot = input.FirstOrDefault(x => x.GetFreeSpace() >= sizeToFind && x.PositionId < section.PositionId);

                if (freeSlot == null)
                    continue;

                freeSlot.FileSizes.Add(section.FileSizes[0]);
                freeSlot.FileIds.Add(section.FileIds[0]);
              

                section.FileSizes = new List<int>();
                section.FileIds = new List<int>();
            }
        }

        private void Compress(int?[] processedInput)
        {
            int i = 0;
            int j = processedInput.Length - 1;

            while (i < j)
            {
                if (processedInput[i] == null)
                {
                    if (processedInput[j] != null)
                    {
                        processedInput[i] = processedInput[j];
                        processedInput[j] = null;
                        i++;
                        j--;
                    }
                    else
                    {
                        j--;
                    }
                }
                else
                {
                    i++;
                }
            }
        }

        private long CalculateChecksum(int?[] input, bool breakOnNull = true)
        {
            long checksum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int? inputItem = input[i];
                if (!inputItem.HasValue)
                {
                    if (breakOnNull)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                checksum += i * inputItem.Value;
            }
            return checksum;
        }

        private List<int?> ProcessInput(string input)
        {
            var processedInput = new List<int?>();
            var isFile = true;
            var fileId = 0;
            foreach (var i in input)
            {
                var number = int.Parse(i.ToString());
                if (isFile)
                {
                    for(int n=0; n<number; n++)
                    {
                        processedInput.Add(fileId);
                    }
                    fileId++;
                    isFile = !isFile;
                    
                }
                else
                {
                    for (int n = 0; n < number; n++)
                    {
                        processedInput.Add(null);
                    }
                    isFile = !isFile;
                }

            }

            return processedInput;
        }
    }
}
