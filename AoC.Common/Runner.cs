namespace AoC.Common;

public interface IRunner<TReturn>
{
    TReturn Run(string path);
}

public class DayRunner<TReturn>
{
    public DayRunner(IRunner<TReturn> partOne, IRunner<TReturn> partTwo)
    {
        PartOne = partOne;
        PartTwo = partTwo;
    }
    public IRunner<TReturn> PartOne { get; set; }
    public IRunner<TReturn> PartTwo { get; set; }
}

public class Runner<TTransformed, TReturn> : IRunner<TReturn>
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, TReturn> _solver;

    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TReturn> solver)
    {
        _tansformer = tansformer;
        _solver = solver;
    }
    public TReturn Run(string path)
    {
        return _solver(_tansformer(path));
    }
}
public class Runner<TTransformed, TOption, TReturn> : IRunner<TReturn>
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, TOption, TReturn> _solver;
    private readonly TOption _option;

    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TOption, TReturn> solver, TOption option)
    {
        _tansformer = tansformer;
        _solver = solver;
        _option = option;
    }
    public TReturn Run(string path)
    {
        return _solver(_tansformer(path), _option);
    }
}
public class Runner<TTransformed, TOptionOne, TOptionTwo, TReturn> : IRunner<TReturn>
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, TOptionOne, TOptionTwo, TReturn> _solver;
    private readonly TOptionOne _optionOne;
    private readonly TOptionTwo _optionTwo;

    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TOptionOne, TOptionTwo, TReturn> solver, TOptionOne optionOne, TOptionTwo optionTwo)
    {
        _tansformer = tansformer;
        _solver = solver;
        _optionOne = optionOne;
        _optionTwo = optionTwo;
    }
    public TReturn Run(string path)
    {
        return _solver(_tansformer(path), _optionOne, _optionTwo);
    }
}
public class Runner<TTransformed, TOptionOne, TOptionTwo, TOptionThree, TReturn> : IRunner<TReturn>
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, TOptionOne, TOptionTwo, TOptionThree, TReturn> _solver;
    private readonly TOptionOne _optionOne;
    private readonly TOptionTwo _optionTwo;
    private readonly TOptionThree _optionThree;


    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TOptionOne, TOptionTwo, TOptionThree, TReturn> solver, TOptionOne optionOne, TOptionTwo optionTwo, TOptionThree optionThree)
    {
        _tansformer = tansformer;
        _solver = solver;
        _optionOne = optionOne;
        _optionTwo = optionTwo;
        _optionThree = optionThree;
    }
    public TReturn Run(string path)
    {
        return _solver(_tansformer(path), _optionOne, _optionTwo, _optionThree);
    }
}
