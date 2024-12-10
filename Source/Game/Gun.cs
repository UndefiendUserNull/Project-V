using FlaxEngine;

namespace Game;

public class Gun
{
    [Tooltip("Optional")]
    public string name = "";
    [Tooltip("In ms")]
    public float fireRate = 0f;
    [Tooltip("Keep in mind, most enemies has 50 health")]
    public int damage = 0;
    [Tooltip("How far the gun can shoot")]
    public float range = 0f;
    [Tooltip("Gun's Model")]
    public Model model = null;
}