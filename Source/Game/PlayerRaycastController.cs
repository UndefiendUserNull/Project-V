using FlaxEngine;
using System.Collections.Generic;

namespace Game;
public class PlayerRaycastController : Script
{
    public JsonAssetReference<Gun> currentGun = null;
    public List<JsonAssetReference<Gun>> guns;
    public LayersMask hittableLayers;

    private float currentGunFireRate = 0f;
    private float gunFireRateTimer = 0f;
    private float currentRange = 0f;
    private int damage = 15;
    private int currentGunIndex = 0;


    private Ray shootRay;
    private Color shootRayColor = Color.Green;
    private IHittable storedHittable = null;
    private IInteractable storedInteractable = null;
    private RayCastHit hit;
    private Actor hitPoint;
    private Actor cameraHolder;
    private StaticModel gunModel;


    public override void OnAwake()
    {
        cameraHolder = Actor.GetChild("Camera Holder");
        hitPoint = cameraHolder.GetChild("Hitpoint");
        gunModel = cameraHolder.GetChild("Gun Model").As<StaticModel>();
    }

    public override void OnStart()
    {
        currentGunFireRate = currentGun.Instance.FireRate;
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
        if (currentGunFireRate != currentGun.Instance.FireRate)
            currentGunFireRate = currentGun.Instance.FireRate;

        if (currentRange != currentGun.Instance.Range)
            currentRange = currentGun.Instance.Range;

        if (damage != currentGun.Instance.Damage)
            damage = currentGun.Instance.Damage;
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
        ChangeWeapon();
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
    private void ChangeWeapon()
    {
        if (Input.MouseScrollDelta > 0)
        {
            if (currentGunIndex < guns.Count - 1)
            {
                currentGunIndex++;
            }
            else
            {
                currentGunIndex = 0;
            }
        }

        else if (Input.MouseScrollDelta < 0)
        {
            if (currentGunIndex > 0)
            {
                currentGunIndex--;
            }
            else
            {
                currentGunIndex = guns.Count - 1;
            }
        }
        currentGun = guns[currentGunIndex];

        if (gunModel.Model != currentGun.Instance.Model)
            gunModel.Model = currentGun.Instance.Model;
    }

    private void Interact()
    {
        storedInteractable?.Interact();
    }

    public override void OnFixedUpdate()
    {
        GameManager.AddDebugText($"Current Gun: {currentGun.Instance.Name}\n");
        if (Physics.RayCast(shootRay.Position, shootRay.Direction, out hit, currentRange, hittableLayers))
        {
            IHittable hittable = hit.Collider?.GetScript<IHittable>();
            IInteractable interactable = hit.Collider?.GetScript<IInteractable>();
            shootRayColor = Color.Red;
            var hitActor = hit.Collider;
            GameManager.AddDebugText($"RayHit: {hitActor.Name}");

            if (hittable != null)
                GameManager.AddDebugText($"\nEnemy Health: {hittable.Health}");

            GameManager.CrosshairUiImage.BackgroundColor = Color.Red;
            storedHittable = hittable;
            storedInteractable = interactable;
        }
        else
        {
            storedHittable = null;
            storedInteractable = null;
            shootRayColor = Color.Green;
            GameManager.CrosshairUiImage.BackgroundColor = Color.White;
            GameManager.AddDebugText("RayHit: None");
        }
    }
}
