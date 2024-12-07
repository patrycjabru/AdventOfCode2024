namespace AdventOfCode2024._06
{
    public class Day06 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("06").ToArray();

            var inputAsArray = ConvertInputToArray(input);

            SimulateGuardRoute(inputAsArray);

            var distinctPositionsCount = CountDistinctPositions(inputAsArray);

            return distinctPositionsCount.ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("06").ToArray();

            var inputAsArray = ConvertInputToArray(input);

            var (guardPositionColumn, guardPositionRow) = FindStartingPosition(inputAsArray);
            int direction = 0; // 0 - up, 1 - right, 2 - down, 3 - left

            var count = SimulateGuardRouteAndCountLoopSolution(inputAsArray, guardPositionColumn, guardPositionRow, direction);

            return count.ToString();
        }

        private int CountDistinctPositions(char[,] inputAsArray) //optimize to count during simulation
        {
            var count = 0;
            foreach (var element in inputAsArray)
            {
                if (element == '0' || element == '1' || element == '2' || element == '3')
                {
                    count++;
                }
            }
            return count;
        }

        private int SimulateGuardRouteAndCountLoopSolution(char[,] inputAsArray, int guardPositionColumn, int guardPositionRow, int direction)
        {
            var isFirstIteration = true;
            var solutions = new HashSet<Tuple<int, int>>();
            var isLoop = false;
            while (true)
            {
                if (CheckIfBorder(inputAsArray, guardPositionColumn, guardPositionRow))
                {
                    break;
                }
                switch (direction)
                {
                    case 0:
                        HandleUp(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        isLoop = CheckNewObstacle(inputAsArray, guardPositionColumn, guardPositionRow, direction);
                        if (isLoop && !isFirstIteration)
                            solutions.Add(new Tuple<int, int>(guardPositionRow, guardPositionColumn));
                        break;
                    case 1:
                        HandleRight(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        isLoop = CheckNewObstacle(inputAsArray, guardPositionColumn, guardPositionRow, direction);
                        if (isLoop)
                            solutions.Add(new Tuple<int, int>(guardPositionRow, guardPositionColumn));
                        break;
                    case 2:
                        HandleDown(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        isLoop = CheckNewObstacle(inputAsArray, guardPositionColumn, guardPositionRow, direction);
                        if (isLoop)
                            solutions.Add(new Tuple<int, int>(guardPositionRow, guardPositionColumn));
                        break;
                    case 3:
                        HandleLeft(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        isLoop = CheckNewObstacle(inputAsArray, guardPositionColumn, guardPositionRow, direction);
                        if (isLoop)
                            solutions.Add(new Tuple<int, int>(guardPositionRow, guardPositionColumn));
                        break;
                    default:
                        {
                            throw new Exception();
                        }
                }
                isFirstIteration = false;
            }
            return solutions.Count;
        }

        private bool CheckNewObstacle(char[,] inputAsArray, int guardPositionColumn, int guardPositionRow, int direction)
        {
            if (CheckIfBorder(inputAsArray, guardPositionColumn, guardPositionRow))
            {
                return false;
            }
            var copy = CopyArray(inputAsArray);
            copy[guardPositionRow, guardPositionColumn] = '#';
            var isLoop = SimulateGuardRouteAndCheckForLoop(copy, guardPositionColumn, guardPositionRow, direction);
            return isLoop;
        }

        private bool SimulateGuardRouteAndCheckForLoop(char[,] inputAsArray, int guardPositionColumn, int guardPositionRow, int direction)
        {
            while (true)
            {
                if (CheckIfBorder(inputAsArray, guardPositionColumn, guardPositionRow))
                {
                    break;
                }
                if (CheckForLoop(inputAsArray, guardPositionColumn, guardPositionRow, direction))
                {
                    return true;
                }
                switch (direction)
                {
                    case 0:
                        HandleUp(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    case 1:
                        HandleRight(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    case 2:
                        HandleDown(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    case 3:
                        HandleLeft(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    default:
                        {
                            throw new Exception();
                        }
                }
            }

            return false;
        }

        private bool CheckForLoop(char[,] inputAsArray, int guardPositionColumn, int guardPositionRow, int direction)
        {
            if (inputAsArray[guardPositionRow, guardPositionColumn] == DigitToChar(direction))
            {
                return true;
            }
            return false;
        }

        private char DigitToChar(int digit)
        {
            return (char)(digit + '0');
        }

        private void SimulateGuardRoute(char[,] inputAsArray)
        {
            int direction = 0; // 0 - up, 1 - right, 2 - down, 3 - left

            var (guardPositionColumn, guardPositionRow) = FindStartingPosition(inputAsArray);

            while (true)
            {
                if (CheckIfBorder(inputAsArray, guardPositionColumn, guardPositionRow))
                {
                    break;
                }
                switch (direction)
                {
                    case 0:
                        HandleUp(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    case 1:
                        HandleRight(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    case 2:
                        HandleDown(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    case 3:
                        HandleLeft(inputAsArray, ref direction, ref guardPositionColumn, ref guardPositionRow);
                        break;
                    default:
                        {
                            throw new Exception();
                        }
                }
            }
        }

        private bool CheckIfBorder(char[,] inputAsArray, int guardPositionColumn, int guardPositionRow)
        {
            if (guardPositionColumn < 0 || guardPositionRow < 0 || guardPositionColumn >= inputAsArray.GetLength(0) || guardPositionRow >= inputAsArray.GetLength(1))
            {
                return true;
            }
            return false;
        }

        private void HandleLeft(char[,] inputAsArray, ref int direction, ref int guardPositionColumn, ref int guardPositionRow)
        {
            if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
            {
                direction = 0;
                guardPositionColumn++;
                guardPositionRow--;
            }
            else
            {
                inputAsArray[guardPositionRow, guardPositionColumn] = '3';
                guardPositionColumn--;
            }
        }

        private void HandleDown(char[,] inputAsArray, ref int direction, ref int guardPositionColumn, ref int guardPositionRow)
        {
            if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
            {
                direction = 3;
                guardPositionRow--;
                guardPositionColumn--;
            }
            else
            {
                inputAsArray[guardPositionRow, guardPositionColumn] = '2';
                guardPositionRow++;
            }
        }

        private void HandleRight(char[,] inputAsArray, ref int direction, ref int guardPositionColumn, ref int guardPositionRow)
        {
            if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
            {
                direction = 2;
                guardPositionColumn--;
                guardPositionRow++;
            }
            else
            {
                inputAsArray[guardPositionRow, guardPositionColumn] = '1';
                guardPositionColumn++;
            }
        }

        private void HandleUp(char[,] inputAsArray, ref int direction, ref int guardPositionColumn, ref int guardPositionRow)
        {
            if (inputAsArray[guardPositionRow, guardPositionColumn] == '#')
            {
                direction = 1;
                guardPositionRow++;
                guardPositionColumn++;
            }
            else
            {
                inputAsArray[guardPositionRow, guardPositionColumn] = '0';
                guardPositionRow--;
            }
        }

        private (int i, int j) FindStartingPosition(char[,] inputAsArray)
        {
            for (int i = 0; i < inputAsArray.GetLength(0); i++)
            {
                for (int j = 0; j < inputAsArray.GetLength(1); j++)
                {
                    if (inputAsArray[i,j] == '^')
                    {
                        return (j, i);
                    }
                }
            }

            throw new Exception("Starting point not found");
        }

        private char[,] ConvertInputToArray(IEnumerable<string> input)
        {
            char[,] result = input
                .Select((str, rowIndex) => str.Select((c, colIndex) => new { rowIndex, colIndex, c }))
                .SelectMany(x => x)
                .Aggregate(
                    new char[input.Count(), input.Max(s => s.Length)],
                (arr, x) => {
                    arr[x.rowIndex, x.colIndex] = x.c; return arr;
                });

            return result;
        }

        private char[,] CopyArray(char[,] input) 
        {
            char[,] copy = new char[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    copy[i, j] = input[i, j];
                }
            }

            return copy;
        }
    }
}
