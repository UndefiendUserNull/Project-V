using FlaxEngine;

namespace Game;

public class MainTarget : Script, IHittable
{
    public int Health { get; set; }

    public void Die()
    {
        GameManager.MainTargetKilled = true;
        Destroy(Actor);
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Die();
    }
}
