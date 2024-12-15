using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._14
{
    public class Day14 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("14");

            var robots = ParseInput(input).ToList();
            CalculateFinalPosition(robots, 101, 103, 100);

            var safetyFactor = CalculateSafetyFactor(robots);

            return safetyFactor.ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("14");

            var robots = ParseInput(input).ToList();
            //DisplayRobots(robots, 101, 103);
            for (var i = 0; i < 10403; i++)
            {
                CalculateFinalPosition(robots, 101, 103, i);
                
                foreach (var robot in robots)
                {
                    if (robots.FirstOrDefault(x => x.CurrentPosition.row == robot.CurrentPosition.row && x.CurrentPosition.column == robot.CurrentPosition.column + 1) != null)
                    {
                        if (robots.FirstOrDefault(x => x.CurrentPosition.row == robot.CurrentPosition.row && x.CurrentPosition.column == robot.CurrentPosition.column + 2) != null) 
                        {
                            if (robots.FirstOrDefault(x => x.CurrentPosition.row == robot.CurrentPosition.row && x.CurrentPosition.column == robot.CurrentPosition.column + 3) != null) 
                            {
                                if (robots.FirstOrDefault(x => x.CurrentPosition.row == robot.CurrentPosition.row && x.CurrentPosition.column == robot.CurrentPosition.column + 4) != null)
                                {
                                    if (robots.FirstOrDefault(x => x.CurrentPosition.row == robot.CurrentPosition.row && x.CurrentPosition.column == robot.CurrentPosition.column + 5) != null)
                                    {
                                        Console.WriteLine("Iterations: " + i);
                                        DisplayRobots(robots, 101, 103);
                                        Console.WriteLine("===========================================================" + i);
                                        Thread.Sleep(500);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return "";
        }

        private void DisplayRobots(IList<Robot> robots, int width, int length)
        {
            var room = new char[width, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    room[j, i] = '.';
                }
            }
            foreach (var robot in robots) 
            {
                if (robot.CurrentPosition == null)
                {
                    room[robot.StartingPosition.column, robot.StartingPosition.row] = 'X';
                }
                else
                {
                    room[robot.CurrentPosition.column, robot.CurrentPosition.row] = 'X';
                }
            }

            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write(room[i, j]);
                }
                Console.WriteLine();
            }
        }

        private long CalculateSafetyFactor(IEnumerable<Robot> robots)
        {
            long zero = 0;
            long first = 0;
            long second = 0;
            long third = 0;
            foreach (var robot in robots)
            {
                if (robot.Quadrant == 0)
                    zero++;
                else if (robot.Quadrant == 1)
                    first++;
                else if (robot.Quadrant == 2)
                    second++;
                else if (robot.Quadrant == 3)
                    third++;
            }
            return zero * first * second * third;
        }

        public void CalculateFinalPosition(IList<Robot> robots, int roomWidth, int roomLength, int simulationTime)
        {
            foreach (var robot in robots)
            {
                var finalPositionColumn = (robot.StartingPosition.column + robot.VelocityToRight * simulationTime) % roomWidth;
                if (finalPositionColumn < 0)
                {
                    finalPositionColumn = roomWidth + finalPositionColumn;
                }
                var finalPositionRow = (robot.StartingPosition.row + robot.VelocityToBottom * simulationTime) % roomLength;
                if (finalPositionRow < 0)
                {
                    finalPositionRow = roomLength + finalPositionRow;
                }
                robot.CurrentPosition = new Point(finalPositionRow, finalPositionColumn);
                robot.Quadrant = FindQuadrant(roomWidth, roomLength, robot, finalPositionColumn, finalPositionRow);
            }
        }

        private int FindQuadrant(int roomWidth, int roomLength, Robot robot, int finalPositionColumn, int finalPositionRow)
        {
            if (finalPositionColumn < roomWidth / 2) // left
            {
                if (finalPositionRow < roomLength / 2) // top
                {
                    return 0;
                }
                else if (finalPositionRow > roomLength / 2) // bottom
                {
                    return 3;
                }
                else // on axis
                {
                    return 4;
                }
            }
            else if (finalPositionColumn > roomWidth / 2) // right
            {
                if (finalPositionRow < roomLength / 2) // top
                {
                    return 1;
                }
                else if (finalPositionRow > roomLength / 2) // bottom
                {
                    return 2;
                }
                else // on axis
                {
                    return 4;
                }
            }
            else // on axis
            {
                return 4;
            }
        }

        private IEnumerable<Robot> ParseInput(IEnumerable<string> input)
        {
            foreach (var line in input)
            {
                var a = line.Split(' ');
                var coordinates = a[0].Split('=')[1].Split(',');
                var velocity = a[1].Split('=')[1].Split(",");
                var robot = new Robot()
                {
                    StartingPosition = new Point(int.Parse(coordinates[1]), int.Parse(coordinates[0])),
                    VelocityToRight = int.Parse(velocity[0]),
                    VelocityToBottom = int.Parse(velocity[1]),
                };
                yield return robot;
            }
        }

    }
}
