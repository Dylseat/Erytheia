using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class Puase : MonoBehaviour {

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
        }

        if (Paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!Paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void Resume()
    {
        Paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Paused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
