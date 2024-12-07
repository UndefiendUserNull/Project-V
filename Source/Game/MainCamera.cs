using FlaxEngine;

public class MainCamera : Script
{
    public float CameraSmoothing { get; set; } = 20.0f;
    public CharacterController controller;

    private float pitch;
    private float yaw;
    private bool showMouse = false;
    public override void OnStart()
    {
        var initialEulerAngles = Actor.Orientation.EulerAngles;
        pitch = initialEulerAngles.X;
        yaw = initialEulerAngles.Y;
    }

    public override void OnUpdate()
    {
        Screen.CursorVisible = showMouse;
        Screen.CursorLock = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyboardKeys.F2))
        {
            showMouse = !showMouse;
        }

        if (showMouse)
        {
            Screen.CursorLock = CursorLockMode.None;
        }
        else
            Screen.CursorLock = CursorLockMode.Locked;

        var mouseDelta = new Float2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        pitch += mouseDelta.Y;
        yaw += mouseDelta.X;
        pitch = Mathf.Clamp(pitch + mouseDelta.Y, -88, 88);

    }

    public override void OnFixedUpdate()
    {
        var camTrans = Actor.Transform;
        var camFactor = Mathf.Saturate(CameraSmoothing * Time.DeltaTime);

        camTrans.Orientation = Quaternion.Lerp(camTrans.Orientation, Quaternion.Euler(pitch, yaw, 0), camFactor);

        Actor.Transform = camTrans;
    }
}
