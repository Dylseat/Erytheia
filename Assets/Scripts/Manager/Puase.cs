using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class Puase : MonoBehaviour
{

    [SerializeField] GameObject PauseUI;
    private bool Paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (InputManager.Devices[0].Command.WasPressed)
        {
            Paused = !Paused;

            if(Paused)
            {
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                PauseUI.SetActive(false);
                Time.timeScale = 1;
            }

        }
    }

    public void Resume()
    {
        Paused = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene("level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        PauseUI.SetActive(false);
        Paused = false;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
