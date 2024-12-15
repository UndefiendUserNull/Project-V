using FlaxEngine;

namespace Game;

public class Computer : Script, IInteractable
{
    private Actor computerScreenUI;
    public override void OnStart()
    {
        computerScreenUI = GameManager.ComputerScreen;
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
