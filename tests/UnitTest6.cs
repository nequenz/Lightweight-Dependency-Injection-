public interface Foo
{
    public void TestCall();
}

public interface Boo
{
    public void TestCall();
}

public class BooImpl : Boo, IInjectable
{
    private ILogger _logger;

    public void InitDependencies(IInstaller? installer)
    {
        Console.WriteLine("logger has inited :" + installer);
        _logger = installer?.Resolve<ILogger>();
    }

    public void TestCall()
    {
        _logger.Log(99);
    }
}

public class FooImpl : Foo, IInjectable
{
    private Boo? _someImpl;

    public void InitDependencies(IInstaller? installer)
    {
        _someImpl = installer?.Resolve<Boo>();
    }

    public void TestCall()
    {
        _someImpl?.TestCall();
    }
}


public class UnitTest6 : IUnitTest
{
    public void Run()
    {
        InstallContainer? container = new InstallContainer(typeof(DefaultInstaller));

        container?.Select<BooImpl>()
            ?.Bind<ILogger, Logger>(TypeParams.Instance);

        BooImpl booImpl = container?.Build<BooImpl>();

        booImpl.TestCall();

        container?.Select<FooImpl>()
            ?.Bind<Boo, BooImpl>(TypeParams.Instance);

        FooImpl foo = container?.Build<FooImpl>();

        foo.TestCall();
    }
}