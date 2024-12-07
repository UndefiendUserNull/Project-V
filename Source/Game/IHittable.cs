using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

public interface IHittable
{

    public void Hit();
    public int Health { get; set; }
    public void Die();
}
