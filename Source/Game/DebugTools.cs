using FlaxEngine;

namespace Game;

class DebugTools : Script
{
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyboardKeys.F4))
        {
            GameManager.PlayerController.NoClip();
        }
    }
}
