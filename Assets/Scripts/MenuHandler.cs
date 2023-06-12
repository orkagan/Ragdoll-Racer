using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Playing,
    Paused,
    Win
}
public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameState gameState;
    public GameState CurrentGameState
    {
        get
        {
            return gameState;
        }
        set
        {
            gameState = value;
            CheckPaused();
            SwitchPanels(gameState);
        }
    }
    //public Dictionary<GameState, GameObject> panels;
    [Serializable]
    public struct Element
    {
        public GameState panelState;
        public GameObject GameObj;
    }
    public Element[] elements;

    #region Singleton Setup
    //Staticly typed property setup for EnemySpawner.Instance
    private static MenuHandler _instance;
    public static MenuHandler Instance
    {
        get => _instance;
        private set
        {
            //check if instance of this class already exists and if so keep orignal existing instance
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.Log($"{nameof(MenuHandler)} instance already exists, destroy the duplicate!");
                Destroy(value);
            }
        }
    }
    private void Awake()
    {
        Instance = this; //sets the static class instance
    }
    #endregion

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (CurrentGameState==GameState.Playing)
            {
                CurrentGameState = GameState.Paused;
            }
            else if (CurrentGameState == GameState.Paused)
            {
                CurrentGameState = GameState.Playing;
            }
        }
    }

    void CheckPaused()
    {
        switch (CurrentGameState)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.Win:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }

    public void Win()
    {
        CurrentGameState = GameState.Win;
    }
    public void SwitchPanels(GameState state)
    {
        foreach (Element item in elements)
        {
            if (item.panelState == CurrentGameState)
            {
                item.GameObj.SetActive(true);
            }
            else
            {
                item.GameObj.SetActive(false);
            }
        }
    }
    public void NextScene()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Load the next scene after the current scene (last scene loops back around to first)
        SceneManager.LoadScene((currentScene.buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void Retry()
    {
        //reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("ExitGame attempted.");
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
