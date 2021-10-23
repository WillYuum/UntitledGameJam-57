using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;
using Tutorial;


public enum GameState { Paused, Playing, Tutorial };

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    public GameState currentGameState { get; private set; }

    public bool hasPlayedTutorial = false;


    void Awake()
    {
        hasPlayedTutorial = false;
        Invoke(nameof(StartGame), 0.1f);
    }

    public void StartGame()
    {
        Debug.Log("Started Game");
        TutorialManager.instance.StartTutorial();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

