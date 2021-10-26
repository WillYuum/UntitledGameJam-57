using UnityEngine;
using Utils.GenericSingletons;
using Tutorial;


public enum GameState { Paused, Playing, Tutorial };

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    [SerializeField] private GameObject startGameNode;
    public GameState currentGameState { get; private set; }

    public bool hasPlayedTutorial = false;


    void Awake()
    {
        hasPlayedTutorial = false;
        Invoke(nameof(StartGame), 0.1f);

        MapController.instance.onCreateNewPlatform += OnStartGame;
    }

    public void StartGame()
    {
        Debug.Log("Started Game");
        startGameNode.SetActive(true);
    }


    private void OnStartGame(Platform platform)
    {
        if (hasPlayedTutorial)
        {
            //Start a level
        }
        else
        {
            TutorialManager.instance.StartTutorial(platform);
        }

        MapController.instance.onCreateNewPlatform -= OnStartGame;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

