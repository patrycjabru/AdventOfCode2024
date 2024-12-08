namespace AdventOfCode2024._08
{
    public class Day08 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("08");
            var inputAsArray = InputReader.ConvertInputToTwodimmensionalArray(input);

            var antennas = FindAntennas(inputAsArray);
            var antinodes = FindAntinodes(inputAsArray, antennas);

            return antinodes.Count().ToString();
        }
        public string GetSecondAnswer()
        {
            throw new NotImplementedException();
        }

        private HashSet<Point> FindAntinodes(char[,] input, Dictionary<char, List<Point>> antennas)
        {
            var result = new HashSet<Point>();

            foreach(var antennaType in antennas.Keys)
            {
                foreach (Point a1 in antennas[antennaType]) 
                {
                    foreach (Point a2 in antennas[antennaType]) // a1, a2 is and antenna pair
                    {
                        if (a1 == a2) {
                            continue;
                        }

                        var distInRows = a1.row - a2.row;
                        var distInColums = a1.column - a2.column;

                        var antinode1Row = a1.row + distInRows;
                        var antinode1Column = a1.column + distInColums;

                        var antinode2Row = a2.row - distInRows;
                        var antinode2Column = a2.column - distInColums;

                        if (antinode1Row >= 0 && antinode1Column >= 0 && antinode1Row < input.GetLength(0) && antinode1Column < input.GetLength(1))
                        {
                            result.Add(new Point(antinode1Row, antinode1Column));
                        }
                        if (antinode2Row >= 0 && antinode2Column >= 0 && antinode2Row < input.GetLength(0) && antinode2Column < input.GetLength(1))
                        {
                            result.Add(new Point(antinode2Row, antinode2Column));
                        }
                    }
                }
            }
            return result;
        }

        private Dictionary<char, List<Point>> FindAntennas(char[,] input)
        {
            var antennas = new Dictionary<char, List<Point>>();

            for (var i = 0; i < input.GetLength(0); i++)
            {
                for (var j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == '.')
                        continue;

                    if (antennas.ContainsKey(input[i, j]))
                        antennas[input[i, j]].Add(new Point(i, j));
                    else
                        antennas.Add(input[i,j], new List<Point>() {new Point(i, j) });
                }
            }

            return antennas;
        }
    }
}
