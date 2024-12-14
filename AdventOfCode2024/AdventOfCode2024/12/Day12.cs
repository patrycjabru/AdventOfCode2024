using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._12
{
    public class Day12 : IDay
    {
        private int MaxIndex = 0;
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("12");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            var nodes = MarkNodesWithRegions(inputAsArray);

            //var regions = GetRegions(nodes);
            //PrintRegions(nodes);
            var count = Count(nodes);

            return count.ToString();
        }

        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private int Count(Node[,] nodes)
        {
            var borders = new Dictionary<int, HashSet<DirectedPoint>>();
            var areaCount = new Dictionary<int, int>(); //regionalNumber - area
            for (var i = 0; i < nodes.GetLength(0); i++)
            {
                for (var j = 0; j < nodes.GetLength(1); j++)
                {
                    areaCount.TryAdd(nodes[i, j].RegionNumber, 0);
                    areaCount[nodes[i, j].RegionNumber]++;

                    borders.TryAdd(nodes[i, j].RegionNumber, new HashSet<DirectedPoint>());

                    CheckAndAddToBorders(borders, nodes, i, j, i - 1, j, 0);
                    CheckAndAddToBorders(borders, nodes, i, j, i, j - 1, 3);
                    CheckAndAddToBorders(borders, nodes, i, j, i + 1, j, 2);
                    CheckAndAddToBorders(borders, nodes, i, j, i, j + 1, 1);
                }
            }

            var sum = 0;
            for(var i = 0; i < MaxIndex; i++)
            {
                if (areaCount.ContainsKey(i))
                {
                    sum += borders[i].Count * areaCount[i];
                }
            }
            return sum;
        }

        private void CheckAndAddToBorders(Dictionary<int, HashSet<DirectedPoint>> borders, Node[,] nodes, int i, int j, int a, int b, int direction)
        {
            var border = borders[nodes[i, j].RegionNumber];
            if (a < 0 || a >= nodes.GetLength(0) || b < 0 || b >= nodes.GetLength(1))
            {
                border.Add(new DirectedPoint(a, b, direction));
                return;
            }
            if (nodes[i, j].RegionNumber != nodes[a, b].RegionNumber)
            {
                border.Add(new DirectedPoint(a, b, direction));
            }
        }

        private void CheckAndAddToPerimeterCounter(Dictionary<int, int> perimeterCount, Node[,] nodes, int i, int j, int a, int b)
        {
            if (a < 0 || a >= nodes.GetLength(0) || b < 0 || b >= nodes.GetLength(1))
            {
                perimeterCount[nodes[i, j].RegionNumber]++;
                return;
            }
            if (nodes[i, j].RegionNumber != nodes[a, b].RegionNumber)
            {
                perimeterCount[nodes[i, j].RegionNumber]++;
            }
        }

        //private Dictionary<int, List<Node>> GetRegions(Node[,] nodes)
        //{
        //    var regions = new Dictionary<int, List<Node>>();
        //    foreach (var node in nodes)
        //    {
        //        if (regions.ContainsKey(node.RegionNumber))
        //        {
        //            regions[node.RegionNumber].Add(node);
        //        }
        //        else
        //        {
        //            regions[node.RegionNumber] = new List<Node>() { node };
        //        }
        //    }
        //    return regions;
        //}

        private void PrintRegions(Node[,] nodes)
        {
            for (var i = 0; i < nodes.GetLength(0); i++)
            {
                for (var j = 0; j < nodes.GetLength(1); j++)
                {
                    Console.Write(nodes[i, j].RegionNumber);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

        private Node[,] MarkNodesWithRegions(char[,] input)
        {
            var highestRegionNumber = 0;
            var nodes = new Node[input.GetLength(0), input.GetLength(1)];

            for (var i = 0; i < input.GetLength(0); i++)
            {
                for (var j = 0; j < input.GetLength(1); j++)
                {
                    int regionNumber;

                    if (i > 0 && j > 0)
                    {
                        if (input[i - 1, j] == input[i, j] && input[i, j] == input[i, j - 1] && nodes[i - 1, j].RegionNumber != nodes[i, j - 1].RegionNumber)
                        {
                            MergeRegions(nodes, nodes[i - 1, j].RegionNumber, nodes[i, j - 1].RegionNumber);
                        }
                    }

                    if (i > 0 && input[i, j] == input[i - 1, j])
                    {
                        regionNumber = nodes[i - 1, j].RegionNumber;
                    }
                    else if (j > 0 && input[i, j] == input[i, j - 1]) 
                    {
                        regionNumber = nodes[i, j - 1].RegionNumber;
                    }
                    else
                    {
                        regionNumber = highestRegionNumber;
                        highestRegionNumber++;
                    }

                    nodes[i, j] = new Node
                    {
                        OriginalLeter = input[i, j],
                        RegionNumber = regionNumber,
                    };
                }
            }
            MaxIndex = highestRegionNumber;
            return nodes;
        }

        private void MergeRegions(Node[,] nodes, int regionNumber1, int regionNumber2)
        {
            for (var i = 0; i < nodes.GetLength(0); i++)
            {
                for (var j = 0; j < nodes.GetLength(1); j++)
                {
                    if (nodes[i,j] != null && nodes[i, j].RegionNumber == regionNumber1)
                    {
                        nodes[i, j].RegionNumber = regionNumber2;
                    }
                }
            }
        }
    }
}
