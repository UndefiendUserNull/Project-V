using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class DebugText : Script
{
    public UIControl generalUiLabel = null;

    public Label _generalUiLabel = null;

    public void AddText(string text)
    {
        _generalUiLabel.Text += text;
    }
}
