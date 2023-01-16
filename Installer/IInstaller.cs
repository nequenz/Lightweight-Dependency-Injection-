
public enum TypeParams
{
    Instance,
    Singleton
}

public interface IInstaller
{
    public IInstaller Bind<I, T>(TypeParams typeParam) where T : class, I;

    public T? Resolve<T>();

    public void SetContainer(IInstallContainer? installContainer);
}