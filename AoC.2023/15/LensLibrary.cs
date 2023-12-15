using AoC.Common;
using FluentAssertions.Equivalency.Steps;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AoC._2023._15
{
    public class LensLibrary : IAoCDay<int>
    {
        private const char EQ = '=';
        private const char DA = '-';
        public DayRunner<int> Runner()
        {
            return new DayRunner<int>(new Runner<List<string>, int>(Transformer, Solve), new Runner<List<string>, int>(Transformer, PartTwo));
        }

        private List<string> Transformer(string path)
        {
            List<string> steps = new();
            foreach (string s in InputReader.ReadLines(path))
            {
                steps.AddRange(s.Split(',').ToList());
            }
            return steps;
        }

        private int Solve(List<string> steps)
        {
            List<int> values = new();
            foreach (string step in steps)
            {
                int val = 0;
                foreach (char c in step)
                {
                    val += c;
                    val *= 17;
                    val %= 256;
                }
                values.Add(val);
            }

            return values.Sum();
        }

        private int PartTwo(List<string> steps)
        {
            List<LensStep> lensSteps = ExtractLensSteps(steps);

            OrderedDictionary[] boxes = new OrderedDictionary[256];
            foreach (LensStep step in lensSteps)
            {
                if (boxes[step.Box] == null) boxes[step.Box] = new();
                if (step.Operation == DA)
                {
                    if (boxes[step.Box].Contains(step.Label))
                    {
                        boxes[step.Box].Remove(step.Label);
                    }
                }
                else if (step.Operation == EQ)
                {
                    if (boxes[step.Box].Contains(step.Label))
                    {
                        boxes[step.Box][step.Label] = step.FocalLength!.Value;
                    }
                    else
                    {
                        boxes[step.Box].Add(step.Label, step.FocalLength);
                    }
                }
            }
            List<List<(string Label, int FocalLength)>> listBoxes = ToLists(boxes);
            List<int> fps = new();
            foreach ((string Label, int Box) lens in lensSteps.Select(x => (x.Label, x.Box)).Distinct())
            {
                (string Label, int FocalLength) bLens = listBoxes[lens.Box].Where(x => x.Label == lens.Label).FirstOrDefault();
                if (bLens != default)
                {
                    int index = listBoxes[lens.Box].IndexOf(bLens);
                    int fp = (lens.Box + 1) * (index + 1) * bLens.FocalLength;
                    fps.Add(fp);
                }
                else
                {

                }
            }
            return fps.Sum();
        }

        private record LensStep(string Label, int Box, char Operation, int? FocalLength);

        private List<List<(string Label, int FocalLength)>> ToLists(OrderedDictionary[] boxes)
        {
            List<List<(string Label, int FocalLength)>> result = new();
            foreach (OrderedDictionary box in boxes)
            {
                if (box == null)
                {
                    result.Add(new());
                    continue;
                }
                string[] keys = new string[box.Count];
                box.Keys.CopyTo(keys, 0);
                result.Add(keys.Select(x => (x, (int)box[x])).ToList());
            }
            return result;
        }

        private List<LensStep> ExtractLensSteps(List<string> steps)
        {
            List<LensStep> lensSteps = new();
            foreach (string step in steps)
            {
                int box = 0;
                string label = "";
                char? op = null;
                int? focalLength = null;
                foreach (char c in step)
                {
                    if (op != null)
                    {
                        focalLength = (int)char.GetNumericValue(c);
                    }
                    else if (c == EQ)
                    {
                        op = EQ;
                    }
                    else if (c == DA)
                    {
                        op = DA;
                    }
                    else
                    {
                        label += c;
                        box += c;
                        box *= 17;
                        box %= 256;
                    }
                }
                lensSteps.Add(new(label, box, op.Value, focalLength));
            }
            return lensSteps;
        }
    }
}
