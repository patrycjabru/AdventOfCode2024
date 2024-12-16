using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._15
{
    public class Room
    {
        public Item[,] map { get; set; }
        public IList<Move> moves { get; set; }

        public Point RobotPosition { get; set; }

        public int SimulationStep { get; set; } = 0;

        public void PrintRoom()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == Item.None)
                        Console.Write('.');
                    else if (map[i, j] == Item.Box)
                        Console.Write('0');
                    else if (map[i,j] == Item.Wall) 
                        Console.Write("#");
                    else 
                        Console.Write("@");
                }
                Console.WriteLine();
            }
        }

        public long GetGPSSum()
        {
            var sum = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == Item.Box)
                        sum += 100 * i + j;
                }
            }
            return sum;
        }


        public void MakeMove()
        {
            var action = moves[SimulationStep];

            if (action == Move.Up)
            {
                var newRow = RobotPosition.row - 1;
                var newColumn = RobotPosition.column;
                MakeMoveToDirection(newRow, newColumn, Move.Up);
            }
            else if (action == Move.Down)
            {
                var newRow = RobotPosition.row + 1;
                var newColumn = RobotPosition.column;
                MakeMoveToDirection(newRow, newColumn, Move.Down);
            }
            else if (action == Move.Left)
            {
                var newRow = RobotPosition.row;
                var newColumn = RobotPosition.column - 1;
                MakeMoveToDirection(newRow, newColumn, Move.Left);
            }
            else //move right
            {
                var newRow = RobotPosition.row;
                var newColumn = RobotPosition.column + 1;
                MakeMoveToDirection(newRow, newColumn, Move.Right);
            }

            SimulationStep++;
        }

        private void MakeMoveToDirection(int newRow, int newColumn, Move direction)
        {
            var objectAbove = map[newRow, newColumn];
            if (objectAbove == Item.Wall)
                return;
            if (objectAbove == Item.Box)
            {
                var canMoveBoxes = TryMoveBoxes(newRow, newColumn, direction);
                if (canMoveBoxes == true)
                {
                    MoveRobot(RobotPosition.row, RobotPosition.column, newRow, newColumn);
                }
                else
                {
                    return;
                }
            }
            if (objectAbove == Item.None)
            {
                MoveRobot(RobotPosition.row, RobotPosition.column, newRow, newColumn);
            }
        }

        private void MoveRobot(int currentRow, int currentColumn, int newRow, int newColumn)
        {
            map[newRow, newColumn] = Item.Robot;
            RobotPosition = new Point(newRow, newColumn);
            map[currentRow, currentColumn] = Item.None;
        }

        private bool TryMoveBoxes(int row, int column, Move direction)
        {
            var r = row;
            var c = column;
            while (true)
            {
                if (map[r, c] == Item.Wall)
                    return false;
                else if (map[r, c] == Item.Box)
                {
                    if (direction == Move.Left)
                        c--;
                    else if (direction == Move.Right)
                        c++;
                    else if (direction == Move.Up)
                        r--;
                    else
                        r++;
                }
                else
                {
                    map[r, c] = Item.Box;
                    map[row, column] = Item.Wall;
                    return true;
                }
            }
        }
    }
}
