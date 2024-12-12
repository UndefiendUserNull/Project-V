using FlaxEngine;

namespace Game;

public class Computer : Script, IInteractable
{
    public Actor computerScreenUI;
    public override void OnStart()
    {
        computerScreenUI.IsActive = false;
    }

    public void Interact()
    {
        computerScreenUI.IsActive = true;
        GameManager.PlayerController.disableMovement = true;
        GameManager.MainCamera.rotate = false;
        GameManager.MainCamera.showMouse = true;
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyboardKeys.Q))
        {
            computerScreenUI.IsActive = false;
            GameManager.PlayerController.disableMovement = false;
            GameManager.MainCamera.rotate = true;
            GameManager.MainCamera.showMouse = false;
        }
    }
    public bool CanInteract()
    {
        return true;
    }
}
