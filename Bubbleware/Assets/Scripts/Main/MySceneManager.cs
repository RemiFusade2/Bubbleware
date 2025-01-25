using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;

    [Header("Settings")]
    public bool loadAllScenesAtStart;

    public string TitleScreenSceneName;
    public string GameHUBScreenSceneName;

    private Scene TitleScreen;
    private Scene GameHUBScreen;

    void Awake()
    {
        // Does another instance already exist?
        if (Instance && Instance != this)
        {
            // Destroy myself
            Destroy(gameObject);
            return;
        }

        // Otherwise store my reference and make me DontDestroyOnLoad
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (loadAllScenesAtStart)
        {
            LoadScenes();
        }
    }

    private void LoadScenes()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        TitleScreen = SceneManager.GetSceneByName(TitleScreenSceneName);
        if (TitleScreen == null || !TitleScreen.isLoaded)
        {
            SceneManager.LoadSceneAsync(TitleScreenSceneName, LoadSceneMode.Additive);
            TitleScreen = SceneManager.GetSceneByName(TitleScreenSceneName);
        }
        if (GameHUBScreen == null || !GameHUBScreen.isLoaded)
        {
            SceneManager.LoadSceneAsync(GameHUBScreenSceneName, LoadSceneMode.Additive);
            GameHUBScreen = SceneManager.GetSceneByName(GameHUBScreenSceneName);
        }
        HideAllScenes();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        ShowTitleScreen();
    }

    private void HideAllScenes()
    {
        GameObject[] rootGameObjects;
        if (TitleScreen.isLoaded)
        {
            rootGameObjects = TitleScreen.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.SetActive(false);
            }
        }
        if (GameHUBScreen.isLoaded)
        {
            rootGameObjects = GameHUBScreen.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ShowTitleScreen()
    {
        HideAllScenes();
        if (TitleScreen.isLoaded)
        {
            GameObject[] rootGameObjects = TitleScreen.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void ShowHUBScreen()
    {
        HideAllScenes();
        if (GameHUBScreen.isLoaded)
        {
            GameObject[] rootGameObjects = GameHUBScreen.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
