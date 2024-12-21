using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._16
{
    public class Day16 : IDay
    {
        int BestPathScore = int.MaxValue;
        List<int> bestPathPointsHistory;
        List<Direction> BestPath;
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("16");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            var start = FindStart(inputAsArray);
            FindPath(inputAsArray, start, inputAsArray.GetLength(0), inputAsArray.GetLength(1));

            return "";
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private Point? FindStart(char[,] input)
        {
            for (var i = 0; i < input.GetLength(0); i++)
            {
                for (var j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i,j] == 'S')
                        return new Point(i, j);
                }
            }

            return null;
        }

        public void FindPath(char[,] input, Point start, int length, int width)
        {
            var paths = new List<List<Direction>>();

            CheckPath(input, new Point(start.row - 1, start.column), length, width, Direction.Up, new List<Direction>(), new HashSet<Point>(), 1000);
            CheckPath(input, new Point(start.row + 1, start.column), length, width, Direction.Down, new List<Direction>(), new HashSet<Point>(), 1000);
            CheckPath(input, new Point(start.row, start.column - 1), length, width, Direction.Left, new List<Direction>(), new HashSet<Point>(), 1000);
            CheckPath(input, new Point(start.row, start.column + 1), length, width, Direction.Right, new List<Direction>(), new HashSet<Point>(), 0);
        }

        public bool CheckPath(char[,] input, Point currentPosition, int length, int width, Direction lastMove, List<Direction> moves, HashSet<Point> positions, int points)
        {
            if (points > BestPathScore)
                return false;

            if (currentPosition.row < 0 || currentPosition.column < 0 || currentPosition.row == length || currentPosition.column == width)
                return false;
            
            if (input[currentPosition.row, currentPosition.column] == '#') 
                return false;

            var checkIfNooLoop = positions.Add(currentPosition);
            if (!checkIfNooLoop)
                return false;

            moves.Add(lastMove);

            if (input[currentPosition.row, currentPosition.column] == 'E')
            {
                foreach (var move in moves)
                {
                    Console.Write(move);
                }
                Console.WriteLine();
                BestPathScore = points + 1;
                BestPath = moves;
                return true;
            }

            if (lastMove != Direction.Down)
            {
                var newPoints = points;
                if (lastMove == Direction.Up)
                    newPoints += 1;
                else
                    newPoints += 1001;
                var upList = new List<Direction>(moves);
                var upPositions = new HashSet<Point>(positions);
                CheckPath(input, new Point(currentPosition.row - 1, currentPosition.column), length, width, Direction.Up, upList, upPositions, newPoints);
            }
            if (lastMove != Direction.Left)
            {
                var newPoints = points;
                if (lastMove == Direction.Right)
                    newPoints += 1;
                else
                    newPoints += 1001;
                var rightList = new List<Direction>(moves);
                var rightPositions = new HashSet<Point>(positions);
                CheckPath(input, new Point(currentPosition.row, currentPosition.column + 1), length, width, Direction.Right, rightList, rightPositions, newPoints);
            }
            if (lastMove != Direction.Up)
            {
                var newPoints = points;
                if (lastMove == Direction.Down)
                    newPoints += 1;
                else
                    newPoints += 1001;
                var downList = new List<Direction>(moves);
                var downPositions = new HashSet<Point>(positions);
                CheckPath(input, new Point(currentPosition.row + 1, currentPosition.column), length, width, Direction.Down, downList, downPositions, newPoints);
            }
            if (lastMove != Direction.Right)
            {
                var newPoints = points;
                if (lastMove == Direction.Left)
                    newPoints += 1;
                else
                    newPoints += 1001;
                var leftList = new List<Direction>(moves);
                var leftPositions = new HashSet<Point>(positions);
                CheckPath(input, new Point(currentPosition.row, currentPosition.column - 1), length, width, Direction.Left, leftList, leftPositions, newPoints);
            }
            return false;
        }
    }
}
