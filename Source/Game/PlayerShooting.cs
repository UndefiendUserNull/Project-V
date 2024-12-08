using FlaxEngine;

namespace Game;
public class PlayerShooting : Script
{
    public int damage = 15;
    public JsonAssetReference<Gun> currentGun = null;

    private float currentGunFireRate = 0f;
    private float gunFireRateTimer = 0f;

    private Ray shootRay;
    private Color shootRayColor = Color.Green;
    private IHittable storedHittable = null;
    private RayCastHit hit;


    public override void OnStart()
    {
        currentGunFireRate = currentGun.Instance.fireRate;
    }

    private void GunFireRateTimer()
    {
        if (gunFireRateTimer < currentGunFireRate)
        {
            gunFireRateTimer += Time.DeltaTime;
        }
    }
    private void ResetGunSettings()
    {
        if (currentGunFireRate != currentGun.Instance.fireRate)
            currentGunFireRate = currentGun.Instance.fireRate;

        if (damage != currentGun.Instance.damage)
            damage = currentGun.Instance.damage;
    }
    public override void OnUpdate()
    {
        shootRay = new Ray(Actor.Position, Actor.Transform.Forward);

        ResetGunSettings();
        GunFireRateTimer();

        if (Input.GetMouseButton(MouseButton.Left))
        {
            Shoot();
        }

        Debug.Log(gunFireRateTimer);
        DebugDraw.DrawRay(shootRay, shootRayColor);
    }
    private void Shoot()
    {
        if (gunFireRateTimer >= currentGunFireRate)
        {
            storedHittable?.Hit(damage);
            gunFireRateTimer = 0f;
            DebugDraw.DrawLine(Actor.Position, hit.Point, Color.Red, 0.5f);
        }
    }

    public override void OnFixedUpdate()
    {
        Physics.RayCast(shootRay.Position, shootRay.Direction, out hit);
        var hittable = hit.Collider?.GetScript<IHittable>();

        if (hittable != null)
        {
            shootRayColor = Color.Red;
            var hitActor = hit.Collider;
            GameManager.AddDebugText($"RayHit: {hitActor.Name}");
            GameManager.crosshairUiImage.BackgroundColor = Color.Red;
            storedHittable = hittable;
        }
        else
        {
            storedHittable = null;
            shootRayColor = Color.Green;
            GameManager.crosshairUiImage.BackgroundColor = Color.White;
            GameManager.AddDebugText("RayHit: None");
        }
    }
}
