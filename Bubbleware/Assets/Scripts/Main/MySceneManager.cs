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

    public List<string> MiniGamesSceneNames;

    private Scene TitleScreen;
    private Scene GameHUBScreen;

    private List<Scene> MiniGamesSceneList;

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

        MiniGamesSceneList = new List<Scene>();
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
        GameHUBScreen = SceneManager.GetSceneByName(GameHUBScreenSceneName);
        if (GameHUBScreen == null || !GameHUBScreen.isLoaded)
        {
            SceneManager.LoadSceneAsync(GameHUBScreenSceneName, LoadSceneMode.Additive);
            GameHUBScreen = SceneManager.GetSceneByName(GameHUBScreenSceneName);
        }
        foreach (string miniGameSceneName in MiniGamesSceneNames)
        {
            Scene MiniGameScene = SceneManager.GetSceneByName(miniGameSceneName);
            if (MiniGameScene == null || !MiniGameScene.isLoaded || !MiniGamesSceneList.Contains(MiniGameScene))
            {
                SceneManager.LoadSceneAsync(miniGameSceneName, LoadSceneMode.Additive);
                MiniGameScene = SceneManager.GetSceneByName(miniGameSceneName);
                MiniGamesSceneList.Add(MiniGameScene);
            }
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
        foreach (Scene MiniGameScene in MiniGamesSceneList)
        {
            if (MiniGameScene.isLoaded)
            {
                rootGameObjects = MiniGameScene.GetRootGameObjects();
                foreach (GameObject gameObject in rootGameObjects)
                {
                    gameObject.SetActive(false);
                }
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

    public void ShowRandomMiniGame()
    {
        HideAllScenes();

        // Pick mini game
        Scene MiniGame = MiniGamesSceneList[Random.Range(0 , MiniGamesSceneList.Count)];
        if (MiniGame.isLoaded)
        {
            GameObject[] rootGameObjects = MiniGame.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
