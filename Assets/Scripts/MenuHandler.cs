using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum GameState
{
    MainMenu,
    LevelSelect,
    Options,
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
            SwitchPanels(gameState);
        }
    }
    //public Dictionary<GameState, GameObject> panels;
    [Serializable]
    public struct Element
    {
        public GameState panelState;
        public GameObject panelGameObj;
        public bool pauses;
    }
    /// <summary>
    /// Stores the UI panel associated with a state and if it should pause the game.
    /// </summary>
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
                item.panelGameObj.SetActive(true);
                Time.timeScale = item.pauses ? 0 : 1;
            }
            else
            {
                item.panelGameObj.SetActive(false);
            }
        }
    }
    public static void NextScene()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Load the next scene after the current scene (last scene loops back around to first)
        SceneManager.LoadScene((currentScene.buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void Retry()
    {
        //reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public static void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("ExitGame attempted.");
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
