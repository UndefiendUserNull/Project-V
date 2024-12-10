using FlaxEngine;

namespace Game;

public class Enemy : Script, IHittable
{
    public int Health { get; set; }
    public bool godMode = false;
    public void Die()
    {
        //Actor.Parent.IsActive = false;
        if (!godMode)
            Destroy(Parent);
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }
}
