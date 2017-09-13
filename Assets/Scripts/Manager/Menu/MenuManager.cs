using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.State = GameManager.States.InMenu;
    }

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Quit();
        }
    }

    // Load Scene with the loader
    public void LoadScene(string sceneName)
    {
        LoadingScreen.instance.Load(sceneName);
    }

    public void LoadScene(int sceneNumber)
    {
        LoadingScreen.instance.Load(sceneNumber);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
