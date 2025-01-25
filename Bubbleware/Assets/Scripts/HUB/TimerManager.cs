using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;

    public int startTime = 3;

    private int timer;

    private Coroutine DecrementTimerCoroutine;

    private void OnEnable()
    {
        if (GameManager.Instance.IsGameOver())
        {
            // Game is over, we show outro cutscene
            MySceneManager.Instance.ShowOutroCutscene();
        }
        else
        {
            // Game is not over, we show timer
            timer = startTime;
            DisplayTimerText();
            DecrementTimerCoroutine = StartCoroutine(DecrementTimerAsync(1));
        }
    }


    private void DisplayTimerText()
    {
        timerText.text = timer.ToString("0");
    }

    private IEnumerator DecrementTimerAsync(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        timer--;
        DisplayTimerText();
        if (timer <= 0)
        {
            StartNewMiniGame();
        }
        else
        {
            DecrementTimerCoroutine = StartCoroutine(DecrementTimerAsync(1));
        }
    }

    private void StartNewMiniGame()
    {
        MySceneManager.Instance.ShowRandomMiniGame();
    }
}
