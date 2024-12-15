using FlaxEngine;

namespace Game;

public class Entity : Script, IHittable
{
    public int Health { get; set; }

    public void Die()
    {

    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Die();
    }
}
