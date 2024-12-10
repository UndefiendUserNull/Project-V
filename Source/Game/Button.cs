using FlaxEngine;

namespace Game;

public class Button : Script, IInteractable
{
    public bool doorButton;
    [VisibleIf("doorButton")]
    public Actor door;

    private Door doorScr;
    private bool interacted = false;

    public override void OnAwake()
    {
        if (door)
            doorScr = door.GetScript<Door>();
    }

    public void Interact()
    {
        if (CanInteract())
        {
            if (door)
                doorScr.Open();

            interacted = true;
        }
    }

    public bool CanInteract()
    {
        return !interacted;
    }
}
