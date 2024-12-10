using FlaxEngine;

namespace Game;
public class PlayerRaycastController : Script
{
    public int damage = 15;
    public JsonAssetReference<Gun> currentGun = null;

    private float currentGunFireRate = 0f;
    private float gunFireRateTimer = 0f;

    private Ray shootRay;
    private Color shootRayColor = Color.Green;
    private IHittable storedHittable = null;
    private IInteractable storedInteractable = null;
    private RayCastHit hit;
    private Actor hitPoint;
    private Actor cameraHolder;

    public override void OnAwake()
    {
        cameraHolder = Actor.GetChild("Camera Holder");
        hitPoint = cameraHolder.GetChild("Hitpoint");
    }

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
        shootRay = new Ray(hitPoint.Position, cameraHolder.Transform.Forward);

        ResetGunSettings();
        GunFireRateTimer();

        if (Input.GetMouseButton(MouseButton.Left))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyboardKeys.E))
        {
            Interact();
        }

        DebugDraw.DrawRay(shootRay, shootRayColor);
    }
    private void Shoot()
    {
        if (gunFireRateTimer >= currentGunFireRate && storedHittable != null)
        {
            storedHittable.Hit(damage);
            gunFireRateTimer = 0f;
            DebugDraw.DrawLine(hitPoint.Position, hit.Point, Color.Red, 0.5f);
        }
    }

    private void Interact()
    {
        storedInteractable?.Interact();
    }
    public override void OnFixedUpdate()
    {
        if (Physics.RayCast(shootRay.Position, shootRay.Direction, out hit))
        {
            IHittable hittable = hit.Collider?.GetScript<IHittable>();
            IInteractable interactable = hit.Collider?.GetScript<IInteractable>();
            shootRayColor = Color.Red;
            var hitActor = hit.Collider;
            GameManager.AddDebugText($"RayHit: {hitActor.Name}");
            GameManager.crosshairUiImage.BackgroundColor = Color.Red;
            storedHittable = hittable;
            storedInteractable = interactable;
        }
        else
        {
            storedHittable = null;
            storedInteractable = null;
            shootRayColor = Color.Green;
            GameManager.crosshairUiImage.BackgroundColor = Color.White;
            GameManager.AddDebugText("RayHit: None");
        }
    }
}
