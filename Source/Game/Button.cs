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
        if (doorButton)
        {
            if (door)
                doorScr = door.GetScript<Door>();
            else
                Debug.LogError("No door found at " + Actor.Position);
        }
    }

    public void Interact()
    {
        if (CanInteract())
        {
            if (doorButton)
            {
                if (door)
                {
                    doorScr.Open();
                    interacted = true;
                }
                else
                    Debug.LogError("No door found at " + Actor.Position);
            }

        }
    }

    public bool CanInteract()
    {
        return !interacted;
    }
}
