using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("Options")]
    [SerializeField]
    private string LoadSceneName = "Load";

    [SerializeField]
    private float timeForScreen = 0.75f;

    [SerializeField]
    private float timeForBar = 0.25f;

    // CanvasGroup who appart in the loading screen
    [SerializeField]
    private CanvasGroup canvasGroup;

    // Material who maske the screen
    [SerializeField]
    private Material cameraMaterial;

    private static Slider slider;

    // Instance of the singeton LoadingScreen
    public static LoadingScreen instance;

    private static bool isLoading = false;

    private static AsyncOperation asyncOperation;

    private static int nextSceneNumber = -1;
    private static string nextSceneName = null;

    void Awake()
    {
        if(instance)
        {
            Destroy(this);
            return;
        }
        instance = this;
        enabled = false;

        transform.position = new Vector3(0.5f, 0.5f, 0.0f);

        slider = GetComponentInChildren<Slider>();

        cameraMaterial.SetFloat("_Cutoff", 0);

        DontDestroyOnLoad(this);
    }

    // Load Scene with loading screen
    public void Load(int sceneNumber)
    {
        if(!instance || isLoading)
            return;

        nextSceneNumber = sceneNumber;
        nextSceneName = null;
        StartCoroutine(LoadingOperation());
    }

    // Load Scene with loading screen
    public void Load(string sceneName)
    {
        if(!instance || isLoading)
            return;

        nextSceneNumber = -1;
        nextSceneName = sceneName;
        StartCoroutine(LoadingOperation());
    }

    IEnumerator LoadingOperation()
    {
        // Preparation
        isLoading = true;
        float timer = 0;
        float pourcent = 0;

        // Waits for animation to hide current screen.
        while(isLoading)
        {
            timer += Time.deltaTime;
            pourcent = timer / timeForScreen;

            // Animations
            cameraMaterial.SetFloat("_Cutoff", pourcent);

            if(pourcent >= 1)
                break;

            yield return null;
        }

        if(canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);
            timer = 0;
            pourcent = 0;

            // Reveals the UI of the Load
            while(isLoading)
            {
                timer += Time.deltaTime;
                pourcent = timer / timeForBar;

                // Animations

                canvasGroup.alpha = pourcent;

                if(pourcent >= 1)
                    break;

                yield return null;
            }
        }

        // Loading the transition scene
        asyncOperation = SceneManager.LoadSceneAsync(LoadSceneName);

        // Waits for the loading of the transition scene
        while(isLoading)
        {
            if(asyncOperation.isDone)
                break;

            yield return null;
        }


        // Loading the real scene
        if(nextSceneNumber != -1)
        {
            asyncOperation = SceneManager.LoadSceneAsync(nextSceneNumber);
        }
        else if(nextSceneName != null)
        {
            asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);
        }
        else
        {
            StopAllCoroutines();
            Debug.LogError("No scene name or number valid");
        }

        while(isLoading)
        {
            if(slider)
            {
                slider.value = asyncOperation.progress;
            }

            yield return null;
            if(asyncOperation.isDone)
                break;
        }

        if(canvasGroup)
        {
            timer = timeForBar;
            pourcent = 0;

            // Remove the UI from the Load
            while(isLoading)
            {
                timer -= Time.deltaTime;
                pourcent = timer / timeForBar;

                // Animations
                canvasGroup.alpha = pourcent;

                if(timer <= 0)
                    break;

                yield return null;
            }

            canvasGroup.gameObject.SetActive(false);
        }

        timer = timeForScreen;
        pourcent = 0;

        cameraMaterial.SetFloat("_Cutoff", 1);

        // Disappear animation of the background of loading
        while(isLoading)
        {
            timer -= Time.deltaTime;
            pourcent = timer / timeForScreen;

            //animations
            cameraMaterial.SetFloat("_Cutoff", pourcent);

            if(timer <= 0)
                isLoading = false;

            yield return null;
        }

        cameraMaterial.SetFloat("_Cutoff", 0);
    }
}