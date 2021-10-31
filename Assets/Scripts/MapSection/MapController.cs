using System;
using UnityEngine;
using Utils.GenericSingletons;
using Utils.HelperClasses;

public class MapController : MonoBehaviourSingleton<MapController>
{
    public event Action<Level> onCreateNewPlatform;

    [SerializeField] private PrefabProps platformPrefab;


    public Level latestSpawnedPlatform { get; private set; }


    [SerializeField] private Utils.PsuedoRandArray<Level> allLevels;

    void Awake()
    {
        AssignFirstSpawnedRoad();
    }

    private void AssignFirstSpawnedRoad()
    {
        latestSpawnedPlatform = platformPrefab.nodeHolder.GetChild(0).GetComponent<Level>();
    }

    public Vector2 GetLatestSpawnedPlatformPos() => latestSpawnedPlatform.transform.position;
    public void SpawnAnotherLevel()
    {
        Level level = SpawnNextLevel();

        PositionPlatform(level);

        var lastLevel = latestSpawnedPlatform;
        Destroy(lastLevel.gameObject, 2.0f);

        latestSpawnedPlatform = level;

        onCreateNewPlatform?.Invoke(latestSpawnedPlatform);
    }

    private Level SpawnNextLevel() => Instantiate(allLevels.PickNext());

    private Platform CreatePlatform()
    {
        GameObject spawnedPlatform = Instantiate(platformPrefab.prefab, platformPrefab.nodeHolder);
        return spawnedPlatform.GetComponent<Platform>();
    }

    private void PositionPlatform(Level level)
    {
        Vector2 lastLevelPos = latestSpawnedPlatform.transform.position;
        level.SetPosAfterPrevLevel(lastLevelPos);
    }

    public Vector2 GetGeneralPlatformSize()
    {
        return platformPrefab.prefab.GetComponent<Platform>().GetPlatformSize();
    }

}
