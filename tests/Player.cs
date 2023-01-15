


public class Player 
{
    private IInstaller? _installer;
    private IWeapon? _weapon;

    public Player(IInstaller installer)
    {
        _installer = installer;
        _weapon = _installer.Resolve<IWeapon>();
    }

    public void Shoot() => _weapon?.Shoot();

}