using UnityEngine;
using Utils.GenericSingletons;
using Tutorial;
using UnityEngine.SceneManagement;


public enum GameState { Paused, Playing, Tutorial };

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public GameState currentGameState { get; private set; }

    public bool hasPlayedTutorial = false;

    [SerializeField] private GameObject YouWonGameText;
    public bool GameIsOn { get; private set; }

    void Awake()
    {
        hasPlayedTutorial = false;
        // Invoke(nameof(StartGame), 0.1f);
        YouWonGameText.SetActive(false);
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

    public void WinGame()
    {
        print("Won game");
        YouWonGameText.SetActive(true);
        Invoke(nameof(Restart), 2.5f);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

