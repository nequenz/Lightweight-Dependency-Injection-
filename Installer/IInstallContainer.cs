

public interface IInstallContainer
{
    public void SelectInstallerType<T>() where T : IInstaller;

    public IInstaller? Select<ContractT>() where ContractT : IInjectable;

    public T? Build<T>() where T : IInjectable;

    public IInstaller? FindInstaller<T>();

    public IInstaller? FindInstaller(Type type);
}