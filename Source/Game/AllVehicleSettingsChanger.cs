using FlaxEngine;

namespace Game;

public class AllVehicleSettingsChanger : Script
{
    [EditorDisplay("General")]
    public float mass = 20f;
    [EditorDisplay("General")]
    public float radius = 28f;
    [EditorDisplay("General")]
    public float width = 20f;

    [EditorDisplay("Steering")]
    public float maxSteerAngle = 70f;
    [EditorDisplay("Steering")]
    public float dampingRate = 0.25f;
    [EditorDisplay("Steering")]
    public float maxBrakeTorque = 1500f;
    [EditorDisplay("Steering")]
    public float maxHandBrakeTorque = 2000f;

    [EditorDisplay("Suspension")]
    public float sprungMassMultiplier = 1f;
    [EditorDisplay("Suspension")]
    public float suspensionDampingRate = 1f;
    [EditorDisplay("Suspension")]
    public float suspensionMaxRaise = 10f;
    [EditorDisplay("Suspension")]
    public float suspensionMaxDrop = 10f;
    [EditorDisplay("Suspension")]
    public float suspensionForceOffset = 0f;

    [EditorDisplay("Tire")]
    public float tireLateralStiffness = 17f;
    [EditorDisplay("Tire")]
    public float tireLateralMax = 2f;
    [EditorDisplay("Tire")]
    public float tireLongitudinalStiffness = 1000f;
    [EditorDisplay("Tire")]
    public float tireFrictionScale = 1f;

    [EditorDisplay("Engine")]
    public float moi = 1f;
    [EditorDisplay("Engine")]
    public float maxTorque = 1000f;
    [EditorDisplay("Engine")]
    public float maxRotationSpeed = 6000f;

    private WheeledVehicle vehicle;

    private void Apply()
    {
        for (int i = 0; i < vehicle.Wheels.Length; i++)
        {
            var wheel = vehicle.Wheels[i];
            wheel.TireFrictionScale = tireFrictionScale;
            wheel.MaxSteerAngle = maxSteerAngle;
            wheel.DampingRate = dampingRate;
            wheel.MaxBrakeTorque = maxBrakeTorque;
            wheel.MaxHandBrakeTorque = maxHandBrakeTorque;
            wheel.SprungMassMultiplier = sprungMassMultiplier;
            wheel.SuspensionDampingRate = suspensionDampingRate;
            wheel.SuspensionMaxRaise = suspensionMaxRaise;
            wheel.SuspensionMaxDrop = suspensionMaxDrop;
            wheel.SuspensionForceOffset = suspensionForceOffset;
            wheel.TireLateralStiffness = tireLateralStiffness;
            wheel.TireLateralMax = tireLateralMax;
            wheel.TireLongitudinalStiffness = tireLongitudinalStiffness;
            Debug.Log(wheel.TireFrictionScale);
        }
        vehicle.Setup();
    }

    public override void OnAwake()
    {
        vehicle = Actor as WheeledVehicle;
    }

}
