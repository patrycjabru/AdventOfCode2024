using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024._13
{
    public class Day13 : IDay
    {
        public string GetFirstAnswer()
        {
            var input = InputReader.ReadInput("13");

            var games = TransformInput(input);
            foreach (var game in games)
            {
                CountClicksAndTokens(game);
            }
            return games.Sum(x => x.UsedTokens).ToString();
        }

        public string GetSecondAnswer()
        {
            var input = InputReader.ReadInput("13");

            var games = TransformInput(input);
            FixConversionError(games);
            foreach (var game in games)
            {
                CountClicksAndTokens(game);
            }
            return games.Sum(x => x.UsedTokens).ToString();
        }

        private void FixConversionError(List<Game> games)
        {
            foreach (var game in games)
            {
                game.PrizeX += 10000000000000;
                game.PrizeY += 10000000000000;
            }
        }

        public void CountClicksAndTokens(Game game)
        {
            var bNumerator = game.OneAButtonPressY * game.PrizeX - game.OneAButtonPressX * game.PrizeY;
            var bDenominator = game.OneAButtonPressY * game.OneBButtonPressX - game.OneAButtonPressX * game.OneBButtonPressY;
            if (bNumerator % bDenominator != 0)
            {
                return; //no integer solution
            }
            var bClicks = bNumerator / bDenominator;

            var aNumerator = game.PrizeY - game.OneBButtonPressY * bClicks;
            var aDenumerator = game.OneAButtonPressY;
            if (aNumerator % aDenumerator != 0)
            {
                return; // no integer solution
            }
            var aClicks = aNumerator / aDenumerator;
            game.BClicks = bClicks;
            game.AClicks = aClicks;

            game.UsedTokens = 3 * aClicks + bClicks;
        }

        public List<Game> TransformInput(IEnumerable<string> input)
        {
            var iterator = 0;
            var singleGame = new Game();
            var listOfGames = new List<Game>();
            foreach (var line in input)
            {
                if (iterator == 0) //read A
                {
                    var a = line.Split(':')[1].Split(',');
                    var x = a[0].Split('+')[1].Trim();
                    var y = a[1].Split('+')[1].Trim();
                    singleGame.OneAButtonPressX = int.Parse(x);
                    singleGame.OneAButtonPressY = int.Parse(y);
                }
                else if (iterator == 1) //read B
                {
                    var b = line.Split(':')[1].Split(',');
                    var x = b[0].Split('+')[1].Trim();
                    var y = b[1].Split('+')[1].Trim();
                    singleGame.OneBButtonPressX = int.Parse(x);
                    singleGame.OneBButtonPressY = int.Parse(y);
                }
                else if (iterator == 2) // read prize
                {
                    var p = line.Split(':')[1].Split(",");
                    var x = p[0].Split("=")[1].Trim();
                    var y = p[1].Split("=")[1].Trim();
                    singleGame.PrizeX = int.Parse(x);
                    singleGame.PrizeY= int.Parse(y);
                }
                else // blank line
                {
                    listOfGames.Add(singleGame);
                    singleGame = new Game();
                }
                iterator++;
                if (iterator == 4)
                {
                    iterator = 0;
                }
            }

            if (iterator == 3)
            {
                listOfGames.Add(singleGame);
            }

            return listOfGames;
        }
    }
}
