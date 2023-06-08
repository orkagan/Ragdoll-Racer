using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static bool paused;
    public GameObject PausePanel;
    public GameObject GameHUD;

    void Start()
    {
        paused = false;
        CheckPaused();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            CheckPaused();
        }
    }

    void CheckPaused()
    {
        if (paused)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            GameHUD.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
            GameHUD.SetActive(true);
        }
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
