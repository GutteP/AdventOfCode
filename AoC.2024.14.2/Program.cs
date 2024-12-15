using AoC._2024;
using AoC.Common;

int xSize = 101;
int ySize = 103;
int maxSteps = 10000;

List<BRRobot> robots = InputReader.ReadLines("14/input.txt").MakeRobots(xSize, ySize);

long maxOutlier = 0;
int maxOutlierI = 0;
long minOutlier = long.MaxValue;
int minOutlierI = 0;


for (int i = 0; i < maxSteps; i++)
{
    robots.Step();
    long v = robots.CountAndMultiplyQuadrants(xSize, ySize);
    if (v > maxOutlier)
    {
        maxOutlier = v;
        maxOutlierI = i;
    }

    if (v < minOutlier)
    {
        minOutlier = v;
        minOutlierI = i;
    }
}

robots = InputReader.ReadLines("14/input.txt").MakeRobots(xSize, ySize);

for (int i = 0; i < maxSteps; i++)
{
    robots.Step();
    if (i == maxOutlierI || i == minOutlierI)
    {
        robots.Print(101, 103);
        Console.WriteLine(i + 1);
        Console.WriteLine("Ser du en gran?");
        Console.ReadKey();
    }
}
