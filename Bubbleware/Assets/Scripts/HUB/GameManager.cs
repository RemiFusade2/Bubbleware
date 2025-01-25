using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    public TMP_Text targetScoreText;

    public TimerManager timerManager;

    [Header("Settings")]
    public int targetScore = 5;

    private int player1Score;
    private int player2Score;

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
        DisplayScores();
    }
    public void PlayerTwoWon()
    {
        player2Score++;
        timerManager.displayPlayerWinsPanel = 2;
        DisplayScores();
    }

    private void DisplayScores()
    {
        player1ScoreText.text = $"P1\n{player1Score}";
        player2ScoreText.text = $"P2\n{player2Score}";
        targetScoreText.text = $"{targetScore} point to win";
    }

    public bool IsGameOver()
    {
        return (player1Score >= targetScore || player2Score >= targetScore);
    }
}
