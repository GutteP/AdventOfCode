namespace AoC.Common;

public interface IRunner
{
    int Run(string path);
}
//public interface IRunner<T>
//{
//    T Run(string path);
//}
public class DayRunner
{
    public DayRunner(IRunner partOne, IRunner partTwo)
    {
        PartOne = partOne;
        PartTwo = partTwo;
    }
    public IRunner PartOne { get; set; }
    public IRunner PartTwo { get; set; }
}
//public class DayRunner<TReturns>
//{
//    public DayRunner(IRunner<TReturns> partOne, IRunner<TReturns> partTwo)
//    {
//        PartOne = partOne;
//        PartTwo = partTwo;
//    }
//    public IRunner<TReturns> PartOne { get; set; }
//    public IRunner<TReturns> PartTwo { get; set; }
//}
//public class Runner<TTransformed, TReturns> : IRunner<TReturns>
//{
//    private readonly Func<string, TTransformed> _tansformer;
//    private readonly Func<TTransformed, TReturns> _solver;

//    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TReturns> solver)
//    {
//        _tansformer = tansformer;
//        _solver = solver;
//    }
//    public TReturns Run(string path)
//    {
//        return _solver(_tansformer(path));
//    }
//}
public class Runner<TTransformed> : IRunner
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, int> _solver;

    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, int> solver)
    {
        _tansformer = tansformer;
        _solver = solver;
    }
    public int Run(string path)
    {
        return _solver(_tansformer(path));
    }
}
public class Runner<TTransformed, TOption> : IRunner
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, TOption, int> _solver;
    private readonly TOption _option;

    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TOption, int> solver, TOption option)
    {
        _tansformer = tansformer;
        _solver = solver;
        _option = option;
    }
    public int Run(string path)
    {
        return _solver(_tansformer(path), _option);
    }
}
public class Runner<TTransformed, TOptionOne, TOptionTwo> : IRunner
{
    private readonly Func<string, TTransformed> _tansformer;
    private readonly Func<TTransformed, TOptionOne, TOptionTwo, int> _solver;
    private readonly TOptionOne _optionOne;
    private readonly TOptionTwo _optionTwo;

    public Runner(Func<string, TTransformed> tansformer, Func<TTransformed, TOptionOne, TOptionTwo, int> solver, TOptionOne optionOne, TOptionTwo optionTwo)
    {
        _tansformer = tansformer;
        _solver = solver;
        _optionOne = optionOne;
        _optionTwo = optionTwo;
    }
    public int Run(string path)
    {
        return _solver(_tansformer(path), _optionOne, _optionTwo);
    }
}
