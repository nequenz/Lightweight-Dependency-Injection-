
public class UnitTest2 : IUnitTest
{
    public void Run()
    {
        DefaultInstaller playerInstaller = new DefaultInstaller();

        playerInstaller.Bind<IWeapon, Pistol>(TypeParams.Instance);

        Player player = new Player();

        player.InitDependencies(playerInstaller);

        player.Shoot();
    }

}