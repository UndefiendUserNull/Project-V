using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using System;

public class GameManager : Script
{
    public enum PlayerLevelState
    {
        MAIN_MENU,
        ROOM,
        MISSION
    }
    public Prefab playerPrefab;
    public static Actor Player { get; private set; }
    public static PlayerController PlayerController { get; private set; }
    public static MainCamera MainCamera { get; private set; }
    public static EmailUi EmailUI { get; private set; }
    public static PlayerLevelState CurrentPlayerLevelState { get; set; }
    public static Label DebugLabel { get; private set; }
    public static Image CrosshairUiImage { get; private set; }
    public static SceneReference CurrentMission { get; set; }

    public int targetFPS = 60;

    public UIControl controlDebugLabel = null;
    public UIControl controlCrosshair = null;
    public UIControl controlEmailUi = null;
    public bool spawnPlayer = true;

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
        if (spawnPlayer)
        {
            Player = PrefabManager.SpawnPrefab(playerPrefab, Scene, new Transform(Vector3.Zero));
            PlayerController = Player.GetScript<PlayerController>();
            MainCamera = Player.GetChild("Camera Holder").GetScript<MainCamera>();
        }
        DebugLabel = controlDebugLabel.Get<Label>();
        CrosshairUiImage = controlCrosshair.Get<Image>();
        EmailUI = controlEmailUi.GetScript<EmailUi>();

    }
    public override void OnUpdate()
    {
        if (!Player && spawnPlayer)
        {
            Debug.LogError("NO PLAYER FOUND alert.mp3");
            return;
        }

        if (Time.UpdateFPS != targetFPS || Time.PhysicsFPS != targetFPS || Time.DrawFPS != targetFPS)
        {
            Time.UpdateFPS = targetFPS;
            Time.PhysicsFPS = targetFPS;
            Time.DrawFPS = targetFPS;
        }

        if (Player)
        {
            debugText = $"FPS: {Engine.FramesPerSecond}\n" +
        $"Player Speed: {MathF.Truncate(PlayerController.currentSpeed)}\n" +
        $"";
            DebugLabel.Text = debugText;
        }
    }
    public static void AddDebugText(string text)
    {
        debugText += text;
        DebugLabel.Text = debugText;
    }

}
