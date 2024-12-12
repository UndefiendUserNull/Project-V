using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class EmailUi : Script
{
    private UIControl _messagesTilePanel;

    private UIControl _subject;
    private UIControl _request;
    private UIControl _approveButton;

    [HideInEditor]
    public RichTextBox Subject { get; set; }
    [HideInEditor]
    public RichTextBox Request { get; set; }
    [HideInEditor]
    public Button ApproveButton { get; set; }

    public override void OnAwake()
    {
        _messagesTilePanel = Actor.GetChild("Contains").As<UIControl>();
        _subject = _messagesTilePanel.GetChild("Subject").As<UIControl>();
        _request = _messagesTilePanel.GetChild("Request").As<UIControl>();
        _approveButton = _messagesTilePanel.GetChild("Approve").As<UIControl>();
    }

    public override void OnStart()
    {
        Subject = _subject.Get<RichTextBox>();
        Request = _request.Get<RichTextBox>();
        ApproveButton = _approveButton.Get<Button>();
    }
}
