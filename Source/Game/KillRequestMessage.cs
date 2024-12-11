using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;

public class KillRequestMessage : Script
{
    public JsonAssetReference<KillRequest> killRequest;
    private UIControl uiControl;
    private Button thisButton;

    public override void OnAwake()
    {
        uiControl = Actor.As<UIControl>();
    }
    public override void OnStart()
    {
        thisButton = uiControl.Get<Button>();
    }
    public override void OnUpdate()
    {
        thisButton.ButtonClicked += ThisButton_ButtonClicked;
    }

    private void ThisButton_ButtonClicked(Button obj)
    {
        //Float2 measured = GameManager.EmailUI.request.TextStyle.Font.GetFont().MeasureText(killRequest.Instance.Message);
        if (GameManager.EmailUI)
        {
            GameManager.EmailUI.request.Text = killRequest.Instance.Message;
            GameManager.EmailUI.subject.Text = killRequest.Instance.Subject;
        }

    }
}
