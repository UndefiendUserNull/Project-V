using FlaxEngine;

namespace Game;

public class Gun
{
    [Tooltip("In ms")]
    public float fireRate = 0f;
    [Tooltip("Keep in mind, most enemies has 50 health")]
    public int damage = 0;
}