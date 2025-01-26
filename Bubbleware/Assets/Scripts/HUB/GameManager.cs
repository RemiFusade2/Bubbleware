using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    public Transform P1BubblesParent;
    public Transform P2BubblesParent;

    public TMP_Text targetScoreText;

    public TimerManager timerManager;

    [Header("Settings")]
    public int targetScore = 5;

    public int player1Score;
    public int player2Score;

    private void Awake()
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

    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
        timerManager.displayPlayerWinsPanel = -1;
        DisplayScores();
    }

    public void PlayerOneWon()
    {
        player1Score++;
        timerManager.displayPlayerWinsPanel = 1;
        AudioManager.Instance.m_globalSfx.PlaySFX(6);
        DisplayScores ();
    }
    public void PlayerTwoWon()
    {
        player2Score++;
        timerManager.displayPlayerWinsPanel = 2;
        AudioManager.Instance.m_globalSfx.PlaySFX(5);
        DisplayScores ();
    }

    private void DisplayScores()
    {
        int scoreCount = 0;
        foreach (Transform P1BubbleChild in P1BubblesParent)
        {
            P1BubbleChild.gameObject.SetActive((scoreCount >= player1Score));
            scoreCount++;
        }
        scoreCount = 0;
        foreach (Transform P2BubbleChild in P2BubblesParent)
        {
            P2BubbleChild.gameObject.SetActive((scoreCount >= player2Score));
            scoreCount++;
        }
    }

    public bool IsGameOver()
    {
        return (player1Score >= targetScore || player2Score >= targetScore);
    }
}
