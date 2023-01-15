


public class Player : IInjectable
{
    private IWeapon? _weapon;


    public void InitDependencies(IInstaller? installer)
    {
        _weapon = installer?.Resolve<IWeapon>();
    }

    public void Shoot() => _weapon?.Shoot();

}