
public struct ImplementatedType
{
    private Type _type;
    private TypeParams _params;
    private object? _instance;

    public object? Instance => _instance;
    public Type ImplementationType => _type;
    public TypeParams Params => _params;

    public ImplementatedType(Type type, TypeParams typeParams)
    {
        _type = type;
        _params = typeParams;
        _instance = null;  
    }

    public void SetInstance(object? instance) => _instance = instance;
}



public class DefaultInstaller : IInstaller
{
    private IInstallContainer? _container;
    private Dictionary<Type, ImplementatedType> _matches = new();

    public bool Build()
    {
        return true;
    }

    public IInstaller Bind<I, T>(TypeParams typeParam) where T : class, I
    {
        Type matchedInterface = typeof(I);
        Type matchedType = typeof(T);
        ImplementatedType type;

        if (_matches.ContainsKey(matchedInterface))
            throw new TypeAccessException("[" + matchedInterface.Name + "] is matched to [" + matchedType.Name + "] yet.");

        type = new ImplementatedType(typeof(T), typeParam);

        _matches.Add(typeof(I), type);

        return this;
    }

    public T? Resolve<T>()
    {
        ImplementatedType implementated;
        object? instance = default;

        bool result = _matches.TryGetValue( typeof(T), out implementated);

        if (result == false)
            throw new ArgumentNullException(typeof(T) + " has no implementation.");

        if (implementated.Params == TypeParams.Instance)
        {
            instance = Activator.CreateInstance(implementated.ImplementationType);
        }
        else if(implementated.Params == TypeParams.Singleton)
        {
            if (implementated.Instance is null)
                instance = Activator.CreateInstance(implementated.ImplementationType);

            implementated.SetInstance(instance);
            _matches.Remove(typeof(T));
            _matches.Add(typeof(T), implementated);
        }


        if(instance is not null 
            && implementated.ImplementationType.GetInterfaces().Contains(typeof(IInjectable)))
        {
            IInstaller? installer;

            if (_container is null)
                throw new ArgumentException("["+instance + "] has own dependencies but this installer has no assigned container to find them." +
                    "Please assign container that can solve needed dependencies.");

            installer = _container.FindInstaller(implementated.ImplementationType);

            if (installer is null)
                throw new ArgumentException("Assigned container has no an installer for [" + implementated.ImplementationType.Name + "]");

            try
            {
                ((IInjectable)instance)?.InitDependencies(installer);
            }
            catch (ArgumentNullException exc)
            {
                Console.WriteLine(exc.Message + "\n" + exc.StackTrace);

                if (implementated.Instance is not null)
                {
                    implementated.SetInstance(null);
                    _matches.Remove(typeof(T));
                    _matches.Add(typeof(T), implementated);
                }
            }
        }

        return (T?)instance;
    }

    public void SetContainer(IInstallContainer? installContainer) => _container = installContainer;
}