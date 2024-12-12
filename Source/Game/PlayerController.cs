using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class PlayerController : Script
{
    [Header("Movement Settings")]
    public float walkSpeed = 25f;
    public float sprintSpeed = 50f;
    public float noClipSpeed = 25f;
    public float upAndDownNoClipSpeed = 25f;
    public float jumpForce = 250f;
    public float speedMultiplier = 1000f;
    public float feetRadius = 25f;
    public bool disableMovement = false;

    public LayersMask groundMask = LayersMask.Default;

    [HideInEditor]
    public float currentSpeed = 0.0f;
    [HideInEditor]
    public Camera mainCamera = null;
    [HideInEditor]
    public MainCamera mainCameraScr = null;

    private bool isGrounded = false;
    private bool isNoClipping = false;
    private const int noClipLayerIndex = 5;
    private const int playerLayerIndex = 1;

    private CharacterController controller = null;
    private Actor playerFeet = null;
    private Actor cameraHolder = null;
    private Vector3 movement = Vector3.Zero;
    private Vector2 userInput = Vector2.Zero;
    private Vector3 move = Vector3.Zero;

    public override void OnAwake()
    {
        cameraHolder = Actor.GetChild("Camera Holder");
        mainCamera = cameraHolder.GetChild<Camera>();
        playerFeet = Actor.GetChild("Feet");
        controller = Actor as CharacterController;
    }
    public override void OnStart()
    {
        mainCameraScr = cameraHolder.GetScript<MainCamera>();
    }
    private void DebugDrawing(BoundingSphere boundingSphere)
    {
        Color debugWireSphereGroundColor = isGrounded ? Color.Green : Color.Red;
        DebugDraw.DrawWireSphere(boundingSphere, debugWireSphereGroundColor);
    }
    private void UserInputCalculation()
    {
        userInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 right = mainCamera.Transform.Right;
        Vector3 forward = mainCamera.Transform.Forward;

        right.Y = 0;
        forward.Y = 0;

        right = right.Normalized;
        forward = forward.Normalized;

        move = (right * userInput.X + forward * userInput.Y).Normalized;
    }
    public void NoClip()
    {
        isNoClipping = !isNoClipping;
        if (isNoClipping)
            controller.Layer = noClipLayerIndex;
        else
            controller.Layer = playerLayerIndex;

        Debug.Log($"No Clip: {isNoClipping}");
    }
    private void PlayerSpeed()
    {
        if (!disableMovement)
            currentSpeed = Input.GetKey(KeyboardKeys.Shift) ? sprintSpeed : walkSpeed;
        else
            currentSpeed = 0;
        movement.X = move.X * currentSpeed;
        movement.Z = move.Z * currentSpeed;
    }

    public override void OnUpdate()
    {
        UserInputCalculation();
        PlayerSpeed();
        isGrounded = IsGrounded();

        DebugDrawing(new(playerFeet.Position, feetRadius));
    }
    private bool IsGrounded()
    {
        return Physics.SphereCast(playerFeet.Position, feetRadius, Vector3.Up, layerMask: groundMask);
    }
    private void Jump(float deltaTime)
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyboardKeys.Spacebar))
            {
                movement.Y = jumpForce * deltaTime;
            }
            else
            {
                movement.Y = 0;
            }
        }

    }

    public override void OnFixedUpdate()
    {
        //TODO: Fix when jump and land on edge player don't stop jumping

        float deltaTime = Time.DeltaTime * speedMultiplier;

        if (!isNoClipping)
        {
            controller.SimpleMove(movement);
        }
        else
        {
            if (Input.GetKey(KeyboardKeys.E))
                controller.Move(Vector3.Up * deltaTime * upAndDownNoClipSpeed);
            if (Input.GetKey(KeyboardKeys.Q))
                controller.Move(Vector3.Down * deltaTime * upAndDownNoClipSpeed);
            movement.Y = 0;
            controller.Move(movement * noClipSpeed * deltaTime / speedMultiplier * upAndDownNoClipSpeed);
            //controller.AddMovement(movement * deltaTime / speedMultiplier * 2);
        }

        Jump(deltaTime);

        // if player hit a ceil or something
        if (Physics.RayCast(mainCamera.Parent.Position, Vector3.Up, 2f))
        {
            movement.Y = 0;
        }
    }
}
