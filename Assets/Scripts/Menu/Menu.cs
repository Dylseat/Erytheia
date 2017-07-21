using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
       
    }
    public void Play(string sceneName)
    {
        SceneManager.LoadScene("level1");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Crédit");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
