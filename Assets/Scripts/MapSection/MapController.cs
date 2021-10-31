using System;
using UnityEngine;
using Utils.GenericSingletons;
using Utils.HelperClasses;

public class MapController : MonoBehaviourSingleton<MapController>
{
    public event Action<Level> onCreateNewPlatform;

    [SerializeField] private PrefabProps platformPrefab;

    [SerializeField] private PrefabProps defaultLevelPrefab;

    public Level latestSpawnedPlatform { get; private set; }


    [SerializeField] private Utils.PsuedoRandArray<Level> allLevels;

    void Start()
    {
        AssignFirstSpawnedRoad();
    }

    private void AssignFirstSpawnedRoad()
    {
        latestSpawnedPlatform = platformPrefab.nodeHolder.GetChild(0).GetComponent<Level>();
        onCreateNewPlatform?.Invoke(latestSpawnedPlatform);
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

    public void SpawnDefaultLevel()
    {
        Level defaultLevel = CreateDefaultLevel();
        PositionPlatform(defaultLevel);

        var lastLevel = latestSpawnedPlatform;
        Destroy(lastLevel.gameObject, 2.0f);

        latestSpawnedPlatform = defaultLevel;

        onCreateNewPlatform?.Invoke(latestSpawnedPlatform);
    }

    private Level SpawnNextLevel() => Instantiate(allLevels.PickNext(), defaultLevelPrefab.nodeHolder);

    private Level CreateDefaultLevel()
    {
        GameObject spawnedPlatform = Instantiate(defaultLevelPrefab.prefab, defaultLevelPrefab.nodeHolder);
        return spawnedPlatform.GetComponent<Level>();
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
