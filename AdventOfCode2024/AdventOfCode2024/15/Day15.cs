using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._15
{
    public class Day15 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("15");
            var room = ParseInput(input, 50, 50);
            Simulation(room);
            return room.GetGPSSum().ToString();
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private void Simulation(Room room)
        {
            foreach (var move in room.moves)
            {
                //Console.WriteLine("Iteration " + room.SimulationStep);
                //Console.WriteLine("Move" + room.moves[room.SimulationStep]);
                //room.PrintRoom();
                //Console.ReadLine();
                room.MakeMove();
            }
            //room.PrintRoom();
            //Console.ReadLine();
        }

        private Room ParseInput(IEnumerable<string> input, int inputRows, int inputColumns)
        {
            var secondPart = false;
            var roomMap = new Item[inputRows, inputColumns];
            var moves = new List<Move>();
            Point robotPosition = new Point(0, 0);

            var rowIterator = 0;
            foreach (var line in input)
            {
                var columnIterator = 0;
                if (!secondPart) //room map
                {
                    if (line.Length == 0) 
                        secondPart = true;
                    foreach (var c in line)
                    {
                        if (c == '.')
                        {
                            roomMap[rowIterator, columnIterator] = Item.None;
                        }
                        else if (c == '#')
                        {
                            roomMap[rowIterator, columnIterator] = Item.Wall;
                        }
                        else if (c == '@')
                        {
                            roomMap[rowIterator, columnIterator] = Item.Robot;
                            robotPosition = new Point(rowIterator, columnIterator);
                        }
                        else if (c == 'O')
                        {
                            roomMap[rowIterator, columnIterator] = Item.Box;
                        }
                        columnIterator++;
                    }
                }
                else //moves
                {
                    foreach (var c in line)
                    {
                        if (c == '<')
                        {
                            moves.Add(Move.Left);
                        }
                        else if (c == '^')
                        {
                            moves.Add(Move.Up);
                        }
                        else if (c == '>')
                        {
                            moves.Add(Move.Right);
                        }
                        else 
                        {
                            moves.Add(Move.Down);
                        }
                    }
                }
                rowIterator++;
            }
            return new Room()
            {
                moves = moves,
                map = roomMap,
                RobotPosition = robotPosition,
            };
        }
    }
}
