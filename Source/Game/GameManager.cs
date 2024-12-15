using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using System;
using System.Collections.Generic;
using System.IO;

public class GameManager : Script
{
    public enum PlayerLevelState
    {
        MAIN_MENU,
        ROOM,
        MISSION
    }
    public static Actor Player { get; private set; }
    public static PlayerController PlayerController { get; private set; }
    public static MainCamera MainCamera { get; private set; }
    public static EmailUi EmailUI { get; private set; }
    public static PlayerLevelState CurrentPlayerLevelState { get; set; } = PlayerLevelState.ROOM;
    public static Label DebugLabel { get; private set; }
    public static Image CrosshairUiImage { get; private set; }
    public static SceneReference CurrentMission { get; set; }
    public static bool MainTargetKilled { get; set; }
    public static Actor ComputerScreen { get; private set; }


    public int targetFPS = 60;

    private Prefab Pre_Player;
    private Prefab Pre_DebugUIC;
    private Prefab Pre_ControlEmailUi;
    private Prefab Pre_PC_Screen;
    private Prefab Pre_MainUI;
    private Prefab Pre_Computer;

    private UIControl controlDebugLabel;
    private UIControl controlCrosshair;
    private UIControl controlEmailUi;
    private UICanvas mainUI;

    private readonly Vector3 COMPUTER_SPAWN_POSITION = new(8.0f, -230.0f, -180.0f);
    private readonly string PREFABS_FOLDER = Path.Combine(Globals.ProjectContentFolder, "Prefabs");
    private const string PATH_PRE_PLAYER = "PLAYER.prefab";
    private const string PATH_PRE_DEBUG_UIC = "DEBUG_UIC.prefab";
    private const string PATH_PRE_EMAILUI = "EMAILUI.prefab";
    private const string PATH_PRE_PC_SCREEN = "PC_SCREEN.prefab";
    private const string PATH_PRE_MAIN_UI = "MAIN_UI.prefab";
    private const string PATH_PRE_COMPUTER = "COMPUTER.prefab";

    public static GameManager Instance { get; private set; }

    private static string debugText = String.Empty;
    private Prefab LoadPrefab(string path)
    {
        try
        {
            return Content.LoadAsync<Prefab>(Path.Combine(PREFABS_FOLDER, path));
        }
        catch
        {
            Debug.LogError($"Couldn't load {Path.Combine(PREFABS_FOLDER, path)}");
            return null;
        }
    }
    private void LoadPrefabs()
    {
        Pre_Player = LoadPrefab(PATH_PRE_PLAYER);
        Pre_DebugUIC = LoadPrefab(PATH_PRE_DEBUG_UIC);
        Pre_PC_Screen = LoadPrefab(PATH_PRE_PC_SCREEN);
        Pre_MainUI = LoadPrefab(PATH_PRE_MAIN_UI);
        Pre_ControlEmailUi = LoadPrefab(PATH_PRE_EMAILUI);
        Pre_Computer = LoadPrefab(PATH_PRE_COMPUTER);
    }

    private void Init()
    {
        Debug.Log("Game Init");

        mainUI = PrefabManager.SpawnPrefab(Pre_MainUI, Scene).As<UICanvas>();
        controlDebugLabel = PrefabManager.SpawnPrefab(Pre_DebugUIC, mainUI).As<UIControl>();

        if (CurrentPlayerLevelState == PlayerLevelState.ROOM)
        {
            ComputerScreen = PrefabManager.SpawnPrefab(Pre_PC_Screen, Scene, new Transform(Vector3.Zero));
            PrefabManager.SpawnPrefab(Pre_Computer, COMPUTER_SPAWN_POSITION);
            controlEmailUi = PrefabManager.SpawnPrefab(Pre_ControlEmailUi, ComputerScreen).As<UIControl>();
            ComputerScreen.IsActive = false;
        }

        controlCrosshair = mainUI.GetChild("CROSSHAIR").As<UIControl>();
        Player = PrefabManager.SpawnPrefab(Pre_Player, Vector3.Zero);
        PlayerController = Player.GetScript<PlayerController>();
        MainCamera = Player.GetChild("Camera Holder").GetScript<MainCamera>();

        Debug.Log("Game Init Successful");
    }

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
        LoadPrefabs();
    }


    public override void OnStart()
    {

        Init();
        DebugLabel = controlDebugLabel.Get<Label>();
        CrosshairUiImage = controlCrosshair.Get<Image>();
        if (CurrentPlayerLevelState == PlayerLevelState.ROOM)
        {
            EmailUI = controlEmailUi.GetScript<EmailUi>();
        }
    }
    public override void OnUpdate()
    {
        if (!Player)
        {
            Debug.LogError("NO PLAYER FOUND alert.mp3");
            return;
        }
        else
        {
            if (DebugLabel != null)
            {
                debugText = $"FPS: {Engine.FramesPerSecond}\n" +
        $"Player Speed: {MathF.Truncate(PlayerController.currentSpeed)}\n" +
        $"";
                DebugLabel.Text = debugText;
            }
        }

        if (Time.UpdateFPS != targetFPS || Time.PhysicsFPS != targetFPS || Time.DrawFPS != targetFPS)
        {
            Time.UpdateFPS = targetFPS;
            Time.PhysicsFPS = targetFPS;
            Time.DrawFPS = targetFPS;
        }
    }
    public void AddDebugText(string text)
    {
        if (controlDebugLabel)
        {
            debugText += text;
            DebugLabel.Text = debugText;
        }
    }

}
