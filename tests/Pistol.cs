
public interface IWeapon
{
    public void Shoot();
}

public class Pistol : IWeapon
{
    public Pistol()
    {

    }

    public void Shoot()
    {
        Console.WriteLine("Pistol");
    }

}

public class RPG : IWeapon
{
    public void Shoot()
    {
        Console.WriteLine("RPG");
    }
}