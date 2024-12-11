using FlaxEngine;

namespace Game;

public class Gun
{
    [Tooltip("Optional")]
    public string Name { get; set; } = "";
    [Tooltip("In ms")]
    public float FireRate { get; set; } = 0f;
    [Tooltip("Keep in mind, most enemies has 50 health")]
    public int Damage { get; set; } = 0;
    [Tooltip("How far the gun can shoot")]
    public float Range { get; set; } = 0f;
    [Tooltip("Gun's Model")]
    public Model Model { get; set; } = null;
}