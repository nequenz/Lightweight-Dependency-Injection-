

public class UnitTest3 : IUnitTest
{

    public void Run()
    {
        DefaultInstaller playerInstaller = new DefaultInstaller();

        playerInstaller.Bind<IWeapon, RPG>(TypeParams.Instance);

        try
        {
            playerInstaller.Bind<IWeapon, Pistol>(TypeParams.Instance);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("cathed:" + ex.Message);

        }

        new Player(playerInstaller).Shoot();
    }

}

