
public interface ISingleton
{
    public int Value { get; set; }

    public void ShowValue();
}

public class MainService : ISingleton
{
    private int _value = -99;

    public int Value 
    { 
        get => _value;
        set => _value = value;
    }

    public void ShowValue()
    {
        Console.WriteLine("Value is " + _value);
    }
}



public class UnitTest4 : IUnitTest
{
    public void Run()
    {
        DefaultInstaller installer = new DefaultInstaller();

        ISingleton s = installer.Bind<ISingleton, MainService>(TypeParams.Singleton).Resolve<ISingleton>();

        s.Value = 45;
        s.ShowValue();

        installer.Resolve<ISingleton>().ShowValue();

    }
}