using UnityEngine;
using Utils.GenericSingletons;
using Tutorial;
using UnityEngine.SceneManagement;


public enum GameState { Paused, Playing, Tutorial };

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public GameState currentGameState { get; private set; }

    public bool hasPlayedTutorial = false;

    public bool GameIsOn { get; private set; }

    void Awake()
    {
        hasPlayedTutorial = false;
        // Invoke(nameof(StartGame), 0.1f);

        MapController.instance.onCreateNewPlatform += OnStartGame;
    }

    // public void StartGame()
    // {
    //     Debug.Log("Started Game");
    // }


    private void OnStartGame(Level level)
    {
        GameIsOn = true;
        if (hasPlayedTutorial)
        {
            //Start a level
        }
        else
        {
            TutorialManager.instance.StartTutorial(level);
        }

        MapController.instance.onCreateNewPlatform -= OnStartGame;
    }

    public void LoseGame()
    {
        GameIsOn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

