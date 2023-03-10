public class InstallContainer : IInstallContainer
{
    private Dictionary<Type, IInstaller?> _matches = new();
    private Type _chosenInstallerType;

    public InstallContainer(Type type)
    {
        _chosenInstallerType = type;
    }

    public void SelectInstallerType<T>() where T : IInstaller => _chosenInstallerType = typeof(T);

    public IInstaller? Select<ContractT>() where ContractT : IInjectable
    {
        Type type = typeof(ContractT);
        IInstaller? installer;

        if (_matches.ContainsKey(type))
            throw new TypeAccessException("[" + type.Name + "] has a installer.");

        installer = (IInstaller?)Activator.CreateInstance(_chosenInstallerType);
        installer?.SetContainer(this);
        _matches.Add(type, installer);

        return installer;
    }

    public T? Build<T>() where T : IInjectable
    {
        Type type = typeof(T);
        T? result = default;

        if (_matches.TryGetValue(type, out IInstaller? installer))
        {
            result = (T?)Activator.CreateInstance(type);
            result?.InitDependencies(installer);
        }

        return result;
    }

    public IInstaller? FindInstaller<T>() => _matches.GetValueOrDefault(typeof(T));

    public IInstaller? FindInstaller(Type type) => _matches.GetValueOrDefault(type);
}
