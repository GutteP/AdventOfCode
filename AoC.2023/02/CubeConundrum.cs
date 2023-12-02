using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC._2023._02;

public class CubeConundrum : IAoCDay<int>
{
    //only 12 red cubes, 13 green cubes, and 14 blue cubes
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<CubeGame>, int, int, int, int>(MaxGames, SumOfPossibleGames, 12, 13, 14), new Runner<List<CubeGame>, int>(MaxGames, PowerOfMinimumGames));
    }

    private List<CubeGame> MaxGames(string path)
    {

        List<CubeGame> maxGames = new();
        foreach (string game in InputReader.ReadLines(path))
        {
            var numberAndFists = game.SplitOn(Seperator.Colon);
            int gameNumber = int.Parse(numberAndFists[0].SplitOn(Seperator.Space)[1]);
            var fists = numberAndFists[1].SplitOn(Seperator.Semicolon);
            int maxRed = 0, maxGreen = 0, maxBlue = 0;
            foreach (var fist in fists)
            {
                var cubes = fist.SplitOn(Seperator.Comma);
                foreach (var cubesWithColor in cubes)
                {
                    var countAndColor = cubesWithColor.Trim().SplitOn(Seperator.Space);
                    switch (countAndColor[1])
                    {
                        case "red":
                            int redCount = int.Parse(countAndColor[0]);
                            if (redCount > maxRed) maxRed = redCount;
                            break;
                        case "green":
                            int greenCount = int.Parse(countAndColor[0]);
                            if (greenCount > maxGreen) maxGreen = greenCount;
                            break;
                        case "blue":
                            int blueCount = int.Parse(countAndColor[0]);
                            if (blueCount > maxBlue) maxBlue = blueCount;
                            break;
                        default:
                            throw new Exception("Oj då, det var en okänd färg...");
                    }
                }
            }
            maxGames.Add(new CubeGame(gameNumber, maxRed, maxGreen, maxBlue));
        }
        return maxGames;
    }

    public int SumOfPossibleGames(List<CubeGame> maxGames, int red, int green, int blue)
    {
        return maxGames.Where(x => x.MaxRed <= red && x.MaxGreen <= green && x.MaxBlue <= blue).Sum(x => x.Id);
    }

    public int PowerOfMinimumGames(List<CubeGame> maxGames)
    {
        return maxGames.Select(x => x.MaxRed * x.MaxGreen * x.MaxBlue).Sum();
    }

    public record CubeGame(int Id, int MaxRed, int MaxGreen, int MaxBlue);
}
