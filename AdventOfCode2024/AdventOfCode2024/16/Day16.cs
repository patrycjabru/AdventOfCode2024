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
            char[,] transposed = new char[inputAsArray.GetLength(1), inputAsArray.GetLength(0)];

            // Perform the row-column swap (transpose the array)
            for (int i = 0; i < inputAsArray.GetLength(0); i++)
            {
                for (int j = 0; j < inputAsArray.GetLength(1); j++)
                {
                    transposed[j, i] = inputAsArray[i, j];
                }
            }

            var (start, end) = FindStartAndEnd(inputAsArray);

            var cells = FindShortestPathCells(transposed, start.column, start.row, end.column, end.row);

            return cells.Count.ToString();
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

        public static List<(int x, int y)> FindShortestPathCells(char[,] maze, int startX, int startY, int endX, int endY)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);

            // Compute shortest path distances from the start and the end
            int[,] startDistances = Dijkstra(maze, startX, startY, endX, endY, Direction.Up);
            int[,] endDistances = Dijkstra(maze, endX, endY, startX, startY, Direction.Down);

            // Find the length of the shortest path from start to end
            int shortestPathLength = startDistances[endX, endY];
            if (shortestPathLength == int.MaxValue)
            {
                Console.WriteLine("No path found.");
                return null;
            }

            // Iterate through the maze and find all cells that belong to all shortest paths
            List<(int x, int y)> commonCells = new List<(int x, int y)>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var fromStart = startDistances[i, j];
                    var fromEnd = endDistances[i, j];
                    // Check if the cell is open and part of the shortest paths
                    if (maze[i, j] != '#' &&
                        fromStart + fromEnd == shortestPathLength || fromStart + fromEnd + 1000 == shortestPathLength)
                    {
                        commonCells.Add((i, j));
                    }
                }
            }

            //foreach (var cell in commonCells)
            //{
            //    Console.WriteLine(cell);
            //}

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
