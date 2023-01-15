


public class Player : IInjectable
{
    private IInstaller? _installer;
    private IWeapon? _weapon;


    public void SetInstaller(IInstaller? installer) => _installer = installer;

    public void InitDependencies()
    {
        _weapon = _installer?.Resolve<IWeapon>();
    }

    public void Shoot() => _weapon?.Shoot();

}