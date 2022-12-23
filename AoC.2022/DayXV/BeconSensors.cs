using AoC.Common;
using System.Collections.Immutable;

namespace AoC._2022.DayXV
{
    public class BeconSensors : IAoCDay<double>
    {
        public DayRunner<double> Runner()
        {
            return new DayRunner<double>(new Runner<List<BeconSensor>, int, int, double>(Transformer, RowChecker, 2000000, 10), new Runner<List<BeconSensor>, int, int, double>(Transformer, DistressBeconFider, 4000000, 20));
        }

        private List<BeconSensor> Transformer(string path)
        {
            List<BeconSensor> sensors = new List<BeconSensor>();
            foreach (var row in InputReader.ReadLines(path))
            {
                var parts = row.Split(' ');
                Position<int> sensorPosition = new(int.Parse(parts[2].Split('=')[1].Trim(',')), int.Parse(parts[3].Split('=')[1].Trim(':')));
                Position<int> closestBecon = new(int.Parse(parts[8].Split('=')[1].Trim(',')), int.Parse(parts[9].Split('=')[1]));

                sensors.Add(new BeconSensor(sensorPosition, closestBecon));
            }
            return sensors;
        }

        private double RowChecker(List<BeconSensor> sensors, int rowToCheck, int rowToCheckTest)
        {
            if (sensors[0].Position.X == 2) rowToCheck = rowToCheckTest;

            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int maxRange = int.MinValue;

            foreach (BeconSensor sensor in sensors)
            {
                if (sensor.Position.X < minX) minX = sensor.Position.X;
                if (sensor.Position.X > maxX) maxX = sensor.Position.X;

                if (sensor.ClosestBecon.X < minX) minX = sensor.ClosestBecon.X;
                if (sensor.ClosestBecon.X > maxX) maxX = sensor.ClosestBecon.X;

                if (sensor.Range > maxRange) maxRange = sensor.Range;
            }
            minX -= maxRange;
            maxX += maxRange;

            int inRange = 0;
            for (int x = minX; x <= maxX; x++)
            {
                Position<int> toCheck = new(x, rowToCheck);
                if (ShouldSkip(sensors, toCheck)) continue;
                foreach (BeconSensor sensor in sensors)
                {
                    if (sensor.InRange(toCheck))
                    {
                        inRange++;
                        break;
                    }
                }
            }

            return inRange;
        }

        private double DistressBeconFider(List<BeconSensor> sensors, int max, int maxTest)
        {
            if (sensors[0].Position.X == 2) max = maxTest;

            Dictionary<int, List<(int a, int b)>> area = new();
            foreach (var s in sensors)
            {
                area = s.GetAndCombineRange(area, max);
            }
            Position<int> missing = default;
            for (int i = 0; i <= max; i++)
            {
                if (area[i].Count != 1 || area[i][0].a > 0 || area[i][0].b < max)
                {
                    var r = area[i];
                    missing = new(area[i][0].b + 1, i);
                    break;
                }
                if (area[i][0].a > 0)
                {
                    var r = area[i];
                    missing = new(0, i);
                    break;
                }
                if (area[i][0].b < max)
                {
                    var r = area[i];
                    missing = new(max, i);
                    break;
                }
            }

            return ((double)missing.X * 4000000) + missing.Y;
        }

        private bool ShouldSkip(List<BeconSensor> sensors, Position<int> toCheck)
        {
            foreach (BeconSensor sensor in sensors)
            {
                if (sensor.ClosestBecon.X == toCheck.X && sensor.ClosestBecon.Y == toCheck.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class BeconSensor
    {
        public BeconSensor(Position<int> position, Position<int> closestBecon)
        {
            Position = position;
            ClosestBecon = closestBecon;
        }

        public Position<int> Position { get; set; }
        public Position<int> ClosestBecon { get; set; }
        public int Range
        {
            get
            {
                var d = Position.Distance(ClosestBecon);
                return Math.Abs(d.X) + Math.Abs(d.Y);
            }
        }

        public Dictionary<int, List<(int a, int b)>> GetAndCombineRange(Dictionary<int, List<(int a, int b)>> area, int max)
        {
            int num = 1;
            int yFrom = Position.Y - Range;
            int yTo = Position.Y + Range;

            for (int y = yFrom; y <= yTo; y++)
            {

                if (y < 0 || y > max)
                {
                    if (y < Position.Y) num += 2;
                    else num -= 2;
                    continue;
                }
                int rStart = Position.X - (num / 2) > 0 ? Position.X - (num / 2) : 0;
                int altNum = num + rStart - 1 <= max ? num - 1 : max - rStart;
                var r = (rStart, rStart + altNum);
                if (area.ContainsKey(y))
                {
                    area[y].Add(r);
                }
                else area[y] = new() { r };

                if (y < Position.Y) num += 2;
                else num -= 2;
            }
            //Combine ranges
            for (int i = yFrom; i <= yTo; i++)
            {
                if (i < 0 || i > max || area[i].Count == 1) continue;
                area[i] = area[i].CombineRanges();
            }
            return area;
        }

        public bool InRange(Position<int> other)
        {
            var distance = Position.Distance(other);
            return Range >= (Math.Abs(distance.X) + Math.Abs(distance.Y));
        }
    }
}
