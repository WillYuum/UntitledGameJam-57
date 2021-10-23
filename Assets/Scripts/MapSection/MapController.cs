using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;
using Utils.HelperClasses;

public class MapController : MonoBehaviourSingleton<MapController>
{
    [SerializeField] private PrefabProps platformPrefab;





    private Platform latestSpawnedPlatform = null;
    private void CreatePlatform()
    {
        GameObject spawnedPlatform = Instantiate(platformPrefab.prefab, platformPrefab.nodeHolder);
        Platform platform = spawnedPlatform.GetComponent<Platform>();

        PositionPlatform(platform);

        latestSpawnedPlatform = platform;
    }

    private void PositionPlatform(Platform spawnedPlatform)
    {
        Vector2 lastPlatformPos = latestSpawnedPlatform.transform.position;
        spawnedPlatform.SetPosAfterPrevPlatform(lastPlatformPos);
    }

    public Vector2 GetGeneralPlatformSize()
    {
        return platformPrefab.prefab.GetComponent<Platform>().GetPlatformSize();
    }

}
