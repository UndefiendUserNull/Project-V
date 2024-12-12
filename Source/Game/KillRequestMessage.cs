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
    private Button approveButton;

    public override void OnAwake()
    {
        uiControl = Actor.As<UIControl>();
    }
    public override void OnStart()
    {
        thisButton = uiControl.Get<Button>();
        approveButton = GameManager.EmailUI.ApproveButton;
    }
    public override void OnEnable()
    {
        thisButton.ButtonClicked += ThisButton_ButtonClicked;
        approveButton.ButtonClicked += ApproveButton_ButtonClicked;
    }

    public override void OnDisable()
    {
        thisButton.ButtonClicked -= ThisButton_ButtonClicked;
        approveButton.ButtonClicked -= ApproveButton_ButtonClicked;
    }
    private void ApproveButton_ButtonClicked(Button obj)
    {
        GameManager.CurrentMission = killRequest.Instance.MissionScene;
    }

    private void ThisButton_ButtonClicked(Button obj)
    {
        //Float2 measured = GameManager.EmailUI.Request.TextStyle.Font.GetFont().MeasureText(killRequest.Instance.Message);
        if (GameManager.EmailUI)
        {
            GameManager.EmailUI.Request.Text = killRequest.Instance.Message;
            GameManager.EmailUI.Subject.Text = killRequest.Instance.Subject;
        }

    }
}
