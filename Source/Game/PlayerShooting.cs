using FlaxEngine;

namespace Game;

public class PlayerShooting : Script
{
    public int damage = 15;
    private Ray shootRay;
    private Color rayColor = Color.Green;

    public override void OnUpdate()
    {
        shootRay = new Ray(Actor.Position, Actor.Transform.Forward);
        DebugDraw.DrawRay(shootRay, rayColor);
    }
    public override void OnFixedUpdate()
    {
        Physics.RayCast(shootRay.Position, shootRay.Direction, out RayCastHit hit);
        if (hit.Collider)
        {
            rayColor = Color.Red;
            var hitActor = hit.Collider;
            GameManager.AddDebugText($"RayHit: {hitActor.Name}");
            GameManager.crosshairUiImage.BackgroundColor = Color.Red;
        }
        else
        {
            rayColor = Color.Green;
            GameManager.crosshairUiImage.BackgroundColor = Color.White;
            GameManager.AddDebugText("RayHit: None");
        }
    }
}
