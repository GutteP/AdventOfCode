namespace AoC._2024;

public class D17
{
    public string PartOne(string inputPath)
    {
        (List<long> program, long registerA, long registerB, long registerC) = InputReader.ReadLines(inputPath).ExtractProgramAndRegisters();

        List<long> outputs = new();
        int instruction = 0;
        try
        {
            while (instruction < program.Count)
            {
                switch (program[instruction])
                {
                    case 0:
                        registerA = D17Extensions.Dv(registerA, D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)); // Adv
                        instruction += 2;
                        break;
                    case 1:
                        registerB = D17Extensions.Bxl(registerB, program[instruction + 1]);
                        instruction += 2;
                        break;
                    case 2:
                        registerB = D17Extensions.Bst(D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC));
                        instruction += 2;
                        break;
                    case 3:
                        if (D17Extensions.Jnz(registerA))
                        {
                            instruction = (int)program[instruction + 1];
                        }
                        else instruction += 2;
                        break;
                    case 4:
                        registerB = D17Extensions.Bxc(registerB, registerC);
                        instruction += 2;
                        break;
                    case 5:
                        outputs.Add(D17Extensions.Out(D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)));
                        instruction += 2;
                        break;
                    case 6:
                        registerB = D17Extensions.Dv(registerA, D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)); //Bdv
                        instruction += 2;
                        break;
                    case 7:
                        registerC = D17Extensions.Dv(registerA, D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)); //Cdv
                        instruction += 2;
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception e)
        {
            // done
        }


        string result = string.Join(',', outputs);
        return result;
    }


    public long? PartTwo(string inputPath)
    {
        (List<long> program, long registerA, long registerB, long registerC) = InputReader.ReadLines(inputPath).ExtractProgramAndRegisters();
        List<long> outputs = new();
        long oB = registerB;
        long oC = registerC;

        var from = Math.Pow(8, program.Count - 1);
        var to = Math.Pow(8, program.Count);
        //var to = 181473076710656;
        //var to = 140737486710656;


        //var from = Math.Pow(8, 3);
        //var to = Math.Pow(8, 4);


        long stepSize = 100000000;
        int lookingForIndex = program.Count - 1;
        Dictionary<string, List<long>> countsOfOutputs = new();
        for (long i = (long)from; i <= to; i += stepSize)
        {
            registerA = i;
            registerB = oB;
            registerC = oC;
            outputs.Clear();
            int instruction = 0;
            try
            {
                while (instruction < program.Count)
                {
                    switch (program[instruction])
                    {
                        case 0:
                            registerA = D17Extensions.Dv(registerA, D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)); // Adv
                            instruction += 2;
                            break;
                        case 1:
                            registerB = D17Extensions.Bxl(registerB, program[instruction + 1]);
                            instruction += 2;
                            break;
                        case 2:
                            registerB = D17Extensions.Bst(D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC));
                            instruction += 2;
                            break;
                        case 3:
                            if (D17Extensions.Jnz(registerA))
                            {
                                instruction = (int)program[instruction + 1];
                            }
                            else instruction += 2;
                            break;
                        case 4:
                            registerB = D17Extensions.Bxc(registerB, registerC);
                            instruction += 2;
                            break;
                        case 5:
                            outputs.Add(D17Extensions.Out(D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)));
                            instruction += 2;
                            break;
                        case 6:
                            registerB = D17Extensions.Dv(registerA, D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)); //Bdv
                            instruction += 2;
                            break;
                        case 7:
                            registerC = D17Extensions.Dv(registerA, D17Extensions.Combo(program[instruction + 1], registerA, registerB, registerC)); //Cdv
                            instruction += 2;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                // done
            }
            if (outputs.SequenceEqual(program))
            {
                return i;
            }
            if (lookingForIndex == 0)
            {
                string o = string.Join(',', outputs);
                if (countsOfOutputs.ContainsKey(o))
                {
                    countsOfOutputs[o].Add(i);
                }
                else
                {
                    countsOfOutputs.Add(o, new List<long> { i });
                }
            }
            if (lookingForIndex > 0 && outputs.Count == program.Count && outputs[lookingForIndex] == program[lookingForIndex])
            {
                bool dont = false;
                for (int j = lookingForIndex; j <= program.Count - 1; j++)
                {
                    if (outputs[j] != program[j])
                    {
                        dont = true;
                    }
                }
                if (!dont)
                {
                    string o = string.Join(',', outputs);
                    if (countsOfOutputs.ContainsKey(o))
                    {
                        countsOfOutputs[o].Add(i);
                    }
                    else
                    {
                        countsOfOutputs.Add(o, new List<long> { i });
                    }
                    i -= stepSize * 10;
                    stepSize = stepSize / 10;
                    if (stepSize == 1)
                    {
                        lookingForIndex = 0;
                    }
                    else lookingForIndex--;
                }
            }
        }

        throw new Exception("No solution found");
    }
}


public static class D17Extensions
{
    public static long Combo(long operand, long registerA, long registerB, long registerC)
    {
        if (operand >= 0 && operand <= 3) return operand;
        if (operand == 4) return registerA;
        if (operand == 5) return registerB;
        if (operand == 6) return registerC;
        if (operand == 7) throw new NotImplementedException("Combo 7..");
        else throw new NotImplementedException("Unknown combo");
    }

    public static long Dv(long a, long combo)
    {
        return (long)(a / (Math.Pow(2, combo)));
    }
    public static long Bxl(long b, long literal)
    {
        return b ^ literal;
    }
    public static long Bxc(long b, long c)
    {
        return b ^ c;
    }
    public static long Bst(long combo)
    {
        return combo % 8;
    }

    internal static bool Jnz(long registerA)
    {
        return registerA != 0;
    }

    internal static long Out(long combo)
    {
        return combo % 8;
    }

    public static (List<long> Program, long A, long B, long C) ExtractProgramAndRegisters(this List<string> input)
    {
        List<long> program = new();
        long registerA = 0;
        long registerB = 0;
        long registerC = 0;
        foreach (var line in input)
        {
            if (line.StartsWith("Register A:"))
            {
                registerA = long.Parse(line.Split(' ')[2]);
            }
            if (line.StartsWith("Register B:"))
            {
                registerB = long.Parse(line.Split(' ')[2]);
            }
            if (line.StartsWith("Register C:"))
            {
                registerC = long.Parse(line.Split(' ')[2]);
            }
            if (line.StartsWith("Program:"))
            {
                program = line.Split(' ')[1].Split(',').Select(x => long.Parse(x)).ToList();
            }
        }
        return (program, registerA, registerB, registerC);
    }
}


