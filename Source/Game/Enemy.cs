using FlaxEngine;

namespace Game;

public class Enemy : Script, IHittable
{
    public int Health { get; set; }

    public void Die()
    {
        Actor.IsActive = false;
    }

    public void Hit()
    {
        Health--;
        if (Health <= 0)
            Die();
    }
}
