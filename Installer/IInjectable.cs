

public interface IInjectable
{
    public void SetInstaller(IInstaller? installer);

    public void InitDependencies();
}