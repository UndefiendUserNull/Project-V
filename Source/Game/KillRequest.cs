using FlaxEngine;

namespace Game;

public class KillRequest
{
    public string Subject { get; set; }
    [MultilineText]
    public string Message { get; set; }
    public int Reward { get; set; }
    // Audio Message
    public SceneReference MissionScene { get; set; }
}