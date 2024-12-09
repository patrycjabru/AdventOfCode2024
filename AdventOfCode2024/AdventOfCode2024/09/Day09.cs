using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._09
{
    public class Day09 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("09").First();

            return CalculateChecksum(input).ToString();
        }
        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private long CalculateChecksum(string input)
        {
            long checksum = 0;
            var isFreeSpace = false;
            var blockCounter = 0;
            var fileId = 0;
            var fileIdFromEnd = input.Length / 2 + 1;
            var fileIndexFromEnd = input.Length - 1;
            var howManyLeftWithThisId = 0;
            var iterationsToFinish = int.MaxValue;
            var flagToFinish = false;
            var processedFiles = new Dictionary<int, int>(); //key - file id, value - number of blocks processed

            for (int indexFromBegining = 0; indexFromBegining < input.Length; indexFromBegining++)
            {
                
                if (fileId >= fileIdFromEnd)
                {
                    if (iterationsToFinish == 0)
                    {
                        break;
                    }
                    if (!flagToFinish)
                    {
                        var size = int.Parse(input[indexFromBegining + 1].ToString());
                        flagToFinish = true;
                        iterationsToFinish = size - processedFiles[fileIdFromEnd];
                    }
                }
                    
                if (!isFreeSpace)
                {
                    var fileSize = int.Parse(input[indexFromBegining].ToString());
                    for (int fileBlockIndex = 0; fileBlockIndex < fileSize; fileBlockIndex++)
                    {
                        if (flagToFinish)
                        {
                            iterationsToFinish--;
                        }
                        checksum += blockCounter * fileId;
                        if (iterationsToFinish == 0)
                            break;
                        UpdateDictionary(fileId, processedFiles);
                        blockCounter++;
                    }
                    fileId++;
                }
                else
                {
                    var freeSpaceSize = int.Parse(input[indexFromBegining].ToString());
                    for (int freeBlockIndex = 0; freeBlockIndex < freeSpaceSize; freeBlockIndex++)
                    {
                        if (howManyLeftWithThisId == 0)
                        {
                            var fileSizeFromEnd = int.Parse(input[fileIndexFromEnd].ToString());
                            howManyLeftWithThisId = fileSizeFromEnd;
                            fileIdFromEnd--;
                            fileIndexFromEnd -= 2;
                        }

                        if(flagToFinish)
                        {
                            iterationsToFinish--;
                        }
                        checksum += blockCounter * fileIdFromEnd;
                        if (iterationsToFinish == 0)
                            break;
                        UpdateDictionary(fileIdFromEnd, processedFiles);
                        blockCounter++;
                        howManyLeftWithThisId--;
                    }
                }
                isFreeSpace = !isFreeSpace;
            }

            return checksum;
        }

        private void UpdateDictionary(int fileId, Dictionary<int, int> processedFiles)
        {
            if (processedFiles.ContainsKey(fileId))
            {
                processedFiles[fileId]++;
            }
            else
            {
                processedFiles.Add(fileId, 1);
            }
        }
    }
}
