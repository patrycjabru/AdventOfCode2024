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
        private static readonly int[] dCol = { 0, 1, 0, -1 };  // row deltas for up/down
        private static readonly int[] dRow = { -1, 0, 1, 0 };  // col deltas for left/right
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("16");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            var (start, end) = FindStartAndEnd(inputAsArray);

            var result = Dijkstra(inputAsArray, start.column, start.row, end.column, end.row, Direction.Up);
            var dist = result[end.column, end.row];
            return dist.ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("16");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);
            char[,] transposed = TransposeMaze(inputAsArray);

            var (start, end) = FindStartAndEnd(inputAsArray);

            var cells = FindShortestPathCells(transposed, start.column, start.row, end.column, end.row);

            return cells.Count.ToString();
        }

        private static char[,] TransposeMaze(char[,] inputAsArray)
        {
            char[,] transposed = new char[inputAsArray.GetLength(1), inputAsArray.GetLength(0)];

            for (int i = 0; i < inputAsArray.GetLength(0); i++)
            {
                for (int j = 0; j < inputAsArray.GetLength(1); j++)
                {
                    transposed[j, i] = inputAsArray[i, j];
                }
            }

            return transposed;
        }

        private (Point?, Point?) FindStartAndEnd(char[,] input)
        {
            Point? start = null;
            Point? end = null;
            for (var i = 0; i < input.GetLength(0); i++)
            {
                for (var j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 'S')
                        start = new Point(i, j);
                    else if (input[i, j] == 'E')
                        end = new Point(i, j);
                }
            }

            return (start, end);
        }

        public static int[,] Dijkstra(char[,] maze, int startCol, int startRow, int endCol, int endRow, Direction startDirection)
        {
            int cols = maze.GetLength(0);
            int rows = maze.GetLength(1);
            int[,] distances = InitializeDistances(startCol, startRow, rows, cols);

            var pq = new PriorityQueue<(int col, int row, Direction prevDir), int>();
            pq.Enqueue((startCol, startRow, startDirection), 0);


            while (pq.Count > 0)
            {
                var (currentCol, currentRow, prevDir) = pq.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int newCol = currentCol + dCol[i];
                    int newRow = currentRow + dRow[i];

                    if (newCol >= 0 && newCol < cols && newRow >= 0 && newRow < rows && maze[newCol, newRow] != '#')
                    {
                        var newDirection = (Direction)i;
                        int directionCost = (prevDir == newDirection || prevDir == Direction.None) ? 1 : 1001;
                        int newDist = distances[currentCol, currentRow] + directionCost;

                        if (newDist < distances[newCol, newRow])
                        {
                            distances[newCol, newRow] = newDist;
                            pq.Enqueue((newCol, newRow, newDirection), newDist);
                        }
                    }
                }
            }
            return distances;
        }

        public static List<(int x, int y)> FindShortestPathCells(char[,] maze, int startCol, int startRow, int endCol, int endRow)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            int[,] startDistances = Dijkstra(maze, startCol, startRow, endCol, endRow, Direction.Up);
            int[,] endDistances = Dijkstra(maze, endCol, endRow, startCol, startRow, Direction.Down);

            int shortestPathLength = startDistances[endCol, endRow];
            if (shortestPathLength == int.MaxValue)
            {
                Console.WriteLine("No path found.");
                return null;
            }

            List<(int, int)> commonCells = new List<(int, int)>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var fromStart = startDistances[i, j];
                    var fromEnd = endDistances[i, j];

                    if (maze[i, j] != '#' &&
                        fromStart + fromEnd == shortestPathLength || fromStart + fromEnd + 1000 == shortestPathLength)
                    {
                        commonCells.Add((i, j));
                    }
                }
            }

            return commonCells;
        }


        private static int[,] InitializeDistances(int startCol, int startRow, int rows, int cols)
        {
            int[,] distances = new int[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    distances[i, j] = int.MaxValue;
                }
            }
            distances[startCol, startRow] = 0;
            return distances;
        }
    }
}
