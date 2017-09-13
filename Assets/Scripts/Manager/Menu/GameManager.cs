using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private GameManager() { }

    //Instance of the singeton GameManager
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    // Possible states of the game
    public enum States
    {
        Other,
        InMenu,
        InGame,
        InLoading
    }

    private static States state = States.InMenu;
    // Actual state of the game

    // Make the transition from one state to another
    public static States State
    {
        get
        {
            return state;
        }

        set
        {
            if(state == value)
            {
                return;
            }

            switch(value)
            {
                case States.Other:
                    Cursor.visible = false;
                    break;
                case States.InMenu:
                    Cursor.visible = true;
                    break;
                case States.InGame:
                    Cursor.visible = false;
                    break;
                case States.InLoading:
                    Cursor.visible = false;
                    break;
                default:
                    Cursor.visible = false;
                    break;
            }
            state = value;
        }
    }

    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Load Scene with the loader
    public void LoadScene(string sceneName)
    {
        PauseManager.IsPause = false;
        LoadingScreen.instance.Load(sceneName);
    }

    // Load Scene with the loader
    public void LoadScene(int sceneNumber)
    {
        PauseManager.IsPause = false;
        LoadingScreen.instance.Load(sceneNumber);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
