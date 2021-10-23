using System;
using UnityEngine;
using Utils.GenericSingletons;

public class GameLoopManager : MonoBehaviourSingleton<GameLoopManager>
{

    public void HandlePlayerOnReachEndOfPlatform()
    {
        Debug.Log("Switching to new platform");

        MapController.instance.SpawnAnotherPlatform();

        Vector2 nextCameraPos = MapController.instance.GetLatestSpawnedPlatformPos();
        CameraController.instance.MoveCameraToNextPlatform(nextCameraPos);

        Player.instance.MoveToNextPlatform();
    }



}
