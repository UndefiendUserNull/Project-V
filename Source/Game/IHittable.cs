namespace Game;

public interface IHittable
{

    public void Hit(int damage);
    public int Health { get; set; }
    public void Die();
}
