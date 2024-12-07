using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class PlayerController : Script
{
    public float walkSpeed = 25f;
    public float sprintSpeed = 50f;
    public float jumpForce = 250f;
    public float speedMultiplier = 1000f;
    public float feetRadius = 25f;
    [HideInEditor]
    public float currentSpeed = 0.0f;
    public LayersMask groundMask;
    public UIControl Debug_CurrentSpeed = null;

    private bool isGrounded = false;

    private CharacterController controller = null;
    private Camera mainCamera = null;
    private Label DebugLabel_CurrentSpeed = null;
    private Actor playerFeet = null;

    private Vector3 movement = Vector3.Zero;
    private Vector2 userInput = Vector2.Zero;
    private Vector3 move = Vector3.Zero;

    public override void OnAwake()
    {
        controller = Actor as CharacterController;
        mainCamera = Actor.GetChild("Camera Holder").GetChild<Camera>() as Camera;
        playerFeet = Actor.GetChild("Feet");
        DebugLabel_CurrentSpeed = Debug_CurrentSpeed.Get<Label>();
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
    private void PlayerSpeed()
    {
        currentSpeed = Input.GetKey(KeyboardKeys.Shift) ? sprintSpeed : walkSpeed;

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
    private void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyboardKeys.Spacebar))
            {
                movement.Y = jumpForce;
            }
            else
            {
                movement.Y = 0;
            }
        }
    }
    public override void OnFixedUpdate()
    {
        float deltaTime = Time.DeltaTime * speedMultiplier;

        controller.SimpleMove(movement * deltaTime);
        Jump();

        // if player hit a ceil or something
        if (Physics.RayCast(mainCamera.Parent.Position, Vector3.Up, 2f))
        {
            movement.Y = 0;
        }
    }
}
