using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

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
        DisplayScores();
    }

    public void PlayerOneWon()
    {
        player1Score++;
        DisplayScores();
    }
    public void PlayerTwoWon()
    {
        player2Score++;
        DisplayScores();
    }

    private void DisplayScores()
    {
        player1ScoreText.text = $"Score P1: {player1Score}";
        player2ScoreText.text = $"Score P2: {player2Score}";
    }

    public bool IsGameOver()
    {
        return (player1Score >= targetScore || player2Score >= targetScore);
    }
}
