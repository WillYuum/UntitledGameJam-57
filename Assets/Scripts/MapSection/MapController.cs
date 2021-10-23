using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;
using Utils.HelperClasses;

public class MapController : MonoBehaviourSingleton<MapController>
{
    [SerializeField] private PrefabProps platformPrefab;


    public Platform latestSpawnedPlatform { get; private set; }

    void Awake()
    {
        AssignFirstSpawnedRoad();
    }

    private void AssignFirstSpawnedRoad()
    {
        latestSpawnedPlatform = platformPrefab.nodeHolder.GetChild(0).GetComponent<Platform>();
    }

    public Vector2 GetLatestSpawnedPlatformPos() => latestSpawnedPlatform.transform.position;
    public void SpawnAnotherPlatform()
    {
        Platform platform = CreatePlatform();

        PositionPlatform(platform);

        var prevPlatform = latestSpawnedPlatform;
        Destroy(prevPlatform.gameObject, 2.0f);

        latestSpawnedPlatform = platform;
    }

    private Platform CreatePlatform()
    {
        GameObject spawnedPlatform = Instantiate(platformPrefab.prefab, platformPrefab.nodeHolder);
        return spawnedPlatform.GetComponent<Platform>();
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
