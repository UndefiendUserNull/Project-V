using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class EmailUi : Script
{
    private UIControl messagesTilePanel;

    private UIControl _subject;
    private UIControl _request;
    private UIControl _approveButton;

    [HideInEditor]
    public RichTextBox subject;
    [HideInEditor]
    public RichTextBox request;
    [HideInEditor]
    public Button approveButton;

    public override void OnAwake()
    {
        messagesTilePanel = Actor.GetChild("Contains").As<UIControl>();
        _subject = messagesTilePanel.GetChild("Subject").As<UIControl>();
        _request = messagesTilePanel.GetChild("Request").As<UIControl>();
        _approveButton = messagesTilePanel.GetChild("Approve").As<UIControl>();
    }

    public override void OnStart()
    {
        subject = _subject.Get<RichTextBox>();
        request = _request.Get<RichTextBox>();
        approveButton = _approveButton.Get<Button>();
        Debug.Log(_subject);
        Debug.Log(request);
    }
}
