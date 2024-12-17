using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FlaxEngine;

namespace Game;

public class DebugConsole : Script
{
    private List<SceneReference> scenesFound;

    public void Execute(string funcName, string[] @params)
    {
        MethodInfo method = typeof(DebugConsole).GetMethod(funcName, BindingFlags.Public);
        method.Invoke(this, @params);
    }

    public void LoadLevel(string level)
    {
        var filesFound = Directory.GetFiles(GameManager.SCENES_FOLDER, "*.scene", SearchOption.AllDirectories);

        foreach (var file in filesFound)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);

            if (fileName.Equals(level, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"Loading scene: {file}");

                // Load synchronously to test stability
                var sceneAsset = Content.Load<SceneAsset>(file);

                if (sceneAsset != null)
                {
                    Debug.Log($"Scene '{level}' loaded. ID: {sceneAsset.ID}");
                    Level.LoadSceneAsync(sceneAsset.ID);
                    return;
                }
                else
                {
                    Debug.LogError($"Failed to load SceneAsset: {file}");
                    return;
                }
            }
        }

        Debug.LogError($"Scene '{level}' not found in {GameManager.SCENES_FOLDER}.");
    }

    public override void OnStart()
    {
        LoadLevel("Playground");
    }
}
