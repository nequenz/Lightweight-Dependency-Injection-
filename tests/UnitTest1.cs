

public class UnitTest1 : IUnitTest
{

    public void Run()
    {
        DefaultInstaller playerInstaller = new DefaultInstaller();

        playerInstaller.Bind<IWeapon, RPG>(TypeParams.Instance);

        Player player = new Player();

        player.InitDependencies(playerInstaller);

        player.Shoot();

    }

}

