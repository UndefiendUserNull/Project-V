using System;
using FlaxEngine;

namespace Game;

public class Computer : Script, IInteractable
{
    private Actor cameraPos;

    public override void OnAwake()
    {
        cameraPos = Actor.GetChild("CameraPos");
    }

    public void Interact()
    {
        //GameManager.PlayerController.mainCamera.Position = cameraPos.Position;
        GameManager.PlayerController.mainCamera.Transform = cameraPos.Transform;
        GameManager.PlayerController.mainCameraScr.moveMouse = false;
        GameManager.PlayerController.mainCameraScr.showMouse = true;
    }

    public bool CanInteract()
    {
        throw new NotImplementedException();
    }
}
