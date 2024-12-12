using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

public class ExitDoor : Script
{
    private SceneReference sceneToLoad;

    private Collider collider;

    public override void OnAwake()
    {
        collider = Actor.GetChild(0).As<Collider>();
    }

    public override void OnEnable()
    {
        collider.TriggerEnter += Collider_TriggerEnter;
    }

    public override void OnDisable()
    {
        collider.TriggerEnter -= Collider_TriggerEnter;
    }

    private void Collider_TriggerEnter(PhysicsColliderActor obj)
    {
        if (obj is CharacterController)
        {
            sceneToLoad = GameManager.CurrentMission;
            if (sceneToLoad.ID != Guid.Empty)
                Level.ChangeSceneAsync(sceneToLoad);
            else
                Debug.LogError("No Mission Selected");
        }
    }
}
