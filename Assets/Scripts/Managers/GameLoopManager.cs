using UnityEngine;
using Utils.GenericSingletons;
using Tutorial;

public class GameLoopManager : MonoBehaviourSingleton<GameLoopManager>
{

    public void HandlePlayerOnReachEndOfPlatform()
    {
        Debug.Log("Switching to new platform");

        if (TutorialManager.instance.tutorialIsActive)
        {
            MapController.instance.SpawnDefaultLevel();
        }
        else
        {
            MapController.instance.SpawnAnotherLevel();
        }


        Vector2 nextCameraPos = MapController.instance.GetLatestSpawnedPlatformPos();
        CameraController.instance.MoveCameraToNextPlatform(nextCameraPos);

        Player.instance.MoveToNextPlatform();
    }



}
