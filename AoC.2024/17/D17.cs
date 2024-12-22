namespace AoC._2024;

public class D17
{
    public string PartOne(string inputPath)
    {
        (List<int> program, int registerA, int registerB, int registerC) = InputReader.ReadLines(inputPath).ExtractProgramAndRegisters();

        List<int> outputs = new();
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
                            instruction = program[instruction + 1];
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
        List<string> input = InputReader.ReadLines(inputPath);
        return 0;
    }
}


public static class D17Extensions
{
    public static int Combo(int operand, int registerA, int registerB, int registerC)
    {
        if (operand >= 0 && operand <= 3) return operand;
        if (operand == 4) return registerA;
        if (operand == 5) return registerB;
        if (operand == 6) return registerC;
        if (operand == 7) throw new NotImplementedException("Combo 7..");
        else throw new NotImplementedException("Unknown combo");
    }

    public static int Dv(int a, int combo)
    {
        return (int)(a / (Math.Pow(2, combo)));
    }
    public static int Bxl(int b, int literal)
    {
        return b ^ literal;
    }
    public static int Bxc(int b, int c)
    {
        return b ^ c;
    }
    public static int Bst(int combo)
    {
        return combo % 8;
    }

    internal static bool Jnz(int registerA)
    {
        return registerA != 0;
    }

    internal static int Out(int combo)
    {
        return combo % 8;
    }

    public static (List<int> Program, int A, int B, int C) ExtractProgramAndRegisters(this List<string> input)
    {
        List<int> program = new();
        int registerA = 0;
        int registerB = 0;
        int registerC = 0;
        foreach (var line in input)
        {
            if (line.StartsWith("Register A:"))
            {
                registerA = int.Parse(line.Split(' ')[2]);
            }
            if (line.StartsWith("Register B:"))
            {
                registerB = int.Parse(line.Split(' ')[2]);
            }
            if (line.StartsWith("Register C:"))
            {
                registerC = int.Parse(line.Split(' ')[2]);
            }
            if (line.StartsWith("Program:"))
            {
                program = line.Split(' ')[1].Split(',').Select(x => int.Parse(x)).ToList();
            }
        }
        return (program, registerA, registerB, registerC);
    }
}


