public interface IRandomizer
{
    public int GetRandomValue();
}

public interface ILogger
{
    public void Log(int value);
}

public class Logger : ILogger
{
    public void Log( int value )
    {
        Console.WriteLine("Value is " + value);
    }
}

public class Randimizer3 : IRandomizer
{
    public int GetRandomValue()
    {
        /* yep */
        return 3;
    }
}

public class Randimizer2 : IRandomizer
{
    public int GetRandomValue()
    {
        /* yep */
        return 2;
    }
}


public class RandomValueLogger : IInjectable
{
    ILogger? _logger;
    IRandomizer? _randomizer;

    public void ShowRandomValue() => _logger?.Log( _randomizer.GetRandomValue() );

    public void InitDependencies(IInstaller? installer)
    {
        _logger = installer?.Resolve<ILogger>();
        _randomizer = installer?.Resolve<IRandomizer>();

        Console.WriteLine(_logger + ", " + _randomizer + ", "+ installer);
    }

}


public class UnitTest5 : IUnitTest
{
    public void Run()
    {
        InstallContainer? container = new(typeof(DefaultInstaller));

        container?.Select<RandomValueLogger>()
            ?.Bind<ILogger, Logger>(TypeParams.Instance)
            .Bind<IRandomizer, Randimizer2>(TypeParams.Instance);

        RandomValueLogger? randomValueLogger = container?.Build<RandomValueLogger>();

        randomValueLogger?.ShowRandomValue();
    }
}
    