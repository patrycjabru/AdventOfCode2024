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
            throw new NotImplementedException();
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

        private long CalculateChecksum(int?[] input)
        {
            long checksum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int? inputItem = input[i];
                if (!inputItem.HasValue)
                    break;
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
