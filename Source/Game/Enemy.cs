using FlaxEngine;

namespace Game;

public class Enemy : Script, IHittable
{
    public int Health { get; set; }

    public void Die()
    {
        //Actor.Parent.IsActive = false;
        Destroy(Actor.Parent);
    }

    public void Hit(int damage)
    {
        Health--;
        if (Health <= 0)
            Die();
    }
}
