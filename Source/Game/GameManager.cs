using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using System;

public class GameManager : Script
{
    public Prefab playerPrefab;
    public static Actor Player { get; private set; }
    public static PlayerController PlayerController { get; private set; }

    public static Label DebugLabel { get; private set; }
    public static Image CrosshairUiImage { get; private set; }
    public int targetFPS = 60;

    public UIControl controlDebugLabel = null;
    public UIControl controlCrosshair = null;

    public static GameManager Instance { get; private set; }

    private static string debugText = String.Empty;
    public override void OnAwake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Actor);
        }
    }
    public override void OnStart()
    {
        Player = PrefabManager.SpawnPrefab(playerPrefab, Scene, new Transform(Vector3.Zero));
        PlayerController = Player.GetScript<PlayerController>();
        DebugLabel = controlDebugLabel.Get<Label>();
        CrosshairUiImage = controlCrosshair.Get<Image>();
    }
    public override void OnUpdate()
    {
        if (!Player)
        {
            Debug.LogError("NO PLAYER FOUND alert.mp3");
        }

        if (Time.UpdateFPS != targetFPS || Time.PhysicsFPS != targetFPS || Time.DrawFPS != targetFPS)
        {
            Time.UpdateFPS = targetFPS;
            Time.PhysicsFPS = targetFPS;
            Time.DrawFPS = targetFPS;
        }

        debugText = $"FPS: {Engine.FramesPerSecond}\n" +
        $"Player Speed: {MathF.Truncate(PlayerController.currentSpeed)}\n" +
        $"";
        DebugLabel.Text = debugText;
    }
    public static void AddDebugText(string text)
    {
        debugText += text;
        DebugLabel.Text = debugText;
    }

}
