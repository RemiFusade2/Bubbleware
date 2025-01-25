using System;
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
    public string IntroCutsceneSceneName;
    public string GameHUBScreenSceneName;
    public string OutroCutsceneSceneName;
    public string ScoreScreenSceneName;

    public List<string> MiniGamesSceneNames;

    private Scene TitleScreen;
    private Scene IntroCutscene;
    private Scene GameHUBScreen;
    private Scene OutroCutscene;
    private Scene ScoreScreen;

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

        // Title Screen
        TitleScreen = SceneManager.GetSceneByName(TitleScreenSceneName);
        if (TitleScreen == null || !TitleScreen.isLoaded)
        {
            SceneManager.LoadSceneAsync(TitleScreenSceneName, LoadSceneMode.Additive);
            TitleScreen = SceneManager.GetSceneByName(TitleScreenSceneName);
        }

        // Intro cutscene
        IntroCutscene = SceneManager.GetSceneByName(IntroCutsceneSceneName);
        if (IntroCutscene == null || !IntroCutscene.isLoaded)
        {
            SceneManager.LoadSceneAsync(IntroCutsceneSceneName, LoadSceneMode.Additive);
            IntroCutscene    = SceneManager.GetSceneByName(IntroCutsceneSceneName);
        }

        // Game HUB
        GameHUBScreen = SceneManager.GetSceneByName(GameHUBScreenSceneName);
        if (GameHUBScreen == null || !GameHUBScreen.isLoaded)
        {
            SceneManager.LoadSceneAsync(GameHUBScreenSceneName, LoadSceneMode.Additive);
            GameHUBScreen = SceneManager.GetSceneByName(GameHUBScreenSceneName);
        }

        // Outro cutscene
        OutroCutscene = SceneManager.GetSceneByName(OutroCutsceneSceneName);
        if (OutroCutscene == null || !OutroCutscene.isLoaded)
        {
            SceneManager.LoadSceneAsync(OutroCutsceneSceneName, LoadSceneMode.Additive);
            OutroCutscene = SceneManager.GetSceneByName(OutroCutsceneSceneName);
        }

        // Score screen
        ScoreScreen = SceneManager.GetSceneByName(ScoreScreenSceneName);
        if (ScoreScreen == null || !ScoreScreen.isLoaded)
        {
            SceneManager.LoadSceneAsync(ScoreScreenSceneName, LoadSceneMode.Additive);
            ScoreScreen = SceneManager.GetSceneByName(ScoreScreenSceneName);
        }

        // All mini games
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

        // Disable all objects in all scenes
        HideAllScenes();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShowTitleScreen();
    }

    private void HideAllScenes()
    {
        GameObject[] rootGameObjects;

        // Hide Title screen
        SetAllObjectsInSceneActive(TitleScreen, false);

        // Hide intro cutscene
        SetAllObjectsInSceneActive(IntroCutscene, false);

        // Hide Game HUB
        SetAllObjectsInSceneActive(GameHUBScreen, false);

        // Hide outro cutscene
        SetAllObjectsInSceneActive(OutroCutscene, false);

        // Hide score screen
        SetAllObjectsInSceneActive(ScoreScreen, false);

        // Hide mini games
        foreach (Scene MiniGameScene in MiniGamesSceneList)
        {
            SetAllObjectsInSceneActive(MiniGameScene, false);
        }
    }

    public void ShowTitleScreen()
    {
        HideAllScenes();
        SetAllObjectsInSceneActive(TitleScreen, true);
    }

    public void StartGame()
    {
        GameManager.Instance.ResetScores();
        ShowIntroCutscene();
    }

    private void SetAllObjectsInSceneActive(Scene scene, bool active)
    {
        if (scene.isLoaded)
        {
            GameObject[] rootGameObjects = scene.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                // Do not disable PlayerInputHandlers
                if (!active && gameObject.GetComponent<PlayerInputHandler>() != null)
                {
                    continue;
                }
                gameObject.SetActive(active);
            }
        }
    }

    public void ShowIntroCutscene()
    {
        HideAllScenes();
        SetAllObjectsInSceneActive(IntroCutscene, true);
    }

    public void ShowHUBScreen()
    {
        HideAllScenes();
        SetAllObjectsInSceneActive(GameHUBScreen, true);
    }

    public void ShowOutroCutscene()
    {
        HideAllScenes();
        SetAllObjectsInSceneActive(OutroCutscene, true);
    }

    public void ShowScoreSceen()
    {
        HideAllScenes();
        SetAllObjectsInSceneActive(ScoreScreen, true);
    }


    public void ShowRandomMiniGame()
    {
        HideAllScenes();

        // Pick mini game
        Scene MiniGame = MiniGamesSceneList[UnityEngine.Random.Range(0 , MiniGamesSceneList.Count)];
        SetAllObjectsInSceneActive(MiniGame, true);
    }
}
