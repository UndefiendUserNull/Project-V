using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using System;

public class GameManager : Script
{
    public Prefab playerPrefab;
    public static Actor player;
    public static PlayerController playerController;

    public UIControl controlDebugLabel = null;
    public UIControl controlCrosshair = null;

    public static Label debugLabel = null;
    public static Image crosshairUiImage = null;

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
        player = PrefabManager.SpawnPrefab(playerPrefab, Scene, new Transform(Vector3.Zero));
        playerController = player.GetScript<PlayerController>();
        debugLabel = controlDebugLabel.Get<Label>();
        crosshairUiImage = controlCrosshair.Get<Image>();
    }
    public override void OnUpdate()
    {
        if (!player)
        {
            Debug.LogError("NO PLAYER FOUND alert.mp3");
        }
        debugText = $"FPS: {Engine.FramesPerSecond}\n" +
        $"Player Speed: {MathF.Truncate(playerController.currentSpeed)}\n" +
        $"";
        debugLabel.Text = debugText;
    }
    public static void AddDebugText(string text)
    {
        debugText += text;
        debugLabel.Text = debugText;
    }

}
