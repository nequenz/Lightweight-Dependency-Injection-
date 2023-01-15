
public struct ContaineredType
{
    private Type _type;
    private TypeParams _params;
    private object? _instance;

    public object? Instance => _instance;
    public Type CurrentType => _type;
    public TypeParams Params => _params;

    public ContaineredType(Type type, TypeParams typeParams)
    {
        _type = type;
        _params = typeParams;
        _instance = null;  
    }

    public void SetInstance(object? instance) => _instance = instance;
}



public class DefaultInstaller : IInstaller
{
    private Dictionary<Type, ContaineredType> _mathes = new();

    public bool Build()
    {
        return true;
    }

    public IInstaller Bind<I, T>(TypeParams typeParam) where T : class, I
    {
        Type matchedInterface = typeof(I);
        Type matchedType = typeof(T);
        ContaineredType type;

        if (_mathes.ContainsKey(matchedInterface))
            throw new ArgumentException("[" + matchedInterface.Name + "] is matched to [" + matchedType.Name + "] yet.");

        type = new ContaineredType(typeof(T), typeParam);

        _mathes.Add(typeof(I), type);

        return this;
    }

    public T? Resolve<T>()
    {
        ContaineredType type;
        object? instance = default;

        bool result = _mathes.TryGetValue( typeof(T), out type);

        if (result == false)
            throw new ArgumentNullException(typeof(T) + " has no implementation.");

        if(type.Params == TypeParams.Instance)
        {
            instance = Activator.CreateInstance(type.CurrentType);
        }
        else if(type.Params == TypeParams.Singleton)
        {
            if (type.Instance == null)
            {
                type.SetInstance(Activator.CreateInstance(type.CurrentType));
            }

            instance = (T?)type.Instance;
            _mathes.Remove(typeof(T));
            _mathes.Add(typeof(T), type);
        }

        return (T?)instance;
    }
}