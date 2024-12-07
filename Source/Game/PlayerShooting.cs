using FlaxEngine;

namespace Game;
//TODO: Use gun's damage and firerate
public class PlayerShooting : Script
{
    public int damage = 15;
    public JsonAssetReference<Gun> currentGun = null;
    private Ray shootRay;
    private Color rayColor = Color.Green;
    private IHittable storedHittable = null;
    private RayCastHit hit;
    public override void OnUpdate()
    {
        shootRay = new Ray(Actor.Position, Actor.Transform.Forward);
        if (Input.GetMouseButton(MouseButton.Left))
        {
            Shoot();
        }
        DebugDraw.DrawRay(shootRay, rayColor);
    }
    private void Shoot()
    {
        DebugDraw.DrawLine(Actor.Position, hit.Point, Color.Red, 0.5f);
        storedHittable?.Hit(damage);
    }

    public override void OnFixedUpdate()
    {
        Physics.RayCast(shootRay.Position, shootRay.Direction, out hit);
        var hittable = hit.Collider?.GetScript<IHittable>();
        if (hittable != null)
        {
            rayColor = Color.Red;
            var hitActor = hit.Collider;
            GameManager.AddDebugText($"RayHit: {hitActor.Name}");
            GameManager.crosshairUiImage.BackgroundColor = Color.Red;
            storedHittable = hittable;
        }
        else
        {
            storedHittable = null;
            rayColor = Color.Green;
            GameManager.crosshairUiImage.BackgroundColor = Color.White;
            GameManager.AddDebugText("RayHit: None");
        }
    }
}
