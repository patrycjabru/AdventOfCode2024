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
        private static readonly int[] dx = { -1, 1, 0, 0 };  // row deltas for up/down
        private static readonly int[] dy = { 0, 0, -1, 1 };  // col deltas for left/right
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("16");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            var (start, end) = FindStartAndEnd(inputAsArray);

            var result = Dijkstra(inputAsArray, start.column, start.row, end.column, end.row);

            return result.ToString();
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
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

        public static int Dijkstra(char[,] maze, int startX, int startY, int endX, int endY)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);
            int[,] distances = InitializeDistances(startX, startY, rows, cols);

            var pq = new PriorityQueue<(int x, int y, Direction prevDir), int>();
            pq.Enqueue((startX, startY, Direction.Up), 0);


            while (pq.Count > 0)
            {
                var (currentX, currentY, prevDir) = pq.Dequeue();

                if (currentX == endX && currentY == endY)
                {
                    return distances[endX, endY];
                }

                for (int i = 0; i < 4; i++)
                {
                    int newX = currentX + dx[i];
                    int newY = currentY + dy[i];

                    if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && maze[newX, newY] != '#')
                    {
                        int directionCost = (prevDir == (Direction)i) ? 1 : 1001;
                        int newDist = distances[currentX, currentY] + directionCost;

                        if (newDist < distances[newX, newY])
                        {
                            distances[newX, newY] = newDist;
                            pq.Enqueue((newX, newY, (Direction)i), newDist);
                        }
                    }
                }
            }
            return 0;
        }

        private static int[,] InitializeDistances(int startX, int startY, int rows, int cols)
        {
            int[,] distances = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    distances[i, j] = int.MaxValue;
                }
            }
            distances[startX, startY] = 0;
            return distances;
        }
    }
}
